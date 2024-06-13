using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace controller
{
    public class Database : IDisposable
    {
        private readonly MySqlConnection _connection;

        public Database(string connectionString = null)
        {
            _connection = new MySqlConnection(connectionString ?? GetConnectionStringAsync().Result);
            _connection.Open();
        }

        public static async Task<string> GetConnectionStringAsync()
        {
            var connectionStrings = new List<string>
            {
                "server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30",
                // "server=localhost;port=8088;user id=root; password=password;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=1",
                // "server=hkg1.clusters.zeabur.com;port=32298;user id=root; password=ixYr958dIF4Zo3Xvbnp62SQ7f1yVs0Mt;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30"
            };

            var tasks = connectionStrings.Select(TestConnectionAsync).ToArray();
            var completedTask = await Task.WhenAny(tasks);

            return await completedTask ?? throw new Exception("No valid connection string found.");
        }

        private static async Task<string> TestConnectionAsync(string connectionString)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    return connectionString;
                }
            }
            catch
            {
                return null;
            }
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }

        private async Task ExecuteCommandAsync(string sqlQuery, Dictionary<string, object> queryParameters,
            Func<MySqlCommand, Task> execute)
        {
            try
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

                    await execute(command);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing command", ex);
            }
        }

        public async Task<object> ExecuteScalarCommandAsync(string sqlQuery,
            Dictionary<string, object> queryParameters = null)
        {
            object result = null;
            await ExecuteCommandAsync(sqlQuery, queryParameters,
                async command => result = await command.ExecuteScalarAsync());
            return result;
        }

        public async Task ExecuteNonQueryCommandAsync(string sqlQuery,
            Dictionary<string, object> queryParameters = null)
        {
            await ExecuteCommandAsync(sqlQuery, queryParameters, async command => await command.ExecuteNonQueryAsync());
        }

        public async Task<DataTable> ExecuteDataTableAsync(string sqlQuery,
            Dictionary<string, object> queryParameters = null)
        {
            var dt = new DataTable();
            await ExecuteCommandAsync(sqlQuery, queryParameters, async command =>
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            });
            return dt;
        }
    }
}