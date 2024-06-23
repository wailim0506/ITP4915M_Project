# Readme

## configuration

### app.config

```xml
<appSettings>
   <add key="connectionString1" value="server=localhost;database=your_database;uid=your_username;password=your_password;"/>
</appSettings>
```

### google api

```xml
<appSettings>
   <add key="google_api_key" value="your_api_key"/>
</appSettings>
```

### Database Account for Generating Reports

```sql
GRANT ALL PRIVILEGES ON *.* TO `report`@`%` IDENTIFIED BY PASSWORD '*8E2B5A8BF835E14935C5C7F3ADE1022CE13A371F' WITH GRANT OPTION;
```

[//]: # ()

[//]: # (本指南將詳細說明如何使用新版 `Database` 類別中的各種方法，以及在不同情況下應該使用哪種方法。此外，我們將展示每個方法返回的模擬數據結果，並解釋新舊版本之間的主要區別。)

[//]: # ()

[//]: # (## 目錄)

[//]: # ()

[//]: # (1. [初始化]&#40;#初始化&#41;)

[//]: # (2. [方法概覽]&#40;#方法概覽&#41;)

[//]: # (3. [不同情況下的最佳使用方法]&#40;#不同情況下的最佳使用方法&#41;)

[//]: # (4. [模擬數據結果]&#40;#模擬數據結果&#41;)

[//]: # (5. [新版與舊版的區別]&#40;#新版與舊版的區別&#41;)

[//]: # ()

[//]: # (## 初始化)

[//]: # ()

[//]: # (在使用 `Database` 類別之前，需要先初始化一個 `Database` 對象。初始化時可以選擇傳入一個連接字符串，否則系統會使用預設的連接字符串。)

[//]: # ()

[//]: # (```csharp)

[//]: # (using controller;)

[//]: # ()

[//]: # (var db = new Database&#40;&#41;;)

[//]: # (```)

[//]: # ()

[//]: # (### 1. `GetConnectionStringAsync&#40;&#41;`)

[//]: # ()

[//]: # (返回一個有效的連接字符串。系統會測試多個連接字符串並返回第一個可用的連接。)

[//]: # ()

[//]: # (```csharp)

[//]: # (string connectionString = Database.GetConnectionStringAsync&#40;&#41;.Result;)

[//]: # (```)

[//]: # ()

[//]: # (## 方法概覽)

[//]: # ()

[//]: # (### 1. `ExecuteScalarCommandAsync&#40;string sqlQuery, Dictionary<string, object> queryParameters&#41;`)

[//]: # ()

[//]: # (執行一個 SQL 查詢，並返回查詢結果的第一行第一列的值。)

[//]: # ()

[//]: # (**參數：**)

[//]: # ()

[//]: # (- `sqlQuery`: 要執行的 SQL 查詢。)

[//]: # (- `queryParameters`: SQL 查詢的參數集合，使用字典來表示。)

[//]: # ()

[//]: # (**返回值：**)

[//]: # ()

[//]: # (- 查詢結果的第一行第一列的值。)

[//]: # ()

[//]: # (```csharp)

[//]: # (object result = db.ExecuteScalarCommandAsync&#40;"SELECT COUNT&#40;*&#41; FROM users", null&#41;.Result;)

[//]: # (```)

[//]: # ()

[//]: # (### 2. `ExecuteNonQueryCommandAsync&#40;string sqlQuery, Dictionary<string, object> queryParameters&#41;`)

[//]: # ()

[//]: # (執行一個不返回結果集的 SQL 命令，例如 `INSERT`、`UPDATE` 或 `DELETE`。)

[//]: # ()

[//]: # (**參數：**)

[//]: # ()

[//]: # (- `sqlQuery`: 要執行的 SQL 查詢。)

[//]: # (- `queryParameters`: SQL 查詢的參數集合，使用字典來表示。)

[//]: # ()

[//]: # (**返回值：**)

[//]: # ()

[//]: # (- 無返回值。)

[//]: # ()

[//]: # (```csharp)

[//]: # (db.ExecuteNonQueryCommandAsync&#40;"UPDATE users SET name = @name WHERE id = @id", new Dictionary<string, object> { { "@name", "John Doe" }, { "@id", 1 } }&#41;.Wait&#40;&#41;;)

[//]: # (```)

[//]: # ()

[//]: # (### 3. `ExecuteDataTableAsync&#40;string sqlQuery, Dictionary<string, object> queryParameters&#41;`)

[//]: # ()

[//]: # (執行一個 SQL 查詢，並返回一個 `DataTable` 對象來保存結果集。)

[//]: # ()

[//]: # (**參數：**)

[//]: # ()

[//]: # (- `sqlQuery`: 要執行的 SQL 查詢。)

[//]: # (- `queryParameters`: SQL 查詢的參數集合，使用字典來表示。)

[//]: # ()

[//]: # (**返回值：**)

[//]: # ()

[//]: # (- `DataTable` 對象。)

[//]: # ()

[//]: # (```csharp)

[//]: # (DataTable dt = db.ExecuteDataTableAsync&#40;"SELECT * FROM users", null&#41;.Result;)

[//]: # (foreach &#40;DataRow row in dt.Rows&#41;)

[//]: # ({)

[//]: # (    Console.WriteLine&#40;row["name"]&#41;;)

[//]: # (})

[//]: # (```)

[//]: # ()

[//]: # (## 不同情況下的最佳使用方法)

[//]: # ()

[//]: # (### 1. 獲取單一值)

[//]: # ()

[//]: # (當需要從資料庫獲取單一值（例如計算總數或獲取單個字段值）時，應使用 `ExecuteScalarCommandAsync` 方法。)

[//]: # ()

[//]: # (**示例：**)

[//]: # ()

[//]: # (```csharp)

[//]: # (var count = db.ExecuteScalarCommandAsync&#40;"SELECT COUNT&#40;*&#41; FROM users", null&#41;.Result;)

[//]: # (```)

[//]: # ()

[//]: # (### 2. 執行不返回結果的命令)

[//]: # ()

[//]: # (當需要執行 `INSERT`、`UPDATE` 或 `DELETE` 等不返回結果集的命令時，應使用 `ExecuteNonQueryCommandAsync` 方法。)

[//]: # ()

[//]: # (**示例：**)

[//]: # ()

[//]: # (```csharp)

[//]: # (db.ExecuteNonQueryCommandAsync&#40;"UPDATE users SET name = @name WHERE id = @id", new Dictionary<string, object> { { "@name", "John Doe" }, { "@id", 1 } }&#41;.Wait&#40;&#41;;)

[//]: # (```)

[//]: # ()

[//]: # (### 3. 獲取結果集)

[//]: # ()

[//]: # (當需要執行一個返回結果集的查詢時，應使用 `ExecuteDataTableAsync` 方法。)

[//]: # ()

[//]: # (**示例：**)

[//]: # ()

[//]: # (```csharp)

[//]: # (DataTable dt = db.ExecuteDataTableAsync&#40;"SELECT * FROM users"&#41;.Result;)

[//]: # (foreach &#40;DataRow row in dt.Rows&#41;)

[//]: # ({)

[//]: # (    Console.WriteLine&#40;row["name"]&#41;;)

[//]: # (})

[//]: # (```)

[//]: # ()

[//]: # (## 模擬數據結果)

[//]: # ()

[//]: # (### `ExecuteScalarCommandAsync`)

[//]: # ()

[//]: # (```csharp)

[//]: # (var result = db.ExecuteScalarCommandAsync&#40;"SELECT COUNT&#40;*&#41; FROM users", null&#41;.Result;)

[//]: # (// 假設結果為：5)

[//]: # (```)

[//]: # ()

[//]: # (### `ExecuteNonQueryCommandAsync`)

[//]: # ()

[//]: # (```csharp)

[//]: # (db.ExecuteNonQueryCommandAsync&#40;"UPDATE users SET name = @name WHERE id = @id", new Dictionary<string, object> { { "@name", "John Doe" }, { "@id", 1 } }&#41;.Wait&#40;&#41;;)

[//]: # (// 假設更新了1行數據)

[//]: # (```)

[//]: # ()

[//]: # (### `ExecuteDataTableAsync`)

[//]: # ()

[//]: # (```csharp)

[//]: # (DataTable dt = db.ExecuteDataTableAsync&#40;"SELECT * FROM users"&#41;.Result;)

[//]: # (// 假設返回兩行數據：)

[//]: # (// DataRow 1: [id: 1, name: "John Doe"])

[//]: # (// DataRow 2: [id: 2, name: "Jane Smith"])

[//]: # (```)

[//]: # ()

[//]: # (## 新版與舊版的區別)

[//]: # ()

[//]: # (### 1. 非同步方法)

[//]: # ()

[//]: # (新版代碼中，所有主要的數據庫操作方法都改為了非同步方法，使用 `async` 和 `await`)

[//]: # (關鍵字來提高性能，特別是在處理大量數據或需要等待數據庫響應時。舊版代碼中，很多方法是同步的，這可能會導致應用程序在等待數據庫響應時變得無響應。)

[//]: # ()

[//]: # (### 2. 簡化的內部邏輯)

[//]: # ()

[//]: # (新版代碼中，通過 `ExecuteCommandAsync` 方法統一了數據庫命令的執行邏輯，減少了代碼重複，提高了可讀性和可維護性。這使得新增或修改數據庫操作邏輯變得更加容易。)

[//]: # ()

[//]: # (### 3. 改進的異常處理)

[//]: # ()

[//]: # (新版代碼中的異常處理更具體，將異常捕獲和處理封裝在統一的方法中，使得異常處理邏輯更加集中和一致，便於調試和維護。)

[//]: # ()

[//]: # (### 4. 減少了不必要的同步方法)

[//]: # ()

[//]: # (新版代碼中去除了部分不必要的同步方法，鼓勵使用非同步方法來進行數據庫操作，這符合現代 C# 編程的最佳實踐，有助於提升應用程序的響應速度和用戶體驗。)

[//]: # ()

[//]: # (這些改進使得新版 `Database` 類別在性能和可維護性上都有了顯著提升。)

[//]: # ()

[//]: # (## 新增示例方法)

[//]: # ()

[//]: # (### 獲取送貨日期)

[//]: # ()

[//]: # (此方法展示如何使用 `ExecuteDataTableAsync` 方法來獲取送貨日期。)

[//]: # ()

[//]: # (```csharp)

[//]: # (public string GetDeliveryDate&#40;string id&#41;)

[//]: # ({)

[//]: # (    return _db.ExecuteDataTableAsync&#40;$"SELECT shippingDate FROM shipping_detail WHERE orderID = '{id}'"&#41;.Result)

[//]: # (        .Rows[0][0])

[//]: # (        .ToString&#40;&#41;.Split&#40;' '&#41;[0];)

[//]: # (})

[//]: # (```)

[//]: # ()

[//]: # (這個方法中使用 `ExecuteDataTableAsync` 來執行查詢，並且使用 `.Result`)

[//]: # (來等待查詢完成並獲取結果。隨後，從結果中提取 `shippingDate` 字段，並返回日期部分。)

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

JetBrains.Annotations -Version 2023.3.0

### for testing

NUnit version 4.1.0

Moq version 4.20.70

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
Install-Package JetBrains.Annotations -Version 2023.3.0
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

accountController.HashPassword("123456"); // <- it will return a boolean value
// for verifying the password
accountController.isValidatePassword("123456", thePwdFromDB); // <- it will return a boolean value

using Bcrypt.Net.Next;

string password = "123456";


var hashedPassword = BCrypt.Net.Next.BCrypt.EnhancedHashPassword(password);

// verify the password
var passwordFromDB = GetUserPasswordFromDB();
bool isPasswordMatch = BCrypt.Net.Next.BCrypt.EnhancedVerify(password, passwordFromDB);


```

## Remote Database Connection
