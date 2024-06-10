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
        private Database _db;
        private AccountController _ac;

        public orderListController(Database db = null)
        {
            _db = db ?? new Database(); 
            _ac = new AccountController();
            sqlCmd = "";
        }

        public string getCustomerAccountID(string id) //id = customerID
        {
            return _db.ExecuteDataTable($"SELECT customerAccountID FROM customer_account WHERE customerID = '{id}'").Rows[0][0].ToString();
        }

        public int countOrder(string id, string sortBy)
        {
            string sqlCmd = sortBy == "All" 
                ? $"SELECT COUNT(*) FROM order_ WHERE customerAccountID = '{getCustomerAccountID(id)}'"
                : $"SELECT COUNT(*) FROM order_ WHERE customerAccountID = '{getCustomerAccountID(id)}' AND status = '{sortBy}'";

            return int.Parse(_db.ExecuteDataTable(sqlCmd).Rows[0][0].ToString());
        }

        public DataTable getOrder(string id, string sortBy)
        {
            string sqlCmd = sortBy == "All" 
                ? $"SELECT * FROM order_ WHERE customerAccountID = '{getCustomerAccountID(id)}'"
                : $"SELECT * FROM order_ WHERE customerAccountID = '{getCustomerAccountID(id)}' AND status = '{sortBy}'";

            return _db.ExecuteDataTable(sqlCmd);
        }

        public string getStafftID(string id) //staff account id
        {
            DataTable dt = _ac.GetStaffDetail(id);
            return dt.Rows[0][0].ToString();
        }

        public string getStaffName(string id) //staff account id
        {
            DataTable dt = _ac.GetStaffDetail(id);
            return $"{dt.Rows[0][2].ToString()} {dt.Rows[0][3].ToString()}";
        }

        public string getStaffContact(string id)
        {
            DataTable dt = _ac.GetStaffDetail(id);
            return dt.Rows[0][6].ToString();
        }
    }
}