using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace controller.Utilities
{
    public class Log
    {
        private static ILogger<Log> logger;
        private static readonly string CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
        private static readonly string LogDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        private static readonly string LogFileName = Path.Combine(LogDirectory, $"Log-{CurrentTime}.txt");

        public Log(ILogger<Log> logger = null)
        {
            Log.logger = logger ?? LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            }).CreateLogger<Log>();

            CreateLogFile();
        }

        public static void LogMessage(LogLevel logLevel, string programName, string message)
        {
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - [{logLevel}] - [{programName}] - {message}";
            logger.Log(logLevel, logMessage);
            WriteToLogFile(logMessage);
        }

        public static void LogException(Exception ex, string programName)
        {
            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - [Exception] - [{programName}] - {ex.Message}";
            logger.LogError(logMessage);
            WriteToLogFile(logMessage);
        }

        private void CreateLogFile()
        {
            Directory.CreateDirectory(LogDirectory);
            if (!File.Exists(LogFileName))
            {
                File.WriteAllText(LogFileName, string.Empty);
            }
        }

        private static void WriteToLogFile(string message)
        {
            File.AppendAllText(LogFileName, $"{message}{Environment.NewLine}");
        }
    }
}