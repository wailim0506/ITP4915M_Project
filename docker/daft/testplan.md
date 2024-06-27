## Test Cases

## Test Environment Setup

To ensure a consistent and reproducible environment for running the test cases, follow the steps below to set up your test environment. This setup includes configuring the necessary tools, dependencies, and configurations required to execute the unit tests for the controllers.

### Prerequisites

1. **.NET SDK**: Ensure you have the .NET SDK installed. You can download it from the [official .NET website](https://dotnet.microsoft.com/download).

2. **Unit Testing Framework**: This guide uses xUnit as the testing framework. Ensure you have the xUnit NuGet package installed in your project.

3. **Mocking Library**: This guide uses Moq for creating mock objects. Ensure you have the Moq NuGet package installed in your project.

### Steps to Set Up the Test Environment

1. **Install .NET SDK**
   - Download and install the latest .NET SDK from the [official .NET website](https://dotnet.microsoft.com/download).

2. **Create a New .NET Project**
   - Open a terminal and create a new .NET project:

     ```bash
     dotnet new console -n MyProject
     cd MyProject
     ```

3. **Add Unit Test Project**
   - Add a unit test project to your solution:

     ```bash
     dotnet new xunit -n MyProject.Tests
     cd MyProject.Tests
     ```

   - Add a reference to the main project in your test project:

     ```bash
     dotnet add reference ../MyProject/MyProject.csproj
     ```

4. **Install Required NuGet Packages**
   - Install xUnit and Moq packages:

     ```bash
     dotnet add package xunit
     dotnet add package Moq
     dotnet add package xunit.runner.visualstudio
     ```

5. **Configure Your Test Project**
   - Ensure your test project references the main project and includes necessary using directives.

### Example Code for Setting Up Test Environment

#### `AccountControllerTests.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Data;
using Xunit;
using Moq;
using MyProject.Controllers;
using MyProject.Models;

namespace MyProject.Tests.Controllers
{
    public class AccountControllerTests
    {
        private readonly AccountController _accountController;
        private readonly FakeDatabaseAccount _fakeDatabaseAccount;
        private readonly FakeUIController _fakeUIController;

        public AccountControllerTests()
        {
            _fakeDatabaseAccount = new FakeDatabaseAccount();
            _fakeUIController = new FakeUIController(_accountController, _fakeDatabaseAccount);
            _accountController = new AccountController(_fakeDatabaseAccount);
        }

        [Fact]
        public void DelAccount_DisablesAccount()
        {
            // Arrange
            string userId = "LMC00001";
            _fakeDatabaseAccount.SetUserInfo(userId, "John", "Doe");

            // Act
            _accountController.Login(userId, "password", _fakeUIController);
            var result = _accountController.DelAccount();

            // Assert
            Assert.True(result);
            Assert.True(_fakeDatabaseAccount.AccountDisabled);
        }

        // Additional test cases...
    }
}
```

#### `FakeDatabaseAccount.cs`

```csharp
using System;
using System.Collections.Generic;
using System.Data;
using MyProject.Models;

namespace MyProject.Tests.Fakes
{
    public class FakeDatabaseAccount : Database
    {
        public DataTable _dataTable;
        private string _storedPassword;
        private string _accountStatus;

        public bool LogSet { get; private set; }
        public bool AccountDisabled { get; private set; }

        public void SetAccountData(string userId, string password, string status)
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("password");
            _dataTable.Columns.Add("status");
            _dataTable.Rows.Add(BCrypt.Net.BCrypt.HashPassword(password), status == "active" ? "active" : "inactive");
            _storedPassword = password;
            _accountStatus = status;
        }

        public void SetUserInfo(string userId, string firstName, string lastName)
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("firstName");
            _dataTable.Columns.Add("lastName");
            _dataTable.Columns.Add("accountID");
            _dataTable.Rows.Add(firstName, lastName, "accountID");
        }

        public void SetLogData(string userId, string lastLoginDate)
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("loginDate");
            _dataTable.Rows.Add(lastLoginDate);
        }

        public void SetPwdChangeData(string userId, DateTime pwdChangeDate)
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("pwdChangeDate");
            _dataTable.Rows.Add(pwdChangeDate);
        }

        public override DataTable ExecuteDataTable(string sqlStr, Dictionary<string, object> queryParameters = null)
        {
            return _dataTable;
        }

        public override void ExecuteNonQueryCommand(string sqlStr, Dictionary<string, object> queryParameters = null)
        {
            if (sqlStr.Contains("INSERT INTO"))
            {
                LogSet = true;
            }

            if (sqlStr.Contains("UPDATE customer_account SET Status = 'disable'"))
            {
                AccountDisabled = true;
            }
        }
    }
}
```

### Running the Tests

1. **Build the Solution**

   ```bash
   dotnet build
   ```

2. **Run the Tests**

   ```bash
   dotnet test
   ```

### Verifying the Test Results

- After running the tests, you should see a summary indicating how many tests passed, failed, or were skipped. Ensure all tests pass successfully to verify the correct functionality of your controllers and their interactions with the database and UI components.

By following these steps, you can set up a robust test environment to execute and validate your test cases, ensuring the reliability and accuracy of your application.

##

### 1. DelAccount_DisablesAccount

**Purpose:** Verify that when a user deletes their account, the account status is set to disabled.

**Expected Result:** The account is successfully disabled and the method returns `true`.

**Test Steps:**

1. Initialize `FakeDatabaseAccount` and `AccountController`.
2. Set user data in `FakeDatabaseAccount`.
3. Log in the user.
4. Call the `DelAccount` method.
5. Assert that the method returns `true` and the account is disabled.

```csharp
        [Fact]
        public void DelAccount_DisablesAccount()
        {
            // Arrange
            string userId = "LMC00001";
            _FakeDatabaseAccount.SetUserInfo(userId, "John", "Doe");

            // Act
            _accountController.Login(userId, "password", _fakeUIController);
            var result = _accountController.DelAccount();

            // Assert
            Assert.True(result);
            Assert.True(_FakeDatabaseAccount.AccountDisabled);
        }
