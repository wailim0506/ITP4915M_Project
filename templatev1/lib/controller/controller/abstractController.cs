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
        protected static string connString = Database.GetConnectionString();


        protected MySqlConnection conn = new MySqlConnection(connString);
        protected MySqlDataAdapter adr;
    }
}