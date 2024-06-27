using System;
using System.Drawing;
using System.Dynamic;
using System.Windows.Forms;
using LMCIS.controller;
using LMCIS.Online_Ordering_Platform;
using LMCIS.Order_Management;
using LMCIS.Profile;
using LMCIS.Stock_Manag;
using LMCIS.System_page;
using LMCIS.User_Manag;

namespace LMCIS.On_Sale_Product_Manag
{
    public partial class OnSaleDis : Form
    {
        private string uName, UID, selectedDiscountID;
        private int index;
        bool create = false;
        dynamic discountValue;
        AccountController accountController;
        UIController UIController;
        OnSaleProductController onSaleProductController;


        public OnSaleDis(AccountController accountController, UIController UIController,
            OnSaleProductController onSaleProductController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            this.onSaleProductController = onSaleProductController;
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
            lblTitTotalNoItem.Text = "No. of discounts in the system: " + onSaleProductController.GetTotalDiscountQty();

            dgvDiscount.DataSource = onSaleProductController.GetDiscount();
            dgvDiscount.ClearSelection();
            //DgvIndicator();
            dgvDiscount.ColumnHeadersDefaultCellStyle.Font
                = new Font("Times New Roman", 13F, FontStyle.Bold);

            CreateDiscount();


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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            getPage("On-Sale Product Management");
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

        private void btnSetModify_Click(object sender, EventArgs e)
        {
            lblDateMsg.Text = lblPerMsg.Text = lblRangeMsg.Text = "";

            if (create) //Create a new discount.
            {
                if (CheckCreate())
                {
                    setValue();
                    discountValue.postDate = dtpPostDate.Value.ToString("yyyy-MM-dd");
                    if (onSaleProductController.CreateDiscount(discountValue))
                    {
                        MessageBox.Show(
                            $"Create new discount success! New discountID is {lblDiscountID.Text}"
                            , "System message", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        dgvDiscount.DataSource = onSaleProductController.GetDiscount();
                        dgvDiscount.ClearSelection();
                    }
                    else
                        MessageBox.Show("System Error! Please Contact The Help Desk.", "System error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                }
            }
            else //Modify current discount.
            {
                if (CheckModify())
                {
                    setValue();
                    if (onSaleProductController.ModifyDiscount(discountValue))
                    {
                        MessageBox.Show("Modify discount success!", "System message", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        dgvDiscount.DataSource = onSaleProductController.GetDiscount();
                        dgvDiscount.ClearSelection();
                    }
                    else
                        MessageBox.Show("System Error! Please Contact The Help Desk.", "System error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvDiscount_MouseClick(object sender, MouseEventArgs e)
        {
            create = false;
            palCreateDis.Visible = create;
            btnSetModify.Text = "Modify";
            if (dgvDiscount.Rows.Count > 0)
            {
                dgvDiscount.ClearSelection();
                index = dgvDiscount.CurrentCell.RowIndex;

                //Select the whole row.
                for (int r = 0; r < dgvDiscount.ColumnCount; r++)
                    dgvDiscount[r, index].Selected = true;
                selectedDiscountID =
                    dgvDiscount.Rows[index].Cells[0].Value.ToString(); //Get the item ID for the selected row.

                //Set value to stockInfo.
                lblDiscountID.Text = onSaleProductController.GetDiscountInfo(selectedDiscountID).discountID;
                lblPostDate.Text = onSaleProductController.GetDiscountInfo(selectedDiscountID).createDate
                    .ToString("yyyy/MM/dd");
                dtpEndDate.Value =
                    DateTime.ParseExact(
                        onSaleProductController.GetDiscountInfo(selectedDiscountID).endDate.ToString("dd/MM/yyyy"),
                        "dd/MM/yyyy", null);
                tbPercentage.Text = onSaleProductController.GetDiscountInfo(selectedDiscountID).percentage;
                tbRange.Text = onSaleProductController.GetDiscountInfo(selectedDiscountID).discountRange;
            }
            else //There has not any record in the database.
                MessageBox.Show("Discount NOT found.",
                    "System message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CreateDiscount()
        {
            create = true;
            palCreateDis.Visible = create;
            tbPercentage.Text = tbRange.Text = "";
            btnSetModify.Text = "Set";
            dtpEndDate.Value = DateTime.Now.AddDays(2);

            lblDiscountID.Text = onSaleProductController.GetDiscountID();
            selectedDiscountID = null;
            dgvDiscount.ClearSelection();
        }

        private void dgvDiscount_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CreateDiscount();
        }

        private bool CheckModify()
        {
            //Check range.
            int Range;
            if (!int.TryParse(tbRange.Text.ToString(), out Range) || Range <= 0 || Range > 999)
            {
                lblRangeMsg.Text = "minimum 1 maximum 999.";
                tbRange.Select();
                return false;
            }

            //Check percentage.
            int percentage;
            if (!int.TryParse(tbPercentage.Text.ToString(), out percentage) || percentage <= 0 || percentage > 100)
            {
                lblPerMsg.Text = "minimum 1 maximum 100.";
                tbPercentage.Select();
                return false;
            }

            //Check endDate
            DateTime postDate = onSaleProductController.GetDiscountInfo(selectedDiscountID).createDate;
            DateTime endDate = dtpEndDate.Value;
            if (endDate < postDate)
            {
                lblDateMsg.Text = "End date CANNOT eary than post date.";
                dtpEndDate.Select();
                return false;
            }

            return true;
        }

        private bool CheckCreate()
        {
            //Check range.
            int Range;
            if (!int.TryParse(tbRange.Text.ToString(), out Range) || Range <= 0 || Range > 999)
            {
                lblRangeMsg.Text = "minimum 1 maximum 999.";
                tbRange.Select();
                return false;
            }

            //Check percentage.
            int percentage;
            if (!int.TryParse(tbPercentage.Text.ToString(), out percentage) || percentage <= 0 || percentage > 100)
            {
                lblPerMsg.Text = "minimum 1 maximum 100.";
                tbPercentage.Select();
                return false;
            }

            //Check postDate
            DateTime postDate = dtpPostDate.Value;
            if (postDate < DateTime.Now)
            {
                lblDateMsg.Text = "Post date CANNOT be today OR later than today.";
                dtpEndDate.Select();
                return false;
            }

            //Check endDate
            DateTime endDate = dtpEndDate.Value;
            if (endDate <= postDate)
            {
                lblDateMsg.Text = "End date CANNOT eary than post date.";
                dtpEndDate.Select();
                return false;
            }

            return true;
        }

        private void setValue()
        {
            discountValue = new ExpandoObject();
            discountValue.discountID = lblDiscountID.Text;
            discountValue.endDate = dtpEndDate.Value.ToString("yyyy-MM-dd");
            discountValue.range = tbRange.Text;
            discountValue.percentage = tbPercentage.Text;
        }
    }
}