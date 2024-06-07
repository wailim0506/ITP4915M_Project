using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1.Order_Management
{
    public partial class customerEditOrder : Form
    {
        int index = 0;
        controller.accountController accountController;
        controller.UIController UIController;
        controller.editOrderController controller;
        private string uName, UID;
        string orderID;
        private Boolean isLM;

        public customerEditOrder()
        {
            InitializeComponent();
            controller = new controller.editOrderController();
        }

        public customerEditOrder(string orderID, controller.accountController accountController,
            controller.UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            this.controller = new controller.editOrderController();
            //UID = this.accountController.getUID();

            UID = "LMC00001"; //hard code for testing
            //UID = "LMC00003"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
            lblLoc.Text += $" {orderID}";
            isLM = accountController.getIsLM();
        }

        private void customerEditOrder_Load(object sender, EventArgs e)
        {
            cmbSortOrder.SelectedIndex = 0;
            loadData(cmbSortOrder.Text.ToString());
        }

        public void picPencil_Click(object sender, EventArgs e)
        {
            PictureBox clickedPencil = sender as PictureBox;

            if (clickedPencil != null)
            {
                string picPencilName = clickedPencil.Name;
                index = getIndex(picPencilName);
                if (index != -1)
                {
                    int i = 0;

                    foreach (Control control in pnlSP.Controls)
                    {
                        if (control.Name == $"lblPartNum{index}")
                        {
                            string partToEdit = control.Text;
                            showEdit(partToEdit);
                            clickedPencil.Image = Properties.Resources.bin;
                            clickedPencil.Click += new EventHandler(this.bin_click);
                            return;
                        }

                        ++i;
                    }
                }
            }
        }

        public void bin_click(object sender, EventArgs e) //delete spare part
        {
            string partToDelete = ""; //part num
            foreach (Control control in pnlSP.Controls)
            {
                if (control.Name == $"lblPartNum{index}")
                {
                    partToDelete = control.Text;
                }
            }

            //get quantity of the spare part in the order first
            int qtyInOrderNow = controller.getPartQtyInOrder(partToDelete, orderID);
            DialogResult dialogResult = MessageBox.Show(
                $"Are you sure you want to remove {controller.getPartName(partToDelete)} from your order?\nYour action cannot be revoked after confirming it.",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes && controller.deleteSparePart(orderID, partToDelete))
            {
                //add qty back to db
                controller.addQtyBack(partToDelete, qtyInOrderNow, 0, isLM);
                MessageBox.Show("Delete successful.", " Delete Successful", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Form customerEditOrder = new customerEditOrder(orderID, accountController, UIController);
                loadData(cmbSortOrder.Text.ToString());
            }
            else if (dialogResult == DialogResult.Yes && controller.deleteSparePart(orderID, partToDelete) == false)
            {
                MessageBox.Show("Something went wrong.\nPlease contact our staff for help", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Form customerEditOrder = new customerEditOrder(orderID, accountController, UIController);
                loadData(cmbSortOrder.Text.ToString());
            }
        }

        public void showEdit(string id) //part Number
        {
            lblEditQuantity.Visible = true;
            lblEditQuantity.Text = $"Edit {id} quantity :";
            tbQauntity.Visible = true;
            tbQauntity.Name = id;
            picTick.Visible = true;
        }

        private int getIndex(string picPencilName)
        {
            int i = 1;
            while (true)
            {
                if (picPencilName == $"picPencil{i}")
                {
                    return i;
                }

                i++;
            }

            int x = -1;
            return x;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form customerViewOrder = new customerViewOrder(orderID, accountController, UIController);
            this.Hide();
            customerViewOrder.StartPosition = FormStartPosition.Manual;
            customerViewOrder.Location = this.Location;
            customerViewOrder.ShowDialog();
            this.Close();
            return;
        }

        private void tbQauntity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void picTick_Click(object sender, EventArgs e)
        {
            string partToUpdate = tbQauntity.Name; //part number
            string quantity = tbQauntity.Text; //quantity
            int qtyInOrderNow; //qty in the order before edit

            if (quantity == "")
            {
                MessageBox.Show("Quantity cannot be empty.", "Invalid Quantity", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (int.Parse(quantity) < 0)
            {
                MessageBox.Show("Quantity cannot be negative.", "Invalid Quantity", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (int.Parse(quantity) == 0)
            {
                MessageBox.Show("Quantity cannot be zero.", "Invalid Quantity", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                // add qty back to db first
                //get quantity of the spare part in the order first
                qtyInOrderNow = controller.getPartQtyInOrder(partToUpdate, orderID);
                //add back to db
                try
                {
                    controller.addQtyBack(partToUpdate, qtyInOrderNow, int.Parse(quantity), isLM);
                }
                catch (Exception)
                {
                    MessageBox.Show("Sorry, we dont have enough spare part\nPlease try adjusting the quantity",
                        "Edit Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //deduct db qty after adding back order qty to db
                if (controller.editDbQty(partToUpdate, int.Parse(quantity), isLM))
                {
                    //edit order line qty
                    if (controller.editOrderLineQuantity(orderID, partToUpdate, quantity))
                    {
                        MessageBox.Show("Edit successful.", " Edit Successful", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        loadData(cmbSortOrder.Text.ToString());
                        lblEditQuantity.Visible = false;
                        tbQauntity.Visible = false;
                        picTick.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong.\nPlease contact our staff for help", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Something went wrong.\nPlease contact our staff for help", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void picRefresh_Click(object sender, EventArgs e)
        {
            loadData(cmbSortOrder.Text.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form o = new giveFeedback(accountController, UIController);
            this.Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = this.Location;
            o.ShowDialog();
            this.Close();
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            Form o = new Online_Ordering_Platform.favourite(accountController, UIController);
            this.Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = this.Location;
            o.ShowDialog();
            this.Close();
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            Form o = new Online_Ordering_Platform.cart(accountController, UIController);
            this.Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = this.Location;
            o.ShowDialog();
            this.Close();
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            Form o = new Online_Ordering_Platform.sparePartList(accountController, UIController);
            this.Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = this.Location;
            o.ShowDialog();
            this.Close();
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            Form o = new Online_Ordering_Platform.customerOrderList(accountController, UIController);
            this.Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = this.Location;
            o.ShowDialog();
            this.Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form o = new Login();
            this.Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = this.Location;
            o.ShowDialog();
            this.Close();
        }

        private void picBWMode_Click(object sender, EventArgs e)
        {
            BWMode();
        }

        public void loadData(string sortBy)
        {
            pnlSP.Controls.Clear();
            //ordered spare part
            DataTable dt;
            dt = new DataTable();
            dt = controller.getOrderedSparePart(orderID, sortBy);
            int row = dt.Rows.Count;

            if (row == 0) //all spare part is removed, the order can be delete
            {
                controller.deleteOrder(orderID);
                MessageBox.Show("Since no spare part exist in this order, this order is deleted");
                Form orderList = new Online_Ordering_Platform.customerOrderList(accountController, UIController);
                this.Hide();
                orderList.StartPosition = FormStartPosition.Manual;
                orderList.Location = this.Location;
                orderList.ShowDialog();
                this.Close();
                return;
            }


            int rowPosition = 8;
            int orderTotalPrice = 0;
            for (int i = 1; i <= row; i++)
            {
                Label lblRowNum = new Label()
                {
                    Name = $"lblRowNum{i}", Text = $"{i.ToString()}.",
                    Location = new System.Drawing.Point(3, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    TextAlign = ContentAlignment.MiddleCenter, Size = new System.Drawing.Size(30, 20)
                };
                Label lblItemNum = new Label()
                {
                    Name = $"lblItemNum{i}", Text = $"{controller.getItemNum(dt.Rows[i - 1][0].ToString())}",
                    Location = new System.Drawing.Point(35, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new System.Drawing.Size(83, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPartNum = new Label()
                {
                    Name = $"lblPartNum{i}", Text = $"{dt.Rows[i - 1][0].ToString()}",
                    Location = new System.Drawing.Point(124, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new System.Drawing.Size(97, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPartName = new Label()
                {
                    Name = $"lblPartName{i}", Text = $"{controller.getPartName(dt.Rows[i - 1][0].ToString())}",
                    Location = new System.Drawing.Point(227, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new System.Drawing.Size(300, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblQuantity = new Label()
                {
                    Name = $"lblQuantity{i}", Text = $"{dt.Rows[i - 1][2].ToString()}",
                    Location = new System.Drawing.Point(533, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new System.Drawing.Size(106, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblUnitPrice = new Label()
                {
                    Name = $"lblUnitPrice{i}", Text = $"¥{dt.Rows[i - 1][3].ToString()}",
                    Location = new System.Drawing.Point(645, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new System.Drawing.Size(144, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblRowTotalPrice = new Label()
                {
                    Name = $"lblRowTotalPrice{i}",
                    Text =
                        $"¥{(int.Parse(dt.Rows[i - 1][2].ToString()) * int.Parse(dt.Rows[i - 1][3].ToString())).ToString()}",
                    Location = new System.Drawing.Point(795, rowPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new System.Drawing.Size(114, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                PictureBox picPencil = new PictureBox()
                {
                    Name = $"picPencil{i}", SizeMode = PictureBoxSizeMode.Zoom, Size = new Size(23, 29),
                    Location = new Point(907, rowPosition - 5), Image = Properties.Resources.pencil,
                    Cursor = Cursors.Hand
                };

                picPencil.Click += new EventHandler(this.picPencil_Click);
                rowPosition += 50;
                orderTotalPrice += (int.Parse(dt.Rows[i - 1][2].ToString()) * int.Parse(dt.Rows[i - 1][3].ToString()));

                pnlSP.Controls.Add(lblRowNum);
                pnlSP.Controls.Add(lblItemNum);
                pnlSP.Controls.Add(lblPartNum);
                pnlSP.Controls.Add(lblPartName);
                pnlSP.Controls.Add(lblQuantity);
                pnlSP.Controls.Add(lblUnitPrice);
                pnlSP.Controls.Add(lblRowTotalPrice);
                pnlSP.Controls.Add(picPencil);
            }

            //delivery info
            dt = new DataTable();
            dt = controller.getShippingDetail(orderID);
            string shippingDate = dt.Rows[0][2].ToString();
            string[]
                d = shippingDate
                    .Split(' '); //since the database also store the time follwing the date, split it so that only date will be display
            shippingDate = d[0];
            string[] delivermanDetail = controller.getDelivermanDetail(orderID);
            lblDelivermanID.Text = dt.Rows[0][1].ToString();
            lblDelivermanName.Text = $"{delivermanDetail[0]} {delivermanDetail[1]}";
            lblDelivermanContact.Text = delivermanDetail[2];
            lblShippingDate.Text = $"{shippingDate}";
            lblExpressNum.Text = dt.Rows[0][4].ToString();
            lblShippingAddress.Text = controller.getShippingAddress(UID);

            //order basic info
            dt = controller.getOrder(orderID);
            lblOrderID.Text = orderID;
            lblSerialNum.Text = $"{dt.Rows[0][3]}";
            lblOrderDate.Text = $"{dt.Rows[0][4]}";
            lblStaffName.Text = $"{controller.getStaffName(dt.Rows[0][2].ToString())}";
            lblStaffID.Text = $"{controller.getStafftID(dt.Rows[0][2].ToString())}";
            lblStaffContact.Text = $"{controller.getStaffContact(dt.Rows[0][2].ToString())}";
            lblStatus.Text = $"{dt.Rows[0][6]}";
        }

        private void cmbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cmbSortOrder.Text.ToString());
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
    }
}