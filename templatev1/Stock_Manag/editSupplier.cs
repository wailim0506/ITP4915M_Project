using System;
using System.Windows.Forms;
using System.Dynamic;
using controller;

namespace templatev1
{
    public partial class editSupplier : Form
    {
        private string uName, UID;
        AccountController accountController;
        stockController stockController;
        UIController UIController;
        dynamic placeholder, update;


        public editSupplier(AccountController accountController, UIController UIController,
            stockController stockController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            this.stockController = stockController;
        }

        private void editSupplier_Load(object sender, EventArgs e)
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

            //For supplier info.
            placeholder = stockController.GetModifySupplierInfo();
            lblSupplierID.Text = placeholder.supplierID;
            lblCountry.Text = placeholder.country;
            tbName.Text = placeholder.name;
            tbPhone.Text = placeholder.phone;
            tbAddress.Text = placeholder.address;
            chkStatus.Checked = placeholder.status.Equals("Enable") ? true : false;


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
                case "Invoice Management":
                    next = new staffInvoiceList(accountController, UIController);
                    break;
                case "On-Sale Product Management":
                    next = new OnSaleMain(accountController, UIController);
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

        private void picHome_Click_1(object sender, EventArgs e)
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

        private void tbName_Enter(object sender, EventArgs e)
        {
            if (tbName.Text == placeholder.name)
                tbName.Text = "";
        }

        private void tbName_Leave(object sender, EventArgs e)
        {
            if (tbName.Text == "")
                tbName.Text = placeholder.name;
        }

        private void tbPhone_Enter(object sender, EventArgs e)
        {
            if (tbPhone.Text == placeholder.phone)
                tbPhone.Text = "";
        }

        private void tbPhone_Leave(object sender, EventArgs e)
        {
            if (tbPhone.Text == "")
                tbPhone.Text = placeholder.phone;
        }

        private void tbAddress_Enter(object sender, EventArgs e)
        {
            if (tbAddress.Text == placeholder.address)
                tbAddress.Text = "";
        }

        private void tbAddress_Leave(object sender, EventArgs e)
        {
            if (tbAddress.Text == "")
                tbAddress.Text = placeholder.address;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            var result = DialogResult.Yes;

            if (checkInfo())
            {
                if (update.status.Equals("Disable"))
                {
                    result =
                        MessageBox.Show($"Are you sure to disable the supplier?" +
                                        $"\nThis operation will also disable spare parts and on-sale " +
                                        $"products provided by the supplier.", "System message",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);
                }

                if (result == DialogResult.Yes)
                {
                    if (stockController.ModifySupplierInfo(update))
                    {
                        MessageBox.Show("Modify successful!", "System message", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        Form viewSupplier = new viewSupplier(accountController, UIController, stockController);
                        Hide();
                        //Swap the current form to another.
                        viewSupplier.StartPosition = FormStartPosition.Manual;
                        viewSupplier.Location = Location;
                        viewSupplier.Size = Size;
                        viewSupplier.ShowDialog();
                        Close();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Form viewSupplier = new viewSupplier(accountController, UIController, stockController);
            Hide();
            //Swap the current form to another.
            viewSupplier.StartPosition = FormStartPosition.Manual;
            viewSupplier.Location = Location;
            viewSupplier.Size = Size;
            viewSupplier.ShowDialog();
            Close();
        }

        private bool checkInfo()
        {
            lblNameMsg.Text = lblAddMsg.Text = lblPhoneMsg.Text = "";
            update = new ExpandoObject();

            //Check and update supplier name if have change.
            if (tbName.Text != placeholder.name)
            {
                if (tbName.Text.Length < 2 || tbName.Text.Length > 50)
                {
                    lblNameMsg.Text = "Name too short or too long, minimum 2 maximum 50.";
                    lblNameMsg.Select();
                    return false;
                }

                update.name = tbName.Text;
            }
            else
                update.name = placeholder.name;

            //Check and update supplier address if have change.
            if (tbAddress.Text != placeholder.address)
            {
                if (tbAddress.Text.Length < 2 || tbAddress.Text.Length > 70)
                {
                    lblAddMsg.Text = "Address too short or too long, minimum 2 maximum 70.";
                    tbAddress.Select();
                    return false;
                }

                update.address = tbAddress.Text;
            }
            else
                update.address = placeholder.address;

            //Check and update phone if have change.
            if (tbAddress.Text != placeholder.phone)
            {
                if (tbPhone.Text.Length < 5 || tbPhone.Text.Length > 20)
                {
                    lblPhoneMsg.Text = "Phone number too short or too long, minimum 5 maximum 20.";
                    tbPhone.Select();
                    return false;
                }

                update.phone = tbPhone.Text;
            }
            else
                update.phone = placeholder.phone;

            //Check and update status
            update.status = chkStatus.Checked == true ? "Enable" : "Disable";

            return true;
        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }
    }
}