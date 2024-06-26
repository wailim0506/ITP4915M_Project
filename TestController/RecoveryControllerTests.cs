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
            var result = recoveryController.CheckEmailPhone("peter.zha123ng@abc.com");

            Assert.True(result);
        }

        [Fact]
        public void CheckEmailPhone_NonUniqueEmailPhone_ReturnsFalse()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.CheckEmailPhone("existingemail@example.com");

            Assert.False(result);
        }
    }
}