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
        }

        public PasswordRe(controller.RecoveryController recoveryController)
        {
            InitializeComponent();
            this.recoveryController = recoveryController;
            userFound = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            tbPassword.PasswordChar = tbConfirmPass.PasswordChar = '*';
        }

        private void btnToLogin_Click(object sender, EventArgs e)
        {
            Form Login = new Login();
            Hide();
            //Swap the current form to another.
            Login.StartPosition = FormStartPosition.Manual;
            Login.Location = Location;
            Login.Size = Size;
            Login.ShowDialog();
            Close();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            //Clean the content in the label.
            lblChangePassMsg.Text = "";

            //Show the error if the user hasn't found the account yet.
            if (!userFound)
            {
                lblChangePassMsg.Text = "Please find the account first.";
                tbUserID.Select();
            }
            else
            {
                if (tbPassword.Text.Length < 10 || tbPassword.Text.Length > 50) //Too short.
                {
                    lblChangePassMsg.Text = "Password too short or too long, minimum 10 maximum 50.";
                    tbPassword.Select();
                }
                else
                {
                    if (tbPassword.Text.Equals(tbConfirmPass.Text)) //Confirm password matched.
                    {
                        recoveryController.ChangePassword(tbConfirmPass.Text);
                        MessageBox.Show("Password changed! The system will redirect to the login page.",
                            "System message");
                        Form login = new Login();
                        Hide();
                        //Swap the current form to another.
                        login.StartPosition = FormStartPosition.Manual;
                        login.Location = Location;
                        login.Size = Size;
                        login.ShowDialog();
                        Close();
                    }
                    else //NOT match.
                    {
                        lblChangePassMsg.Text = "Passwords do NOT match.";
                        tbConfirmPass.Select();
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
            UID = phone = email = "";

            //Counting the inputted information.
            if (!string.IsNullOrEmpty(tbUserID.Text))
            {
                count += 2;
                UID = tbUserID.Text;
            }

            if (!string.IsNullOrEmpty(tbPhone.Text))
            {
                count++;
                phone = tbPhone.Text;
            }

            if (!string.IsNullOrEmpty(tbEmail.Text))
            {
                count++;
                email = tbEmail.Text;
            }

            //Show the error if the user entered less than any two types of information.
            if (count < 3)
            {
                lblFinfMsg.Text = "Please at least enter any two types of information.";
                tbUserID.Select();
                userFound = false;
            }
            else
            {
                if (recoveryController.FindUser(UID, email, phone)) //User found
                {
                    lblFinfMsg.Text = "User found!";
                    tbPassword.Select();
                    userFound = true;
                }
                else //User NOT found
                {
                    lblFinfMsg.Text = "User NOT found, please retry.";
                    tbUserID.Select();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }
    }
}