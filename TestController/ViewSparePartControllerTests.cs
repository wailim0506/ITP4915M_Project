using NUnit.Framework;
using controller;
using controller.Utilities;
using System.Data;
using Moq;
using System.Collections.Generic;

namespace Tests
{
    public class ViewSparePartControllerTests
    {
        private Mock<IDatabase> _mockDatabase;
        private viewSparePartController _controller;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new Mock<IDatabase>();
            _controller = new viewSparePartController((Database)_mockDatabase.Object);
        }

        [Test]
        public void GetInfo_ReturnsExpectedData_WhenPartNumberIsProvided()
        {
            var expectedDataTable = new DataTable();
            _mockDatabase.Setup(db => db.ExecuteDataTable(It.IsAny<string>(), null)).Returns(expectedDataTable);

            var result = _controller.GetInfo("A00001");

            Assert.AreEqual(expectedDataTable, result);
        }

        [Test]
        public void IsFavourite_ReturnsTrue_WhenPartIsFavourite()
        {
            var expectedDataTable = new DataTable();
            _mockDatabase.Setup(db => db.ExecuteDataTable(It.IsAny<string>(), null)).Returns(expectedDataTable);

            var result = _controller.IsFavourite("A00001", "LMC0001");

            Assert.AreEqual(expectedDataTable, result);
        }

        [Test]
        public void AddToFavourite_ReturnsTrue_WhenPartIsAddedToFavourite()
        {
            var expectedDataTable = new DataTable();
            _mockDatabase.Setup(db => db.ExecuteDataTable(It.IsAny<string>(), null)).Returns(expectedDataTable);

            var result = _controller.AddToFavourite("A00001", "LMC0001");

            Assert.AreEqual(expectedDataTable, result);
        }

        [Test]
        public void RemoveFavourite_ReturnsTrue_WhenPartIsRemovedFromFavourite()
        {
            var expectedDataTable = new DataTable();
            _mockDatabase.Setup(db => db.ExecuteDataTable(It.IsAny<string>(), null)).Returns(expectedDataTable);

            var result = _controller.RemoveFavourite("A00001", "LMC0001");

            Assert.AreEqual(expectedDataTable, result);
        }

        [Test]
        public void AddToCart_ReturnsTrue_WhenPartIsAddedToCart()
        {
            var expectedDataTable = new DataTable();
            _mockDatabase.Setup(db => db.ExecuteDataTable(It.IsAny<string>(), null)).Returns(expectedDataTable);

            var result = _controller.AddToCart("LMSC0001", "A00001", 1, false);

            Assert.AreEqual(expectedDataTable, result);
        }

        [Test]
        public void DeductQtyInDb_ReturnsTrue_WhenQuantityIsDeducted()
        {
            // var expectedDataTable = new DataTable();
            // _mockDatabase.Setup(db => db.ExecuteDataTable(It.IsAny<string>(), null)).Returns(expectedDataTable);
            //
            // var result = _controller.DeductQtyInDb( 
            //
            // Assert.AreEqual(expectedDataTable, result);
        }
    }
}