using System;
using System.Threading;
using System.Windows.Forms;
using controller.Utilities;
using Microsoft.Extensions.Logging;

namespace templatev1
{
    internal static class Program
    {
        public static Log log;
        private static IServiceProvider serviceProvider;

        public static dateHandler handler;
        // private static string connString = "server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30;";
        // private static Database db = new Database(connString);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                log = new Log();
                handler = new dateHandler();
                // Start a new thread to run the application
                StartThread(() => RunApplication(() => new Login()));
                //StartThread(() => RunApplication(() => new DeliverymanEditOrderRelay("OD24060003")));
            }
            catch (Exception ex)
            {
                throw new Exception("Error while running the application", ex);
                //Log.LogException(new Exception("Error while running the application", ex), "LMCIS");
            }
        }

        // Try to run the application in a new thread
        // to avoid the application to be closed before the thread is finished and
        // maximized the performance of the application
        private static void StartThread(ThreadStart threadStart)
        {
            int workerThreads = 100;
            int completionPortThreads = 100;
            // Set the maximum number of worker threads and completion port threads
            ThreadPool.SetMaxThreads(workerThreads, completionPortThreads);
            // Set the minimum number of worker threads and completion port threads
            ThreadPool.SetMinThreads(workerThreads, completionPortThreads);

            var thread = new Thread(threadStart)
            {
                Name = "LMCIS Thread",
                Priority = ThreadPriority.Highest
            };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            Log.LogMessage(LogLevel.Information, "LMCIS", "Started thread");
        }

        // the Background thread is for the background process of the application
        // it is welcome to add more background process here
        private static void StartBackgroundThread(ThreadStart threadStart)
        {
            var thread = new Thread(threadStart)
            {
                Name = "LMCIS Background Thread",
                Priority = ThreadPriority.Normal
            };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            Log.LogMessage(LogLevel.Information, "LMCIS", "Started background thread");
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
                Log.LogException(new Exception("Error while running the application", ex), "LMCIS");
            }
        }
    }
}