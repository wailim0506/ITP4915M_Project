using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using controller;

namespace templatev1
{
    public partial class staffInvoiceList : Form
    {
        private string uName, UID;
        AccountController accountController;
        UIController UIController;
        staffInvoiceListController controller;
        Boolean isManager;

        public staffInvoiceList()
        {
            InitializeComponent();
            controller = new staffInvoiceListController();
            lblUid.Text = $"Uid: {UID}";
        }

        public staffInvoiceList(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new staffInvoiceListController();
            UID = accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isManager = accountController.CheckIsManager();
        }

        private void staffInvoiceList_Load(object sender, EventArgs e)
        {
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Invoice Management"));
            cmbStatus.SelectedIndex = 0;
            cmbSorting.SelectedIndex = 0;
            load_data(cmbSorting.Text, cmbStatus.Text);
        }

        public void load_data(string sortBy, string status)
        {
            pnl_Invoice.Controls.Clear();
            DataTable dt = new DataTable();

            switch (sortBy)
            {
                case "Invoice Number (Ascending)":
                    dt = controller.getData(UID, "IN", status, isManager);
                    break;
                case "Invoice Number (Descending)":
                    dt = controller.getData(UID, "INDESC", status, isManager);
                    break;
                case "Order Date (Nearest)":
                    dt = controller.getData(UID, "ODate", status, isManager);
                    break;
                case "Order Date (Furtherest)":
                    dt = controller.getData(UID, "ODateDESC", status, isManager);
                    break;
                case "Delivery Date (Nearest)":
                    dt = controller.getData(UID, "DDate", status, isManager);
                    break;
                case "Delivery Date (Furtherest)":
                    dt = controller.getData(UID, "DDateDESC", status, isManager);
                    break;
                case "Order ID (Ascending)":
                    dt = controller.getData(UID, "OID", status, isManager);
                    break;
                case "Order ID (Descending)":
                    dt = controller.getData(UID, "OIDDESC", status, isManager);
                    break;
                default:
                    dt = controller.getData(UID, "", status, isManager);
                    break;
            }


            int yPosition = 9;
            lblNumberOfInvoiceShown.Text = $"{dt.Rows.Count}";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string orderDate = dt.Rows[i][3].ToString();
                string[]
                    d = orderDate
                        .Split(' '); //since the database also store the time follwing the date, split it so that only date will be disp;ay
                orderDate = d[0];
                string deliveryDate = dt.Rows[i][4].ToString();
                string[]
                    e = deliveryDate.Split(' ');
                deliveryDate = e[0];
                string Invoicestatus = dt.Rows[i][2].ToString();
                if (Invoicestatus == "")
                {
                    Invoicestatus = "Not Confirm";
                }
                else
                {
                    Invoicestatus = "Confirmed";
                }

                Label lblRowNum = new Label
                {
                    Text = $"{(i + 1)}.",
                    Location = new Point(3, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(50, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblInvoiceNumebr = new Label
                {
                    Text = $"{dt.Rows[i][1]}",
                    Location = new Point(54, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(141, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblOrderID = new Label
                {
                    Name = $"lblOrderID{i}",
                    Text = $"{dt.Rows[i][0]}",
                    Location = new Point(201, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(163, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblOrderDate = new Label
                {
                    Text = $"{orderDate}",
                    Location = new Point(370, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(153, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblDeliveryDate = new Label
                {
                    Text = $"{deliveryDate}",
                    Location = new Point(529, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(152, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblStatus = new Label
                {
                    Text = Invoicestatus,
                    Location = new Point(687, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(151, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button btnView = new Button
                {
                    Name = $"btnView{i}",
                    Text = "View",
                    Location = new Point(845, yPosition - 5),
                    Font = new Font("Times New Roman", 12),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                    Cursor = Cursors.Hand
                };

                btnView.Click += btnView_Click;

                pnl_Invoice.Controls.Add(lblRowNum);
                pnl_Invoice.Controls.Add(lblInvoiceNumebr);
                pnl_Invoice.Controls.Add(lblOrderID);
                pnl_Invoice.Controls.Add(lblOrderDate);
                pnl_Invoice.Controls.Add(lblDeliveryDate);
                pnl_Invoice.Controls.Add(lblStatus);
                pnl_Invoice.Controls.Add(btnView);

                yPosition += 50;
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

                    foreach (Control control in pnl_Invoice.Controls)
                    {
                        if (control.Name == $"lblOrderID{index}")
                        {
                            Form o =
                                new staffViewInvoice(control.Text, accountController, UIController, true);
                            Hide();
                            o.StartPosition = FormStartPosition.Manual;
                            o.Location = Location;
                            o.ShowDialog();
                            Close();
                            return;
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

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbSorting.Text, cmbStatus.Text);
        }

        private void cmbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbSorting.Text, cmbStatus.Text);
        }

        public void hideButton()
        {
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

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            Form o =
                new staffOrderList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            Form o =
                new staffInvoiceList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
        }

        //TODO:On-Sale Product Management
        private void btnFunction3_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form o = new Login();
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
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
            proFile.ShowDialog();
            Close();
        }

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            Form proFile = new SAccManage(accountController, UIController);
            Hide();
            //Swap the current form to another.
            proFile.StartPosition = FormStartPosition.Manual;
            proFile.Location = Location;
            proFile.ShowDialog();
            Close();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            Form home = new Home(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.ShowDialog();
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
    }
}