using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace controller
{
    public class editOrderController : abstractController
    {
        string sqlCmd;
        viewOrderController c;
        private readonly Database _db;

        public editOrderController(Database database = null)
        {
            sqlCmd = "";
            _db = database ?? new Database();
            c = new viewOrderController(_db);
        }

        // getter
        public DataTable GetOrderedSparePart(string orderID, string sortBy)
        {
            return c.getOrderedSparePart(orderID, sortBy);
        }

        public string GetItemNum(string id) //part Number
        {
            return c.getItemNum(id);
        }

        public string GetPartName(string id) //part Number
        {
            return c.getPartName(id);
        }

        public DataTable GetShippingDetail(string id) //orderID
        {
            return c.getShippingDetail(id);
        }

        public string[] GetDelivermanDetail(string id) //orderID
        {
            return c.getDelivermanDetail(id);
        }

        public DataTable GetOrder(string id) //orderID
        {
            return c.getOrder(id);
        }

        public string GetStaffName(string id) //staff account id
        {
            return c.getStaffName(id);
        }

        public string GetStafftId(string id) //staff account id
        {
            return c.getStafftID(id);
        }

        public string GetStaffContact(string id)
        {
            return c.getStaffContact(id);
        }

        // Database operation
        //part num //order id
        public int GetPartQtyInOrder(string num, string id) //part num //order id
        {
            return int.Parse(
                _db.ExecuteDataTableAsync($"SELECT quantity FROM order_line WHERE partNumber = '{num}' and orderID = '{id}'").Result
                    .Rows[0][0].ToString());
        }

        public Boolean AddQtyBack(string num, int currentOrderQty, int desiredQty,
            Boolean isLM)
        {
            cartController cc = new cartController();
            try
            {
                cc.addQtyBack(num, currentOrderQty, desiredQty, isLM);
                return true;
            }
            catch (notEnoughException e)
            {
                throw e;
            }
        }

        public Boolean
            AddQtyBackToSparePart(string num, string id, int currentOrderQty) //part num, order id

        {
            int partQtyInSparePart = GetSpareQtyInDb(num); //current qty in db

            partQtyInSparePart += currentOrderQty;
            try
            {
                _ = _db.ExecuteNonQueryCommandAsync(
                    "UPDATE spare_part SET quantity = @qty WHERE partNumber = @num",
                    new Dictionary<string, object> { { "@qty", partQtyInSparePart }, { "@num", num } });
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public int GetSpareQtyInDb(string num)
        {
            int qtyInSpare_Part = int.Parse(_db.ExecuteDataTableAsync(
                $"SELECT quantity FROM spare_part " +
                $"WHERE partNumber = '{num}'").Result.Rows[0][0].ToString());
            return qtyInSpare_Part;
        }

        public int getPartQtyInOrder(string id, string num) //order id, part num
        {
            sqlCmd = $"SELECT quantity from order_line WHERE partNumber = \'{num}\' AND orderID = \'{id}\'";
            return int.Parse(_db.ExecuteDataTableAsync(sqlCmd).Result.Rows[0][0].ToString());
        }

        public Boolean EditDbQty(string num, int desiredQty, Boolean isLM, string id) //order id
        {
            cartController cc = new cartController();
            if (cc.EditDbQty(num, desiredQty, isLM, true, getPartQtyInOrder(id, num)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //id = order id, num = part num, qty = new qty wanted
        public Boolean EditOrderLineQuantity(string id, string num, string qty)
        {
            try
            {
                _ = _db.ExecuteNonQueryCommandAsync(
                    "UPDATE order_line SET quantity = @qty WHERE partNumber = @num AND orderID = @id",
                    new Dictionary<string, object> { { "@qty", qty }, { "@num", num }, { "@id", id } });
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public Boolean DeleteSparePart(string id, string num) //id = order id, num = part num
        {
            try
            {
                _ = _db.ExecuteNonQueryCommandAsync("DELETE FROM order_line WHERE partNumber = @num AND orderID = @id",
                    new Dictionary<string, object> { { "@num", num }, { "@id", id } });
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public void DeleteOrder(string id) //order id
        {
            c.deleteOrder(id);
        }
    }
}