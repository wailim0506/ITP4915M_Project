using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace controller
{
    public class OrderAnalysisReportController
    {
        private readonly Database _database;

        public OrderAnalysisReportController(Database database = null)
        {
            _database = database ?? new Database();
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