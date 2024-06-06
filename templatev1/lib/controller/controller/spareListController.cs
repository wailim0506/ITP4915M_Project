using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace controller
{
    public class spareListController : abstractController
    {
        public string sqlCmd;

        public spareListController()
        {
            sqlCmd = "";
        }

        public DataTable getAllPart()
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT * FROM spare_part";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public DataTable getSpareWhenTextChange(string category, string kw, string sorting) {
            sqlCmd = "SELECT * FROM spare_part x, product y WHERE x.partNumber = y.partNumber";
            if (category != "All")
            {
                sqlCmd += $" AND x.categoryID = '{category}'";
            }

            if (kw != "")
            {
                sqlCmd += $" AND x.name LIKE '%{kw}%'";
            }

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
            }

            DataTable dt = new DataTable();
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public string getCategoryName(string id) //category id
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT type FROM category WHERE categoryID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public string getPrice(string num) //part num
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT price FROM product WHERE partNumber = \'{num}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public int getOnSaleQty(string num, Boolean isLM)
        {
            DataTable dt = new DataTable();
            if (!isLM)
            {
                sqlCmd = $"SELECT onSaleQty FROM product WHERE partNumber = \'{num}\'";

            }
            else
            {
                sqlCmd = $"SELECT LM_onSaleQty FROM product WHERE partNumber = \'{num}\'";

            }
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public Boolean addCart(string id, string num, int qty, Boolean isLM) //customer id, part num, quantity
        {
           viewSparePartController c = new viewSparePartController();
           return  c.addToCart(id, num, qty,isLM);
        }

        public List<string> getAllPartName()
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT name FROM spare_part";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);

            List<string> partName = new List<string>();
            for (int i = 0; i < partName.Count; i++)
            {
                partName.Add(dt.Rows[i][0].ToString());
            }

            return partName;
        }
    }
}
