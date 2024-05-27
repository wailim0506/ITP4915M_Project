using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1.Online_Ordering_Platform.viewSparePart
{
    public partial class viewSparePartA1 : Form
    {
        public viewSparePartA1()
        {
            InitializeComponent();
        }

        private void viewSparePartA1_Load(object sender, EventArgs e)
        {
            controller.viewSparePartController controller = new controller.viewSparePartController();
            lblDescription.Text = controller.getDescription("A00001");
            lblItemNum.Text = controller.getItemID("A00001");
            lblPartNum.Text = controller.getPartNum("A00001");
            lblCategory.Text = controller.getCategory("A00001");
            lblName.Text = controller.getName("A00001");
            lblSupplier.Text = controller.getSupplier("A00001");
            lblCountry.Text = controller.getCountry("A00001");
            lblPrice.Text = controller.getPrice("A00001");
            lblOnSalesQty.Text = controller.getOnSalesQty("A00001");

        }

        private void tbQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the entered character is a number or control (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void btnAddQty_Click(object sender, EventArgs e)
        {
            if (tbQty.Text != "")
            {
                int qty = int.Parse(tbQty.Text.ToString());
                qty++;
                tbQty.Text = qty.ToString();
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
            if (tbQty.Text != "")
            {
                if (int.Parse(tbQty.Text.ToString()) == 0)
                {
                    return;
                }
                else
                {
                    int qty = int.Parse(tbQty.Text.ToString());
                    qty--;
                    tbQty.Text = qty.ToString();
                }
                
            }
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            if (tbQty.Text != "")
            {
                if (int.Parse(tbQty.Text.ToString()) == 0)
                {
                    MessageBox.Show("Quantity cannot be zero.");
                }
                else
                {
                    int qty = int.Parse(tbQty.Text.ToString());
                    MessageBox.Show($"{qty} {lblName.Text.ToString()} has been added to cart.");
                }
            }
            else
            {
                MessageBox.Show("Please input the quantity.");
            }
        }
    }
}
