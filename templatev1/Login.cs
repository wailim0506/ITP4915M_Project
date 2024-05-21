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
    public partial class Login : Form
    {
        public Login()
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

            //For remember me function.
            if (String.IsNullOrEmpty(Properties.Settings.Default.Usesrname) == false)
            {
                chkRememberMe.CheckState = CheckState.Checked;
                tbUsername.Text = Properties.Settings.Default.Usesrname;
                tbPassword.Text = Properties.Settings.Default.Password;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Clean the content in the label.
            lblUsernameMsg.Text = "";
            lblPasswordMsg.Text = "";

            //username and password have not been entered.
            if (String.IsNullOrEmpty(tbPassword.Text) && String.IsNullOrEmpty(tbUsername.Text))
            {
                lblPasswordMsg.Text = "Please enter your password.";
                lblUsernameMsg.Text = "Please enter your password.";
            }
            //password have not been entered.
            else if (String.IsNullOrEmpty(tbPassword.Text))
            {
                lblPasswordMsg.Text = "Please enter your password.";
            }
            //username have not been entered.
            else if (String.IsNullOrEmpty(tbUsername.Text))
            {
                lblUsernameMsg.Text = "Invalid username or password.";
            }

            //For remember me function.
            if (chkRememberMe.Checked == true)
            {
                Properties.Settings.Default.Usesrname = tbUsername.Text;
                Properties.Settings.Default.Password = tbPassword.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Usesrname = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }
        }

        private void btnForgetPassword_Click(object sender, EventArgs e)
        {
            Form PasswordRecovery = new PasswordRe();
            this.Hide();
            //Swap the current form to another.
            PasswordRecovery.StartPosition = FormStartPosition.Manual;
            PasswordRecovery.Location = this.Location;
            PasswordRecovery.Size = this.Size;
            PasswordRecovery.ShowDialog();
            this.Close();
        }

        private void btnCreateCustomerAcc_Click(object sender, EventArgs e)
        {
            Form CreateCustoemrAcc = new CreateCustomerAcc();
            this.Hide();
            //Swap the current form to another.
            CreateCustoemrAcc.StartPosition = FormStartPosition.Manual;
            CreateCustoemrAcc.Location = this.Location;
            CreateCustoemrAcc.Size = this.Size;
            CreateCustoemrAcc.ShowDialog();
            this.Close();
        }
    }
}
