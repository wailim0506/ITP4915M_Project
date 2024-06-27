using System.Data;
using Xunit;
using controller;
using controller.Utilities;

namespace TestController
{
    public class RecoveryControllerTests
    {
        private readonly RecoveryFakeDatabase _RecoveryFakeDatabase;
        private readonly FakeAccountController _fakeAccountController;
        private readonly RecoveryController _recoveryController;

        public RecoveryControllerTests()
        {
            _RecoveryFakeDatabase = new RecoveryFakeDatabase();
            _fakeAccountController = new FakeAccountController();
            _recoveryController = new RecoveryController(_fakeAccountController, _RecoveryFakeDatabase);
        }

        
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
    }

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
}
