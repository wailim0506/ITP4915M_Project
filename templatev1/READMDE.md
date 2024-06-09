# Readme

## package

if there is something wrong about the package, please contact me. but please try to fix it by installing the package.
The package list is below:

Microsoft.Extensions.DependencyInjection.Abstractions version 8.0.0

Microsoft.Extensions.DependencyInjection version 8.0.0

MySql.Data version 8.4.0

Bcrypt.Net-Next version 4.0.3

## About the Database connection

> please note that I want to replace the MysqlConnector with the MySql.Data package for better performance and more
> functionality.
> Consider remove the MysqlConnector package from the project on the future.

## about the password encryption

the password encryption will be encrypted by the Bcrypt Algorithm. which is a password hashing algorithm.

### to use the Bcrypt Algorithm

```csharp
using Bcrypt.Net.Next;

string password = "123456";


var hashedPassword = BCrypt.Net.Next.BCrypt.EnhancedHashPassword(password);

// verify the password
var passwordFromDB = GetUserPasswordFromDB();
bool isPasswordMatch = BCrypt.Net.Next.BCrypt.EnhancedVerify(password, passwordFromDB);
```

### to use the Mysql.Data package

```csharp
using MySql.Data.MySqlClient;

string password = "123456";

// create a new instance of the MySqlConnection class
MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;");

using (connection)
{
    // open the connection
    connection.Open();

    // create a new instance of the MySqlCommand class
    MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE username = @username", connection);

    // add the parameter to the command
    command.Parameters.AddWithValue("@username", "admin");

    // execute the command
    using (MySqlDataReader reader = command.ExecuteReader())
    {
        // read the data
        while (reader.Read())
        {
            Console.WriteLine(reader["username"]);
        }
    }
    
    // method 2
    // using the dictionary for the parameters
    Dictionary<string, object> parameters = new Dictionary<string, object>();
    parameters.Add("@username", "admin");
    command.Parameters.AddRange(parameters.Keys.Select(key => new MySqlParameter(key, parameters[key])).ToArray());

    // execute the command
    using (MySqlDataReader reader = command.ExecuteReader())
    {
        // read the data
        while (reader.Read())
        {
            Console.WriteLine(reader["username"]);
        }
    }
    
    // method 3
    // using the Database class on the project
    // To be written or you can look at the batabase class on the project
}
```
