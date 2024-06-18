using System;
using System.Globalization;

namespace controller.Utilities
{
    public class dateHandler
    {
        viewOrderController controller;
        public dateHandler() {

            controller = new viewOrderController();
        }
        public int dayDifference(string orderID) //calculate day difference
        {
            string systemFormat = systemDateFormat(); //the date format got from db depend on the operation system setting
            string[] splitSystemFormat = systemFormat.Split('/');

            Boolean monthFirst = false;
            Boolean yearFirst = false;
            Boolean shortYear = false;

            if (splitSystemFormat[0] == "M" || splitSystemFormat[0] == "MM")
            {
                monthFirst = true;
            }
            else if (splitSystemFormat[0] == "yyyy" || splitSystemFormat[0] == "yy")
            {
                yearFirst = true;
                if (splitSystemFormat[0] == "yy")
                {
                    shortYear = true;
                }
            }

            var dt = controller.GetShippingDetail(orderID);
            string shippingDate = dt.Rows[0][2].ToString();
            string[] d = shippingDate.Split(' '); //split to get only the date
            shippingDate = d[0];

            string sysYear = DateTime.Now.ToString("yyyy"); //today year 
            string sysMonth = DateTime.Now.ToString("MM"); //today month
            string sysDay = DateTime.Now.ToString("dd"); //today day

            string[] splitShipDate = shippingDate.Split('/');

            string shipYear, shipMonth, shipDay;

            if (yearFirst)
            {
                shipYear = splitShipDate[0];
                shipMonth = splitShipDate[1];
                shipDay = splitShipDate[2];
            }
            else if (monthFirst)
            {
                shipMonth = splitShipDate[0];
                shipDay = splitShipDate[1];
                shipYear = splitShipDate[2];
            }
            else
            {
                shipDay = splitShipDate[0];
                shipMonth = splitShipDate[1];
                shipYear = splitShipDate[2];
            }

            shipMonth = shipMonth.PadLeft(2, '0');
            shipDay = shipDay.PadLeft(2, '0');

            // Handle two-digit year format
            if (shortYear && shipYear.Length == 2)
            {
                shipYear = DateTime.Now.ToString("yyyy").Substring(0, 2) + shipYear;
            }

            string formattedShippingDate = $"{shipYear}/{shipMonth}/{shipDay}";
            string formattedSysDate = $"{sysYear}/{sysMonth}/{sysDay}";

            DateTime parsedFormattedShippingDate;
            DateTime parsedFormattedSysDate;

            try
            {
                parsedFormattedShippingDate = DateTime.ParseExact(formattedShippingDate, "yyyy/MM/dd", null);
            }
            catch (Exception e)
            {
                throw new FormatException("Invalid shipping date format");
            }

            try
            {
                parsedFormattedSysDate = DateTime.ParseExact(formattedSysDate, "yyyy/MM/dd", null);
            }
            catch (Exception e)
            {
                throw new FormatException("Invalid system date format");
            }

            TimeSpan difference = parsedFormattedShippingDate - parsedFormattedSysDate;

            return (int)difference.TotalDays;
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
