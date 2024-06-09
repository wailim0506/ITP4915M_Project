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

        public RecoveryController(AccountController accountController, Database db)
        {
            _accountController = accountController;
            UID = accountController.GetUid();
            this.db = db;
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
                dt = db.ExecuteDataTable(sqlStr, queryParameters);
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
                db.ExecuteDataTable(query, new Dictionary<string, object> { { "@province", province } });
            return dataTable.AsEnumerable().Select(row => row["city"].ToString()).ToList();
        }

        public List<string> GetProvince()
        {
            var query = "SELECT DISTINCT province FROM location";
            DataTable dataTable = db.ExecuteDataTable(query);
            return dataTable.AsEnumerable().Select(row => row["province"].ToString()).ToList();
        }

        public void ChangePassword(string newPwd)
        {
            string hashedPwd = HashPassword(newPwd);
            //Update password in the database
            conn.Open();
            sqlStr = UID.StartsWith("LMC")
                ? $"UPDATE customer_account SET password = \'{hashedPwd}\'" +
                  $",pwdChangeDate = \'{DateTime.Now:yyyy-MM-dd HH:mm:ss}\' " +
                  $"WHERE customerID = \'{UID}\'"
                : $"UPDATE staff_account SET password = \'{hashedPwd}\', " +
                  $"pwdChangeDate = \'{DateTime.Now:yyyy-MM-dd HH:mm:ss}\' " +
                  $"WHERE staffID = \'{UID}\'";
            db.ExecuteNonQueryCommand(sqlStr, new Dictionary<string, object> { { "@id", UID } });
        }

        //Return the new LMC ID to the create account form.
        public int getLMCID()
        {
            DataTable dt = new DataTable();
            string SqlQuery = "SELECT COUNT(*) FROM customer";
            dt = db.ExecuteDataTable(SqlQuery);
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
            string LMCID = "LMC" + getLMCID().ToString("D5");
            string accountID = "CA" + getLMCID().ToString("D5");

            var customerParams = new Dictionary<string, object>
            {
                { "@id", LMCID },
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
                { "@accountId", accountID },
                { "@id", LMCID },
                { "@hashedPwd", hashedPwd },
                { "@joinDate", Userinfo.joinDate }
            };

            var dfaddParams = new Dictionary<string, object>
            {
                { "@id", LMCID }
            };

            try
            {
                db.ExecuteNonQueryCommand(
                    "INSERT INTO customer VALUES(@id, @fName, @lName, @gender, @email, @company, @phone, @province, @city, @address1, @address2, @joinDate, @payment, @img, @dob, NULL)",
                    customerParams);
                db.ExecuteNonQueryCommand(
                    "INSERT INTO customer_account VALUES(@accountId, @id, 'active', @hashedPwd, @joinDate, @joinDate)",
                    accountParams);
                db.ExecuteNonQueryCommand("INSERT INTO customer_dfadd VALUES(@id, '1')", dfaddParams);
                return true;
            }
            catch (Exception)
            {
                db.ExecuteNonQueryCommand("DELETE FROM customer WHERE customerID = @id", dfaddParams);
                db.ExecuteNonQueryCommand("DELETE FROM customer_account WHERE customerID = @id", dfaddParams);
                db.ExecuteNonQueryCommand("DELETE FROM customer_dfadd WHERE customerID = @id", dfaddParams);
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
            dt = db.ExecuteDataTable(sqlStr);
            return dt.Rows.Count < 1;
        }


        //Check whether the email or phone has registered an account.
        public bool checkEmailPhone(string data)
        {
            DataTable dt = new DataTable();
            sqlStr =
                $"SELECT emailAddress, phoneNumber FROM customer C, customer_account CA WHERE Status = 'active' AND c.customerID = CA.customerID AND (phoneNumber = \'{data}\' OR emailAddress = \'{data}\') " +
                $"UNION ALL SELECT emailAddress, phoneNumber FROM staff S, staff_account SA WHERE status = 'active' AND s.staffID = sa.staffID AND(phoneNumber = \'{data}\' OR emailAddress = \'{data}\');";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            return dt.Rows.Count < 1;
        }

        //Check whether the email or phone has registered an account.
        public bool checkEmailPhone(string data)
        {
            DataTable dt = new DataTable();
            sqlStr =
                $"SELECT emailAddress, phoneNumber FROM customer C, customer_account CA WHERE Status = 'active' AND c.customerID = CA.customerID AND (phoneNumber = \'{data}\' OR emailAddress = \'{data}\') " +
                $"UNION ALL SELECT emailAddress, phoneNumber FROM staff S, staff_account SA WHERE status = 'active' AND s.staffID = sa.staffID AND(phoneNumber = \'{data}\' OR emailAddress = \'{data}\');";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            return dt.Rows.Count < 1;
        }
    }
}