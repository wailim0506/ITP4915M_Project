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
            isManager = accountController.CheckIsManager();
        }


        private void DID_List_Load(object sender, EventArgs e)
        {
            if (!isManager)
            {
                hideButton();
            }
            timer1.Enabled = true;

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }
    }
}