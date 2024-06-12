using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace templatev1.Order_Management
{
    public partial class delivermanViewOrder : Form
    {
        controller.AccountController accountController;
        controller.UIController UIController;
        controller.viewOrderController controller;
        private string uName, UID;
        string orderID;
        string shipDate;
        public delivermanViewOrder(string orderID)
        {
            InitializeComponent();
            controller = new controller.viewOrderController();
            this.orderID = orderID;
        }

        public delivermanViewOrder(string orderID, controller.AccountController accountController,
            controller.UIController UIController)
        {
            InitializeComponent();
            this.orderID = orderID;
            this.accountController = accountController;
            this.UIController = UIController;
            controller = new controller.viewOrderController();
            shipDate = "";
            UID = this.accountController.GetUid();


            //UID = "LMC00001"; //hard code for testing
            //UID = "LMC00003"; //hard code for testing
            lblUid.Text = $"Uid: {UID}";
        }

        private void delivermanViewOrder_Load(object sender, EventArgs e)
        {
            lblLoc.Text += $" {orderID}";
            load_data();
        }

        public void load_data()
        {
            pnlSP.Controls.Clear();
            DataTable dt = controller.getOrder(orderID);
            string[] f = dt.Rows[0][4].ToString().Split(' ');

            //order info
            lblOrderID.Text = orderID;
            lblOrderSerialNum.Text = $"{dt.Rows[0][3]}";
            lblOrderDate.Text = f[0];
            lblStaffIncharge.Text = $"{controller.getStaffName(dt.Rows[0][2].ToString())}";
            lblStaffID.Text = $"{controller.getStafftID(dt.Rows[0][2].ToString())}";
            lblStaffContact.Text = $"{controller.getStaffContact(dt.Rows[0][2].ToString())}";
            lblStatus.Text = $"{dt.Rows[0][6]}";

            if($"{dt.Rows[0][6]}" == "Shipped") //if status is shipped, hide the job finish button
            {
                btnJobFinished.Visible = false;
                btnReturn.Location = new Point(1037, 820);
            }

            //delivery info
            dt = new DataTable();
            dt = controller.getShippingDetail(orderID);
            string shippingDate = dt.Rows[0][2].ToString();
            string[]
                d = shippingDate
                    .Split(' '); //since the database also store the time follwing the date, split it so that only date will be display
            shippingDate = d[0];
            shipDate = shippingDate;
            string[] delivermanDetail = controller.getDelivermanDetail(orderID);
            if (lblStatus.Text.ToString() == "Cancelled")
            {
                lblDelivermanID.Text = "N/A";
                lblDelivermanName.Text = $"N/A";
                lblDelivermanContact.Text = "N/A";
                lblShippingDate.Text = $"N/A";

                lblExpressNum.Text = "N/A";
            }
            else
            {
                lblDelivermanID.Text = dt.Rows[0][1].ToString();
                lblDelivermanName.Text = $"{delivermanDetail[0]} {delivermanDetail[1]}";
                lblDelivermanContact.Text = delivermanDetail[2];
                if (dayDifference(orderID) >= 0)
                {
                    lblShippingDate.Text = $"Scheduled on {shippingDate}";
                }
                else
                {
                    lblShippingDate.Text = $"Delivered on {shippingDate}";
                }

                lblExpressNum.Text = dt.Rows[0][4].ToString();
            }

            lblShippingAddress.Text = dt.Rows[0][5].ToString();
            if (lblStatus.Text == "Pending" || lblStatus.Text == "Processing")
            {
                lblDayUntil.Text = $"{dayDifference(orderID)} day(s) until shipping";
            }
            else
            {
                lblDayUntil.Text = "N/A";
            }

            //ordered spare part
            dt = new DataTable();
            dt = controller.getOrderedSparePart(orderID, "None");
            int row = dt.Rows.Count;


            int rowPosition = 6;
            for (int i = 1; i <= row; i++)
            {
                Label lblRowNum = new Label()
                {
                    Name = $"lblRowNum{i}",
                    Text = $"{i.ToString()}.",
                    Location = new Point(3, rowPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(33, 20)
                };
                Label lblItemNum = new Label()
                {
                    Name = $"lblItemNum{i}",
                    Text = $"{controller.getItemNum(dt.Rows[i - 1][0].ToString())}",
                    Location = new Point(38, rowPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(109, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPartNum = new Label()
                {
                    Name = $"lblPartNum{i}",
                    Text = $"{dt.Rows[i - 1][0].ToString()}",
                    Location = new Point(153, rowPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(128, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblPartName = new Label()
                {
                    Name = $"lblPartName{i}",
                    Text = $"{controller.getPartName(dt.Rows[i - 1][0].ToString())}",
                    Location = new Point(287, rowPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(508, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Label lblQuantity = new Label()
                {
                    Name = $"lblQuantity{i}",
                    Text = $"{dt.Rows[i - 1][2].ToString()}",
                    Location = new Point(801, rowPosition),
                    Font = new Font("Microsoft Sans Serif", 12),
                    Size = new Size(116, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
               


                rowPosition += 50;

                pnlSP.Controls.Add(lblRowNum);
                pnlSP.Controls.Add(lblItemNum);
                pnlSP.Controls.Add(lblPartNum);
                pnlSP.Controls.Add(lblPartName);
                pnlSP.Controls.Add(lblQuantity);
            }
        }

        private int dayDifference(string orderID) //calculate day difference
        {
            string
                systemFormat = systemDateFormat(); //the date format got from db depend on the operation system setting
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
            dt = controller.getShippingDetail(orderID);
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

            if (monthFirst && splitSystemFormat[0] == "M" && int.Parse(shipMonth) < 10)
            {
                shipMonth = $"0{shipMonth}";
            }
            else if (monthFirst && splitSystemFormat[0] == "MM" && int.Parse(shipMonth) < 10)
            {
                shipMonth = $"{shipMonth}";
            }
            else if (!monthFirst && splitSystemFormat[1] == "M" && int.Parse(shipMonth) < 10)
            {
                shipMonth = $"0{shipMonth}";
            }
            else if (!monthFirst && splitSystemFormat[1] == "MM" && int.Parse(shipMonth) < 10)
            {
                shipMonth = $"{shipMonth}";
            }

            if (monthFirst && splitSystemFormat[1] == "d" && int.Parse(shipDay) < 10)
            {
                shipDay = $"0{shipDay}";
            }
            else if (monthFirst && splitSystemFormat[1] == "dd" && int.Parse(shipDay) < 10)
            {
                shipDay = $"{shipDay}";
            }
            else if (!monthFirst && splitSystemFormat[0] == "d" && int.Parse(shipDay) < 10)
            {
                shipDay = $"0{shipDay}";
            }
            else if (!monthFirst && splitSystemFormat[0] == "dd" && int.Parse(shipDay) < 10)
            {
                shipDay = $"{shipDay}";
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Form deliverman =
                 new deliverman(); //for testing
            // new deliverman(accountController, UIController);
            Hide();
            deliverman.StartPosition = FormStartPosition.Manual;
            deliverman.Location = Location;
            deliverman.ShowDialog();
            Close();
        }

        private void btnJobFinished_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure this order is delivered?", "Job Finished", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes) //confirmed shipped
            {

                if(controller.delivermanJobFinished(orderID))
                {
                    MessageBox.Show("Order status changed.", "Job Finished");
                    Form d =
                        new delivermanViewOrder(orderID); //for testing
                        // new delivermanViewOrder(orderID,accountController, UIController);
                    Hide();
                    d.StartPosition = FormStartPosition.Manual;
                    d.Location = Location;
                    d.ShowDialog();
                    Close();
                }
                else
                {
                    MessageBox.Show("Please try again.", "Job Finished",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }          
        }

        private string systemDateFormat()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;

            string dateFormat = dtfi.ShortDatePattern;
            return dateFormat;
        }


    }
}
