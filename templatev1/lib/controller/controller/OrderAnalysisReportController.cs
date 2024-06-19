using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using controller.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Reporting.WinForms;

namespace controller
{
    public class OrderAnalysisReportController : abstractController
    {
        private readonly Database _database = new Database();

        public void GenerateReportDocument(DataTable reportData, string reportFormat, string outputPath)
        {
            Directory.CreateDirectory(outputPath);
            Log.LogMessage(LogLevel.Information, "OrderAnalysisReportController",
                $"Output path created at {outputPath}");

            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
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
        }

        public DataTable GenerateReport(string period, DateTime startDate, DateTime endDate)
        {
            var query =
                $"SELECT OrderID, CustomerName, OrderDate, OrderStatus FROM shippedordertotals WHERE OrderDate BETWEEN @StartDate AND @EndDate AND {GetPeriodCondition(period)}";
            var parameters = new Dictionary<string, object> { { "@StartDate", startDate }, { "@EndDate", endDate } };
            AddPeriodParameters(parameters, period, startDate);
            return _database.ExecuteDataTable(query, parameters);
        }

        private string GetPeriodCondition(string period)
        {
            return period.ToLower() == "yearly" ? "YEAR(OrderDate) = @Year" :
                period.ToLower() == "monthly" ? "MONTH(OrderDate) = @Month AND YEAR(OrderDate) = @Year" :
                period.ToLower() == "daily" ? "CAST(OrderDate AS DATE) = @Date" :
                throw new ArgumentException("Invalid period specified. Please use 'yearly', 'monthly', or 'daily'.");
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