using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1.Stock_Manag
{
    public partial class editSupplier : Form
    {
        string supplierID;
        DataTable dt;
        controller.supplierController controller;
        private string uName, UID;
        controller.AccountController accountController;
        controller.UIController UIController;

        public editSupplier(string supplierID)
        {
            InitializeComponent();
            this.supplierID = supplierID;
            controller = new controller.supplierController();
            lblSupplierNumber.Text = supplierID;
        }

        public editSupplier(string supplierID, controller.AccountController accountController,
            controller.UIController UIController)
        {
            InitializeComponent();
            this.supplierID = supplierID;
            controller = new controller.supplierController();
            lblSupplierNumber.Text = supplierID;
            this.accountController = accountController;
            this.UIController = UIController;
        }

        private void editSupplier_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            tbName.Text = controller.getSupplierName(supplierID);
            tbPhone.Text = controller.getSupplierPhone(supplierID);
            tbAddress.Text = controller.getSupplierAddress(supplierID);
            lblCountry.Text = controller.getSupplierCountry(supplierID);
            //lblUid.Text = $"Uid: {accountController.GetUID()}";  //not linked yet
        }

        private void tbPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (controller.updateSupplier(supplierID, tbName.Text.ToString(), tbPhone.Text.ToString(),
                    tbAddress.Text.ToString()) && tbName.Text.ToString() != "" && tbPhone.Text.ToString() != "" &&
                tbAddress.Text.ToString() != "")
            {
                MessageBox.Show("Edit succeessfull.");
                Form viewSupplier = new viewSupplier();
                Hide();
                viewSupplier.StartPosition = FormStartPosition.Manual;
                viewSupplier.Location = Location;
                viewSupplier.ShowDialog();
                Close();
            }
            else
            {
                if (tbName.Text.ToString() == "" || tbPhone.Text.ToString() == "" || tbAddress.Text.ToString() == "")
                {
                    MessageBox.Show("Please don't leave blank");
                }
                else
                {
                    MessageBox.Show("Please try again.");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result =
                MessageBox.Show("All unsaved change will be lost!\nAre you sure you want to cancel editing?",
                    "Confirmation", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Form viewSupplier = new viewSupplier();
                Hide();
                viewSupplier.StartPosition = FormStartPosition.Manual;
                viewSupplier.Location = Location;
                viewSupplier.ShowDialog();
                Close();
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }
    }
}