```

### 2. Login_AccountInactive_ReturnsFalse

**Purpose:** Verify that the login fails if the account is inactive.

**Expected Result:** The login attempt fails and the method returns `false`.

**Test Steps:**

1. Initialize `FakeDatabaseAccount` and `AccountController`.
2. Set account data in `FakeDatabaseAccount` with status as inactive.
3. Attempt to log in with valid credentials.
4. Assert that the method returns `false` and the user is not logged in.

```csharp
        [Fact]
        public void Login_AccountInactive_ReturnsFalse()
        {
            // Arrange
            string userId = "LMC00001";
            string password = "password";
            _FakeDatabaseAccount.SetAccountData(userId, password, "inactive");

            // Act
            var result = _accountController.Login(userId, password, _fakeUIController);

            // Assert
            Assert.False(result);
            Assert.False(_accountController.IsLogin);
        }
```

### 3. Login_AccountNotFound_ReturnsFalse

**Purpose:** Verify that the login fails if the account is not found.

**Expected Result:** The login attempt fails and the method returns `false`.

**Test Steps:**

1. Initialize `FakeDatabaseAccount` and `AccountController`.
2. Attempt to log in with credentials that do not match any account.
3. Assert that the method returns `false` and the user is not logged in.

```csharp
        public void Login_AccountNotFound_ReturnsFalse()
        {
            // Arrange
            string userId = "LMC00001";
            string password = "password";

            // Act
            var result = _accountController.Login(userId, password, _fakeUIController);

            // Assert
            Assert.False(result);
            Assert.False(_accountController.IsLogin);
        }
```

### 4. Login_InvalidCredentials_ReturnsFalse

**Purpose:** Verify that the login fails if the credentials are invalid.

**Expected Result:** The login attempt fails and the method returns `false`.

**Test Steps:**

1. Initialize `FakeDatabaseAccount` and `AccountController`.
2. Set account data in `FakeDatabaseAccount` with valid user ID but different password.
3. Attempt to log in with the wrong password.
4. Assert that the method returns `false` and the user is not logged in.

```csharp
        [Fact]
        public void Login_InvalidCredentials_ReturnsFalse()
        {
            // Arrange
            string userId = "LMC00001";
            string password = "invalidPassword";
            _FakeDatabaseAccount.SetAccountData(userId, "storedPassword", "active");

            // Act
            var result = _accountController.Login(userId, password, _fakeUIController);

            // Assert
            Assert.False(result);
            Assert.False(_accountController.IsLogin);
        }
