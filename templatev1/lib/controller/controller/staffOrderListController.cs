using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public DataTable GetOrder(string id, string status, string sortBy, bool isManager)
        {
            string statusCondition = status == "All" ? "" : $"AND x.status = '{status}'";
            string staffCondition = isManager
                ? ""
                : $"AND x.staffAccountID = (SELECT staffAccountID FROM staff_account WHERE staffID = '{id}')";
            string orderCondition = sortBy.EndsWith("DESC") ? $"{sortBy.Substring(0, sortBy.Length - 4)} DESC" : sortBy;
            string sqlCmd = $"SELECT x.orderID, x.orderDate, y.customerID, z.shippingDate, x.status FROM order_ x, " +
                            $"customer_account y, shipping_detail z WHERE x.orderID = z.orderID AND y.customerAccountID = " +
                            $"x.customerAccountID {statusCondition} {staffCondition} ORDER BY {orderCondition}";
            return _db.ExecuteDataTable(sqlCmd, null) ?? throw new Exception("No data found");
        }
    }
}