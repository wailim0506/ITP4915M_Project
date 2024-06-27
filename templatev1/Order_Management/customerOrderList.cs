using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LMCIS.controller;
using LMCIS.On_Sale_Product_Manag;
using LMCIS.Online_Ordering_Platform;
using LMCIS.Profile;
using LMCIS.Properties;
using LMCIS.Stock_Manag;
using LMCIS.System_page;
using LMCIS.User_Manag;
using LMCIS.Properties;

namespace LMCIS.Order_Management
{
    public partial class customerOrderList : Form
    {
        DataTable dtOrder, dtStaff;
        private string uName, UID;
        AccountController accountController;
        UIController UIController;
        orderListController controller;

        public customerOrderList()
        {
            InitializeComponent();
            controller = new orderListController();
            lblUid.Text = $"Uid: {UID}";
        }

        public customerOrderList(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new orderListController();
            UID = accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("yyyy/MM/dd   HH:mm:ss");
        }

        private void customerOrderList_Load(object sender, EventArgs e)
        {
            palSelect1.Visible =
                palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Order Management"));
            timer1.Enabled = true;
            cmbSortOrder.SelectedIndex = 0;
            load_data(cmbSortOrder.Text);
        }

        public void load_data(string sortBy)
        {
            pnlOrder.Controls.Clear();
            int numOfOrder = controller.CountOrder(UID, sortBy, tbKW.Text);
            dtOrder = controller.GetOrder(UID, sortBy, tbKW.Text);

            //create label
            int yPosition = 6;
            lblNumberOfOrderShown.Text = $"{dtOrder.Rows.Count}";
            for (int i = 1; i <= numOfOrder; i++)
            {
                string staffAccountID = dtOrder.Rows[i - 1][2].ToString();
                string orderDate = dtOrder.Rows[i - 1][4].ToString();
                string[]
                    d = orderDate
                        .Split(' '); //since the database also store the time following the date, split it so that only date will be disp;ay
                orderDate = d[0];

                Label lblID = new Label
                {
                    Name = $"lblID{i}", Text = $"{dtOrder.Rows[i - 1][0]}",
                    Location = new Point(10, yPosition), Font = new Font("Times New Roman", 12),
                    Size = new Size(109, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblDate = new Label
                {
                    Name = $"lblDate{i}", Text = $"{orderDate}", Location = new Point(125, yPosition),
                    Font = new Font("Times New Roman", 12), Size = new Size(112, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblStaff = new Label
                {
                    Name = $"lblStaff{i}", Text = controller.GetStaffName(staffAccountID),
                    Location = new Point(243, yPosition), Font = new Font("Times New Roman", 12),
                    Size = new Size(180, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblContact = new Label
                {
                    Name = $"lblContact{i}", Text = controller.GetStaffContact(staffAccountID),
                    Location = new Point(429, yPosition), Font = new Font("Times New Roman", 12),
                    Size = new Size(219, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblStatus = new Label
                {
                    Name = $"lblStatus{i}", Text = $"{dtOrder.Rows[i - 1][6]}",
                    Location = new Point(654, yPosition), Font = new Font("Times New Roman", 12),
                    Size = new Size(115, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Button btnView = new Button
                {
                    Name = $"btnView{i}", Text = "View Order", Location = new Point(810, yPosition - 3),
                    Font = new Font("Times New Roman", 12), TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true, Cursor = Cursors.Hand
                };
                btnView.Click += btnView_Click;

                pnlOrder.Controls.Add(lblID);
                pnlOrder.Controls.Add(lblDate);
                pnlOrder.Controls.Add(lblStaff);
                pnlOrder.Controls.Add(lblContact);
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
                        if (control.Name == $"lblID{index}")
                        {
                            Form customerViewOrder =
                                new customerViewOrder(control.Text, accountController, UIController);
                            Hide();
                            customerViewOrder.StartPosition = FormStartPosition.Manual;
                            customerViewOrder.Location = Location;
                            customerViewOrder.ShowDialog();
                            Close();
                            return;
                        }

                        ++i;
                    }
                }
            }
        }

        private void cmbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sortBy = cmbSortOrder.Text;
            load_data(sortBy);
        }

        private int getIndex(string btnName)
        {
            int i = 1;
            while (true)
            {
                if (btnName == $"btnView{i}")
                {
                    return i;
                }

                i++;
            }
        }



        private void BWMode()
        {
            dynamic value = UIController.getMode();
            Settings.Default.textColor = ColorTranslator.FromHtml(value.textColor);
            Settings.Default.bgColor = ColorTranslator.FromHtml(value.bgColor);
            Settings.Default.navBarColor = ColorTranslator.FromHtml(value.navBarColor);
            Settings.Default.navColor = ColorTranslator.FromHtml(value.navColor);
            Settings.Default.timeColor = ColorTranslator.FromHtml(value.timeColor);
            Settings.Default.locTbColor = ColorTranslator.FromHtml(value.locTbColor);
            Settings.Default.logoutColor = ColorTranslator.FromHtml(value.logoutColor);
            Settings.Default.profileColor = ColorTranslator.FromHtml(value.profileColor);
            Settings.Default.btnColor = ColorTranslator.FromHtml(value.btnColor);
            Settings.Default.BWmode = value.BWmode;
            if (Settings.Default.BWmode)
            {
                picBWMode.Image = Resources.LBWhite;
                picHome.Image = Resources.homeWhite;
            }
            else
            {
                picBWMode.Image = Resources.LB;
                picHome.Image = Resources.home;
            }
        }

        private void tbKW_TextChanged(object sender, EventArgs e)
        {
            load_data(cmbSortOrder.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbKW.Text = "";
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

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            getPage(btnFunction1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            getPage(btnFunction4.Text);
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            getPage(btnFunction3.Text);
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            getPage(btnFunction2.Text);
        }

        private void btnFunction5_Click(object sender, EventArgs e)
        {
            getPage(btnFunction5.Text);
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


        private void picHome_Click_1(object sender, EventArgs e)
        {
            Form home = new Home(accountController, UIController);
            Hide();
            //Swap the current form to another.
            home.StartPosition = FormStartPosition.Manual;
            home.Location = Location;
            home.ShowDialog();
            Close();
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