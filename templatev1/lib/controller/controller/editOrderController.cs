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
        public editOrderController() {
            sqlCmd = "";
            c = new viewOrderController(); ;
        }

        public DataTable getOrderedSparePart(string orderID)
        {
            return c.getOrderedSparePart(orderID,"None");
        }

        public string getItemNum(string id) //part Number
        {
            return c.getItemNum(id);
        }

        public string getPartName(string id) //part Number
        {
            return c.getPartName(id);
        }

        public DataTable getShippingDetail(string id) //orderID
        {
            return c.getShippingDetail(id);
        }

        public string[] getDelivermanDetail(string id) //orderID
        {
            return c.getDelivermanDetail(id);
        }

        public string getShippingAddress(string id) //customerID
        {
            return c.getShippingAddress(id);
        }

        public DataTable getOrder(string id) //orderID
        {
            return c.getOrder(id);
        }

        public string getStaffName(string id) //staff account id
        {
            return c.getStaffName(id);
        }

        public string getStafftID(string id) //staff account id
        {
            return c.getStafftID(id);
        }

        public string getStaffContact(string id)
        {
            return c.getStaffContact(id);
        }

        public int getPartQtyInOrder(string num, string id) //part num //order id
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT quantity FROM order_line WHERE partNumber = \'{num}\' and orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public Boolean addQtyBack(string num, int currentOrderQty, int desiredQty) //part num //add qty back to db for product table and spare_part table
        {
            cartController cc = new cartController();
            try
            {
                cc.addQtyBack(num, currentOrderQty, desiredQty);
                return true;
            }catch(notEnoughException e)
            {
                throw e;
            }
            
        }

        public Boolean editDbQty(string num, int desiredQty)
        {
            cartController cc = new cartController();
            if (cc.editDbQty(num, desiredQty))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Boolean editOrderLineQuantity(string id, string num, string qty)  //id = order id, num = part num, qty = new qty wanted
        {
            string sqlCmd = "UPDATE order_line SET quantity = @qty WHERE partNumber = @num AND orderID = @id";

            try
            {
                
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@qty", qty);
                        command.Parameters.AddWithValue("@num", num);
                        command.Parameters.AddWithValue("@id", id);

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

        public Boolean deleteSparePart(string id, string num) //id = order id, num = part num
        {
            string sqlCmd = "DELETE FROM order_line WHERE partNumber = @num AND orderID = @id";

            try
            {

                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@num", num);
                        command.Parameters.AddWithValue("@id", id);

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

        public void deleteOrder(string id) //order id
        {
            c.deleteOrder(id);
        }
    }
}
