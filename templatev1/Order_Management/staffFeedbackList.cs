using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using controller.Utilities;
using controller;

namespace templatev1
{
    public partial class staffFeedbackList : Form
    {
        private string uName, UID;
        AccountController accountController;
        UIController UIController;
        feedbackController controller;
        bool isManager;
        public staffFeedbackList()
        {
            InitializeComponent();
        }

        

        public staffFeedbackList(AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new feedbackController();
            UID = accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isManager = accountController.CheckIsManager();
        }

        private void staffFeedbackList_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_data();
            palSelect1.Visible =
               palSelect2.Visible = palSelect3.Visible = palSelect4.Visible = palSelect5.Visible = false;
            hideButton();
            setIndicator(UIController.getIndicator("Order Management"));

        }

        public void load_data()
        {
            pnlFeedback.Controls.Clear();
            DataTable dt = controller.getAllFeedback(isManager,UID);

            int yPosition = 9;
            lblNumberOfFeedbackShown.Text = dt.Rows.Count.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string feedbackDate = dt.Rows[i][4].ToString();
                string[]
                    d = feedbackDate
                        .Split(' '); //since the database also store the time follwing the date, split it so that only date will be disp;ay
                feedbackDate = d[0];

                string orderID = "N/A";
              
                if (dt.Rows[i][2].ToString() == "")
                {
                    orderID = "N/A";
                }
                else
                {
                    orderID = dt.Rows[i][2].ToString();
                }

                Label lblRowNum = new Label
                {
                    Text = $"{i+1}.",
                    Location = new Point(0, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(43, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblFeedbackID = new Label
                {
                    Name = $"lblFeedbackID{i}",
                    Text = $"{dt.Rows[i][0]}",
                    Location = new Point(55, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(151, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblcustomerID = new Label
                {
                    Text = $"{dt.Rows[i][1]}",
                    Location = new Point(212, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(140, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                
                Label lblfeedBackDate = new Label
                {
                    Text = $"{feedbackDate}",
                    Location = new Point(358, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(175, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblOrderID = new Label
                {
                    Text = $"{orderID}",
                    Location = new Point(539, yPosition),
                    Font = new Font("Times New Roman", 12),
                    Size = new Size(142, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button btnView = new Button
                {
                    Name = $"btnView{i}",
                    Text = "View",
                    Location = new Point(687, yPosition - 3),
                    Font = new Font("Times New Roman", 12),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                    Cursor = Cursors.Hand
                };
                btnView.Click += btnView_Click;

                yPosition += 50;
                pnlFeedback.Controls.Add(lblRowNum);
                pnlFeedback.Controls.Add(lblFeedbackID);
                pnlFeedback.Controls.Add(lblcustomerID);
                pnlFeedback.Controls.Add(lblfeedBackDate);
                pnlFeedback.Controls.Add(lblOrderID);
                pnlFeedback.Controls.Add(btnView);
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

                    foreach (Control control in pnlFeedback.Controls)
                    {
                        if (control.Name == $"lblFeedbackID{index}")
                        {
                            Form o =
                                new staffViewFeedback(accountController, UIController, control.Text);
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

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form o = new Login();
            Hide();
            o.StartPosition = FormStartPosition.Manual;
            o.Location = Location;
            o.ShowDialog();
            Close();
        }
    }
}
