﻿using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using LMCIS.controller;
using LMCIS.controller.Utilities;
using LMCIS.On_Sale_Product_Manag;
using LMCIS.Order_Management;
using LMCIS.Profile;
using LMCIS.Properties;
using LMCIS.Stock_Manag;
using LMCIS.System_page;
using LMCIS.User_Manag;

namespace LMCIS.Online_Ordering_Platform
{
    public partial class AddPartToExistingOrder : Form
    {
        private string uName, UID;
        AccountController accountController;
        UIController UIController;
        addPartToOrderController controller;
        dateHandler dateHandler;
        private string partNum, qty;
        private Boolean isLM;

        public AddPartToExistingOrder()
        {
            InitializeComponent();
        }


        public AddPartToExistingOrder(string partNum, string qty, AccountController accountController,
            UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new addPartToOrderController();
            dateHandler = new dateHandler();
            this.partNum = partNum;
            this.qty = qty;
            UID = accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isLM = accountController.GetIsLm();
        }

        private void AddPartToExistingOrder_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_data();
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Spare Part"));
        }

        private void load_data()
        {
            DataTable dt = controller.GetPartDetail(partNum);
            lblPartName.Text = dt.Rows[0][0].ToString();
            lblPartNum.Text = dt.Rows[0][1].ToString();
            lblCategory.Text = $"{dt.Rows[0][2]} - {dt.Rows[0][3]}";
            lblSupplier.Text = dt.Rows[0][4].ToString();
            lblCountry.Text = dt.Rows[0][5].ToString();
            lblPrice.Text = dt.Rows[0][6].ToString();
            lblOnSaleQty.Text = isLM ? dt.Rows[0][8].ToString() : dt.Rows[0][7].ToString();
            picSpare.Image = imageString(partNum);

            dt = controller.GetEditableOrderId(UID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbOrderSelection.Items.Add(dt.Rows[i][0].ToString());
            }

            tbQty.Text = qty;
            lblLoc.Text = $"Add {lblPartName.Text} to Order";
        }

        private void cmbOrderSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderID = cmbOrderSelection.Text;
            string shippingDateTime = controller.GetShippingDate(orderID);
            string[] shippingDate = shippingDateTime.Split(' ');
            lblShippingDate.Text = shippingDate[0];
            lblOrderStatus.Text = controller.GetOrderStatus(orderID);
            lblDayUntilDelivery.Text = $"{dateHandler.DayDifference(orderID)} day(s) until shipping.";
            lblLoc.Text = $"Add {lblPartName.Text} to Order {orderID}";
        }


        private void btnAddQty_Click(object sender, EventArgs e)
        {
            if (tbQty.Text != "") //check have quantity input
            {
                if (int.Parse(tbQty.Text) < int.Parse(lblOnSaleQty.Text))
                {
                    int qty = int.Parse(tbQty.Text);
                    qty++;
                    tbQty.Text = qty.ToString();
                }
            }
            else
            {
                int qty = 0;
                qty++;
                tbQty.Text = qty.ToString();
            }
        }

        private void btnMinusQty_Click(object sender, EventArgs e)
        {
            if (tbQty.Text == "") return; //check have quantity input
            if (int.Parse(tbQty.Text) ==
                1) //check quantity input equal 0, do not perform anything if equal to 0
            {
                return;
            }

            int qty = int.Parse(tbQty.Text);
            qty--;
            tbQty.Text = qty.ToString();
        }

        private void btnBackViewPart_Click(object sender, EventArgs e)
        {
            Form c = new viewSparePart(partNum, accountController, UIController);
            Hide();
            c.StartPosition = FormStartPosition.Manual;
            c.Location = Location;
            c.ShowDialog();
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

        private string SystemDateFormat()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string dateFormat = dtfi.ShortDatePattern;
            return dateFormat;
        }


        private static Image imageString(string imageName)
        {
            PropertyInfo property =
                typeof(Resources).GetProperty(imageName, BindingFlags.NonPublic | BindingFlags.Static);
            return property?.GetValue(null, null) as Image;
        }


        private void lblAddToOrder_Click(object sender, EventArgs e)
        {
            if (tbQty.Text != "")
            {
                if (int.Parse(tbQty.Text) <= 0)
                {
                    MessageBox.Show("Quantity is invalid.", "Add to Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (int.Parse(tbQty.Text) > int.Parse(lblOnSaleQty.Text))
                    {
                        //check quantity input is larger than on sales quantity
                        MessageBox.Show(
                            $"Quantity input cannot exceed On Sales Quantity ({lblOnSaleQty.Text})",
                            "Add to Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        load_data();
                        return;
                    }

                    int qty = int.Parse(tbQty.Text);
                    if (controller.addToOrder(cmbOrderSelection.Text, lblPartNum.Text, int.Parse(tbQty.Text),
                            int.Parse(lblPrice.Text), isLM))
                    {
                        MessageBox.Show($"{qty} {lblPartName.Text} has been added to order {cmbOrderSelection.Text}.",
                            "Add to Order");
                        load_data();
                        tbQty.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Please try again.", "Add to Order", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please input the quantity.", "Add to Order", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void tbQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            getPage(btnFunction1.Text);
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction2.Text);
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

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
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

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            getPage(btnFunction3.Text);
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            getPage(btnFunction4.Text);
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
    }
}