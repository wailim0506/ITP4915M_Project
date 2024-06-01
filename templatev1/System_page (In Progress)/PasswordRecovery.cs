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
    public partial class PasswordRe : Form
    {
        controller.RecoveryController recoveryController;
        Boolean userFound;
        string UID, email, phone;

        public PasswordRe()
        {
            InitializeComponent();
            userFound = false;
            UID = email = phone = "";
        }

        public PasswordRe(controller.RecoveryController recoveryController)
        {
            InitializeComponent();
            userFound = false;
            UID = email = phone = "";
            this.recoveryController = recoveryController;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void btnToLogin_Click(object sender, EventArgs e)
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

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            //Clean the content in the label.
            lblChangePassMsg.Text = "";

            //Show the error if the user hasn't found the account yet.
            if (userFound == false)
            {
                lblChangePassMsg.Text = "Please find the account first.";
            }
            else
            {
                if (tbPassword.Text.Length < 10 || tbPassword.Text.Length > 50)         //Too short.
                    lblChangePassMsg.Text = "Password too short or too long, minimum 11 maximum 50.";
                else
                {
                    if (tbPassword.Text.Equals(tbConfirmPass.Text))              //Confirm password matched.
                    {
                        recoveryController.changPwd(tbConfirmPass.Text);
                        MessageBox.Show("Password changed! The system will redirect to the login page.");
                        Form login = new Login();
                        this.Hide();
                        //Swap the current form to another.
                        login.StartPosition = FormStartPosition.Manual;
                        login.Location = this.Location;
                        login.Size = this.Size;
                        login.ShowDialog();
                        this.Close();
                    }
                    else        //NOT match.
                    {
                        lblChangePassMsg.Text = "Passwords do NOT match.";
                    }
                }
            
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //Clean the content in the label.
            lblFinfMsg.Text = "";
            lblChangePassMsg.Text = "";
            int count = 0;
            UID = phone = email ="";

            //Counting the inputted information.
            if (string.IsNullOrEmpty(tbUserID.Text) == false)
            {
                count+=2;
                UID = tbUserID.Text;
            }
            if (string.IsNullOrEmpty(tbPhone.Text) == false)
            {
                count++;
                phone = tbPhone.Text;
            }
            if (string.IsNullOrEmpty(tbEmail.Text) == false)
            {
                count++;
                email = tbEmail.Text;
            }

            //Show the error if the user entered less than any two types of information.
            if (count < 3)
            {
                lblFinfMsg.Text = "Please at least enter any two types of information.";
                userFound = false;
            }
            else 
            {
                if (recoveryController.findUser(UID, email, phone))         //User found
                {
                    lblFinfMsg.Text = "User found!";
                    userFound = true;

                }
                else          //User NOT found
                {
                    lblFinfMsg.Text = "User NOT found, please retry.";
                }
            
            }

        }
    }
}
