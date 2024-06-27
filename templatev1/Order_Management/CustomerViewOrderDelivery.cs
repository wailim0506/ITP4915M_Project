using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using controller;
using controller.Utilities;
using Microsoft.Extensions.Logging;
using templatev1.Properties;

namespace templatev1
{
    public partial class CustomerViewOrderDelivery : Form
    {
        dateHandler dateHandler;
        AccountController accountController;
        UIController UIController;
        DeliveryController DeliveryController;
        viewOrderController ViewController;
        private string uName, UID;
        string orderID;
        string shipDate;
        private Boolean isLM;

        public CustomerViewOrderDelivery(string orderID)
        {
            InitializeComponent();
            DeliveryController = new DeliveryController();
            this.orderID = orderID;
        }

        public CustomerViewOrderDelivery(string orderID, AccountController accountController, UIController UIController,
            viewOrderController ViewController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            dateHandler = new dateHandler();
            DeliveryController = new DeliveryController();
            this.ViewController = ViewController;
            shipDate = "";
            UID = this.accountController.GetUid();
            isLM = accountController.GetIsLm();
            lblUid.Text = $"Uid: {UID}";
            setIndicator(UIController.getIndicator("Order Management"));
        }

        private void CustomerViewOrderDelivery_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            lblLoc.Text += $" {orderID}";

            load_data();
        }

        private void load_data()
        {
            DataTable dt = ViewController.GetOrder(orderID);
            string[] f = dt.Rows[0][4].ToString().Split(' ');

            //order info
            lblOrderID.Text = orderID;

            //delivery info
            dt = new DataTable();
            dt = ViewController.GetShippingDetail(orderID);
            string shippingDate = dt.Rows[0][2].ToString();
            string[]
                d = shippingDate
                    .Split(' '); //since the database also stores the time following the date, split it so that only date will be display
            shippingDate = d[0];
            shipDate = shippingDate;
            string[] delivermanDetail = ViewController.GetDelivermanDetail(orderID);
            lblShippingAddress.Text =
                DeliveryController.GetShippingAddress(orderID).Replace("+", " ").Replace(" , ", ",");


            if (ViewController.GetStatus(orderID) == "Cancelled")
            {
                lblDelivermanID.Text = "N/A";
                lblDelivermanName.Text = "N/A";
                lblDelivermanContact.Text = "N/A";
            }
            else
            {
                lblDelivermanID.Text = dt.Rows[0][1].ToString();
                lblDelivermanName.Text = $"{delivermanDetail[0]} {delivermanDetail[1]}";
                lblDelivermanContact.Text = delivermanDetail[2];
                if (DeliveryController.GetDeliveryMap(orderID, new Size(944, 548)) != "Cancelled")
                {
                    Log.LogMessage(LogLevel.Information, "Delivery Controller",
                        DeliveryController.GetDeliveryMap(orderID, new Size(944, 548)));
                    DeliverayImage.Load(DeliveryController.GetDeliveryMap(orderID, DeliverayImage.Size));
                }
                else
                {
                    DeliverayImage.Image = null;
                    DeliverayImage.Visible = false;
                    Label noAvailable = new Label();
                    noAvailable.Text = "No available delivery map";
                    noAvailable.Location = new Point(10, 10);
                    noAvailable.Size = new Size(300, 100);
                    noAvailable.Font = new Font("Arial", 10);
                    noAvailable.ForeColor = Color.Red;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form customerOrderList = new customerViewOrder(orderID, accountController, UIController);
            Hide();
            customerOrderList.StartPosition = FormStartPosition.Manual;
            customerOrderList.Location = Location;
            customerOrderList.ShowDialog();
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

        private void getPage(string Function)
        {
            Form next = new Home(accountController, UIController);
            switch (Function)
            {
                case "Order Management":
                    if (UID.StartsWith("LMC"))
                    {
                        next = new customerOrderList(accountController, UIController);
                    }
                    else if (accountController.CheckIsDeliverman())
                    {
                        next = new deliverman(accountController, UIController);
                    }
                    else
                    {
                        next = new staffOrderList(accountController, UIController);
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

                case "On-Sale Product Management":
                    next = new OnSaleMain(accountController, UIController);
                    break;
                case "Stock Management":
                    next = new StockMgmt(accountController, UIController);
                    break;
                case "User Management":
                    next = new SAccManage(accountController, UIController);
                    break;
                case "Invoice Management":
                    next = new staffInvoiceList(accountController, UIController);
                    break;
            }

            Hide();
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.ShowDialog();
            Close();
        }


        private void btnFunction1_Click(object sender, EventArgs e)
        {
            getPage(btnFunction1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            getPage(btnFunction4.Text);
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            getPage(btnFunction3.Text);
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction2.Text);
        }

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
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

        private void BWMode()
        {
            dynamic value = UIController.getMode();
            Settings.Default.textColor = ColorTranslator.FromHtml(value.textColor);
            Settings.Default.bgColor = ColorTranslator.FromHtml(value.bgColor);
            Settings.Default.navBarColor = ColorTranslator.FromHtml(value.navBarColor);
            Settings.Default.navColor = ColorTranslator.FromHtml(value.navColor);
            Settings.Default.timeColor = ColorTranslator.FromHtml(value.timeColor);
            Settings.Default.locTbColor = ColorTranslator.FromHtml(value.locTbColor);
            Settings.Default.logoutColor = ColorTranslator.FromHtml(value.logoutColor);
            Settings.Default.profileColor = ColorTranslator.FromHtml(value.profileColor);
            Settings.Default.btnColor = ColorTranslator.FromHtml(value.btnColor);
            Settings.Default.BWmode = value.BWmode;
            if (Settings.Default.BWmode)
            {
                picBWMode.Image = Resources.LBWhite;
                picHome.Image = Resources.homeWhite;
            }
            else
            {
                picBWMode.Image = Resources.LB;
                picHome.Image = Resources.home;
            }
        }
    }
}