using System;
using System.Collections.Generic;
using System.Data;

namespace controller
{
    public class OrderAnalysisReportController
    {
        private readonly Database _database;

        public OrderAnalysisReportController(Database database = null)
        {
<<<<<<< HEAD
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
            _database = database ?? new Database();
=======
>>>>>>> Stashed changes
            Directory.CreateDirectory(outputPath);
            Log.LogMessage(LogLevel.Information, "OrderAnalysisReportController",
                $"Output path created at {outputPath}");

            string currentDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            ReportDataSource dataSource = new ReportDataSource("OrderDataSet", reportData);

            string reportPath = "Resources\\OrderAnalysisReport.rdlc" ??
                                throw new ArgumentException($"Report RDLC file not Exists at {reportPath}");
            ReportViewer viewer = new ReportViewer();
            viewer.LocalReport.ReportPath = reportPath;

            var formatDetails = GetFormatDetails(reportFormat.ToLower());

            string outputFile = $"{outputPath}\\OrderReport-{currentDate}{formatDetails.Extension}";
            byte[] bytes = viewer.LocalReport.Render(formatDetails.Encoding, null, out string mimeTypeOut, out _, out _,
                out _, out _);
            File.WriteAllBytes(outputFile, bytes);
<<<<<<< Updated upstream
        }

        private (string MimeType, string Encoding, string Extension) GetFormatDetails(string reportFormat)
        {
            return reportFormat == "pdf" ? ("application/pdf", "PDF", ".pdf") :
                reportFormat == "excel" ? ("application/vnd.ms-excel", "Excel", ".xlsx") :
                reportFormat == "word" ? ("application/vnd.ms-word", "Word", ".docx") :
                throw new ArgumentException("Invalid report format specified. Please use 'pdf', 'excel', or 'word'.")
                {
                    Source = "OrderAnalysisReportController",
                };
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes
=======
            _database = database ?? new Database();
>>>>>>> parent of 208587a (change all files fleld DataBase to ServiceProvider aka Dependency Injection)
        }

        public DataTable GenerateReport(string period, DateTime startDate, DateTime endDate)
        {
            var query =
                $"SELECT OrderID, CustomerName, OrderDate, OrderStatus FROM shippedordertotals WHERE OrderDate BETWEEN @StartDate AND @EndDate AND {GetPeriodCondition(period)}";
            var parameters = new Dictionary<string, object>
            {
                { "@StartDate", startDate },
                { "@EndDate", endDate }
            };
            AddPeriodParameters(parameters, period, startDate);
            var report = _database.ExecuteDataTable(query, parameters);
            return report;
        }

        private string GetPeriodCondition(string period)
        {
            switch (period.ToLower())
            {
                case "yearly":
                    return "YEAR(OrderDate) = @Year";
                case "monthly":
                    return "MONTH(OrderDate) = @Month AND YEAR(OrderDate) = @Year";
                case "daily":
                    return "CAST(OrderDate AS DATE) = @Date";
                default:
                    throw new ArgumentException(
                        "Invalid period specified. Please use 'yearly', 'monthly', or 'daily'.");
            }
        }

        private void AddPeriodParameters(Dictionary<string, object> parameters, string period, DateTime startDate)
        {
            switch (period.ToLower())
            {
                case "yearly":
                    parameters.Add("@Year", startDate.Year);
                    break;
                case "monthly":
                    parameters.Add("@Month", startDate.Month);
                    parameters.Add("@Year", startDate.Year);
                    break;
                case "daily":
                    parameters.Add("@Date", startDate.Date);
                    break;
                default:
                    throw new ArgumentException(
                        "Invalid period specified. Please use 'yearly', 'monthly', or 'daily'.");
            }
        }
    }
}