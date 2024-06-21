using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using controller;
using controller.Utilities;

namespace templatev1
{
    public partial class staffViewOrder : Form
    {
        dateHandler dateHandler;
        AccountController accountController;
        UIController UIController;
        staffViewOrderController controller;
        private string uName, UID;
        string orderID;
        string shipDate;
        bool isLMOrder;
        bool isManager;

        public staffViewOrder(string orderID)
        {
            InitializeComponent();
            controller = new staffViewOrderController();
            this.orderID = orderID;
        }

        public staffViewOrder(string orderID, AccountController accountController,
            UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            dateHandler = new dateHandler();
            controller = new staffViewOrderController();
            shipDate = "";
            UID = this.accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isManager = accountController.CheckIsManager();
        }

        private void clerkViewOrder_Load(object sender, EventArgs e)
        {
            if (!isManager)
            {
                hideButton();
            }

            timer1.Enabled = true;
            cmbSortOrder.SelectedIndex = 0;
            lblLoc.Text += $" {orderID}";
            load_data(cmbSortOrder.Text);
        }

        public void load_data(string sortBy)
        {
            pnlSP.Controls.Clear();
            DataTable dt = controller.getOrder(orderID);
            string[] f = dt.Rows[0][4].ToString().Split(' ');

            //order info
            lblOrderID.Text = orderID;
            lblOrderSerialNum.Text = $"{dt.Rows[0][3]}";
            lblOrderDate.Text = f[0];
            lblStaffIncharge.Text = $"{controller.getStaffName(dt.Rows[0][2].ToString())}";
            lblStaffID.Text = $"{controller.getStafftID(dt.Rows[0][2].ToString())}";
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
                lblShippingDate.Text = dateHandler.DayDifference(orderID) >= 0
                    ? $"Scheduled on {shippingDate}"
                    : $"Delivered on {shippingDate}";

                lblExpressNum.Text = dt.Rows[0][4].ToString();
            }

            lblShippingAddress.Text = dt.Rows[0][5].ToString();
            if (lblStatus.Text == "Pending" || lblStatus.Text == "Processing" || lblStatus.Text == "Ready to Ship")
            {
                lblDayUntil.Text = $"{dateHandler.DayDifference(orderID)} day(s) until shipping";
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
                    Name = $"lblRowNum{i}",
                    Text = $"{i.ToString()}.",
                    Location = new Point(3, rowPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(30, 20)
                };
                Label lblItemNum = new Label
                {
                    Name = $"lblItemNum{i}",
                    Text = $"{controller.getItemNum(dt.Rows[i - 1][0].ToString())}",
                    Location = new Point(38, rowPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(83, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPartNum = new Label
                {
                    Name = $"lblPartNum{i}",
                    Text = $"{dt.Rows[i - 1][0]}",
                    Location = new Point(127, rowPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(97, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPartName = new Label
                {
                    Name = $"lblPartName{i}",
                    Text = $"{controller.getPartName(dt.Rows[i - 1][0].ToString())}",
                    Location = new Point(230, rowPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(300, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblQuantity = new Label
                {
                    Name = $"lblQuantity{i}",
                    Text = $"{dt.Rows[i - 1][2]}",
                    Location = new Point(536, rowPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(106, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblUnitPrice = new Label
                {
                    Name = $"lblUnitPrice{i}",
                    Text = $"¥{dt.Rows[i - 1][3]}",
                    Location = new Point(648, rowPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(144, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblRowTotalPrice = new Label
                {
                    Name = $"lblRowTotalPrice{i}",
                    Text = $"¥{dt.Rows[i - 1][4]}",
                    Location = new Point(798, rowPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(114, 20),
                    TextAlign = ContentAlignment.MiddleCenter
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


        private void cmbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbSortOrder.Text);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form o = new staffOrderList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
        }

        private void btnViewInvoice_Click(object sender, EventArgs e)
        {
            if (lblStatus.Text == "Cancelled")
            {
                MessageBox.Show("Order already cancelled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (dateHandler.DayDifference(orderID) >= 0)
                {
                    MessageBox.Show("Invoice can only be view after 1 day of delivery", "View Invoice",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    Form o = new staffViewInvoice(orderID, accountController, UIController, false);
                    Hide();
                    o.StartPosition = FormStartPosition.Manual;
                    o.Location = Location;
                    o.ShowDialog();
                    Close();
                    return;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
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
                if (lblStatus.Text != "Ready to Ship")
                {
                    DialogResult dialogResult =
                        MessageBox.Show(
                            $"Are you sure you want to cancel order {orderID} ?\nYour action cannot be revoked after confirming it.",
                            "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    //add qty back to db
                    //get part num and it's qty in the order
                    Dictionary<string, int> partNumQty = controller.GetPartNumWithQty(orderID);
                    //add back now;
                    //no need now
                    //foreach (KeyValuePair<string, int> q in partNumQty)
                    //{
                    //    controller.addQtyback(q.Key, q.Value, UID);
                    //}

                    if (dialogResult == DialogResult.Yes && controller.DeleteOrder(orderID))
                    {
                        MessageBox.Show("Cancel successful.", " Cancel Successful", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        Form customerOrderList =
                            new staffOrderList(accountController, UIController);
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
                    MessageBox.Show("Order cannot be cancel when it is ready to ship", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDIC_Click(object sender, EventArgs e)
        {
            Form o = new DIC(orderID, accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnDID_Click(object sender, EventArgs e)
        {
            Form o = new DID_List(orderID, accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
        }

        public void hideButton()
        {
            palSelect3.Visible = false;
            btnFunction3.Visible = false;
            palSelect4.Visible = false;
            btnFunction4.Visible = false;
            btnFunction5.Location = new Point(0, 233);
            btnFunction5.Controls.Add(palSelect5);
        }
    }
}