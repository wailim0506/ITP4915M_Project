﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LMCIS.controller;
using LMCIS.On_Sale_Product_Manag;
using LMCIS.Order_Management;
using LMCIS.Profile;
using LMCIS.Properties;
using LMCIS.Stock_Manag;
using LMCIS.System_page;
using LMCIS.User_Manag;
using LMCIS.Properties;

namespace LMCIS.Online_Ordering_Platform
{
    public partial class cart : Form
    {
        private string uName, UID;
        private string partToEdit; //for edit qty function
        private Boolean isLM;
        AccountController accountController;
        UIController UIController;
        cartController controller;

        public cart()
        {
            InitializeComponent();
            controller = new cartController();
            lblUid.Text = $"Uid: {UID}";
        }


        public cart(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new cartController();
            UID = accountController.GetUid();
            isLM = accountController.GetIsLm();
            lblUid.Text = $"Uid: {UID}";
        }


        private void cart_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_part(controller.getCartItem(UID));
            load_customer_address(controller.GetCustomerAddress(UID));
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Cart"));
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

        public void load_customer_address(DataTable dt)
        {
            tbAddress.Text = dt.Rows[0][0].ToString();
            tbProvince.Text = dt.Rows[0][1].ToString();
            tbCity.Text = dt.Rows[0][2].ToString();
            DateTime sysDate = DateTime.Now.Date;
            dtpShippingDate.MinDate =
                sysDate.AddDays(5); //the first available shipping date is five day after order date
        }

