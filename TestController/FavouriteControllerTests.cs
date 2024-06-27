using System;
using System.Collections.Generic;
using System.Data;
using Xunit;
using controller;
using controller.Utilities;

namespace TestController
{
    public class FavouriteControllerTests
    {
        private readonly FavouriteFakeDatabase _FavouriteFakeDatabase;
        private readonly favouriteController _favouriteController;

        public FavouriteControllerTests()
        {
            _FavouriteFakeDatabase = new FavouriteFakeDatabase();
            _favouriteController = new favouriteController(_FavouriteFakeDatabase);
        }
        
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
    }

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
}
