using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace controller
{
    public class viewInvoiceController : abstractController
    {
        private Database _db;

        public viewInvoiceController(Database db = null)
        {
            _db = db ?? new Database();
        }

        public string GetOrderDate(string id) //order id
        {
            return _db.ExecuteDataTableAsync($"SELECT orderDate FROM order_ WHERE orderID = '{id}'").Result.Rows[0][0]
                .ToString();
        }

        public string GetCustomerId(string id)
        {
            return _db.ExecuteDataTableAsync(
                    $"SELECT customerID FROM customer_account WHERE customerAccountID = '{_db.ExecuteDataTableAsync($"SELECT customerAccountID FROM order_ WHERE orderID = '{id}'").Result.Rows[0][0]}'")
                .Result.Rows[0][0].ToString();
        }

        public string GetInvoiceNum(string id)
        {
            return _db.ExecuteDataTableAsync($"SELECT invoiceNumber FROM invoice WHERE orderID = '{id}'").Result
                .Rows[0][0]
                .ToString();
        }

        public string GetCustomerAddress(string id)
        {
            return string.Join(", ",
                _db.ExecuteDataTableAsync(
                        $"SELECT companyAddress, province, city FROM customer WHERE customerID = '{GetCustomerId(id)}'")
                    .Result.Rows[0].ItemArray);
        }

        public string GetWarehouseAddress(string id)
        {
            return string.Join(", ",
                _db.ExecuteDataTableAsync(
                        $"SELECT warehouseAddress, province, city FROM customer WHERE customerID = '{GetCustomerId(id)}'")
                    .Result
                    .Rows[0].ItemArray);
        }

        public string[] GetOrderedSparePartNumber(string id)
        {
            return _db.ExecuteDataTableAsync($"SELECT partNumber FROM order_line WHERE orderID = '{id}'").Result
                .AsEnumerable()
                .Select(row => row[0].ToString()).ToArray();
        }

        public string GetPartName(string num) //part num
        {
            return _db.ExecuteDataTableAsync($"SELECT name FROM spare_part WHERE partNumber = '{num}'").Result
                .Rows[0][0]
                .ToString();
        }

        public int GetQty(string id, string num) //id = order id, num = part num
        {
            return int.Parse(
                _db.ExecuteDataTableAsync($"SELECT quantity FROM order_line WHERE partNumber = '{num}' AND orderID = '{id}'").Result
                    .Rows[0][0].ToString());
        }

        public string GetDeliveryDate(string id)
        {
            return _db.ExecuteDataTableAsync($"SELECT shippingDate FROM shipping_detail WHERE orderID = '{id}'").Result.
                Rows[0][0]
                .ToString().Split(' ')[0];
        }

        public Boolean ConfirmInvoice(string num) //invoice num
        {
            try
            {
                _db.ExecuteNonQueryCommandAsync("UPDATE invoice SET status = @status WHERE invoiceNumber = @num",
                    new Dictionary<string, object> { { "@status", "confirmed" }, { "@num", num } }).Wait();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}