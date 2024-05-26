using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;      //must include in every controller file
using MySqlConnector;

namespace controller
{
    public abstract class abstractController    //abstract class for controller
    {
        protected static string connString = "server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;"; //just copy here, change the attribute stated above
        protected MySqlConnection conn = new MySqlConnection(connString); //just copy here
        protected MySqlDataAdapter adr; //just copy here
    }
}
