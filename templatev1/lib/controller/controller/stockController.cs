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
    public class stockController : abstractController
    {
        AccountController accountController;
        private readonly Database db;
        private string sqlStr, ToModifySpareID;
        private DataTable dt;
        private Database _db;

        public stockController(AccountController accountController, Database db = null)
        {
            _db = db ?? new Database();
            this.accountController = accountController;
            this.db = db ?? new Database();
        }

        //Return all records to the data grid view from database.
        public DataTable GetPart()
        {
            sqlStr = "SELECT partNumber, supplierID, categoryID, name, reorderLevel, dangerLevel, quantity, status " +
                     "FROM spare_part WHERE quantity <= dangerLevel " +
                     "UNION ALL SELECT partNumber, supplierID, categoryID, name, reorderLevel, dangerLevel, quantity, status " +
                     "FROM spare_part WHERE quantity <= reorderLevel AND quantity > dangerLevel " + //Move the spare to the top which meets the danger level.
                     "UNION ALL SELECT partNumber, supplierID, categoryID, name, reorderLevel, dangerLevel, quantity, status " +
                     "FROM spare_part WHERE quantity > dangerLevel AND quantity > reorderLevel"; //Move the spare to the top which meets the reorder level.
            return ExecuteSqlQuery(sqlStr);
        }

        //Search function.
        public DataTable SearchPart(string PartID)
        {
            sqlStr = $"SELECT * FROM spare_part WHERE partNumber = \'{PartID}\'";
            return ExecuteSqlQuery(sqlStr);
        }

        public DataTable AdvancedSearch(dynamic partValues)
        {
            string condition;
            condition = "";

            //Set the condition of advenced search.
            condition += string.IsNullOrEmpty(partValues.partName)
                ? ""
                : $" AND SP.name LIKE '%{partValues.partName}%' ";
            condition += string.IsNullOrEmpty(partValues.supplier) ? "" : $" AND S.name = \'{partValues.supplier}\' ";
            condition += string.IsNullOrEmpty(partValues.category) ? "" : $" AND C.type = \'{partValues.category}\' ";
            condition += string.IsNullOrEmpty(partValues.country) ? "" : $" AND S.country = \'{partValues.country}\' ";

            sqlStr =
                $"SELECT SP.partNumber, SP.supplierID, SP.categoryID, SP.name, SP.reorderLevel, SP.dangerLevel, SP.quantity " +
                $"FROM spare_part SP, supplier S, category C " +
                $"WHERE SP.supplierID = S.supplierID AND SP.categoryID = C.categoryID " +
                $"{condition}";
            return ExecuteSqlQuery(sqlStr);
        }


        public bool CheckOutOfStock(string PartID)
        {
            dt = new DataTable();

            sqlStr = $"SELECT * FROM spare_part WHERE quantity = 0 AND partNumber = \'{PartID}\'";
            dt = _db.ExecuteDataTable(sqlStr);

            return dt.Rows.Count == 1;
        }

        public bool CheckReorderLevel(string PartID)
        {
            dt = new DataTable();

            sqlStr = $"SELECT * FROM spare_part WHERE quantity < reorderLevel AND partNumber = \'{PartID}\'";
            dt = _db.ExecuteDataTable(sqlStr);

            return dt.Rows.Count == 1;
        }

        //Return the detail of the selected spare part.
        public dynamic GetPartInfo(string PartID)
        {
            dt = new DataTable();

            sqlStr =
                $"SELECT SP.status, SP.partNumber, SP.supplierID, SP.name AS SPname, SP.reorderLevel, SP.dangerLevel, SP.quantity, " +
                $"SP.lastModified, S.name AS Sname, S.phone, S.address, S.country, C.type FROM spare_part SP, supplier S, category C " +
                $"WHERE SP.partNumber = \'{PartID}\' AND SP.supplierID = S.supplierID AND SP.categoryID = C.categoryID";

            dt = _db.ExecuteDataTable(sqlStr);

            dynamic StockInfo = new ExpandoObject();
            StockInfo.partNumber = dt.Rows[0]["partNumber"].ToString();
            StockInfo.supplierID = dt.Rows[0]["supplierID"].ToString();
            StockInfo.SPname = dt.Rows[0]["SPname"].ToString();
            StockInfo.reorderLevel = dt.Rows[0]["reorderLevel"].ToString();
            StockInfo.dangerLevel = dt.Rows[0]["dangerLevel"].ToString();
            StockInfo.quantity = dt.Rows[0]["quantity"].ToString();
            StockInfo.Sname = dt.Rows[0]["Sname"].ToString();
            StockInfo.phone = dt.Rows[0]["phone"].ToString();
            StockInfo.address = dt.Rows[0]["address"].ToString();
            StockInfo.country = dt.Rows[0]["country"].ToString();
            StockInfo.type = dt.Rows[0]["type"].ToString();
            StockInfo.status = dt.Rows[0]["status"].ToString();
            StockInfo.lastModified = dt.Rows[0]["lastModified"].ToString();

            return StockInfo;
        }

        private dynamic GetOnSaleInfo(string PartID)
        {
            dt = new DataTable();

            sqlStr = $"SELECT * FROM product WHERE partNumber = \'{PartID}\'";

            dt = _db.ExecuteDataTable(sqlStr);

            dynamic OnSaleInfo = new ExpandoObject();
            OnSaleInfo.itemID = dt.Rows[0]["itemID"].ToString();
            OnSaleInfo.onSaleQty = dt.Rows[0]["onSaleQty"].ToString();
            OnSaleInfo.LM_onSaleQty = dt.Rows[0]["LM_onSaleQty"].ToString();
            OnSaleInfo.description = dt.Rows[0]["description"].ToString();
            OnSaleInfo.price = dt.Rows[0]["price"].ToString();
            OnSaleInfo.status = dt.Rows[0]["status"].ToString();

            return OnSaleInfo;
        }


        public List<string> GetCategory()
        {
            var sqlStr = "SELECT type FROM category";
            dt = db.ExecuteDataTable(sqlStr);
            Log.LogMessage(LogLevel.Debug, "Stock Controller", $"GetCategory was executed.");
            return dt.AsEnumerable().Select(row => row["type"].ToString()).ToList();
        }

        public List<string> GetCountry()
        {
            var sqlStr = "SELECT country FROM countries";
            dt = db.ExecuteDataTable(sqlStr);
            Log.LogMessage(LogLevel.Debug, "Stock Controller", $"GetCountry was executed.");
            return dt.AsEnumerable().Select(row => row["country"].ToString()).ToList();
        }

        public List<string> GetSupplier()
        {
            var sqlStr = "SELECT name FROM supplier";
            dt = db.ExecuteDataTable(sqlStr);
            Log.LogMessage(LogLevel.Debug, "Stock Controller", $"GetSupplier was executed.");
            return dt.AsEnumerable().Select(row => row["name"].ToString()).ToList();
        }


        public DataTable GetReorder()
        {
            sqlStr = "SELECT * FROM reorder_request ORDER BY reorderID DESC";
            return ExecuteSqlQuery(sqlStr);
        }

        private string GetReorderID()
        {
            string reorderID = "RE";

            sqlStr = "SELECT * FROM reorder_request";
            dt = db.ExecuteDataTable(sqlStr);
            reorderID += (dt.Rows.Count + 1).ToString("D5");

            return reorderID;
        }

        public void CreateReorderRequest(string PartID, int reorderQty)
        {
            var reorderParams = new Dictionary<string, object>
            {
                { "@ReorderID", GetReorderID() },
                { "@UID", accountController.GetUid() },
                { "@PartID", PartID },
                { "@Date", DateTime.Now.ToString("yyyy/MM/dd") },
                { "@Qty", reorderQty },
                { "@Status", "processing" }
            };

            sqlStr = "INSERT INTO reorder_request VALUES(@ReorderID, @PartID, @UID, @Date, @Qty, @Status)";
            _db.ExecuteNonQueryCommand(sqlStr, reorderParams);
        }

        public void CancelReorder(string reorderID)
        {
            sqlStr = $"UPDATE reorder_request SET status = 'cancelled' WHERE reorderID = \'{reorderID}\'";
            _db.ExecuteNonQueryCommand(sqlStr, null);
        }

        public void AcceptReorderANDRestock(string PartID, string reorderQty, string reorderID)
        {
            sqlStr = $"UPDATE spare_part SET quantity = quantity + \'{reorderQty}\' WHERE partNumber = \'{PartID}\'";
            _db.ExecuteNonQueryCommand(sqlStr, null);
            sqlStr = $"UPDATE reorder_request SET status = 'finished' WHERE reorderID = \'{reorderID}\'";
            _db.ExecuteNonQueryCommand(sqlStr, null);
        }

        public void SetModifyPartID(string PartID)
        {
            this.ToModifySpareID = PartID;
        }

        public dynamic GetModifyPartInfo()
        {
            return GetPartInfo(ToModifySpareID);
        }

        public bool ModifyStockInfo(dynamic StockInfo)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@Name", StockInfo.SPname },
                    { "@RLevel", StockInfo.reorderLevel },
                    { "@DLevel", StockInfo.dangerLevel },
                    { "@Supplier", GetSupplierID(StockInfo.Sname) },
                    { "@Cat", GetCategoryID(StockInfo.category) },
                    { "@Qty", StockInfo.quantity },
                    { "@Status", StockInfo.status },
                    { "@partNumber", ToModifySpareID },
                    { "@UID", accountController.GetUid() }
                };

                sqlStr =
                    "UPDATE spare_part SET supplierID = @Supplier, categoryID = @Cat, name = @Name, reorderLevel = @RLevel, dangerLevel = @DLevel, quantity = @Qty, status = @Status, lastModified = @UID WHERE partNumber = @partNumber";

                _db.ExecuteNonQueryCommand(sqlStr, parameters);

                return true;
            }
            catch (Exception e)
            {
                Log.LogMessage(LogLevel.Error, "stock controller", $"Error modifying stock info: {e.Message}");
                return false; //Something went wrong.
            }
        }

        private string GetSupplierID(string suppilerName)
        {
            dt = new DataTable();

            sqlStr = $"SELECT supplierID FROM supplier WHERE name = \'{suppilerName}\'";

            dt = _db.ExecuteDataTable(sqlStr);

            return dt.Rows[0]["supplierID"].ToString();
        }

        private string GetCategoryID(string Category)
        {
            dt = new DataTable();

            sqlStr = $"SELECT categoryID FROM category WHERE type = \'{Category}\'";

            dt = _db.ExecuteDataTable(sqlStr);

            return dt.Rows[0]["categoryID"].ToString();
        }

        public dynamic GetOnSalePartInfo()
        {
            return GetOnSaleInfo(ToModifySpareID);
        }


        private DataTable ExecuteSqlQuery(string sqlQuery)
        {
            return db.ExecuteDataTable(sqlQuery);
        }
    }
}