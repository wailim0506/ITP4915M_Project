using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace controller
{
    public class staffOrderListController : abstractController
    {
        private readonly Database _db;
        public staffOrderListController(Database database = null)
        {
            _db = database ?? new Database();
        }


        public DataTable getOrder(string id, string status, string sortBy, bool isManager) {  //id = staff id
            String sqlCmd = "";
            if (isManager)
            {
                if (status == "All")
                {
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
                    else if (sortBy == "Date")  //order date ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID ORDER BY x.orderDate DESC";
                    }
                    else if (sortBy == "DateDESC") //order date descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID ORDER BY x.orderDate ";
                    }
                    else if (sortBy == "DDate")  //delivery date ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID ORDER BY z.shippingDate ";
                    }
                    else if (sortBy == "DDateDESC") //delivery date descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID ORDER BY z.shippingDate DESC";
                    }
                    else if (sortBy == "cId") //  customer id ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID ORDER BY y.customerID";
                    }
                    else
                    { //customer id descending
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID ORDER BY y.customerID DESC";
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
                    else if (sortBy == "Date")  //order date ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.status = \'{status}\' ORDER BY x.orderDate DESC";
                    }
                    else if (sortBy == "DateDESC")//order date descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.status = \'{status}\' ORDER BY x.orderDate";
                    }
                    else if (sortBy == "DDate")  //delivery date ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.status = \'{status}\' ORDER BY z.shippingDate ";
                    }
                    else if (sortBy == "DDateDESC") //delivery date descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.status = \'{status}\' ORDER BY z.shippingDate DESC";
                    }
                    else if (sortBy == "cId") //  customer id ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.status = \'{status}\' ORDER BY y.customerID";
                    }
                    else
                    { //customer id descending
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.status = \'{status}\' ORDER BY y.customerID DESC";
                    }
                }
            }
            else { //not manager, can only see the order assigned to him
                if (status == "All")
                {
                    if (sortBy == "Id") //order id ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' ORDER BY x.orderID";
                    }
                    else if (sortBy == "IdDESC")  //order id descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' ORDER BY x.orderID DESC";
                    }
                    else if (sortBy == "Date")  //order date ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' ORDER BY x.orderDate DESC";
                    }
                    else if (sortBy == "DateDESC") //order date descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' ORDER BY x.orderDate ";
                    }
                    else if (sortBy == "DDate")  //delivery date ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' ORDER BY z.shippingDate ";
                    }
                    else if (sortBy == "DDateDESC") //delivery date descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' ORDER BY z.shippingDate DESC";
                    }
                    else if (sortBy == "cId") //  customer id ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' ORDER BY y.customerID";
                    }
                    else
                    { //customer id descending
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' ORDER BY y.customerID DESC";
                    }
                }
                else
                {
                    if (sortBy == "Id") //order id ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' AND x.status = \'{status}\' ORDER BY x.orderID";
                    }
                    else if (sortBy == "IdDESC")  //order id descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' AND x.status = \'{status}\' ORDER BY x.orderID DESC";
                    }
                    else if (sortBy == "Date")  //order date ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' AND x.status = \'{status}\' ORDER BY x.orderDate DESC";
                    }
                    else if (sortBy == "DateDESC")//order date descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' AND x.status = \'{status}\' ORDER BY x.orderDate";
                    }
                    else if (sortBy == "DDate")  //delivery date ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' AND x.status = \'{status}\' ORDER BY z.shippingDate ";
                    }
                    else if (sortBy == "DDateDESC") //delivery date descending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' AND x.status = \'{status}\' ORDER BY z.shippingDate DESC";
                    }
                    else if (sortBy == "cId") //  customer id ascending
                    {
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' AND x.status = \'{status}\' ORDER BY y.customerID";
                    }
                    else
                    { //customer id descending
                        sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                                $"customer_account y, shipping_detail z, staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                                $"x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\' AND x.status = \'{status}\' ORDER BY y.customerID DESC";
                    }
                }
            }
            return _db.ExecuteDataTable(sqlCmd,null);
        }
    }
}

