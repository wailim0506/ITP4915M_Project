using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1.Order_Management
{
    public partial class customerViewOrder : Form
    {
        controller.accountController accountController;
        controller.UIController UIController;
        controller.viewOrderController controller;
        private string uName, UID;
        string orderID;
        public customerViewOrder()
        {
            InitializeComponent();
            controller = new controller.viewOrderController();
        }

        public customerViewOrder(string orderID, controller.accountController accountController, controller.UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            this.controller = new controller.viewOrderController();
            //UID = accountController.getUID();
            //lblUid.Text = UID;

            UID = "LMC00001"; //hard code for testing
            //UID = "LMC00003"; //hard code for testing
            this.orderID = orderID;
        }


        private void customerViewOrder_Load(object sender, EventArgs e)
        {
            DataTable dt =  controller.getOrder(orderID);

            //order basic info
            Label lblID = new Label() { Name = $"lblID", Text = orderID, Location = new System.Drawing.Point(381,123), Font = new Font("Microsoft Sans Serif", 12),AutoSize = true };
            Label lblSerialNum = new Label() { Name = $"lblSerialNum", Text = $"{dt.Rows[0][3]}", Location = new System.Drawing.Point(381, 170), Font = new Font("Microsoft Sans Serif", 12), AutoSize = true };
            Label lblDate = new Label() { Name = $"lblDate", Text = $"{dt.Rows[0][4]}", Location = new System.Drawing.Point(381, 217), Font = new Font("Microsoft Sans Serif", 12), AutoSize = true };
            Label lblStaffName = new Label() { Name = $"lblStaffName", Text = $"{controller.getStaffName(dt.Rows[0][2].ToString())}", Location = new System.Drawing.Point(381, 264), Font = new Font("Microsoft Sans Serif", 12), AutoSize = true };
            Label lblStaffID = new Label() { Name = $"lblStaffID", Text = $"{controller.getStafftID(dt.Rows[0][2].ToString())}", Location = new System.Drawing.Point(381, 311), Font = new Font("Microsoft Sans Serif", 12), AutoSize = true, TextAlign = ContentAlignment.MiddleCenter };
            Label lblStaffContact = new Label() { Name = $"lblStaffContact", Text = $"{controller.getStaffContact(dt.Rows[0][2].ToString())}", Location = new System.Drawing.Point(381, 358), Font = new Font("Microsoft Sans Serif", 12), AutoSize = true};
            Label lblStatus = new Label() { Name = $"lblStatus", Text = $"{dt.Rows[0][6]}", Location = new System.Drawing.Point(381, 405), Font = new Font("Microsoft Sans Serif", 12),AutoSize = true };

            this.Controls.Add(lblID);
            this.Controls.Add(lblSerialNum);
            this.Controls.Add(lblDate);
            this.Controls.Add(lblStaffName);
            this.Controls.Add(lblStaffID);
            this.Controls.Add(lblStaffContact);
            this.Controls.Add(lblStatus);

            //delivery info
            dt = new DataTable();
            dt = controller.getShippingDetail(orderID);
            string shippingDate = dt.Rows[0][2].ToString();
            string[] d = shippingDate.Split(' '); //since the database also store the time follwing the date, split it so that only date will be display
            shippingDate = d[0];
            string[] delivermanDetail = controller.getDelivermanDetail(orderID);
            lblDelivermanID.Text = dt.Rows[0][1].ToString();
            lblDelivermanName.Text = $"{delivermanDetail[0]} {delivermanDetail[1]}";
            lblDelivermanContact.Text = delivermanDetail[2];            
            lblShippingDate.Text = $"{shippingDate}";         
            lblExpressNum.Text = dt.Rows[0][4].ToString();
            lblShippingAddress.Text = controller.getShippingAddress(UID);

            //ordered spare part
            dt = new DataTable();
            dt = controller.getOrderedSparePart(orderID);
            int row = dt.Rows.Count;

            int rowNumPosition = 505;
            int rowDataPostion = 28;
            int orderTotalPrice = 0;
            for (int i = 1; i <= row; i++)
            {
                Label lblRowNum = new Label() { Name = $"lblRowNum{i}", Text = $"{i.ToString()}.", Location = new System.Drawing.Point(220, rowNumPosition), Font = new Font("Microsoft Sans Serif", 12) };
                Label lblItemNum = new Label() { Name = $"lblItemNum{i}", Text = $"{controller.getItemNum(dt.Rows[i - 1][0].ToString())}", Location = new System.Drawing.Point(6, rowDataPostion), Font = new Font("Microsoft Sans Serif", 12), Size = new System.Drawing.Size(83, 20), TextAlign = ContentAlignment.MiddleCenter };
                Label lblPartNum = new Label() { Name = $"lblPartNum{i}", Text = $"{dt.Rows[i - 1][0].ToString()}", Location = new System.Drawing.Point(95, rowDataPostion), Font = new Font("Microsoft Sans Serif", 12), Size = new System.Drawing.Size(97, 20), TextAlign = ContentAlignment.MiddleCenter };
                Label lblPartName = new Label() { Name = $"lblPartName{i}", Text = $"{controller.getPartName(dt.Rows[i - 1][0].ToString())}", Location = new System.Drawing.Point(191, rowDataPostion), Font = new Font("Microsoft Sans Serif", 12), Size = new System.Drawing.Size(320, 20),TextAlign = ContentAlignment.MiddleCenter };
                Label lblQuantity = new Label() { Name = $"lblQuantity{i}", Text = $"{dt.Rows[i - 1][2].ToString()}", Location = new System.Drawing.Point(510, rowDataPostion), Font = new Font("Microsoft Sans Serif", 12), Size = new System.Drawing.Size(130, 20), TextAlign = ContentAlignment.MiddleCenter };
                Label lblUnitPrice = new Label() { Name = $"lblUnitPrice{i}", Text = $"¥{dt.Rows[i - 1][3].ToString()}", Location = new System.Drawing.Point(651, rowDataPostion), Font = new Font("Microsoft Sans Serif", 12), Size = new System.Drawing.Size(144, 20), TextAlign = ContentAlignment.MiddleCenter };
                Label lblRowTotalPrice = new Label() { Name = $"lblRowTotalPrice{i}", Text = $"¥{(int.Parse(dt.Rows[i - 1][2].ToString()) * int.Parse(dt.Rows[i - 1][3].ToString())).ToString()}", Location = new System.Drawing.Point(801, rowDataPostion), Font = new Font("Microsoft Sans Serif", 12), Size = new System.Drawing.Size(127, 20), TextAlign = ContentAlignment.MiddleCenter };
                
                rowNumPosition += 50;
                rowDataPostion += 50;
                orderTotalPrice += (int.Parse(dt.Rows[i - 1][2].ToString()) * int.Parse(dt.Rows[i - 1][3].ToString()));

                this.Controls.Add(lblRowNum);
                grpSparePart.Controls.Add(lblItemNum);
                grpSparePart.Controls.Add(lblPartNum);
                grpSparePart.Controls.Add(lblPartName);              
                grpSparePart.Controls.Add(lblQuantity);
                grpSparePart.Controls.Add(lblUnitPrice);
                grpSparePart.Controls.Add(lblRowTotalPrice);
            }
            lblOrderTotalPriceLabel.Location = new System.Drawing.Point( //no overlap with grpSparePart
                lblOrderTotalPriceLabel.Location.X,
                grpSparePart.Location.Y + grpSparePart.Height + 10 
            );

            lblOrderTotalPrice.Location = new System.Drawing.Point(
                lblOrderTotalPrice.Location.X,
                grpSparePart.Location.Y + grpSparePart.Height + 10 
            );

            lblOrderTotalPrice.Text += orderTotalPrice.ToString();
        }
        





    }
}
