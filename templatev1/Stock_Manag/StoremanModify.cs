using System;
using controller;
using System.Windows.Forms;

namespace templatev1
{
    public partial class SMStockModify : Form
    {
        private string uName, UID, selectedPartID;
        AccountController accountController;
        stockController stockController;
        UIController UIController;

        public SMStockModify(AccountController accountController, UIController UIController, stockController stockController, string selectedPartID)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            this.stockController = stockController;
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
            timer1.Enabled = true;

            UID = accountController.GetUid();
            uName = accountController.GetName();
            lblUid.Text = "UID: " + UID;
            setIndicator(UIController.getIndicator("Stock Management"));
            palSales.Visible = UIController.store().group1;
            palStock.Visible = UIController.store().group2;


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