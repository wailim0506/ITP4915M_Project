using System;
using System.Drawing;
using System.Windows.Forms;
using controller;
using templatev1.Online_Ordering_Platform;
using templatev1.Properties;

namespace templatev1.Order_Management
{
    public partial class customerViewInvoice : Form
    {
        string orderID, UID;
        AccountController accountController;
        UIController UIController;
        viewInvoiceController controller;

        public customerViewInvoice()
        {
            InitializeComponent();
            controller = new viewInvoiceController();
        }

        public customerViewInvoice(string orderID, AccountController accountController,
            UIController UIController)
        {
            InitializeComponent();
            controller = new viewInvoiceController();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            //this.UID = this.accountController.getUID();
            UID = "LMC00001"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
        }

        private void customerViewInvoice_Load(object sender, EventArgs e)
        {
            lblOrderNum.Text = orderID;
            string[] d = controller.GetOrderDate(orderID).Split(' ');
            lblOrderDate.Text = d[0];
            lblCustomerID.Text = controller.getCustomerID(orderID);
            lblInvoiceNum.Text = controller.getInvoiceNum(orderID);
            lblDelaerAddress.Text = controller.getCustomerAddress(orderID);
            lblDeliveryAddress.Text = controller.getWarehouseAddress(orderID);

            //spare part
            string[] partNum = controller.getOrderedSparePartNumber(orderID);
            int rowPos = 1;
            for (int i = 0; i < partNum.Length; i++)
            {
                Label lblPartNum = new Label
                {
                    Text = $"{partNum[i]}", Location = new Point(4, rowPos),
                    Font = new Font("Microsoft Sans Serif", 11), TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(164, 20)
                };
                Label lblPartName = new Label
                {
                    Text = $"{controller.getPartName(partNum[i])}", Location = new Point(174, rowPos),
                    Font = new Font("Microsoft Sans Serif", 11), TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(316, 20)
                };
                Label lblQtyOrdered = new Label
                {
                    Text = $"{controller.getQty(orderID, partNum[i])}",
                    Location = new Point(496, rowPos), Font = new Font("Microsoft Sans Serif", 11),
                    TextAlign = ContentAlignment.MiddleCenter, Size = new Size(123, 20)
                };
                Label lblQtyDelivered = new Label
                {
                    Text = $"{controller.getQty(orderID, partNum[i])}",
                    Location = new Point(625, rowPos), Font = new Font("Microsoft Sans Serif", 11),
                    TextAlign = ContentAlignment.MiddleCenter, Size = new Size(139, 20)
                };

                pnlSP.Controls.Add(lblPartNum);
                pnlSP.Controls.Add(lblPartName);
                pnlSP.Controls.Add(lblQtyOrdered);
                pnlSP.Controls.Add(lblQtyDelivered);
                rowPos += 27;
            }

            lblDeliveryDate.Text += controller.getDeliveryDate(orderID);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (controller.confirmInvoice(lblInvoiceNum.Text))
            {
                MessageBox.Show("Invoice confirmed", "Confirm Invoice");
            }
            else
            {
                MessageBox.Show("Error occur\nPlease try again", "Confirm Invoice", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form o = new giveFeedback(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            Form o = new favourite(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            Form o = new cart(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            Form o = new sparePartList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form customerViewOrder = new customerViewOrder(orderID, accountController, UIController);
            Hide();
            customerViewOrder.StartPosition = FormStartPosition.Manual;
            customerViewOrder.Location = Location;
            customerViewOrder.ShowDialog();
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