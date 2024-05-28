using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace controller
{
    public class proFileController : abstractController
    {
        private static string sqlStr;
        private static string accountType;
        private static string UID;
        private static DateTime dateOfBirth, createDate;
        private static string jobTitle, dept, email, fName, lName, sex, phone; 

        public proFileController()
        {
            sqlStr = "";
            accountType = "";
            jobTitle = dept = email = fName = lName = sex = phone = "";
        }

        private void UserInfo()
        {
            //UID = controller.accountController.getUID();
            DataTable dt = new DataTable();

            if (accountType.Equals("Staff"))          //Staff info
            {
                sqlStr = "SELECT jobTitle, name, emailAddress, firstName, lastName, sex, phoneNumber, dateOfBirth, createDate " +
                    "FROM staff S, department D, staff_account SA WHERE S.deptID = D.deptID AND S.staffID='" + UID + "' AND SA.staffID='" + UID + "'";

            }
            else          //Customer info
            {
                //sqlStr = "SELECT permissionID FROM staff_account_permission SP, staff_account S WHERE SP.staffAccountID = S.staffAccountID AND S.staffID = '" + UserID + "'";
            }
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();

            //Set user data to gobal variable
            jobTitle = dt.Rows[0]["jobTitle"].ToString();
            dept = dt.Rows[0]["name"].ToString();
            email = dt.Rows[0]["emailAddress"].ToString();
            fName = dt.Rows[0]["firstName"].ToString();
            lName = dt.Rows[0]["lastName"].ToString();
            sex = dt.Rows[0]["sex"].ToString();
            phone = dt.Rows[0]["phoneNumber"].ToString();
            dateOfBirth = (DateTime) dt.Rows[0]["dateOfBirth"];
            createDate = (DateTime)dt.Rows[0]["createDate"];







        }

        public static dynamic getUserInfo()
        {
            dynamic expando = new ExpandoObject();
            expando.accountType = accountType;
            expando.jobTitle = jobTitle;
            expando.dept = dept;
            expando.email = email;
            expando.fName = fName;
            expando.lName = lName;
            expando.sex = sex;
            expando.phone = phone;
            expando.dateOfBirth = dateOfBirth;
            expando.createDate = createDate;

            return expando;
        }

        public void setType(string AccType)
        {
            accountType = AccType;
            UserInfo();
        }
    }
}