        private void load_part(DataTable dt)
        {
            pnlSP.Controls.Clear();
            int totalPrice = 0;
            int yPosition = 8;
            for (int i = 0; i < countRow(dt); i++)
            {
                CheckBox checkBox = new CheckBox
                {
                    Name = $"chk{i}", Text = "", Location = new Point(11, yPosition + 6),
                    Size = new Size(15, 14), Cursor = Cursors.Hand
                };
                Label lblCategory = new Label
                {
                    Text = $"{dt.Rows[i][5]}", Location = new Point(37, yPosition),
                    Font = new Font("Times New Roman", 14), Size = new Size(89, 23),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPartNum = new Label
                {
                    Name = $"lblPartNum{i}", Text = $"{dt.Rows[i][6]}",
                    Location = new Point(132, yPosition), Font = new Font("Times New Roman", 14),
                    Size = new Size(141, 23), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPartName = new Label
                {
                    Text = $"{dt.Rows[i]["name"]}", Location = new Point(279, yPosition),
                    Font = new Font("Times New Roman", 14), Size = new Size(253, 23),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblQty = new Label
                {
                    Name = $"lblQty{i}", Text = $"{dt.Rows[i][2]}", Location = new Point(538, yPosition),
                    Font = new Font("Times New Roman", 14), Size = new Size(85, 23),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblUnitPrice = new Label
                {
                    Text = $"¥{dt.Rows[i][10]}", Location = new Point(629, yPosition),
                    Font = new Font("Times New Roman", 14), Size = new Size(95, 23),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblRowTotalPrice = new Label
                {
                    Name = $"lbRowPrice{i}",
                    Text = $"¥{int.Parse(dt.Rows[i][2].ToString()) * int.Parse(dt.Rows[i][10].ToString())}",
                    Location = new Point(730, yPosition), Font = new Font("Times New Roman", 14),
                    Size = new Size(88, 23), TextAlign = ContentAlignment.MiddleCenter
                };
                Button btnView = new Button
                {
                    Name = $"btnView{i}", Text = "View", Location = new Point(824, yPosition - 3),
                    Font = new Font("Times New Roman", 11), TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false, Size = new Size(64, 28), Cursor = Cursors.Hand
                };
                btnView.Click += btnView_Click;

                pnlSP.Controls.Add(checkBox);
                pnlSP.Controls.Add(lblCategory);
                pnlSP.Controls.Add(lblPartNum);
                pnlSP.Controls.Add(lblPartName);
                pnlSP.Controls.Add(lblQty);
                pnlSP.Controls.Add(lblUnitPrice);
                pnlSP.Controls.Add(lblRowTotalPrice);
                pnlSP.Controls.Add(btnView);

                yPosition += 50;
                totalPrice += int.Parse(dt.Rows[i][2].ToString()) * int.Parse(dt.Rows[i][10].ToString());
            }

            lblTotal.Text = $"¥{totalPrice}";
        }

        public void btnView_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                string buttonName = clickedButton.Name;
                int index = getViewIndex(buttonName);
                if (index == -1) return;
                foreach (Control control in pnlSP.Controls)
                {
                    if (control.Name != $"lblPartNum{index}") continue;
                    Form viewSparePart = new viewSparePart(control.Text, accountController, UIController);
                    Hide();
                    viewSparePart.StartPosition = FormStartPosition.Manual;
                    viewSparePart.Location = Location;
                    viewSparePart.ShowDialog();
                    Close();
                    return;
                    //MessageBox.Show(control.Text);
                }
            }
        }

        private int getViewIndex(string btnName)
        {
            int i = 0;
            while (true)
            {
                if (btnName == $"btnView{i}")
                {
                    return i;
                }

                i++;
            }
        }


        public int countRow(DataTable dt)
        {
            return dt.Rows.Count;
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            List<int> checkedIndex = getCheckedIndex(getChecked());
            if (checkedIndex.Count > 0)
            {
                foreach (var t in checkedIndex)
                {
                    foreach (Control control in pnlSP.Controls)
                    {
                        if (control.Name != $"lblPartNum{t}") continue;
                        foreach (Control controls in pnlSP.Controls) //add qty back to db
                        {
                            if (controls.Name != $"lblQty{t}") continue;
                            if (controller.addQtyBack(control.Text, int.Parse(controls.Text), 0,
                                    isLM))
                            {
                            }
                            else
                            {
                                MessageBox.Show("Error occur.\nPlease try again", "Remove From Cart",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        if (controller.RemovePart(control.Text, UID)) //remove item from cart
                        {
                        }
                        else
                        {
                            MessageBox.Show("Error occur.\nPlease try again", "Remove From Cart",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                MessageBox.Show("Item(s) removed", "Remove From Cart", MessageBoxButtons.OK);
                load_part(controller.getCartItem(UID));
            }
            else
            {
                MessageBox.Show("Please select at least one item", "Remove From Cart", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public List<string> getChecked()
        {
            List<string> checkedBox = new List<string>();

            foreach (Control control in pnlSP.Controls)
            {
                if (control is CheckBox checkBox && checkBox.Checked)
                {
                    checkedBox.Add(checkBox.Name);
                }
            }

            return checkedBox;
        }

        private List<int> getCheckedIndex(List<string> checkedBox)
        {
            List<int> index = new List<int>();
            int numOfCheckBoxInPage = getNumOfCheckBox();
            foreach (var t in checkedBox)
            {
                for (int j = 0; j < numOfCheckBoxInPage; j++)
                {
                    if (t == $"chk{j}")
                    {
                        index.Add(j);
                    }
                }
            }

            return index;
        }

        private int getNumOfCheckBox()
        {
            int i = 0;
            foreach (Control control in pnlSP.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    ++i;
                }
            }

            return i;
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if (controller.getCartItem(UID).Rows.Count > 0) //check there are items in cart
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove all items in cart?",
                    "Remove All", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult != DialogResult.Yes) return;
                List<string> allPartNum = controller.getAllPartNumInCart(UID);
                List<int> allItemQty = controller.getAllItemQtyInCart(UID);
                for (int i = 0; i < allPartNum.Count; i++)
                {
                    controller.addQtyBack(allPartNum[i], allItemQty[i], 0, isLM); //add qty back to db
                }

                if (controller.removeAll(UID)) //remove from cart
                {
                    MessageBox.Show("All items removed from cart", "Remove All", MessageBoxButtons.OK);
                    load_part(controller.getCartItem(UID));
                }
                else
                {
                    MessageBox.Show("Error occur\nPlease try again", "Remove All", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Your cart is empty", "Remove All", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditQty_Click(object sender, EventArgs e)
        {
            List<string> checkedBox = getChecked();
            if (checkedBox.Count == 0)
            {
                MessageBox.Show("Please select one item", "Edit Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (checkedBox.Count > 1)
            {
                MessageBox.Show("Please select one item only", "Edit Quantity", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                List<int> checkedIndex = getCheckedIndex(checkedBox);
                foreach (Control control in pnlSP.Controls)
                {
                    if (control.Name != $"lblPartNum{checkedIndex[0]}") continue;
                    partToEdit = control.Text;
                    lblEditQty.Text = $"Edit {partToEdit} Quantity:";
                    lblEditQty.Visible = true;
                    tbQauntity.Visible = true;
                    picTick.Visible = true;
                }
            }
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
            if (tbQauntity.Text == "")
            {
                MessageBox.Show("Please enter a number", "Edit Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (int.Parse(tbQauntity.Text) > 0 && tbQauntity.Text != "")
            {
                //get current quantity in cart first
                int currentQty = controller.GetCurrentQtyInCart(partToEdit, UID);
                //add the current cart value back to db first
                try
                {
                    controller.addQtyBack(partToEdit, currentQty, int.Parse(tbQauntity.Text), isLM);
                }
                catch (Exception)
                {
                    MessageBox.Show("Sorry, we dont have enough spare part\nPlease try adjusting the quantity",
                        "Edit Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //update db with user input
                if (controller.EditDbQty(partToEdit, int.Parse(tbQauntity.Text), isLM, false, 0))
                {
                    //update qty in user cart
                    if (controller.editCartQty(partToEdit, UID, int.Parse(tbQauntity.Text)))
                    {
                        MessageBox.Show("Quantity updated", "Update Quantity", MessageBoxButtons.OK);
                        lblEditQty.Visible = false;
                        tbQauntity.Text = "";
                        tbQauntity.Visible = false;
                        picTick.Visible = false;
                        load_part(controller.getCartItem(UID));
                    }
                    else
                    {
                        MessageBox.Show("Please try again", "Edit Quantity", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please try again", "Edit Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number", "Edit Quantity", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            if (controller.getCartItem(UID).Rows.Count > 0) //check there are items in cart
            {
                if (tbAddress.Text != "" && tbProvince.Text != "" &&
                    tbCity.Text != "") //check shipping detail is filled
                {
                    string shippingAddress = $"{tbAddress.Text}, {tbProvince.Text}, {tbCity.Text}";
                    DialogResult dialogResult = MessageBox.Show(
                        $"Confrim the following detail:\nShipping Date: {dtpShippingDate.SelectionStart.ToString("yyyy-MM-dd")}\nShipping Address: {tbAddress.Text}, {tbProvince.Text}, {tbCity.Text}",
                        "Create Order", MessageBoxButtons.YesNo);
                    if (dialogResult != DialogResult.Yes) return;
                    if (controller.CreateOrder(UID, dtpShippingDate.SelectionStart.ToString("yyyy-MM-dd"),
                            shippingAddress))
                    {
                        controller.ClearCustomerCartAfterCreateOrder(UID);
                        DialogResult dialogResult2 = MessageBox.Show("Order created\nBrowse other spare part?",
                            "Create Order", MessageBoxButtons.YesNo);
                        if (dialogResult2 == DialogResult.Yes)
                        {
                            Form sparePartList = new sparePartList(accountController, UIController);
                            Hide();
                            sparePartList.StartPosition = FormStartPosition.Manual;
                            sparePartList.Location = Location;
                            sparePartList.ShowDialog();
                            Close();
                        }
                        else
                        {
                            load_part(controller.getCartItem(UID));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please try again", "Create Order", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please fill in shipping detail", "Create Order", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Cart is empty", "Create Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form sparePartList = new sparePartList(accountController, UIController);
            Hide();
            sparePartList.StartPosition = FormStartPosition.Manual;
            sparePartList.Location = Location;
            sparePartList.ShowDialog();
            Close();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void picPencil_Click(object sender, EventArgs e)
        {
            if (tbAddress.ReadOnly)
            {
                tbAddress.ReadOnly = false;
                tbProvince.ReadOnly = false;
                tbCity.ReadOnly = false;
            }
            else
            {
                tbAddress.ReadOnly = true;
                tbProvince.ReadOnly = true;
                tbCity.ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dtpShippingDate.SelectionStart.ToString("yyyy-MM-dd"));
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
            UIController.setMode(Settings.Default.BWmode);
            BWMode();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

        private void pnlSP_Paint(object sender, PaintEventArgs e)
        {
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

        //Check or uncheck all checkbox in cart.
        private void chkAll_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
                foreach (Control c in pnlSP.Controls)
                {
                    if ((c is CheckBox))
                        ((CheckBox)c).Checked = true;
                }
            else
            {
                foreach (Control c in pnlSP.Controls)
                {
                    if ((c is CheckBox))
                        ((CheckBox)c).Checked = false;
                }
            }
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