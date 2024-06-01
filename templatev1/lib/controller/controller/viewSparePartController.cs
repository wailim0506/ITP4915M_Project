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

        public Boolean isFavourite(string num)
        {
            favouriteController c = new favouriteController();
            return c.isFavourite(num);
        }
    }
}
