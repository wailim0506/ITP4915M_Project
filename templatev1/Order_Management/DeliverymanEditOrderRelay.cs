using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using controller;

namespace templatev1
{
    public partial class DeliverymanEditOrderRelay : Form
    {
        private readonly AccountController _accountController;
        private readonly UIController _uiController;
        private readonly viewOrderController _viewController;
        private string uName, UID;
        readonly string _orderId;
        public DeliveryController DeliveryController;

        public DeliverymanEditOrderRelay(string orderID)
        {
            InitializeComponent();
            _viewController = new viewOrderController();
            _orderId = orderID;
            DeliveryController = new DeliveryController();
        }

        public DeliverymanEditOrderRelay(string orderId, AccountController accountController, UIController uiController)
        {
            InitializeComponent();
            _orderId = orderId;
            _accountController = accountController;
            _uiController = uiController;
            _viewController = new viewOrderController();
            DeliveryController = new DeliveryController();
            UID = _accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
        }

        private void delivermanViewOrder_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            lblLoc.Text += $" {_orderId}";
            load_data();
            cbxCity.Enabled = false;
            cbxRelayName.Enabled = false;
            List<string> ProvinceList = DeliveryController.GetProvinceList();
            cbxProvince.Items.AddRange(ProvinceList.ToArray());
            cbxProvince.SelectedIndex = 0;
            
        }

        private void load_data()
        {
            DataTable dt = _viewController.GetOrder(_orderId);
            string[] f = dt.Rows[0][4].ToString().Split(' ');

            //order info
            lblOrderID.Text = _orderId;

            if ($"{dt.Rows[0][6]}" == "Shipped") //if status is shipped, hide the job finish button
            {
                btnReturn.Location = new Point(1037, 820);
            }

            //delivery info
            dt = new DataTable();
            dt = _viewController.GetShippingDetail(_orderId);
            string shippingDate = dt.Rows[0][2].ToString();
            string[]
                d = shippingDate
                    .Split(' '); //since the database also store the time follwing the date, split it so that only date will be display
            string[] delivermanDetail = _viewController.GetDelivermanDetail(_orderId);
            if (_viewController.GetStatus(_orderId) == "Cancelled")
            {
                lblDelivermanID.Text = "N/A";
            }
            else
            {
                lblDelivermanID.Text = dt.Rows[0][1].ToString();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form deliverman = new deliverman(_accountController, _uiController);
            Hide();
            deliverman.StartPosition = FormStartPosition.Manual;
            deliverman.Location = Location;
            deliverman.ShowDialog();
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private void btnRelayEdit_Click(object sender, EventArgs e)
        {
            DeliveryController.EditRelay(_orderId, DeliveryController.GetRelayId(cbxRelayName.SelectedItem.ToString()));
        }

        private void cbxProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateComboBox(cbxProvince, cbxCity, DeliveryController.GetCitiesByProvince);
        }

        private void cbxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateComboBox(cbxCity, cbxRelayName, DeliveryController.GetRelayNamesByCity);
        }

        private void UpdateComboBox(ComboBox source, ComboBox target, Func<string, List<string>> fetchItems)
        {
            target.Items.Clear();
            target.Enabled = true;
            var items = fetchItems(source.SelectedItem.ToString());
            target.Items.AddRange(items.ToArray());
            if (target.Items.Count > 0) target.SelectedIndex = 0;
        }

        private void cbxRelayName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string location = DeliveryController.GetRelayLocation(cbxRelayName.SelectedItem.ToString());
            RelayPointImage.Load(DeliveryController.GenerateMapUrl(location, RelayPointImage.Size,
                cbxRelayName.SelectedItem.ToString()));
        }
    }
}