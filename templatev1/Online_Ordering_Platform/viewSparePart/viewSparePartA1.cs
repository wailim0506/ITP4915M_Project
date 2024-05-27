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
    }
}
