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
    }
}
