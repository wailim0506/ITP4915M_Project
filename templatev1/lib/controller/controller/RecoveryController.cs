﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using LMCIS.controller.Utilities;
using Microsoft.Extensions.Logging;

namespace LMCIS.controller
{
    public class RecoveryController : abstractController
    {
        private Database db;

        public string UID;
        private string email, phone;
        private string sqlStr;

        AccountController _accountController;
        private Validator _validator;

        public RecoveryController()
        {
            db = new Database();
            _validator = new Validator();
        }

        public RecoveryController(AccountController accountController, Database db = null)
        {
            _accountController = accountController;
            UID = accountController.GetUid();
            _validator = new Validator();
            this.db = db ?? new Database();
            Log.LogMessage(LogLevel.Debug, "Recovery Controller", "Recovery Controller created.");
        }

        public bool ValidateUserDetails(string userId, string emailAdd, string phoneNo)
        {
            return _validator.IsValidUsername(userId) && _validator.IsValidEmail(emailAdd) && _validator.IsValidPhoneNumber(phoneNo);
        }

        //Find the user in the database
        public bool FindUser(string userId, string emailAdd, string phoneNo)
        {
            //if (!ValidateUserDetails(userId, emailAdd, phoneNo)) return false;
            UID = userId;
            email = emailAdd;
            phone = phoneNo;

            DataTable dt = new DataTable();
            try
            {
                string table = UID.StartsWith("LMC") ? "customer" : UID.StartsWith("LMS") ? "staff" : null;
                if (table == null) return false;

                sqlStr =
                    $"SELECT * FROM {table} WHERE {(table == "customer" ? "customerID" : "staffID")} = '{UID}' AND (phoneNumber = '{phone}' OR emailAddress = '{email}')";
                
                Dictionary<string, object> queryParameters = new Dictionary<string, object> { { "@id", UID } };
                dt = db.ExecuteDataTable(sqlStr, queryParameters);
                Log.LogMessage(LogLevel.Debug, "Recovery Controller",
                    $"FindUser: UserID = {UID}, email = {email}, phone = {phone}");
                return dt.Rows.Count == 1;
            }
            catch (Exception e)
            {
                Log.LogException(new Exception($"Error in FindUser. {e.Message}"), "Recovery Controller");
                return false; //Some error occurs return false to login.
            }
        }

        //Value for listbox.
        public List<string> GetCity(string province)
        {
            var query = $"SELECT city FROM location WHERE province = '{province}'";
            DataTable dataTable =
                db.ExecuteDataTable(query, new Dictionary<string, object> { { "@province", province } });
            Log.LogMessage(LogLevel.Debug, "Recovery Controller", $"GetCity: Province = {province}");
            return dataTable.AsEnumerable().Select(row => row["city"].ToString()).ToList();
        }

        public List<string> GetProvince()
        {
            var query = "SELECT DISTINCT province FROM location";
            DataTable dataTable = db.ExecuteDataTable(query);
            Log.LogMessage(LogLevel.Debug, "Recovery Controller", $"GetProvince was executed.");
            return dataTable.AsEnumerable().Select(row => row["province"].ToString()).ToList();
        }

        //Update password in the database
        public void ChangePassword(string newPwd)
        {
            string hashedPwd = HashPassword(newPwd);
            string table = UID.StartsWith("LMC") ? "customer_account" : "staff_account";
            string idField = UID.StartsWith("LMC") ? "customerID" : "staffID";

            string sqlStr =
                $"UPDATE {table} SET password = '{hashedPwd}', pwdChangeDate = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}' WHERE {idField} = '{UID}'";
            db.ExecuteNonQueryCommand(sqlStr, new Dictionary<string, object> { { "@id", UID } });
            Log.LogMessage(LogLevel.Debug, "Recovery Controller",
                $"ChangePassword: UserID = {UID}, new password = {newPwd}");
        }

