using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Windows.Forms;
using System.Data;
using MySqlConnector;

namespace controller
{
    public class UIController : abstractController
    {
        //For DataBase
        private string sqlStr;

        private bool BWMode;
        private bool showbtn1, showbtn2, showbtn3, showbtn4, showbtn5;      //whether the button is visible.
        private static string funbtn1, funbtn2, funbtn3, funbtn4, funbtn5;        //Text in the button.
        private static string AccountType, permission;

        controller.accountController accountController;

        public UIController()
        {

        }

        public UIController(accountController accountController)
        {
            this.accountController = accountController;
            showbtn1 = showbtn2 = showbtn3 = showbtn4 = showbtn5 = BWMode = false;
        }

        //Set user permission and determine which function button to be shown.
        public void setPermission(string UserID)
        {
            if (AccountType.Equals("Customer"))     //Customer account.
                determineFun("C");
            else       //Staff
            {
                DataTable dt = new DataTable();
                sqlStr = $"SELECT permissionID FROM staff_account_permission SP, staff_account S WHERE SP.staffAccountID = S.staffAccountID AND S.staffID = \'{UserID}\'";
                adr = new MySqlDataAdapter(sqlStr, conn);
                adr.Fill(dt);
                adr.Dispose();
                permission = dt.Rows[0]["permissionID"].ToString();
                determineFun(permission);
            }
        }
        public void setType(string AccType)
        {
            AccountType = AccType;
        }
        private void determineFun(string permission)
        {
            switch (permission)
            {
                case "C":     //Customer
                    //showbtn1 = showbtn2 = true;
                    //funbtn1 = "Spare Part";
                    //funbtn2 = "Invoice Management";

                    //my version I think
                    showbtn1 = showbtn2 = showbtn3 = showbtn4 = showbtn5 = true;
                    funbtn1 = "Order Management";
                    funbtn2 = "Spare Part";
                    funbtn3 = "Cart";
                    funbtn4 = "Favourite";
                    funbtn5 = "Give Feedback";

                    break;
                case "MP01":     //Sales manager
                    showbtn1 = showbtn2 = showbtn3 = showbtn4 = showbtn5 = true;
                    funbtn1 = "Order Management";
                    funbtn2 = "Invoice Management";
                    funbtn3 = "On-Sale Product Management";
                    funbtn4 = "Stock Management";
                    funbtn5 = "User Managemnet";
                    break;
                case "MP02":     //Storeman
                    showbtn1 = showbtn2 = showbtn3 = true;
                    funbtn1 = "Order Management";
                    funbtn2 = "Stock Management";
                    funbtn3 = "User Managemnet";
                    break;
                case "MP03":     //Department manager
                    showbtn1 = true;
                    funbtn1 = "User Managemnet";
                    break;
                case "MP04":     //Delivery man
                    showbtn1 = showbtn2 = true;
                    funbtn1 = "Order Management";
                    funbtn2 = "User Management";
                    break;
            }
        }
        //Return the button name and whick to be shown.
        public dynamic showFun()
        {
            dynamic funbtn = new ExpandoObject();
            funbtn.btn1show = showbtn1;
            funbtn.btn1value = funbtn1;
            funbtn.btn2show = showbtn2;
            funbtn.btn2value = funbtn2;
            funbtn.btn3show = showbtn3;
            funbtn.btn3value = funbtn3;
            funbtn.btn4show = showbtn4;
            funbtn.btn4value = funbtn4;
            funbtn.btn5show = showbtn5;
            funbtn.btn5value = funbtn5;
            return funbtn;
        }

        //For dark mode function.
        public void setMode(bool value)
        {
            BWMode = value;
        }
        public dynamic getMode()
        {
            dynamic mode = new ExpandoObject();
            if (!BWMode)     //normal
            {
                mode.textColor = "#FFFFFF";
                mode.bgColor = "#404040";
                mode.navBarColor = "#008000";
                mode.navColor = "#A0A0A0";
                mode.timeColor = "#A9A9A9";
                mode.locTbColor = "#808080";
                mode.logoutColor = "#FF0000";
                mode.profileColor = "#BDB76B";
                mode.btnColor = "#808080";
                mode.BWmode = true;
                return mode;
            }
            else    //dark.
            {
                mode.textColor = "#000000";
                mode.bgColor = "#F0F0F0";
                mode.navBarColor = "#3bd5b8";
                mode.navColor = "#B9D1EA";
                mode.timeColor = "#cccccc";
                mode.locTbColor = "#E3E3E3";
                mode.logoutColor = "#ffc0c0";
                mode.profileColor = "#ffffc0";
                mode.btnColor = "#FFFFFF";
                mode.BWmode = false;
                return mode;
            }
        }

        //Return the indicator's location.
        public int getIndicator(string btnText)
        {
            if (btnText.Equals(funbtn1))
                return 1;
            else if (btnText.Equals(funbtn2))
                return 2;
            else if (btnText.Equals(funbtn3))
                return 3;
            else if (btnText.Equals(funbtn4))
                return 4;
            else
                return 5;
        }

        //Change the information needs to show between customer and staff.
        public dynamic proFile()
        {
            dynamic profile = new ExpandoObject();
            if (AccountType.Equals("Customer"))
            {
                profile.group1 = false;
                profile.group2 = true;
            }
            else
            {
                profile.group1 = true;
                profile.group2 = false;
            }
            return profile;
        }

        //Change the information needs to show between storeman and sale manager.
        public dynamic store()
        {
            dynamic store = new ExpandoObject();
            if (permission.Equals("MP02"))
            {
                store.group1 = false;
            }
            else
            {
                store.group1 = true;
            }
            return store;


        }
    }
}
