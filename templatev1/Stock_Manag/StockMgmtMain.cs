using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Drawing.Printing;
using System.Windows.Forms;
using controller;

namespace templatev1
{
    public partial class StockMgmt : Form
    {
        private string uName, UID, selectedPartID;
        private int index;
        AccountController accountController;
        stockController stockController;
        UIController UIController;

        //For print stock card function.
        Bitmap MemoryImage;
        private PrintDocument printDocument1 = new PrintDocument();
        private PrintPreviewDialog previewdlg = new PrintPreviewDialog();

        public StockMgmt(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            stockController = new stockController(accountController);
            printDocument1.PrintPage += new PrintPageEventHandler(printdoc1_PrintPage);
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
            cmbType.Items.AddRange(stockController.GetCategory().ToArray());
            cmbCountry.Items.AddRange(stockController.GetCountry().ToArray());
            cmbSuppiler.Items.AddRange(stockController.GetSupplier().ToArray());

            //Datagridview properties
            dgvStock.DataSource = stockController.GetPart();
            dgvStock.ClearSelection();
            dgvStock.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 13F, FontStyle.Bold);
            DgvIndicator(); //If lower than danger level change color of the row to red. 


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

            //Swap the form between storeman and sale manager
            dynamic funstion = UIController.store();
            palOrder.Visible = funstion.group1;
            palStore.Visible = funstion.group2;

            //For icon color
            if (Properties.Settings.Default.BWmode == true)
            {
                picBWMode.Image = Properties.Resources.LBWhite;
                picHome.Image = Properties.Resources.homeWhite;
            }
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

        private void btnProFile_Click(object sender, EventArgs e)
        {
            controller.proFileController proFileController = new controller.proFileController(accountController);

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

        private void chkAdvancedSearch_CheckedChanged(object sender, EventArgs e)
        {
            grpAdvancedSearch.Visible = chkAdvancedSearch.Checked;

            //Clean All condition.
            if (!chkAdvancedSearch.Checked)
                btnClean_Click(this, new EventArgs());
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Form StockModify = new StockModify();
            Hide();
            //Swap the current form to another.
            StockModify.StartPosition = FormStartPosition.Manual;
            StockModify.Location = Location;
            StockModify.Size = Size;
            StockModify.ShowDialog();
            Close();
        }

        private void btnAddSpare_Click(object sender, EventArgs e)
        {
            Form StockAdd = new StockAdd();
            Hide();
            //Swap the current form to another.
            StockAdd.StartPosition = FormStartPosition.Manual;
            StockAdd.Location = Location;
            StockAdd.Size = Size;
            StockAdd.ShowDialog();
            Close();
        }

        //Clear datagridview selection.
        private void dgvStock_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            dgvStock.ClearSelection();
            lblPartID.Text = lblCat.Text = lblPName.Text = lblSuppID.Text = lblCountry.Text
                = lblPhoneNo.Text = lblSName.Text = lblAdd.Text = lblRLevel.Text = lblDLevel.Text
                    = lblQty.Text = lblCardPartNo.Text = "";
        }

