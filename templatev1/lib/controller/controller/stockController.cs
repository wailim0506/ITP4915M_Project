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
        private string sqlStr;
        private DataTable dt;
        private Database _db;

        public stockController(AccountController accountController, Database db = null)
        {
            _db = db ?? new Database();
            this.accountController = accountController;
            this.db = db ?? new Database();
        }

        public DataTable GetPart()
        {
            sqlStr = "SELECT * FROM spare_part WHERE quantity <= dangerLevel " +
                     "UNION ALL SELECT * FROM spare_part WHERE quantity <= reorderLevel AND quantity > dangerLevel " + //Move the spare to the top which meets the danger level.
                     "UNION ALL SELECT * FROM spare_part WHERE quantity > dangerLevel AND quantity > reorderLevel"; //Move the spare to the top which meets the reorder level.
            return ExecuteSqlQuery(sqlStr);
        }

        public DataTable SearchPart(string PartID)
        {
            sqlStr = $"SELECT * FROM spare_part WHERE partNumber = \'{PartID}\'";
            return ExecuteSqlQuery(sqlStr);
        }

        public DataTable AdvancedSearch(dynamic partValues)
        {
            string condition;
            condition = "";

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

        public dynamic GetPartInfo(string PartID)
        {
            dt = new DataTable();

            sqlStr =
                $"SELECT SP.partNumber, SP.supplierID, SP.name AS SPname, SP.reorderLevel, SP.dangerLevel, SP.quantity, " +
                $"S.name AS Sname, S.phone, S.address, S.country, C.type FROM spare_part SP, supplier S, category C " +
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

            return StockInfo;
        }

        public List<string> GetCategory()
        {
            var query = "SELECT type FROM category";
            DataTable dataTable = db.ExecuteDataTable(query);
            Log.LogMessage(LogLevel.Debug, "Stock Controller", $"GetCategory was executed.");
            return dataTable.AsEnumerable().Select(row => row["type"].ToString()).ToList();
        }

        public List<string> GetCountry()
        {
            var query = "SELECT DISTINCT country FROM supplier";
            DataTable dataTable = db.ExecuteDataTable(query);
            Log.LogMessage(LogLevel.Debug, "Stock Controller", $"GetCountry was executed.");
            return dataTable.AsEnumerable().Select(row => row["country"].ToString()).ToList();
        }

        public List<string> GetSupplier()
        {
            var query = "SELECT name FROM supplier";
            DataTable dataTable = db.ExecuteDataTable(query);
            Log.LogMessage(LogLevel.Debug, "Stock Controller", $"GetSupplier was executed.");
            return dataTable.AsEnumerable().Select(row => row["name"].ToString()).ToList();
        }


        private DataTable ExecuteSqlQuery(string sqlQuery)
        {
            return db.ExecuteDataTable(sqlQuery);
        }
    }
}