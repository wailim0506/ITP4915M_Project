using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace controller
{

    public class favouriteController : abstractController
    {
        string sqlCmd;
        public favouriteController()
        {
            sqlCmd = "";
        }

        public DataTable getFavourite(string id) //customer id
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT * FROM favourite x, product y, spare_part z, category zz WHERE x.customerID = \'{id}\' AND x.itemID = y.itemID AND y.partNumber = z.partNumber AND z.categoryID = zz.categoryID";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public Boolean removeFromFavourite(string num, string id) //partNum , customerID
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT itemID FROM Product WHERE partNumber = \'{num}\' ";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            string itemID = dt.Rows[0][0].ToString();

            sqlCmd = "DELETE FROM favourite WHERE itemID = @id AND customerID = @cid";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        command.Parameters.AddWithValue("@id", itemID);
                        command.Parameters.AddWithValue("@cid", id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

            return true;
        }

        public DataTable getFavouriteWhenTextChange(string id,string category, string kw, string sorting)
        {
            sqlCmd = $"SELECT * FROM favourite x, product y, spare_part z, category zz WHERE x.customerID = \'{id}\' AND x.itemID = y.itemID AND y.partNumber = z.partNumber AND z.categoryID = zz.categoryID";
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

            DataTable dt = new DataTable();
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public Boolean isFavourite(string num)
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT * FROM favourite x, product y WHERE x.itemID = y.itemID AND partNumber = '{num}';";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            try
            {
                string x = dt.Rows[0][0].ToString();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}


