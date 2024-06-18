using System;
using System.Collections.Generic;
using System.Data;

namespace controller
{
    public class favouriteController : abstractController
    {
        string sqlCmd;
        private readonly Database _database;

        public favouriteController(Database database = null)
        {
            sqlCmd = "";
            _database = database ?? new Database();
        }

        public DataTable GetFavourite(string id) //customer id
        {
            sqlCmd =
                $"SELECT * FROM favourite x, product y, spare_part z, category zz " +
                $"WHERE x.customerID = \'{id}\' AND x.itemID = y.itemID AND y.partNumber = z.partNumber AND z.categoryID = zz.categoryID";
            DataTable dt = _database.ExecuteDataTable(sqlCmd);
            return dt;
        }

        public Boolean RemoveFromFavourite(string num, string id) //partNum , customerID
        {
            sqlCmd = $"SELECT itemID FROM product WHERE partNumber = \'{num}\' ";
            DataTable dt = _database.ExecuteDataTable(sqlCmd);
            string itemID = dt.Rows[0][0].ToString();

            sqlCmd = "DELETE FROM favourite WHERE itemID = @id AND customerID = @cid";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", itemID },
                { "@cid", id }
            };

            try
            {
                _database.ExecuteNonQueryCommand(sqlCmd, parameters);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public DataTable GetFavouriteWhenTextChange(string id, string category, string kw, string sorting)
        {
            sqlCmd =
                $"SELECT * FROM favourite x, product y, spare_part z, category zz " +
                $"WHERE x.customerID = \'{id}\' AND x.itemID = y.itemID AND y.partNumber = z.partNumber AND z.categoryID = zz.categoryID";
            if (category != "All")
            {
                sqlCmd += $" AND z.categoryID = '{category}'";
            }

            if (kw != "")
            {
                sqlCmd += $" AND z.name LIKE '%{kw}%'";
            }

            switch (sorting)
            {
                case "Category":
                    sqlCmd += " ORDER BY z.categoryID, z.partNumber";
                    break;
                case "Price (Ascending)":
                    sqlCmd += " ORDER BY y.price";
                    break;
                case "Price (Descending)":
                    sqlCmd += " ORDER BY y.price DESC";
                    break;
            }

            DataTable dt = _database.ExecuteDataTable(sqlCmd);
            return dt;
        }

        public Boolean IsFavourite(string num, string id) //part num, customer id
        {
            sqlCmd =
                $"SELECT * FROM favourite x, product y WHERE x.itemID = y.itemID AND partNumber = '{num}' AND x.customerID = \'{id}\';";
            DataTable dt = _database.ExecuteDataTable(sqlCmd);
            if (dt.Rows.Count != 0)
            {
                return true;
            }

            return false;
        }

        public Boolean AddToFavourite(string num, string id) //partNum , customerID
        {
            sqlCmd = $"SELECT itemID FROM product WHERE partNumber = \'{num}\' ";
            DataTable dt = _database.ExecuteDataTable(sqlCmd);
            string itemID = dt.Rows[0][0].ToString();

            sqlCmd = "INSERT INTO favourite (customerID,itemID) VALUES (@cid,@id);";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@id", itemID },
                { "@cid", id }
            };

            try
            {
                _database.ExecuteNonQueryCommand(sqlCmd, parameters);
            }
            catch (Exception e)
            {
                Log.LogException(e, "favouriteController");
                return false;
            }

            return true;
        }
    }
}