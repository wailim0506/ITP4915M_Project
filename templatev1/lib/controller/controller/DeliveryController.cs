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
            var location = orderStatus == "Ready to Ship" ? "" : GetLocation(orderId);
            return location == "" ? location : GenerateMapUrl(location, orderId);
        }

        private string GetOrderStatus(string orderId) =>
            _database.ExecuteDataTable("SELECT status FROM order_ WHERE OrderID = @orderId",
                new Dictionary<string, object> { { "@orderId", orderId } }).Rows[0][0].ToString();

        private string GetLocation(string orderId)
        {
            var orderStatus = GetOrderStatus(orderId);
            return orderStatus != "Processing" ? "" : GetDeliveryRelay(orderId);
        }

        private string GetDeliveryRelay(string orderId)
        {
            var relayId = GetDeliveryRelayId(orderId);
            var dataTable = _database.ExecuteDataTable("SELECT * FROM deliveryrelay WHERE RelayID = @relayId",
                new Dictionary<string, object> { { "@relayId", relayId } });
            return dataTable.Rows.Count > 0 ? $"{dataTable.Rows[0][4]},{dataTable.Rows[0][5]}" : "";
        }

        private string GetDeliveryRelayId(string orderId)
        {
            return _database.ExecuteDataTable("SELECT DeliveryRelayID FROM order_ WHERE OrderID = @orderId",
                new Dictionary<string, object> { { "@orderId", orderId } }).Rows[0][0].ToString();
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
            return "AIzaSyCvDbMpDYOev7-eygdiIP0e9xG-gPV18H8";
        }

        private string GenerateMapUrl(string location , string orderId)
        {
            return $"https://maps.googleapis.com/maps/api/staticmap?center={location}&zoom=15" +
                   $"&size=764x548&maptype=roadmap" +
                   $"&markers=color:red%7Clabel:{GetLocationName(GetDeliveryRelayId(orderId))}%7C{location}" +
                   $"&key={GetApiKey()}";
        }

        private string GetLocationName(string getDeliveryRelayId)
        {
            var dataTable = _database.ExecuteDataTable("SELECT * FROM deliveryrelay WHERE RelayID = @relayId",
                new Dictionary<string, object> { { "@relayId", getDeliveryRelayId } });
            return dataTable.Rows.Count > 0 ? dataTable.Rows[0][2].ToString() : "";
        }
    }
}