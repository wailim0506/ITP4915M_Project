using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;      
using MySqlConnector;

namespace controller
{
    public class orderListController : abstractController
    {
        string sqlCmd;
        public orderListController()
        {
            sqlCmd = "";
        }

        public string getCustomerAccountID(string id) //id = customerID
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT customerAccountID FROM customer_account WHERE customerID = \'{id}\' ";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public int countOrder(string id) 
        {

            DataTable dt = new DataTable();
            sqlCmd = $"SELECT COUNT(*) FROM order_ WHERE customerAccountID = \'{getCustomerAccountID(id)}\' ";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public DataTable getOrder(string id)
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT * FROM order_ WHERE customerAccountID = \'{getCustomerAccountID(id)}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public string getStaffAccountID(string id) //staff account id
        {
            controller.accountController ac = new controller.accountController();
            DataTable dt = ac.getStaffDetail(id);
            return dt.Rows[0][0].ToString();
        }

        public string getStaffName(string id) //staff account id
        {
            controller.accountController ac = new controller.accountController();
            DataTable dt = ac.getStaffDetail(id);
            return $"{dt.Rows[0][2].ToString()} {dt.Rows[0][3].ToString()}";
        }

        public string getStaffContact(string id)
        {
            controller.accountController ac = new controller.accountController();
            DataTable dt = ac.getStaffDetail(id);
            return dt.Rows[0][6].ToString();
        }
    }
}
