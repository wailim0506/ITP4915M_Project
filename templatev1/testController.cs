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
    public partial class testController : Form
    {
        public testController()
        {
            InitializeComponent();
        }

        private void testContorller_Load(object sender, EventArgs e)
        {
            controller.testController dt = new controller.testController();
            dataGridView1.DataSource = dt.testGetData();
        }

        private void testController_Load(object sender, EventArgs e)
        {
            controller.testController dt = new controller.testController();
            dataGridView1.DataSource = dt.testGetData();
        }
    }
}
