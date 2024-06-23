using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Data;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using controller.Utilities;
using System.Linq;

namespace controller
{
    public class OnSaleProductController : abstractController
    {
        AccountController accountController;
        private readonly Database db;
        private string sqlStr, ToModifyItemID;
        private DataTable dt;
        private Database _db;

        public OnSaleProductController(AccountController accountController, Database db = null)
        {
            _db = db ?? new Database();
            this.accountController = accountController;
            this.db = db ?? new Database();
        }
        private DataTable ExecuteSqlQuery(string sqlQuery)
        {
            return db.ExecuteDataTable(sqlQuery);
        }

        //Return all records to the data grid view from database.
        public DataTable GetProduct()
        {
            sqlStr = "SELECT itemID, type, onSaleQty, LM_onSaleQty, price, status, onShelvesDate " +
                "FROM product P, category C WHERE P.category = C.categoryID";
            return ExecuteSqlQuery(sqlStr);
        }

        public int GetTotalProductQty()
        {
            dt = new DataTable();

            sqlStr = $"SELECT * FROM product";

            dt = _db.ExecuteDataTable(sqlStr);

            return dt.Rows.Count;
        }


        //Return the detail of the selected product.
        public dynamic GetProductInfo(string itemID)
        {
            dt = new DataTable();

            sqlStr = $"SELECT P.itemID, P.partNumber, P.onSaleQty, P.LM_onSaleQty, P.price, P.description, " +
                $"P.lastModified, P.status, P.onShelvesDate, C.type, SP.name, SP.quantity, SUPP.name AS suppName, SUPP.supplierID " +
                $"FROM product P, category C, spare_part SP, supplier SUPP " +
                $"WHERE P.category = C.categoryID AND P.partNumber = SP.partNumber " +
                $"AND SP.supplierID = SUPP.supplierID AND itemID = \'{itemID}\'";

            dt = _db.ExecuteDataTable(sqlStr);

            dynamic ProductInfo = new ExpandoObject();
            ProductInfo.itemID = dt.Rows[0]["itemID"].ToString();
            ProductInfo.partNumber = dt.Rows[0]["partNumber"].ToString();
            ProductInfo.onSaleQty = dt.Rows[0]["onSaleQty"].ToString();
            ProductInfo.LM_onSaleQty = dt.Rows[0]["LM_onSaleQty"].ToString();
            ProductInfo.price = dt.Rows[0]["price"].ToString();
            ProductInfo.description = dt.Rows[0]["description"].ToString();
            ProductInfo.lastModified = dt.Rows[0]["lastModified"].ToString();
            ProductInfo.status = dt.Rows[0]["status"].ToString();
            ProductInfo.onShelvesDate = (DateTime)dt.Rows[0]["onShelvesDate"];
            ProductInfo.name = dt.Rows[0]["name"].ToString();
            ProductInfo.suppName = dt.Rows[0]["suppName"].ToString();
            ProductInfo.supplierID = dt.Rows[0]["supplierID"].ToString();
            ProductInfo.type = dt.Rows[0]["type"].ToString();
            ProductInfo.quantity = dt.Rows[0]["quantity"].ToString();

            return ProductInfo;
        }

        public DataTable SearchProduct(string ItemID)
        {
            sqlStr = $"SELECT itemID, type, onSaleQty, LM_onSaleQty, price, status, onShelvesDate " +
                $"FROM product P, category C WHERE P.category = C.categoryID AND itemID = \'{ItemID}\'";
            return ExecuteSqlQuery(sqlStr);
        }

        public void RemoveFromShelve(string ItemID)
        {
            sqlStr = $"UPDATE product SET status = 'Disable', lastModified = \'{accountController.GetUid()}\' WHERE itemID = \'{ItemID}\'";

            _db.ExecuteNonQueryCommand(sqlStr, null);
        }

        public void SetToModityItemID(string ItemID)
        {
            ToModifyItemID = ItemID;
        }

        public dynamic GetModifyItemInfo()
        {
            return GetProductInfo(ToModifyItemID);
        }

        public bool ModifyItemInfo(dynamic ItemInfo) 
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@itemID", ToModifyItemID },
                    { "@onSaleQty", ItemInfo.onSaleQty },
                    { "@LM_onSaleQty", ItemInfo.LM_onSaleQty },
                    { "@description", ItemInfo.description },
                    { "@status", ItemInfo.status },
                    { "@price", accountController.GetUid() }
                };

                sqlStr =
                    "UPDATE product SET onSaleQty = @onSaleQty, LM_onSaleQty = @LM_onSaleQty" +
                    ", description = @description, status = @status WHERE itemID = @itemID";

                _db.ExecuteNonQueryCommand(sqlStr, parameters);

                return true;
            }
            catch (Exception e)
            {
                Log.LogMessage(LogLevel.Error, "OnSaleProduct Controller", $"Error modifying item info: {e.Message}");
                return false; //Something went wrong.
            }


        }

    }
}
