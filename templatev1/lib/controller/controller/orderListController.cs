using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace controller
{
    public class orderListController : abstractController
    {
        string sqlCmd;
        private Database _db;
        private AccountController _ac;

        public orderListController(Database db = null)
        {
            _db = ServiceProvider.GetRequiredService<Database>();
            _ac = ServiceProvider.GetRequiredService<AccountController>();
            sqlCmd = "";
        }

        public string GetCustomerAccountId(string id) //id = customerID
        {
            return _db.ExecuteDataTable(
                    $"SELECT customerAccountID FROM customer_account WHERE customerID = '{id}'")
                .Rows[0][0].ToString();
        }

        public int CountOrder(string id, string sortBy)
        {
            string sqlCmd = sortBy == "All"
                ? $"SELECT COUNT(*) FROM order_ WHERE customerAccountID = '{GetCustomerAccountId(id)}'"
                : $"SELECT COUNT(*) FROM order_ WHERE customerAccountID = '{GetCustomerAccountId(id)}' AND status = '{sortBy}'";

            return int.Parse(_db.ExecuteDataTable(sqlCmd).Rows[0][0].ToString());
        }

        public DataTable GetOrder(string id, string sortBy)
        {
            string sqlCmd = sortBy == "All"
                ? $"SELECT * FROM order_ WHERE customerAccountID = '{GetCustomerAccountId(id)}'"
                : $"SELECT * FROM order_ WHERE customerAccountID = '{GetCustomerAccountId(id)}' AND status = '{sortBy}'";

            return _db.ExecuteDataTable(sqlCmd);
        }

        public string GetStafftId(string id) //staff account id
        {
            DataTable dt = _ac.GetStaffDetail(id);
            return dt.Rows[0][0].ToString();
        }

        public string GetStaffName(string id) //staff account id
        {
            DataTable dt = _ac.GetStaffDetail(id);
            return $"{dt.Rows[0][2].ToString()} {dt.Rows[0][3].ToString()}";
        }

        public string GetStaffContact(string id)
        {
            DataTable dt = _ac.GetStaffDetail(id);
            return dt.Rows[0][6].ToString();
        }
    }
}