using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace controller
{
    class controller
    {
        private static string connString = "server=localhost;port=3306;user id=root; password=;database=classicmodels;charset=utf8;";
        MySqlConnection conn = new MySqlConnection(connString);
        MySqlDataAdapter adr;

        public controller()
        {

        }

        public DataTable testGetData()
        {
            DataTable dt = new DataTable();
            string sqlCmd = "SELECT * FROM department";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }
    }
}
