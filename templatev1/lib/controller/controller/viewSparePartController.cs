using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;      //must include in every controller file
using MySqlConnector;  //must include in every controller file 


 namespace controller
{
    public class viewSparePartController : abstractController
    {

        public viewSparePartController() { }  

        public string getDescription(string sp) //sp = spare part
        {
            string description = "";
            string sqlCmd = $"SELECT description FROM product WHERE partNumber = \'{sp}\'";

            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd, conn))
                {
                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column and add it to the list
                            description = reader.GetString(0); 
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }
            return description;
        }
        public string getItemID(string sp) //sp = spare part
        {
            string id = "";
            string sqlCmd = $"SELECT itemID FROM product WHERE partNumber = \'{sp}\'";

            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd, conn))
                {
                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column and add it to the list
                            id = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }
            return id;
        }

        public string getPartNum(string sp)
        {
            string id = "";
            string sqlCmd = $"SELECT partNumber FROM product WHERE partNumber = \'{sp}\'";

            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd, conn))
                {
                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column and add it to the list
                            id = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }
            return id;
        }

        public string getCategory(string sp)
        {
            string category = "";
            string sqlCmd1 = $"SELECT category FROM product WHERE partNumber = \'{sp}\'"; //get the category (A,B,C,D) 
            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd1, conn))
                {
                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column and add it to the list
                            category = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }

            string categoryName = "";
            string sqlCmd2 = $"SELECT type FROM category WHERE categoryID = \'{category}\'"; //get the category name base on category got from sqlCmd1
            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd2, conn))
                {
                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column and add it to the list
                            categoryName = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }

            return $"{category} - {categoryName}";
        }

        public string getName(string sp) 
        {
            string name = "";
            string sqlCmd = $"SELECT name FROM spare_part WHERE partNumber = \'{sp}\'";

            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd, conn))
                {
                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column and add it to the list
                            name = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }
            return name;
        }

        public string getSupplier(string sp)
        {
            string supplierID = "";
            string sqlCmd1 = $"SELECT supplierID FROM spare_part WHERE partNumber = \'{sp}\'"; //get supplierID first

            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd1, conn))
                {
                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column and add it to the list
                            supplierID = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }

            string supplierName = "";
            string sqlCmd2 = $"SELECT name FROM supplier WHERE supplierID = \'{supplierID}\'"; //get supplier name by supplierID
            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd2, conn))
                {
                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column and add it to the list
                            supplierName = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }

            return supplierName;
        }

        public string getCountry(string sp) {
            string supplierID = "";
            string sqlCmd1 = $"SELECT supplierID FROM spare_part WHERE partNumber = \'{sp}\'"; //get supplierID first

            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd1, conn))
                {
                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column and add it to the list
                            supplierID = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }

            string country = "";
            string sqlCmd2 = $"SELECT country FROM supplier WHERE supplierID = \'{supplierID}\'"; //get country name by supplierID

            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd2, conn))
                {
                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column and add it to the list
                            country = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }
            return country;
        }

        public string getPrice(string sp)
        {
            int price = 0;
            string sqlCmd = $"SELECT price FROM product WHERE partNumber = \'{sp}\'";

            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd, conn))
                {
                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column and add it to the list
                            price = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }
            return price.ToString();

        }

        public string getOnSalesQty(string sp)
        {
            int qty = 0;
            string sqlCmd = $"SELECT onSaleQty FROM product WHERE partNumber = \'{sp}\'";

            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd, conn))
                {
                    // Execute the query and get the result
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column and add it to the list
                            qty = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }
            return qty.ToString();
        }
    }
}
