using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using controller;

namespace templatev1
{
    public partial class staffOrderList : Form
    {
        private string uName, UID;
        AccountController accountController;
        UIController UIController;
        staffOrderListController controller;
        Boolean isManager;

        public staffOrderList()
        {
            InitializeComponent();
            controller = new staffOrderListController();
            lblUid.Text = $"Uid: {UID}";
        }


        public staffOrderList(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new staffOrderListController();
            UID = accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isManager = accountController.CheckIsManager();
        }

        private void staffOrderList_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            palSelect1.Visible =
                           palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Order Management"));
            cmbStatus.SelectedIndex = 0;
            cmbSorting.SelectedIndex = 0;
            load_data(cmbStatus.Text, cmbSorting.Text, isManager);
        }

        public void load_data(string status, string sortBy, bool isManager)
        {
            pnlOrder.Controls.Clear();
            DataTable dt = new DataTable();

            switch (sortBy)
            {
                case "Order ID (Ascending)":
                    dt = controller.getOrder(UID, status, "Id", isManager);
                    break;
                case "Order ID (Descending)":
                    dt = controller.getOrder(UID, status, "IdDESC", isManager);
                    break;
                case "Order Date (Nearest)":
                    dt = controller.getOrder(UID, status, "Date", isManager);
                    break;
                case "Order Date (Furtherest)":
                    dt = controller.getOrder(UID, status, "DateDESC", isManager);
                    break;
                case "Delivery Date (Nearest)":
                    dt = controller.getOrder(UID, status, "DDate", isManager);
                    break;
                case "Delivery Date (Furtherest)":
                    dt = controller.getOrder(UID, status, "DDateDESC", isManager);
                    break;
                case "Customer ID (Ascending)":
                    dt = controller.getOrder(UID, status, "cId", isManager);
                    break;
                case "Customer ID (Descending)":
                    dt = controller.getOrder(UID, status, "cIdDESC", isManager);
                    break;
            }

            int yPosition = 9;
            lblNumberOfOrderShown.Text = $"{dt.Rows.Count}";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string orderDate = dt.Rows[i][1].ToString();
                string[]
                    d = orderDate
                        .Split(' '); //since the database also store the time follwing the date, split it so that only date will be disp;ay
                orderDate = d[0];
                string deliveryDate = dt.Rows[i][3].ToString();
                string[]
                    e = deliveryDate.Split(' ');
                deliveryDate = e[0];

                Label lblOrderID = new Label
                {
                    Name = $"lblOrderID{i}",
                    Text = $"{dt.Rows[i][0]}",
                    Location = new Point(10, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(128, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblOrderDate = new Label
                {
                    Name = $"lblOrderDate{i}",
                    Text = $"{orderDate}",
                    Location = new Point(164, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(153, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblCustomerId = new Label
                {
                    Name = $"lblCustomerId{i}",
                    Text = $"{dt.Rows[i][2]}",
                    Location = new Point(347, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(141, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblDeliveryDate = new Label
                {
                    Name = $"lblDeliveryDate{i}",
                    Text = $"{deliveryDate}",
                    Location = new Point(516, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(152, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblStatus = new Label
                {
                    Name = $"lblStatus{i}",
                    Text = $"{dt.Rows[i][4]}",
                    Location = new Point(697, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(115, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button btnView = new Button
                {
                    Name = $"btnView{i}",
                    Text = "View Order",
                    Location = new Point(835, yPosition - 5),
                    Font = new Font("Microsoft Sans Serif", 12),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                    Cursor = Cursors.Hand
                };
                btnView.Click += btnView_Click;

                pnlOrder.Controls.Add(lblOrderID);
                pnlOrder.Controls.Add(lblOrderDate);
                pnlOrder.Controls.Add(lblCustomerId);
                pnlOrder.Controls.Add(lblDeliveryDate);
                pnlOrder.Controls.Add(lblStatus);
                pnlOrder.Controls.Add(btnView);

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

                    foreach (Control control in pnlOrder.Controls)
                    {
                        if (control.Name == $"lblOrderID{index}")
                        {
                            Form o =
                                new staffViewOrder(control.Text, accountController, UIController);
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

        private void cmbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbStatus.Text, cmbSorting.Text, isManager);
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblHeading.Text = cmbStatus.Text + " Order(s)";
            load_data(cmbStatus.Text, cmbSorting.Text, isManager);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
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