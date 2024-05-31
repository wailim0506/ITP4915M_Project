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
    public partial class customerViewInvoice : Form
    {
        string orderID, UID;
        controller.accountController accountController;
        controller.UIController UIController;
        controller.viewInvoiceController controller;
        public customerViewInvoice()
        {
            InitializeComponent();
            controller = new controller.viewInvoiceController();

        }

        public customerViewInvoice(string orderID, controller.accountController accountController, controller.UIController UIController)
        {
            InitializeComponent();
            controller = new controller.viewInvoiceController();
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
            string[] d = controller.getOrderDate(orderID).Split(' ');
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
                Label lblPartNum = new Label() { Text = $"{partNum[i]}", Location = new System.Drawing.Point(4, rowPos), Font = new Font("Microsoft Sans Serif", 11), TextAlign = ContentAlignment.MiddleCenter, Size = new System.Drawing.Size(164, 20) };
                Label lblPartName = new Label() { Text = $"{controller.getPartName(partNum[i])}", Location = new System.Drawing.Point(174, rowPos), Font = new Font("Microsoft Sans Serif", 11), TextAlign = ContentAlignment.MiddleCenter, Size = new System.Drawing.Size(316, 20) };
                Label lblQtyOrdered = new Label() { Text = $"{controller.getQty(orderID, partNum[i])}", Location = new System.Drawing.Point(496, rowPos), Font = new Font("Microsoft Sans Serif", 11), TextAlign = ContentAlignment.MiddleCenter, Size = new System.Drawing.Size(123, 20) };
                Label lblQtyDelivered = new Label() { Text = $"{controller.getQty(orderID, partNum[i])}", Location = new System.Drawing.Point(625, rowPos), Font = new Font("Microsoft Sans Serif", 11), TextAlign = ContentAlignment.MiddleCenter, Size = new System.Drawing.Size(139, 20) };

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
            if (controller.confirmInvoice(lblInvoiceNum.Text.ToString()) == true)
            {
                MessageBox.Show("Invoice confirmed", "Confirm Invoice");
            }
            else
            {
                MessageBox.Show("Error occur\nPlease try again", "Confirm Invoice",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form customerViewOrder = new Order_Management.customerViewOrder(orderID, accountController, UIController);
            this.Hide();
            customerViewOrder.StartPosition = FormStartPosition.Manual;
            customerViewOrder.Location = this.Location;
            customerViewOrder.ShowDialog();
            this.Close();
        }
    }
}
