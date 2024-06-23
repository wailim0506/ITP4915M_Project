using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace controller.Utilities
{
    public class Database : IDisposable, IDatabase
    {
        private readonly MySqlConnection _connection;
        private Log _logger;

        public Database(string connectionString = null)
        {
            _connection = new MySqlConnection(connectionString ?? GetConnectionString());
            _connection.Open();
            _logger = new Log();
        }

        public string GetConnectionString()
        {
            var connectionStrings = new List<string>();
            connectionStrings.Add(Configuration.DataBaseConnectionString);

            return TestConnection(connectionStrings) ?? throw new Exception("No valid connection string found.");
        }

        public string TestConnection(List<string> connectionStrings)
        {
            foreach (var connectionString in connectionStrings)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        return connectionString;
                    }
                    catch
                    {
                        // Ignore the exception and try the next connection string
                    }
                }
            }

            // If none of the connection strings work, throw an exception or return null
            throw new Exception("No valid connection string found.");
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }

        public object ExecuteScalarCommand(string sqlQuery, Dictionary<string, object> queryParameters)
        {
            return ExecuteCommand(sqlQuery, queryParameters, command => command.ExecuteScalar());
        }

        // Execute a non-query command and return the number of affected rows
        public void ExecuteNonQueryCommand(string sqlQuery, Dictionary<string, object> queryParameters)
        {
            Log.LogMessage(LogLevel.Debug, "Database",
                $"ExecuteNonQueryCommand : {sqlQuery + queryParameters}");
            ExecuteCommand(sqlQuery, queryParameters, command => command.ExecuteNonQuery());
        }

        public MySqlDataReader ExecuteReaderCommand(string sqlQuery, Dictionary<string, object> queryParameters)
        {
            Log.LogMessage(LogLevel.Debug, "Database",
                $"ExecuteReaderCommand : {sqlQuery + queryParameters}");
            return (MySqlDataReader)ExecuteCommand(sqlQuery, queryParameters, command => command.ExecuteReader());
        }

        public object ExecuteCommand(string sqlQuery, Dictionary<string, object> queryParameters,
            Func<MySqlCommand, object> execute)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = sqlQuery;
                if (queryParameters != null)
                {
                    foreach (var parameter in queryParameters)
                    {
                        command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                    }
                }

                try
                {
                    return execute(command);
                }
                catch (Exception ex)
                {
                    Log.LogException(ex, "Database");
                    throw new InvalidOperationException("Database operation failed", ex);
                }
            }
        }

        public MySqlCommand CreateCommand(string query, Dictionary<string, object> parameters)
        {
            var command = _connection.CreateCommand();
            command.CommandText = query;
            foreach (var parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }

            Log.LogMessage(LogLevel.Debug, "Database", $"CreateCommand : {command.CommandText}");
            return command;
        }

        public void ExecuteNonQuery(MySqlCommand command)
        {
            command.ExecuteNonQuery();
        }

        public DataTable ExecuteDataTable(string sqlQuery, Dictionary<string, object> queryParameters)
        {
            var reader = (MySqlDataReader)ExecuteCommand(sqlQuery, queryParameters, command => command.ExecuteReader());
            var dt = new DataTable();
            dt.Load(reader);
            Log.LogMessage(LogLevel.Debug, "Database", $"ExecuteDataTable : {sqlQuery}");
            return dt;
        }

        public DataTable ExecuteDataTable(string sqlQuery)
        {
            var reader = (MySqlDataReader)ExecuteCommand(sqlQuery, null, command => command.ExecuteReader());
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt;
            //return (DataTable)ExecuteCommand(sqlQuery, null, command => command.ExecuteReader());
        }

        // Execute a scalar command and return the result
        public object ExecuteScalar(string sqlCmd)
        {
            using (MySqlCommand command = new MySqlCommand(sqlCmd, _connection))
            {
                Log.LogMessage(LogLevel.Debug, "Database", $"ExecuteScalar : {sqlCmd}");
                return command.ExecuteScalar();
            }
        }

        // Execute a scalar command and return the result as a string
        public string ExecuteScalarCommand(string sqlQuery)
        {
            using (MySqlCommand command = new MySqlCommand(sqlQuery, _connection))
            {
                Log.LogMessage(LogLevel.Debug, "Database", $"ExecuteScalarCommand : {sqlQuery}");
                return command.ExecuteScalar().ToString();
            }
        }
    }
}