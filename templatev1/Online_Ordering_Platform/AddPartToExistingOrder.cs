using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1
{
    public partial class AddPartToExistingOrder : Form
    {
        private string uName, UID;
        controller.AccountController accountController;
        controller.UIController UIController;
        controller.addPartToOrderController controller;
        private string partNum, qty;

        public AddPartToExistingOrder()
        {
            InitializeComponent();
            //partNum = "D00004";
            //controller = new controller.addPartToOrderController();
            //lblUid.Text = $"Uid: {UID}";
        }


        public AddPartToExistingOrder(string partNum, string qty, controller.AccountController accountController,
            controller.UIController UIController)
        {
            InitializeComponent();
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new controller.addPartToOrderController();
            this.partNum = partNum;
            this.qty = qty;
            UID = accountController.GetUid();
            lblUid.Text = $"Uid: {UID}";
        }

        private void AddPartToExistingOrder_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            load_data();
        }

        private void load_data()
        {
            DataTable dt = controller.GetPartDetail(partNum);
            lblPartName.Text = dt.Rows[0][0].ToString();
            lblPartNum.Text = dt.Rows[0][1].ToString();
            lblCategory.Text = $"{dt.Rows[0][2]} - {dt.Rows[0][3]}";
            lblSupplier.Text = dt.Rows[0][4].ToString();
            lblCountry.Text = dt.Rows[0][5].ToString();
            lblPrice.Text = dt.Rows[0][6].ToString();
            lblOnSaleQty.Text = dt.Rows[0][7].ToString();
            picSpare.Image = imageString(partNum);

            dt = controller.GetEditableOrderId(UID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbOrderSelection.Items.Add(dt.Rows[i][0].ToString());
            }

            tbQty.Text = qty;
            lblLoc.Text = $"Add {lblPartName.Text} to Order";
        }

        private void cmbOrderSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderID = cmbOrderSelection.Text.ToString();
            string shippingDateTime = controller.GetShippingDate(orderID);
            string[] shippingDate = shippingDateTime.Split(' ');
            lblShippingDate.Text = shippingDate[0];
            lblOrderStatus.Text = controller.GetOrderStatus(orderID);
            lblDayUntilDelivery.Text = $"{dayDifference(orderID)} day(s) until shipping.";
            lblLoc.Text = $"Add {lblPartName.Text} to Order {orderID}";
        }

        private int dayDifference(string orderID) //calculate day difference
        {
            string
                systemFormat = SystemDateFormat(); //the date format got from db depend on the operation system setting
            string[] splitSystemFormat = systemFormat.Split('/');

            Boolean monthFirst;

            if (splitSystemFormat[0] == "M" || splitSystemFormat[0] == "MM")
            {
                monthFirst = true;
            }
            else
            {
                monthFirst = false;
            }


            DataTable dt;
            dt = controller.GetShippingDetail(orderID);
            string shippingDate = dt.Rows[0][2].ToString();
            string[]
                d = shippingDate
                    .Split(' '); //since the database also store the time follwing the date, split it so that only date will be display
            shippingDate = d[0];
            string shipDate = shippingDate; //   d/M/yyyy


            string sysYear = DateTime.Now.ToString("yyyy"); //today year 
            string sysMonth = DateTime.Now.ToString("MM"); //today month
            string sysDay = DateTime.Now.ToString("dd"); //today month

            string[] splitShipDate = shipDate.Split('/');

            string shipMonth, shipDay, shipYear;
            if (monthFirst)
            {
                shipMonth = splitShipDate[0];
                shipDay = splitShipDate[1];
                shipYear = splitShipDate[2];
            }
            else
            {
                shipMonth = splitShipDate[1];
                shipDay = splitShipDate[0];
                shipYear = splitShipDate[2];
            }

            if (int.Parse(shipMonth) < 10)
            {
                shipMonth = shipMonth.PadLeft(2, '0');
            }

            if (int.Parse(shipDay) < 10)
            {
                shipDay = shipDay.PadLeft(2, '0');
            }


            string formatedShippingDate = $"{shipDay}/{shipMonth}/{shipYear}";
            string formatedSysDate = $"{sysDay}/{sysMonth}/{sysYear}";

            DateTime parsedFormatedShippingDate;
            DateTime parsedFormatedSysDate;

            try
            {
                parsedFormatedShippingDate = DateTime.ParseExact(formatedShippingDate, "dd/MM/yyyy", null);
            }
            catch (Exception e)
            {
                parsedFormatedShippingDate = DateTime.ParseExact(formatedShippingDate, "MM/dd/yyyy", null);
            }
            //MessageBox.Show(parsedFormatedShippingDate.ToString());

            try
            {
                parsedFormatedSysDate = DateTime.ParseExact(formatedSysDate, "dd/MM/yyyy", null);
            }
            catch (Exception e)
            {
                parsedFormatedSysDate = DateTime.ParseExact(formatedSysDate, "MM/dd/yyyy", null);
            }


            TimeSpan difference = parsedFormatedShippingDate - parsedFormatedSysDate;

            string[] f = difference.ToString().Split('.');
            return int.Parse(f[0]);
        }

        private void btnAddQty_Click(object sender, EventArgs e)
        {
            if (tbQty.Text != "") //check have quantity input
            {
                int qty = int.Parse(tbQty.Text.ToString());
                qty++;
                tbQty.Text = qty.ToString();
            }
            else
            {
                int qty = 0;
                qty++;
                tbQty.Text = qty.ToString();
            }
        }

        private void btnMinusQty_Click(object sender, EventArgs e)
        {
            if (tbQty.Text == "") return; //check have quantity input
            if (int.Parse(tbQty.Text.ToString()) ==
                1) //check quantity input equal 0, do not perform anything if equal to 0
            {
                return;
            }
            else
            {
                int qty = int.Parse(tbQty.Text.ToString());
                qty--;
                tbQty.Text = qty.ToString();
            }
        }

        private void btnBackViewPart_Click(object sender, EventArgs e)
        {
            Form c = new viewSparePart(partNum, accountController, UIController);
            Hide();
            c.StartPosition = FormStartPosition.Manual;
            c.Location = Location;
            c.ShowDialog();
            Close();
            return;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeDate.Text = DateTime.Now.ToString("dd-MM-yy HH:mm:ss");
        }

        private string SystemDateFormat()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string dateFormat = dtfi.ShortDatePattern;
            return dateFormat;
        }


        private static Image imageString(string imageName)
        {
            PropertyInfo property =
                typeof(Properties.Resources).GetProperty(imageName, BindingFlags.NonPublic | BindingFlags.Static);
            return property?.GetValue(null, null) as Image;
        }
    }
}