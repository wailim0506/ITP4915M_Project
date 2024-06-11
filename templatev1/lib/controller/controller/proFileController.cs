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
        private MySqlCommand cmd;
        private Database _db;

        private string sqlStr;
        private string accountType, UID;
        private DateTime dateOfBirth, createDate;

        private string jobTitle,
            dept,
            email,
            fName,
            lName,
            sex,
            phone,
            payment,
            caddress,
            dfwaddress,
            waddress1,
            waddress2,
            corp,
            city,
            province;

        private bool NGDateOfBirth = false;
        private int dfadd;

        AccountController accountController;

        public proFileController()
        {
        }

        public proFileController(AccountController accountController, Database db = null)
        {
            this.accountController = accountController;
            _db = db ?? new Database();
        }


        //For department manager view or modify the user account.
        public proFileController(string UID)
        {
            this.UID = UID;
        }

        //Get the default address value for customer user from database and set the value.
        private void GetDfAdd()
        {
            DataTable dt = new DataTable();
            sqlStr = $"SELECT dfadd FROM customer_dfadd WHERE customerID = \'{UID}\'";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();

            dfadd = int.Parse(dt.Rows[0]["dfadd"].ToString());
        }

        //Set the account type.
        public void setType(string AccType)
        {
            accountType = AccType;
            UserInfo();
        }


        private void UserInfo()
        {
            UID = accountController.GetUid();
            DataTable dt = new DataTable();

            if (accountType.Equals("Staff")) //Staff info
            {
                sqlStr =
                    $"SELECT jobTitle, name, emailAddress, firstName, lastName, sex, phoneNumber, dateOfBirth, createDate " +
                    $"FROM staff S, department D, staff_account SA WHERE S.deptID = D.deptID AND S.staffID = \'{UID}\' AND SA.staffID = \'{UID}\'";
            }
            else //Customer info
            {
                GetDfAdd();
                sqlStr =
                    $"SELECT emailAddress, firstName, lastName, sex, phoneNumber, dateOfBirth, createDate, paymentMethod, province, city, companyAddress, warehouseAddress, company, warehouseAddress2 " +
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
                waddress1 = dt.Rows[0]["warehouseAddress"].ToString();
                waddress2 = dt.Rows[0]["warehouseAddress2"].ToString();
                if (dfadd == 1)
                    dfwaddress = waddress1;
                else
                    dfwaddress = waddress2;

                payment = dt.Rows[0]["paymentMethod"].ToString();
                caddress = dt.Rows[0]["companyAddress"].ToString();
                corp = dt.Rows[0]["company"].ToString();
                city = dt.Rows[0]["city"].ToString();
                province = dt.Rows[0]["province"].ToString();
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

        //Return value to the profile.
        public dynamic getUserInfo()
        {
            dynamic UserInfo = new ExpandoObject();
            UserInfo.accountType = accountType;
            UserInfo.jobTitle = jobTitle;
            UserInfo.dept = dept;
            UserInfo.email = email;
            UserInfo.fName = fName;
            UserInfo.lName = lName;
            UserInfo.sex = sex;
            UserInfo.phone = phone;
            UserInfo.dateOfBirth = dateOfBirth;
            UserInfo.createDate = createDate;
            UserInfo.payment = payment;
            UserInfo.caddress = caddress + ", " + city + ", " + province;
            UserInfo.NGDateOfBirth = NGDateOfBirth;
            UserInfo.corp = corp;
            UserInfo.waddress = dfwaddress + ", " + city + ", " + province;
            return UserInfo;
        }

        //Return the address for customer user.
        public dynamic getAddinfo()
        {
            dynamic AddInfo = new ExpandoObject();
            DataTable dt = new DataTable();

            sqlStr =
                $"SELECT province, city, companyAddress, warehouseAddress, warehouseAddress2 FROM customer WHERE customerID =\'{UID}\'";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();

            AddInfo.province = province;
            AddInfo.city = city;
            AddInfo.corpAdd = caddress;
            AddInfo.wAdd1 = waddress1;
            AddInfo.wAdd2 = waddress2;
            AddInfo.dfvalue = dfadd;

            return AddInfo;
        }

        //Values for listbox
        public List<string> getcity(string priovince)
        {
            DataTable dt = new DataTable();
            sqlStr = $"SELECT city FROM location WHERE priovince = \'{priovince}\'";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            List<string> city = new List<string>();

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                city.Add(dt.Rows[i]["city"].ToString());
            }

            return city;
        }

        public List<string> getpriovince()
        {
            DataTable dt = new DataTable();
            sqlStr = $"SELECT DISTINCT province FROM location";
            adr = new MySqlDataAdapter(sqlStr, conn);
            adr.Fill(dt);
            adr.Dispose();
            List<string> priovince = new List<string>();

            for (int i = 0; i <= (dt.Rows.Count - 1); i++)
            {
                priovince.Add(dt.Rows[i]["province"].ToString());
            }

            return priovince;
        }

        //Check whether the email or phone has registered an account.
        public bool checkEmailPhone(string data)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlStr =
                    $"SELECT emailAddress, phoneNumber FROM customer C, customer_account CA WHERE Status = 'active' AND c.customerID = CA.customerID AND (phoneNumber = \'{data}\' OR emailAddress = \'{data}\') " +
                    $"UNION ALL SELECT emailAddress, phoneNumber FROM staff S, staff_account SA WHERE status = 'active' AND s.staffID = sa.staffID AND(phoneNumber = \'{data}\' OR emailAddress = \'{data}\');)";
                adr = new MySqlDataAdapter(sqlStr, conn);
                adr.Fill(dt);
                adr.Dispose();

                if (dt.Rows.Count >= 1)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                return false; //Something went wrong.
            }
        }

        //Update the user's info in the database.
        public bool modify(dynamic info)
        {
            try
            {
                conn.Open();
                if (accountType.Equals("Customer"))
                    sqlStr =
                        $"UPDATE customer SET firstName = \'{info.fName}\', lastName = \'{info.lName}\', sex = \'{info.sex}\', phoneNumber = \'{info.phone}\'" +
                        $", paymentMethod = \'{info.pay}\', dateofBirth = {info.DFB}, company = \'{info.corp}\' WHERE customerID = \'{UID}\'";
                else
                    sqlStr =
                        $"UPDATE staff SET firstName = \'{info.fName}\', lastName = \'{info.lName}\', sex = \'{info.sex}\', phoneNumber = \'{info.phone}\', dateofBirth = {info.DFB} WHERE staffID = \'{UID}\'";
                cmd = new MySqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false; //Something went wrong.
            }
        }

        //Update the address in the database.
        public bool modifyAdd(dynamic Addinfo)
        {
            try
            {
                conn.Open();
                sqlStr =
                    $"UPDATE customer SET province = \'{Addinfo.province}\', city = \'{Addinfo.city}\', companyAddress = \'{Addinfo.corpAdd}\'" +
                    $", warehouseAddress = \'{Addinfo.wAdd1}\', warehouseAddress2 = \'{Addinfo.wAdd2}\' WHERE customerID = \'{UID}\'";
                cmd = new MySqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();

                sqlStr = $"UPDATE customer_dfadd SET dfadd = \'{Addinfo.dfvalue}\' WHERE customerID = \'{UID}\'";
                cmd = new MySqlCommand(sqlStr, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false; //Something went wrong.
            }
        }
    }
}