﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using controller;
using templatev1;

namespace templatev1
{
    public partial class UserModify : Form
    {
        private string uName, UID, type, modifyUserUID;
        AccountController accountController;
        UIController UIController;
        proFileController proFileController;
        UserController UserController;
        proFileController UserInfo;
        Bitmap IMG;
        dynamic placeholder, update;
        bool IMGUploaded;

        public UserModify(AccountController accountController, UIController UIController, string modifyUserUID)
        {
            InitializeComponent();
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            UserController = new UserController();
            this.modifyUserUID = modifyUserUID;
            this.accountController = accountController;
            this.UIController = UIController;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialization();
        }

        private void Initialization()
        {
            setIndicator(UIController.getIndicator("User Management"));
            timer1.Enabled = true;
            UID = accountController.GetUid();
            uName = accountController.GetName();
            lblUid.Text = "UID: " + UID;
            type = modifyUserUID.StartsWith("LMS") ? "Staff" : "Customer";
            palstaff.Visible = type.Equals("Staff") ? true : false;
            chkNGDateOfBirth.Visible = type.Equals("Customer") ? true : false;
            lblAccType.Text = type;
            lblFormUID.Text = modifyUserUID;


            //For determine which button needs to be shown.
            dynamic btnFun = UIController.showFun();
            btnFunction1.Visible = btnFun.btn1show;
            btnFunction1.Text = btnFun.btn1value;
            btnFunction2.Visible = btnFun.btn2show;
            btnFunction2.Text = btnFun.btn2value;
            btnFunction3.Visible = btnFun.btn3show;
            btnFunction3.Text = btnFun.btn3value;
            btnFunction4.Visible = btnFun.btn4show;
            btnFunction4.Text = btnFun.btn4value;
            btnFunction5.Visible = btnFun.btn5show;
            btnFunction5.Text = btnFun.btn5value;

            UserInfo = new proFileController(modifyUserUID, type);
            dynamic info = UserInfo.getUserInfo();
            tbFirstName.Text = info.fName;
            tbLastName.Text = info.lName;
            cmbGender.Text = (info.sex).Equals("M")?"Male":"Female";
            tbEmail.Text = info.email;
            tbPhone.Text = info.phone;
            lblDept.Text = info.dept;
            lblJob.Text = info.jobTitle;
            chkNGDateOfBirth.Checked = info.NGDateOfBirth;
            dtpDateOfBirth.Value = DateTime.ParseExact((info.dateOfBirth).ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
            lblCreateDate.Text = (info.createDate).ToString("yyyy/MM/dd");

            placeholder = UserInfo.getUserInfo();
        }

        private void setIndicator(int btnNo)
        {
            switch (btnNo)
            {
                case 1:
                    palSelect1.Visible = true;
                    break;
                case 2:
                    palSelect2.Visible = true;
                    break;
                case 3:
                    palSelect3.Visible = true;
                    break;
                case 4:
                    palSelect4.Visible = true;
                    break;
                case 5:
                    palSelect5.Visible = true;
                    break;
            }
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            getPage(btnFunction1.Text);
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction2.Text);
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            getPage(btnFunction3.Text);
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            getPage(btnFunction4.Text);
        }

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
        }

        //Determine next page.
        private void getPage(string Function)
        {
            Form next = new Home(accountController, UIController);
            switch (Function)
            {
                //my version
                case "Order Management":
                    if (UID.StartsWith("LMC"))
                    {
                        next = new customerOrderList(accountController, UIController);
                    }
                    else
                    {
                        next = new templatev1.staffOrderList(accountController, UIController);
                    }

                    break;
                case "Spare Part":
                    next = new sparePartList(accountController, UIController);
                    break;
                case "Cart":
                    next = new cart(accountController, UIController);
                    break;
                case "Favourite":
                    next = new favourite(accountController, UIController);
                    break;
                case "Give Feedback":
                    next = new giveFeedback(accountController, UIController);
                    break;
                //my version

                case "On-Sale Product Management":
                    next = new OnSaleMain(accountController, UIController);
                    break;
                case "Stock Management":
                    next = new StockMgmt(accountController, UIController);
                    break;
                case "User Management":
                    next = new SAccManage(accountController, UIController);
                    break;
            }

            Hide();
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.ShowDialog();
            Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form login = new Login();
            Hide();
            //Swap the current form to another.
            login.StartPosition = FormStartPosition.Manual;
            login.Location = Location;
            login.Size = Size;
            login.ShowDialog();
            Close();
        }

        private void lblCorpName_Click(object sender, EventArgs e)
        {
            Form about = new About(accountController, UIController);
            Hide();
            //Swap the current form to another.
            about.StartPosition = FormStartPosition.Manual;
            about.Location = Location;
            about.Size = Size;
            about.ShowDialog();
            Close();
        }

        //The placeholder for textbox.
        private void tbFirstName_Enter(object sender, EventArgs e)
        {
            if (tbFirstName.Text == placeholder.fName)
                tbFirstName.Text = "";
        }

        private void tbFirstName_Leave(object sender, EventArgs e)
        {
            if (tbFirstName.Text == "")
                tbFirstName.Text = placeholder.fName;
        }