```

#### Fake FakeDatabase for AccountController

```csharp
public partial class FakeDatabaseAccount : Database
    {
        public DataTable _dataTable;
        private string _storedPassword;
        private string _accountStatus;

        public bool LogSet { get; private set; }
        public bool AccountDisabled { get; private set; }

        public void SetAccountData(string userId, string password, string status)
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("password");
            _dataTable.Columns.Add("status");
            _dataTable.Rows.Add(BCrypt.Net.BCrypt.HashPassword(password), status == "active" ? "active" : "inactive");
            _storedPassword = password;
            _accountStatus = status;
        }

        public void SetUserInfo(string userId, string firstName, string lastName)
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("firstName");
            _dataTable.Columns.Add("lastName");
            _dataTable.Columns.Add("accountID");
            _dataTable.Rows.Add(firstName, lastName, "accountID");
        }

        public void SetLogData(string userId, string lastLoginDate)
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("loginDate");
            _dataTable.Rows.Add(lastLoginDate);
        }

        public void SetPwdChangeData(string userId, DateTime pwdChangeDate)
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("pwdChangeDate");
            _dataTable.Rows.Add(pwdChangeDate);
        }

        public override DataTable ExecuteDataTable(string sqlStr, Dictionary<string, object> queryParameters = null)
        {
            return _dataTable;
        }

        public override void ExecuteNonQueryCommand(string sqlStr, Dictionary<string, object> queryParameters = null)
        {
            if (sqlStr.Contains("INSERT INTO"))
            {
                LogSet = true;
            }

            if (sqlStr.Contains("UPDATE customer_account SET Status = 'disable'"))
            {
                AccountDisabled = true;
            }
        }
    }
```

#### Fake UiController for AccountController

```csharp
    public class FakeUIController : UIController
    {
        public override void SetPermission(string userId)
        {
        }

        public FakeUIController(AccountController accountController, Database db = null) : base(accountController, db)
        {
        }
    }
```

### 5. FavouriteControllerTests: IsFavourite_InvalidData_ReturnsFalse

**Purpose:** Verify that the `IsFavourite` method returns `false` when given invalid data.

**Expected Result:** The method returns `false`.

**Test Steps:**

1. Initialize `FavouriteFakeDatabase` and `favouriteController`.
2. Set up `FavouriteFakeDatabase` with invalid data.
3. Call the `IsFavourite` method with part number and customer ID that do not match.
4. Assert that the method returns `false`.

```csharp
        [Fact]
        public void IsFavourite_InvalidData_ReturnsFalse()
        {
            // Arrange
            string partNumber = "LMP00003";
            string customerId = "LMS00001";

            // Act
            var result = _favouriteController.IsFavourite(partNumber, customerId);

            // Assert
            Assert.False(result);
        }
```

#### Fake FakeDatabase for FavouriteController

```csharp
    public partial class FavouriteFakeDatabase : Database
    {
        private bool _throwError;
        DataTable _dataTable;

        public bool IsFavouriteRemoved { get; private set; }
        public bool IsFavouriteAdded { get; private set; }

        public void SetFavouriteData(string customerId)
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("customerID");
            _dataTable.Columns.Add("itemID");
            _dataTable.Columns.Add("partNumber");
            _dataTable.Columns.Add("categoryID");
            _dataTable.Rows.Add(customerId, customerId, "LMP00003", "1");
        }

        public void SetProductData(string partNumber, string itemId, bool throwError = false)
        {
            _throwError = throwError;
            _dataTable = new DataTable();
            _dataTable.Columns.Add("itemID");
            _dataTable.Rows.Add(itemId);
        }

        public void SetFavouriteCheckData(string partNumber, string customerId)
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("customerID");
            _dataTable.Columns.Add("itemID");
            _dataTable.Columns.Add("partNumber");
            _dataTable.Rows.Add(customerId, customerId, partNumber);
        }

        public DataTable ExecuteDataTable(string sqlStr, Dictionary<string, object> queryParameters = null)
        {
            if (_throwError)
            {
                throw new Exception("Database error");
            }

            return _dataTable;
        }

        public void ExecuteNonQueryCommand(string sqlStr, Dictionary<string, object> queryParameters = null)
        {
            if (_throwError)
            {
                throw new Exception("Database error");
            }

            if (sqlStr.Contains("DELETE FROM favourite"))
            {
                IsFavouriteRemoved = true;
            }

            if (sqlStr.Contains("INSERT INTO favourite"))
            {
                IsFavouriteAdded = true;
            }
        }
    }
```

### 6. RecoveryControllerTests: CheckEmail_UniqueEmail_ReturnsTrue

**Purpose:** Verify that the `CheckEmail` method returns `true` for a unique email.

**Expected Result:** The method returns `true`.

**Test Steps:**

1. Initialize `RecoveryFakeDatabase` and `RecoveryController`.
2. Set up `RecoveryFakeDatabase` with no matching email data.
3. Call the `CheckEmail` method with a unique email.
4. Assert that the method returns `true`.

```csharp
        [Fact]
        public void CheckEmail_UniqueEmail_ReturnsTrue()
        {
            // Arrange
            string email = "unique@example.com";
            _RecoveryFakeDatabase.SetEmailCheckDataTable();

            // Act
            var result = _recoveryController.CheckEmail(email);

            // Assert
            Assert.True(result);
        }
