using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace controller
{
    class controller
    {
        public controller()
        {
        }

        public DataTable testGetData()
        {
            DataTable dt = new DataTable();
            string sqlCmd = "SELECT * FROM department";
            return dt;
        }
    }
}