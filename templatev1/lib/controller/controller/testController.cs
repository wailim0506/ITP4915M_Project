//each function need one controller

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;      //must include in every controller file
using MySqlConnector;  //must include in every controller file  
                       //need to download 'MySqlConnector' in NuGet Package Manager first 

namespace controller
{
    public /*<--add here*/ class testController   //need to add 'public' here
    {
        //port = port Number of MySql in XAMPP (usually 3306)
        //database = the name of your database
        private static string connString = "server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;"; //just copy here, change the attribute stated above
        MySqlConnection conn = new MySqlConnection(connString); //just copy here
        MySqlDataAdapter adr; //just copy here

        public testController()  //constructor, useless
        {
        }

        public DataTable testGetData()  //method name can change
        {
            DataTable dt = new DataTable();  //just copy here
            string sqlCmd = "SELECT * FROM department"; //the sql query you want to run
            adr = new MySqlDataAdapter(sqlCmd, conn); //just copy here
            adr.Fill(dt); //just copy
            return dt;   //just copy
        }
    }
}