        //Return the new LMC ID to the create account form.
        public int getLMCID()
        {
            DataTable dt = new DataTable();
            string SqlQuery = "SELECT * FROM customer";
            dt = db.ExecuteDataTable(SqlQuery);
            Log.LogMessage(LogLevel.Debug, "Recovery Controller", $"getLMCID: Customer table was executed.");
            return dt.Rows.Count + 1;
        }

        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            Log.LogMessage(LogLevel.Debug, "Recovery Controller", $"HashPassword: Salt = {salt}");
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        //For create a new customer accounr.
        public bool create(dynamic Userinfo)
        {
            string hashedPwd = HashPassword(Userinfo.pwd);
            string lmcid = "LMC" + getLMCID().ToString("D5");
            string accountId = "CA" + getLMCID().ToString("D5");

            var customerParams = new Dictionary<string, object>
            {
                { "@id", lmcid },
                { "@fName", Userinfo.fName },
                { "@lName", Userinfo.lName },
                { "@gender", Userinfo.gender },
                { "@email", Userinfo.email },
                { "@company", Userinfo.company },
                { "@phone", Userinfo.phone },
                { "@province", Userinfo.province },
                { "@city", Userinfo.city },
                { "@address1", Userinfo.address1 },
                { "@address2", Userinfo.address2 },
                { "@joinDate", Userinfo.joinDate },
                { "@payment", Userinfo.payment },
                { "@img", Userinfo.IMG },
                { "@dob", Userinfo.dateOfBirth }
            };

            var accountParams = new Dictionary<string, object>
            {
                { "@accountId", accountId },
                { "@id", lmcid },
                { "@hashedPwd", hashedPwd },
                { "@joinDate", Userinfo.joinDate }
            };

            var dfaddParams = new Dictionary<string, object>
            {
                { "@id", lmcid }
            };

            try
            {
                db.ExecuteNonQueryCommand(
                    "INSERT INTO customer VALUES(@id, @fName, @lName, @gender, @email, @company, @phone, @province, @city, @address1, @address2, @joinDate, @payment, @img, @dob, NULL)",
                    customerParams);
                db.ExecuteNonQueryCommand(
                    "INSERT INTO customer_account VALUES(@accountId, @id, 'N', 'active', @hashedPwd, @joinDate, @joinDate)",
                    accountParams);
                db.ExecuteNonQueryCommand("INSERT INTO customer_dfadd VALUES(@id, '1')", dfaddParams);
                Log.LogMessage(LogLevel.Debug, "Recovery Controller",
                    $"Create: UserID = {lmcid}, email = {Userinfo.email}, phone = {Userinfo.phone}");
                return true;
            }
            catch (Exception ex)
            {
                db.ExecuteNonQueryCommand("DELETE FROM customer WHERE customerID = @id", accountParams);
                db.ExecuteNonQueryCommand("DELETE FROM customer_account WHERE customerID = @id", accountParams);
                db.ExecuteNonQueryCommand("DELETE FROM customer_dfadd WHERE customerID = @id", accountParams);

                Log.LogException(new Exception($"Error in create customer account. {ex.Message}"),
                    "Recovery Controller - Create Account");
                return false;
            }
        }
        
        // Check Email is unique or not
        public bool CheckEmail(string email)
        {
            if (email == null) return false;
            string sqlStr = "SELECT emailAddress FROM customer C, customer_account CA WHERE CA.Status = 'active' AND C.customerID = CA.customerID AND C.emailAddress = @Email";
            DataTable dt = db.ExecuteDataTable(sqlStr, new Dictionary<string, object> { { "@Email", email } });
            Log.LogMessage(LogLevel.Debug, "Recovery Controller", $"CheckEmail: Email = {email}");

            string sqlStr2 = "SELECT emailAddress FROM staff S, staff_account SA WHERE SA.status = 'active' AND S.staffID = SA.staffID AND S.emailAddress = @Email";
            DataTable dt2 = db.ExecuteDataTable(sqlStr2, new Dictionary<string, object> { { "@Email", email } });
            Log.LogMessage(LogLevel.Debug, "Recovery Controller", $"CheckEmail: Email = {email}");

            return dt.Rows.Count < 1 && dt2.Rows.Count < 1;
        }

        public bool CheckPhone(string phone)
        {
            if (phone == null) return false;

            string sqlStr = "SELECT phoneNumber FROM customer C, customer_account CA WHERE CA.Status = 'active' AND C.customerID = CA.customerID AND C.phoneNumber = @Phone";
            DataTable dt = db.ExecuteDataTable(sqlStr, new Dictionary<string, object> { { "@Phone", phone } });
            Log.LogMessage(LogLevel.Debug, "Recovery Controller", $"CheckPhone: Phone = {phone} at customer table");

            string sqlStr2 = "SELECT phoneNumber FROM staff S, staff_account SA WHERE SA.status = 'active' AND S.staffID = SA.staffID AND S.phoneNumber = @Phone";
            DataTable dt2 = db.ExecuteDataTable(sqlStr2, new Dictionary<string, object> { { "@Phone", phone } });
            Log.LogMessage(LogLevel.Debug, "Recovery Controller", $"CheckPhone: Phone = {phone} at staff table");

            return dt.Rows.Count < 1 && dt2.Rows.Count < 1;
        }


        public void UploadUserAvatar(string newFileName)
        {
            string path = Directory.GetCurrentDirectory() + "\\Upload\\";
            string fileName = Path.GetFileName(newFileName);
            string newfileName = "usr_" + getLMCID().ToString("D5") + "_" + Guid.NewGuid().ToString("D5") + "_" +
                                 fileName;
            string newFilePath = path + newfileName;
            File.Move(newFileName, newFilePath);
            db.ExecuteNonQueryCommand("INSERT INTO resource VALUES(@id, @name, @type, @path)",
                new Dictionary<string, object>
                    { { "@id", newFileName }, { "@name", fileName }, { "@type", "avatar" }, { "@path", newFilePath } });
            Log.LogMessage(LogLevel.Debug, "Recovery Controller", $"UploadUserAvatar: New file path = {newFilePath}");
        }

        public void DeleteUserAvatar(string newFileName)
        {
            string path = Directory.GetCurrentDirectory() + "\\Upload\\";
            string fileName = Path.GetFileName(newFileName);
            string newFilePath = path + fileName;
            File.Delete(newFilePath);
            Log.LogMessage(LogLevel.Debug, "Recovery Controller", $"DeleteUserAvatar: New file path = {newFilePath}");
        }

        public void CheckUserAvatar(string newFileName)
        {
            string path = Directory.GetCurrentDirectory() + "\\Upload\\";
            string fileName = Path.GetFileName(newFileName);
            string newFilePath = path + fileName;
            Log.LogMessage(LogLevel.Debug, "Recovery Controller", $"CheckUserAvatar: New file path = {newFilePath}");
        }
    }
}