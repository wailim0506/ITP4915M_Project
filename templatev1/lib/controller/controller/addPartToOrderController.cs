using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace controller
{
    public class addPartToOrderController : abstractController
    {
        private readonly Database _db;

        public addPartToOrderController(Database database = null)
        {
            _db = ServiceProvider.GetRequiredService<Database>();
        }

        public DataTable GetPartDetail(string partNum)
        {
            string sqlCmd =
                $"SELECT w.name, w.partNumber, w.categoryID,x.type, y.name,y.country,z.price, z.onSaleQty FROM spare_part w, category x, supplier y, product z WHERE w.partNumber = z.partNumber AND w.categoryID = x.categoryID AND w.supplierID = y.supplierID AND w.partNumber = '{partNum}'";
            return _db.ExecuteDataTable(sqlCmd);
        }

        public DataTable GetEditableOrderId(string id) //customer id
        {
            string customerAccountID = GetCustomerAccountId(id);
            string sqlCmd =
                $"SELECT orderID from order_ WHERE customerAccountID = '{customerAccountID}' AND (status = 'Pending' OR status = 'Processing')";
            return _db.ExecuteDataTable(sqlCmd);
        }

        public string GetShippingDate(string id) //order id
        {
            string sqlCmd = $"SELECT shippingDate from shipping_detail WHERE orderID = '{id}'";
            return _db.ExecuteDataTable(sqlCmd).Rows[0][0].ToString();
        }

        public string GetOrderStatus(string id) //order id
        {
            string sqlCmd = $"SELECT status from order_ WHERE orderID = '{id}'";
            return _db.ExecuteDataTable(sqlCmd).Rows[0][0].ToString();
        }

        public DataTable GetShippingDetail(string id) //orderID
        {
            //orderID
            string sqlCmd = $"SELECT * FROM shipping_detail WHERE orderID = '{id}'";
            return _db.ExecuteDataTable(sqlCmd);
        }

        public string GetCustomerAccountId(string id) //id = customerID
        {
            string sqlCmd = $"SELECT customerAccountID FROM customer_account WHERE customerID = '{id}'";
            return _db.ExecuteDataTable(sqlCmd).Rows[0][0].ToString();
        }
    }
}