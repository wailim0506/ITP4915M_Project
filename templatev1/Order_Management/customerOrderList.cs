using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using controller;
using templatev1.Order_Management;
using templatev1.Properties;

namespace templatev1.Online_Ordering_Platform
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
            UID = "LMC00001"; //hard code for testing
            //UID = "LMC00003"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
        }

        public customerOrderList(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new orderListController();
            UID = accountController.GetUid();
            //UID = "LMC00001"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void customerOrderList_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_data("All");
            cmbSortOrder.SelectedIndex = 0;
        }

        public void load_data(string sortBy)
        {
            pnlOrder.Controls.Clear();
            int numOfOrder = controller.countOrder(UID, sortBy);
            dtOrder = controller.getOrder(UID, sortBy);

            //create label
            int yPosition = 6;
            for (int i = 1; i <= numOfOrder; i++)
            {
                string staffAccountID = dtOrder.Rows[i - 1][2].ToString();
                string orderDate = dtOrder.Rows[i - 1][4].ToString();
                string[]
                    d = orderDate
                        .Split(' '); //since the database also store the time follwing the date, split it so that only date will be disp;ay
                orderDate = d[0];

                Label lblID = new Label
                {
                    Name = $"lblID{i}", Text = $"{dtOrder.Rows[i - 1][0]}",
                    Location = new Point(10, yPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(109, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblDate = new Label
                {
                    Name = $"lblDate{i}", Text = $"{orderDate}", Location = new Point(125, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12), Size = new Size(112, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblStaff = new Label
                {
                    Name = $"lblStaff{i}", Text = controller.getStaffName(staffAccountID),
                    Location = new Point(243, yPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(180, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblContact = new Label
                {
                    Name = $"lblContact{i}", Text = controller.getStaffContact(staffAccountID),
                    Location = new Point(429, yPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(219, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblStatus = new Label
                {
                    Name = $"lblStatus{i}", Text = $"{dtOrder.Rows[i - 1][6]}",
                    Location = new Point(654, yPosition), Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(115, 20), TextAlign = ContentAlignment.MiddleCenter
                };
                Button btnView = new Button
                {
                    Name = $"btnView{i}", Text = "View Order", Location = new Point(810, yPosition - 3),
                    Font = new Font("Microsoft Sans Serif", 12), TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true
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

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            Form o = new customerOrderList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            Form o = new sparePartList(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            Form o = new cart(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void btnFunction4_Click(object sender, EventArgs e)
        {
            Form o = new favourite(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form o = new giveFeedback(accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
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

        private void picBWMode_Click(object sender, EventArgs e)
        {
            BWMode();
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
    }
}