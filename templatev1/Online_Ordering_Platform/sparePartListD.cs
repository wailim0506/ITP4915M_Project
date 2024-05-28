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
    public partial class sparePartListD : Form
    {
        public sparePartListD()
        {
            InitializeComponent();
        }

        private void sparePartListD_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            controller.SparePartListController controller = new controller.SparePartListController(); //create controller object
            List<string> name = controller.getName("D");
            lblD1Name.Text += name.ElementAt(0);
            lblD2Name.Text += name.ElementAt(1);
            lblD3Name.Text += name.ElementAt(2);
            lblD4Name.Text += name.ElementAt(3);
            lblD5Name.Text += name.ElementAt(4);




            lblD1Category.Text = "D";
            lblD2Category.Text += "D";
            lblD3Category.Text += "D";
            lblD4Category.Text += "D";
            lblD5Category.Text += "D";

            List<string> num = controller.getNum("D");
            lblD1Num.Text += num.ElementAt(0);
            lblD2Num.Text += num.ElementAt(1);
            lblD3Num.Text += num.ElementAt(2);
            lblD4Num.Text += num.ElementAt(3);
            lblD5Num.Text += num.ElementAt(4);

            List<int> price = controller.getPrice("D");
            lblD1Price.Text += price.ElementAt(0).ToString();
            lblD2Price.Text += price.ElementAt(1).ToString();
            lblD3Price.Text += price.ElementAt(2).ToString();
            lblD4Price.Text += price.ElementAt(3).ToString();
            lblD5Price.Text += price.ElementAt(4).ToString();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbCategory.Text == "Category A") //if user want to view spare part category A
            {
                Form sparePartListA = new sparePartListA();
                this.Hide();
                //Swap the current form to another.
                sparePartListA.StartPosition = FormStartPosition.Manual;
                sparePartListA.Location = this.Location;
                sparePartListA.Size = this.Size;
                sparePartListA.ShowDialog();
                this.Close();
            }

            if (cmbCategory.Text == "Category B") //if user want to view spare part category B
            {
                Form sparePartListB = new sparePartListB();
                this.Hide();
                //Swap the current form to another.
                sparePartListB.StartPosition = FormStartPosition.Manual;
                sparePartListB.Location = this.Location;
                sparePartListB.Size = this.Size;
                sparePartListB.ShowDialog();
                this.Close();
            }

            if (cmbCategory.Text == "Category C") //if user want to view spare part category C
            {
                Form sparePartListC = new sparePartListC();
                this.Hide();
                //Swap the current form to another.
                sparePartListC.StartPosition = FormStartPosition.Manual;
                sparePartListC.Location = this.Location;
                sparePartListC.Size = this.Size;
                sparePartListC.ShowDialog();
                this.Close();
            }
        }

        private void btnD1AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtD1Num.Text.ToString()} {lblD1Name.Text.ToString()}  added to cart");
        }

        private void btnD2AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtD2Num.Text.ToString()} {lblD2Name.Text.ToString()}  added to cart");

        }

        private void btnD3AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtD3Num.Text.ToString()} {lblD3Name.Text.ToString()}  added to cart");

        }

        private void btnD4AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtD4Num.Text.ToString()} {lblD4Name.Text.ToString()}  added to cart");

        }

        private void btnD5AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtD5Num.Text.ToString()} {lblD5Name.Text.ToString()}  added to cart");

        }

        private void txtD1Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the entered character is a number or control (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void txtD2Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the entered character is a number or control (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void txtD3Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the entered character is a number or control (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void txtD4Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the entered character is a number or control (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void txtD5Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the entered character is a number or control (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");

        }
    }
}
