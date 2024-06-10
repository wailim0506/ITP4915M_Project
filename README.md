# Database Class 使用指南

本指南將詳細說明如何使用 `Database` 類別中的各種方法，以及在不同情況下應該使用哪種方法。此外，我們將展示每個方法返回的模擬數據結果。

## 目錄
1. [初始化](#初始化)
2. [方法概覽](#方法概覽)
3. [不同情況下的最佳使用方法](#不同情況下的最佳使用方法)
4. [模擬數據結果](#模擬數據結果)

## 初始化

在使用 `Database` 類別之前，需要先初始化一個 `Database` 對象。初始化時可以選擇傳入一個連接字符串，否則系統會使用預設的連接字符串。

```csharp
using controller;

var db = new Database();
```

### 1. `GetConnectionString()`

返回一個有效的連接字符串。系統會測試多個連接字符串並返回第一個可用的連接。

```csharp
string connectionString = Database.GetConnectionString();
```

## 方法概覽

### 1. `ExecuteScalarCommand(string sqlQuery, Dictionary<string, object> queryParameters)`

執行一個 SQL 查詢，並返回查詢結果的第一行第一列的值。

**參數：**
- `sqlQuery`: 要執行的 SQL 查詢。
- `queryParameters`: SQL 查詢的參數集合，使用字典來表示。

**返回值：**
- 查詢結果的第一行第一列的值。

```csharp
object result = db.ExecuteScalarCommand("SELECT COUNT(*) FROM users", null);
```

### 2. `ExecuteNonQueryCommand(string sqlQuery, Dictionary<string, object> queryParameters)`

執行一個不返回結果集的 SQL 命令，例如 `INSERT`、`UPDATE` 或 `DELETE`。

**參數：**
- `sqlQuery`: 要執行的 SQL 查詢。
- `queryParameters`: SQL 查詢的參數集合，使用字典來表示。

**返回值：**
- 無返回值。

```csharp
db.ExecuteNonQueryCommand("UPDATE users SET name = @name WHERE id = @id", new Dictionary<string, object> { { "@name", "John Doe" }, { "@id", 1 } });
```

### 3. `ExecuteReaderCommand(string sqlQuery, Dictionary<string, object> queryParameters)`

執行一個 SQL 查詢，並返回一個 `MySqlDataReader` 對象來讀取結果集。

**參數：**
- `sqlQuery`: 要執行的 SQL 查詢。
- `queryParameters`: SQL 查詢的參數集合，使用字典來表示。

**返回值：**
- `MySqlDataReader` 對象。

```csharp
MySqlDataReader reader = db.ExecuteReaderCommand("SELECT * FROM users", null);
while (reader.Read())
{
    Console.WriteLine(reader["name"]);
}
```

### 4. `CreateCommand(string query, Dictionary<string, object> parameters)`

創建一個 `MySqlCommand` 對象，用於執行 SQL 查詢。

**參數：**
- `query`: 要執行的 SQL 查詢。
- `parameters`: SQL 查詢的參數集合，使用字典來表示。

**返回值：**
- `MySqlCommand` 對象。

```csharp
MySqlCommand command = db.CreateCommand("UPDATE users SET name = @name WHERE id = @id", new Dictionary<string, object> { { "@name", "John Doe" }, { "@id", 1 } });
```

### 5. `ExecuteNonQuery(MySqlCommand command)`

執行一個不返回結果集的 SQL 命令。

**參數：**
- `command`: 要執行的 `MySqlCommand` 對象。

**返回值：**
- 無返回值。

```csharp
db.ExecuteNonQuery(command);
```

### 6. `ExecuteDataTable(string sqlQuery, Dictionary<string, object> queryParameters)`

執行一個 SQL 查詢，並返回一個 `DataTable` 對象來保存結果集。

**參數：**
- `sqlQuery`: 要執行的 SQL 查詢。
- `queryParameters`: SQL 查詢的參數集合，使用字典來表示。

**返回值：**
- `DataTable` 對象。

```csharp
DataTable dt = db.ExecuteDataTable("SELECT * FROM users", null);
foreach (DataRow row in dt.Rows)
{
    Console.WriteLine(row["name"]);
}
```

### 7. `ExecuteDataTable(string sqlQuery)`

執行一個無參數的 SQL 查詢，並返回一個 `DataTable` 對象來保存結果集。

**參數：**
- `sqlQuery`: 要執行的 SQL 查詢。

**返回值：**
- `DataTable` 對象。

```csharp
DataTable dt = db.ExecuteDataTable("SELECT * FROM users");
foreach (DataRow row in dt.Rows)
{
    Console.WriteLine(row["name"]);
}
```

### 8. `ExecuteScalar(string sqlCmd)`

執行一個無參數的 SQL 查詢，並返回查詢結果的第一行第一列的值。

**參數：**
- `sqlCmd`: 要執行的 SQL 查詢。

**返回值：**
- 查詢結果的第一行第一列的值。

```csharp
object result = db.ExecuteScalar("SELECT COUNT(*) FROM users");
```

## 不同情況下的最佳使用方法

### 1. 獲取單一值
當需要從資料庫獲取單一值（例如計算總數或獲取單個字段值）時，應使用 `ExecuteScalarCommand` 或 `ExecuteScalar` 方法。

**示例：**
```csharp
var count = db.ExecuteScalarCommand("SELECT COUNT(*) FROM users", null);
// 或
var count = db.ExecuteScalar("SELECT COUNT(*) FROM users");
```

### 2. 執行不返回結果的命令
當需要執行 `INSERT`、`UPDATE` 或 `DELETE` 等不返回結果集的命令時，應使用 `ExecuteNonQueryCommand` 或 `ExecuteNonQuery` 方法。

**示例：**
```csharp
db.ExecuteNonQueryCommand("UPDATE users SET name = @name WHERE id = @id", new Dictionary<string, object> { { "@name", "John Doe" }, { "@id", 1 } });
// 或
var command = db.CreateCommand("UPDATE users SET name = @name WHERE id = @id", new Dictionary<string, object> { { "@name", "John Doe" }, { "@id", 1 } });
db.ExecuteNonQuery(command);
```

### 3. 獲取結果集
當需要執行一個返回結果集的查詢時，應使用 `ExecuteReaderCommand` 或 `ExecuteDataTable` 方法。

**示例：**
```csharp
// 使用 ExecuteReaderCommand
var reader = db.ExecuteReaderCommand("SELECT * FROM users", null);
while (reader.Read())
{
    Console.WriteLine(reader["name"]);
}

// 或使用 ExecuteDataTable
DataTable dt = db.ExecuteDataTable("SELECT * FROM users");
foreach (DataRow row in dt.Rows)
{
    Console.WriteLine(row["name"]);
}
```

## 模擬數據結果

### `ExecuteScalarCommand` 和 `ExecuteScalar`
```csharp
var result = db.ExecuteScalarCommand("SELECT COUNT(*) FROM users", null);
// 假設結果為：5
```

### `ExecuteNonQueryCommand` 和 `ExecuteNonQuery`
```csharp
db.ExecuteNonQueryCommand("UPDATE users SET name = @name WHERE id = @id", new Dictionary<string, object> { { "@name", "John Doe" }, { "@id", 1 } });
// 假設更新了1行數據
```

### `ExecuteReaderCommand`
```csharp
var reader = db.ExecuteReaderCommand("SELECT * FROM users", null);
// 假設返回兩行數據：
// id: 1, name: "John Doe"
// id: 2, name: "Jane Smith"
```

### `ExecuteDataTable`
```csharp
DataTable dt = db.ExecuteDataTable("SELECT * FROM users");
// 假設返回兩行數據：
// DataRow 1: [id: 1, name: "John Doe"]
// DataRow 2: [id: 2, name: "Jane Smith"]
```

### `CreateCommand` 和 `ExecuteNonQuery`
```csharp
var command = db.CreateCommand("UPDATE users SET name = @name WHERE id = @id", new Dictionary<string, object> { { "@name", "John Doe" }, { "@id", 1 } });
db.ExecuteNonQuery(command);
// 假設更新了1行數據
```

這些方法的組合提供了一個強大且靈活的方式來與資料庫交互，根據不同的需求選擇適合的方法可以有效提高開發效率和代碼可讀性。
