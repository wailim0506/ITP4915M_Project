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
                ? "SELECT S.staffID, firstName, lastName, createDate, status, name AS Department, jobTitle " +
                  "FROM staff S, staff_account SA, department D WHERE S.staffID = SA.staffID AND S.deptID = D.deptID ORDER BY staffID"
                : "SELECT C.customerID, firstName, lastName, createDate, status, isLM as LM_Account FROM customer C, customer_account CA WHERE C.customerID = CA.customerID ORDER BY customerID";
        }

        private string GetUserListQuery(string type, string dept)
        {
            return $"SELECT S.staffID, firstName, lastName, createDate, status, jobTitle, name " +
                   $"FROM staff S, staff_account SA, department D WHERE S.staffID = SA.staffID AND S.deptID = D.deptID AND name = \'{dept}\'ORDER BY staffID";
        }

        public int getLMSID()
        {
            string SqlQuery = "SELECT * FROM staff";
            dt = ExecuteSqlQuery(SqlQuery);
            return dt.Rows.Count + 1;
        }


        //Check whether the email or phone has registered an account.
        public bool CheckEmailPhone(string data)
        {
            sqlstr =
                $"SELECT emailAddress, phoneNumber FROM customer C, customer_account CA WHERE Status = 'active' AND c.customerID = CA.customerID AND (phoneNumber = \'{data}\' OR emailAddress = \'{data}\') " +
                $"UNION ALL SELECT emailAddress, phoneNumber FROM staff S, staff_account SA WHERE status = 'active' AND s.staffID = sa.staffID AND(phoneNumber = \'{data}\' OR emailAddress = \'{data}\');";
            dt = ExecuteSqlQuery(sqlstr);
            return dt.Rows.Count < 1;
        }

        public List<string> GetJob(string dept)
        {
            sqlstr = $"SELECT jobtitle FROM jobTitle WHERE department = \'{dept}\'";
            DataTable dataTable = db.ExecuteDataTable(sqlstr);
            return dataTable.AsEnumerable().Select(row => row["jobTitle"].ToString()).ToList();
        }

        private DataTable ExecuteSqlQuery(string sqlQuery)
        {
            return db.ExecuteDataTable(sqlQuery);
        }

        private int GetDeliveryManID()
        {
            sqlstr = "SELECT * FROM deliverman";
            dt = db.ExecuteDataTable(sqlstr);
            return dt.Rows.Count + 1;
        }

        private string GetPermission(string job)
        {
            sqlstr = $"SELECT permissionID FROM jobtitle WHERE jobTitle = \'{job}\'";
            dt = db.ExecuteDataTable(sqlstr);
            return dt.Rows[0]["permissionID"].ToString();
        }


        //For create a new staff accounr.
        public bool create(dynamic Userinfo)
        {
            string hashedPwd = HashPassword(Userinfo.pwd);
            string lmsid = "LMS" + getLMSID().ToString("D5");
            string accountId = "SA" + getLMSID().ToString("D5");
            string dept = Userinfo.dept;
            string permission = GetPermission(Userinfo.jobTitle);
            string LMDID = "LMD" + GetDeliveryManID().ToString("D3");

            var staffParams = new Dictionary<string, object>
            {
                { "@id", lmsid },
                { "@fName", Userinfo.fName },
                { "@lName", Userinfo.lName },
                { "@gender", Userinfo.gender },
                { "@email", Userinfo.email },
                { "@phone", Userinfo.phone },
                { "@img", Userinfo.IMG },
                { "@dob", Userinfo.dateOfBirth },
                { "@dept", Userinfo.dept },
                { "@job", Userinfo.jobTitle }
            };

            var accountParams = new Dictionary<string, object>
            {
                { "@accountId", accountId },
                { "@id", lmsid },
                { "@hashedPwd", hashedPwd },
                { "@joinDate", Userinfo.joinDate },
                { "@dept", Userinfo.dept },
                { "@permission", permission }
            };


            if (dept.Equals("LMD03"))
            {
                staffParams.Add("@LMDID", ("LMD" + GetDeliveryManID().ToString("D3")));
            }
            else
                staffParams.Add("@LMDID", DBNull.Value);


            try
            {
                if (dept.Equals("LMD03"))
                {
                    db.ExecuteNonQueryCommand(
                        "INSERT INTO deliverman VALUES(@LMDID, @id)", staffParams);
                }

                db.ExecuteNonQueryCommand(
                    "INSERT INTO staff VALUES(@id, @dept, @fName, @lName, @gender, @email, @phone, @dob, @job, NULL, NULL)",
                    staffParams);
                db.ExecuteNonQueryCommand(
                    "INSERT INTO staff_account VALUES(@accountId, @id, 'active', @hashedPwd, @joinDate, @joinDate)",
                    accountParams);
                db.ExecuteNonQueryCommand(
                    "INSERT INTO staff_account_permission VALUES(@accountId, @permission)", accountParams);
                return true;
            }
            catch (Exception)
            {
                db.ExecuteNonQueryCommand("DELETE FROM staff WHERE staffID = @id", accountParams);
                db.ExecuteNonQueryCommand("DELETE FROM staff_account WHERE staffID = @id", accountParams);
                db.ExecuteNonQueryCommand("DELETE FROM staff_account_permission WHERE staffID = @id", accountParams);
                db.ExecuteNonQueryCommand("DELETE FROM deliverman WHERE staffID = @id", accountParams);
                return false;
            }
        }

        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}