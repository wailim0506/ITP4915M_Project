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
    public partial class proFileMain : Form
    {
        private string uName, UID;
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
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {

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
            //controller.UIController.setMode(Properties.Settings.Default.BWmode);
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
        }

        private void btnManagAddress_Click(object sender, EventArgs e)
        {
            Form AddressMgmt = new AddressMgmt(accountController, UIController);
            this.Hide();
            //Swap the current form to another.
            AddressMgmt.StartPosition = FormStartPosition.Manual;
            AddressMgmt.Location = this.Location;
            AddressMgmt.Size = this.Size;
            AddressMgmt.ShowDialog();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

    }
}
