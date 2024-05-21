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
    public partial class CreateCustomerAcc : Form
    {
        public CreateCustomerAcc()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Form Login = new Login();
            this.Hide();
            //Swap the current form to another.
            Login.StartPosition = FormStartPosition.Manual;
            Login.Location = this.Location;
            Login.Size = this.Size;
            Login.ShowDialog();
            this.Close();
        }
    }
}
