using System;
using System.Windows.Forms;
using controller.Utilities;
using controller;
using System.Data;
using System.Drawing;
using templatev1.Properties;

namespace templatev1
{
    public partial class staffEditOrder : Form
    {
        private dateHandler handler;
        int index;
        AccountController accountController;
        UIController UIController;
        staffEditOrderController controller;
        private string uName, UID;
        string orderID;
        private Boolean isLMOrder;

        public staffEditOrder()
        {
            InitializeComponent();
            handler = new dateHandler();
        }

        public staffEditOrder(string orderID, AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new staffEditOrderController();
            handler = new dateHandler();
            UID = this.accountController.GetUid();

            lblUid.Text = $"Uid: {UID}";
            lblLoc.Text += $" {orderID}";
            isLMOrder = controller.isLMOrder(orderID);
        }

        private void staffEditOrder_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            palSelect1.Visible =
               palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Order Management"));
            cmbSortOrder.SelectedIndex = 0;
            loadData(cmbSortOrder.Text);
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
                            clickedPencil.Image = Resources.bin;
                            clickedPencil.Click += bin_click;
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
            int qtyInOrderNow = controller.GetPartQtyInOrder(partToDelete, orderID);
            DialogResult dialogResult = MessageBox.Show(
                $"Are you sure you want to remove {controller.GetPartName(partToDelete)} from your order?\nYour action cannot be revoked after confirming it.",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes && controller.DeleteSparePart(orderID, partToDelete))
            {
                //add qty back to db
                //no need to add back as no deduction when create order
                //controller.AddQtyBackToSparePart(partToDelete, orderID, qtyInOrderNow);
                MessageBox.Show("Delete successful.", " Delete Successful", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                hideEdit();
                loadData(cmbSortOrder.Text);
            }
            else if (dialogResult == DialogResult.Yes && controller.DeleteSparePart(orderID, partToDelete) == false)
            {
                MessageBox.Show("Something went wrong.\nPlease contact our staff for help", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Form customerEditOrder = new staffEditOrder(orderID, accountController, UIController);
                loadData(cmbSortOrder.Text);
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

        public void hideEdit()
        {
            lblEditQuantity.Visible = false;
            lblEditQuantity.Text = $"";
            tbQauntity.Visible = false;
            tbQauntity.Name = "";
            picTick.Visible = false;
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
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form o = new staffViewOrder(orderID, accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void tbQauntity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void picTick_Click(object sender, EventArgs e) //edit quantity
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
                qtyInOrderNow = controller.GetPartQtyInOrder(partToUpdate, orderID);
                //add back to db
                try
                {
                    controller.AddQtyBack(partToUpdate, qtyInOrderNow, int.Parse(quantity), isLMOrder);
                }
                catch (Exception)
                {
                    MessageBox.Show("Sorry, we dont have enough spare part\nPlease try adjusting the quantity",
                        "Edit Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //deduct db qty after adding back order qty to db
                if (controller.EditDbQty(partToUpdate, int.Parse(quantity), isLMOrder, orderID))
                {
                    //edit order line qty
                    if (controller.EditOrderLineQuantity(orderID, partToUpdate, quantity))
                    {
                        MessageBox.Show("Edit successful.", " Edit Successful", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        loadData(cmbSortOrder.Text);
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

        private void picRefresh_Click(object sender, EventArgs e)
        {
            hideEdit();
            loadData(cmbSortOrder.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = handler.GetSystemDateTime();
        }


        public void loadData(string sortBy)
        {
            pnlSP.Controls.Clear();
            //ordered spare part
            DataTable dt;
            dt = new DataTable();

            dt = controller.GetOrderedSparePart(orderID, sortBy);


            int row = dt.Rows.Count;

            if (row == 0) //all spare part is removed, the order can be delete
            {
                controller.DeleteOrder(orderID);
                MessageBox.Show("Since no spare part exist in this order, this order is deleted");
                Form orderList = new staffOrderList(accountController, UIController);
                Hide();
                orderList.StartPosition = FormStartPosition.Manual;
                orderList.Location = Location;
                orderList.ShowDialog();
                Close();
                return;
            }


            int rowPosition = 8;
            int orderTotalPrice = 0;
            for (int i = 1; i <= row; i++)
            {
                Label lblRowNum = new Label
                {
                    Name = $"lblRowNum{i}",
                    Text = $"{i.ToString()}.",
                    Location = new Point(3, rowPosition),
                    Font = new Font("Times New Roman", 12),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(30, 20)
                };
                Label lblItemNum = new Label
                {
                    Name = $"lblItemNum{i}",
                    Text = $"{controller.GetItemNum(dt.Rows[i - 1][0].ToString())}",
                    Location = new Point(35, rowPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(83, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPartNum = new Label
                {
                    Name = $"lblPartNum{i}",
                    Text = $"{dt.Rows[i - 1][0]}",
                    Location = new Point(124, rowPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(97, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPartName = new Label
                {
                    Name = $"lblPartName{i}",
                    Text = $"{controller.GetPartName(dt.Rows[i - 1][0].ToString())}",
                    Location = new Point(227, rowPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(300, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblQuantity = new Label
                {
                    Name = $"lblQuantity{i}",
                    Text = $"{dt.Rows[i - 1][2]}",
                    Location = new Point(533, rowPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(106, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblUnitPrice = new Label
                {
                    Name = $"lblUnitPrice{i}",
                    Text = $"¥{dt.Rows[i - 1][3]}",
                    Location = new Point(645, rowPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(144, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblRowTotalPrice = new Label
                {
                    Name = $"lblRowTotalPrice{i}",
                    Text =
                        $"¥{(int.Parse(dt.Rows[i - 1][2].ToString()) * int.Parse(dt.Rows[i - 1][3].ToString())).ToString()}",
                    Location = new Point(795, rowPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(114, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                PictureBox picPencil = new PictureBox
                {
                    Name = $"picPencil{i}",
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(23, 29),
                    Location = new Point(907, rowPosition - 5),
                    Image = Resources.pencil,
                    Cursor = Cursors.Hand
                };

                picPencil.Click += picPencil_Click;
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
            dt = controller.GetShippingDetail(orderID);
            string shippingDate = dt.Rows[0][2].ToString();
            string[]
                d = shippingDate
                    .Split(' '); //since the database also store the time follwing the date, split it so that only date will be display
            shippingDate = d[0];
            string[] delivermanDetail = controller.GetDelivermanDetail(orderID);
            lblDelivermanID.Text = dt.Rows[0][1].ToString();
            lblDelivermanName.Text = $"{delivermanDetail[0]} {delivermanDetail[1]}";
            lblDelivermanContact.Text = delivermanDetail[2];
            lblShippingDate.Text = $"{shippingDate}";
            lblExpressNum.Text = dt.Rows[0][4].ToString();
            lblShippingAddress.Text = dt.Rows[0][5].ToString();

            //order basic info
            dt = controller.GetOrder(orderID);
            lblOrderID.Text = orderID;
            lblSerialNum.Text = $"{dt.Rows[0][3]}";
            lblOrderDate.Text = $"{dt.Rows[0][4]}";
            lblStaffName.Text = $"{controller.GetStaffName(dt.Rows[0][2].ToString())}";
            lblStaffID.Text = $"{controller.GetStafftId(dt.Rows[0][2].ToString())}";
            lblStaffContact.Text = $"{controller.GetStaffContact(dt.Rows[0][2].ToString())}";
            lblStatus.Text = $"{dt.Rows[0][6]}";
        }

        private void cmbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData(cmbSortOrder.Text);
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


        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form o = new Login();
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
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
            Form proFile = new SAccManage(accountController, UIController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.ShowDialog();
            Close();
        }

        private void palTime_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddPart_Click(object sender, EventArgs e)
        {
            Form o =
                new staffAddPartToExistingOrder(orderID,accountController,UIController,isLMOrder);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
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