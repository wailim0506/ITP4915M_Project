using System;
using System.Collections.Generic;
using System.Data;
using Xunit;
using controller;
using controller.Utilities;

namespace TestController
{
    public class AccountControllerTests
    {
        private readonly FakeDatabaseAccount _FakeDatabaseAccount;
        private readonly FakeUIController _fakeUIController;
        private readonly AccountController _accountController;

        public AccountControllerTests()
        {
            _FakeDatabaseAccount = new FakeDatabaseAccount();

            _accountController = new AccountController(_FakeDatabaseAccount);
            _fakeUIController = new FakeUIController(_accountController);
        }

        
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
    }

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

    public class FakeUIController : UIController
    {
        public override void SetPermission(string userId)
        {
            // No-op for fake implementation
        }

        public FakeUIController(AccountController accountController, Database db = null) : base(accountController, db)
        {
        }
    }
}
