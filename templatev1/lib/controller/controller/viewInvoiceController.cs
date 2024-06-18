using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Extensions.DependencyInjection;

namespace controller
{
    public class viewInvoiceController : abstractController
    {
        private Database _db;

        public viewInvoiceController()
        {
            _db = ServiceProvider.GetRequiredService<Database>();
        }

        public string GetOrderDate(string id) //order id
        {
            return _db.ExecuteDataTable($"SELECT orderDate FROM order_ WHERE orderID = '{id}'").Rows[0][0]
                .ToString();
        }

        public string GetCustomerId(string id)
        {
            return _db.ExecuteDataTable(
                    $"SELECT customerID FROM customer_account WHERE customerAccountID = '{_db.ExecuteDataTable($"SELECT customerAccountID FROM order_ WHERE orderID = '{id}'").Rows[0][0]}'")
                .Rows[0][0].ToString();
        }

        public string GetInvoiceNum(string id)
        {
            return _db.ExecuteDataTable($"SELECT invoiceNumber FROM invoice WHERE orderID = '{id}'")
                .Rows[0][0]
                .ToString();
        }

        public string GetCustomerAddress(string id)
        {
            return string.Join(", ",
                _db.ExecuteDataTable(
                        $"SELECT companyAddress, province, city FROM customer WHERE customerID = '{GetCustomerId(id)}'")
                    .Rows[0].ItemArray);
        }

        public string GetWarehouseAddress(string id)
        {
            return string.Join(", ",
                _db.ExecuteDataTable(
                        $"SELECT warehouseAddress, province, city FROM customer WHERE customerID = '{GetCustomerId(id)}'")
                    .Rows[0].ItemArray);
        }

        public string[] GetOrderedSparePartNumber(string id)
        {
            return _db.ExecuteDataTable($"SELECT partNumber FROM order_line WHERE orderID = '{id}'")
                .AsEnumerable()
                .Select(row => row[0].ToString()).ToArray();
        }

        public string GetPartName(string num) //part num
        {
            return _db.ExecuteDataTable($"SELECT name FROM spare_part WHERE partNumber = '{num}'")
                .Rows[0][0]
                .ToString();
        }

        public int GetQty(string id, string num) //id = order id, num = part num
        {
            return int.Parse(
                _db.ExecuteDataTable(
                        $"SELECT quantity FROM order_line WHERE partNumber = '{num}' AND orderID = '{id}'")
                    .Rows[0][0].ToString());
        }

        public string GetDeliveryDate(string id)
        {
            return _db.ExecuteDataTable($"SELECT shippingDate FROM shipping_detail WHERE orderID = '{id}'")
                .Rows[0][0]
                .ToString().Split(' ')[0];
        }

        public Boolean ConfirmInvoice(string num) //invoice num
        {
            try
            {
                _db.ExecuteNonQueryCommand("UPDATE invoice SET status = @status WHERE invoiceNumber = @num",
                    new Dictionary<string, object> { { "@status", "confirmed" }, { "@num", num } });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}