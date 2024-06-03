using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1
{
    public partial class proFileMain : Form
    {
        private string uName, UID;
        Bitmap IMG;
        dynamic placeholder, update;
        bool IMGUploaded;

        controller.accountController accountController;
        controller.UIController UIController;
        controller.proFileController proFileController;
        

        public proFileMain()
        {
            InitializeComponent();
        }

        public proFileMain(controller.accountController accountController, controller.UIController UIController, controller.proFileController proFileController)
        {
            InitializeComponent();

            this.accountController = accountController;
            this.UIController = UIController;
            this.proFileController = proFileController;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialization();
            tbOldPass.PasswordChar = tbPass.PasswordChar = tbConfirmPass.PasswordChar = '*';
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {
            proFileController = new controller.proFileController(accountController);

            proFileController.setType(accountController.getType());

            Form proFile = new proFileMain(accountController, UIController, proFileController);
            this.Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = this.Location;
            proFile.Size = this.Size;
            proFile.ShowDialog();
            this.Close();
        }

        private void lblCorpName_Click(object sender, EventArgs e)
        {
            Form about = new About(accountController, UIController);
            this.Hide();
            //Swap the current form to another.
            about.StartPosition = FormStartPosition.Manual;
            about.Location = this.Location;
            about.Size = this.Size;
            about.ShowDialog();
            this.Close();
        }

        private void picBWMode_Click(object sender, EventArgs e)
        {
            UIController.setMode(Properties.Settings.Default.BWmode);
            BWMode();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form login = new Login();
            this.Hide();
            //Swap the current form to another.
            login.StartPosition = FormStartPosition.Manual;
            login.Location = this.Location;
            login.Size = this.Size;
            login.ShowDialog();
            this.Close();
        }

        

        private void Initialization()
        {
            timer1.Enabled = true;
            UID = accountController.getUID();
            uName = accountController.getName();
            lblUid.Text = "UID: " + UID;
            lblUserUID.Text = UID;
            placeholder = proFileController.getUserInfo();

            //Show user information
            dynamic info = proFileController.getUserInfo();
            lblAccType.Text = info.accountType;
            lblJobTitle.Text = info.jobTitle;
            lblDept.Text = info.dept;
            lblEmail.Text = info.email;
            tbFirstName.Text = info.fName;
            tbLastName.Text = info.lName;
            cmbGender.Text = info.sex;
            tbPhone.Text = info.phone;
            dtpDateOfBirth.Value = DateTime.ParseExact((info.dateOfBirth).ToString("dd/MM/yyyy"), "dd/MM/yyyy", null); 
            lblCreateDate.Text = (info.createDate).ToString("yyyy/MM/dd");
            chkNGDateOfBirth.Checked = info.NGDateOfBirth;
            lblCorpAddress.Text = info.caddress;
            cmbPayment.Text = info.payment;
            tbCorp.Text = info.corp;
            lblWareAddress.Text = info.waddress;


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
            

            //For swap the form betwee staff and customer
            dynamic show = UIController.proFile();
            lblTitJobTitle.Visible = lblJobTitle.Visible = lblTitDept.Visible = lblDept.Visible = show.group1;
            lblTitWareAdd.Visible = lblWareAddress.Visible = lblTitCorpAdd.Visible = lblCorpAddress.Visible 
                = lblTItCCorpName.Visible = tbCorp.Visible = chkNGDateOfBirth.Visible = lblTitPayment.Visible 
                = cmbPayment.Visible = btnManagAddress.Visible = btnDelete.Visible = show.group2;

            //For icon color
            if (Properties.Settings.Default.BWmode == true)
            {
                picBWMode.Image = Properties.Resources.LBWhite;
                picHome.Image = Properties.Resources.homeWhite;
            }
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Form home = new Home(accountController, UIController);
            this.Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = this.Location;
            home.Size = this.Size;
            home.ShowDialog();
            this.Close();
        }


        //For Dark Color function
        private void BWMode()
        {
            dynamic value = UIController.getMode();
            Properties.Settings.Default.textColor = ColorTranslator.FromHtml(value.textColor);
            Properties.Settings.Default.bgColor = ColorTranslator.FromHtml(value.bgColor);
            Properties.Settings.Default.navBarColor = ColorTranslator.FromHtml(value.navBarColor);
            Properties.Settings.Default.navColor = ColorTranslator.FromHtml(value.navColor);
            Properties.Settings.Default.timeColor = ColorTranslator.FromHtml(value.timeColor);
            Properties.Settings.Default.locTbColor = ColorTranslator.FromHtml(value.locTbColor);
            Properties.Settings.Default.logoutColor = ColorTranslator.FromHtml(value.logoutColor);
            Properties.Settings.Default.profileColor = ColorTranslator.FromHtml(value.profileColor);
            Properties.Settings.Default.btnColor = ColorTranslator.FromHtml(value.btnColor);
            Properties.Settings.Default.BWmode = value.BWmode;
            if (Properties.Settings.Default.BWmode == true)
            {
                picBWMode.Image = Properties.Resources.LBWhite;
                picHome.Image = Properties.Resources.homeWhite;
            }
            else
            {
                picBWMode.Image = Properties.Resources.LB;
                picHome.Image = Properties.Resources.home;
            }
        }

        private void btnManagAddress_Click(object sender, EventArgs e)
        {
            Form AddressMgmt = new AddressMgmt(accountController, UIController, proFileController);
            this.Hide();
            //Swap the current form to another.
            AddressMgmt.StartPosition = FormStartPosition.Manual;
            AddressMgmt.Location = this.Location;
            AddressMgmt.Size = this.Size;
            AddressMgmt.ShowDialog();
            this.Close();
        }


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

        private void tbCorp_Enter(object sender, EventArgs e)
        {
            if (tbCorp.Text == placeholder.corp)
                tbCorp.Text = "";
        }

        private void tbCorp_Leave(object sender, EventArgs e)
        {
            if (tbCorp.Text == "")
                tbCorp.Text = placeholder.corp;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Form home = new Home(accountController, UIController);
            this.Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = this.Location;
            home.Size = this.Size;
            home.ShowDialog();
            this.Close();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (checkInfo())
                if (proFileController.modify(update))
                {
                    MessageBox.Show("Modify successful!", "System message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    proFileController = new controller.proFileController(accountController);
                    proFileController.setType(accountController.getType());

                    Form proFile = new proFileMain(accountController, UIController, proFileController);
                    this.Hide();
                    //Swap the current form to another.
                    proFile.StartPosition = FormStartPosition.Manual;
                    proFile.Location = this.Location;
                    proFile.Size = this.Size;
                    proFile.ShowDialog();
                    this.Close();
                }
                else
                    MessageBox.Show("System Error! Please Contact The Help Desk.", "System error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private bool checkInfo()
        {
            lblfNameMsg.Text = lbllNameMsg.Text = lblDateMsg.Text = lblContactMsg.Text = lblPhoneMsg.Text = "";
            update = new ExpandoObject();

            if (tbFirstName.Text != placeholder.fName)           //Check and update firstName if have change.
            {
                if (tbFirstName.Text.Length < 2 || tbFirstName.Text.Length > 20)
                {
                    lblfNameMsg.Text = "FirstName too short or too long, minimum 2 maximum 20.";
                    tbFirstName.Select();
                    return false;
                }
                else
                    update.fName = tbFirstName.Text;
            }
            else
                update.fName = placeholder.fName;


            if (tbLastName.Text != placeholder.lName)           //Check and update lastName if have change.
            {
                if (tbLastName.Text.Length < 2 || tbLastName.Text.Length > 20)
                {
                    lbllNameMsg.Text = "LastName too short or too long, minimum 2 maximum 20.";
                    tbLastName.Select();
                    return false;
                }
                else
                    update.lName = tbLastName.Text;
            }
            else
                update.lName = placeholder.lName;


           if (cmbGender.SelectedIndex == 0)           //Check and update gender if have change.
                update.sex = "M";
           else
                update.sex = "F";


            if (dtpDateOfBirth.Value != DateTime.ParseExact((placeholder.dateOfBirth).ToString("dd/MM/yyyy"), "dd/MM/yyyy", null) || !chkNGDateOfBirth.Checked)           //Check and update date of birth if have change.
            {
                if (!chkNGDateOfBirth.Checked && (dtpDateOfBirth.Value.Date > DateTime.Now.Date || dtpDateOfBirth.Value.Date > new DateTime(2007, 1, 1)))
                {
                    lblDateMsg.Text = "Please select a valid date or click NOT provided.";
                    return false;
                }
                else 
                    update.DFB = "'" + placeholder.dateOfBirth.ToString("yyyy-MM-dd") + "'";
            }
            else if (chkNGDateOfBirth.Checked)
                update.DFB = "NULL";
            else
                update.DFB = "'" + dtpDateOfBirth.Value.ToString("yyyy-MM-dd") + "'";


            if (tbPhone.Text != placeholder.phone)           //Check and update phone if have change.
            {
                if (tbPhone.Text.Length != 11)
                {
                    lblPhoneMsg.Text = "Please enter the correct phone format.";
                    tbPhone.Select();
                    return false;
                }
                else
                    update.phone = tbPhone.Text;
            }
            else
                update.phone = placeholder.phone;


            if (placeholder.accountType.Equals("Customer"))
            {
                update.pay = cmbPayment.SelectedItem.ToString();           //Check and update payment if have change.

                if (tbCorp.Text != placeholder.corp)           //Check and update company name if have change.
                {
                    if (tbCorp.Text.Length > 30)
                    {
                        lblContactMsg.Text = "Company name too long, maximum 30.";
                        tbCorp.Select();
                        return false;
                    }
                    else
                        update.corp = tbCorp.Text;
                }
                else
                    update.corp = placeholder.corp;
            }

            return true;
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
        private void btnRemoveIMG_Click(object sender, EventArgs e)
        {
            IMG = null;
            btnUploadIMG.Visible = true;
            picUserIMG.Image = IMG;
        }

        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbOldPass.Text))
            {
                if (accountController.matchPwd(tbOldPass.Text))
                {
                    if (checkPwd() == true)
                    {
                        controller.RecoveryController recoveryController = new controller.RecoveryController(accountController);
                        recoveryController.changPwd(tbConfirmPass.Text);
                        MessageBox.Show("Password changed successful!\nThe system will redirect to the login page.", "System message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Form login = new Login();
                        this.Hide();
                        //Swap the current form to another.
                        login.StartPosition = FormStartPosition.Manual;
                        login.Location = this.Location;
                        login.Size = this.Size;
                        login.ShowDialog();
                        this.Close();
                    }
                }
                else
                    lblPwdMsg.Text = "Incorrect old password.";
            }
            else
                lblPwdMsg.Text = "Please enter old password.";
        }

        private bool checkPwd()
        {
            if (string.IsNullOrEmpty(tbPass.Text))               //Check whether the password and confirm the password match.
            {
                lblPwdMsg.Text = "Please enter the new password.";
                return false;
            }
            else if (tbPass.Text.Length < 10 || tbPass.Text.Length > 50)
            {
                lblPwdMsg.Text = "New password too short or too long, minimum 10 maximum 50.";
                return false;
            }
            else if (!tbPass.Text.Equals(tbConfirmPass.Text))
            {
                lblPwdMsg.Text = "Confirm password does NOT match.";
                return false;
            }
            return true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            var result = MessageBox.Show("Are you sure to delete your account?\nThis operation also deletes all sub-accounts belonging to this account.\nClick Yes to continue.", "System message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (accountController.delAccount())
                {
                    MessageBox.Show("Account deleted successful.\nThe system will redirect to the login page.", "System message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form login = new Login();
                    this.Hide();
                    //Swap the current form to another.
                    login.StartPosition = FormStartPosition.Manual;
                    login.Location = this.Location;
                    login.Size = this.Size;
                    login.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("System Error! Please Contact The Help Desk.", "System error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Form login = new Login();
                    this.Hide();
                    //Swap the current form to another.
                    login.StartPosition = FormStartPosition.Manual;
                    login.Location = this.Location;
                    login.Size = this.Size;
                    login.ShowDialog();
                    this.Close();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

    }
}
