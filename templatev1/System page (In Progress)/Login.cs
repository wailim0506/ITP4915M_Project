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
        public static string UID;      //The user ID.
        public static string uName;      //The users name.
        public static bool showbtn1, showbtn2, showbtn3, showbtn4, showbtn5;      //whether the button is visible.
        public static string funbtn1, funbtn2, funbtn3, funbtn4, funbtn5;        //Text in the button.
        public static readonly string sysInsDate = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");

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
            UID = uName = funbtn1 = funbtn2 = funbtn3 = funbtn4 = funbtn5 = "";
            showbtn1 = showbtn2 = showbtn3 = showbtn4 = showbtn5 = false;

            Properties.Settings.Default.textColor = Color.Black;
            Properties.Settings.Default.bgColor = SystemColors.Control;
            Properties.Settings.Default.navBarColor = Color.FromArgb(59, 213, 184);
            Properties.Settings.Default.navColor = SystemColors.GradientActiveCaption;
            Properties.Settings.Default.timeColor = Color.FromArgb(204, 204, 204);
            Properties.Settings.Default.locTbColor = SystemColors.ControlLight;
            Properties.Settings.Default.logoutColor = Color.FromArgb(255, 192, 192);
            Properties.Settings.Default.profileColor = Color.FromArgb(255, 255, 192);
            Properties.Settings.Default.BWmode = false;

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

        //--For testimg----------------------------------------------
        private void button4_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00002";
            tbPassword.Text = "1234516789";
        }

        private void btnTest4_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00003";
            tbPassword.Text = "12345qwe6789";
        }

        private void btnTest5_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00004";
            tbPassword.Text = "123456ghdf789";
        }

        private void btnTest6_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00005";
            tbPassword.Text = "12345Zcz6789";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMC00001";
            tbPassword.Text = "123456we789";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            tbUsername.Text = "LMS00001";
            tbPassword.Text = "12345qer6789";
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Clean the content in the label.
            lblUsernameMsg.Text = "";
            lblPasswordMsg.Text = "";

            //username and password have not been entered.
            if (string.IsNullOrEmpty(tbPassword.Text) && string.IsNullOrEmpty(tbUsername.Text))
            {
                lblPasswordMsg.Text = "Please enter your password.";
                lblUsernameMsg.Text = "Please enter your password.";
            }
            //password have not been entered.
            else if (string.IsNullOrEmpty(tbPassword.Text))
            {
                lblPasswordMsg.Text = "Please enter your password.";
            }
            //username have not been entered.
            else if (string.IsNullOrEmpty(tbUsername.Text))
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
                Properties.Settings.Default.Reset();
            }
            


            if (tbUsername.Text.Equals("LMC00001") && string.IsNullOrEmpty(tbPassword.Text) == false)
            {
                UID = tbUsername.Text;
                uName = "Ben";
                determineFun(1);

                Form Home = new Home();
                this.Hide();
                //Swap the current form to another.
                Home.StartPosition = FormStartPosition.Manual;
                Home.Location = this.Location;
                Home.Size = this.Size;
                Home.ShowDialog();
                this.Close();
            }

            if (tbUsername.Text.Equals("LMS00001") && string.IsNullOrEmpty(tbPassword.Text) == false)
            {
                UID = tbUsername.Text;
                uName = "Ryan";
                determineFun(2);

                Form Home = new Home();
                this.Hide();
                //Swap the current form to another.
                Home.StartPosition = FormStartPosition.Manual;
                Home.Location = this.Location;
                Home.Size = this.Size;
                Home.ShowDialog();
                this.Close();
            }

            if (tbUsername.Text.Equals("LMS00002") && string.IsNullOrEmpty(tbPassword.Text) == false)
            {
                UID = tbUsername.Text;
                uName = "Kit";
                determineFun(3);

                Form Home = new Home();
                this.Hide();
                //Swap the current form to another.
                Home.StartPosition = FormStartPosition.Manual;
                Home.Location = this.Location;
                Home.Size = this.Size;
                Home.ShowDialog();
                this.Close();
            }

            if (tbUsername.Text.Equals("LMS00003") && string.IsNullOrEmpty(tbPassword.Text) == false)
            {
                UID = tbUsername.Text;
                uName = "Polly";
                determineFun(4);

                Form Home = new Home();
                this.Hide();
                //Swap the current form to another.
                Home.StartPosition = FormStartPosition.Manual;
                Home.Location = this.Location;
                Home.Size = this.Size;
                Home.ShowDialog();
                this.Close();
            }

            if (tbUsername.Text.Equals("LMS00004") && string.IsNullOrEmpty(tbPassword.Text) == false)
            {
                UID = tbUsername.Text;
                uName = "Ned";
                determineFun(5);

                Form Home = new Home();
                this.Hide();
                //Swap the current form to another.
                Home.StartPosition = FormStartPosition.Manual;
                Home.Location = this.Location;
                Home.Size = this.Size;
                Home.ShowDialog();
                this.Close();
            }

            if (tbUsername.Text.Equals("LMS00005") && string.IsNullOrEmpty(tbPassword.Text) == false)
            {
                UID = tbUsername.Text;
                uName = "Hob";
                determineFun(6);

                Form Home = new Home();
                this.Hide();
                //Swap the current form to another.
                Home.StartPosition = FormStartPosition.Manual;
                Home.Location = this.Location;
                Home.Size = this.Size;
                Home.ShowDialog();
                this.Close();
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

        //To determine which button needs to be shown.
        private void determineFun(int jobTitle)
        {
            switch (jobTitle)
            {
                case 1:     //Customer
                    showbtn1 = showbtn2 = showbtn3 = true;
                    funbtn1 = "Order Management";
                    funbtn2 = "Invoice Management";
                    funbtn3 = "User Managemnet";
                    break;
                case 2:     //Sales manager
                    showbtn1 = showbtn2 = showbtn3 = showbtn4 = showbtn5 = true;
                    funbtn1 = "Order Management";
                    funbtn2 = "Invoice Management";
                    funbtn3 = "On-Sale Product Management";
                    funbtn4 = "Stock Management";
                    funbtn5 = "User Managemnet";
                    break;
                case 3:     //Order processing clerk
                    showbtn1 = showbtn2 = true;
                    funbtn1 = "Order Management";
                    funbtn2 = "User Managemne";
                    break;
                case 4:     //Storeman
                    showbtn1 = showbtn2 = showbtn3 = true;
                    funbtn1 = "Order Management";
                    funbtn2 = "Stock Management";
                    funbtn3 = "User Managemnet";
                    break;
                case 5:     //Department manager
                    showbtn1 = true;
                    funbtn1 = "User Managemnet";
                    break;
                case 6:     //Delivery man
                    showbtn1 = true;
                    funbtn1 = "Order Management";
                    break;
            }
        }
        //--END------------------------------------------------------
        //Return the value of determineFun.
        public static dynamic showFun()
        { 
            return new {btn1show = showbtn1, btn1value = funbtn1, btn2show = showbtn2, btn2value = funbtn2, btn3show = showbtn3, btn3value = funbtn3,
                btn4show = showbtn4, btn4value = funbtn4, btn5show = showbtn5, btn5value = funbtn5, };
        }

        public static dynamic getUserInfo()
        {
            return new {userName = uName, uID = UID};
        }

        public static string getInsDate()
        {
            return sysInsDate;
        }
    }
}
