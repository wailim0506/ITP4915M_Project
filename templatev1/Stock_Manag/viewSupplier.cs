using System;
using System.Drawing;
using System.Windows.Forms;
using LMCIS.controller;
using LMCIS.On_Sale_Product_Manag;
using LMCIS.Online_Ordering_Platform;
using LMCIS.Order_Management;
using LMCIS.Profile;
using LMCIS.Properties;
using LMCIS.System_page;
using LMCIS.User_Manag;

namespace LMCIS.Stock_Manag
{
    public partial class viewSupplier : Form
    {
        private string uName, UID, selectedSupplierID;
        private int index;
        AccountController accountController;
        stockController stockController;
        UIController UIController;


        public viewSupplier(AccountController accountController, UIController UIController,
            stockController stockController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            this.stockController = stockController;
        }

        private void viewSupplier_Load(object sender, EventArgs e)
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
            dgvSupplier.DataSource = (stockController.GetSupplierList());
            dgvSupplier.ClearSelection();
            DgvIndicator();
            dgvSupplier.ColumnHeadersDefaultCellStyle.Font
                = new Font("Times New Roman", 13F, FontStyle.Bold);
            palStock.Visible = UIController.ViewSupplier();

            lblTitTotalNoSupplier.Text = "No. of suppliers in the system: " + stockController.GetTotalSupplierQty();
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
            if (templatev1.Properties.Settings.Default.BWmode == true)
            {
                picBWMode.Image = Resources.LBWhite;
                picHome.Image = Resources.homeWhite;
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

        private void dgvSupplier_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvSupplier.Rows.Count > 0)
            {
                dgvSupplier.ClearSelection();
                index = dgvSupplier.CurrentCell.RowIndex;

                //Select the whole row.
                for (int r = 0; r < dgvSupplier.ColumnCount; r++)
                    dgvSupplier[r, index].Selected = true;
                selectedSupplierID =
                    dgvSupplier.Rows[index].Cells[0].Value.ToString(); //Get the spare part ID for the selected row.

                //Set value to stockInfo.
                lblSSuppID.Text = stockController.GetSupplierInfo(selectedSupplierID).supplierID;
                lblSName.Text = stockController.GetSupplierInfo(selectedSupplierID).name;
                lblSPhone.Text = stockController.GetSupplierInfo(selectedSupplierID).phone;
                lblSAdd.Text = stockController.GetSupplierInfo(selectedSupplierID).address;
                lblSCountry.Text = stockController.GetSupplierInfo(selectedSupplierID).country;
                lblSStatus.Text = stockController.GetSupplierInfo(selectedSupplierID).status;
            }
            else //There has not any record in the database.
                MessageBox.Show("Spare part NOT found.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgvSupplier_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lblSSuppID.Text = lblSName.Text = lblSPhone.Text = lblSAdd.Text
                = lblSCountry.Text = lblSStatus.Text = "";
            selectedSupplierID = null;
            dgvSupplier.ClearSelection();
        }

        private void dgvSupplier_Sorted(object sender, EventArgs e)
        {
            dgvSupplier.ClearSelection();
            DgvIndicator();
        }


        //Color the data grid view.
        private void DgvIndicator()
        {
            dgvSupplier.ClearSelection();
            //If status is enable color it to green otherwise red.
            for (int r = 0; r < dgvSupplier.RowCount; r++)
            {
                if (dgvSupplier.Rows[r].Cells[5].Value.ToString().Equals("Enable")) //Enabel.
                    dgvSupplier.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#C6FEB8");
                if (dgvSupplier.Rows[r].Cells[5].Value.ToString().Equals("Disable")) //Disable.
                    dgvSupplier.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FEB8B8");
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedSupplierID)) //Check whether a part number is selected.
            {
                stockController.SetModifySupplierID(selectedSupplierID); //Set the part number that to be modify.
                Form editSupplier = new editSupplier(accountController, UIController, stockController);
                Hide();
                //Swap the current form to another.
                editSupplier.StartPosition = FormStartPosition.Manual;
                editSupplier.Location = Location;
                editSupplier.Size = Size;
                editSupplier.ShowDialog();
                Close();
            }
            else
                MessageBox.Show("Supplier has NOT selected.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbSearch.Text)) //Nither using advanced search
            {
                //nor normal search show all records.
                dgvSupplier.DataSource = stockController.GetSupplierList();
                DgvIndicator();
            }
            else if (tbSearch.Text.StartsWith("SID")) //Check if is a valid part number.
            {
                dgvSupplier.DataSource = stockController.SearchSupplier(tbSearch.Text);
                DgvIndicator();
            }
            else //Not a valid part number.
                MessageBox.Show("Not a valid supplierID",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                picSearch_Click(this, new EventArgs());
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            getPage("Stock Management");
        }

        private void btnAct_Click(object sender, EventArgs e)
        {
            Form addSupplier = new addSupplier(accountController, UIController, stockController);
            Hide();
            //Swap the current form to another.
            addSupplier.StartPosition = FormStartPosition.Manual;
            addSupplier.Location = Location;
            addSupplier.Size = Size;
            addSupplier.ShowDialog();
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }
    }
}