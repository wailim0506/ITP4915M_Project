using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;      //must include in every controller file
using MySqlConnector;  //must include in every controller file  

namespace controller
{
    public class SparePartListController : abstractController //extends abstractController class
    {

        public SparePartListController() { }

        public List<string> getName(string category)
        {

            List<string> name = new List<string>(); ;
            string sqlCmd ="";
            if (category == "A")
            {
                sqlCmd = "SELECT name FROM spare_part WHERE categoryID = 'A'";
            }
            else if (category == "B")
            {
                sqlCmd = "SELECT name FROM spare_part WHERE categoryID = 'B'";
            }
            else if (category == "C")
            {
                sqlCmd = "SELECT name FROM spare_part WHERE categoryID = 'C'";
            }
            else
            {
                sqlCmd = "SELECT name FROM spare_part WHERE categoryID = 'D'";
            }


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
                            name.Add(reader.GetString(0));
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

        public List<string> getNum(string category)
        {

            List<string> num = new List<string>(); ;
            string sqlCmd = "";
            if (category == "A")
            {
                sqlCmd = "SELECT partNumber FROM spare_part WHERE categoryID = 'A'";
            }
            else if (category == "B")
            {
                sqlCmd = "SELECT partNumber FROM spare_part WHERE categoryID = 'B'";
            }
            else if (category == "C")
            {
                sqlCmd = "SELECT partNumber FROM spare_part WHERE categoryID = 'C'";
            }
            else
            {
                sqlCmd = "SELECT partNumber FROM spare_part WHERE categoryID = 'D'";
            }


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
                            num.Add(reader.GetString(0));
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
            return num;
        }
    }
}
