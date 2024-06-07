using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace controller
{
    public class Database
    {
        private static MySqlConnection _databaseConnection;

        public Database(MySqlConnection databaseConnection = null)
        {
            _databaseConnection = databaseConnection ?? new MySqlConnection(
                "server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;");
            _databaseConnection.Open();
        }

        public static void CloseConnection() => _databaseConnection.Close();

        public static object ExecuteScalarCommand(string sqlQuery, Dictionary<string, object> queryParameters) =>
            ExecuteCommand(sqlQuery, queryParameters, command => command.ExecuteScalar());

        public static void ExecuteNonQueryCommand(string sqlQuery, Dictionary<string, object> queryParameters) =>
            ExecuteCommand(sqlQuery, queryParameters, command => command.ExecuteNonQuery());

        private static object ExecuteCommand(string sqlQuery, Dictionary<string, object> queryParameters,
            Func<MySqlCommand, object> execute)
        {
            using (var command = _databaseConnection.CreateCommand())
            {
                command.CommandText = sqlQuery;
                foreach (var parameter in queryParameters)
                {
                    command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                }

                return execute(command);
            }
        }

        public static object ExecuteReaderCommand(string ReaderQuery) =>
            ExecuteCommand(ReaderQuery, null, command => command.ExecuteReader());
    }
}