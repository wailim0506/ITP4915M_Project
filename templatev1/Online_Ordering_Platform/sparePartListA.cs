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
    public partial class sparePartListA : Form
    {
        public sparePartListA()
        {
            InitializeComponent();
        }

        private void sparePartList_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            controller.SparePartListController controller = new controller.SparePartListController(); //create controller object
            List<string> name = controller.getName("A"); 
            lblA1Name.Text += name.ElementAt(0);
            lblA2Name.Text += name.ElementAt(1);
            lblA3Name.Text += name.ElementAt(2);
            lblA4Name.Text += name.ElementAt(3);
            lblA5Name.Text += name.ElementAt(4);




            lblA1Category.Text += "A";
            lblA2Category.Text += "A";
            lblA3Category.Text += "A";
            lblA4Category.Text += "A";
            lblA5Category.Text += "A";

            List<string> num = controller.getNum("A");
            lblA1Num.Text += num.ElementAt(0);
            lblA2Num.Text += num.ElementAt(1);
            lblA3Num.Text += num.ElementAt(2);
            lblA4Num.Text += num.ElementAt(3);
            lblA5Num.Text += num.ElementAt(4);

            List<int> price = controller.getPrice("A");
            lblA1Price.Text += price.ElementAt(0).ToString();
            lblA2Price.Text += price.ElementAt(1).ToString();
            lblA3Price.Text += price.ElementAt(2).ToString();
            lblA4Price.Text += price.ElementAt(3).ToString();
            lblA5Price.Text += price.ElementAt(4).ToString();

        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

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

            if (cmbCategory.Text == "Category D") //if user want to view spare part category D
            {
                Form sparePartListD = new sparePartListD();
                this.Hide();
                //Swap the current form to another.
                sparePartListD.StartPosition = FormStartPosition.Manual;
                sparePartListD.Location = this.Location;
                sparePartListD.Size = this.Size;
                sparePartListD.ShowDialog();
                this.Close();
            }

        }

        private void btnA1AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtA1Num.Text.ToString()} {lblA1Name.Text.ToString()}  added to cart");

        }

        private void btnA2AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtA2Num.Text.ToString()} {lblA2Name.Text.ToString()}  added to cart");

        }

        private void btnA3AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtA3Num.Text.ToString()} {lblA3Name.Text.ToString()}  added to cart");

        }

        private void btnA4AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtA4Num.Text.ToString()} {lblA4Name.Text.ToString()}  added to cart");

        }

        private void btnA5AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtA5Num.Text.ToString()} {lblA5Name.Text.ToString()}  added to cart");

        }

        private void txtA1Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the entered character is a number or control (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void txtA2Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the entered character is a number or control (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void txtA3Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the entered character is a number or control (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void txtA4Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the entered character is a number or control (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void txtA5Num_KeyPress(object sender, KeyPressEventArgs e)
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

