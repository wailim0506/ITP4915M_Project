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
    public partial class sparePartListB : Form
    {
        private string uName, UID;
        controller.accountController accountController;
        controller.UIController UIController;
        public sparePartListB()
        {
            InitializeComponent();
        }

        public sparePartListB(controller.accountController accountController, controller.UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
        }

        private void sparePartListB_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            //lblUid.Text = $"Uid: {accountController.getUID()}";  //not linked yet

            controller.SparePartListController controller = new controller.SparePartListController(); //create controller object
            List<string> name = controller.getName("B");
            lblB1Name.Text += name.ElementAt(0);
            lblB2Name.Text += name.ElementAt(1);
            lblB3Name.Text += name.ElementAt(2);
            lblB4Name.Text += name.ElementAt(3);
            lblB5Name.Text += name.ElementAt(4);




            lblB1Category.Text += "B";
            lblB2Category.Text += "B";
            lblB3Category.Text += "B";
            lblB4Category.Text += "B";
            lblB5Category.Text += "B";

            List<string> num = controller.getNum("B");
            lblB1Num.Text += num.ElementAt(0);
            lblB2Num.Text += num.ElementAt(1);
            lblB3Num.Text += num.ElementAt(2);
            lblB4Num.Text += num.ElementAt(3);
            lblB5Num.Text += num.ElementAt(4);

            List<int> price = controller.getPrice("B");
            lblB1Price.Text += price.ElementAt(0).ToString();
            lblB2Price.Text += price.ElementAt(1).ToString();
            lblB3Price.Text += price.ElementAt(2).ToString();
            lblB4Price.Text += price.ElementAt(3).ToString();
            lblB5Price.Text += price.ElementAt(4).ToString();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategory.Text == "Category A") //if user want to view spare part category A
            {
                Form sparePartListA = new sparePartListA(accountController, UIController);
                this.Hide();
                //Swap the current form to another.
                sparePartListA.StartPosition = FormStartPosition.Manual;
                sparePartListA.Location = this.Location;
                sparePartListA.Size = this.Size;
                sparePartListA.ShowDialog();
                this.Close();
            }

            if (cmbCategory.Text == "Category C") //if user want to view spare part category C
            {
                Form sparePartListC = new sparePartListC(accountController, UIController);
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
                Form sparePartListD = new sparePartListD(accountController, UIController);
                this.Hide();
                //Swap the current form to another.
                sparePartListD.StartPosition = FormStartPosition.Manual;
                sparePartListD.Location = this.Location;
                sparePartListD.Size = this.Size;
                sparePartListD.ShowDialog();
                this.Close();
            }
        }

        private void btnB1AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtB1Num.Text.ToString()} {lblB1Name.Text.ToString()}  added to cart");
        }

        private void btnB2AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtB2Num.Text.ToString()} {lblB2Name.Text.ToString()}  added to cart");
        }

        private void btnB3AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtB3Num.Text.ToString()} {lblB3Name.Text.ToString()}  added to cart");
        }

        private void btnB4AddCart_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtB4Num.Text.ToString()} {lblB4Name.Text.ToString()}  added to cart");
        }

        private void btnB5AddCArt_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{txtB5Num.Text.ToString()} {lblB5Name.Text.ToString()}  added to cart");
        }


        //only allow integer input for txtB1-5Num
        private void txtB1Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtB2Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtB3Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtB4Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void txtB5Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }
    }
}


            
