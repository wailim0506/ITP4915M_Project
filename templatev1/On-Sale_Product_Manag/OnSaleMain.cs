using System;
using System.Windows.Forms;
using controller;
using System.Data;
using System.Drawing;

namespace templatev1
{
    public partial class OnSaleMain : Form
    {
        private string uName, UID;
        AccountController accountController;
        UIController UIController;
        productListController controller;


        public OnSaleMain()
        {
            InitializeComponent();
        }

        public OnSaleMain(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new productListController();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }


        private void OnSaleMain_Load(object sender, EventArgs e)
        {
            Initialization();
            cmbCategory.SelectedIndex = 0;
            cmbKW.SelectedIndex = 0;
            cmbSorting.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;


            load_data(cmbKW.Text, tbKw.Text, cmbCategory.Text, cmbStatus.Text, cmbSorting.Text);
        }

        private void Initialization()
        {
            setIndicator(UIController.getIndicator("On-Sale Product Management"));
            timer1.Enabled = true;
            UID = accountController.GetUid();
            uName = accountController.GetName();
            lblUid.Text = "UID: " + UID;


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

        public void load_data(string kwType, string kw, string category, string status, string sortBy)
        {
            pnlProduct.Controls.Clear();
            DataTable dt = new DataTable();
            switch (sortBy)
            {
                case "None":
                    dt = controller.getProductList(kwType, kw, category, status, "");
                    break;
                case "Item No. (Ascending)":
                    dt = controller.getProductList(kwType, kw, category, status, "ItemNo");
                    break;
                case "Item No. (Descending)":
                    dt = controller.getProductList(kwType, kw, category, status, "ItemNoDSEC");
                    break;
                case "Part No. (Ascending)":
                    dt = controller.getProductList(kwType, kw, category, status, "PartNo");
                    break;
                case "Part No. (Descending)":
                    dt = controller.getProductList(kwType, kw, category, status, "PartNoDESC");
                    break;
                case "Non-LM Qty (Ascending)":
                    dt = controller.getProductList(kwType, kw, category, status, "NonLMQty");
                    break;
                case "Non-LM Qty (Descending)":
                    dt = controller.getProductList(kwType, kw, category, status, "NonLMQtyDESC");
                    break;
                case "LM Qty (Ascending)":
                    dt = controller.getProductList(kwType, kw, category, status, "LMQty");
                    break;
                case "LM Qty (Descending)":
                    dt = controller.getProductList(kwType, kw, category, status, "LMQtyDESC");
                    break;
                case "Stock Qty (Ascending)":
                    dt = controller.getProductList(kwType, kw, category, status, "StockQty");
                    break;
                case "Stock Qty (Descending)":
                    dt = controller.getProductList(kwType, kw, category, status, "StockQtyDESC");
                    break;
                case "Price (Ascending)":
                    dt = controller.getProductList(kwType, kw, category, status, "Price");
                    break;
                case "Price (Descending)":
                    dt = controller.getProductList(kwType, kw, category, status, "PriceDESC");
                    break;
            }

            int yPosition = 8;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label lblRowNum = new Label
                {
                    Text = $"{i + 1}",
                    Location = new Point(3, yPosition),
                    Font = new Font("Times New Roman", 13),
                    Size = new Size(47, 21),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblCategory = new Label
                {
                    Text = $"{dt.Rows[i][1]}",
                    Location = new Point(56, yPosition),
                    Font = new Font("Times New Roman", 13),
                    Size = new Size(97, 21),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblItemNum = new Label
                {
                    Name = $"lblItemNum{i}",
                    Text = $"{dt.Rows[i][0]}",
                    Location = new Point(159, yPosition),
                    Font = new Font("Times New Roman", 13),
                    Size = new Size(97, 21),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblPartNum = new Label
                {
                    Text = $"{dt.Rows[i][2]}",
                    Location = new Point(262, yPosition),
                    Font = new Font("Times New Roman", 13),
                    Size = new Size(111, 21),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblName = new Label
                {
                    Text = $"{dt.Rows[i][11]}",
                    Location = new Point(379, yPosition),
                    Font = new Font("Times New Roman", 13),
                    Size = new Size(217, 21),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblNonLMQty = new Label
                {
                    Text = $"{dt.Rows[i][3]}",
                    Location = new Point(602, yPosition),
                    Font = new Font("Times New Roman", 13),
                    Size = new Size(168, 21),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblLMQty = new Label
                {
                    Text = $"{dt.Rows[i][4]}",
                    Location = new Point(776, yPosition),
                    Font = new Font("Times New Roman", 13),
                    Size = new Size(132, 21),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblStockQty = new Label
                {
                    Text = $"{dt.Rows[i][10]}",
                    Location = new Point(914, yPosition),
                    Font = new Font("Times New Roman", 13),
                    Size = new Size(94, 21),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPrice = new Label
                {
                    Text = $"{dt.Rows[i][6]}",
                    Location = new Point(1014, yPosition),
                    Font = new Font("Times New Roman", 13),
                    Size = new Size(79, 21),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblStatus = new Label
                {
                    Text = $"{dt.Rows[i][9]}",
                    Location = new Point(1099, yPosition),
                    Font = new Font("Times New Roman", 13),
                    Size = new Size(77, 21),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button btnView = new Button
                {
                    Name = $"btnView{i}",
                    Text = "Modify",
                    Location = new Point(1175, yPosition - 2),
                    Font = new Font("Times New Roman", 11),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                    Cursor = Cursors.Hand
                };
                btnView.Click += btnView_Click;

                yPosition += 50;

                pnlProduct.Controls.Add(lblRowNum);
                pnlProduct.Controls.Add(lblCategory);
                pnlProduct.Controls.Add(lblItemNum);
                pnlProduct.Controls.Add(lblPartNum);
                pnlProduct.Controls.Add(lblName);
                pnlProduct.Controls.Add(lblNonLMQty);
                pnlProduct.Controls.Add(lblLMQty);
                pnlProduct.Controls.Add(lblStockQty);
                pnlProduct.Controls.Add(lblPrice);
                pnlProduct.Controls.Add(lblStatus);
                pnlProduct.Controls.Add(btnView);
            }
        }

        public void btnView_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string buttonName = clickedButton.Name;
                int index = getIndex(buttonName);
                if (index != -1)
                {
                    int i = 0;

                    foreach (Control control in pnlProduct.Controls)
                    {
                        if (control.Name == $"lblItemNum{index}")
                        {
                            //Form o =
                            //    new staffViewOrder(control.Text, accountController, UIController);
                            //Hide();
                            //o.StartPosition = FormStartPosition.Manual;
                            //o.Location = Location;
                            //o.ShowDialog();
                            //Close();
                            //return;
                        }

                        ++i;
                    }
                }
            }
        }

        private int getIndex(string btnName)
        {
            int i = 0;
            while (true)
            {
                if (btnName == $"btnView{i}")
                {
                    return i;
                }

                i++;
            }
        }

        private void cmbKW_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbKW.Text, tbKw.Text, cmbCategory.Text, cmbStatus.Text, cmbSorting.Text);
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbKW.Text, tbKw.Text, cmbCategory.Text, cmbStatus.Text, cmbSorting.Text);
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbKW.Text, tbKw.Text, cmbCategory.Text, cmbStatus.Text, cmbSorting.Text);
        }

        private void cmbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbKW.Text, tbKw.Text, cmbCategory.Text, cmbStatus.Text, cmbSorting.Text);
        }

        private void tbKw_TextChanged(object sender, EventArgs e)
        {
            load_data(cmbKW.Text, tbKw.Text, cmbCategory.Text, cmbStatus.Text, cmbSorting.Text);
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


        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Form OnSaleAdd = new OnSaleAdd();
            Hide();
            //Swap the current form to another.
            OnSaleAdd.StartPosition = FormStartPosition.Manual;
            OnSaleAdd.Location = Location;
            OnSaleAdd.Size = Size;
            OnSaleAdd.ShowDialog();
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
                        next = new templatev1.staffOrderList(accountController, UIController);
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
            next.ShowDialog();
            Close();
        }
    }
}