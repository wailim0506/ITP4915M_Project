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
    public class accountController : abstractController
    {
        //For DataBase
        private string sqlStr;

        public bool IsLogin;
        private string accountID, firstName, lastName, UserID, AccountType, decryptedPassword;

        byte[] key = new byte[16];
        byte[] iv = new byte[16];

        private MySqlCommand cmd;

        UIController UIController;

        public accountController()
        {
            IsLogin = false;
            sqlStr = "";
            accountID = firstName = lastName = UserID = AccountType = decryptedPassword = "";
        }

        public bool login(string UID, string Pass, UIController UI)
        {
            //try
            //{
            DataTable dt = new DataTable();

            if (UID.StartsWith("LMC") || UID.StartsWith("LMS"))
            {
                sqlStr = $"SELECT password, pwdKEY, pwdIV, status FROM customer_account WHERE customerID = \'{UID}\' " +
                $"UNION ALL SELECT password, pwdKEY, pwdIV, status FROM staff_account WHERE staffID = \'{UID}\'";
            }
            else
                return IsLogin;      //Not a LM account

            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();

            if (dt.Rows.Count < 1)          //Account NOT found
                return IsLogin;

            //Decrypt data from the database
            key = Convert.FromBase64String(dt.Rows[0]["pwdKEY"].ToString());
            iv = Convert.FromBase64String(dt.Rows[0]["pwdIV"].ToString());
            byte[] pwdbyte = System.Convert.FromBase64String(dt.Rows[0]["password"].ToString());
            decryptedPassword = Decrypt(pwdbyte, key, iv);

            if (Pass.Equals(decryptedPassword) && dt.Rows[0]["status"].Equals("active"))          //Account found AND verify the password
            {
                IsLogin = true;
                UserID = UID;

                UIController = UI;

                UserInfo();
                UIController.setPermission(UserID);
            }
            return IsLogin;
            //}
            //catch (Exception e)
            //{
            //    return IsLogin;     //Some error occurs retrn false to login
            //}
        }
        //Decrypt the password that has got form the database.
        private string Decrypt(byte[] cipheredtext, byte[] key, byte[] iv)
        {
            string simpletext = String.Empty;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream(cipheredtext))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            simpletext = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return simpletext;
        }



        private void UserInfo()
        {
            DataTable dt = new DataTable();

            if (UserID.StartsWith("LMC"))         //A customer account
            {
                sqlStr = $"SELECT customerAccountID AS accountID, firstName, lastName FROM customer C, customer_account CA WHERE CA.customerID = \'{UserID}\' AND C.customerID = \'{UserID}\'";
                AccountType = "Customer";
                UIController.setType(AccountType);

            }
            else     //A staff account
            {
                sqlStr = $"SELECT staffAccountID AS accountID, firstName, lastName FROM staff S, staff_account SA WHERE SA.staffID = \'{UserID}\' AND S.staffID = \'{UserID}\'";
                AccountType = "Staff";
                UIController.setType(AccountType);
            }

            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            conn.Close();

            lastName = dt.Rows[0]["lastName"].ToString();
            firstName = dt.Rows[0]["firstName"].ToString();
            accountID = dt.Rows[0]["accountID"].ToString();
        }

        public string getName()
        {
            return lastName + " " + firstName;
        }

        public string getUID()
        {
            return UserID;
        }

        public string getType()
        {
            return AccountType;
        }


        //update the login record into the database.
        public void setLog(string Date)
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
        public string getLog()
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
        //Return the last password chagne date.
        public DateTime getPwdChange()
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
        //Return the full login record.
        public DataTable getFullLog()
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

        public bool matchPwd(string gettedPwd)
        {
            if (gettedPwd.Equals(decryptedPassword))
                return true;
            else
                return false;
        }



        public DataTable getStaffDetail(string id) //use in OrderListController     //id = staff account id
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
        
        public DataTable getCustomerDetail(string id) //use in viewOrderController   //id = customerID
        {
            DataTable dt = new DataTable();
            string sqlCmd = $"SELECT * FROM customer WHERE customerID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public bool delAccount()
        {
            try
            {
                //Insert a record into customer table
                conn.Open();
                sqlStr = $"UPDATE customer_account SET Status = 'disable' WHERE customerID = \'{accountID}\'";
                cmd = new MySqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;           //Something went wrong.
            }
        }

    }
}