        private void tbLastName_Enter(object sender, EventArgs e)
        {
            if (tbLastName.Text == placeholder.lName)
                tbLastName.Text = "";
        }

        private void tbLastName_Leave(object sender, EventArgs e)
        {
            if (tbLastName.Text == "")
                tbLastName.Text = placeholder.lName;
        }

        private void tbPhone_Enter(object sender, EventArgs e)
        {
            if (tbPhone.Text == placeholder.phone)
                tbPhone.Text = "";
        }

        private void tbPhone_Leave(object sender, EventArgs e)
        {
            if (tbPhone.Text == "")
                tbPhone.Text = placeholder.phone;
        }

        private void tbEmail_Leave(object sender, EventArgs e)
        {
            if (tbEmail.Text == "")
                tbEmail.Text = placeholder.email; 
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (checkInfo())
                if (UserInfo.MgmtModifyUserInfo(update))
                {
                    MessageBox.Show("Modify successful!", "System message", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    
                }
                else //Something wrong from the controller.
                    MessageBox.Show("System Error! Please Contact The Help Desk.", "System error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
        }

        private bool checkInfo()
        {
            lblfNameMsg.Text = lbllNameMsg.Text = lblDateMsg.Text = lblPhoneMsg.Text = "";
            update = new ExpandoObject();

            //Check and update firstName if have change.
            if (tbFirstName.Text != placeholder.fName)
            {
                if (tbFirstName.Text.Length < 2 || tbFirstName.Text.Length > 20)
                {
                    lblfNameMsg.Text = "FirstName too short or too long, minimum 2 maximum 20.";
                    tbFirstName.Select();
                    return false;
                }

                update.fName = tbFirstName.Text;
            }
            else
                update.fName = placeholder.fName;

            //Check and update lastName if have change.
            if (tbLastName.Text != placeholder.lName)
            {
                if (tbLastName.Text.Length < 2 || tbLastName.Text.Length > 20)
                {
                    lbllNameMsg.Text = "LastName too short or too long, minimum 2 maximum 20.";
                    tbLastName.Select();
                    return false;
                }

                update.lName = tbLastName.Text;
            }
            else
                update.lName = placeholder.lName;

            //Check and update gender if have change.
            update.sex = cmbGender.SelectedIndex == 0 ? "M" : "F";

            //Check and update date of birth if have change.
            if (dtpDateOfBirth.Value !=
                DateTime.ParseExact((placeholder.dateOfBirth).ToString("dd/MM/yyyy"), "dd/MM/yyyy", null) ||
                !chkNGDateOfBirth.Checked)
            {
                if (!chkNGDateOfBirth.Checked && (dtpDateOfBirth.Value.Date > DateTime.Now.Date ||
                                                  dtpDateOfBirth.Value.Date > new DateTime(2007, 1, 1)))
                {
                    lblDateMsg.Text = type.Equals("Staff")?"Please select a valid date or click NOT provided.": "Please select a valid date or click NOT provided.";
                    return false;
                }

                update.DFB = dtpDateOfBirth.Value.ToString("yyyy-MM-dd");
            }
            else if (chkNGDateOfBirth.Checked)
                update.DFB = DBNull.Value;
            else
                update.DFB = dtpDateOfBirth.Value.ToString("yyyy-MM-dd");

            //Check and update phone if have change.
            if (tbPhone.Text != placeholder.phone)
            {
                if (tbPhone.Text.Length != 11)
                {
                    lblPhoneMsg.Text = "Please enter the correct phone format.";
                    tbPhone.Select();
                    return false;
                }

                if (!proFileController.CheckEmailPhone(tbPhone.Text))
                {
                    lblPhoneMsg.Text = "The phone number has already registered an account.";
                    tbPhone.Select();
                    return false;
                }

                update.phone = tbPhone.Text;
            }
            else
                update.phone = placeholder.phone;

            //Check and update email if have chagne.
            if (tbEmail.Text != placeholder.email)
            {
                if (!IsValidEmail(tbEmail.Text) || tbEmail.Text.Length > 30)
                {
                    lblPhoneMsg.Text = "Please enter the correct format.";
                    tbEmail.Select();
                    return false;
                }
                else if (!proFileController.CheckEmailPhone(tbEmail.Text))
                {
                    lblPhoneMsg.Text = "The email address has already registered an account.";
                    tbEmail.Select();
                    return false;
                }

                update.email = tbPhone.Text;
            }
            else
                update.email = placeholder.phone;


            return true;
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

        private void tbEmail_Enter(object sender, EventArgs e)
        {
            if (tbEmail.Text == placeholder.email)
                tbEmail.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Form SAccManage = new SAccManage(accountController, UIController);
            Hide();
            //Swap the current form to another.
            SAccManage.StartPosition = FormStartPosition.Manual;
            SAccManage.Location = Location;
            SAccManage.Size = Size;
            SAccManage.ShowDialog();
            Close();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Form home = new Home(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.Size = Size;
            home.ShowDialog();
            Close();
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {
            proFileController = new proFileController(accountController);

            proFileController.setType(accountController.GetAccountType());

            Form proFile = new proFileMain(accountController, UIController, proFileController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.Size = Size;
            proFile.ShowDialog();
            Close();
        }
    }
}