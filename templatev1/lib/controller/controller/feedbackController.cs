using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;      //must include in every controller file
using MySqlConnector;  //must include in every controller file 

namespace controller
{
    public class feedbackController : abstractController
    {
        public feedbackController() { }

        public int countFeedback()
        { //count how many feedback already in database
            int count = 0;
            string sqlCmd = "SELECT COUNT(*) FROM feedback";
            try
            {
                // Open the connection
                conn.Open();

                // Create the MySqlCommand object
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read all rows from the result set
                        while (reader.Read())
                        {
                            // Get the value from the first column at each row
                            count = reader.GetInt32(0);
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

            return count;
        }

        public Boolean addFeedback(string custID, string feedback)
        {
            //Data to be inserted
            string feedbackID = feedBackIDGenerator();
            string customerID = custID;
            string content = feedback;
            string feedbackDate = DateTime.Now.ToString(); //convert date time to string 

            string sqlCmd = "INSERT INTO feedback (feedbackID, customerID, content, feedbackDate) VALUES (@feedbackID, @customerID, @content, @feedbackDate)";

            try
            {
                // Create a new MySqlConnection
                using (MySqlConnection connection = new MySqlConnection(connString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a MySqlCommand object
                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@feedbackID", feedbackID);
                        command.Parameters.AddWithValue("@customerID", customerID);
                        command.Parameters.AddWithValue("@content", content);
                        command.Parameters.AddWithValue("@feedbackDate", feedbackDate);

                        //exceute
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close(); //close connection
            }
            return true;
        }

        public string feedBackIDGenerator() {
            int count = countFeedback();
            string feedbackID = "";
            if (++count < 10)
            {
                feedbackID = "FB2400" + count;

            }
            else
            {
                feedbackID = "FB240" + count;
            }

            return feedbackID;
        }

    }
}
