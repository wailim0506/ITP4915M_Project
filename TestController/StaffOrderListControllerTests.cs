using System;
using NUnit.Framework;
using System.Data;
using controller;
using controller.Utilities;
using Moq;

namespace TestController
{
    [TestFixture]
    public class StaffOrderListControllerTests
    {
        private Mock<IDatabase> _mockDatabase;
        private staffOrderListController _controller;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new Mock<IDatabase>();
            _controller = new staffOrderListController();
        }

        [Test]
        public void GetOrder_ReturnsExpectedData_WhenStatusIsAllAndIsManagerIsTrue()
        {
            // Arrange
            var expectedDataTable = new DataTable();
            _mockDatabase.Setup(db => db.ExecuteDataTable(It.IsAny<string>(), null)).Returns(expectedDataTable);

            // Act
            var result = _controller.getOrder("SA00001", "All", "orderID", true);

            // Assert
            Assert.AreEqual(expectedDataTable, result);
        }

        [Test]
        public void GetOrder_ReturnsExpectedData_WhenStatusIsNotAllAndIsManagerIsFalse()
        {
            // Arrange
            var expectedDataTable = new DataTable();
            _mockDatabase.Setup(db => db.ExecuteDataTable(It.IsAny<string>(), null)).Returns(expectedDataTable);

            // Act
            var result = _controller.getOrder("SA00001", "Pending", "orderID", false);

            // Assert
            Assert.AreEqual(expectedDataTable, result);
        }

        [Test]
        public void GetOrder_ReturnsExpectedData_WhenSortByEndsWithDesc()
        {
            // Arrange
            var expectedDataTable = new DataTable();
            _mockDatabase.Setup(db => db.ExecuteDataTable(It.IsAny<string>(), null)).Returns(expectedDataTable);

            // Act
            var result = _controller.getOrder("SA00001", "All", "orderID DESC", true);

            // Assert
            Assert.AreEqual(expectedDataTable, result);
        }

        [Test]
        public void GetOrder_ThrowsException_WhenNoDataFound()
        {
            // Arrange
            _mockDatabase.Setup(db => db.ExecuteDataTable(It.IsAny<string>(), null)).Returns((DataTable)null);

            // Act & Assert
            Assert.Throws<Exception>(() => _controller.getOrder("SA00001", "All", "orderID", true));
        }
    }
}