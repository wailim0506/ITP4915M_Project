using System.Windows.Forms;
using controller;

namespace templatev1
{
    public partial class DID_List : Form
    {
        AccountController accountController;
        UIController UIController;
        didListController controller;
        private string uName, UID;
        string orderID;
        bool isManager;
        public DID_List(string orderID)
        {
            InitializeComponent();
            controller = new didListController();
            this.orderID = orderID;
            isManager = false;
        }

        public DID_List(string orderID, AccountController accountController,
            UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new didListController();
            UID = this.accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            lblLoc.Text += $" {orderID}";
            lblHeading.Text += $" {orderID}";
            isManager = accountController.CheckIsManager();
        }


        private void DID_List_Load(object sender, EventArgs e)
        {
            if (!isManager)
            {
                hideButton();
            }
            timer1.Enabled = true;
            cmbSorting.SelectedIndex = 0;
            load_data(cmbSorting.Text);
        }

        public void load_data(string sortBy) {
            pnlPartList.Controls.Clear();
            DataTable dt = controller.getData(sortBy,orderID);

            int yPosition = 8;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label lblRowNum = new Label
                {
                    Text = $"{(i+1)}.",
                    Location = new Point(3, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(41, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblPartNum = new Label
                {
                    Name = $"lblPartNum{i}",
                    Text = $"{dt.Rows[i][0]}",
                    Location = new Point(50, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(128, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblPartName = new Label
                {
                    Text = $"{dt.Rows[i][1]}",
                    Location = new Point(188, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(307, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblQty = new Label
                {
                    Text = $"{dt.Rows[i][2]}",
                    Location = new Point(507, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(128, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button btnView = new Button
                {
                    Name = $"btnView{i}",
                    Text = "View DID",
                    Location = new Point(647, yPosition - 2),
                    Font = new Font("Microsoft Sans Serif", 12),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(116, 25),
                    Cursor = Cursors.Hand
                };
                btnView.Click += btnView_Click;

                yPosition += 50;

                pnlPartList.Controls.Add(lblRowNum);
                pnlPartList.Controls.Add(lblPartNum);
                pnlPartList.Controls.Add(lblPartName);
                pnlPartList.Controls.Add(lblQty);
                pnlPartList.Controls.Add(btnView);

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

                    foreach (Control control in pnlPartList.Controls)
                    {
                        if (control.Name == $"lblPartNum{index}")
                        {
                            Form o =
                                new DID(orderID,control.Text, accountController, UIController);
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

        public void hideButton()
        {
            palSelect3.Visible = false;
            btnFunction3.Visible = false;
            palSelect4.Visible = false;
            btnFunction4.Visible = false;
            btnFunction5.Location = new Point(0, 233);
            btnFunction5.Controls.Add(palSelect5);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form o = new staffViewOrder(orderID, accountController, UIController);
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
            return;
        }

        private void cmbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_data(cmbSorting.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }
    }
}