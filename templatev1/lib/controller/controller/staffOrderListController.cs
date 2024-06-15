using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace controller
{
    public class staffOrderListController
    {
        private readonly Database _db;
        public staffOrderListController(Database database = null)
        {
            _db = database ?? new Database();
        }

        public DataTable getOrder(string id, string status, string sortBy, bool isManager) {  //id = staff id
            String sqlCmd = "";
            if (isManager) {
                if (status == "All") {
                    if (sortBy == "Id") //order id ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID ORDER BY x.orderID";
                    }
                    else if (sortBy == "IdDESC")  //order id descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID ORDER BY x.orderID DESC";
                    }
                    else if (sortBy == "Date")  //date ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID ORDER BY x.orderDate";
                    }
                    else //date descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID ORDER BY x.orderDate DESC";
                    }
                }
                else
                {
                    if (sortBy == "Id") //order id ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.status = \'{status}\' ORDER BY x.orderID";
                    }
                    else if (sortBy == "IdDESC")  //order id descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.status = \'{status}\' ORDER BY x.orderID DESC";
                    }
                    else if (sortBy == "Date")  //date ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.status = \'{status}\' ORDER BY x.orderDate";
                    }
                    else //date descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.status = \'{status}\' ORDER BY x.orderDate DESC";
                    }
                }
            }
            return _db.ExecuteDataTable(sqlCmd,null);

            //string sqlCmd = sortBy == "All"
            //    ? $"SELECT * FROM order_ WHERE customerAccountID = '{GetCustomerAccountId(id)}'"
            //    : $"SELECT * FROM order_ WHERE customerAccountID = '{GetCustomerAccountId(id)}' AND status = '{sortBy}'";


        }
    }
}


//None
//Order ID (Ascending)
//OrderID(Descending)
//Order Date(Nearest)
//Order Date(Furtherest)