using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using controller;

namespace templatev1.Order_Management
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
            UID = "LMS00001"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
        }


        public staffOrderList(AccountController accountController, UIController UIController) {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new staffOrderListController();
            UID = accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isManager = accountController.checkIsManager();
        }

        private void staffOrderList_Load(object sender, EventArgs e)
        {
            cmbStatus.SelectedIndex = 0;
            cmbSorting.SelectedIndex = 0;
            load_data(cmbStatus.Text, cmbSorting.Text, isManager);
        }

        public void load_data(string status, string sortBy, bool isManager) {
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
                    dt = controller.getOrder(UID, status, "", isManager);
                    break;
            }

            int yPosition = 9;
            for (int i = 0; i < dt.Rows.Count; i++) {

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
                    Size = new Size(109, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblOrderDate = new Label
                {
                    Name = $"lblOrderDate{i}",
                    Text = $"{orderDate}",
                    Location = new Point(125, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(112, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblCustomerId = new Label
                {
                    Name = $"lblCustomerId{i}",
                    Text = $"{dt.Rows[i][2]}",
                    Location = new Point(125, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(112, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblDeliveryDate = new Label
                {
                    Name = $"lblDeliveryDate{i}",
                    Text = $"{deliveryDate}",
                    Location = new Point(125, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(112, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblStatus = new Label
                {
                    Name = $"lblStatus{i}",
                    Text = $"{dt.Rows[i][4]}",
                    Location = new Point(125, yPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(112, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                pnlOrder.Controls.Add(lblOrderID);
                pnlOrder.Controls.Add(lblOrderDate);
                pnlOrder.Controls.Add(lblCustomerId);
                pnlOrder.Controls.Add(lblDeliveryDate);
                pnlOrder.Controls.Add(lblStatus);

                yPosition += 50;
            }
        }

//Order ID(Ascending)
//Order ID(Descending)
//Order Date(Nearest)
//Order Date(Furtherest)

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblHeading.Text = cmbStatus.Text + " Order";
        }


    }
}
