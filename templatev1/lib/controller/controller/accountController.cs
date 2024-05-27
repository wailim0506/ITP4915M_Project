using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace controller
{
    public class accountController
    {
        //For DataBase
        private static string connString = "server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;"; //just copy here, change the attribute stated above
        MySqlConnection conn = new MySqlConnection(connString);
        MySqlDataAdapter adr;
        private static string sqlStr;

        public bool IsLogin;
        private static string firstName;
        private static string lastName;
        public static string UserID;

        controller.UIController UIController;

        public accountController()
        {
            IsLogin = false;
            sqlStr = UserID = "";
        
        }

        public bool login(string UID, string Pass)
        {
            try
            {
                DataTable dt = new DataTable();

                if (UID.StartsWith("LMC"))         //A customer account
                {
                    sqlStr = "SELECT password, status FROM customer_account WHERE customerID = '" + UID + "'";
                }
                else if (UID.StartsWith("LMS"))         //A staff account
                {
                    sqlStr = "SELECT password, status FROM staff_account WHERE staffID = '" + UID + "'";
                }
                else
                    return IsLogin;      //Not a LM account

                adr = new MySqlDataAdapter(sqlStr, conn);
                adr.Fill(dt);
                adr.Dispose();

                if (dt.Rows.Count < 1)
                    return IsLogin;
                else if (Pass.Equals(dt.Rows[0]["password"]) && dt.Rows[0]["status"].Equals("active"))
                {
                    IsLogin = true;
                    UserID = UID;
                    UserName();

                    UIController = new controller.UIController();
                    UIController.setPermission(UserID);


                }
                return IsLogin;
            }
            catch (Exception e)
            {
                return IsLogin;     //Some error occurs retrn false to login
            }
        }

        private void UserName()
        {
            DataTable dt = new DataTable();

            if (UserID.StartsWith("LMC"))         //A customer account
            {
                sqlStr = "SELECT firstName, lastName FROM customer WHERE customerID = '" + UserID + "'";
            }
            else     //A staff account
            {
                sqlStr = "SELECT firstName, lastName FROM staff WHERE staffID = '" + UserID + "'";
            }

            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();

            lastName = dt.Rows[0]["lastName"].ToString();
            firstName = dt.Rows[0]["firstName"].ToString();
        }

        public static string getName()
        {
            return lastName + " " + firstName;
        }

        public static string getUID()
        {
            return UserID;
        }
    }
}
