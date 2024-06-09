using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using controller;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using templatev1.Online_Ordering_Platform;

namespace templatev1
{
    internal static class Program
    {
        private static Log log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                var service = new ServiceCollection();
                ConfigureServices(service);
                var serviceProvider = service.BuildServiceProvider();
                // Create and show the loading form
                LoadingForm loadingForm = new LoadingForm();
                loadingForm.Show();
                // Start new thread to run the application
                StartThread(() => RunApplication(() => new Login()));
                //StartThread(() => RunApplication(() => new customerOrderList()));
            }
            catch (Exception ex)
            {
                throw new Exception("Error while running the application", ex);
            }
            // Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Login());

            //Application.Run(new customerOrderList());
        }

        // Configure the services
        // make the MysqlConnection as models
        private static void ConfigureServices(IServiceCollection service)
        {
            // time out for connection for 30 seconds
            string connString =
                //"server=localhost;port=8088;user id=root; password=password;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30;";
            "server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30;";
            service.AddSingleton<Database>(_ => new Database(Database.GetConnectionString()));
            service.AddSingleton<Log>(_ => new Log());

            var controllers = new List<Type>
            {
                typeof(AccountController),
                typeof(addPartToOrderController),
                typeof(cartController),
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

        // Try to run the application in a new thread
        // to avoid the application to be closed before the thread is finished and
        // maximized the performance of the application
        private static void StartThread(ThreadStart threadStart)
        {
            var thread = new Thread(threadStart);
            thread.Name = "LMCIS Thread";
            thread.Priority = ThreadPriority.Normal;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        // the Background thread is for the background process of the application
        // it is welcome to add more background process here
        private static void StartBackgroundThread(ThreadStart threadStart)
        {
            var thread = new Thread(threadStart);
            thread.Name = "LMCIS Background Thread";
            thread.Priority = ThreadPriority.Normal;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        // the main thread is for the main process of the application
        private static void RunApplication(Func<Form> createForm)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(createForm());
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}