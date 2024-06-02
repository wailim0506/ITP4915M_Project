using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Dynamic;
using System.Windows;

namespace templatev1
{
    public partial class CreateCustomerAcc : Form
    {
        controller.RecoveryController recoveryController;
        Bitmap IMG;
        bool IMGUploaded;
        dynamic expando;

        public CreateCustomerAcc()
        {
            InitializeComponent();
        }

        public CreateCustomerAcc(controller.RecoveryController recoveryController)
        {
            InitializeComponent();
            this.recoveryController = recoveryController;
            expando = new ExpandoObject();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IMG = null;
            IMGUploaded = false;
            timer1.Enabled = true;
            lblUID.Text = "LMC" + recoveryController.getLMCID().ToString("D5");
            lblCrateDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            tbPass.PasswordChar = tbConfirmPass.PasswordChar = '*';

            cmbProvince.Items.AddRange(recoveryController.getpriovince().ToArray()); 
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (checkInfo())       //Pass to controller and create account
            {
                setValue();
                if (recoveryController.create(expando))
                {
                    MessageBox.Show("Create account success! Your UID is LMC" + recoveryController.NumOfUser().ToString("D5") + ".\nThe system will redirect to the login page.", "System message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Form Login = new Login();
                    this.Hide();
                    //Swap the current form to another.
                    Login.StartPosition = FormStartPosition.Manual;
                    Login.Location = this.Location;
                    Login.Size = this.Size;
                    Login.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("System Error! Please Contact The Help Desk.", "System error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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

        //For image upload and remove function
        private void btnRemoveIMG_Click(object sender, EventArgs e)
        {
            IMG = null;
            btnUploadIMG.Visible = true;
            picUserIMG.Image = IMG;
        }
        private void btnUploadIMG_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp); *.PNG|*.jpg; *.jpeg; *.gif; *.bmp; *.PNG";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (new FileInfo(ofd.FileName).Length > 1000000)         //File can't larger than 1MB
                    {
                        MessageBox.Show("File too large! Maximum 1MB.");
                        IMGUploaded = false;
                    }
                    else
                    {
                        IMG = new Bitmap(Image.FromFile(ofd.FileName));
                        btnUploadIMG.Visible = false;
                        picUserIMG.Image = IMG;
                        IMGUploaded = true;

                        //Upload to the server





                    }
                }
            }
            catch
            {
                MessageBox.Show("Illegal operation, please retry.", "System error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                IMGUploaded = false;
            }
        }


        private void cmbProvince_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbCity.SelectedIndex = -1;       //clear the selected value when the province has change.
            cmbCity.Items.Clear();            //clear the value when the selected province has change.
            cmbCity.Items.AddRange(recoveryController.getcity(cmbProvince.Text).ToArray());             //change city list base on current selected province.
        }


        private bool checkInfo()
        {
            //Clean previous error message.
            lblfNameMsg.Text = lblLNameMsg.Text = lblSexMsg.Text = lblPaymentMsg.Text = lblDateMsg.Text = lblPhoneMsg.Text = lblEmailMsg.Text = lblContactMsg.Text = lblPwdMsg.Text = "";


            if (string.IsNullOrEmpty(tbFirstName.Text))               //Check firstName
            {
                lblfNameMsg.Text = "Please enter your firstName.";
                tbFirstName.Select();
                return false;
            }
            else if (tbFirstName.Text.Length < 2 || tbFirstName.Text.Length > 20)
            {
                lblfNameMsg.Text = "FirstName too short or too long, minimum 2 maximum 20.";
                tbFirstName.Select();
                return false;
            }


            if (string.IsNullOrEmpty(tbLastName.Text))               //Check lastName
            {
                lblLNameMsg.Text = "Please enter your lastName.";
                tbLastName.Select();
                return false;
            }
            else if (tbLastName.Text.Length < 2 || tbLastName.Text.Length > 20)
            {
                lblLNameMsg.Text = "LastName too short or too long, minimum 2 maximum 20.";
                tbLastName.Select();
                return false;
            }


            if (cmbGender.SelectedItem == null)               //Check gender
            {
                lblSexMsg.Text = "Please select a gender.";
                return false;
            }
;

            if (!chkNGDateOfBirth.Checked && (dtpDateOfBirth.Value.Date == DateTime.Now.Date))               //Check date of birth, MUST today > selected value > age 17.
            {
                lblDateMsg.Text = "Please select the date or click NOT provided.";
                return false;
            }
            else if (!chkNGDateOfBirth.Checked && (dtpDateOfBirth.Value.Date > DateTime.Now.Date || dtpDateOfBirth.Value.Date > new DateTime(2007, 1, 1)))
            {
                lblDateMsg.Text = "Please select a valid date or click NOT provided.";
                return false;
            }


            if (cmbPayment.SelectedItem == null)               //Check payment
            {
                lblPaymentMsg.Text = "Please select a payment method.";
                return false;
            }


            if (string.IsNullOrEmpty(tbPhone.Text))               //Check phone number
            {
                lblPhoneMsg.Text = "Please enter the phone number.";
                return false;
            }
            else if (tbPhone.Text.Length != 11)
            {
                lblPhoneMsg.Text = "Please enter the correct format.";
                return false;
            }


            if (string.IsNullOrEmpty(tbEmail.Text))               //Check email address
            {
                lblEmailMsg.Text = "Please enter the email address.";
                return false;
            }
            else if (!IsValidEmail(tbEmail.Text) || tbEmail.Text.Length > 30)
            {
                lblEmailMsg.Text = "Please enter the correct format.";
                return false;
            }

            if (string.IsNullOrEmpty(tbCompanyName.Text))               //Check company name
            {
                lblContactMsg.Text = "Please enter the company name";
                return false;
            }
            else if (tbCompanyName.Text.Length > 30)
            {
                lblContactMsg.Text = "Company name too long, maximum 30.";
                return false;
            }


            if (string.IsNullOrEmpty(tbAddress1.Text) || string.IsNullOrEmpty(tbAddress2.Text))               //Check address
            {
                lblContactMsg.Text = "Please enter both company address and warehouse address.";
                return false;
            }
            else if (tbAddress1.Text.Length > 50 || tbAddress2.Text.Length > 50 || tbAddress1.Text.Length < 5 || tbAddress2.Text.Length < 5)
            {
                lblContactMsg.Text = "Address too long or too short, minimum 5, maximum 50.";
                return false;
            }
            else if (cmbProvince.SelectedItem == null)
            {
                lblContactMsg.Text = "Please select a province.";
                return false;
            }
            else if (cmbCity.SelectedItem == null)
            {
                lblContactMsg.Text = "Please select a city.";
                return false;
            }


            if (string.IsNullOrEmpty(tbPass.Text))               //Check whether the password and confirm the password match.
            {
                lblPwdMsg.Text = "Please enter the password.";
                return false;
            }
            else if (tbPass.Text.Length < 10 || tbPass.Text.Length > 50)
            {
                lblPwdMsg.Text = "Password too short or too long, minimum 10 maximum 50.";
                return false;
            }
            else if (!tbPass.Text.Equals(tbConfirmPass.Text))
            {
                lblPwdMsg.Text = "Confirm password does NOT match.";
                return false;
            }
                return true;
        }

        private void setValue()
        {
            expando = new ExpandoObject();
            expando.fName = tbFirstName.Text;
            expando.lName = tbLastName.Text;
            expando.joinDate = DateTime.Now.ToString("yyyy/MM/dd");

            if (cmbGender.SelectedIndex == 0)
                expando.gender = "M";
            else
                expando.gender = "F";

            if (chkNGDateOfBirth.Checked)
                expando.dateOfBirth = "NULL";
            else
                expando.dateOfBirth = dtpDateOfBirth.Value.ToString("'yyyy-MM-dd'");

            if (IMGUploaded)
                expando.IMG = "''";
            else
                expando.IMG = "NULL";

            expando.payment = cmbPayment.SelectedItem.ToString();
            expando.phone = tbPhone.Text;
            expando.email = tbEmail.Text;
            expando.company = tbCompanyName.Text;
            expando.province = cmbProvince.SelectedItem.ToString();
            expando.city = cmbCity.SelectedItem.ToString();
            expando.address1 = tbAddress1.Text;
            expando.address2 = tbAddress2.Text;
            expando.pwd = tbConfirmPass.Text;
        }




        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }


    }
}
