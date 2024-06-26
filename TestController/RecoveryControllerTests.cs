using controller;
using Xunit;

namespace TestController
{
    public class RecoveryControllerTests
    {
        [Fact]
        public void FindUser_ValidUser_ReturnsTrue()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.FindUser("LMC00001", "peter.zhang@abc.com", "13012345678");

            Assert.True(result);
        }

        [Fact]
        public void FindUser_InvalidUser_ReturnsFalse()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.FindUser("LMC10001", "peter.zha123ng@abc.com", "13112345678");

            Assert.False(result);
        }

        [Fact]
        public void CheckEmailPhone_UniqueEmailPhone_ReturnsTrue()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.CheckEmailPhone("sdrfdsf@abc.com");

            Assert.True(result);
        }

        [Fact]
        public void CheckEmailPhone_NonUniqueEmailPhone_ReturnsFalse()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.CheckEmailPhone("peter.zhang@abc.com");

            Assert.False(result);
        }
        [Fact]
        public void FindUser_WithNullInputs_ReturnsFalse()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.FindUser(null, null, null);
            Assert.False(result);
        }

        [Fact]
        public void FindUser_WithInvalidUserId_ReturnsFalse()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.FindUser("InvalidUserId", "peter.zhang@abc.com", "13012345678");
            Assert.False(result);
        }

        [Fact]
        public void CheckEmailPhone_WithNullInput_ReturnsFalse()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.CheckEmailPhone(null);
            Assert.False(result);
        }

        [Fact]
        public void CheckEmailPhone_WithInvalidEmail_ReturnsFalse()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.CheckEmailPhone("invalidEmail");
            Assert.False(result);
        }

        [Fact]
        public void ValidateUserDetails_WithInvalidInputs_ReturnsFalse()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.ValidateUserDetails("InvalidUserId", "invalidEmail", "invalidPhone");
            Assert.False(result);
        }

        [Fact]
        public void ValidateUserDetails_WithNullInputs_ReturnsFalse()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.ValidateUserDetails(null, null, null);
            Assert.False(result);
        }
    }
}