using System;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Dynamic;
using System.Drawing.Printing;
using System.Windows.Forms;
using controller;

namespace templatev1
{
    public partial class StockMgmt : Form
    {
        private string uName, UID, selectedPartID, selectedReorderID;
        private int index, reorderQty, reorderIndex;
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
            lblTitTotalStock.Text = "No. of spare parts in the system: " + stockController.GetTotalSpareQty();

            //Get valuse from the database.
            cmbType.Items.AddRange(stockController.GetCategory().ToArray());
            cmbCountry.Items.AddRange(stockController.GetCountry().ToArray());
            cmbSupplier.Items.AddRange(stockController.GetAllSupplier().ToArray());

            //Datagridview properties
            dgvReorder.DataSource = stockController.GetReorder();
            dgvStock.DataSource = stockController.GetPart();
            dgvReorder.ColumnHeadersDefaultCellStyle.Font = dgvStock.ColumnHeadersDefaultCellStyle.Font
                = new Font("Times New Roman", 13F, FontStyle.Bold);
            DgvIndicator(); //If lower than danger level change color of the row to red. 

            //Clean default selection.
            dgvStock.ClearSelection();
            dgvReorder.ClearSelection();

            //For determine which button needs to be shown.
            btnFunction1.Visible = UIController.showFun().btn1show;
            btnFunction1.Text = UIController.showFun().btn1value;
            btnFunction2.Visible = UIController.showFun().btn2show;
            btnFunction2.Text = UIController.showFun().btn2value;
            btnFunction3.Visible = UIController.showFun().btn3show;
            btnFunction3.Text = UIController.showFun().btn3value;
            btnFunction4.Visible = UIController.showFun().btn4show;
            btnFunction4.Text = UIController.showFun().btn4value;
            btnFunction5.Visible = UIController.showFun().btn5show;
            btnFunction5.Text = UIController.showFun().btn5value;

            //Swap the form between storeman and sale manager
            palStockRestock.Visible = UIController.store().group1;
            btnConfirmOrder.Visible = UIController.store().group2;
            btnShowReorder.Visible = UIController.store().group3;
            btnModify.Visible = UIController.store().group4;

            //For icon color
            if (Properties.Settings.Default.BWmode == true)
            {
                picBWMode.Image = Properties.Resources.LBWhite;
                picHome.Image = Properties.Resources.homeWhite;
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

        //Set the function button induction.
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

        //Search function.
        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                picSearch_Click(this, new EventArgs());
            }
        }

        //Show or hide the advanced search.
        private void chkAdvancedSearch_CheckedChanged(object sender, EventArgs e)
        {
            grpAdvancedSearch.Visible = chkAdvancedSearch.Checked;

            //Clean All condition.
            if (!chkAdvancedSearch.Checked)
                btnClean_Click(this, new EventArgs());
        }

        //Clean data in the advanced serch.
        private void btnClean_Click(object sender, EventArgs e)
        {
            tbPartName.Text = null;
            cmbSupplier.SelectedIndex = cmbType.SelectedIndex = cmbCountry.SelectedIndex = -1;
        }

