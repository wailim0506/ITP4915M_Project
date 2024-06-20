using System;
using System.Collections.Generic;
using System.Data;
using controller.Utilities;

namespace controller
{
    public class staffOrderListController : abstractController
    {
        private readonly Database _db;

        public staffOrderListController(Database database = null)
        {
            _db = database ?? new Database();
        }

        public DataTable getOrder(string id, string status, string sortBy, bool isManager) //id = staff id
        {
            string sqlCmd = "";
            var sortByOptions = new Dictionary<string, string>
            {
                { "Id", "x.orderID" },
                { "IdDESC", "x.orderID DESC" },
                { "Date", "x.orderDate DESC" },
                { "DateDESC", "x.orderDate" },
                { "DDate", "z.shippingDate" },
                { "DDateDESC", "z.shippingDate DESC" },
                { "cId", "y.customerID" },
                { "cIdDESC", "y.customerID DESC" }
            };

            if (!sortByOptions.ContainsKey(sortBy)) return _db.ExecuteDataTable(sqlCmd, null);
            sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                     $"customer_account y, shipping_detail z";
            sqlCmd += !isManager
                ? $", staff_account aa WHERE x.orderID = z.orderID AND y.customerAccountID = x.customerAccountID AND x.staffAccountID = aa.staffAccountID AND aa.staffID = \'{id}\'"
                : " WHERE x.orderID = z.orderID AND y.customerAccountID = x.customerAccountID";

            if (status != "All")
            {
                sqlCmd += $" AND x.status = '{status}'";
            }

            sqlCmd += $" ORDER BY {sortByOptions[sortBy]}";

            return _db.ExecuteDataTable(sqlCmd, null);
        }
    }
}