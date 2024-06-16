using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using controller;
using templatev1.Online_Ordering_Platform;
using templatev1.Properties;

namespace templatev1.Order_Management
{
    public partial class customerViewOrder : Form
    {
        AccountController accountController;
        UIController UIController;
        viewOrderController controller;
        private string uName, UID;
        string orderID;
        string shipDate;
        private Boolean isLM;

        public customerViewOrder(string orderID)
        {
            InitializeComponent();
            controller = new viewOrderController();
            this.orderID = orderID;
        }

        public customerViewOrder(string orderID, AccountController accountController,
            UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new viewOrderController();
            shipDate = "";
            UID = this.accountController.GetUid();
            isLM = accountController.GetIsLm();
            lblUid.Text = $"Uid: {UID}";
        }


        private void customerViewOrder_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            cmbSortOrder.SelectedIndex = 0;
            lblLoc.Text += $" {orderID}";
            load_data(cmbSortOrder.Text);
        }

        public void load_data(string sortBy)
        {
            pnlSP.Controls.Clear();
            DataTable dt = controller.GetOrder(orderID);
            string[] f = dt.Rows[0][4].ToString().Split(' ');

            //order info
            lblOrderID.Text = orderID;
            lblOrderSerialNum.Text = $"{dt.Rows[0][3]}";
            lblOrderDate.Text = f[0];
            lblStaffIncharge.Text = $"{controller.GetStaffName(dt.Rows[0][2].ToString())}";
            lblStaffID.Text = $"{viewOrderController.GetStafftId(dt.Rows[0][2].ToString())}";
            lblStaffContact.Text = $"{controller.getStaffContact(dt.Rows[0][2].ToString())}";
            lblStatus.Text = $"{dt.Rows[0][6]}";

            //delivery info
            dt = new DataTable();
            dt = controller.GetShippingDetail(orderID);
            string shippingDate = dt.Rows[0][2].ToString();
            string[]
                d = shippingDate
                    .Split(' '); //since the database also store the time follwing the date, split it so that only date will be display
            shippingDate = d[0];
            shipDate = shippingDate;
            string[] delivermanDetail = controller.GetDelivermanDetail(orderID);
            if (lblStatus.Text == "Cancelled")
            {
                lblDelivermanID.Text = "N/A";
                lblDelivermanName.Text = "N/A";
                lblDelivermanContact.Text = "N/A";
                lblShippingDate.Text = "N/A";
            
                lblExpressNum.Text = "N/A";
            }
            else
            {
                lblDelivermanID.Text = dt.Rows[0][1].ToString();
                lblDelivermanName.Text = $"{delivermanDetail[0]} {delivermanDetail[1]}";
                lblDelivermanContact.Text = delivermanDetail[2];
                lblShippingDate.Text = dayDifference(orderID) >= 0
                    ? $"Scheduled on {shippingDate}"
                    : $"Delivered on {shippingDate}";
            
                lblExpressNum.Text = dt.Rows[0][4].ToString();
            }
            
            lblShippingAddress.Text = dt.Rows[0][5].ToString();
            if (lblStatus.Text == "Pending" || lblStatus.Text == "Processing")
            {
                lblDayUntil.Text = $"{dayDifference(orderID)} day(s) until shipping";
            }
            else
            {
                lblDayUntil.Text = "N/A";
            }

            //ordered spare part
            dt = new DataTable();
            dt = controller.getOrderedSparePart(orderID, sortBy);
            int row = dt.Rows.Count;


            int rowPosition = 8;
            int orderTotalPrice = 0;
            for (int i = 1; i <= row; i++)
            {
                Label lblRowNum = new Label
                {
                    Name = $"lblRowNum{i}", Text = $"{i.ToString()}.",
                    Location = new Point(3, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    TextAlign = ContentAlignment.MiddleCenter, Size = new Size(30, 20)
                };
                Label lblItemNum = new Label
                {
                    Name = $"lblItemNum{i}", Text = $"{controller.GetItemNum(dt.Rows[i - 1][0].ToString())}",
                    Location = new Point(38, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(83, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPartNum = new Label
                {
                    Name = $"lblPartNum{i}", Text = $"{dt.Rows[i - 1][0]}",
                    Location = new Point(127, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(97, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPartName = new Label
                {
                    Name = $"lblPartName{i}", Text = $"{controller.GetPartName(dt.Rows[i - 1][0].ToString())}",
                    Location = new Point(230, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(300, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblQuantity = new Label
                {
                    Name = $"lblQuantity{i}", Text = $"{dt.Rows[i - 1][2]}",
                    Location = new Point(536, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(106, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblUnitPrice = new Label
                {
                    Name = $"lblUnitPrice{i}", Text = $"¥{dt.Rows[i - 1][3]}",
                    Location = new Point(648, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(144, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblRowTotalPrice = new Label
                {
                    Name = $"lblRowTotalPrice{i}", Text = $"¥{dt.Rows[i - 1][4]}",
                    Location = new Point(798, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(114, 20), TextAlign = ContentAlignment.MiddleCenter
                };


                rowPosition += 50;
                orderTotalPrice += (int.Parse(dt.Rows[i - 1][2].ToString()) * int.Parse(dt.Rows[i - 1][3].ToString()));
                lblOrderTotalPrice.Text = $"¥ {orderTotalPrice.ToString()}";

                pnlSP.Controls.Add(lblRowNum);
                pnlSP.Controls.Add(lblItemNum);
                pnlSP.Controls.Add(lblPartNum);
                pnlSP.Controls.Add(lblPartName);
                pnlSP.Controls.Add(lblQuantity);
                pnlSP.Controls.Add(lblUnitPrice);
                pnlSP.Controls.Add(lblRowTotalPrice);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lblStatus.Text == "Cancelled" || lblStatus.Text == "Shipped")
            {
                MessageBox.Show(lblStatus.Text == "Cancelled" ? "Order already cancelled." : "Order already finish.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (dayDifference(orderID) >= 3)
                {
                    Form customerEditOrder = new customerEditOrder(orderID, accountController, UIController);
                    Hide();
                    customerEditOrder.StartPosition = FormStartPosition.Manual;
                    customerEditOrder.Location = Location;
                    customerEditOrder.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Order cannot be edited three day before the shipping date", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lblStatus.Text == "Cancelled" || lblStatus.Text == "Shipped")
            {
                MessageBox.Show(
                    lblStatus.Text == "Cancelled" ? "Order already cancelled." : "Order already finish.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (dayDifference(orderID) >= 3)
                {
                    DialogResult dialogResult =
                        MessageBox.Show(
                            $"Are you sure you want to cancel order {orderID} ?\nYour action cannot be revoked after confirming it.",
                            "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    //add qty back to db
                    //get part num and it's qty in the order
                    Dictionary<string, int> partNumQty = controller.GetPartNumWithQty(orderID);
                    //add back now;
                    foreach (KeyValuePair<string, int> q in partNumQty)
                    {
                        controller.addQtyback(q.Key, q.Value, UID);
                    }

                    if (dialogResult == DialogResult.Yes && controller.DeleteOrder(orderID))
                    {
                        MessageBox.Show("Cancel successful.", " Cancel Successful", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        Form customerOrderList =
                            new customerOrderList(accountController, UIController);
                        Hide();
                        customerOrderList.StartPosition = FormStartPosition.Manual;
                        customerOrderList.Location = Location;
                        customerOrderList.ShowDialog();
                        Close();
                    }
                    else if (dialogResult == DialogResult.Yes && !controller.DeleteOrder(orderID))
                    {
                        MessageBox.Show("Something went wrong.\nPlease contact our staff for help", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Order cannot be cancel three day before the shipping date", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form customerOrderList = new customerOrderList(accountController, UIController);
            Hide();
            customerOrderList.StartPosition = FormStartPosition.Manual;
            customerOrderList.Location = Location;
            customerOrderList.ShowDialog();
            Close();
        }

        private void btnViewInvoice_Click(object sender, EventArgs e)
        {
            if (lblStatus.Text == "Cancelled")
            {
                MessageBox.Show("Order already cancelled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (dayDifference(orderID) >= 0)
                {
                    MessageBox.Show("Invoice can only be view after 1 day of delivery", "View Invoice",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    Form customerViewInvoice = new customerViewInvoice(orderID, accountController, UIController);
                    Hide();
                    customerViewInvoice.StartPosition = FormStartPosition.Manual;
                    customerViewInvoice.Location = Location;
                    customerViewInvoice.ShowDialog();
                    Close();
                }
            }
        }


        private int dayDifference(string orderID) //calculate day difference
        {
            string
                systemFormat = systemDateFormat(); //the date format got from db depend on the operation system setting
            string[] splitSystemFormat = systemFormat.Split('/');

            Boolean monthFirst;

            if (splitSystemFormat[0] == "M" || splitSystemFormat[0] == "MM")
            {
                monthFirst = true;
            }
            else
            {
                monthFirst = false;
            }


            var dt = controller.GetShippingDetail(orderID);
            string shippingDate = dt.Rows[0][2].ToString();
            string[]
                d = shippingDate
                    .Split(' '); //since the database also store the time follwing the date, split it so that only date will be display
            shippingDate = d[0];
            string shipDate = shippingDate; //   d/M/yyyy


            string sysYear = DateTime.Now.ToString("yyyy"); //today year 
            string sysMonth = DateTime.Now.ToString("MM"); //today month
            string sysDay = DateTime.Now.ToString("dd"); //today month

            string[] splitShipDate = shipDate.Split('/');

            string shipMonth, shipDay, shipYear;
            if (monthFirst)
            {
                shipMonth = splitShipDate[0];
                shipDay = splitShipDate[1];
                shipYear = splitShipDate[2];
            }
            else
            {
                shipMonth = splitShipDate[1];
                shipDay = splitShipDate[0];
                shipYear = splitShipDate[2];
            }

            if (monthFirst && splitSystemFormat[0] == "M" && int.Parse(shipMonth) < 10)
            {
                shipMonth = $"0{shipMonth}";
            }
            else if (monthFirst && splitSystemFormat[0] == "MM" && int.Parse(shipMonth) < 10)
            {
                shipMonth = $"{shipMonth}";
            }
            else if (!monthFirst && splitSystemFormat[1] == "M" && int.Parse(shipMonth) < 10)
            {
                shipMonth = $"0{shipMonth}";
            }
            else if (!monthFirst && splitSystemFormat[1] == "MM" && int.Parse(shipMonth) < 10)
            {
                shipMonth = $"{shipMonth}";
            }

            if (monthFirst && splitSystemFormat[1] == "d" && int.Parse(shipDay) < 10)
            {
                shipDay = $"0{shipDay}";
            }
            else if (monthFirst && splitSystemFormat[1] == "dd" && int.Parse(shipDay) < 10)
            {
                shipDay = $"{shipDay}";
            }
            else if (!monthFirst && splitSystemFormat[0] == "d" && int.Parse(shipDay) < 10)
            {
                shipDay = $"0{shipDay}";
            }
            else if (!monthFirst && splitSystemFormat[0] == "dd" && int.Parse(shipDay) < 10)
            {
                shipDay = $"{shipDay}";
            }


            string formatedShippingDate = $"{shipDay}/{shipMonth}/{shipYear}";
            string formatedSysDate = $"{sysDay}/{sysMonth}/{sysYear}";

            DateTime parsedFormatedShippingDate;
            DateTime parsedFormatedSysDate;

            try
            {
                parsedFormatedShippingDate = DateTime.ParseExact(formatedShippingDate, "dd/MM/yyyy", null);
            }
            catch (Exception e)
            {
                parsedFormatedShippingDate = DateTime.ParseExact(formatedShippingDate, "MM/dd/yyyy", null);
            }
            //MessageBox.Show(parsedFormatedShippingDate.ToString());

            try
            {
                parsedFormatedSysDate = DateTime.ParseExact(formatedSysDate, "dd/MM/yyyy", null);
            }
            catch (Exception e)
            {
                parsedFormatedSysDate = DateTime.ParseExact(formatedSysDate, "MM/dd/yyyy", null);
            }


            TimeSpan difference = parsedFormatedShippingDate - parsedFormatedSysDate;

            string[] f = difference.ToString().Split('.');
            return int.Parse(f[0]);
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

        private string systemDateFormat()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string dateFormat = dtfi.ShortDatePattern;
            return dateFormat;
        }

        private void btnReorder_Click(object sender, EventArgs e)
        {
            //get all part num and qty in the order first
            Dictionary<string, int> partNumQty = controller.GetPartNumWithQty(orderID);
            //add to cart
            try
            {
                foreach (var k in partNumQty)
                {
                    if (k.Value <= controller.checkOnSaleQty(k.Key))
                    {
                        controller.reOrder(UID, k.Key, k.Value, isLM);
                    }
                    else
                    {
                        DialogResult dialogResult2 =
                            MessageBox.Show($"We dont have enough quantity for {k.Key}.\nContinue to add other item?",
                                "Re-order", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (dialogResult2 == DialogResult.Yes)
                        {
                            continue;
                        }

                        DialogResult dialogResult3 = MessageBox.Show("Clear item in cart?", "Re-order",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (dialogResult3 == DialogResult.Yes)
                        {
                            List<string> allPartNum = controller.GetAllPartNumInCart(UID);
                            List<int> allItemQty = controller.GetAllItemQtyInCart(UID);
                            for (int i = 0; i < allPartNum.Count; i++)
                            {
                                controller.AddQtyBack(allPartNum[i], allItemQty[i], 0, isLM); //add qty back to db
                            }

                            if (controller.RemoveAll(UID)) //remove from cart
                            {
                                MessageBox.Show("All items removed from cart", "Remove All", MessageBoxButtons.OK);
                            }

                            return;
                        }

                        return;
                    }
                }

                DialogResult dialogResult =
                    MessageBox.Show("All available item in this order added to cart.\nProceed to cart to create order?",
                        "Re-order", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Form cart = new cart(accountController, UIController);
                    Hide();
                    cart.StartPosition = FormStartPosition.Manual;
                    cart.Location = Location;
                    cart.ShowDialog();
                    Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please try again.", "Re-order", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbSortOrder.Text);
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