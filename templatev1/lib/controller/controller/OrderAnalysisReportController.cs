using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace controller
{
    public class OrderAnalysisReportController
    {
        private readonly MySqlConnection _connection;

        public OrderAnalysisReportController(MySqlConnection connection)
        {
            _connection = connection;
        }

        public DataTable GenerateReport(string period, DateTime startDate, DateTime endDate)
        {
            var report = new DataTable();
            var query =
                $"SELECT OrderID, CustomerName, OrderDate, DeliveryDate, OrderStatus FROM ShippedOrderTotals WHERE OrderDate BETWEEN @StartDate AND @EndDate AND {GetPeriodCondition(period)}";
            var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@StartDate", startDate);
            command.Parameters.AddWithValue("@EndDate", endDate);
            AddPeriodParameters(command, period, startDate);
            new MySqlDataAdapter(command).Fill(report);
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

        private void AddPeriodParameters(MySqlCommand command, string period, DateTime startDate)
        {
            switch (period.ToLower())
            {
                case "yearly":
                    command.Parameters.AddWithValue("@Year", startDate.Year);
                    break;
                case "monthly":
                    command.Parameters.AddWithValue("@Month", startDate.Month);
                    command.Parameters.AddWithValue("@Year", startDate.Year);
                    break;
                case "daily":
                    command.Parameters.AddWithValue("@Date", startDate.Date);
                    break;
                default:
                    throw new ArgumentException(
                        "Invalid period specified. Please use 'yearly', 'monthly', or 'daily'.");
            }
        }
    }
}