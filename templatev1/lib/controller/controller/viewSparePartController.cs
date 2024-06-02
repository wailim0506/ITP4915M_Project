using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;      //must include in every controller file
using MySqlConnector;  //must include in every controller file 


 namespace controller
{
    public class viewSparePartController : abstractController
    {
        string sqlCmd;
        public viewSparePartController()
        {
            sqlCmd = "";
        }

        public DataTable getInfo(string num)  //part num
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT * FROM spare_part x, product y, supplier z WHERE x.partNumber = y.partNumber AND x.partNumber =\'{num}\' AND x.supplierID = z.supplierID";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public Boolean isFavourite(string num ,string id)//part num, customer id
        {
            favouriteController c = new favouriteController();
            return c.isFavourite(num, id);
        }

        public Boolean addToFavourite(string num, string id)
        {
            favouriteController c = new favouriteController();
            return c.addToFavourite(num, id);

        }

        public Boolean removeFavourite(string num, string id)
        {
            favouriteController c = new favouriteController();
            return c.removeFromFavourite(num, id);

        }
    }
}
