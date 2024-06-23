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
            var result = recoveryController.FindUser("LMC12345", "test@example.com", "1234567890");

            Assert.True(result);
        }

        [Fact]
        public void FindUser_InvalidUser_ReturnsFalse()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.FindUser("LMC12345", "wrongemail@example.com", "1234567890");

            Assert.False(result);
        }

        [Fact]
        public void CheckEmailPhone_UniqueEmailPhone_ReturnsTrue()
        {
            var recoveryController = new RecoveryController();
            var result = recoveryController.CheckEmailPhone("uniqueemail@example.com");

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