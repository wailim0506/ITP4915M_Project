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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dayDifference();
        }

        private void dayDifference()  //calculate day difference
        {
            DataTable dt;
            controller.viewOrderController controller = new controller.viewOrderController();
            dt = controller.getShippingDetail("OD24060003");
            string shippingDate = dt.Rows[0][2].ToString();
            string[] d = shippingDate.Split(' '); //since the database also store the time follwing the date, split it so that only date will be display
            shippingDate = d[0];
            string shipDate = shippingDate; //   d/M/yyyy

            string sysYear = DateTime.Now.ToString("yyyy"); //today year 
            string sysMonth = DateTime.Now.ToString("MM"); //today month
            string sysDay = DateTime.Now.ToString("dd"); //today month

            string[] splitShipDate = shipDate.Split('/');
            string shipMonth = splitShipDate[0];
            string shipDay = splitShipDate[1];
            string shipYear = splitShipDate[2];

            if (int.Parse(shipMonth) < 10)
            {
                shipMonth = $"0{shipMonth}";
            }

            if (int.Parse(shipDay) < 10)
            {
                shipDay = $"0{shipDay}";
            }




            string formatedShippingDate = $"{shipDay}/{shipMonth}/{shipYear}";
            string formatedSysDate = $"{sysDay}/{sysMonth}/{sysYear}";

            //MessageBox.Show(formatedShippingDate);
            //MessageBox.Show(formatedSysDate);

            DateTime parsedFormatedShippingDate = DateTime.ParseExact(formatedShippingDate, "dd/MM/yyyy", null);
            DateTime parsedFormatedSysDate = DateTime.ParseExact(formatedSysDate, "dd/MM/yyyy", null);

            TimeSpan difference = parsedFormatedShippingDate - parsedFormatedSysDate;

            string[] f = difference.ToString().Split('.');
            MessageBox.Show(f[0]);
            //return int.Parse(f[0]);  //time difference in day


            //string year = DateTime.Now.ToString("yy"); //today year 
            //string month = DateTime.Now.ToString("MM"); //today month

        }
    }
}
