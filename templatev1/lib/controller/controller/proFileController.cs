using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using System.Windows.Forms;

namespace controller
{
    public class proFileController : abstractController
    {
        private string sqlStr;
        private string accountType;
        private string UID;
        private MySqlCommand cmd;
        private DateTime dateOfBirth, createDate;
        private string jobTitle, dept, email, fName, lName, sex, phone, payment, caddress, waddress, corp;
        private bool NGDateOfBirth = false;

        controller.accountController accountController;

        public proFileController()
        {
            sqlStr = "";
            accountType = "";
            UID = jobTitle = dept = email = fName = lName = sex = phone = payment = caddress = waddress = corp = "";
        }

        public proFileController(string UID)
        {
            this.UID = UID;
        }

        public proFileController(controller.accountController accountController)
        {
            this.accountController = accountController;
        }

        private void UserInfo()
        {
            UID = accountController.getUID();
            DataTable dt = new DataTable();

            if (accountType.Equals("Staff"))          //Staff info
            {
                sqlStr = $"SELECT jobTitle, name, emailAddress, firstName, lastName, sex, phoneNumber, dateOfBirth, createDate " +
                    $"FROM staff S, department D, staff_account SA WHERE S.deptID = D.deptID AND S.staffID = \'{UID}\' AND SA.staffID = \'{UID}\'";

            }
            else          //Customer info
            {
                sqlStr = $"SELECT emailAddress, firstName, lastName, sex, phoneNumber, dateOfBirth, createDate, paymentMethod, province, city, companyAddress, warehouseAddress, company " +
                    $"FROM customer_account CA, customer C WHERE CA.customerID = \'{UID}\' AND C.customerID = \'{UID}\'";
            }
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();

            //Set user data to gobal variable
            if (accountType.Equals("Staff"))
            {
                jobTitle = dt.Rows[0]["jobTitle"].ToString();
                dept = dt.Rows[0]["name"].ToString();
            }
            else
            {
                payment = dt.Rows[0]["paymentMethod"].ToString();
                caddress = dt.Rows[0]["companyAddress"].ToString() + ", " + dt.Rows[0]["city"].ToString() + ", " + dt.Rows[0]["province"].ToString();
                waddress = dt.Rows[0]["warehouseAddress"].ToString() + ", " + dt.Rows[0]["city"].ToString() + ", " + dt.Rows[0]["province"].ToString();
                corp = dt.Rows[0]["company"].ToString();
            }

            email = dt.Rows[0]["emailAddress"].ToString();
            fName = dt.Rows[0]["firstName"].ToString();
            lName = dt.Rows[0]["lastName"].ToString();
            sex = dt.Rows[0]["sex"].ToString();
            phone = dt.Rows[0]["phoneNumber"].ToString();
            createDate = (DateTime)dt.Rows[0]["createDate"];

            //If the date of birth is not provided
            if (string.IsNullOrEmpty(dt.Rows[0]["dateOfBirth"].ToString()) && accountType.Equals("Customer"))
            {
                NGDateOfBirth = true;
                dateOfBirth = DateTime.Now;
            }
            else
                dateOfBirth = (DateTime)dt.Rows[0]["dateOfBirth"];
        }

        public dynamic getUserInfo()
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
            expando.payment = payment;
            expando.caddress = caddress;
            expando.NGDateOfBirth = NGDateOfBirth;
            expando.corp = corp;
            expando.waddress = waddress;
            return expando;
        }

        public void setType(string AccType)
        {
            accountType = AccType;
            UserInfo();
        }

        public bool modify(dynamic info)
        {
            try
            {
                conn.Open();
                if (accountType.Equals("Customer"))
                    sqlStr = $"UPDATE customer SET firstName = \'{info.fName}\', lastName = \'{info.lName}\', sex = \'{info.sex}\', phoneNumber = \'{info.phone}\'" +
                        $", paymentMethod = \'{info.pay}\', dateofBirth = {info.DFB}, company = \'{info.corp}\' WHERE customerID = \'{UID}\'";
                else
                    sqlStr = $"UPDATE staff SET firstName = \'{info.fName}\', lastName = \'{info.lName}\', sex = \'{info.sex}\', phoneNumber = \'{info.phone}\', dateofBirth = {info.DFB} WHERE staffID = \'{UID}\'";
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
