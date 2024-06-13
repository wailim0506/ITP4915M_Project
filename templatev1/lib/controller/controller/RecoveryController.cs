using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using System.Windows.Forms;

namespace controller
{
    public class RecoveryController : abstractController
    {
        private MySqlCommand cmd;
        private Database db;

        private string UID, email, phone;
        private string sqlStr;

        AccountController _accountController;

        public RecoveryController()
        {
            db = new Database();
        }

        public RecoveryController(Database db)
        {
            this.db = db;
        }

        public RecoveryController(AccountController accountController, Database db = null)
        {
            _accountController = accountController;
            UID = accountController.GetUid();
            this.db = db ?? new Database();
        }

        //Find the user in the database
        public bool FindUser(string UserID, string emailAdd, string phoneNo)
        {
            UID = UserID;
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
                dt = db.ExecuteDataTableAsync(sqlStr, queryParameters).Result;
                return dt.Rows.Count == 1;
            }
            catch (Exception e)
            {
                return false; //Some error occurs return false to login.
            }
        }

        //Value for listbox.
        public List<string> GetCity(string province)
        {
            var query = $"SELECT city FROM location WHERE province = '{province}'";
            DataTable dataTable =
                db.ExecuteDataTableAsync(query, new Dictionary<string, object> { { "@province", province } }).Result;
            return dataTable.AsEnumerable().Select(row => row["city"].ToString()).ToList();
        }

        public List<string> GetProvince()
        {
            var query = "SELECT DISTINCT province FROM location";
            DataTable dataTable = db.ExecuteDataTableAsync(query).Result;
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
            db.ExecuteNonQueryCommandAsync(sqlStr, new Dictionary<string, object> { { "@id", UID } }).Wait();
        }

        //Return the new LMC ID to the create account form.
        public int getLMCID()
        {
            DataTable dt = new DataTable();
            string SqlQuery = "SELECT COUNT(*) FROM customer";
            dt = db.ExecuteDataTableAsync(SqlQuery).Result;
            return dt.Rows.Count + 1;
        }

        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
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
                _ = db.ExecuteNonQueryCommandAsync(
                    "INSERT INTO customer VALUES(@id, @fName, @lName, @gender, @email, @company, @phone, @province, @city, @address1, @address2, @joinDate, @payment, @img, @dob, NULL)",
                    customerParams);
                _ = db.ExecuteNonQueryCommandAsync(
                    "INSERT INTO customer_account VALUES(@accountId, @id, 'active', @hashedPwd, @joinDate, @joinDate)",
                    accountParams);
                _ = db.ExecuteNonQueryCommandAsync("INSERT INTO customer_dfadd VALUES(@id, '1')", dfaddParams);
                return true;
            }
            catch (Exception)
            {
                _ = db.ExecuteNonQueryCommandAsync("DELETE FROM customer WHERE customerID = @id", dfaddParams);
                _ = db.ExecuteNonQueryCommandAsync("DELETE FROM customer_account WHERE customerID = @id", dfaddParams);
                _ = db.ExecuteNonQueryCommandAsync("DELETE FROM customer_dfadd WHERE customerID = @id", dfaddParams);
                return false;
            }
        }

        //Check whether the email or phone has registered an account.
        public bool CheckEmailPhone(string data)
        {
            DataTable dt = new DataTable();
            sqlStr =
                $"SELECT emailAddress, phoneNumber FROM customer C, customer_account CA WHERE Status = 'active' AND c.customerID = CA.customerID AND (phoneNumber = \'{data}\' OR emailAddress = \'{data}\') " +
                $"UNION ALL SELECT emailAddress, phoneNumber FROM staff S, staff_account SA WHERE status = 'active' AND s.staffID = sa.staffID AND(phoneNumber = \'{data}\' OR emailAddress = \'{data}\');";
            dt = db.ExecuteDataTableAsync(sqlStr).Result;
            return dt.Rows.Count < 1;
        }
    }
}