using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace controller
{
    public class Database : IDisposable
    {
        private readonly MySqlConnection connection;

        public Database(string connectionString = null)
        {
            connection = new MySqlConnection(connectionString ?? GetConnectionString());
            connection.Open();
        }

        public static string GetConnectionString()
        {
            var connectionStrings = new List<string>
            {
                "server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=1",
                "server=localhost;port=8088;user id=root; password=password;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=1",
                "server=hkg1.clusters.zeabur.com;port=32298;user id=root; password=ixYr958dIF4Zo3Xvbnp62SQ7f1yVs0Mt;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30"
            };

            return TestConnection(connectionStrings) ?? throw new Exception("No valid connection string found.");
        }

        private static string TestConnection(List<string> connectionStrings)
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
            connection?.Close();
            connection?.Dispose();
        }

        public object ExecuteScalarCommand(string sqlQuery, Dictionary<string, object> queryParameters)
        {
            return ExecuteCommand(sqlQuery, queryParameters, command => command.ExecuteScalar());
        }

        public void ExecuteNonQueryCommand(string sqlQuery, Dictionary<string, object> queryParameters)
        {
            Log.LogMessage(Log.LogLevel.Debug, "Database", $"ExecuteNonQueryCommand : {sqlQuery + queryParameters}");
            ExecuteCommand(sqlQuery, queryParameters, command => command.ExecuteNonQuery());
        }

        public MySqlDataReader ExecuteReaderCommand(string sqlQuery, Dictionary<string, object> queryParameters)
        {
            return (MySqlDataReader)ExecuteCommand(sqlQuery, queryParameters, command => command.ExecuteReader());
        }

        private object ExecuteCommand(string sqlQuery, Dictionary<string, object> queryParameters,
            Func<MySqlCommand, object> execute)
        {
            using (var command = connection.CreateCommand())
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
            var command = connection.CreateCommand();
            command.CommandText = query;
            foreach (var parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }

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
            return dt;
        }

        public DataTable ExecuteDataTable(string sqlQuery)
        {
            var reader = (MySqlDataReader)ExecuteCommand(sqlQuery, null, command => command.ExecuteReader());
            var dt = new DataTable();
            dt.Load(reader);
            return dt;
            //return (DataTable)ExecuteCommand(sqlQuery, null, command => command.ExecuteReader());
        }
    }
}