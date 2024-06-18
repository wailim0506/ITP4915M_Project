using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using controller.Utilities;

namespace controller
{
    public class UserController : abstractController
    {
        public bool IsLogin;
        private readonly Database db;
        private string sqlstr;
        private DataTable dt;

        public UserController(Database db = null)
        {
            this.db = db ?? new Database();
        }

        public List<string> GetDept()
        {
            sqlstr = "SELECT name FROM department";
            dt = ExecuteSqlQuery(sqlstr);
            return dt.AsEnumerable().Select(row => row["name"].ToString()).ToList();
        }

        public void StatusAcc(string type, string Uid, int status)
        {
            try
            {
                db.ExecuteNonQueryCommand(GetAccTypeQuery(type, Uid, status), null);
                MessageBox.Show("Operation successful!", "System message", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("System Error! Please Contact The Help Desk.", "System error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
        }

        private string GetAccTypeQuery(string type, string Uid, int status)
        {
            string operation = status == 0
                ? operation = "active"
                : operation = "disable";

            return type.Equals("Staff")
                ? $"UPDATE staff_account SET status = \'{operation}\' WHERE staffID = \'{Uid}\'"
                : $"UPDATE customer_account SET status = \'{operation}\' WHERE customerID = \'{Uid}\'";
        }

        public DataTable GetUser(String UID)
        {
            return ExecuteSqlQuery(GetUserQuery(UID));
        }

        private string GetUserQuery(string UID)
        {
            return UID.StartsWith("LMS")
                ? $"SELECT S.staffID, firstName, lastName, createDate, status, jobTitle, name " +
                $"FROM staff S, staff_account SA, department D WHERE S.staffID = \'{UID}\' AND SA.staffID = \'{UID}\' AND S.deptID = D.deptID ORDER BY staffID"
                : $"SELECT C.customerID, firstName, lastName, createDate, status, isLM " +
                $"FROM customer C, customer_account CA WHERE C.customerID = \'{UID}\' AND CA.customerID = \'{UID}\' ORDER BY customerID";
        }

        public DataTable GetUserList(String type)
        {
            return ExecuteSqlQuery(GetUserListQuery(type));
        }

        public DataTable GetUserList(String type, string dept)
        {
            return ExecuteSqlQuery(GetUserListQuery(type, dept));
        }


        public string GetTotalUser(String type)
        {
            dt = ExecuteSqlQuery(GetUserListQuery(type));
            return dt.Rows.Count.ToString();

        }

        private string GetUserListQuery(string type)
        {
            return type.Equals("Staff")
                ? "SELECT S.staffID, firstName, lastName, createDate, status, jobTitle, name " +
                "FROM staff S, staff_account SA, department D WHERE S.staffID = SA.staffID AND S.deptID = D.deptID ORDER BY staffID"
                : "SELECT C.customerID, firstName, lastName, createDate, status, isLM " +
                "FROM customer C, customer_account CA WHERE C.customerID = CA.customerID ORDER BY customerID";
        }

        private string GetUserListQuery(string type, string dept)
        {
            return $"SELECT S.staffID, firstName, lastName, createDate, status, jobTitle, name " +
                $"FROM staff S, staff_account SA, department D WHERE S.staffID = SA.staffID AND S.deptID = D.deptID AND name = \'{dept}\'ORDER BY staffID";
        }

        public int getLMSID()
        {
            DataTable dt = new DataTable();
            string SqlQuery = "SELECT * FROM staff";
            dt = ExecuteSqlQuery(SqlQuery);
            return dt.Rows.Count + 1;
        }





        private DataTable ExecuteSqlQuery(string sqlQuery)
        {
            return db.ExecuteDataTable(sqlQuery);
        }
    }
}