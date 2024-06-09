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
            connection = new MySqlConnection(connectionString ??
                                             "server=localhost;port=8088;user id=root; password=password;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30");
            connection.Open();
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