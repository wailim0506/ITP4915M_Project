﻿using System;
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

        private bool IsAccountActive(string status) => status.Equals("active");

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

        private string GetIsLmDataQuery(string UID) => $"SELECT isLM from customer_account WHERE customerID = '{UID}'";

        private void UserInfo()
        {
            var dt = ExecuteSqlQuery(GetUserInfoDataQuery(UserID));

            lastName = dt.Rows[0]["lastName"].ToString();
            firstName = dt.Rows[0]["firstName"].ToString();
            accountID = dt.Rows[0]["accountID"].ToString();
            AccountType = GetAccountType(UserID);
        }

        private string GetUserInfoDataQuery(string UID) => UID.StartsWith("LMC")
            ? $"SELECT customerAccountID AS accountID, firstName, lastName FROM customer C, customer_account CA WHERE CA.customerID = '{UID}' AND C.customerID = '{UID}'"
            : $"SELECT staffAccountID AS accountID, firstName, lastName FROM staff S, staff_account SA WHERE SA.staffID = '{UID}' AND S.staffID = '{UID}'";

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
        public DataTable GetCustomerDetail(string id) =>
            ExecuteSqlQuery($"SELECT * FROM customer WHERE customerID = '{id}'");

        //User's information for UI
        public string GetName() => lastName + " " + firstName;
        public virtual string GetUid() => UserID;
        public string GetAccountType() => AccountType;

        public string GetAccountType(string UID) => UID.StartsWith("LMC")
            ? "Customer"
            : "Staff";

        public bool GetIsLm() => isLM;
        private DataTable ExecuteSqlQuery(string sqlQuery) => db.ExecuteDataTable(sqlQuery);

        public bool CheckIsManager() =>
            ExecuteSqlQuery($"SELECT jobTitle FROM staff WHERE staffID = '{UserID}'").Rows[0][0].ToString() ==
            "Sales Manager";

        public bool CheckIsDeliverman() =>
            ExecuteSqlQuery($"SELECT jobTitle FROM staff WHERE staffID = '{UserID}'").Rows[0][0].ToString() ==
            "Deliverman";

        public bool CheckIsStoreman() =>
            ExecuteSqlQuery($"SELECT jobTitle FROM staff WHERE staffID = '{UserID}'").Rows[0][0].ToString() ==
            "Storeman";

        private string GetJobTit(string UID)
        {
            var dt = ExecuteSqlQuery($"SELECT jobTitle FROM staff WHERE staffID = \'{UID}\'");

            return dt.Rows[0]["jobTitle"].ToString();
        }

        private string GetMessageFormat(string UID)
        {
            string MSGID = null;

            //Select the correct message format.
            if (UID.StartsWith("LMS"))
            {
                switch (GetJobTit(UID))
                {
                    case "Storeman":
                        MSGID = "MSG01";
                        break;
                    case "Manager":
                        MSGID = "MSG05";
                        break;
                    case "Order Processing Clerk":
                        MSGID = "MSG03";
                        break;
                    case "Deliverman":
                        MSGID = "MSG06";
                        break;
                    case "Sales Manager":
                        MSGID = "MSG07";
                        break;
                    case "Goods Inward Staff":
                        MSGID = "MSG04";
                        break;

                }
            }
            else
                MSGID = "MSG02";

            DataTable dt = new DataTable();

            sqlStr = $"SELECT content FROM message WHERE msgID = \'{MSGID}\'";

            dt = db.ExecuteDataTable(sqlStr);

            return dt.Rows[0]["content"].ToString();
        }

        public string GetMessage()
        {   
            DataTable dt = new DataTable();
            string message = null;
            string horizontialLine =
                    "\n------------------------------------" +
                    "--------------------------------------" +
                    "--------------------------------------------------\n";

            //Select the correct date format.
            if (UserID.StartsWith("LMS"))
            {
                switch (GetJobTit(UserID))
                {
                    case "Storeman":
                        sqlStr = "SELECT partNumber, quantity FROM spare_part WHERE quantity < dangerLevel";
                        break;
                    case "Manager":
                        sqlStr = "SELECT ( SELECT COUNT(*) FROM staff ) AS staff, ( SELECT COUNT(*) FROM customer ) AS customer FROM dual";
                        break;
                    case "Order Processing Clerk":
                        sqlStr = "SELECT orderID, orderDate FROM order_ WHERE status = 'Pending'";
                        break;
                    case "Deliverman":
                        sqlStr = "SELECT OD.orderID, SD.shippingAddress FROM order_ OD, shipping_detail SD WHERE OD.status = 'Ready to Ship' AND OD.orderID = SD.orderID";
                        break;
                    case "Sales Manager":
                        sqlStr = "SELECT itemID FROM product WHERE onSaleQty = 0 OR LM_onSaleQty = 0";
                        break;
                    case "Goods Inward Staff":
                        sqlStr = "SELECT reorderID, partNumber, quantity FROM reorder_request WHERE status = 'processing'";
                        break;

                }
            }
            else
                sqlStr = $"SELECT O.orderID, O.status FROM order_ O, customer_account CA WHERE O.customerAccountID = CA.customerAccountID AND CA.customerID = \'{UserID}\'";

            dt = db.ExecuteDataTable(sqlStr);

            if (dt.Rows.Count > 0)
            {
                //Get message format.
                message = GetMessageFormat(UserID);

                //Fill data.
                int index = dt.Rows.Count - 1;
                for (int i = 0; i <= index; i++)
                {
                    if (UserID.StartsWith("LMS"))
                    {
                        switch (GetJobTit(UserID))
                        {
                            case "Storeman":
                                message = message.Replace("[SP]", dt.Rows[i]["partNumber"].ToString())
                                    .Replace("[Qty]", dt.Rows[i]["quantity"].ToString());
                                break;
                            case "Manager":
                                message = message.Replace("[NS]", dt.Rows[i]["staff"].ToString())
                                    .Replace("[NC]", dt.Rows[i]["customer"].ToString());
                                break;
                            case "Order Processing Clerk":
                                message = message.Replace("[OID]", dt.Rows[i]["orderID"].ToString())
                                    .Replace("[OD]", ((DateTime)dt.Rows[i]["orderDate"]).ToString("yyyy/MM/dd"));
                                break;
                            case "Deliverman":
                                message = message.Replace("[OID]", dt.Rows[i]["orderID"].ToString())
                                    .Replace("[SA]", dt.Rows[i]["shippingAddress"].ToString());
                                break;
                            case "Sales Manager":
                                message = message.Replace("[IID]", dt.Rows[i]["itemID"].ToString());
                                break;
                            case "Goods Inward Staff":
                                message = message.Replace("[OID]", dt.Rows[i]["reorderID"].ToString())
                                    .Replace("[PN]", dt.Rows[i]["partNumber"].ToString())
                                    .Replace("[Qty]", dt.Rows[i]["quantity"].ToString());
                                break;

                        }
                    }
                    else
                        message = message.Replace("[OID]", dt.Rows[i]["orderID"].ToString())
                                    .Replace("[S]", dt.Rows[i]["status"].ToString());

                    message += horizontialLine;

                    if (!(i + 1 > index))    //Add next if have.
                        message += GetMessageFormat(UserID);
                }
            }
            return message;
        }
    }
}