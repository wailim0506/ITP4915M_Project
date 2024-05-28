using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1
{
    public partial class test_Database_and_Controller : Form
    {
        public test_Database_and_Controller()
        {
            InitializeComponent();
        }

        private void test_Database_and_Controller_Load(object sender, EventArgs e)
        {
            controller.testController dt = new controller.testController();      //testController is the name of the controller, different function have different controller
            //dataGridView1.DataSource = dt.login();     //test1() is the method inside the controller file
           // dataGridView2.DataSource = dt.test2();
            //dataGridView3.DataSource = dt.test3();
        }
    }
}
