using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1.Online_Ordering_Platform
{
    public partial class cart : Form
    {
        private string uName, UID;
        private string partToEdit; //for edit qty function
        controller.accountController accountController;
        controller.UIController UIController;
        controller.cartController controller;
        public cart()
        {
            InitializeComponent();
            UID = "LMC00001"; //hard code for testing
            controller = new controller.cartController();
            lblUid.Text = $"Uid: {UID}";
        }


        public cart(controller.accountController accountController, controller.UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new controller.cartController();
            //UID = accountController.getUID();
            UID = "LMC00001"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
        }


        private void cart_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_part(controller.getCartItem(UID));
        }

        private void load_part(DataTable dt)
        {
            pnlSP.Controls.Clear();
            int yPosition = 8;
            for (int i = 0; i < countRow(dt); i++)
            {
                CheckBox checkBox = new CheckBox() { Name = $"chk{i}", Text = "", Location = new System.Drawing.Point(11, yPosition + 6), Size = new System.Drawing.Size(15, 14), Cursor = Cursors.Hand };
                Label lblCategory = new Label() { Text = $"{dt.Rows[i][4]}", Location = new System.Drawing.Point(37, yPosition), Font = new Font("Microsoft Sans Serif", 14), Size = new System.Drawing.Size(89, 23), TextAlign = ContentAlignment.MiddleCenter };
                Label lblPartNum = new Label() { Name = $"lblPartNum{i}", Text = $"{dt.Rows[i][5]}", Location = new System.Drawing.Point(132, yPosition), Font = new Font("Microsoft Sans Serif", 14), Size = new System.Drawing.Size(141, 23), TextAlign = ContentAlignment.MiddleCenter };
                Label lblPartName = new Label() { Text = $"{dt.Rows[i][14]}", Location = new System.Drawing.Point(279, yPosition), Font = new Font("Microsoft Sans Serif", 14), Size = new System.Drawing.Size(253, 23), TextAlign = ContentAlignment.MiddleCenter };
                Label lblQty = new Label() { Name = $"lblQty{i}", Text = $"{dt.Rows[i][2]}", Location = new System.Drawing.Point(538, yPosition), Font = new Font("Microsoft Sans Serif", 14), Size = new System.Drawing.Size(85, 23), TextAlign = ContentAlignment.MiddleCenter };
                Label lblUnitPrice = new Label() { Text = $"{dt.Rows[i][8]}", Location = new System.Drawing.Point(629, yPosition), Font = new Font("Microsoft Sans Serif", 14), Size = new System.Drawing.Size(95, 23), TextAlign = ContentAlignment.MiddleCenter };
                Label lblRowTotalPrice = new Label() { Name = $"lbRowPrice{i}", Text = $"{int.Parse(dt.Rows[i][2].ToString()) * int.Parse(dt.Rows[i][8].ToString())}", Location = new System.Drawing.Point(730, yPosition), Font = new Font("Microsoft Sans Serif", 14), Size = new System.Drawing.Size(88, 23), TextAlign = ContentAlignment.MiddleCenter };
                Button btnView = new Button() { Name = $"btnView{i}", Text = "View", Location = new System.Drawing.Point(824, yPosition - 3), Font = new Font("Microsoft Sans Serif", 11), TextAlign = ContentAlignment.MiddleCenter, AutoSize = false, Size = new System.Drawing.Size(64, 28), Cursor = Cursors.Hand };
                btnView.Click += new EventHandler(this.btnView_Click);

                pnlSP.Controls.Add(checkBox);
                pnlSP.Controls.Add(lblCategory);
                pnlSP.Controls.Add(lblPartNum);
                pnlSP.Controls.Add(lblPartName);
                pnlSP.Controls.Add(lblQty);
                pnlSP.Controls.Add(lblUnitPrice);
                pnlSP.Controls.Add(lblRowTotalPrice);
                pnlSP.Controls.Add(btnView);

                yPosition += 50;
            }     
        }
        public void btnView_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;
                int index = getViewIndex(buttonName);
                if (index != -1)
                {
                    foreach (Control control in pnlSP.Controls)
                    {
                        if (control.Name == $"lblPartNum{index}")
                        {
                            Form viewSparePart = new viewSparePart(control.Text, accountController, UIController);
                            this.Hide();
                            viewSparePart.StartPosition = FormStartPosition.Manual;
                            viewSparePart.Location = this.Location;
                            viewSparePart.ShowDialog();
                            this.Close();
                            return;
                        }
                    }
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
            int x = -1;
            return x;
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
                for (int i = 0; i < checkedIndex.Count; i++)  
                {
                    foreach (Control control in pnlSP.Controls)
                    {
                        if (control.Name == $"lblPartNum{checkedIndex[i]}")
                        {
                            foreach (Control controls in pnlSP.Controls)   //add qty to db
                            {
                                if (controls.Name == $"lblQty{checkedIndex[i]}")
                                {
                                    if (controller.addQtyBack(control.Text, int.Parse(controls.Text.ToString()),0))
                                    {

                                    }
                                    else
                                    {
                                        MessageBox.Show("Error occur.\nPlease try again", "Remove From Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                            if (controller.removePart(control.Text, UID)) //remove item from cart
                            {
                                
                            }
                            else
                            {
                                MessageBox.Show("Error occur.\nPlease try again", "Remove From Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }

                MessageBox.Show("Item(s) removed", "Remove From Cart", MessageBoxButtons.OK);
                load_part(controller.getCartItem(UID));
            }
            else
            {
                MessageBox.Show("Please select at least one item", "Remove From Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            for (int i = 0; i < checkedBox.Count; i++)
            {
                for (int j = 0; j < numOfCheckBoxInPage; j++)
                {
                    if (checkedBox[i] == $"chk{j}")
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
            
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove all items in cart?", "Remove All", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                List<string> allPartNum = controller.getAllPartNumInCart(UID);
                List<int> allItemQty = controller.getAllItemQtyInCart(UID);
                for (int i = 0; i < allPartNum.Count; i++)
                {
                    controller.addQtyBack(allPartNum[i], allItemQty[i], 0); //add qty to db
                }
                if (controller.removeAll(UID)) //remove from cart
                {
                    MessageBox.Show("All items removed from cart", "Remove All", MessageBoxButtons.OK);
                    load_part(controller.getCartItem(UID));
                }
                else
                {
                    MessageBox.Show("Error occue\nPlease try again", "Remove All", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //restore to db!!!!!!!!
        }

        private void btnEditQty_Click(object sender, EventArgs e)
        {
            List<string> checkedBox = getChecked();
            if (checkedBox.Count == 0)
            {
                MessageBox.Show("Please select one item", "Edit Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }else if (checkedBox.Count > 1)
            {
                MessageBox.Show("Please select one item only", "Edit Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                List<int> checkedIndex = getCheckedIndex(checkedBox);
                foreach (Control control in pnlSP.Controls)
                {
                    if (control.Name == $"lblPartNum{checkedIndex[0]}")
                    {
                        partToEdit = control.Text;
                        lblEditQty.Text = $"Edit {partToEdit} Quantity:";
                        lblEditQty.Visible = true;
                        tbQauntity.Visible = true;
                        picTick.Visible = true;
                    }
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
            if (int.Parse(tbQauntity.Text.ToString()) > 0 && tbQauntity.Text.ToString()!="")
            {
                //get current quantity in cart first
                int currentQty = controller.getCurrentQtyInCart(partToEdit, UID);
                MessageBox.Show(currentQty.ToString());
                //add the current cart value back to db first
                if (controller.addQtyBack(partToEdit, currentQty, int.Parse(tbQauntity.Text.ToString())))
                {
                    //update db with user input
                    if (controller.editDbQty(partToEdit, int.Parse(tbQauntity.Text.ToString())))
                    {
                        //update qty in user cart
                        if (controller.editCartQty(partToEdit, UID, int.Parse(tbQauntity.Text.ToString())))
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
                            MessageBox.Show("Please try again", "Edit Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Please try again", "Edit Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Please try again", "Edit Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number", "Edit Quantity", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            if (controller.getCartItem(UID).Rows.Count > 0) //check there are items in cart
            {
                if (controller.createOrder(UID))
                {
                    MessageBox.Show("OK");
                }
            }
            else
            {
                MessageBox.Show("Cart is empty", "Create Order", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }
    }
}
