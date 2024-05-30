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
        controller.RecoveryController recoveryController;

        public CreateCustomerAcc()
        {
            InitializeComponent();
        }

        public CreateCustomerAcc(controller.RecoveryController recoveryController)
        {
            InitializeComponent();
            this.recoveryController = recoveryController;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            lblUID.Text = "LMC" + recoveryController.getLMCID().ToString("D4");
            lblCrateDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

    }
}