        //Select a spare part and set value to stockInfo.
        private void dgvStock_MouseClick(object sender, MouseEventArgs e)
        {
            //Check if spart part no found or empty DB.
            if (dgvStock.Rows.Count > 0)
            {
                dgvStock.ClearSelection();
                index = dgvStock.CurrentCell.RowIndex;

                //Select the whole row.
                for (int r = 0; r < dgvStock.ColumnCount; r++)
                    dgvStock[r, index].Selected = true;
                selectedPartID =
                    dgvStock.Rows[index].Cells[0].Value.ToString(); //Get the spare part ID for the selected row.

                //Set value to stockInfo.
                lblPartID.Text = stockController.GetPartInfo(selectedPartID).partNumber;
                lblCat.Text = stockController.GetPartInfo(selectedPartID).type;
                lblPName.Text = stockController.GetPartInfo(selectedPartID).SPname;
                lblSuppID.Text = stockController.GetPartInfo(selectedPartID).supplierID;
                lblCountry.Text = stockController.GetPartInfo(selectedPartID).country;
                lblPhoneNo.Text = stockController.GetPartInfo(selectedPartID).phone;
                lblSName.Text = stockController.GetPartInfo(selectedPartID).Sname;
                lblAdd.Text = stockController.GetPartInfo(selectedPartID).address;
                lblRLevel.Text = stockController.GetPartInfo(selectedPartID).reorderLevel;
                lblDLevel.Text = stockController.GetPartInfo(selectedPartID).dangerLevel;
                lblQty.Text = stockController.GetPartInfo(selectedPartID).quantity;
                lblCardPartNo.Text = stockController.GetPartInfo(selectedPartID).partNumber;
            }
            else
                MessageBox.Show("Spare part NOT found.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSendOrder_Click(object sender, EventArgs e)
        {
            if (chkPrtStockCard.Checked && !string.IsNullOrEmpty(selectedPartID)
                                        && stockController.CheckOutOfStock(selectedPartID))
                Print(this.palOutOfStock);
            else if (chkPrtStockCard.Checked)
            {
                MessageBox.Show("The selected item is still in stock.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkPrtStockCard.Checked = false;
            }
        }


        //For print stock card function.
        public void GetPrintArea(Panel pnl)
        {
            MemoryImage = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(MemoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (MemoryImage != null)
            {
                e.Graphics.DrawImage(MemoryImage, 0, 0);
                base.OnPaint(e);
            }
        }

        void printdoc1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(MemoryImage, (pagearea.Width / 2) - (this.palOutOfStock.Width / 2),
                this.palOutOfStock.Location.Y);
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                picSearch_Click(this, new EventArgs());
            }
        }

        private void picSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPartName.Text) && !chkAdvancedSearch.Checked)
            {
                dgvStock.DataSource = stockController.GetPart();
                DgvIndicator();
            }
            else if (tbSearch.Text.StartsWith("A") || tbSearch.Text.StartsWith("B") || tbSearch.Text.StartsWith("C") ||
                     tbSearch.Text.StartsWith("D") || chkAdvancedSearch.Checked)
            {
                if (chkAdvancedSearch.Checked) //Using advanced serch.
                {
                    dynamic partValues = new ExpandoObject();
                    partValues.partName = string.IsNullOrEmpty(tbPartName.Text) ? null : tbPartName.Text;
                    partValues.supplier = cmbSuppiler.SelectedIndex == -1 ? null : cmbSuppiler.SelectedItem.ToString();
                    partValues.category = cmbType.SelectedIndex == -1 ? null : cmbType.SelectedItem.ToString();
                    partValues.country = cmbCountry.SelectedIndex == -1 ? null : cmbCountry.SelectedItem.ToString();

                    dgvStock.DataSource = stockController.AdvancedSearch(partValues);
                    DgvIndicator();
                }
                else
                {
                    dgvStock.DataSource = stockController.SearchPart(tbSearch.Text);
                    DgvIndicator();
                }
            }
            else //Not a valid part number.
                MessageBox.Show("Not a valid part number",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgvStock_Sorted(object sender, EventArgs e)
        {
            DgvIndicator();
        }

        private void DgvIndicator()
        {
            dgvStock.ClearSelection();
            //If lower than danger level change color of the row to red. 
            for (int r = 0; r < dgvStock.RowCount; r++)
            {
                if (int.Parse(dgvStock.Rows[r].Cells[6].Value.ToString()) <=
                    int.Parse(dgvStock.Rows[r].Cells[4].Value.ToString())) //meets re-order level
                    dgvStock.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FDFF6F");
                if (int.Parse(dgvStock.Rows[r].Cells[6].Value.ToString()) <=
                    int.Parse(dgvStock.Rows[r].Cells[5].Value.ToString())) //meets danger level
                    dgvStock.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FF8486");
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            tbPartName.Text = null;
            cmbSuppiler.SelectedIndex = cmbType.SelectedIndex = cmbCountry.SelectedIndex = -1;
        }

        public void Print(Panel pnl)
        {
            Panel pannel = pnl;
            GetPrintArea(pnl);
            previewdlg.Document = printDocument1;
            previewdlg.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }
    }
}