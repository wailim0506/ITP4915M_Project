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
        protected static string connString = Database.GetConnectionStringAsync().Result;
        protected MySqlConnection conn = new MySqlConnection(connString);
        protected MySqlDataAdapter adr;

        protected Database _db;
    }
}