using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using LMCIS.controller;
using LMCIS.On_Sale_Product_Manag;
using LMCIS.Order_Management;
using LMCIS.Profile;
using LMCIS.Properties;
using LMCIS.Stock_Manag;
using LMCIS.System_page;
using LMCIS.User_Manag;
using LMCIS.Properties;

namespace LMCIS.Online_Ordering_Platform
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
            lblUid.Text = $"Uid: {UID}";
            isLM = accountController.GetIsLm();
        }

        private void viewSparePart_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_part();
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Spare Part"));
        }


        public void load_part()
        {
            lblLoc.Text = "Spare Part";
            DataTable dt = controller.GetInfo(partNum);
            lblPartNum.Text = partNum;
            lblCategory.Text = dt.Rows[0][2].ToString();
            lblName.Text = dt.Rows[0][3].ToString();
            lblItemNum.Text = dt.Rows[0][8].ToString();
            lblOnSalesQty.Text = !isLM ? dt.Rows[0][12].ToString() : dt.Rows[0][13].ToString();

            lblDescription.Text = dt.Rows[0]["description"].ToString();
            lblPrice.Text = dt.Rows[0]["price"].ToString();
            lblSupplier.Text = dt.Rows[0][21].ToString();
            lblCountry.Text = dt.Rows[0][24].ToString();
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
                if (int.Parse(tbQty.Text) < int.Parse(lblOnSalesQty.Text))
                {
                    int qty = int.Parse(tbQty.Text);
                    qty++;
                    tbQty.Text = qty.ToString();
                }
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
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
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

        public void hideButton()
        {
            dynamic btnFun = UIController.showFun();
            btnFunction1.Visible = btnFun.btn1show;
            btnFunction1.Text = btnFun.btn1value;
            btnFunction2.Visible = btnFun.btn2show;
            btnFunction2.Text = btnFun.btn2value;
            btnFunction3.Visible = btnFun.btn3show;
            btnFunction3.Text = btnFun.btn3value;
            btnFunction4.Visible = btnFun.btn4show;
            btnFunction4.Text = btnFun.btn4value;
            btnFunction5.Visible = btnFun.btn5show;
            btnFunction5.Text = btnFun.btn5value;
        }

        private void getPage(string Function)
        {
            Form next = new Home(accountController, UIController);
            switch (Function)
            {
                case "Order Management":
                    if (UID.StartsWith("LMC"))
                    {
                        next = new customerOrderList(accountController, UIController);
                    }
                    else if (accountController.CheckIsDeliverman())
                    {
                        next = new deliverman(accountController, UIController);
                    }
                    else
                    {
                        next = new staffOrderList(accountController, UIController);
                    }

                    break;
                case "Spare Part":
                    next = new sparePartList(accountController, UIController);
                    break;
                case "Cart":
                    next = new cart(accountController, UIController);
                    break;
                case "Favourite":
                    next = new favourite(accountController, UIController);
                    break;
                case "Give Feedback":
                    next = new giveFeedback(accountController, UIController);
                    break;

                case "On-Sale Product Management":
                    next = new OnSaleMain(accountController, UIController);
                    break;
                case "Stock Management":
                    next = new StockMgmt(accountController, UIController);
                    break;
                case "User Management":
                    next = new SAccManage(accountController, UIController);
                    break;
                case "Invoice Management":
                    next = new staffInvoiceList(accountController, UIController);
                    break;
            }

            Hide();
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.ShowDialog();
            Close();
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            getPage(btnFunction1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            getPage(btnFunction4.Text);
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            getPage(btnFunction3.Text);
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction2.Text);
        }

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
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

        private void btnProFile_Click(object sender, EventArgs e)
        {
            proFileController proFileController = new proFileController(accountController);

            proFileController.setType(accountController.GetAccountType());

            Form proFile = new proFileMain(accountController, UIController, proFileController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.ShowDialog();
            Close();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Form home = new Home(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.ShowDialog();
            Close();
        }

        private void lblCorpName_Click(object sender, EventArgs e)
        {
            Form about = new About(accountController, UIController);
            Hide();
            //Swap the current form to another.
            about.StartPosition = FormStartPosition.Manual;
            about.Location = Location;
            about.Size = Size;
            about.ShowDialog();
            Close();
        }


        private void setIndicator(int btnNo)
        {
            switch (btnNo)
            {
                case 1:
                    palSelect1.Visible = true;
                    break;
                case 2:
                    palSelect2.Visible = true;
                    break;
                case 3:
                    palSelect3.Visible = true;
                    break;
                case 4:
                    palSelect4.Visible = true;
                    break;
                case 5:
                    palSelect5.Visible = true;
                    break;
            }
        }
    }
}