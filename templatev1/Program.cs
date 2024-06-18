using System;
using System.Threading;
using System.Windows.Forms;
using controller;
using controller.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace templatev1
{
    internal static class Program
    {
        private static Log log;
        public static IServiceProvider ServiceProvider { get; private set; }
        public static Database Database { get; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            try
            {
                ServiceProvider = Startup.GetServiceProvider();
                //Configure logging
                log = Startup.ServiceProvider.GetService<Log>();
                Log.LogMessage(LogLevel.Information, "Program", "Startup");
                StartThread(() => RunApplication(() => new Login()));
            }
            catch (Exception ex)
            {
                Log.LogException(new Exception("Error while running the application", ex), "LMCIS");
                throw new Exception("Error while running the application", ex);
            }
        }

        // Try to run the application in a new thread
        // to avoid the application to be closed before the thread is finished and
        // maximized the performance of the application
        private static void StartThread(ThreadStart threadStart)
        {
            ThreadPool.SetMaxThreads(100, 100);

            ThreadPool.SetMinThreads(100, 100);

            ThreadPool.GetAvailableThreads(out _, out _);

            var thread = new Thread(threadStart)
            {
                Name = "LMCIS Thread",
                Priority = ThreadPriority.Highest,
                IsBackground = false,
            };

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
    Log.LogMessage(LogLevel.Information, "LMCIS",
        $"Started thread {Enum.GetName(typeof(ApartmentState), thread.GetApartmentState())}");
        }

        // the Background thread is for the background process of the application
        // it is welcome to add more background process here
private static void StartBackgroundThread(ThreadStart threadStart)
{
    var thread = new Thread(threadStart)
    {
        Name = "LMCIS Background Thread",
        Priority = ThreadPriority.Normal,
        IsBackground = true,
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

        public static ThreadState GetThreadState(ThreadState ts)
        {
            return ts & (ThreadState.Unstarted | ThreadState.WaitSleepJoin | ThreadState.Stopped);
        }

        /// <summary>
        /// Gets the maximum number of ThreadPool worker threads and completion port threads.
        /// </summary>
        /// <param name="workerThreads">The maximum number of worker threads that the ThreadPool can support.</param>
        /// <param name="completionPortThreads">The maximum number of completion port threads that the ThreadPool can support.</param>
        public static void GetMaxThreads(out int workerThreads, out int completionPortThreads)
        {
            // Use the ThreadPool.GetMaxThreads method to get the maximum number of worker threads and completion port threads
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
        }
    }
}