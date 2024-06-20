using System;
using System.Windows.Forms;
using controller.Utilities;

namespace templatev1
{
    public partial class clerkEditOrder : Form
    {
        private dateHandler handler;

        public clerkEditOrder()
        {
            InitializeComponent();
            dateHandler handler = new dateHandler();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = handler.GetSystemDateTime();
        }

        private void clerkEditOrder_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}