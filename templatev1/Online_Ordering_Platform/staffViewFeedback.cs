using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using controller;
using controller.Utilities;

namespace templatev1
{
    public partial class staffViewFeedback : Form
    {
        private string uName, UID;
        AccountController accountController;
        UIController UIController;
        feedbackController controller;
        string feedbackID;
        public staffViewFeedback()
        {
            InitializeComponent();
        }

        public staffViewFeedback(AccountController accountController, UIController UIController, string feedbackID)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new feedbackController();
            this.feedbackID = feedbackID;
            UID = accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
        }

        private void staffViewFeedback_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_data();
            palSelect1.Visible =
               palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Order Management"));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        public void load_data()
        {
            DataTable dt = controller.getFeedbackDetail(feedbackID);
            string feedbackDate = dt.Rows[0][4].ToString();
            string[]
                d = feedbackDate
                    .Split(' '); //since the database also store the time follwing the date, split it so that only date will be disp;ay
            feedbackDate = d[0];

            string orderID = "N/A";

            if (dt.Rows[0][2].ToString() == "")
            {
                orderID = "N/A";
            }
            else
            {
                orderID = dt.Rows[0][2].ToString();
            }

            lblFeedbackID.Text += $" {feedbackID}";
            lblCustomerID.Text = $" {dt.Rows[0][1]}";
            lblDate.Text = $" {feedbackDate}";
            lblOrderID.Text = $" {orderID}";
            lblContent.Text = $" {dt.Rows[0][3]}";
        }

        public void hideButton()
        {
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
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form o = new staffFeedbackList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            Form o =
                new staffOrderList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            Form o =
                new staffInvoiceList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
        }

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            Form proFile = new SAccManage(accountController, UIController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.ShowDialog();
            Close();
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {
            proFileController proFileController = new proFileController(accountController);

            proFileController.setType(accountController.GetAccountType());

            Form proFile = new proFileMain(accountController, UIController, proFileController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.ShowDialog();
            Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form o = new Login();
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Form home = new Home(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.ShowDialog();
            Close();
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            Form home = new StockMgmt(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.ShowDialog();
            Close();
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            Form home = new OnSaleMain(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.ShowDialog();
            Close();
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


    }
}
