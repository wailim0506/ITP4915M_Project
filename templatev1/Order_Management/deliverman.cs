using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1
{
    public partial class deliverman : Form
    {
        controller.AccountController accountController;
        controller.UIController UIController;
        controller.delivermanOrderListController controller;
        private string uName, UID;

        public deliverman()
        {
            InitializeComponent();
            UID = "LMS00009"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
            controller = new controller.delivermanOrderListController();
        }

        public deliverman(controller.AccountController accountController,
            controller.UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new controller.delivermanOrderListController();
            UID = this.accountController.GetUid();


            lblUid.Text = $"Uid: {UID}";
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_data("");
        }

        public void load_data(string sortBy)
        {
            pnlOrder.Controls.Clear();
            DataTable dt = controller.getAllOrder(UID, "");

            int yPosition = 9;

            //create label
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label lblRowNum = new Label
                {
                    Text = $"{(i + 1)}{"."} ",
                    Location = new Point(10, yPosition),
                    Font = new Font("Times New Roman", 15),
                    Size = new Size(30, 22),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblOrderID = new Label
                {
                    Name = $"lblOrderID{i}",
                    Text = $"{dt.Rows[i][0]}",
                    Location = new Point(46, yPosition),
                    Font = new Font("Times New Roman", 15),
                    Size = new Size(129, 22),
                    TextAlign = ContentAlignment.MiddleCenter
                };


                string orderDate = dt.Rows[i][2].ToString();
                string[]
                    d = orderDate
                        .Split(' '); //since the database also store the time follwing the date, split it so that only date will be disp;ay
                orderDate = d[0];

                Label lblDate = new Label
                {
                    Name = $"lblDate{i}",
                    Text = $"{orderDate}",
                    Location = new Point(181, yPosition),
                    Font = new Font("Times New Roman", 15),
                    Size = new Size(150, 22),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblAddress = new Label
                {
                    Name = $"lblAddress{i}",
                    Text = $"{dt.Rows[i][5]}",
                    Location = new Point(337, yPosition),
                    Font = new Font("Times New Roman", 15),
                    Size = new Size(489, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Button btnView = new Button
                {
                    Name = $"btnView{i}",
                    Text = "View Order",
                    Location = new Point(832, yPosition - 4),
                    Font = new Font("Times New Roman", 13),
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true
                };
                //btnView.Click += btnView_Click;
                pnlOrder.Controls.Add(lblRowNum);
                pnlOrder.Controls.Add(lblOrderID);
                pnlOrder.Controls.Add(lblDate);
                pnlOrder.Controls.Add(lblAddress);
                pnlOrder.Controls.Add(btnView);


                yPosition += 50;
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }
    }
}