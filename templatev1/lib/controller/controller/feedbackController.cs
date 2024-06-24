using System;
using System.Collections.Generic;
using System.Data;
using controller.Utilities;


namespace controller
{
    public class feedbackController : abstractController
    {
        private readonly Database _db;

        public feedbackController(Database database = null)
        {
            _db = database ?? new Database();
        }

        public int CountFeedback()
        {
            //count how much feedback already in the database
            int count = 0;
            try
            {
                string sqlCmd = "SELECT COUNT(*) FROM feedback";
                return int.Parse(_db.ExecuteDataTable(sqlCmd).Rows[0][0].ToString());
            }
            catch (Exception e)
            {
                // ignored
            }

            return count;
        }

        public Boolean AddFeedback(string custID, string feedback, string orderID)
        {
            string feedbackID = FeedBackIdGenerator();
            string customerID = custID;
            string content = feedback;
            string feedbackDate = DateTime.Now.ToString("dd/MM/yyyy"); //today date

            string sqlCmd = orderID != "N/A"
                ? "INSERT INTO feedback (feedbackID, customerID, orderID, content, feedbackDate) VALUES (@feedbackID, @customerID, @orderID, @content, @feedbackDate)"
                : "INSERT INTO feedback (feedbackID, customerID, content, feedbackDate) VALUES (@feedbackID, @customerID, @content, @feedbackDate)";

            var parameters = new Dictionary<string, object>
            {
                { "@feedbackID", feedbackID },
                { "@customerID", customerID },
                { "@content", content },
                { "@feedbackDate", feedbackDate }
            };

            if (orderID != "N/A")
            {
                parameters.Add("@orderID", orderID);
            }

            try
            {
                _db.ExecuteNonQueryCommand(sqlCmd, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add feedback", ex);
            }

            return true;
        }

        public string FeedBackIdGenerator()
        {
            int count = CountFeedback();
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

        public List<string> getOrderID(string id) //customer id
        {
            string sqlCmd =
                $"SELECT orderID FROM order_ x, customer_account y WHERE x.customerAccountID = y.customerAccountID AND y.customerID = '{id}' ";
            var dt = _db.ExecuteDataTable(sqlCmd);

            List<string> orderID = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                orderID.Add(dt.Rows[i][0].ToString());
            }

            return orderID;
        }

        public DataTable getAllFeedback(bool isManager, string staffID)
        {
            string sqlCmd =
                $"SELECT x.feedBackID, x.customerID, x.orderID, x.content, x.feedbackDate FROM feedback x, " +
                $"staff_account y , order_ z WHERE x.orderID = z.orderID AND " +
                $"y.staffAccountID = z.staffAccountID";



            if (!isManager)
            {
                sqlCmd += $" AND y.staffID = \'{staffID}\'";
            }
            return _db.ExecuteDataTable(sqlCmd);
        }

        public DataTable getFeedbackDetail(string feedbackID)
        {
            string sqlCmd =
                $"SELECT feedBackID, customerID, orderID, content, feedbackDate FROM feedback WHERE feedBackID = \'{feedbackID}\'";
            return _db.ExecuteDataTable(sqlCmd);
        }
    }
}