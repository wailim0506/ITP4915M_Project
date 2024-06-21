using System;
using System.Windows.Forms;
using controller.Utilities;

namespace templatev1
{
    public partial class staffEditOrder : Form
    {
        private dateHandler handler;

        public staffEditOrder()
        {
            InitializeComponent();
            handler = new dateHandler();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = handler.GetSystemDateTime();
        }

        private void clerkEditOrder_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form o = new Login();
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }
    }
}