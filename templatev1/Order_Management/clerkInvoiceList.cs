using System;
using System.Windows.Forms;

namespace templatev1
{
    public partial class clerkInvoiceList : Form
    {
        public clerkInvoiceList()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void clerkInvoiceList_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}