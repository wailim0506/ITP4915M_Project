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

namespace templatev1
{
    public partial class DID : Form
    {
        AccountController accountController;
        UIController UIController;
        didController controller;
        private string uName, UID;
        string orderID;
        string partNum;
        bool isManager;

        public DID(string orderID, string partNum)
        {
            InitializeComponent();
            controller = new didController();
            this.orderID = orderID;
            this.partNum = partNum;
        }

        public DID(string orderID, string partNum, AccountController accountController, UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.partNum = partNum;
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new didController();
            UID = this.accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
            isManager = accountController.CheckIsManager();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void DID_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_data();
        }

        private void load_data()
        {
            DataTable dt;
            dt = controller.getData(orderID, partNum);
            lblOrderDate.Text = dt.Rows[0][0].ToString();
            lblOrderSerialNum.Text = dt.Rows[0][1].ToString();
            lblOrderQty.Text = dt.Rows[0][2].ToString();
            lblTotalToDespatch.Text = dt.Rows[0][2].ToString();
            lblPartName.Text = dt.Rows[0][3].ToString();
            lblCategory.Text = $"{dt.Rows[0][4]} - {dt.Rows[0][5]}";
            lblCustomerID.Text = dt.Rows[0][6].ToString();
            lblDeliveryman.Text = $"{dt.Rows[0][7]} - {dt.Rows[0][8]} " +
                                  $"{dt.Rows[0][9]}";
        }
    }
}