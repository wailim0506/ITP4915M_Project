using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using controller;
using templatev1.Properties;

namespace templatev1.Online_Ordering_Platform
{
    public partial class viewSparePart : Form
    {
        private string uName, UID;
        private Boolean isLM;
        AccountController accountController;
        UIController UIController;
        viewSparePartController controller;
        private string partNum;

        public viewSparePart()
        {
            InitializeComponent();
            partNum = "D00004";
            controller = new viewSparePartController();
            UID = "LMC00001"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
        }

        public viewSparePart(string partNum, AccountController accountController,
            UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new viewSparePartController();
            this.partNum = partNum;
            UID = accountController.GetUid();
            //UID = "LMC00001"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
            isLM = accountController.GetIsLm();
        }

        private void viewSparePart_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_part();
        }


        public void load_part()
        {
            lblLoc.Text = "Spare Part";
            DataTable dt = controller.GetInfo(partNum);
            lblPartNum.Text = partNum;
            lblCategory.Text = dt.Rows[0][2].ToString();
            lblName.Text = dt.Rows[0][3].ToString();
            lblItemNum.Text = dt.Rows[0][7].ToString();
            if (!isLM)
            {
                lblOnSalesQty.Text = dt.Rows[0][10].ToString();
            }
            else
            {
                lblOnSalesQty.Text = dt.Rows[0][11].ToString();
            }
            
            lblDescription.Text = dt.Rows[0][12].ToString();
            lblPrice.Text = dt.Rows[0][13].ToString();
            lblSupplier.Text = dt.Rows[0][17].ToString();
            lblCountry.Text = dt.Rows[0][20].ToString();
            picSpare.Image = ImageString(partNum);
            lblLoc.Text += $" - {dt.Rows[0][3]}";

            if (!IsFavourite(partNum))
            {
                btnAddFavourit.Click += AddFavourite;
            }
            else
            {
                btnAddFavourit.Text = "Remove Favourite";
                btnAddFavourit.Click += RemoveFavourite;
            }
        }

        private void btnBackSearch_Click(object sender, EventArgs e)
        {
            Form sparePartList = new sparePartList(accountController, UIController);
            Hide();
            sparePartList.StartPosition = FormStartPosition.Manual;
            sparePartList.Location = Location;
            sparePartList.ShowDialog();
            Close();
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
                int qty = int.Parse(tbQty.Text);
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
                if (int.Parse(tbQty.Text) ==
                    1) //check quantity input equal 0, do not perform anything if equal to 0
                {
                    return;
                }

                int qty = int.Parse(tbQty.Text);
                qty--;
                tbQty.Text = qty.ToString();
            }
        }

        private void btnAddCart_Click(object sender, EventArgs e)
        {
            if (tbQty.Text != "")
            {
                if (int.Parse(tbQty.Text) <= 0)
                {
                    MessageBox.Show("Quantity is invalid.", "Add Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (int.Parse(tbQty.Text) > int.Parse(lblOnSalesQty.Text))
                    {
                        //check quantity input is larger than on sales quantity
                        MessageBox.Show(
                            $"Quantity input cannot exceed On Sales Quantity ({lblOnSalesQty.Text})",
                            "Add Cart", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int qty = int.Parse(tbQty.Text);
                    if (controller.AddToCart(UID, partNum, qty, isLM))
                    {
                        MessageBox.Show($"{qty} {lblName.Text} has been added to cart.", "Add Cart");
                        tbQty.Text = "";
                        load_part();
                    }
                    else
                    {
                        MessageBox.Show("Please try again.", "Add Cart", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
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


        public Boolean IsFavourite(string partNum)
        {
            return controller.IsFavourite(partNum, UID);
        }

        private void AddFavourite(object sender, EventArgs e)
        {
            if (controller.AddToFavourite(partNum, UID))
            {
                MessageBox.Show("Added to favourtie.", "Add Favourite", MessageBoxButtons.OK);
                btnAddFavourit.Text = "Remove Favourite";
                btnAddFavourit.Click -= AddFavourite;
                btnAddFavourit.Click += RemoveFavourite;
            }
            else
            {
                MessageBox.Show("Please try again.", "Add Favourite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveFavourite(object sender, EventArgs e)
        {
            if (controller.RemoveFavourite(partNum, UID))
            {
                MessageBox.Show("Removed from favourtie.", "Add Favourite", MessageBoxButtons.OK);
                btnAddFavourit.Text = "Add to Favourite";
                btnAddFavourit.Click -= RemoveFavourite;
                btnAddFavourit.Click += AddFavourite;
            }
            else
            {
                MessageBox.Show("Please try again.", "Remove Favourite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            Form cart = new cart(accountController, UIController);
            Hide();
            cart.StartPosition = FormStartPosition.Manual;
            cart.Location = Location;
            cart.ShowDialog();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form o = new giveFeedback(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            Form o = new favourite(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            Form o = new sparePartList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            Form o = new customerOrderList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form o = new Login();
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void picBWMode_Click(object sender, EventArgs e)
        {
            BWMode();
        }

        private Image ImageString(string imageName)
        {
            PropertyInfo property =
                typeof(Resources).GetProperty(imageName, BindingFlags.NonPublic | BindingFlags.Static);
            return property?.GetValue(null, null) as Image;
        }

        private void btnAddToExistingOrder_Click(object sender, EventArgs e)
        {
            Form addPart = new AddPartToExistingOrder(partNum, tbQty.Text, accountController, UIController);
            Hide();
            addPart.StartPosition = FormStartPosition.Manual;
            addPart.Location = Location;
            addPart.ShowDialog();
            Close();
        }

        private void BWMode()
        {
            dynamic value = UIController.getMode();
            Settings.Default.textColor = ColorTranslator.FromHtml(value.textColor);
            Settings.Default.bgColor = ColorTranslator.FromHtml(value.bgColor);
            Settings.Default.navBarColor = ColorTranslator.FromHtml(value.navBarColor);
            Settings.Default.navColor = ColorTranslator.FromHtml(value.navColor);
            Settings.Default.timeColor = ColorTranslator.FromHtml(value.timeColor);
            Settings.Default.locTbColor = ColorTranslator.FromHtml(value.locTbColor);
            Settings.Default.logoutColor = ColorTranslator.FromHtml(value.logoutColor);
            Settings.Default.profileColor = ColorTranslator.FromHtml(value.profileColor);
            Settings.Default.btnColor = ColorTranslator.FromHtml(value.btnColor);
            Settings.Default.BWmode = value.BWmode;
            if (Settings.Default.BWmode)
            {
                picBWMode.Image = Resources.LBWhite;
                picHome.Image = Resources.homeWhite;
            }
            else
            {
                picBWMode.Image = Resources.LB;
                picHome.Image = Resources.home;
            }
        }
    }
}