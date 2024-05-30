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
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sqlCmd, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {                        
                        while (reader.Read())
                        {         
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
                conn.Close(); 
            }
            return count;
        }

        public Boolean addFeedback(string custID, string feedback)
        {            
            string feedbackID = feedBackIDGenerator();
            string customerID = custID;
            string content = feedback;
            string feedbackDate = DateTime.Now.ToString("dd/MM/yyyy"); //today date 

            string sqlCmd = "INSERT INTO feedback (feedbackID, customerID, content, feedbackDate) VALUES (@feedbackID, @customerID, @content, @feedbackDate)";

            try
            {    
                using (MySqlConnection connection = new MySqlConnection(connString))
                {   
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sqlCmd, connection))
                    {
                        
                        command.Parameters.AddWithValue("@feedbackID", feedbackID);
                        command.Parameters.AddWithValue("@customerID", customerID);
                        command.Parameters.AddWithValue("@content", content);
                        command.Parameters.AddWithValue("@feedbackDate", feedbackDate);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close(); 
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
