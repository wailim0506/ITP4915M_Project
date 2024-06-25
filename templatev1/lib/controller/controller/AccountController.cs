using System;
using System.Data;
using System.Windows.Forms;
using controller.Utilities;
using Microsoft.Extensions.Logging;

namespace controller
{
    public class AccountController : abstractController
    {
        public bool IsLogin;
        private string accountID, firstName, lastName, UserID, AccountType, sqlStr;
        private Boolean isLM;
        UIController UIController;
        private readonly Database db;
        private readonly Validator validator = new Validator();

        public AccountController(Database db = null)
        {
            IsLogin = false;
            isLM = false;
            this.db = db ?? new Database();
        }

        public bool Login(string UID, string Pass, UIController UI)
        {
            try
            {
                // avoid sql injection
                if (validator.IsValidUsername(UID) == false || validator.IsValidPassword(Pass) == false)
                {
                    Log.LogMessage(LogLevel.Critical, "AccountController",
                        $"Login method User id: {UID} Password : {Pass} is not valid.");
                    return false;
                }

                var dt = ExecuteSqlQuery(GetAccountDataQuery(UID));

                // Account not found
                if (dt.Rows.Count < 1)
                {
                    Log.LogMessage(LogLevel.Debug, "AccountController",
                        $"Login method User id: {UID} Account not found.");
                    return false;
                }

                if (IsPasswordValid(Pass, dt.Rows[0]["password"].ToString()) &&
                    IsAccountActive(dt.Rows[0]["status"].ToString()))
                {
                    SetLoginStatus(UID, UI);
                }

                return IsLogin;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private string GetAccountDataQuery(string UID)
        {
            return UID.StartsWith("LMC") || UID.StartsWith("LMS")
                ? $"SELECT password, status FROM customer_account WHERE customerID = '{UID}' " +
                  $"UNION ALL SELECT password, status FROM staff_account WHERE staffID = '{UID}'"
                : throw new Exception("Not a LM account.");
        }

        // check if the password is valid
        private bool IsPasswordValid(string inputPassword, string storedPassword)
        {
            bool isValid = BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);
            if (!isValid)
            {
                Log.LogMessage(LogLevel.Debug, "AccountController",
                    $"Login method User id: {UserID} Password : {inputPassword} not valid with stored password : {storedPassword}.");
            }

            return isValid;
        }

        private bool IsAccountActive(string status)
        {
            return status.Equals("active");
        }

        private void SetLoginStatus(string UID, UIController UI)
        {
            try
            {
                var dt = ExecuteSqlQuery(GetIsLmDataQuery(UID));
                if (dt.Rows[0][0].ToString() == "Y")
                {
                    isLM = true;
                }
            }
            catch (Exception e)
            {
                //if it is a staff account, it will not have data on dt.Rows[0][0]
                //ignore it
            }


            IsLogin = true;
            UserID = UID;
            UIController = UI;
            UserInfo();
            UIController.SetPermission(UserID);
        }

        private string GetIsLmDataQuery(string UID)
        {
            return $"SELECT isLM from customer_account WHERE customerID = \'{UID}\'";
        }


        private void UserInfo()
        {
            var dt = ExecuteSqlQuery(GetUserInfoDataQuery(UserID));

            lastName = dt.Rows[0]["lastName"].ToString();
            firstName = dt.Rows[0]["firstName"].ToString();
            accountID = dt.Rows[0]["accountID"].ToString();
            AccountType = GetAccountType(UserID);
        }

        private string GetUserInfoDataQuery(string UID)
        {
            return UID.StartsWith("LMC")
                ? $"SELECT customerAccountID AS accountID, firstName, lastName FROM customer C, customer_account CA WHERE CA.customerID = '{UID}' AND C.customerID = '{UID}'"
                : $"SELECT staffAccountID AS accountID, firstName, lastName FROM staff S, staff_account SA WHERE SA.staffID = '{UID}' AND S.staffID = '{UID}'";
        }

        //update the login record into the database.
        public void SetLog(string Date)
        {
            var query = UserID.StartsWith("LMC")
                ? $"INSERT INTO customer_login_history VALUES('{accountID}', '{Date}')"
                : $"INSERT INTO staff_login_history VALUES('{accountID}', '{Date}')";

            db.ExecuteNonQueryCommand(query, null);
        }

        //Return the last login date time.
        public string GetLog()
        {
            var dt = ExecuteSqlQuery(
                $"SELECT loginDate FROM customer_login_history WHERE customerAccountID = '{accountID}' " +
                $"UNION ALL SELECT loginDate FROM staff_login_history WHERE staffAccountID = '{accountID}' ORDER BY loginDATE DESC");

            return dt.Rows[0]["loginDate"].ToString();
        }

        //Return the full login record.
        public DataTable GetFullLog()
        {
            return ExecuteSqlQuery(
                $"SELECT loginDate FROM customer_login_history WHERE customerAccountID = '{accountID}' " +
                $"UNION ALL SELECT loginDate FROM staff_login_history WHERE staffAccountID = '{accountID}' ORDER BY loginDate DESC");
        }

        //Return the last password change date.
        public DateTime GetPwdChange()
        {
            var dt = ExecuteSqlQuery(
                $"SELECT pwdChangeDate FROM customer_account WHERE customerAccountID = '{accountID}' " +
                $"UNION ALL SELECT pwdChangeDate FROM staff_account WHERE staffAccountID = '{accountID}'");

            return (DateTime)dt.Rows[0]["pwdChangeDate"];
        }


        //For the password change function in the profile.
        public bool MatchPwd(string gettedPwd)
        {
            var dt = ExecuteSqlQuery(GetAccountDataQuery(UserID));
            return IsPasswordValid(gettedPwd, dt.Rows[0]["password"].ToString());
        }

        //For customer account disable function.
        public bool DelAccount()
        {
            try
            {
                db.ExecuteNonQueryCommand(
                    $"UPDATE customer_account SET Status = 'disable' WHERE customerAccountID = '{accountID}'", null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable GetStaffDetail(string id)
        {
            var dt = ExecuteSqlQuery($"SELECT staffID FROM staff_account WHERE staffAccountID = '{id}'");
            var staffID = dt.Rows[0][0].ToString();
            return ExecuteSqlQuery($"SELECT * FROM staff WHERE staffID = '{staffID}'");
        }

        //use in viewOrderController   //id = customerID
        public DataTable GetCustomerDetail(string id)
        {
            return ExecuteSqlQuery($"SELECT * FROM customer WHERE customerID = '{id}'");
        }


        //User's information for UI
        public string GetName()
        {
            return lastName + " " + firstName;
        }

        public string GetUid()
        {
            return UserID;
        }

        public string GetAccountType()
        {
            return AccountType;
        }

        public string GetAccountType(string UID)
        {
            return UID.StartsWith("LMC")
                ? "Customer"
                : "Staff";
        }

        public bool GetIsLm()
        {
            return isLM;
        }

        private DataTable ExecuteSqlQuery(string sqlQuery)
        {
            return db.ExecuteDataTable(sqlQuery);
        }

        public bool CheckIsManager()
        {
            if (ExecuteSqlQuery($"SELECT jobTitle FROM staff WHERE staffID = \'{UserID}\'").Rows[0][0].ToString() ==
                "Sales Manager")
            {
                return true;
            }

            return false;
        }

        public bool checkIsDeliverman()
        {
            if (ExecuteSqlQuery($"SELECT jobTitle FROM staff WHERE staffID = \'{UserID}\'").Rows[0][0].ToString() ==
                "Deliverman")
            {
                return true;
            }

            return false;
        }

        public string GetMessage()
        {
            string Message = "There is a spare part that achieves the danger level. Please handle it as soon as possible." +
                "\nSpare part number: [SP]" +
                "\nCurrent quantity: [Qty]";
            string horizontialLine = "\n----------------------------------------------------------------------------------------------------------------------------\n";




            DataTable dt = new DataTable();

            sqlStr = "SELECT partNumber, quantity FROM spare_part WHERE quantity < dangerLevel";


            dt = db.ExecuteDataTable(sqlStr);


            int index = dt.Rows.Count - 1;

            for (int i = 0; i <= index; i++)
            { 
            Message = Message.Replace("[SP]", dt.Rows[i]["partNumber"].ToString()).Replace("[Qty]", dt.Rows[i]["quantity"].ToString());

            Message += horizontialLine;

            if(!(i+1 > index))
                    Message += "There is a spare part that achieves the danger level. Please handle it as soon as possible." +
                "\nSpare part number: [SP]" +
                "\nCurrent quantity: [Qty]";




            }

            return Message;
            



        }







        public bool checkIsStoreman()
        {
            if (ExecuteSqlQuery($"SELECT jobTitle FROM staff WHERE staffID = \'{UserID}\'").Rows[0][0].ToString() ==
                "Storeman")
            {
                return true;
            }

            return false;
        }
    }
}