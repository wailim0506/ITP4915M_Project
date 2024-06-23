using System;
using System.Windows.Forms;
using controller;
using System.Dynamic;

namespace templatev1
{
    public partial class StockAdd : Form
    {
        private string uName, UID;
        dynamic newPartsInfo;
        AccountController accountController;
        stockController stockController;
        UIController UIController;


        public StockAdd(AccountController accountController, UIController UIController,
            stockController stockController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            this.stockController = stockController;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialization();
        }

        private void Initialization()
        {
            timer1.Enabled = true;

            UID = accountController.GetUid();
            uName = accountController.GetName();
            lblUid.Text = "UID: " + UID;
            setIndicator(UIController.getIndicator("Stock Management"));
            lblPartIDMsg.Text = "Select a category to generate the part number..";

            //Get valuse from the database.
            cmbSupplier.Items.AddRange(stockController.GetSupplier().ToArray());
            cmbCategory.Items.AddRange(stockController.GetCategory().ToArray());


            //For determine which button needs to be shown.
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

            //For icon color
            if (Properties.Settings.Default.BWmode == true)
            {
                picBWMode.Image = Properties.Resources.LBWhite;
                picHome.Image = Properties.Resources.homeWhite;
            }
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

        //Determine next page.
        private void btnFunction1_Click(object sender, EventArgs e)
        {
            getPage(btnFunction1.Text);
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction2.Text);
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            getPage(btnFunction3.Text);
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            getPage(btnFunction4.Text);
        }

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
        }

        //Determine next page.
        private void getPage(string Function)
        {
            Form next = new Home(accountController, UIController);
            switch (Function)
            {
                case "Order Management":


                    break;
                case "Invoice Management":


                    break;
                case "On-Sale Product Management":


                    break;
                case "Stock Management":
                    next = new StockMgmt(accountController, UIController);
                    break;
                case "User Managemnet":
                    next = new SAccManage(accountController, UIController);
                    break;
            }

            Hide();
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.ShowDialog();
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
            proFile.Size = Size;
            proFile.ShowDialog();
            Close();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form login = new Login();
            Hide();
            //Swap the current form to another.
            login.StartPosition = FormStartPosition.Manual;
            login.Location = Location;
            login.Size = Size;
            login.ShowDialog();
            Close();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Form home = new Home(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.Size = Size;
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


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Form stockMgmt = new StockMgmt(accountController, UIController);
            Hide();
            //Swap the current form to another.
            stockMgmt.StartPosition = FormStartPosition.Manual;
            stockMgmt.Location = Location;
            stockMgmt.Size = Size;
            stockMgmt.ShowDialog();
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (checkInfo()) //Pass to controller and create account
            {
                setValue(); //If passed set the value in to dynameic.
                if (stockController.CreateNewParts(newPartsInfo))
                {
                    MessageBox.Show(
                        $"Create new spare part success! New spare part number is {lblPartNumber.Text}"
                        , "System message", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    getPage("Stock Management");
                }
                else
                {
                    MessageBox.Show("System Error! Please Contact The Help Desk.", "System error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    getPage("Stock Management");
                }
            }
        }

        private void cmbCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            lblPartIDMsg.Text = "";
            lblPartNumber.Text
                = stockController.GenPartNumber(cmbCategory.SelectedItem.ToString());
        }

        //Check the inputted data.
        private bool checkInfo()
        {
            //Clean previous error message.
            lblSuppMsg.Text = lblNameMsg.Text = lblDLevelMsg.Text
                = lblRLevelMsg.Text = lblCatMsg.Text = lblQtyMsg.Text = "";

            //Check category.
            if (cmbCategory.SelectedItem == null)
            {
                lblCatMsg.Text = "Please select a catagory.";
                cmbCategory.Select();
                return false;
            }

            //Check spare part name.
            if (tbName.Text.Length < 2 || tbName.Text.Length > 50)
            {
                lblNameMsg.Text = "Name too short or too long, minimum 2 maximum 50.";
                lblNameMsg.Select();
                return false;
            }

            //Check supplier.
            if (cmbSupplier.SelectedItem == null)
            {
                lblCatMsg.Text = "Please select a supplier.";
                cmbSupplier.Select();
                return false;
            }

            //Check reorder level.
            int RQty;
            if (!int.TryParse(tbRLevel.Text.ToString(), out RQty) || RQty <= 0 || RQty > 99999)
            {
                lblRLevelMsg.Text = "minimum 1 maximum 99999.";
                lblRLevelMsg.Select();
                return false;
            }

            //Check danger level.
            int DQty;
            if (!int.TryParse(tbDLevel.Text.ToString(), out DQty) || DQty <= 0 || DQty > 99999)
            {
                lblDLevelMsg.Text = "minimum 1 maximum 99999.";
                lblDLevelMsg.Select();
                return false;
            }

            //Check quantity.
            int Qty;
            if (!int.TryParse(tbQty.Text.ToString(), out Qty) || Qty <= 0 || Qty > 99999)
            {
                lblQtyMsg.Text = "minimum 1 maximum 99999.";
                lblQtyMsg.Select();
                return false;
            }

            return true;
        }

        private void setValue()
        {
            newPartsInfo = new ExpandoObject();
            newPartsInfo.SPNumber = lblPartNumber.Text;
            newPartsInfo.Category = cmbCategory.SelectedItem.ToString();
            newPartsInfo.SPname = tbName.Text;
            newPartsInfo.Supp = cmbSupplier.SelectedItem.ToString();
            newPartsInfo.RLevel = tbRLevel.Text;
            newPartsInfo.DLevel = tbDLevel.Text;
            newPartsInfo.Qty = tbQty.Text;
            newPartsInfo.Status = chkStatus.Checked ? "Enable" : "Disable";
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }
    }
}