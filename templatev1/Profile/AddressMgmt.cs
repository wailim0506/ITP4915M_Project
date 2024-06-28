using System;
using System.Dynamic;
using System.Windows.Forms;
using LMCIS.controller;
using LMCIS.controller.Utilities;
using LMCIS.On_Sale_Product_Manag;
using LMCIS.Online_Ordering_Platform;
using LMCIS.Order_Management;
using LMCIS.Properties;
using LMCIS.Stock_Manag;
using LMCIS.System_page;
using LMCIS.User_Manag;
using LMCIS.Properties;
using Microsoft.Extensions.Logging;

namespace LMCIS.Profile
{
    public partial class AddressMgmt : Form
    {
        private string uName, UID;
        dynamic placeholder, update;
        AccountController accountController;
        UIController UIController;
        proFileController proFileController;

        public AddressMgmt()
        {
            InitializeComponent();
        }

        public AddressMgmt(AccountController accountController, UIController UIController,
            proFileController proFileController)
        {
            Log.LogMessage(LogLevel.Information, "[View] User Management", $"User: {UID} is loaded the form.");
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            this.proFileController = proFileController;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialization();
            cmbProvince.Items.AddRange(proFileController.getpriovince().ToArray());
        }

        private void Initialization()
        {
            timer1.Enabled = true;

            UID = accountController.GetUid();
            uName = accountController.GetName();
            lblUid.Text = "UID: " + UID;
            placeholder = proFileController.getAddinfo();

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

            //Show user information
            dynamic add = proFileController.getAddinfo();
            tbCorpAdd.Text = add.corpAdd;
            tbWarehouseAdd1.Text = add.wAdd1;
            tbWarehouseAdd2.Text = add.wAdd2;
            cmbProvince.Text = add.province;
            cmbCity.Text = add.city;
            if (add.dfvalue == 1)
                rbtA1.Checked = true;
            else
                rbtA2.Checked = true;

            //For icon color
            if (Settings.Default.BWmode)
            {
                picBWMode.Image = Resources.LBWhite;
                picHome.Image = Resources.homeWhite;
            }
        }

        //To determine and show the next page.
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

            Log.LogMessage(LogLevel.Information, "[View] User Management", $"User: {UID} is going to the {Function} page.");
            Hide();
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.ShowDialog();
            Close();
        }

        private void btnProFile_Click(object sender, EventArgs e)
        {
            Log.LogMessage(LogLevel.Information, "[View] User Management", $"User: {UID} is going to the profile page.");
            proFileController = new proFileController(accountController);

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

        private void chkCancel_Click(object sender, EventArgs e)
        {
            proFileController = new proFileController(accountController);

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

        //Set new value to the city listbox when the selected province has changed.
        private void cmbProvince_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbCity.SelectedIndex = -1; //clear the selected value when the province has change.
            cmbCity.Text = "";
            cmbCity.Items.Clear(); //clear the value when the selected province has change.
            cmbCity.Items.AddRange(proFileController.GetCity(cmbProvince.Text)
                .ToArray()); //change city list base on current selected province.
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (checkInfo())
                if (proFileController.ModifyAddress(update))
                {
                    MessageBox.Show("Modify successful!", "System message", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    btnProFile_Click(this, e); //Refresh the profile page.
                }
                else //Something wrong from the controller.
                    MessageBox.Show("System Error! Please Contact The Help Desk.", "System error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
        }

        //The placeholder for textbox.
        private void tbCorpAdd_Enter(object sender, EventArgs e)
        {
            if (tbCorpAdd.Text == placeholder.corpAdd)
                tbCorpAdd.Text = "";
        }

        private void tbCorpAdd_Leave(object sender, EventArgs e)
        {
            if (tbCorpAdd.Text == "")
                tbCorpAdd.Text = placeholder.corpAdd;
        }

        private void tbWarehouseAdd1_Enter(object sender, EventArgs e)
        {
            if (tbWarehouseAdd1.Text == placeholder.wAdd1)
                tbWarehouseAdd1.Text = "";
        }

        private void tbWarehouseAdd1_Leave(object sender, EventArgs e)
        {
            if (tbWarehouseAdd1.Text == "")
                tbWarehouseAdd1.Text = placeholder.wAdd1;
        }

        private void tbWarehouseAdd2_Enter(object sender, EventArgs e)
        {
            if (tbWarehouseAdd2.Text == placeholder.wAdd2)
                tbWarehouseAdd2.Text = "";
        }

        private void tbWarehouseAdd2_Leave(object sender, EventArgs e)
        {
            if (tbWarehouseAdd2.Text == "")
                tbWarehouseAdd2.Text = placeholder.wAdd2;
        }

        //Check the inputted data.
        private bool checkInfo()
        {
            lblAddMsg.Text = "";
            update = new ExpandoObject();

            //Check company address
            if (tbCorpAdd.Text != placeholder.corpAdd)
            {
                if (tbCorpAdd.Text.Length > 50 || tbCorpAdd.Text.Length > 50)
                {
                    lblAddMsg.Text = "Conpany address too long or too short, minimum 5, maximum 50.";
                    tbCorpAdd.Select();
                    return false;
                }

                update.corpAdd = tbCorpAdd.Text;
            }
            else
                update.corpAdd = placeholder.corpAdd;

            //Check company address
            if (tbWarehouseAdd1.Text != placeholder.wAdd1 || rbtA1.Checked == false)
            {
                if (tbWarehouseAdd1.Text.Length > 50 || tbWarehouseAdd1.Text.Length > 50)
                {
                    lblAddMsg.Text = "Warehouse 1 address too long or too short, minimum 5, maximum 50.";
                    tbWarehouseAdd1.Select();
                    return false;
                }

                update.wAdd1 = tbWarehouseAdd1.Text;
            }
            else
                update.wAdd1 = placeholder.wAdd1;

            //Check company address
            if (tbWarehouseAdd2.Text != placeholder.wAdd2 || rbtA2.Checked == false)
            {
                if (tbWarehouseAdd2.Text.Length > 50 || tbWarehouseAdd2.Text.Length > 50)
                {
                    lblAddMsg.Text = "Warehouse 2 address too long or too short, minimum 5, maximum 50.";
                    tbWarehouseAdd2.Select();
                    return false;
                }

                update.wAdd2 = tbWarehouseAdd2.Text;
            }
            else
                update.wAdd2 = placeholder.wAdd2;

            update.dfvalue = rbtA1.Checked ? "1" : "2";

            //Check city.
            if (string.IsNullOrEmpty(cmbCity.Text))
            {
                lblAddMsg.Text = "Please select a city.";
                cmbCity.Select();
                return false;
            }

            update.city = cmbCity.Text;

            update.province = cmbProvince.Text;

            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }
    }
}