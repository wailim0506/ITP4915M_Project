using System;
using System.Threading;
using System.Windows.Forms;
using LMCIS.controller.Utilities;
using LMCIS.System_page;
using Microsoft.Extensions.Logging;

namespace LMCIS
{
    internal static class Program
    {
        // Log instance for logging application events
        public static Log log;

        // Date handler instance for handling date related operations
        public static dateHandler handler;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // Initialize log and date handler
                log = new Log();
                handler = new dateHandler();
                Configuration.CreateAvatarFolder();
                // Start a new thread to run the application
                StartThread(() => RunApplication(() => new Login()));
            }
            catch (Exception ex)
            {
                throw new Exception("Error while running the application", ex);
            }
        }

        /// <summary>
        /// Starts a new thread for running the application.
        /// </summary>
        /// <param name="threadStart">The ThreadStart delegate to be invoked when the thread begins executing.</param>
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

        /// <summary>
        /// Starts a new background thread for the application.
        /// </summary>
        /// <param name="threadStart">The ThreadStart delegate to be invoked when the thread begins executing.</param>
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

        /// <summary>
        /// Runs the application in the main thread.
        /// </summary>
        /// <param name="createForm">A function that creates the main form of the application.</param>
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