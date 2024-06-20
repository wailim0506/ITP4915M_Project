using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using controller.Utilities;

namespace controller
{
    public class staffInvoiceListController : abstractController
    {
        private readonly Database _db;

        public staffInvoiceListController(Database database = null)
        {
            _db = database ?? new Database();
        }

        public DataTable getData(string id, string sortBy, string status, bool isManager) //id = staff id
        {
            string sqlCmd = "";
            var sortByOptions = new Dictionary<string, string>
            {
                { "IN", "x.invoiceNumber" },
                { "INDESC", "x.invoiceNumber DESC" },
                { "ODate", "y.orderDate DESC" },
                { "ODateDESC", "y.orderDate" },
                { "DDate", "z.shippingDate" },
                { "DDateDESC", "z.shippingDate DESC" },
                { "OID", "x.orderID" },
                { "OIDDESC", "x.orderID DESC" }
            };


            sqlCmd +=
                $"SELECT x.orderID,x.invoiceNumber,x.status, y.orderDate, z.shippingDate FROM invoice x, order_ y, " +
                $"shipping_detail z, staff_account aa WHERE x.orderID = y.orderID AND y.orderID = z.orderID AND y.staffAccountID = aa.staffAccountID " +
                $"AND y.status = \'Shipped\'";

            sqlCmd += !isManager
                ? $" AND aa.staffID = \'{id}\'"
                : "";

            if (status != "All")
            {
                switch (status)
                {
                    case "Confirmed":
                        sqlCmd += $" AND x.status = \'confirmed\'";
                        break;
                    case "Not Confirmed":
                        sqlCmd += $" AND x.status IS NULL";
                        break;
                }
            }

            if (sortBy != "")
            {
                sqlCmd += $" ORDER BY {sortByOptions[sortBy]}";
            }

            return _db.ExecuteDataTable(sqlCmd, null);
        }
    }
}

//All
//Confirmed
//Not Confirmed


//Invoice Number(Ascending)
//Invoice Number(Descending)
//Order Date(Nearest)
//Order Date(Furtherest)
//Order ID(Ascending)
//Order ID(Descending)
//Delivery Date(Nearest)
//Delivery Date(Furtherest)