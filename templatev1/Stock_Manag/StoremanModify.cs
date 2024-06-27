using System;
using controller;
using System.Windows.Forms;
using System.Dynamic;

namespace templatev1
{
    public partial class SMStockModify : Form
    {
        private string uName, UID;
        AccountController accountController;
        stockController stockController;
        UIController UIController;
        dynamic placeholder, update;

        public SMStockModify(AccountController accountController, UIController UIController,
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

            //For spare part info.
            placeholder = stockController.GetModifyPartInfo();
            lblPartNumber.Text = placeholder.partNumber;
            lblCatID.Text = placeholder.type;
            lblLastModified.Text = placeholder.lastModified;
            tbName.Text = placeholder.SPname;
            tbRLevel.Text = placeholder.reorderLevel;
            tbDLevel.Text = placeholder.dangerLevel;
            cmbSupplier.Text = placeholder.Sname;
            cmbCategory.Text = placeholder.type;
            tbQty.Text = placeholder.quantity;
            chkStockEnable.Checked = placeholder.status.Equals("Enable") ? true : false;


            //For determine which panel to show.
            palStockStatus.Visible = palStock.Visible = UIController.ModifyStore();
            tbName.ReadOnly = UIController.ModifyStore() == true ? false : true;

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


        private void btnModify_Click(object sender, EventArgs e)
        {
            var result = DialogResult.Yes;

            if (checkInfo())
            {
                if (update.status.Equals("Disable"))
                {
                    result =
                        MessageBox.Show($"Are you sure to disable the spare part?" +
                                        $"\nThis operation will also disable on-sale products" +
                                        $" that are related to.", "System message", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);
                }

                if (result == DialogResult.Yes)
                {
                    if (stockController.ModifyStockInfo(update))
                    {
                        MessageBox.Show("Modify successful!", "System message", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        getPage("Stock Management");
                    }
                    else //Something wrong from the controller.
                        MessageBox.Show("System Error! Please Contact The Help Desk.", "System error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                }
                else if (result == DialogResult.No) //User cancel operation.
                    return;
            }
        }

        private void tbName_Enter(object sender, EventArgs e)
        {
            if (tbName.Text == placeholder.SPname)
                tbName.Text = "";
        }

        private void tbName_Leave(object sender, EventArgs e)
        {
            if (tbName.Text == "")
                tbName.Text = placeholder.SPname;
        }

        private void tbRLevel_Enter(object sender, EventArgs e)
        {
            if (tbRLevel.Text == placeholder.reorderLevel)
                tbRLevel.Text = "";
        }

        private void tbRLevel_Leave(object sender, EventArgs e)
        {
            if (tbRLevel.Text == "")
                tbRLevel.Text = placeholder.reorderLevel;
        }

        private void tbDLevel_Enter(object sender, EventArgs e)
        {
            if (tbDLevel.Text == placeholder.dangerLevel)
                tbDLevel.Text = "";
        }

        private void tbDLevel_Leave(object sender, EventArgs e)
        {
            if (tbDLevel.Text == "")
                tbDLevel.Text = placeholder.dangerLevel;
        }

        private void tbQty_Enter(object sender, EventArgs e)
        {
            if (tbQty.Text == placeholder.quantity)
                tbQty.Text = "";
        }

        private void tbQty_Leave(object sender, EventArgs e)
        {
            if (tbQty.Text == "")
                tbQty.Text = placeholder.quantity;
        }

        private bool checkInfo()
        {
            lblNameMsg.Text = lblDLevelMsg.Text = lblRLevelMsg.Text = lblQtyMsg.Text = "";
            update = new ExpandoObject();

            //Check and update spare part name if have change.
            if (tbName.Text != placeholder.SPname)
            {
                if (tbName.Text.Length < 2 || tbName.Text.Length > 50)
                {
                    lblNameMsg.Text = "Name too short or too long, minimum 2 maximum 50.";
                    tbName.Select();
                    return false;
                }

                update.SPname = tbName.Text;
            }
            else
                update.SPname = placeholder.SPname;

            //Check and update spare part danger level if have change.
            if (tbDLevel.Text != placeholder.dangerLevel)
            {
                int Qty;
                if (!int.TryParse(tbDLevel.Text.ToString(), out Qty) || Qty <= 0 || Qty > 99999)
                {
                    lblDLevelMsg.Text = "minimum 1 maximum 99999.";
                    tbDLevel.Select();
                    return false;
                }

                update.dangerLevel = tbDLevel.Text;
            }
            else
                update.dangerLevel = placeholder.dangerLevel;

            //Check and update spare part reorder level if have change.
            if (tbRLevel.Text != placeholder.reorderLevel)
            {
                int Qty;
                if (!int.TryParse(tbRLevel.Text.ToString(), out Qty) || Qty <= 0 || Qty > 99999 ||
                    Qty < int.Parse(tbDLevel.Text))
                {
                    lblRLevelMsg.Text = "Can NOT lower than danger level AND minimum 1 maximum 99999.";
                    tbDLevel.Select();
                    return false;
                }

                update.reorderLevel = tbRLevel.Text;
            }
            else
                update.reorderLevel = placeholder.reorderLevel;

            //Check and update supplier
            update.Sname = cmbSupplier.Text;

            //Check and update category
            update.category = cmbCategory.Text;

            //Check and update status
            update.status = chkStockEnable.Checked == true ? "Enable" : "Disable";

            //Check and update spare part danger level if have change.
            if (tbQty.Text != placeholder.quantity)
            {
                int Qty;
                if (!int.TryParse(tbQty.Text.ToString(), out Qty) || Qty <= 0 || Qty > 99999)
                {
                    lblQtyMsg.Text = "minimum 1 maximum 99999.";
                    tbQty.Select();
                    return false;
                }

                update.quantity = tbQty.Text;
            }
            else
                update.quantity = placeholder.quantity;

            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }
    }
}