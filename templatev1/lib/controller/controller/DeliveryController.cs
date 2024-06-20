using System.Collections.Generic;
using controller.Utilities;

namespace controller
{
    public class DeliveryController : abstractController
    {
        protected Database Database;
        public DeliveryController()
        {
            Database = new Database();
        }
        
        
        public void GetDeliveryMap(string orderID)
        {
            Dictionary<string, string> deliveryMap = new Dictionary<string, string>();
        }
    }
}