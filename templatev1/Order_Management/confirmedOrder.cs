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
    public partial class confirmedOrder : Form
    {
        public confirmedOrder()
        {
            InitializeComponent();
        }

        private void confirmedOrder_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
