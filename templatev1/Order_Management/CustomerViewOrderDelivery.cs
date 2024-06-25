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
                    DeliverayImage.Load(DeliveryController.GetDeliveryMap(orderID, new Size(944, 548)));
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form feedback = new giveFeedback(accountController, UIController);
            Hide();
            feedback.StartPosition = FormStartPosition.Manual;
            feedback.Location = Location;
            feedback.ShowDialog();
            Close();
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            Form fav = new favourite(accountController, UIController);
            Hide();
            fav.StartPosition = FormStartPosition.Manual;
            fav.Location = Location;
            fav.ShowDialog();
            Close();
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            Form cart = new cart(accountController, UIController);
            Hide();
            cart.StartPosition = FormStartPosition.Manual;
            cart.Location = Location;
            cart.ShowDialog();
            Close();
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            Form spare = new sparePartList(accountController, UIController);
            Hide();
            spare.StartPosition = FormStartPosition.Manual;
            spare.Location = Location;
            spare.ShowDialog();
            Close();
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            Form o = new customerOrderList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {
            Form proFile = new proFileMain();
            Hide();
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

        private void picBWMode_Click(object sender, EventArgs e)
        {
            BWMode();
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