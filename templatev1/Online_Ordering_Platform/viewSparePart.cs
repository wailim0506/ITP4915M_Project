using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace templatev1.Online_Ordering_Platform
{
    public partial class viewSparePart : Form
    {
        private string uName, UID;
        controller.accountController accountController;
        controller.UIController UIController;
        controller.viewSparePartController controller;
        private string partNum;
        public viewSparePart()
        {
            InitializeComponent();
        }

        public viewSparePart(string partNum, controller.accountController accountController, controller.UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new controller.viewSparePartController();
            this.partNum = partNum;
            //UID = accountController.getUID();
            UID = "LMC00001"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
        }

        private void viewSparePart_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            DataTable dt =  controller.getInfo(partNum);
            lblPartNum.Text = partNum;
            lblCategory.Text = dt.Rows[0][2].ToString();
            lblName.Text = dt.Rows[0][3].ToString();
            lblItemNum.Text = dt.Rows[0][7].ToString();
            lblOnSalesQty.Text = dt.Rows[0][10].ToString();
            lblDescription.Text = dt.Rows[0][11].ToString();
            lblPrice.Text = dt.Rows[0][12].ToString();
            lblSupplier.Text = dt.Rows[0][16].ToString();
            lblCountry.Text = dt.Rows[0][19].ToString();
            picSpare.Image = imageString(partNum);
            lblLoc.Text += $" - {dt.Rows[0][3].ToString()}";

            if (!isFavourite(partNum))
            {
                btnAddFavourit.Click += new EventHandler(addFavourite);
            }
            else
            {
                btnAddFavourit.Text = "Remove Favourite";
                btnAddFavourit.Click += new EventHandler(removeFavourite);
            }
        }

        private void btnBackSearch_Click(object sender, EventArgs e)
        {
            Form sparePartList = new sparePartList( accountController, UIController);
            this.Hide();
            sparePartList.StartPosition = FormStartPosition.Manual;
            sparePartList.Location = this.Location;
            sparePartList.ShowDialog();
            this.Close();
        }

        private void tbQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnAddQty_Click(object sender, EventArgs e)
        {
            if (tbQty.Text != "") //check have quantity input
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
            if (tbQty.Text != "") //check have quantity input
            {
                if (int.Parse(tbQty.Text.ToString()) == 0) //check quantity input equal 0, do not perform anything if equal to 0
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
                if (int.Parse(tbQty.Text.ToString()) <= 0)  
                {
                    MessageBox.Show("Quantity is invalid." , "Add Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (int.Parse(tbQty.Text.ToString()) > int.Parse(lblOnSalesQty.Text.ToString()))
                    {  //check quantity input is larger than on sales quantity
                        MessageBox.Show($"Quantity input cannot exceed On Sales Quantity ({lblOnSalesQty.Text.ToString()})", "Add Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    int qty = int.Parse(tbQty.Text.ToString());   //quantity input is smaller than on sales quantity
                    MessageBox.Show($"{qty} {lblName.Text.ToString()} has been added to cart.", "Add Cart");
                    tbQty.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Please input the quantity.", "Add Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");

        }

        private void btnGoFavourite_Click(object sender, EventArgs e)
        {
            Form fav = new favourite(accountController, UIController);
            this.Hide();
            fav.StartPosition = FormStartPosition.Manual;
            fav.Location = this.Location;
            fav.ShowDialog();
            this.Close();
        }

        public Boolean isFavourite(string partNum)
        {
            return controller.isFavourite(partNum, UID);
        }

        private void addFavourite(object sender, EventArgs e)
        { 
            if (controller.addToFavourite(partNum, UID))
            {
                MessageBox.Show("Added to favourtie.", "Add Favourite", MessageBoxButtons.OK);
                btnAddFavourit.Text = "Remove Favourite";
                btnAddFavourit.Click -= addFavourite;
                btnAddFavourit.Click += new EventHandler(removeFavourite);
            }
            else
            {
                MessageBox.Show("Please try again.", "Add Favourite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void removeFavourite(object sender, EventArgs e)
        {
            if (controller.removeFavourite(partNum, UID))
            {
                MessageBox.Show("Removed from favourtie.", "Add Favourite", MessageBoxButtons.OK);
                btnAddFavourit.Text = "Add to Favourite";
                btnAddFavourit.Click -= removeFavourite;
                btnAddFavourit.Click += new EventHandler(addFavourite);
            }
            else
            {
                MessageBox.Show("Please try again.", "Remove Favourite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            Form cart = new cart(accountController, UIController);
            this.Hide();
            cart.StartPosition = FormStartPosition.Manual;
            cart.Location = this.Location;
            cart.ShowDialog();
            this.Close();
            return;
        }

        private Image imageString(string imageName)
        {
            PropertyInfo property = typeof(Properties.Resources).GetProperty(imageName, BindingFlags.NonPublic | BindingFlags.Static);
            return property?.GetValue(null, null) as Image;
        }
    }
}
