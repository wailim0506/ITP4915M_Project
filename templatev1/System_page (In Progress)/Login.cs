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
        //For install date in about page.
        public static string sysInsDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

        private bool IsLogin;

        controller.accountController accountController;
        controller.UIController UIController;
        controller.RecoveryController recoveryController;

        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            accountController = new controller.accountController();
            UIController = new controller.UIController(accountController);

            IsLogin = false;
            timer1.Enabled = true;
            tbPassword.PasswordChar = '*';

            rememberMe();
        }

        //Login the system.
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Clean the error message in the label.
            lblUsernameMsg.Text = "";
            lblPasswordMsg.Text = "";

            if (string.IsNullOrEmpty(tbUsername.Text))            //username and password have not been entered.
            {
                lblUsernameMsg.Text = "Please enter your UserID.";
                tbUsername.Select();
            }
            else if (string.IsNullOrEmpty(tbPassword.Text))
            {
                lblPasswordMsg.Text = "Please enter your password.";
                tbPassword.Select();
            }
            else if (string.IsNullOrEmpty(tbPassword.Text))            //password have not been entered.
            {
                lblPasswordMsg.Text = "Please enter your password.";
                tbPassword.Select();
            }
            else if (accountController.login(tbUsername.Text, tbPassword.Text, UIController))        //Checking the password
            {
                IsLogin = true;
                rememberMe();
                accountController.setLog(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                //Back to login page
                Form Home = new Home(accountController, UIController);
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
                tbUsername.Select();
            }
        }

        //For remember me function.
        private void rememberMe()
        {
            if (IsLogin)
            {
                if (chkRememberMe.Checked == true)          //Store to local
                {
                    Properties.Settings.Default.Usesrname = tbUsername.Text;
                    Properties.Settings.Default.Password = tbPassword.Text;
                    Properties.Settings.Default.Save();
                }
                else
                    Properties.Settings.Default.Reset();        //Clean local data
            }
            else        //Read from local
            {
                if (!string.IsNullOrEmpty(Properties.Settings.Default.Usesrname))
                {
                    chkRememberMe.CheckState = CheckState.Checked;
                    tbUsername.Text = Properties.Settings.Default.Usesrname;
                    tbPassword.Text = Properties.Settings.Default.Password;
                    btnLogin.Select();
                }
            }
        }

        //Forget password.
        private void btnForgetPassword_Click(object sender, EventArgs e)
        {
            recoveryController = new controller.RecoveryController();

            Form PasswordRecovery = new PasswordRe(recoveryController);
            this.Hide();
            //Swap the current form to another.
            PasswordRecovery.StartPosition = FormStartPosition.Manual;
            PasswordRecovery.Location = this.Location;
            PasswordRecovery.Size = this.Size;
            PasswordRecovery.ShowDialog();
            this.Close();
        }

        //Create a customer account.
        private void btnCreateCustomerAcc_Click(object sender, EventArgs e)
        {
            recoveryController = new controller.RecoveryController();

            Form CreateCustoemrAcc = new CreateCustomerAcc(recoveryController);
            this.Hide();
            //Swap the current form to another.
            CreateCustoemrAcc.StartPosition = FormStartPosition.Manual;
            CreateCustoemrAcc.Location = this.Location;
            CreateCustoemrAcc.Size = this.Size;
            CreateCustoemrAcc.ShowDialog();
            this.Close();
        }


        //Development Tools
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

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, new EventArgs());
            }
        }

        //Set installed date for the about page.
        public static string getInsDate()
        {
            return sysInsDate;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }
    }
}
