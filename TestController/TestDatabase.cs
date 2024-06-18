using System;
using System.Collections.Generic;
using System.Data;
using controller;
using Moq;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace TestController
{
    public class TestDatabase
    {
        [TestFixture]
        public class DatabaseTests
        {
            [Test]
            public void ExecuteScalarCommand_WithValidQuery_ReturnsExpectedResult()
            {
                // Setup
                var database = new Database();
                var query = "SELECT firstName FROM customer WHERE customerID = @id";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@id", "LMC00001"}
                };

                // Act
                var result = database.ExecuteScalarCommand(query, parameters);
                var expectedResult = "Peter";
                
                // Assert
                Assert.AreEqual(expectedResult, result);
            }

            [Test]
            public void ExecuteNonQueryCommand_WithValidQuery_DoesNotThrowException()
            {
                // Setup
                var database = new Database();
                var query = "UPDATE customer SET firstName = '@firstName' WHERE customerID = @id";
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@firstName", "John"},
                    {"@id", "LMC00004"}
                };
                // Act & Assert
                Assert.DoesNotThrow(() => database.ExecuteNonQueryCommand(query, parameters));
            }

            [Test]
            public void ExecuteReaderCommand_WithValidQuery_ReturnsDataReader()
            {
                // Setup
                var database = new Database();
                var query = "SELECT * FROM customer";

                // Act
                Dictionary<string, object> parameters = new Dictionary<string, object> { { "Id", "LMC00004" } };
                var result = database.ExecuteReaderCommand(query, parameters);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.HasRows);
            }

            [Test]
            public void ExecuteDataTable_WithValidQuery_ReturnsPopulatedDataTable()
            {
                // Setup
                var database = new Database();
                var query = "SELECT * FROM customer";

                // Act
                var result = database.ExecuteDataTable(query);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Rows.Count > 0);
            }
        }
    }
}