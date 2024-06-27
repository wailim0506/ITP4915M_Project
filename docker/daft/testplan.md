### 17 Test Cases

#### 17.1 DelAccount_DisablesAccount

**Purpose:** Verify that when a user deletes their account, the account status is set to disabled.  
**Expected Result:** The account is successfully disabled, and the method returns true.  

**Test Steps:**

1. Initialize `FakeDatabaseAccount` and `AccountController`.
2. Set user data in `FakeDatabaseAccount`.
3. Log in the user.
4. Call the `DelAccount` method.
5. Assert that the method returns true and the account is disabled.

**Example Test Case Implementation:**

```csharp
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
```

#### 17.2 Login_AccountInactive_ReturnsFalse

**Purpose:** Verify that the login fails if the account is inactive.  
**Expected Result:** The login attempt fails, and the method returns false.  

**Test Steps:**

1. Initialize `FakeDatabaseAccount` and `AccountController`.
2. Set account data in `FakeDatabaseAccount` with status as inactive.
3. Log in with valid credentials.
4. Assert that the method returns false and the user is not logged in.

**Example Test Case Implementation:**

```csharp
[Fact]
public void Login_AccountInactive_ReturnsFalse()
{
    // Arrange
    string userId = "LMC00001";
    string password = "password";
    _fakeDatabaseAccount.SetAccountData(userId, password, "inactive");
    // Act
    var result = _accountController.Login(userId, password, _fakeUIController);
    // Assert
    Assert.False(result);
    Assert.False(_accountController.IsLogin);
}
```

#### 17.3 Login_AccountNotFound_ReturnsFalse

**Purpose:** Verify that the login fails if the account is not found.  
**Expected Result:** The login attempt fails, and the method returns false.  

**Test Steps:**

1. Initialize `FakeDatabaseAccount` and `AccountController`.
2. Attempt to log in with credentials that do not match any account.
3. Assert that the method returns false and the user is not logged in.

**Example Test Case Implementation:**

```csharp
[Fact]
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

#### 17.4 Login_InvalidCredentials_ReturnsFalse

**Purpose:** Verify that the login fails if the credentials are invalid.  
**Expected Result:** The login attempt fails, and the method returns false.  

**Test Steps:**

1. Initialize `FakeDatabaseAccount` and `AccountController`.
2. Set account data in `FakeDatabaseAccount` with valid user ID but different password.
3. Attempt to log in with the wrong password.
4. Assert that the method returns false and the user is not logged in.

**Example Test Case Implementation:**

```csharp
[Fact]
public void Login_InvalidCredentials_ReturnsFalse()
{
    // Arrange
    string userId = "LMC00001";
    string password = "invalidPassword";
    _fakeDatabaseAccount.SetAccountData(userId, "storedPassword", "active");
    // Act
    var result = _accountController.Login(userId, password, _fakeUIController);
    // Assert
    Assert.False(result);
    Assert.False(_accountController.IsLogin);
}
```

#### 17.5 IsFavourite_InvalidData_ReturnsFalse

**Purpose:** Verify that the `IsFavourite` method returns false when given invalid data.  
**Expected Result:** The method returns false.  

**Test Steps:**

1. Initialize `FavouriteFakeDatabase` and `FavouriteController`.
2. Set up `FavouriteFakeDatabase` with invalid data.
3. Call the `IsFavourite` method with part number and customer ID that do not match.
4. Assert that the method returns false.

**Example Test Case Implementation:**

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

#### 17.6 CheckEmail_UniqueEmail_ReturnsTrue

**Purpose:** Verify that the `CheckEmail` method returns true for a unique email.  
**Expected Result:** The method returns true.  

**Test Steps:**

1. Initialize `RecoveryFakeDatabase` and `RecoveryController`.
2. Set up `RecoveryFakeDatabase` with no matching email data.
3. Call the `CheckEmail` method with a unique email.
4. Assert that the method returns true.

**Example Test Case Implementation:**

```csharp
[Fact]
public void CheckEmail_UniqueEmail_ReturnsTrue()
{
    // Arrange
    string email = "<unique@example.com>";
    _recoveryFakeDatabase.SetEmailCheckDataTable();
    // Act
    var result = _recoveryController.CheckEmail(email);
    // Assert
    Assert.True(result);
}
```

#### 17.7 CheckPhone_UniquePhone_ReturnsTrue

**Purpose:** Verify that the `CheckPhone` method returns true for a unique phone number.  
**Expected Result:** The method returns true.  

**Test Steps:**

1. Initialize `RecoveryFakeDatabase` and `RecoveryController`.
2. Set up `RecoveryFakeDatabase` with no matching phone number data.
3. Call the `CheckPhone` method with a unique phone number.
4. Assert that the method returns true.

**Example Test Case Implementation:**

```csharp
[Fact]
public void CheckPhone_UniquePhone_ReturnsTrue()
{
    // Arrange
    string phone = "1234567890";
    _recoveryFakeDatabase.SetPhoneCheckDataTable();
    // Act
    var result = _recoveryController.CheckPhone(phone);
    // Assert
    Assert.True(result);
}
```

#### 17.8 FindUser_InvalidUserDetails_ReturnsFalse

**Purpose:** Verify that the `FindUser` method returns false when provided with invalid user details.  
**Expected Result:** The method returns false.  

**Test Steps:**

1. Initialize `RecoveryFakeDatabase` and `RecoveryController`.
2. Set up `RecoveryFakeDatabase` with no matching user details.
3. Call the `FindUser` method with invalid user details.
4. Assert that the method returns false.

**Example Test Case Implementation:**

```csharp
[Fact]
public void FindUser_InvalidUserDetails_ReturnsFalse()
{
    // Arrange
    string userId = "invalidId";
    string email = "invalidEmail";
    string phone = "invalidPhone";
    _recoveryFakeDatabase.SetValidations(false, false, false);
    // Act
    var result = _recoveryController.FindUser(userId, email, phone);
    // Assert
    Assert.False(result);
}
```

#### 17.9 ValidateUserDetails_InvalidDetails_ReturnsFalse

**Purpose:** Verify that the `ValidateUserDetails` method returns false when provided with invalid user details.  
**Expected Result:** The method returns false.  

**Test Steps:**

1. Initialize `RecoveryFakeDatabase` and `RecoveryController`.
2. Set up `RecoveryFakeDatabase` to validate email and phone as invalid.
3. Call the `ValidateUserDetails` method with invalid user details.
4. Assert that the method returns false.

**Example Test Case Implementation:**

```csharp
[Fact]
public void ValidateUserDetails_InvalidDetails_ReturnsFalse()
{
    // Arrange
    string userId = "LMC00001";
    string email = "invalid email";
    string phone = "12345";
    _recoveryFakeDatabase.SetValidations(true, false, false);
    // Act
    var result = _recoveryController.ValidateUserDetails(userId, email, phone);
    // Assert
    Assert.False(result);
}
```

By following the structured format provided above, critical functionalities of the controllers will be thoroughly tested, ensuring they can handle various edge cases and maintain reliability.
