using controller;

namespace TestController
{
    public class TestDeliveryController
    {
        private string orderid = "OD24020001";
        viewOrderController viewOrder = new viewOrderController();


        public void TestURL()
        {
            viewOrderController viewOrder = new viewOrderController();

            if (viewOrder.GetStatus(orderid) != "Shipped")
            {
                
            }
            
            
        }


    }
}