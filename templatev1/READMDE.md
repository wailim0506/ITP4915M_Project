# Readme

## useful links

1. jetbrains student license
   https://www.jetbrains.com/shop/eform/students

## package

if there is something wrong about the package, please contact me. but please try to fix it by installing the package.

The package list is below:

Microsoft.Extensions.DependencyInjection.Abstractions version 8.0.0

Microsoft.Extensions.DependencyInjection version 8.0.0

MySql.Data version 8.4.0

Bcrypt.Net-Next version 4.0.3

Microsoft.Extensions.Logging version 8.0.0
Microsoft.Extensions.Logging.Abstractions version 8.0.0
Microsoft.Extensions.Logging.Console version 8.0.0
Microsoft.Extensions.Logging.Configuration version 8.0.0

### install the package via Visual Studio

1. open the project
2. right click on the project
3. click on the Manage NuGet Packages -> NuGet Console
4. type the command below

```shell
Install-Package Bcrypt.Net-Next -Version 4.0.3
Install-Package Microsoft.Extensions.DependencyInjection.Abstractions -Version 8.0.0
Install-Package Microsoft.Extensions.DependencyInjection -Version 8.0.0
Install-Package MySql.Data -Version 8.4.0
Install-Package Microsoft.Extensions.Logging -Version 8.0.0
Install-Package Microsoft.Extensions.Logging.Abstractions -Version 8.0.0
Install-Package Microsoft.Extensions.Logging.Console -Version 8.0.0
Install-Package Microsoft.Extensions.Logging.Configuration -Version 8.0.0
```

### install the package via the terminal

```shell

## Start project before you start

### method 1 via XMAPP

1. start the XMAPP
2. start the Apache and MySQL
3. open the phpMyAdmin

### method 2 via Docker

#### Windows

1. install Docker Desktop
2. start the Docker Desktop
3. open the phpMyAdmin

#### Mac

1. install orbstack (https://orbstack.com/) or via the terminal

```shell
brew install orbstack
```

2. start the orbstack and start the docker-compose via the terminal

```shell
orbstack start
cd /path/to/your/project
docker-compose up -d
```

## About the Database connection

> please note that I want to replace the MysqlConnector with the MySql.Data package for better performance and more
> functionality.
> Consider removing the MysqlConnector package from the project on the future.

## about the password encryption

the password encryption will be encrypted by the Bcrypt Algorithm. which is a password hashing algorithm.

### to use the Bcrypt Algorithm

```csharp
// just the project just use the HashPassword method

accountController.HashPassword("123456", thePwdFromDB); // <- it will return a boolean value
// for verifying the password
accountController.isValidatePassword("123456", thePwdFromDB); // <- it will return a boolean value

using Bcrypt.Net.Next;

string password = "123456";


var hashedPassword = BCrypt.Net.Next.BCrypt.EnhancedHashPassword(password);

// verify the password
var passwordFromDB = GetUserPasswordFromDB();
bool isPasswordMatch = BCrypt.Net.Next.BCrypt.EnhancedVerify(password, passwordFromDB);


```

[//]: # (### to use the Mysql.Data package)

[//]: # ()

[//]: # (```csharp)

[//]: # (using MySql.Data.MySqlClient;)

[//]: # ()

[//]: # (string password = "123456";)

[//]: # ()

[//]: # (// create a new instance of the MySqlConnection class)

[//]: # (MySqlConnection connection = new MySqlConnection&#40;"server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;"&#41;;)

[//]: # ()

[//]: # (using &#40;connection&#41;)

[//]: # ({)

[//]: # (    // open the connection)

[//]: # (    connection.Open&#40;&#41;;)

[//]: # ()

[//]: # (    // create a new instance of the MySqlCommand class)

[//]: # (    MySqlCommand command = new MySqlCommand&#40;"SELECT * FROM users WHERE username = @username", connection&#41;;)

[//]: # ()

[//]: # (    // add the parameter to the command)

[//]: # (    command.Parameters.AddWithValue&#40;"@username", "admin"&#41;;)

[//]: # ()

[//]: # (    // execute the command)

[//]: # (    using &#40;MySqlDataReader reader = command.ExecuteReader&#40;&#41;&#41;)

[//]: # (    {)

[//]: # (        // read the data)

[//]: # (        while &#40;reader.Read&#40;&#41;&#41;)

[//]: # (        {)

[//]: # (            Console.WriteLine&#40;reader["username"]&#41;;)

[//]: # (        })

[//]: # (    })

[//]: # (    )

[//]: # (    // method 2)

[//]: # (    // using the dictionary for the parameters)

[//]: # (    Dictionary<string, object> parameters = new Dictionary<string, object>&#40;&#41;;)

[//]: # (    parameters.Add&#40;"@username", "admin"&#41;;)

[//]: # (    command.Parameters.AddRange&#40;parameters.Keys.Select&#40;key => new MySqlParameter&#40;key, parameters[key]&#41;&#41;.ToArray&#40;&#41;&#41;;)

[//]: # ()

[//]: # (    // execute the command)

[//]: # (    using &#40;MySqlDataReader reader = command.ExecuteReader&#40;&#41;&#41;)

[//]: # (    {)

[//]: # (        // read the data)

[//]: # (        while &#40;reader.Read&#40;&#41;&#41;)

[//]: # (        {)

[//]: # (            Console.WriteLine&#40;reader["username"]&#41;;)

[//]: # (        })

[//]: # (    })

[//]: # (    )

[//]: # (})

[//]: # (```)

## How to use the Database class

The `Database` class is responsible for managing the database connection and executing SQL commands.
It uses the `MySql.Data.MySqlClient` namespace from the `MySql.Data` package.

```csharp
Dictionary<string, object> parameters = new Dictionary<string, object>();
parameters.Add("@username", "admin");
parameters.Add("@password", "password");
database.ExecuteNonQueryCommand("INSERT INTO users (username, password) VALUES (@username, @password)", parameters);

// ExecuteNonQueryCommand is for the command that will return no value
database.ExecuteNonQueryCommand("INSERT INTO users (username, password) VALUES (@username, @password)", parameters);

// ExecuteReaderCommand is for the command that will return a DataReader

var result = database.ExecuteReaderCommand("SELECT * FROM users WHERE username = @username", parameters);

// ExecuteDataTable is for the command that will return a DataTable
var result = database.ExecuteDataTable("SELECT * FROM users WHERE username = @username", parameters);
// it will return a DataTable

```

## Remote Database Connection

1. userid:`root`
2. password:`ixYr958dIF4Zo3Xvbnp62SQ7f1yVs0Mt`
3. Host:`hkg1.clusters.zeabur.com`
4. Port:`32298`

```shell
mysqlsh --sql --host=hkg1.clusters.zeabur.com --port=32298 --user=root --password=ixYr958dIF4Zo3Xvbnp62SQ7f1yVs0Mt --schema=zeabur
```