```

### 7. RecoveryControllerTests: CheckPhone_UniquePhone_ReturnsTrue

**Purpose:** Verify that the `CheckPhone` method returns `true` for a unique phone number.

**Expected Result:** The method returns `true`.

**Test Steps:**

1. Initialize `RecoveryFakeDatabase` and `RecoveryController`.
2. Set up `RecoveryFakeDatabase` with no matching phone number data.
3. Call the `CheckPhone` method with a unique phone number.
4. Assert that the method returns `true`.

```csharp
        [Fact]
        public void CheckPhone_UniquePhone_ReturnsTrue()
        {
            // Arrange
            string phone = "1234567890";
            _RecoveryFakeDatabase.SetPhoneCheckDataTable();

            // Act
            var result = _recoveryController.CheckPhone(phone);

            // Assert
            Assert.True(result);
        }
```

### 8. RecoveryControllerTests: FindUser_InvalidUserDetails_ReturnsFalse

**Purpose:** Verify that the `FindUser` method returns `false` when provided with invalid user details.

**Expected Result:** The method returns `false`.

**Test Steps:**

1. Initialize `RecoveryFakeDatabase` and `RecoveryController`.
2. Set up `RecoveryFakeDatabase` with no matching user details.
3. Call the `FindUser` method with invalid user details.
4. Assert that the method returns `false`.

```csharp
        [Fact]
        public void FindUser_InvalidUserDetails_ReturnsFalse()
        {
            // Arrange
            string userId = TestData.invalidId;
            string email = TestData.invalidEmail;
            string phone = TestData.invalidPhone;

            _RecoveryFakeDatabase.SetValidations(false, false, false);

            // Act
            var result = _recoveryController.FindUser(userId, email, phone);

            // Assert
            Assert.False(result);
        }
```

### 9. RecoveryControllerTests: ValidateUserDetails_InvalidDetails_ReturnsFalse

**Purpose:** Verify that the `ValidateUserDetails` method returns `false` when provided with invalid user details.

**Expected Result:** The method returns `false`.

**Test Steps:**

1. Initialize `RecoveryFakeDatabase` and `RecoveryController`.
2. Set up `RecoveryFakeDatabase` to validate email and phone as invalid.
3. Call the `ValidateUserDetails` method with invalid user details.
4. Assert that the method returns `false`.

```csharp
        [Fact]
        public void ValidateUserDetails_InvalidDetails_ReturnsFalse()
        {
            // Arrange
            string userId = "LMC00001";
            string email = "invalid email";
            string phone = "12345";

            _RecoveryFakeDatabase.SetValidations(true, false, false);

            // Act
            var result = _recoveryController.ValidateUserDetails(userId, email, phone);

            // Assert
            Assert.False(result);
        }
```

#### Fake FakeDatabase for RecoveryController

```csharp
    public partial class RecoveryFakeDatabase : Database
    {
        private bool _validUsername;
        private bool _validEmail;
        private bool _validPhoneNumber;
        DataTable _dataTable;

        public bool PasswordChanged { get; private set; }

        public void SetValidations(bool validUsername, bool validEmail, bool validPhoneNumber)
        {
            _validUsername = validUsername;
            _validEmail = validEmail;
            _validPhoneNumber = validPhoneNumber;
        }

        public void SetFindUserDataTable(string userId, string email, string phone)
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("customerID");
            _dataTable.Columns.Add("phoneNumber");
            _dataTable.Columns.Add("emailAddress");
            _dataTable.Rows.Add(userId, phone, email);
        }

        public void SetCityDataTable()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("city");
            _dataTable.Rows.Add("City1");
            _dataTable.Rows.Add("City2");
        }

        public void SetProvinceDataTable()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("province");
            _dataTable.Rows.Add("Province1");
            _dataTable.Rows.Add("Province2");
        }

        public void SetEmailCheckDataTable()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("emailAddress");
        }

        public void SetPhoneCheckDataTable()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("phoneNumber");
        }
    }
```

#### Fake FakeAccountController

```csharp
    public class FakeAccountController : AccountController
    {
        private string _uid;

        public void SetUid(string uid)
        {
            _uid = uid;
        }

        public override string GetUid()
        {
            return _uid;
        }
    }
```

This comprehensive test plan ensures that the critical functionalities of the controllers are tested thoroughly to handle various edge cases and ensure reliability.
