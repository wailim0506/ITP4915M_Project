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
        public sparePartListB()
        {
            InitializeComponent();
        }

        private void sparePartListB_Load(object sender, EventArgs e)
        {
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
    }
}
