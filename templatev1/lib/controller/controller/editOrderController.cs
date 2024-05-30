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
            return c.getOrderedSparePart(orderID);
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

        public Boolean editQuantity(string id, string num, string qty)  //id = order id, num = part num
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
    }
}
