using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using controller.Utilities;

namespace controller
{
    public class DeliveryController : abstractController
    {
        private readonly Database _database = new Database();

        public string GetDeliveryMap(string orderId)
        {
            var orderStatus = GetOrderStatus(orderId);
            var location = orderStatus == "Ready to Ship" ? "Not available" : GetLocation(orderId, orderStatus);
            return location == "Not available"
                ? location
                : $"https://maps.googleapis.com/maps/api/staticmap?center={location}&zoom=15&size=600x400&maptype=roadmap&markers=color:red%7C{location}&key={GetApiKey()}";
        }

        private string GetOrderStatus(string orderId) =>
            _database.ExecuteDataTable("SELECT status FROM order_ WHERE OrderID = @orderId",
                new Dictionary<string, object> { { "@orderId", orderId } }).Rows[0][0].ToString();

        private string GetLocation(string orderId, string orderStatus)
        {
            if (orderStatus != "Shipping")
            {
                return "Not available";
            }

            return GetDeliveryRelay(orderId);
        }

        private string GetDeliveryRelay(string orderId)
        {
            var lat = _database.ExecuteDataTable("SELECT latitude FROM deliveryrelay WHERE RelayID = @relayId",
                new Dictionary<string, object> { { "@relayId", orderId } }).Rows[0][0].ToString();
            var lng = _database.ExecuteDataTable("SELECT longitude FROM deliveryrelay WHERE RelayID = @relayId",
                new Dictionary<string, object> { { "@relayId", orderId } }).Rows[0][0].ToString();
            return $"{lat},{lng}";
        }

        public async Task LoadImageAsync(string url, PictureBox pictureBox)
        {
            using (HttpClient client = new HttpClient())
            {
                using (Stream stream = await client.GetStreamAsync(url))
                {
                    pictureBox.Image = new Bitmap(stream);
                }
            }
        }

        private string GetApiKey()
        {
            return "AIzaSyCkf_QIkfACP5z6IlGJzZqqJHl6KBBn4Kg";
        }
    }
}