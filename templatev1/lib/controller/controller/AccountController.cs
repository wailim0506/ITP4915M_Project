using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using System.Windows.Forms;

namespace controller
{
    public class AccountController : abstractController
    {
        private string sqlStr;
        private MySqlCommand cmd;
        public bool IsLogin;
        private string accountID, firstName, lastName, UserID, AccountType;
        private Boolean isLM;
        UIController UIController;

        public AccountController()
        {
            IsLogin = false;
            isLM = false;
        }

        public bool Login(string UID, string Pass, UIController UI)
        {
            try
            {
                DataTable dt = ExecuteSqlQuery(GetAccountDataQuery(UID));

                if (dt.Rows.Count < 1) //Account NOT found.
                    return false;

                //Account status is active AND verify the password.
                if (IsPasswordValid(Pass, dt.Rows[0]["password"].ToString()) &&
                    IsAccountActive(dt.Rows[0]["status"].ToString()))
                {
                    SetLoginStatus(UID, UI);
                }

                return IsLogin;
            }
            catch (Exception e)
            {
                return false; //Some error occurs return false to login.
            }
        }

        private string GetAccountDataQuery(string UID)
        {
            return UID.StartsWith("LMC") || UID.StartsWith("LMS")
                ? $"SELECT password, status FROM customer_account WHERE customerID = '{UID}' " +
                  $"UNION ALL SELECT password, status FROM staff_account WHERE staffID = '{UID}'"
                : throw new Exception("Not a LM account.");
        }

        private bool IsPasswordValid(string inputPassword, string storedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);
        }

        private bool IsAccountActive(string status)
        {
            return status.Equals("active");
        }

        private void SetLoginStatus(string UID, UIController UI)
        {
            DataTable dt = ExecuteSqlQuery(GetIsLmDataQuery(UID));
            if (dt.Rows[0][0].ToString() == "Y")
            {
                isLM = true;
            }

            IsLogin = true;
            UserID = UID;
            UIController = UI;
            UserInfo(); //Get user info and store in global variable.
            UIController.setPermission(UserID);
        }

        private string GetIsLmDataQuery(string UID)
        {
            return $"SELECT isLM from customer_account WHERE customerID = \'{UID}\' ";
        }

        private DataTable ExecuteSqlQuery(string sqlQuery)
        {
            DataTable dt = new DataTable();
            adr = new MySqlDataAdapter(sqlQuery, conn);
            adr.Fill(dt);
            adr.Dispose();
            return dt;
        }

        private void UserInfo()
        {
            DataTable dt = GetUserInfoData(UserID);

            lastName = dt.Rows[0]["lastName"].ToString();
            firstName = dt.Rows[0]["firstName"].ToString();
            accountID = dt.Rows[0]["accountID"].ToString();
        }

        private DataTable GetUserInfoData(string UID)
        {
            string sqlQuery = UID.StartsWith("LMC")
                ? $"SELECT customerAccountID AS accountID, firstName, lastName FROM customer C, customer_account CA WHERE CA.customerID = '{UID}' AND C.customerID = '{UID}'"
                : $"SELECT staffAccountID AS accountID, firstName, lastName FROM staff S, staff_account SA WHERE SA.staffID = '{UID}' AND S.staffID = '{UID}'";

            return ExecuteSqlQuery(sqlQuery);
        }

        //update the login record into the database.
        public void SetLog(string Date)
        {
            conn.Open();

            if (UserID.StartsWith("LMC"))
                sqlStr = $"INSERT INTO customer_login_history VALUES(\'{accountID}\', \'{Date}\')";
            else
                sqlStr = $"INSERT INTO staff_login_history VALUES(\'{accountID}\', \'{Date}\')";

            cmd = new MySqlCommand(sqlStr, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Retuen the last login date time.
        public string GetLog()
        {
            DataTable dt = new DataTable();

            sqlStr = $"SELECT loginDate FROM customer_login_history WHERE customerAccountID = \'{accountID}\' " +
                     $"UNION ALL SELECT loginDate FROM staff_login_history WHERE staffAccountID = \'{accountID}\' ORDER BY loginDATE DESC";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            conn.Close();

            return dt.Rows[0]["loginDate"].ToString();
        }

        //Return the full login record.
        public DataTable GetFullLog()
        {
            DataTable dt = new DataTable();

            sqlStr = $"SELECT loginDate FROM customer_login_history WHERE customerAccountID = \'{accountID}\' " +
                     $"UNION ALL SELECT loginDate FROM staff_login_history WHERE staffAccountID = \'{accountID}\' ORDER BY loginDate DESC";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            conn.Close();

            return dt;
        }

        //Return the last password chagne date.
        public DateTime GetPwdChange()
        {
            DataTable dt = new DataTable();

            sqlStr = $"SELECT pwdChangeDate FROM customer_account WHERE customerAccountID = \'{accountID}\' " +
                     $"UNION ALL SELECT pwdChangeDate FROM staff_account WHERE staffAccountID = \'{accountID}\'";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            conn.Close();

            return (DateTime)dt.Rows[0]["pwdChangeDate"];
        }

        public DataTable GetAccountData(string UID) => ExecuteSqlQuery(GetAccountDataQuery(UID));

        //For the password change function in the profile.
        public bool MatchPwd(string gettedPwd)
        {
            if (BCrypt.Net.BCrypt.Verify(gettedPwd, GetAccountData(UserID).Rows[0]["password"].ToString()))
                return true;
            else
                return false;
            // return BCrypt.Net.BCrypt.Verify(BCrypt.Net.BCrypt.HashPassword(gettedPwd), decryptedPassword);
        }

        //For customer account disable function.
        public bool DelAccount()
        {
            try
            {
                //Insert a record into customer table.
                conn.Open();
                sqlStr = $"UPDATE customer_account SET Status = 'disable' WHERE customerAccountID = \'{accountID}\'";
                cmd = new MySqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false; //Something went wrong.
            }
        }


        public DataTable GetStaffDetail(string id) //use in OrderListController     //id = staff account id
        {
            DataTable dt = new DataTable();
            string sqlCmd = $"SELECT staffID FROM staff_account WHERE staffAccountID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string staffID = dt.Rows[0][0].ToString();
            //get detail
            sqlCmd = $"SELECT * FROM staff WHERE staffID = \'{staffID}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            dt = new DataTable();
            adr.Fill(dt);
            return dt;
        }

        public DataTable GetCustomerDetail(string id) //use in viewOrderController   //id = customerID
        {
            DataTable dt = new DataTable();
            string sqlCmd = $"SELECT * FROM customer WHERE customerID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
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

        public Boolean GetIsLm()
        {
            return isLM;
        }
    }
}