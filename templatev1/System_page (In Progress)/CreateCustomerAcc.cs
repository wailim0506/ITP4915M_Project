using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace templatev1
{
    public partial class CreateCustomerAcc : Form
    {
        controller.RecoveryController recoveryController;
        Bitmap IMG;
        bool IMGUploaded;
        dynamic value;

        public CreateCustomerAcc()
        {
            InitializeComponent();
        }

        public CreateCustomerAcc(controller.RecoveryController recoveryController)
        {
            InitializeComponent();
            this.recoveryController = recoveryController;
            value = new ExpandoObject();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IMG = null;
            IMGUploaded = false;
            timer1.Enabled = true;
            lblUID.Text = "LMC" + recoveryController.getLMCID().ToString("D5");
            lblCrateDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            tbPass.PasswordChar = tbConfirmPass.PasswordChar = '*';

            cmbProvince.Items.AddRange(recoveryController.GetProvince().ToArray());
        }

        //User create a new customer account.
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (checkInfo()) //Pass to controller and create account
            {
                setValue(); //If passed set the value in to dynameic.
                if (recoveryController.create(value))
                {
                    MessageBox.Show(
                        "Create account success! Your UID is LMC" + (recoveryController.getLMCID() - 1).ToString("D5") +
                        ".\nThe system will redirect to the login page.", "System message", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Form Login = new Login();
                    Hide();
                    //Swap the current form to another.
                    Login.StartPosition = FormStartPosition.Manual;
                    Login.Location = Location;
                    Login.Size = Size;
                    Login.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("System Error! Please Contact The Help Desk.", "System error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    Form Login = new Login();
                    Hide();
                    //Swap the current form to another.
                    Login.StartPosition = FormStartPosition.Manual;
                    Login.Location = Location;
                    Login.Size = Size;
                    Login.ShowDialog();
                    Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
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
                    if (new FileInfo(ofd.FileName).Length > 1000000) //File can't larger than 1MB
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

                        //Upload to local drive.
                    }
                }
            }
            catch
            {
                MessageBox.Show("Illegal operation, please retry.", "System error", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                IMGUploaded = false;
            }
        }

        //Set new value to the city listbox when the selected province has changed.
        private void cmbProvince_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbCity.SelectedIndex = -1; //clear the selected value when the province has change.
            cmbCity.Items.Clear(); //clear the value when the selected province has change.
            cmbCity.Items.AddRange(recoveryController.GetCity(cmbProvince.Text)
                .ToArray()); //change city list base on current selected province.
        }

        //Check the inputted data.
        private bool checkInfo()
        {
            //Clean previous error message.
            lblfNameMsg.Text = lblLNameMsg.Text = lblSexMsg.Text = lblPaymentMsg.Text = lblDateMsg.Text =
                lblPhoneMsg.Text = lblEmailMsg.Text = lblContactMsg.Text = lblPwdMsg.Text = "";

            //Check firstName
            if (string.IsNullOrEmpty(tbFirstName.Text))
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

            //Check lastName
            if (string.IsNullOrEmpty(tbLastName.Text))
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

            //Check gender
            if (cmbGender.SelectedItem == null)
            {
                lblSexMsg.Text = "Please select a gender.";
                cmbGender.Select();
                return false;
            }

            //Check date of birth, MUST today > selected value > age 17.
            if (!chkNGDateOfBirth.Checked && (dtpDateOfBirth.Value.Date == DateTime.Now.Date))
            {
                lblDateMsg.Text = "Please select the date or click NOT provided.";
                return false;
            }
            else if (!chkNGDateOfBirth.Checked && (dtpDateOfBirth.Value.Date > DateTime.Now.Date ||
                                                   dtpDateOfBirth.Value.Date > new DateTime(2007, 1, 1)))
            {
                lblDateMsg.Text = "Please select a valid date or click NOT provided.";
                return false;
            }

            //Check payment
            if (cmbPayment.SelectedItem == null)
            {
                lblPaymentMsg.Text = "Please select a payment method.";
                cmbPayment.Select();
                return false;
            }

            //Check phone number
            if (string.IsNullOrEmpty(tbPhone.Text))
            {
                lblPhoneMsg.Text = "Please enter the phone number.";
                tbPhone.Select();
                return false;
            }
            else if (!Regex.Match(tbPhone.Text, @"^([0-9]{11})$").Success && !Regex.Match(tbPhone.Text, @"^([0-9]{8})$").Success)
            {
                lblPhoneMsg.Text = "Please enter the correct format.";
                tbPhone.Select();
                return false;
            }
            else if (!recoveryController.CheckEmailPhone(tbPhone.Text))
            {
                lblPhoneMsg.Text = "The phone number has already registered an account.";
                tbPhone.Select();
                return false;
            }

            //Check email address
            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                lblEmailMsg.Text = "Please enter the email address.";
                tbEmail.Select();
                return false;
            }
            else if (!IsValidEmail(tbEmail.Text) || tbEmail.Text.Length > 30)
            {
                lblEmailMsg.Text = "Please enter the correct format.";
                tbEmail.Select();
                return false;
            }
            else if (!recoveryController.CheckEmailPhone(tbEmail.Text))
            {
                lblEmailMsg.Text = "The email address has already registered an account.";
                tbEmail.Select();
                return false;
            }

            //Check company name
            if (string.IsNullOrEmpty(tbCompanyName.Text))
            {
                lblContactMsg.Text = "Please enter the company name";
                tbCompanyName.Select();
                return false;
            }
            else if (tbCompanyName.Text.Length > 30)
            {
                lblContactMsg.Text = "Company name too long, maximum 30.";
                tbCompanyName.Select();
                return false;
            }

            //Check address
            if (string.IsNullOrEmpty(tbAddress1.Text) || string.IsNullOrEmpty(tbAddress2.Text))
            {
                lblContactMsg.Text = "Please enter both company address and warehouse address.";
                tbAddress1.Select();
                return false;
            }
            else if (tbAddress1.Text.Length > 50 || tbAddress2.Text.Length > 50 || tbAddress1.Text.Length < 5 ||
                     tbAddress2.Text.Length < 5)
            {
                lblContactMsg.Text = "Address too long or too short, minimum 5, maximum 50.";
                tbAddress1.Select();
                return false;
            }
            else if (cmbProvince.SelectedItem == null)
            {
                lblContactMsg.Text = "Please select a province.";
                cmbProvince.Select();
                return false;
            }
            else if (cmbCity.SelectedItem == null)
            {
                lblContactMsg.Text = "Please select a city.";
                cmbCity.Select();
                return false;
            }

            //Check whether the password and confirm the password match.
            if (string.IsNullOrEmpty(tbPass.Text))
            {
                lblPwdMsg.Text = "Please enter the password.";
                tbPass.Select();
                return false;
            }
            else if (tbPass.Text.Length < 10 || tbPass.Text.Length > 50)
            {
                lblPwdMsg.Text = "Password too short or too long, minimum 10 maximum 50.";
                tbPass.Select();
                return false;
            }
            else if (!tbPass.Text.Equals(tbConfirmPass.Text))
            {
                lblPwdMsg.Text = "Confirm password does NOT match.";
                tbConfirmPass.Select();
                return false;
            }

            return true;
        }

        //Check the email address.
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

        //Return the value to the contoller.
        private void setValue()
        {
            value = new ExpandoObject();
            value.fName = tbFirstName.Text;
            value.lName = tbLastName.Text;
            value.joinDate = DateTime.Now.ToString("yyyy/MM/dd");

            if (cmbGender.SelectedIndex == 0)
                value.gender = "M";
            else
                value.gender = "F";

            if (chkNGDateOfBirth.Checked)
                value.dateOfBirth = DBNull.Value;
            else
                value.dateOfBirth = dtpDateOfBirth.Value.ToString("yyyy/MM/dd");

            if (IMGUploaded)
                value.IMG = "''";
            else
                value.IMG = "NULL";

            value.payment = cmbPayment.SelectedItem.ToString();
            value.phone = tbPhone.Text;
            value.email = tbEmail.Text;
            value.company = tbCompanyName.Text;
            value.province = cmbProvince.SelectedItem.ToString();
            value.city = cmbCity.SelectedItem.ToString();
            value.address1 = tbAddress1.Text;
            value.address2 = tbAddress2.Text;
            value.pwd = tbConfirmPass.Text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }
    }
}