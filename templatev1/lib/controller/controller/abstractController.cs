using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace controller
{
    public abstract class abstractController //abstract class for controller
    {
        protected static string connString =
            "server=localhost;port=8088;user id=root; password=password;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30"; // for my docker container , welcome to comment it out
        //"server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;";


        protected MySqlConnection conn = new MySqlConnection(connString);
        protected MySqlDataAdapter adr;
    }
}