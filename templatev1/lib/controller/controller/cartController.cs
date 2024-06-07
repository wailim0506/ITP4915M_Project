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

        public cartController()
        {
            sqlCmd = "";
        }

        public DataTable getCartItem(string id)
        {
            string cartID = getCartID(id);
            DataTable dt = new DataTable();
            sqlCmd =
                $"SELECT * from product_in_cart x, product y, spare_part z where x.cartID = \'{cartID}\' AND x.itemID = y.itemID AND y.partNumber = z.partNumber";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public List<string> getAllPartNumInCart(string id) //customer id  //for remove all item
        {
            string cartID = getCartID(id);
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT itemID FROM product_in_cart WHERE cartID = \'{cartID}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);

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
                adr = new MySqlDataAdapter(sqlCmd, conn);
                adr.Fill(dt);
                partNum.Add(dt.Rows[0][0].ToString());
            }

            return partNum;
        }

        public List<int> getAllItemQtyInCart(string id) //for remove all item
        {
            string cartID = getCartID(id);
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT quantity FROM product_in_cart WHERE cartID = \'{cartID}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);

            List<int> itemQty = new List<int>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                itemQty.Add(int.Parse(dt.Rows[i][0].ToString()));
            }

            return itemQty;
        }


        public Boolean removePart(string num, string id) //partNum, customer id
        {
            string cartID = getCartID(id);
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT itemID FROM product WHERE partNumber = \'{num}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string itemID = dt.Rows[0][0].ToString();

            sqlCmd = "DELETE FROM product_in_cart WHERE itemID = @id AND cartID = @cartID";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@id", itemID);
                        command.Parameters.AddWithValue("@cartID", cartID);

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

            return true;
        }

        public Boolean removeAll(string id) //customer id
        {
            string cartID = getCartID(id);
            sqlCmd = "DELETE FROM product_in_cart WHERE cartID = @cartID";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@cartID", cartID);

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

            return true;
        }

        public string getCartID(string id) //customer id
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT customerAccountID FROM customer_account WHERE customerID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string customerAccointID = dt.Rows[0][0].ToString();

            dt = new DataTable();
            sqlCmd = $"SELECT cartID FROM cart WHERE customerAccountID = \'{customerAccointID}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public int getCurrentQtyInCart(string num, string id) //part num, customer id
        {
            string cartID = getCartID(id);
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT itemID FROM product WHERE partNumber = \'{num}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string itemID = dt.Rows[0][0].ToString();

            dt = new DataTable();
            sqlCmd = $"SELECT quantity FROM product_in_cart WHERE itemID = \'{itemID}\' AND cartID = \'{cartID}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public Boolean
            addQtyBack(string num, int currentCartQty, int desiredQty,
                Boolean isLM) //part num //add qty back to db for product table and spare_part table
        {
            //get the qty in db first

            int qtyInProduct = getOnSaleQtyInDb(num, isLM);
            int qtyInSpare_Part = getSpareQtyInDb(num);

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
            int qtyInSpare_Part = getSpareQtyInDb(num);

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
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT itemID FROM product WHERE partNumber = \'{num}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string itemID = dt.Rows[0][0].ToString();
            sqlCmd = $"UPDATE product_in_cart SET quantity = @qty WHERE itemID = @num AND cartID = @cartID";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@qty", newQty);
                        command.Parameters.AddWithValue("@num", itemID);
                        command.Parameters.AddWithValue("@cartID", cartID);

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

            return true;
        }

        public int getOnSaleQtyInDb(string num, Boolean isLM) //part num
        {
            DataTable dt = new DataTable();
            if (!isLM)
            {
                sqlCmd = $"SELECT onSaleQty FROM product WHERE partNumber = \'{num}\'";
            }
            else
            {
                sqlCmd = $"SELECT LM_onSaleQty FROM product WHERE partNumber = \'{num}\'";
            }

            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            int qtyInProduct = int.Parse(dt.Rows[0][0].ToString());
            return qtyInProduct;
        }

        public int getSpareQtyInDb(string num)
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT quantity FROM spare_part WHERE partNumber = \'{num}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            int qtyInSpare_Part = int.Parse(dt.Rows[0][0].ToString());
            return qtyInSpare_Part;
        }

        public Boolean createOrder(string id, string shippingDate) //customerID
        {
            //get customer account id first
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT customerAccountID FROM customer_account WHERE customerID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string customerAccountID = dt.Rows[0][0].ToString();

            //generate an order id
            string orderID = orderIdGenerator();
            //generate order serial num
            string orderSerialNum = orderSerialNumGenerator();
            //assign an order processing clerk to this order
            string staffAccountID = orderProcessingClerkAssignment();
            //get today date
            string orderDate = DateTime.Now.ToString("yyyy-MM-dd");


            //create order
            sqlCmd =
                $"INSERT INTO order_ (orderID, customerAccountID, staffAccountID, orderSerialNumber,orderDate, status) VALUES(@orderID,@CAID,@SAID,@OSN,@date,@status)";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@orderID", orderID);
                        command.Parameters.AddWithValue("@CAID", customerAccountID);
                        command.Parameters.AddWithValue("@SAID", staffAccountID);
                        command.Parameters.AddWithValue("@OSN", orderSerialNum);
                        command.Parameters.AddWithValue("@date", orderDate);
                        command.Parameters.AddWithValue("@status", "Pending");

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

            //insert to order line, create invoice
            if (createOrderLineRow(id, orderID) && createInvoice(customerAccountID, orderID) &&
                createShippingDetail(orderID, shippingDate))
            {
                return true;
            }
            else
            {
                return false;
            }

            //create invoice
        }

        public Boolean createOrderLineRow(string cid, string id) //customer id, order id
        {
            //get item in cart
            DataTable dt = getCartItem(cid);
            //insert to db
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sqlCmd = $"INSERT INTO order_line VALUES(@partNum,@orderID,@qty,@price)";
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connString))
                    {
                        connection.Open();

                        using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                        {
                            command.Parameters.AddWithValue("@partNum", dt.Rows[i][5].ToString());
                            command.Parameters.AddWithValue("@orderID", id);
                            command.Parameters.AddWithValue("@qty", dt.Rows[i][2].ToString());
                            command.Parameters.AddWithValue("@price", dt.Rows[i][8].ToString());


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
            }

            return true;
        }

        public Boolean createInvoice(string caid, string id) //customer account id, order id
        {
            //count how many inovice in db first
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT COUNT(*) FROM invoice";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            int numOfInvoice = int.Parse(dt.Rows[0][0].ToString());

            numOfInvoice++; //invoice number of the order
            string invoiceNum = "IN";
            if (numOfInvoice <= 9)
            {
                invoiceNum += $"0000{numOfInvoice}";
            }
            else if (numOfInvoice <= 99)
            {
                invoiceNum += $"000{numOfInvoice}";
            }
            else if (numOfInvoice <= 999)
            {
                invoiceNum += $"00{numOfInvoice}";
            }
            else if (numOfInvoice <= 9999)
            {
                invoiceNum += $"0{numOfInvoice}";
            }
            else
            {
                invoiceNum += $"{numOfInvoice}";
            }

            sqlCmd =
                $"INSERT INTO invoice (customerAccountID, orderID, invoiceNumber) VALUES(@CAID,@orderID,@invoiceNum)";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@CAID", caid);
                        command.Parameters.AddWithValue("@orderID", id);
                        command.Parameters.AddWithValue("@invoiceNum", invoiceNum);

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

            return true;
        }

        public Boolean createShippingDetail(string id, string shippingDate)
        {
            string deliverman = delivermanAssignment();
            sqlCmd =
                $"INSERT INTO shipping_detail (orderID, delivermanID, shippingDate) VALUES(@orderID,@deliverman,@date)";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@orderID", id);
                        command.Parameters.AddWithValue("@deliverman", deliverman);
                        command.Parameters.AddWithValue("@date", shippingDate);

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

            return true;
        }

        public void clearCustomerCartAfterCreateOrder(string id) //customer id
        {
            string cartID = getCartID(id);
            sqlCmd = $"DELETE FROM product_in_cart WHERE cartID = @cartID";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@cartID", cartID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                conn.Close();
            }
        }


        public string orderIdGenerator()
        {
            string year = DateTime.Now.ToString("yy"); //today year 
            string month = DateTime.Now.ToString("MM"); //today month

            //see how many order in this year month
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT orderID FROM order_ WHERE orderID LIKE \'OD{year}{month}%\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            int orderInYearMonth = dt.Rows.Count;
            ++orderInYearMonth; //+1 for order id

            string generatedOrderID = $"OD{year}{month}";
            if (orderInYearMonth <= 9)
            {
                generatedOrderID += $"000{orderInYearMonth}";
            }
            else if (orderInYearMonth <= 99)
            {
                generatedOrderID += $"00{orderInYearMonth}";
            }
            else if (orderInYearMonth <= 999)
            {
                generatedOrderID += $"0{orderInYearMonth}";
            }
            else
            {
                generatedOrderID += $"{orderInYearMonth}";
            }

            return generatedOrderID;
        }

        public string orderSerialNumGenerator()
        {
            string year = DateTime.Now.ToString("yy"); //today year 
            string month = DateTime.Now.ToString("MM"); //today month

            //see how many order in this year month
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT orderSerialNumber FROM order_ WHERE orderSerialNumber LIKE \'SN{year}{month}%\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            int orderInYearMonth = dt.Rows.Count;
            ++orderInYearMonth; //+1 for order id

            string generatedOrderID = $"SN{year}{month}";
            if (orderInYearMonth <= 9)
            {
                generatedOrderID += $"000{orderInYearMonth}";
            }
            else if (orderInYearMonth <= 99)
            {
                generatedOrderID += $"00{orderInYearMonth}";
            }
            else if (orderInYearMonth <= 999)
            {
                generatedOrderID += $"0{orderInYearMonth}";
            }
            else
            {
                generatedOrderID += $"{orderInYearMonth}";
            }

            return generatedOrderID;
        }

        public string orderProcessingClerkAssignment()
        {
            //get num of opc first
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT staffID, jobTitle FROM staff WHERE jobTitle = 'order processing clerk'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            int numOfOpc = dt.Rows.Count;


            //random assignment
            Random random = new Random();
            int num = random.Next(0, numOfOpc);
            string staffIDAssigned = dt.Rows[num][0].ToString(); //db dont have enough data now
            //string staffIDAssigned = "LMS00002";

            //get staff account id
            dt = new DataTable();
            sqlCmd = $"SELECT staffAccountID FROM staff_account WHERE staffID = \'{staffIDAssigned}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public string delivermanAssignment()
        {
            //get num of opc first
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT delivermanID FROM deliverman";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            int numOfDeliverman = dt.Rows.Count;

            //random assignment
            Random random = new Random();
            int num = random.Next(0, numOfDeliverman);
            return dt.Rows[num][0].ToString();
        }

        public DataTable getCustomerAddress(string id) //customer id
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT warehouseAddress, province, city FROM customer WHERE customerID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }
    }

    class notEnoughException : Exception
    {
    }
}