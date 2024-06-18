using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace controller.Utils
{
    public class Startup
    {
        public static IServiceProvider ServiceProvider = GetServiceProvider();
        public static IServiceProvider GetServiceProvider()
        {
            var service = new ServiceCollection();
            ConfigureServices(service);
            return ServiceProvider = service.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection service)
        {
            // time out for connection for 30 seconds
            string connString =
                "server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30;";
            service.AddSingleton(provider => new Database(Database.GetConnectionString()));
            service.AddSingleton(_ => new Log());
            service.AddSingleton(provider => new Startup());

            var controllers = new List<Type>
            {
                typeof(AccountController),
                typeof(addPartToOrderController),
                typeof(cartController),
                typeof(staffOrderListController),
                typeof(delivermanOrderListController),
                typeof(editOrderController),
                typeof(favouriteController),
                typeof(feedbackController),
                typeof(orderListController),
                typeof(proFileController),
                typeof(RecoveryController),
                typeof(spareListController),
                typeof(supplierController),
                typeof(UIController),
                typeof(viewInvoiceController),
                typeof(viewOrderController),
                typeof(viewSparePartController)
                // add controllers here
                // typeof(The name of the controller)
            };
            foreach (var controller in controllers)
            {
                service.AddTransient(controller);
            }
        }
    }
}