        //Pass search values to the controller.
        private void picSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPartName.Text) && !chkAdvancedSearch.Checked) //Nither using advanced search
            {
                //nor normal search show all records.
                dgvStock.DataSource = stockController.GetPart();
                DgvIndicator();
            }
            else if (tbSearch.Text.StartsWith("A") || tbSearch.Text.StartsWith("B") || tbSearch.Text.StartsWith("C")
                     || tbSearch.Text.StartsWith("D") || chkAdvancedSearch.Checked) //Check if is a valid part number.
            {
                if (chkAdvancedSearch.Checked) //Using advanced serch.
                {
                    dynamic partValues = new ExpandoObject();
                    partValues.partName = string.IsNullOrEmpty(tbPartName.Text) ? null : tbPartName.Text;
                    partValues.supplier = cmbSupplier.SelectedIndex == -1 ? null : cmbSupplier.SelectedItem.ToString();
                    partValues.category = cmbType.SelectedIndex == -1 ? null : cmbType.SelectedItem.ToString();
                    partValues.country = cmbCountry.SelectedIndex == -1 ? null : cmbCountry.SelectedItem.ToString();

                    dgvStock.DataSource = stockController.AdvancedSearch(partValues);
                    DgvIndicator();
                }
                else //Normal search.
                {
                    dgvStock.DataSource = stockController.SearchPart(tbSearch.Text);
                    DgvIndicator();
                }
            }
            else //Not a valid part number.
                MessageBox.Show("Not a valid part number",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedPartID)) //Check whether a part number is selected.
            {
                stockController.SetModifyPartID(selectedPartID); //Set the part number that to be modify.
                Form StockModify = new SMStockModify(accountController, UIController, stockController);
                Hide();
                //Swap the current form to another.
                StockModify.StartPosition = FormStartPosition.Manual;
                StockModify.Location = Location;
                StockModify.Size = Size;
                StockModify.ShowDialog();
                Close();
            }
            else
                MessageBox.Show("Spare part has NOT selected.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnAddSpare_Click(object sender, EventArgs e)
        {
            Form StockAdd = new StockAdd(accountController, UIController, stockController);
            Hide();
            //Swap the current form to another.
            StockAdd.StartPosition = FormStartPosition.Manual;
            StockAdd.Location = Location;
            StockAdd.Size = Size;
            StockAdd.ShowDialog();
            Close();
        }

        //Clear datagridview selection and shown part Info.
        private void dgvStock_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            dgvStock.ClearSelection();
            selectedPartID = null;
            lblPartID.Text = lblCat.Text = lblPName.Text = lblSuppID.Text = lblCountry.Text
                = lblPhoneNo.Text = lblSName.Text = lblAdd.Text = lblRLevel.Text = lblDLevel.Text
                    = lblQty.Text = lblCardPartNo.Text = "";
        }

        private void dgvReorder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            dgvReorder.ClearSelection();
            selectedReorderID = null;
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
                lblStatus.Text = stockController.GetPartInfo(selectedPartID).status;
                lblLastModified.Text = stockController.GetPartInfo(selectedPartID).lastModified;
            }
            else //There has not any record in the database.
                MessageBox.Show("Spare part NOT found.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //Re-color the data grid view if user soted the record.
        private void dgvStock_Sorted(object sender, EventArgs e)
        {
            DgvIndicator();
        }

        private void dgvReorder_Sorted(object sender, EventArgs e)
        {
            DgvReorderIndicator();
        }

        //Color the data grid view.
        private void DgvIndicator()
        {
            dgvStock.ClearSelection();
            //If lower than danger level change color of the row to red. 
            for (int r = 0; r < dgvStock.RowCount; r++)
            {
                if (int.Parse(dgvStock.Rows[r].Cells[6].Value.ToString()) >=
                    int.Parse(dgvStock.Rows[r].Cells[4].Value.ToString())) //Normal.
                    dgvStock.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#C6FEB8");
                if (int.Parse(dgvStock.Rows[r].Cells[6].Value.ToString()) <=
                    int.Parse(dgvStock.Rows[r].Cells[4].Value.ToString())) //meets re-order level.
                    dgvStock.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FDFF6F");
                if (int.Parse(dgvStock.Rows[r].Cells[6].Value.ToString()) <=
                    int.Parse(dgvStock.Rows[r].Cells[5].Value.ToString())) //meets danger level.
                    dgvStock.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FEB8B8");
                if (dgvStock.Rows[r].Cells[7].Value.ToString().Equals("Disable")) //The status is disable.
                    dgvStock.Rows[r].DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void DgvReorderIndicator()
        {
            dgvReorder.ClearSelection();
            //If lower than danger level change color of the row to red. 
            for (int r = 0; r < dgvReorder.RowCount; r++)
            {
                if (dgvReorder.Rows[r].Cells[5].Value.ToString().Equals("cancelled")) //The order status was cancelled.
                    dgvReorder.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FEB8B8");
                if (dgvReorder.Rows[r].Cells[5].Value.ToString().Equals("processing")) //The order status is processing.
                    dgvReorder.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FDFF6F");
                if (dgvReorder.Rows[r].Cells[5].Value.ToString().Equals("finished")) //The order status was finished.
                    dgvReorder.Rows[r].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#C6FEB8");
            }
        }

        //Recorder finction.
        private void btnSendOrder_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedPartID)) //Check whether a part number is selected.
            {
                if (chkPrtStockCard.Checked) //Determine whether meets 
                {
                    //the requirement to print stock card.
                    if (stockController.CheckOutOfStock(selectedPartID))
                    {
                        if (!ReorderInputBox()) //Ask user to enter the values.
                            return;
                        stockController.CreateReorderRequest(selectedPartID, reorderQty); //Create restock request.
                        Print(this.palOutOfStock); //Pritn stock card.
                        dgvReorder.DataSource = stockController.GetReorder(); //Reflesh the data grid view.
                        DgvReorderIndicator();
                    }
                    else
                        MessageBox.Show("The selected item does NOT meets the requirement to print stock card.",
                            "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    chkPrtStockCard.Checked = false; //Set to default value.
                }
                else if (stockController.CheckReorderLevel(selectedPartID)) //Stock quantity < orderlevel.
                {
                    if (!ReorderInputBox()) //Ask user to enter the values.
                        return;
                    stockController.CreateReorderRequest(selectedPartID, reorderQty); //Create add stock request.
                    dgvReorder.DataSource = stockController.GetReorder();
                    DgvReorderIndicator();
                }
                else
                    MessageBox.Show("The selected item is still in stock.",
                        "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Spare part has NOT selected.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //Select the whole row.
        private void dgvReorder_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvReorder.Rows.Count > 0)
            {
                dgvReorder.ClearSelection();
                reorderIndex = dgvReorder.CurrentCell.RowIndex;

                selectedReorderID =
                    dgvReorder.Rows[reorderIndex].Cells[0].Value.ToString(); //Get selected spare number.

                //Select the whole row.
                for (int r = 0; r < dgvReorder.ColumnCount; r++)
                    dgvReorder[r, reorderIndex].Selected = true;
            }
        }

        //Cancel the order.
        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedReorderID))
            {
                if (dgvReorder.Rows[reorderIndex].Cells[5].Value.ToString()
                    .Equals("processing")) //Only the order that the status
                {
                    //is "processing" can be cancelled.
                    var result =
                        MessageBox.Show($"Are you sure to cancel the reorder request?" +
                                        $"\nReorderID: {selectedReorderID}", "System message", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        stockController.CancelReorder(selectedReorderID); //Cancel the order.

                        MessageBox.Show($"The order {selectedReorderID} has cancelled.",
                            "System message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        dgvReorder.DataSource = stockController.GetReorder(); //Refresh data grid view
                        DgvReorderIndicator();
                    }
                }
                else
                    MessageBox.Show($"The order {selectedReorderID} was finished or cancelled.",
                        "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Order NOT selected.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //
        private void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            //Check if spart part no found or empty DB.
            if (dgvReorder.Rows.Count > 0)
            {
                string ReorderSparePartID = dgvReorder.Rows[reorderIndex].Cells[1].Value.ToString(); //
                string RestockOrderQty = dgvReorder.Rows[reorderIndex].Cells[4].Value.ToString(); //
                string selectedReorderID = dgvReorder.Rows[reorderIndex].Cells[0].Value.ToString(); //

                if (dgvReorder.Rows[reorderIndex].Cells[5].Value.ToString().Equals("processing")) //
                {
                    var result =
                        MessageBox.Show($"Are you sure to confirm reorder request AND restock?" +
                                        $"\nReorderID: {selectedReorderID}\nPart number: {ReorderSparePartID}" +
                                        $"\nRestock quantity: {RestockOrderQty}"
                            , "System message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        stockController.AcceptReorderANDRestock(ReorderSparePartID, RestockOrderQty, selectedReorderID);

                        dgvReorder.DataSource = stockController.GetReorder(); //Refresh data grid view
                        dgvStock.DataSource = stockController.GetPart();
                        DgvIndicator();
                        DgvReorderIndicator();
                    }
                }
                else
                    MessageBox.Show($"The order {selectedReorderID} was finished or cancelled.",
                        "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Reorder ID NOT selected.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //Show or hide the reorder panel.
        private void btnHideReorder_Click(object sender, EventArgs e)
        {
            palOrder.Visible = false;
        }

        private void btnShowReorder_Click(object sender, EventArgs e)
        {
            palOrder.Visible = true;
            dgvReorder.ClearSelection();
            DgvReorderIndicator();
        }


        //Show input box to enter the reorder value.
        private bool ReorderInputBox()
        {
            var reorderMsg = "";

            reorderMsg = Interaction.InputBox($"Please enter the quantity of the reorder request." +
                                              $"\n\nSpartNumber: {lblPartID.Text}\nName: {lblPName.Text}",
                "Reorder request");
            if (!((string)reorderMsg == ""))
            {
                int Qty;
                //Check the inputted value.
                if (!int.TryParse(reorderMsg.ToString(), out Qty) || Qty <= 0 || Qty > 999)
                {
                    var result = MessageBox.Show("Please enter a valid value, Min: 1 Max: 999. Re-enter the value?",
                        "System message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                        ReorderInputBox(); //Request for re-enter the value.
                    else
                        return false;
                }
                else
                    reorderQty = int.Parse(reorderMsg); //Put into reorderQty if is valid.

                return true;
            }

            return false;
        }

        //For print stock card function.
        public void GetPrintArea(Panel pnl)
        {
            MemoryImage = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(MemoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }

        //View, edit, add supplier.
        private void btnViewSupp_Click(object sender, EventArgs e)
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