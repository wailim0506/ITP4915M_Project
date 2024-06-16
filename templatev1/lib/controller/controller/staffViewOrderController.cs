using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controller
{
    public class staffViewOrderController : abstractController
    {
        string sqlCmd;
        public staffViewOrderController(Database database = null)
        {
            sqlCmd = "";
            _db = database ?? new Database();
        }

        public DataTable getOrder(string id) //orderID
        {
            string sqlCmd = $"SELECT * FROM order_ WHERE orderID = \'{id}\'";
            DataTable dt = new DataTable();
            dt = _db.ExecuteDataTable(sqlCmd, null);
            return dt;
        }

        public string getStafftID(string id) //staff account id
        {
            AccountController ac = new AccountController();
            DataTable dt = ac.GetStaffDetail(id);
            return dt.Rows[0][0].ToString();
        }

        public string getStaffName(string id) //staff account id
        {
            AccountController ac = new AccountController();
            DataTable dt = ac.GetStaffDetail(id);
            return $"{dt.Rows[0][2].ToString()} {dt.Rows[0][3].ToString()}";
        }

        public string getStaffContact(string id)
        {
            AccountController ac = new AccountController();
            DataTable dt = ac.GetStaffDetail(id);
            return dt.Rows[0][6].ToString();
        }

        public DataTable getOrderedSparePart(string id, string sortBy) //orderID
        {
            switch (sortBy)
            {
                case "None":
                    sqlCmd = $"SELECT *,(quantity*orderUnitPrice)FROM order_line WHERE orderID = \'{id}\'";
                    break;
                case "Quantity(Ascending)":
                    sqlCmd =
                        $"SELECT *,(quantity*orderUnitPrice) FROM order_line WHERE orderID = \'{id}\' ORDER BY quantity";
                    break;
                case "Quantity(Descending)":
                    sqlCmd =
                        $"SELECT *,(quantity*orderUnitPrice) FROM order_line WHERE orderID = \'{id}\' ORDER BY quantity DESC";
                    break;
                case "Total Price(Ascending)":
                    sqlCmd =
                        $"SELECT *,(quantity*orderUnitPrice) FROM order_line WHERE orderID = \'{id}\' ORDER BY (quantity*orderUnitPrice)";
                    break;
                case "Total Price(Descending)":
                    sqlCmd =
                        $"SELECT *,(quantity*orderUnitPrice) FROM order_line WHERE orderID = \'{id}\' ORDER BY (quantity*orderUnitPrice) DESC";
                    break;
            }

            DataTable dt = new DataTable();
            dt = _db.ExecuteDataTable(sqlCmd, null);
            return dt;
        }


        public string getItemNum(string id) //part number
        {
            string query = $"SELECT itemID FROM product WHERE partNumber = \'{id}\'";
            DataTable dt = new DataTable();
            dt = _db.ExecuteDataTable(query, null);
            return dt.Rows[0][0].ToString();
        }

        public string getPartName(string id)
        {
            DataTable dt = new DataTable();
            string query = $"SELECT name FROM spare_part WHERE partNumber = \'{id}\'";
            dt = _db.ExecuteDataTable(query, null);
            return dt.Rows[0][0].ToString();
        }

        public DataTable GetShippingDetail(string id) //orderID
        {
            //orderID
            DataTable dt = new DataTable();
            string query = $"SELECT * FROM shipping_detail WHERE orderID = \'{id}\'";
            dt = _db.ExecuteDataTable(query, null);
            return dt;
        }

        public string[] GetDelivermanDetail(string id) //orderID
        {
            string delivermanID = ExecuteSqlQueryAndReturnFirstRow(
                $"SELECT delivermanID FROM shipping_detail WHERE orderID = \'{id}\'");

            //get deliverman name and contact from staff table
            DataTable dt = _db.ExecuteDataTable(
                $"SELECT firstName, lastName, phoneNumber FROM staff WHERE delivermanID = \'{delivermanID}\'", null);

            return new string[] { dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString() };
        }

        private string ExecuteSqlQueryAndReturnFirstRow(string s)
        {
            DataTable dt = _db.ExecuteDataTable(s, null);
            return dt.Rows[0][0].ToString();
        }
    }
}
