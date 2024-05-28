using System;
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
        private string firstName, lastName, UserID, AccountType;

        controller.UIController UIController;
        controller.proFileController proFileController;

        public accountController()
        {
            IsLogin = false;
            sqlStr = "";
            firstName = lastName = UserID = AccountType = "";
        
        }

        public bool login(string UID, string Pass, controller.UIController UI)
        {
            //try
            //{
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

                if (dt.Rows.Count < 1)          //Account NOT found
                    return IsLogin;
                else if (Pass.Equals(dt.Rows[0]["password"]) && dt.Rows[0]["status"].Equals("active"))          //Account found
                {
                    IsLogin = true;
                    UserID = UID;

                    UIController = UI;
                    //proFileController = new controller.proFileController();

                    UserName();
                    UIController.setPermission(UserID);



                }
                return IsLogin;
            //}
            //catch (Exception e)
            //{
            //    return IsLogin;     //Some error occurs retrn false to login
            //}
        }

        private void UserName()
        {
            DataTable dt = new DataTable();

            if (UserID.StartsWith("LMC"))         //A customer account
            {
                sqlStr = "SELECT firstName, lastName FROM customer WHERE customerID = '" + UserID + "'";
                AccountType = "Customer";
                UIController.setType(AccountType);
                //proFileController.setType(AccountType);

            }
            else     //A staff account
            {
                sqlStr = "SELECT firstName, lastName FROM staff WHERE staffID = '" + UserID + "'";
                AccountType = "Staff";
                UIController.setType(AccountType);
                //proFileController.setType(AccountType);
            }

            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();

            lastName = dt.Rows[0]["lastName"].ToString();
            firstName = dt.Rows[0]["firstName"].ToString();
        }

        public string getName()
        {
            return lastName + " " + firstName;
        }

        public string getUID()
        {
            return UserID;
        }

    }
}
