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

        public DataTable getAllOrder(string id, string sortBy) //staff id
        {
            string delivermanID = getDelivermanID(id);
            string status = "Ready to Ship";
            string sqlCmd = "";

            if (sortBy == "Nearest Dates")
            {
                sqlCmd = $"SELECT x.*,y.status from shipping_detail x, order_ y WHERE x.delivermanID = \'{delivermanID}\' AND x.orderID = y.orderID AND y.status = \'{status}\'";
            }
            else
            {
                sqlCmd = $"SELECT x.*,y.status from shipping_detail x, order_ y WHERE x.delivermanID = \'{delivermanID}\' AND x.orderID = y.orderID AND y.status = \'{status}\' ORDER BY shippingDate DESC";
            }
            return _db.ExecuteDataTableAsync(sqlCmd).Result;
        }

        public DataTable getAllFinishedOrder(string id, string sortBy) //staff id
        {
            string delivermanID = getDelivermanID(id);
            string status = "Shipped";
            string sqlCmd = "";

            if (sortBy == "Nearest Dates")
            {
                sqlCmd =
                    $"SELECT x.*,y.status from shipping_detail x, order_ y WHERE x.delivermanID = \'{delivermanID}\' AND x.orderID = y.orderID AND y.status = \'{status}\' ORDER BY shippingDate DESC";
            }
            else
            {
                sqlCmd =
                    $"SELECT x.*,y.status from shipping_detail x, order_ y WHERE x.delivermanID = \'{delivermanID}\' AND x.orderID = y.orderID AND y.status = \'{status}\'";

            }
            return _db.ExecuteDataTableAsync(sqlCmd).Result;
        }

        public string getDelivermanID(string id) //staff id
        {
            string sqlCmd =
                $"SELECT delivermanID from staff WHERE staffID = \'{id}\'";
            return _db.ExecuteDataTableAsync(sqlCmd).Result.Rows[0][0].ToString();
        }
    }
}