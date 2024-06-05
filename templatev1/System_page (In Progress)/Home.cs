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
    public partial class Home : Form
    {
        private string uName, UID;
        controller.accountController accountController;
        controller.UIController UIController;
        controller.proFileController proFileController;

        public Home()
        {
            InitializeComponent();
        }

        public Home(controller.accountController accountController, controller.UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialization();
        }

        private void Initialization()
        {
            timer1.Enabled = true;

            UID = accountController.getUID();
            uName = accountController.getName();
            lblUid.Text = "UID: " + UID;
            lblWelUser.Text = "Welcome, " + uName + "!";
            lblLastPassChange.Text = accountController.getPwdChange().ToString("yyyy/MM/dd");
            lblLastLogin.Text = accountController.getLog();


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

            //For icon color
            if (Properties.Settings.Default.BWmode == true)
            {
                picBWMode.Image = Properties.Resources.LBWhite;
                picHome.Image = Properties.Resources.homeWhite;
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
        private void getPage(string Function)
        {
            Form next = new Home(accountController, UIController);
            switch (Function)
            {
                //my version
                case "Order Management":
                    next = new Online_Ordering_Platform.customerOrderList(accountController, UIController);
                    break;
                case "Spare Part":
                    next = new Online_Ordering_Platform.sparePartList(accountController, UIController);
                    break;
                case "Cart":
                    next = new Online_Ordering_Platform.cart(accountController, UIController);
                    break;
                case "Favourite":
                    next = new Online_Ordering_Platform.favourite(accountController, UIController);
                    break;
                case "Give Feedback":
                    next = new giveFeedback(accountController, UIController);
                    break;
                //my version
                case "Invoice Management":



                    break;
                case "On-Sale Product Management":



                    break;
                case "Stock Management":
                    next = new StockMgmt(accountController, UIController);
                    break;
                case "User Managemnet":
                    next = new SAccManage(accountController, UIController);
                    break;
            }

            this.Hide();
            next.StartPosition = FormStartPosition.Manual;
            next.Location = this.Location;
            next.Size = this.Size;
            next.ShowDialog();
            this.Close();
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

        //For Dark Color function
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            UIController.setMode(Properties.Settings.Default.BWmode);
            BWMode();
        }
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

        private void btnChangePass_Click(object sender, EventArgs e)
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

        private void btnViewFullRec_Click(object sender, EventArgs e)
        {
            Form LogHis = new LogHis(accountController, UIController);
            this.Hide();
            //Swap the current form to another.
            LogHis.StartPosition = FormStartPosition.Manual;
            LogHis.Location = this.Location;
            LogHis.Size = this.Size;
            LogHis.ShowDialog();
            this.Close();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");

        }

    }
}
