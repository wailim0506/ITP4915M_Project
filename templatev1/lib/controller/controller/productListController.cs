using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using controller.Utilities;


namespace controller
{
    public class productListController : abstractController
    {
        private readonly Database _db;

        public productListController(Database database = null)
        {
            _db = database ?? new Database();
        }

        public DataTable getProductList(string kwType,string kw, string category, string status, string sortBy)
        {
            string sqlCmd = $"SELECT x.*,y.quantity,y.name FROM product x, spare_part y WHERE x.partNumber = y.partNumber";
            var sortByOptions = new Dictionary<string, string>
            {
                { "ItemNo", "x.itemID" },
                { "ItemNoDESC", "x.itemID DESC" },
                { "PartNo", "x.partNumber" },
                { "PartNoDESC", "x.partNumber DESC" },
                { "NonLMQty", "x.onSaleQty" },
                { "NonLMQtyDESC", "x.onSaleQty DESC" },
                { "LMQty", "x.LM_onSaleQty" },
                { "LMQtyDESC", "x.LM_onSaleQty DESC" },
                { "StockQty", "y.quantity" },
                { "StockQtyDESC", "y.quantity DESC" },
                { "Price", "x.price" },
                { "PriceDESC", "x.price DESC"}
            };

            if (category != "All")
            {
                sqlCmd += $" AND x.category = \'{category}\'";
            }


            if (status != "All")
            {
                sqlCmd += $" AND x.status = \'{status}\'";
            }

            if (kw != "")
            {
                switch (kwType)
                {
                    case "Item No.":
                        sqlCmd += $" AND x.itemID LIKE \'%{kw}%\'";
                        break;
                    case "Part No.":
                        sqlCmd += $" AND x.partNumber LIKE \'%{kw}%\'";
                        break;
                    case "Name":
                        sqlCmd += $" AND y.name LIKE \'%{kw}%\'";
                        break;
                }
            }

            if (!sortByOptions.ContainsKey(sortBy)) return _db.ExecuteDataTable(sqlCmd, null);
            sqlCmd += $" ORDER BY {sortByOptions[sortBy]}";
            return _db.ExecuteDataTable(sqlCmd, null);

        }
    }
}