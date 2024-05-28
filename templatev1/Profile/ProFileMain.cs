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

        
        

        public proFileMain()
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

            Initialization();

            //Get UID and Name
            //UID = controller.accountController.getUID();
            //uName = controller.accountController.getName();
            lblUid.Text = "UID: " + UID;
            lblUserUID.Text = UID;
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {

        }

        private void lblCorpName_Click(object sender, EventArgs e)
        {
            Form about = new About();
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
            dynamic info = controller.proFileController.getUserInfo();
            lblAccType.Text = info.accountType;
            lblJobTitle.Text = info.jobTitle;
            lblDept.Text = info.dept;
            lblEmail.Text = info.email;
            tbFirstName.Text = info.fName;
            tbLastName.Text = info.lName;
            cmbGender.Text = info.sex;
            tbPhone.Text = info.phone;
            dtpDateOfBirth.Value = DateTime.ParseExact((info.dateOfBirth).ToString("dd/MM/yyyy"), "dd/MM/yyyy", null); 
            lblCreateDate.Text = (info.createDate).ToString("dd/MM/yyyy");



            /*For determine which button needs to be shown.
            dynamic btnFun = controller.UIController.showFun();
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
            */

            //For swap the form betwee staff and customer
            dynamic show = controller.UIController.proFile();
            lblTitJobTitle.Visible = show.TitJobTitel;
            lblJobTitle.Visible = show.JobTitel;
            lblTitDept.Visible = show.TitDept;
            lblDept.Visible = show.Dept;
            chkNGDateOfBirth.Visible = show.NGDateOfBirth;
            lblTitPayment.Visible = show.TitPayment;
            cmbPayment.Visible = show.Payment;
            lblTitAddress.Visible = show.TitAddress;
            lblAddress.Visible = show.Address;
            btnManagAddress.Visible = show.ManagAddress;


        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Form home = new Home();
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
            /*
            dynamic value = controller.UIController.getMode();
            Properties.Settings.Default.textColor = ColorTranslator.FromHtml(value.textColor);
            Properties.Settings.Default.bgColor = ColorTranslator.FromHtml(value.bgColor);
            Properties.Settings.Default.navBarColor = ColorTranslator.FromHtml(value.navBarColor);
            Properties.Settings.Default.navColor = ColorTranslator.FromHtml(value.navColor);
            Properties.Settings.Default.timeColor = ColorTranslator.FromHtml(value.timeColor);
            Properties.Settings.Default.locTbColor = ColorTranslator.FromHtml(value.locTbColor);
            Properties.Settings.Default.logoutColor = ColorTranslator.FromHtml(value.logoutColor);
            Properties.Settings.Default.profileColor = ColorTranslator.FromHtml(value.profileColor);
            Properties.Settings.Default.BWmode = value.BWmode;
            */
        }

    }
}
