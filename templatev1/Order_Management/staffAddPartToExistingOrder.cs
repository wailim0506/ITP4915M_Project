using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using controller;
using controller.Utilities;
using templatev1.Properties;

namespace templatev1
{
    public partial class staffAddPartToExistingOrder : Form
    {
        private dateHandler handler;
        int index;
        AccountController accountController;
        UIController UIController;
        addPartToOrderController controller;
        private string uName, UID;
        string orderID;
        private Boolean isLMOrder;
        public staffAddPartToExistingOrder()
        {
            InitializeComponent();
        }

        public staffAddPartToExistingOrder(string orderID, AccountController accountController, UIController UIController, bool isLMOrder)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new addPartToOrderController();
            handler = new dateHandler();
            UID = this.accountController.GetUid();

            lblUid.Text = $"Uid: {UID}";
            this.isLMOrder = isLMOrder;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = handler.GetSystemDateTime();

        }

        private void staffAddPartToExistingOrder_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            palSelect1.Visible =
               palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Order Management"));
            lblOrderID.Text = orderID;
            load_data(orderID);
        }

        public void load_data(string orderID)
        {
            string shippingDateTime = controller.GetShippingDate(orderID);
            string[] shippingDate = shippingDateTime.Split(' ');
            lblShippingDate.Text = shippingDate[0];
            lblOrderStatus.Text = controller.GetOrderStatus(orderID);
            lblDayUntilDelivery.Text = $"{handler.DayDifference(orderID)} day(s) until shipping.";
            lblIsLM.Text = isLMOrder ? "Yes" : "No";
            DataTable dt = controller.getSparePartList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbSparePartSelection.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        private void cmbSparePartSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = controller.GetPartDetail(cmbSparePartSelection.Text);
            lblPartName.Text = dt.Rows[0][0].ToString();
            lblCategory.Text = $"{dt.Rows[0][2]} - {dt.Rows[0][3]}";
            lblSupplier.Text = dt.Rows[0][4].ToString();
            lblCountry.Text = dt.Rows[0][5].ToString();
            lblPrice.Text = dt.Rows[0][6].ToString();
            lblOnSaleQty.Text = isLMOrder ? dt.Rows[0][8].ToString() : dt.Rows[0][7].ToString();
            picSpare.Image = imageString(cmbSparePartSelection.Text);
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

        private void btnBackViewPart_Click(object sender, EventArgs e)
        {
            Form c = new staffEditOrder(orderID, accountController, UIController);
            Hide();
            c.StartPosition = FormStartPosition.Manual;
            c.Location = Location;
            c.ShowDialog();
            Close();
        }

        private void tbQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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
                        
                        return;
                    }

                    int qty = int.Parse(tbQty.Text);
                    if (controller.addToOrder(orderID, cmbSparePartSelection.Text, int.Parse(tbQty.Text), int.Parse(lblPrice.Text), isLMOrder))
                    {
                        MessageBox.Show($"{qty} {lblPartName.Text} has been added to order {lblOrderID.Text}.", "Add to Order");
                        Form c = new staffAddPartToExistingOrder(orderID, accountController, UIController, isLMOrder);
                        Hide();
                        c.StartPosition = FormStartPosition.Manual;
                        c.Location = Location;
                        c.ShowDialog();
                        Close();
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
                MessageBox.Show("Please input the quantity.", "Add Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);

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

        private void palTime_Paint(object sender, PaintEventArgs e)
        {

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

        private static Image imageString(string imageName)
        {
            PropertyInfo property =
                typeof(Resources).GetProperty(imageName, BindingFlags.NonPublic | BindingFlags.Static);
            return property?.GetValue(null, null) as Image;
        }
    }
}
