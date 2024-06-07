using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; //must include in every controller file
using MySqlConnector; //must include in every controller file 


namespace controller
{
    public class viewSparePartController : abstractController
    {
        string sqlCmd;

        public viewSparePartController()
        {
            sqlCmd = "";
        }

        public DataTable getInfo(string num) //part num
        {
            DataTable dt = new DataTable();
            sqlCmd =
                $"SELECT * FROM spare_part x, product y, supplier z WHERE x.partNumber = y.partNumber AND x.partNumber =\'{num}\' AND x.supplierID = z.supplierID";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public Boolean isFavourite(string num, string id) //part num, customer id
        {
            favouriteController c = new favouriteController();
            return c.isFavourite(num, id);
        }

        public Boolean addToFavourite(string num, string id)
        {
            favouriteController c = new favouriteController();
            return c.addToFavourite(num, id);
        }

        public Boolean removeFavourite(string num, string id)
        {
            favouriteController c = new favouriteController();
            return c.removeFromFavourite(num, id);
        }

        public Boolean addToCart(string id, string num, int qty, Boolean isLM) //customer id,part num, quantity
        {
            //get cart id of the customer first
            DataTable dt = new DataTable();
            sqlCmd =
                $"SELECT cartID FROM cart x, customer_account y where x.customerAccountID = y.customerAccountID AND y.customerID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string cartID = dt.Rows[0][0].ToString();

            //get item id
            dt = new DataTable();
            sqlCmd =
                $"SELECT itemID from product x, spare_part y where x.partNumber = y.partNumber AND y.partNumber = \'{num}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string itemID = dt.Rows[0][0].ToString();

            //check in cart or not first
            dt = new DataTable();
            sqlCmd = $"SELECT * FROM `product_in_cart` WHERE itemID = \'{itemID}\' AND cartID = \'{cartID}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            Boolean isInCart = (dt.Rows.Count > 0);

            if (isInCart)
            {
                //get qty already in cart first
                dt = new DataTable();
                sqlCmd =
                    $"SELECT quantity FROM `product_in_cart` WHERE itemID = \'{itemID}\' AND cartID = \'{cartID}\'";
                adr = new MySqlDataAdapter(sqlCmd, conn);
                adr.Fill(dt);
                int qtyAlreadyInCart = int.Parse(dt.Rows[0][0].ToString());

                //new qty in cart
                int newQty = qty + qtyAlreadyInCart;

                //update cart value
                sqlCmd =
                    $"UPDATE product_in_cart SET quantity = @qty WHERE itemID = \'{itemID}\' AND cartID = \'{cartID}\'";

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connString))
                    {
                        connection.Open();
                        using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                        {
                            command.Parameters.AddWithValue("@cID", cartID);
                            command.Parameters.AddWithValue("@iID", itemID);
                            command.Parameters.AddWithValue("@qty", newQty);

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

                if (deductQtyInDB(itemID, num, qty,
                        isLM)) //deduct qty in db as stated in function requirement that when add item to cart, deduct qty in db
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //add to cart
                sqlCmd = "INSERT INTO product_in_cart VALUES (@cID, @iID, @qty,@date)";

                //try
                //{
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@cID", cartID);
                        command.Parameters.AddWithValue("@iID", itemID);
                        command.Parameters.AddWithValue("@qty", qty);
                        command.Parameters.AddWithValue("@date", DateTime.Now.ToString("dd/MM/yyyy"));

                        command.ExecuteNonQuery();
                    }
                }

                //}
                //catch (Exception ex)
                //{
                //    return false;
                //}
                //finally
                //{
                conn.Close();
                //}
                if (deductQtyInDB(itemID, num, qty,
                        isLM)) //deduct qty in db as stated in function requirement that when add item to cart, deduct qty in db
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Boolean deductQtyInDB(string itemID, string partNum, int qty, Boolean isLM)
        {
            //get qty in spare_part table first
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT quantity FROM spare_part WHERE partNumber = \'{partNum}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            int qtyInDB = int.Parse(dt.Rows[0][0].ToString());

            //new qty in spare_part
            qtyInDB -= qty;

            //update spare_part table
            //sqlCmd = $"UPDATE spare_part SET quantity = @qty WHERE partNumber = @partNum";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@qty", qtyInDB);
                        command.Parameters.AddWithValue("@partNum", partNum);

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

            //get qty in product table 
            dt = new DataTable();
            sqlCmd = !isLM ? $"SELECT OnSaleQty FROM product WHERE itemID = \'{itemID}\'" : $"SELECT LM_OnSaleQty FROM product WHERE itemID = \'{itemID}\'";

            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            qtyInDB = int.Parse(dt.Rows[0][0].ToString());

            //new qty in product
            qtyInDB -= qty;

            //update product table
            sqlCmd = !isLM ? $"UPDATE product SET OnSaleQty = @qty WHERE itemID = @id" : $"UPDATE product SET LM_OnSaleQty = @qty WHERE itemID = @id";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@qty", qtyInDB);
                        command.Parameters.AddWithValue("@id", itemID);

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
    }
}