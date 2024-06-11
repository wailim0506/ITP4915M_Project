//each function need one controller

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms; //must include in every controller file
using MySqlConnector; //must include in every controller file  
//need to download 'MySqlConnector' in NuGet Package Manager first if can't run

namespace controller
{
    public /*<--add here*/ class testController //need to add 'public' here
    {
        //port = port Number of MySql in XAMPP (usually 3306)
        //database = the name of your database
        private static string connString =
            "server=localhost;port=8088;user id=root; password=password;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30";
        //"server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;"; //just copy here, change the attribute stated above

        MySqlConnection conn = new MySqlConnection(connString); //just copy here
        MySqlDataAdapter adr; //just copy here

        public testController() //constructor, useless
        {
        }

        public DataTable test1() //method name can change
        {
            DataTable dt = new DataTable(); //just copy here
            string sqlCmd = "SELECT * FROM customer"; //the sql query you want to run
            adr = new MySqlDataAdapter(sqlCmd, conn); //just copy here
            adr.Fill(dt); //just copy
            return dt; //just copy
        }

        public DataTable test2() //method name can change
        {
            DataTable dt = new DataTable(); //just copy here
            string sqlCmd =
                "SELECT * FROM supplier WHERE Country = 'United States'"; //the sql query you want to run  //need to use single quote
            adr = new MySqlDataAdapter(sqlCmd, conn); //just copy here
            adr.Fill(dt); //just copy
            return dt; //just copy
        }

        public DataTable test3() //method name can change
        {
            DataTable dt = new DataTable(); //just copy here
            string sqlCmd =
                "SELECT jobTitle, name, emailAddress, firstName, lastName, sex, phoneNumber, dateOfBirth, createDate " +
                "FROM staff S, department D, staff_account SA WHERE S.deptID = D.deptID AND S.staffID='LMS00001' AND SA.staffID='LMS00001'";
            adr = new MySqlDataAdapter(sqlCmd, conn); //just copy here
            adr.Fill(dt); //just copy
            return dt; //just copy
        }

        public bool UpdatePassword(Dictionary<string, string> usersToUpdate)
        {
            try
            {
                foreach (var user in usersToUpdate)
                {
                    var oldPassword = GetOldPassword(user.Key);
                    var hashedPassword = HashPassword(oldPassword);
                    var sqlQuery =
                        $"UPDATE {(user.Key.StartsWith("LMS") ? "staff_account" : "customer_account")} SET password = '{hashedPassword}' WHERE {(user.Key.StartsWith("LMS") ? "staffID" : "customerID")} = '{user.Key}'";
                    ExecuteSqlQuery(sqlQuery);
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private string GetOldPassword(string userId)
        {
            var dt = ExecuteSqlQuery(
                $"SELECT * FROM {(userId.StartsWith("LMS") ? "staff_account" : "customer_account")} WHERE {(userId.StartsWith("LMS") ? "staffID" : "customerID")} = '{userId}'");
            var key = Convert.FromBase64String(dt.Rows[0]["pwdKEY"].ToString());
            var iv = Convert.FromBase64String(dt.Rows[0]["pwdIV"].ToString());
            var pwdbyte = Convert.FromBase64String(dt.Rows[0]["password"].ToString());
            return Decrypt(pwdbyte, key, iv);
        }

        private string Decrypt(byte[] cipheredtext, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream(cipheredtext))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public bool DeveloperToolForgetPassword(string userid, string newPassword)
        {
            string password = HashPassword(newPassword);
            string sqlQuery =
                $"UPDATE {(userid.StartsWith("LMS") ? "staff_account" : "customer_account")} SET password = '{password}' WHERE {(userid.StartsWith("LMS") ? "staffID" : "customerID")} = '{userid}'";
            ExecuteSqlQuery(sqlQuery);
            DataTable dt = ExecuteSqlQuery(
                $"SELECT * FROM {(userid.StartsWith("LMS") ? "staff_account" : "customer_account")} " +
                $"WHERE {(userid.StartsWith("LMS") ? "staffID" : "customerID")} = '{userid}'");
            return dt.Rows.Count == 1;
        }


        private DataTable ExecuteSqlQuery(string sqlQuery)
        {
            DataTable dt = new DataTable();
            adr = new MySqlDataAdapter(sqlQuery, conn);
            adr.Fill(dt);
            adr.Dispose();
            return dt;
        }

        public static string HashPassword(string password)
        {
            RecoveryController RecoveryController = new RecoveryController();
            return RecoveryController.HashPassword(password);
        }

        public void UpdateDeveloperAccount()
        {
            var usersToUpdate = new Dictionary<string, string>
            {
                { "LMC00001", "password123" },
                { "LMC00002", "password123" },
                { "LMS00001", "password123" },
                { "LMS00002", "abc123456" },
                { "LMS00003", "xyz789!@#" },
                { "LMS00004", "qwer5678" },
                { "LMS00005", "asdf1234!" }
            };

            foreach (var user in usersToUpdate)
            {
                if (UpdatePasswordAndTestLogin(user.Key, user.Value))
                {
                    MessageBox.Show($"Password updated and login successful at {user.Key}");
                }
                else
                {
                    MessageBox.Show($"Update or login failed at {user.Key}");
                }
            }
        }

        private bool UpdatePasswordAndTestLogin(string userId, string newPassword)
        {
            if (!DeveloperToolForgetPassword(userId, newPassword)) return false;
            return TestLogin(userId, newPassword);
        }

        private bool TestLogin(string userId, string password)
        {
            var accountController = new AccountController();
            var UIController = new UIController(accountController);
            return accountController.Login(userId, password, UIController);
        }
    }
}