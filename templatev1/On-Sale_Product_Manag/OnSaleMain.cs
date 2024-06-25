using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Dynamic;
using System.Drawing.Printing;
using System.Windows.Forms;
using controller;

namespace templatev1
{
    public partial class OnSaleMain : Form
    {
        private string uName, UID, selectedProductID;
        private int index;
        AccountController accountController;
        UIController UIController;
        OnSaleProductController onSaleProductController;

        public OnSaleMain(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            onSaleProductController = new OnSaleProductController(accountController);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialization();
        }

        private void Initialization()
        {
            setIndicator(UIController.getIndicator("On-Sale Product Management"));
            timer1.Enabled = true;
            UID = accountController.GetUid();
            uName = accountController.GetName();
            lblUid.Text = "UID: " + UID;
            lblTitTotalNoItem.Text = "No. of Items in the system: " + onSaleProductController.GetTotalProductQty();

            dgvProduct.DataSource = onSaleProductController.GetProduct();
            DgvIndicator();
            dgvProduct.ColumnHeadersDefaultCellStyle.Font
                = new Font("Times New Roman", 13F, FontStyle.Bold);


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

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedProductID)) //Check whether a item is selected.
            {
                onSaleProductController.SetToModityItemID(selectedProductID);
                Form OnSaleModify = new OnSaleModify(accountController, UIController, onSaleProductController);
                Hide();
                //Swap the current form to another.
                OnSaleModify.StartPosition = FormStartPosition.Manual;
                OnSaleModify.Location = Location;
                OnSaleModify.Size = Size;
                OnSaleModify.ShowDialog();
                Close();
            }
            else
                MessageBox.Show("Item part has NOT selected.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Form OnSaleAdd = new OnSaleAdd(accountController, UIController, onSaleProductController);
            Hide();
            //Swap the current form to another.
            OnSaleAdd.StartPosition = FormStartPosition.Manual;
            OnSaleAdd.Location = Location;
            OnSaleAdd.Size = Size;
            OnSaleAdd.ShowDialog();
            Close();
        }

        private void btnSetModifyDis_Click(object sender, EventArgs e)
        {
            Form OnSaleDis = new OnSaleDis(accountController, UIController, onSaleProductController);
            Hide();
            //Swap the current form to another.
            OnSaleDis.StartPosition = FormStartPosition.Manual;
            OnSaleDis.Location = Location;
            OnSaleDis.Size = Size;
            OnSaleDis.ShowDialog();
            Close();
        }

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
                //my version
                case "Order Management":
                    if (UID.StartsWith("LMC"))
                    {
                        next = new customerOrderList(accountController, UIController);
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
                //my version

                case "On-Sale Product Management":
                    next = new OnSaleMain(accountController, UIController);
                    break;
                case "Stock Management":
                    next = new StockMgmt(accountController, UIController);
                    break;
                case "User Management":
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

        private void dgvProduct_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvProduct.Rows.Count > 0)
            {
                dgvProduct.ClearSelection();
                index = dgvProduct.CurrentCell.RowIndex;

                //Select the whole row.
                for (int r = 0; r < dgvProduct.ColumnCount; r++)
                    dgvProduct[r, index].Selected = true;
                selectedProductID =
                    dgvProduct.Rows[index].Cells[0].Value.ToString(); //Get the item ID for the selected row.

                //Set value to stockInfo.
                lblItemID.Text = onSaleProductController.GetProductInfo(selectedProductID).itemID;
                lblPName.Text = onSaleProductController.GetProductInfo(selectedProductID).name;
                lblPPrice.Text = onSaleProductController.GetProductInfo(selectedProductID).price;
                lblPStatus.Text = onSaleProductController.GetProductInfo(selectedProductID).status;
                lblPLastMod.Text = onSaleProductController.GetProductInfo(selectedProductID).lastModified;
                lblSuppID.Text = onSaleProductController.GetProductInfo(selectedProductID).supplierID;
                lblPCat.Text = onSaleProductController.GetProductInfo(selectedProductID).type;
                lblCountry.Text = onSaleProductController.GetProductInfo(selectedProductID).country;
                lblPSuppName.Text = onSaleProductController.GetProductInfo(selectedProductID).suppName;
                lblPStock.Text = onSaleProductController.GetProductInfo(selectedProductID).quantity;
                lblPOnSaleQty.Text = onSaleProductController.GetProductInfo(selectedProductID).onSaleQty;
                lblLMOnSaleQty.Text = onSaleProductController.GetProductInfo(selectedProductID).LM_onSaleQty;
                lblPOnShelve.Text = onSaleProductController.GetProductInfo(selectedProductID).onShelvesDate
                    .ToString("yyyy/MM/dd");
            }
            else //There has not any record in the database.
                MessageBox.Show("Spare part NOT found.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgvProduct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lblItemID.Text = lblPName.Text = lblPPrice.Text = lblPStatus.Text = lblPLastMod.Text
                = lblSuppID.Text = lblPCat.Text = lblPSuppName.Text = lblPStock.Text = lblPOnSaleQty.Text
                    = lblLMOnSaleQty.Text = lblPOnShelve.Text = "";
            selectedProductID = null;
            dgvProduct.ClearSelection();
        }


        private void picSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbSearch.Text)) //Nither using advanced search
            {
                //nor normal search show all records.
                dgvProduct.DataSource = onSaleProductController.GetProduct();
                DgvIndicator();
            }
            else if (tbSearch.Text.StartsWith("LMP")) //Check if is a valid part number.
            {
                dgvProduct.DataSource = onSaleProductController.SearchProduct(tbSearch.Text);
                DgvIndicator();
            }
            else //Not a valid part number.
                MessageBox.Show("Not a valid ItemID",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnRemoveFromShelves_Click(object sender, EventArgs e)
        {
            if (dgvProduct.Rows[index].Cells[5].Value.ToString().Equals("Disable"))
                MessageBox.Show("The current status is DISABLE!",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                var result =
                    MessageBox.Show(
                        $"Are you sure to DISABLE itemID {selectedProductID}?\nClick Yes to continue.",
                        "System message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    onSaleProductController.RemoveFromShelve(selectedProductID);
                    dgvProduct.DataSource = onSaleProductController.GetProduct();
                    DgvIndicator();
                }
            }
        }

        private void dgvProduct_Sorted(object sender, EventArgs e)
        {
            DgvIndicator();
        }

        //Color the data grid view.
        private void DgvIndicator()
        {
            dgvProduct.ClearSelection();
            //If status is enable color it to green otherwise red.
            for (int r = 0; r < dgvProduct.RowCount; r++)
            {
                if (dgvProduct.Rows[r].Cells[5].Value.ToString().Equals("Enable")) //Enabel.
                    dgvProduct.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#C6FEB8");
                if (dgvProduct.Rows[r].Cells[5].Value.ToString().Equals("Disable")) //Disable.
                    dgvProduct.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FEB8B8");
            }
        }
    }
}