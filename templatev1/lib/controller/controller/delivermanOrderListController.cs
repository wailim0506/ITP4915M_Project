using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace controller
{
    public class delivermanOrderListController
    {

        private readonly Database _db;

        public delivermanOrderListController(Database database = null)
        {
            _db = database ?? new Database();
        }

        public DataTable getAllOrder(string id,string sortBy) //staff id
        {
            string delivermanID = getDelivermanID(id);
            string status = "Ready to Ship";
            string sqlCmd =
                $"SELECT x.*,y.status from shipping_detail x, order_ y WHERE x.delivermanID = \'{delivermanID}\' AND x.orderID = y.orderID AND y.status = \'{status}\'";
            return _db.ExecuteDataTable(sqlCmd);
        }

        public string getDelivermanID(string id) //staff id
        {
            string sqlCmd =
                $"SELECT delivermanID from staff WHERE staffID = \'{id}\'";
            return _db.ExecuteDataTable(sqlCmd).Rows[0][0].ToString();
        }
    }
}
