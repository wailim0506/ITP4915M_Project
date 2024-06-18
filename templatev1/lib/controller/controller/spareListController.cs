using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace controller
{
    public class spareListController : abstractController
    {
        private Database db = new Database();

        private DataTable ExecuteQuery(string sqlCmd) => db.ExecuteDataTable(sqlCmd);

        public DataTable getAllPart() => ExecuteQuery("SELECT * FROM spare_part");

        public DataTable getSpareWhenTextChange(string category, string kw, string sorting)
        {
            string sqlCmd = "SELECT * FROM spare_part x, product y WHERE x.partNumber = y.partNumber";
            if (category != "All") sqlCmd += $" AND x.categoryID = '{category}'";
            if (kw != "") sqlCmd += $" AND x.name LIKE '%{kw}%'";
            switch (sorting)
            {
                case "Category":
                    sqlCmd += " ORDER BY x.categoryID, x.partNumber";
                    break;
                case "Price (Ascending)":
                    sqlCmd += " ORDER BY y.price";
                    break;
                case "Price (Descending)":
                    sqlCmd += " ORDER BY y.price DESC";
                    break;
                default:
                    sqlCmd += "";
                    break;
            }

            return ExecuteQuery(sqlCmd);
        }

        public string getCategoryName(string id) =>
            ExecuteQuery($"SELECT type FROM category WHERE categoryID = '{id}'").Rows[0][0].ToString();

        public string getPrice(string num) =>
            ExecuteQuery($"SELECT price FROM product WHERE partNumber = '{num}'").Rows[0][0].ToString();

        public int getOnSaleQty(string num, Boolean isLM)
        {
            string sqlCmd = isLM
                ? $"SELECT LM_onSaleQty FROM product WHERE partNumber = '{num}'"
                : $"SELECT onSaleQty FROM product WHERE partNumber = '{num}'";
            return int.Parse(ExecuteQuery(sqlCmd).Rows[0][0].ToString());
        }

        public Boolean addCart(string id, string num, int qty, Boolean isLM) =>
            new viewSparePartController().AddToCart(id, num, qty, isLM);

        public List<string> getAllPartName() => ExecuteQuery("SELECT name FROM spare_part").AsEnumerable()
            .Select(row => row[0].ToString()).ToList();
    }
}