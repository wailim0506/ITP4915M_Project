using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace controller
{
    public class cartController : abstractController
    {
        string sqlCmd;
        private readonly Database _db;

        public cartController(Database database = null)
        {
            sqlCmd = "";
            _db = database ?? new Database();
        }

        public DataTable getCartItem(string id)
        {
            string cartID = getCartID(id);
            string sqlCmd =
                $"SELECT * from product_in_cart x, product y, spare_part z where x.cartID = \'{cartID}\' AND x.itemID = y.itemID AND y.partNumber = z.partNumber";
            return _db.ExecuteDataTable(sqlCmd);
        }

        public List<string> getAllPartNumInCart(string id) //customer id  //for remove all item
        {
            string cartID = getCartID(id);
            string sqlCmd = $"SELECT itemID FROM product_in_cart WHERE cartID = \'{cartID}\'";
            DataTable dt = _db.ExecuteDataTable(sqlCmd);

            List<string> itemId = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                itemId.Add(dt.Rows[i][0].ToString());
            }

            List<string> partNum = new List<string>();
            for (int i = 0; i < itemId.Count; i++)
            {
                dt = new DataTable();
                sqlCmd = $"SELECT partNumber FROM product WHERE itemID = \'{itemId[i]}\'";
                dt = _db.ExecuteDataTable(sqlCmd);
                partNum.Add(dt.Rows[0][0].ToString());
            }

            return partNum;
        }

        public List<int> getAllItemQtyInCart(string id) //for remove all item
        {
            string cartID = getCartID(id);
            string sqlCmd = $"SELECT quantity FROM product_in_cart WHERE cartID = \'{cartID}\'";
            DataTable dt = _db.ExecuteDataTable(sqlCmd);

            List<int> itemQty = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                itemQty.Add(int.Parse(dt.Rows[i][0].ToString()));
            }

            return itemQty;
        }


        public Boolean RemovePart(string num, string id) //partNum, customer id
        {
            string cartID = getCartID(id);
            string sqlCmd = $"SELECT itemID FROM product WHERE partNumber = \'{num}\'";
            DataTable dt = _db.ExecuteDataTable(sqlCmd);
            string itemID = dt.Rows[0][0].ToString();

            sqlCmd = "DELETE FROM product_in_cart WHERE itemID = @id AND cartID = @cartID";
            var parameters = new Dictionary<string, object>
            {
                { "@id", itemID },
                { "@cartID", cartID }
            };

            try
            {
                _db.ExecuteNonQueryCommand(sqlCmd, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to remove part", ex);
            }

            return true;
        }

        public Boolean removeAll(string id) //customer id
        {
            string cartID = getCartID(id);
            string sqlCmd = "DELETE FROM product_in_cart WHERE cartID = @cartID";
            var parameters = new Dictionary<string, object>
            {
                { "@cartID", cartID }
            };

            try
            {
                _db.ExecuteNonQueryCommand(sqlCmd, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to remove all items", ex);
            }

            return true;
        }

        public string getCartID(string id) //customer id
        {
            string sqlCmd = $"SELECT customerAccountID FROM customer_account WHERE customerID = '{id}'";
            string customerAccointID = _db.ExecuteDataTable(sqlCmd).Rows[0][0].ToString();

            sqlCmd = $"SELECT cartID FROM cart WHERE customerAccountID = '{customerAccointID}'";
            return _db.ExecuteDataTable(sqlCmd).Rows[0][0].ToString();
        }

        public int GetCurrentQtyInCart(string num, string id) //part num, customer id
        {
            string cartID = getCartID(id);
            string itemID = _db.ExecuteDataTable($"SELECT itemID FROM product " +
                                                 $"WHERE partNumber = '{num}'").Rows[0][0].ToString();
            return int.Parse(_db.ExecuteDataTable($"SELECT quantity FROM product_in_cart " +
                                                  $"WHERE itemID = '{itemID}' " +
                                                  $"AND cartID = '{cartID}'").Rows[0][0].ToString());
        }

        //add qty back to db for product table and spare_part table
        public Boolean addQtyBack(string num, int currentCartQty, int desiredQty, Boolean isLM) //part num 
        {
            //get the qty in db first

            int qtyInProduct = getOnSaleQtyInDb(num, isLM);
            int qtyInSpare_Part = GetSpareQtyInDb(num);

            //add db qty with cart qty
            qtyInProduct += currentCartQty;
            qtyInSpare_Part += currentCartQty;

            try
            {
                if ((qtyInProduct - desiredQty < 0) ||
                    (qtyInSpare_Part - desiredQty <
                     0)) //check if the desired qty is larger than availabe qty after add cart qty to db
                {
                    throw new notEnoughException();
                }
            }
            catch (notEnoughException e)
            {
                throw e;
            }


            //Add to db
            if (!isLM)
            {
                sqlCmd = $"UPDATE product SET onSaleQty = @qty WHERE partNumber = @num";
            }
            else
            {
                sqlCmd = $"UPDATE product SET LM_onSaleQty = @qty WHERE partNumber = @num";
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@qty", qtyInProduct);
                        command.Parameters.AddWithValue("@num", num);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

            //sqlCmd = $"UPDATE spare_part SET quantity = @qty WHERE partNumber = @num";
            //try
            //{
            //    using (MySqlConnection connection = new MySqlConnection(connString))
            //    {
            //        connection.Open();

            //        using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
            //        {
            //            command.Parameters.AddWithValue("@qty", qtyInSpare_Part);
            //            command.Parameters.AddWithValue("@num", num);

            //            command.ExecuteNonQuery();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            //finally
            //{
            //    conn.Close();
            //}

            return true;
        }

        public Boolean editDbQty(string num, int newQty, Boolean isLM) //part num 
        {
            //get the qty in db first
            int qtyInProduct = getOnSaleQtyInDb(num, isLM);
            int qtyInSpare_Part = GetSpareQtyInDb(num);

            //deduct db qty with cart qty
            qtyInProduct -= newQty;
            qtyInSpare_Part -= newQty;

            //edit to db
            if (!isLM)
            {
                sqlCmd = $"UPDATE product SET onSaleQty = @qty WHERE partNumber = @num";
            }
            else
            {
                sqlCmd = $"UPDATE product SET LM_onSaleQty = @qty WHERE partNumber = @num";
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@qty", qtyInProduct);
                        command.Parameters.AddWithValue("@num", num);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

            //sqlCmd = $"UPDATE spare_part SET quantity = @qty WHERE partNumber = @num";
            //try
            //{
            //    using (MySqlConnection connection = new MySqlConnection(connString))
            //    {
            //        connection.Open();

            //        using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
            //        {
            //            command.Parameters.AddWithValue("@qty", qtyInSpare_Part);
            //            command.Parameters.AddWithValue("@num", num);

            //            command.ExecuteNonQuery();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            //finally
            //{
            //    conn.Close();
            //}

            return true;
        }

        public Boolean editCartQty(string num, string id, int newQty) //part num, customer id, new qty
        {
            string cartID = getCartID(id);
            string itemID = _db.ExecuteDataTable($"SELECT itemID FROM product WHERE partNumber = '{num}'").Rows[0][0]
                .ToString();

            var parameters = new Dictionary<string, object>
            {
                { "@qty", newQty },
                { "@num", itemID },
                { "@cartID", cartID }
            };

            try
            {
                _db.ExecuteNonQueryCommand(
                    "UPDATE product_in_cart SET quantity = @qty WHERE itemID = @num AND cartID = @cartID", parameters);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public int getOnSaleQtyInDb(string num, Boolean isLM) //part num
        {
            string column = isLM ? "LM_onSaleQty" : "onSaleQty";
            string sqlCmd = $"SELECT {column} FROM product WHERE partNumber = '{num}'";
            int qtyInProduct = int.Parse(_db.ExecuteDataTable(sqlCmd).Rows[0][0].ToString());
            return qtyInProduct;
        }

        public int GetSpareQtyInDb(string num)
        {
            int qtyInSpare_Part = int.Parse(_db.ExecuteDataTable(
                $"SELECT quantity FROM spare_part " +
                $"WHERE partNumber = '{num}'").Rows[0][0].ToString());
            return qtyInSpare_Part;
        }

        public Boolean CreateOrder(string id, string shippingDate) //customerID
        {
            //get the customer account id first
            string customerAccountID =
                _db.ExecuteDataTable(
                    $"SELECT customerAccountID " +
                    $"FROM customer_account " +
                    $"WHERE customerID = '{id}'").Rows[0][0].ToString();
            //generate an order id
            string orderID = OrderIdGenerator();
            //generate order serial num
            string orderSerialNum = OrderSerialNumGenerator();
            //assign an order processing clerk to this order
            string staffAccountID = OrderProcessingClerkAssignment();
            //get today date
            string orderDate = DateTime.Now.ToString("yyyy-MM-dd");

            //create order
            var parameters = new Dictionary<string, object>
            {
                { "@orderID", orderID },
                { "@CAID", customerAccountID },
                { "@SAID", staffAccountID },
                { "@OSN", orderSerialNum },
                { "@date", orderDate },
                { "@status", "Pending" }
            };
            try
            {
                _db.ExecuteNonQueryCommand(
                    "INSERT INTO order_ (orderID, customerAccountID, staffAccountID, " +
                    "orderSerialNumber,orderDate, status) " +
                    "VALUES(@orderID,@CAID,@SAID,@OSN,@date,@status)", parameters);
            }
            catch (Exception ex)
            {
                return false;
            }

            //insert to order line, create invoice
            return CreateOrderLineRow(id, orderID) &&
                   createInvoice(customerAccountID, orderID) &&
                   createShippingDetail(orderID, shippingDate);
        }

        public Boolean CreateOrderLineRow(string cid, string id) //customer id, order id
        {
            //get item in cart
            DataTable dt = getCartItem(cid);
            //insert to db
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sqlCmd = $"INSERT INTO order_line VALUES(@partNum,@orderID,@qty,@price)";
                var parameters = new Dictionary<string, object>
                {
                    { "@partNum", dt.Rows[i][5].ToString() },
                    { "@orderID", id },
                    { "@qty", dt.Rows[i][2].ToString() },
                    { "@price", dt.Rows[i][8].ToString() }
                };

                try
                {
                    _db.ExecuteNonQueryCommand(sqlCmd, parameters);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }

        public Boolean createInvoice(string caid, string id) //customer account id, order id
        {
//count how many inovice in db first
            string sqlCmd = $"SELECT COUNT(*) FROM invoice";
            int numOfInvoice = Convert.ToInt32(_db.ExecuteScalarCommand(sqlCmd, null));

            numOfInvoice++; //invoice number of the order
            string invoiceNum = $"IN{numOfInvoice:D5}";

            sqlCmd =
                $"INSERT INTO invoice (customerAccountID, orderID, invoiceNumber) VALUES(@CAID,@orderID,@invoiceNum)";
            var parameters = new Dictionary<string, object>
            {
                { "@CAID", caid },
                { "@orderID", id },
                { "@invoiceNum", invoiceNum }
            };
            try
            {
                _db.ExecuteNonQueryCommand(sqlCmd, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create invoice", ex);
            }

            return true;
        }

        public Boolean createShippingDetail(string id, string shippingDate)
        {
            string deliverman = delivermanAssignment();
            string sqlCmd =
                "INSERT INTO shipping_detail (orderID, delivermanID, shippingDate) VALUES(@orderID,@deliverman,@date)";
            var parameters = new Dictionary<string, object>
            {
                { "@orderID", id },
                { "@deliverman", deliverman },
                { "@date", shippingDate }
            };

            try
            {
                _db.ExecuteNonQueryCommand(sqlCmd, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create shipping detail", ex);
            }

            return true;
        }

        public void ClearCustomerCartAfterCreateOrder(string id) //customer id
        {
            string cartID = getCartID(id);
            string sqlCmd = "DELETE FROM product_in_cart WHERE cartID = @cartID";
            var parameters = new Dictionary<string, object> { { "@cartID", cartID } };

            try
            {
                _db.ExecuteNonQueryCommand(sqlCmd, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to clear customer cart", ex);
            }
        }


        public string OrderIdGenerator()
        {
            string yearMonth = DateTime.Now.ToString("yyMM");
            string sqlCmd = $"SELECT COUNT(orderID) FROM order_ WHERE orderID LIKE 'OD{yearMonth}%'";

            //see how many order in this year month
            int orderInYearMonth = Convert.ToInt32(_db.ExecuteScalar(sqlCmd)) + 1; //+1 for order id
            return $"OD{yearMonth}{orderInYearMonth:D4}";
        }

        public string OrderSerialNumGenerator()
        {
            string yearMonth = DateTime.Now.ToString("yyMM");
            string sqlCmd =
                $"SELECT COUNT(orderSerialNumber) FROM order_ WHERE orderSerialNumber LIKE 'SN{yearMonth}%'";
            int orderInYearMonth = Convert.ToInt32(_db.ExecuteScalar(sqlCmd)) + 1; //+1 for order id

            //see how many order in this year month
            return $"SN{yearMonth}{orderInYearMonth:D4}";
        }

        public string OrderProcessingClerkAssignment()
        {
            //get num of opc first
            string sqlCmd = "SELECT staffID FROM staff WHERE jobTitle = 'order processing clerk'";
            DataTable dt = _db.ExecuteDataTable(sqlCmd);
            int numOfOpc = dt.Rows.Count;


            //random assignment
            Random random = new Random();
            int num = random.Next(0, numOfOpc);
            string staffIDAssigned = dt.Rows[num][0].ToString(); //db dont have enough data now
            //string staffIDAssigned = "LMS00002";

            //get the staff account id
            sqlCmd = $"SELECT staffAccountID FROM staff_account WHERE staffID = '{staffIDAssigned}'";
            dt = _db.ExecuteDataTable(sqlCmd);
            return dt.Rows[0][0].ToString();
        }

        public string delivermanAssignment()
        {
            //get num of opc first
            string sqlCmd = "SELECT delivermanID FROM deliverman";
            DataTable dt = _db.ExecuteDataTable(sqlCmd);
            int numOfDeliverman = dt.Rows.Count;

            //random assignment
            Random random = new Random();
            int num = random.Next(0, numOfDeliverman);
            return dt.Rows[num][0].ToString();
        }

        public DataTable getCustomerAddress(string id) //customer id
        {
            string sqlCmd = $"SELECT warehouseAddress, province, city FROM customer WHERE customerID = '{id}'";
            return _db.ExecuteDataTable(sqlCmd);
        }
    }

    class notEnoughException : Exception
    {
        public notEnoughException() : base("Not enough items in the cart.")
        {
        }
    }
}