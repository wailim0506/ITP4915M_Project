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
        public static readonly string sysInsDate = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        controller.accountController accController;
        

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
            accController = new controller.accountController();
            timer1.Enabled = true;
            tbPassword.PasswordChar = '*';

            //For remember me function.
            if (string.IsNullOrEmpty(Properties.Settings.Default.Usesrname) == false)
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

            if (string.IsNullOrEmpty(tbPassword.Text) && string.IsNullOrEmpty(tbUsername.Text))            //username and password have not been entered.
            {
                lblPasswordMsg.Text = "Please enter your password.";
                lblUsernameMsg.Text = "Please enter your password.";
            }
            else if (string.IsNullOrEmpty(tbPassword.Text))            //password have not been entered.
            {
                lblPasswordMsg.Text = "Please enter your password.";
            }
            else if (accController.login(tbUsername.Text, tbPassword.Text))        //Checking the password
            {
                Form Home = new Home();
                this.Hide();
                //Swap the current form to another.
                Home.StartPosition = FormStartPosition.Manual;
                Home.Location = this.Location;
                Home.Size = this.Size;
                Home.ShowDialog();
                this.Close();
            }
            else         //Password dose not match.
            {
                tbPassword.Clear();
                lblPasswordMsg.Text = "Invalid username or password.";
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
                Properties.Settings.Default.Reset();
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

        public static string getInsDate()
        {
            return sysInsDate;
        }





        //--For development Tools----------------------------------------------
        private void btnTest1_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMC00001";
            tbPassword.Text = "password123";
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00001";
            tbPassword.Text = "password123";
        }
        private void btnTest3_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00002";
            tbPassword.Text = "abc123456";
        }

        private void btnTest4_Click_1(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00003";
            tbPassword.Text = "xyz789!@#";
        }

        private void btnTest5_Click_1(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00004";
            tbPassword.Text = "qwer5678";
        }

        private void btnTest6_Click_1(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00005";
            tbPassword.Text = "asdf1234!";
        }
        //------------------------------------------------------------------
    }
}
