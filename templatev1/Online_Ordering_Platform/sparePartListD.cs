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
            controller.SparePartListController controller = new controller.SparePartListController(); //create controller object
            List<string> name = controller.getName("D");
            lblD1Name.Text += name.ElementAt(0);
            lblD2Name.Text += name.ElementAt(1);
            lblD3Name.Text += name.ElementAt(2);
            lblD4Name.Text += name.ElementAt(3);
            lblD5Name.Text += name.ElementAt(4);




            lblD1Category.Text += "D";
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
    }
}
