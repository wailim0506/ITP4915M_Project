using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Windows.Forms;
using System.Data;
using MySqlConnector;

namespace controller
{
    public class stockController : abstractController
    {
        controller.accountController accountController;

        public stockController()
        {

        }

        public stockController(controller.accountController accountController)
        {
            this.accountController = accountController;
        }


    }
}
