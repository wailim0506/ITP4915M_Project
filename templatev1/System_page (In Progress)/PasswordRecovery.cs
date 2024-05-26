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
        Boolean userFound = false;
        public PasswordRe()
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
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //Clean the content in the label.
            lblFinfMsg.Text = "";
            int count = 0;

            //Counting the inputted information.
            if (string.IsNullOrEmpty(tbUserID.Text) == false)
            {
                count++;
            }
            if (string.IsNullOrEmpty(tbPhone.Text) == false)
            {
                count++;
            }
            if (string.IsNullOrEmpty(tbEmail.Text) == false)
            {
                count++;
            }

            //Show the error if the user entered less than any two types of information.
            if (count < 2)
            {
                lblFinfMsg.Text = "Please at least enter any two types of information.";
            }


        }
    }
}
