using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace controller
{
    public class addPartToOrderController : abstractController
    {
        string sqlCmd;

        public addPartToOrderController()
        {
            sqlCmd = "";
        }

        public DataTable getPartDetail(string partNum)
        {
            DataTable dt = new DataTable();
            sqlCmd =
                $"SELECT w.name, w.partNumber, w.categoryID,x.type, y.name,y.country,z.price, z.onSaleQty FROM spare_part w, category x, supplier y, product z WHERE w.partNumber = z.partNumber AND w.categoryID = x.categoryID AND w.supplierID = y.supplierID AND w.partNumber = \'{partNum}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public DataTable getEditableOrderID(string id) //customer id
        {
            string customerAccountID = getCustomerAccountID(id);
            DataTable dt = new DataTable();
            sqlCmd =
                $"SELECT orderID from order_ WHERE customerAccountID = \'{customerAccountID}\' AND (status = 'Pending' OR status = 'Processing')";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public string getShippingDate(string id) //order id
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT shippingDate from shipping_detail WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public string getOrderStatus(string id) //order id
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT status from order_ WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public DataTable getShippingDetail(string id) //orderID
        {
            //orderID
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT * FROM shipping_detail WHERE orderID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public string getCustomerAccountID(string id) //id = customerID
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT customerAccountID FROM customer_account WHERE customerID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }
    }
}