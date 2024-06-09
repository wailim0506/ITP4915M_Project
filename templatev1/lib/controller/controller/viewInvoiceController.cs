using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace controller
{
    public class viewInvoiceController : abstractController
    {
        string sqlCmd;

        public viewInvoiceController()
        {
            sqlCmd = "";
        }

        public string GetOrderDate(string id) //order id
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT orderDate FROM order_ WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public string getCustomerID(string id)
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT customerAccountID FROM order_ WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);

            DataTable dr = new DataTable();
            sqlCmd =
                $"SELECT customerID FROM customer_account WHERE customerAccountID = \'{dt.Rows[0][0].ToString()}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dr);
            return dr.Rows[0][0].ToString();
        }

        public string getInvoiceNum(string id)
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT invoiceNumber FROM invoice WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public string getCustomerAddress(string id)
        {
            string customerID = getCustomerID(id);
            DataTable dr = new DataTable();
            sqlCmd = $"SELECT companyAddress, province, city FROM customer WHERE customerID = \'{customerID}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dr);
            return $"{dr.Rows[0][0]}, {dr.Rows[0][1]}, {dr.Rows[0][2]}";
        }

        public string getWarehouseAddress(string id)
        {
            string customerID = getCustomerID(id);
            DataTable dr = new DataTable();
            sqlCmd = $"SELECT warehouseAddress, province, city FROM customer WHERE customerID = \'{customerID}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dr);
            return $"{dr.Rows[0][0]}, {dr.Rows[0][1]}, {dr.Rows[0][2]}";
        }

        public string[] getOrderedSparePartNumber(string id)
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT partNumber FROM order_line WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string[] partNum = new string[dt.Rows.Count];
            for (int i = 0; i < partNum.Length; i++)
            {
                partNum[i] = dt.Rows[i][0].ToString();
            }

            return partNum;
        }

        public string getPartName(string num) //part num
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT name FROM spare_part WHERE partNumber = \'{num}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public int getQty(string id, string num) //id = order id, num = part num
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT quantity FROM order_line WHERE partNumber = \'{num}\' AND orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public string getDeliveryDate(string id)
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT shippingDate FROM shipping_detail WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string[] d = dt.Rows[0][0].ToString().Split(' ');
            return d[0];
        }

        public Boolean confirmInvoice(string num) //invoice num
        {
            string sqlCmd = "UPDATE invoice SET status = @status WHERE invoiceNumber = @num";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@status", "confirmed");
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

            return true;
        }
    }
}