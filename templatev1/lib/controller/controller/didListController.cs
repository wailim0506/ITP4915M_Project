using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using controller.Utilities;

namespace controller
{
    public class didListController : abstractController
    {
        private readonly Database _db;

        public didListController(Database database = null)
        {
            _db = database ?? new Database();
        }
        public DataTable getData(string sortBy, string id) //id = order id 
        {
            DataTable dt;
            string sqlCmd = "";

            switch (sortBy) 
            {
                case "Part Number (Ascending)":
                    sqlCmd = $"SELECT x.partNumber,y.name, x.quantity FROM order_line x, spare_part y " +
                             $"WHERE x.partNumber = y.partNumber AND x.orderID = \'{id}\' ORDER BY x.partNumber";
                    break;
                case "Part Number (Descending)":
                    sqlCmd = $"SELECT x.partNumber,y.name, x.quantity FROM order_line x, spare_part y " +
                             $"WHERE x.partNumber = y.partNumber AND x.orderID = \'{id}\' ORDER BY x.partNumber DESC";
                    break;
                case "Quantity (Ascending)":
                    sqlCmd = $"SELECT x.partNumber,y.name, x.quantity FROM order_line x, spare_part y " +
                             $"WHERE x.partNumber = y.partNumber AND x.orderID = \'{id}\' ORDER BY x.quantity";
                    break;
                case "Quantity (Descending)":
                    sqlCmd = $"SELECT x.partNumber,y.name, x.quantity FROM order_line x, spare_part y " +
                             $"WHERE x.partNumber = y.partNumber AND x.orderID = \'{id}\' ORDER BY x.quantity DESC";
                    break;
                default:  //sort by = none
                    sqlCmd = $"SELECT x.partNumber,y.name, x.quantity FROM order_line x, spare_part y " +
                             $"WHERE x.partNumber = y.partNumber AND x.orderID = \'{id}\'";
                    break;
            }

            dt = _db.ExecuteDataTable(sqlCmd, null);
            return dt;
        }
    }
}
