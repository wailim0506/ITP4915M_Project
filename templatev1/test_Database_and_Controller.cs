using System;
using System.Collections.Generic;
using System.Windows.Forms;
using controller;

namespace templatev1
{
    public partial class test_Database_and_Controller : Form
    {
        public test_Database_and_Controller()
        {
            InitializeComponent();
        }

        private void test_Database_and_Controller_Load(object sender, EventArgs e)
        {
            testController
                dt = new testController(); //testController is the name of the controller, different function have different controller
            //dataGridView1.DataSource = dt.login();     //test1() is the method inside the controller file
            // dataGridView2.DataSource = dt.test2();
            //dataGridView3.DataSource = dt.test3();
        }

        // hash password and copy to clipboard
        private void btnTest1_Click_1(object sender, EventArgs e)
        {
            string password = tbHashPassword.Text;
            string hashedPassword = testController.HashPassword(password);
            Clipboard.SetText(hashedPassword);
            MessageBox.Show("Password hashed successfully and Copied to clipboard.");
        }

        private void UpdateUsersPassword()
        {
            testController testController = new testController();
            var usersToUpdate = new Dictionary<string, string>
            {
                // Staff
                { "LMS00001", "123456" },
                { "LMS00002", "123456" },
                { "LMS00003", "123456" },
                { "LMS00004", "123456" },
                { "LMS00005", "123456" },
                { "LMS00006", "123456" },
                { "LMS00007", "123456" },
                { "LMS00008", "123456" },
                { "LMS00009", "123456" },
                { "LMS00010", "123456" },
                { "LMS00011", "123456" },
                // Customer
                { "LMC00001", "123456" },
                { "LMC00002", "123456" },
                { "LMC00003", "123456" },
                { "LMC00004", "123456" }
            };

            // update password
            MessageBox.Show(testController.UpdatePassword(usersToUpdate)
                ? "Password updated successfully."
                : $"not updated at {string.Join(", ", usersToUpdate.Keys)}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //UpdateUsersPassword();
            testController testController = new testController();
            testController.UpdateDeveloperAccount();
            MessageBox.Show("Password updated successfully.");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            testController testController = new testController();
            string userid = textBox3.Text;
            string newPassword = textBox1.Text;

            MessageBox.Show(testController.DeveloperToolForgetPassword(userid, newPassword)
                ? "Password updated successfully."
                : "Password not updated.");
        }
    }
}