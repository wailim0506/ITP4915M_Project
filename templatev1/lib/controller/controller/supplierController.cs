using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;      //must include in every controller file
using MySqlConnector;  //must include in every controller file 

namespace controller
{
    public class supplierController : abstractController 
    {
        string sqlCmd;
        public supplierController()
        {
            sqlCmd = "";
        }

        public DataTable getSupplierList()
        {
            DataTable dt = new DataTable();
            sqlCmd = "SELECT * FROM supplier";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt;
        }

        public int countSupplier()
        {
            DataTable dt = new DataTable();
            sqlCmd = "SELECT COUNT(*) FROM supplier";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public Boolean deleteSupplier(string id) {  //id = supplier id
            sqlCmd = $"DELETE FROM supplier WHERE supplierID = \'{id}\'";

            MySqlCommand command = new MySqlCommand(sqlCmd, conn);

            conn.Open();
            try
            {
                int rowsDel = command.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e) {
                return false;
            }
            return true;
        }

        public string getSupplierName(string id) {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT name FROM supplier WHERE supplierID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public string getSupplierCountry(string id)
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT country FROM supplier WHERE supplierID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public string getSupplierPhone(string id)
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT phone FROM supplier WHERE supplierID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public string getSupplierAddress(string id)
        {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT address FROM supplier WHERE supplierID = \'{id}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public Boolean updateSupplier(string id, string name, string phone, string address)
        {
            string sqlCmd = "UPDATE supplier SET name = @name, phone = @phone, address = @address WHERE supplierID = @id";

            MySqlCommand command = new MySqlCommand(sqlCmd, conn);

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@id", id);

            conn.Open();
            try
            {
                int rowsChange = command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public int getSupplierNumFromSameCountry(string country) {
            DataTable dt = new DataTable();
            sqlCmd = $"SELECT COUNT(*) FROM supplier WHERE country = \'{country}\'";
            adr = new MySqlDataAdapter(sqlCmd, conn);
            adr.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public Boolean addSupplier(string id, string name, string phone, string address, string country) {
            string sqlCmd = "INSERT INTO supplier VALUES(@id, @name, @phone, @address, @country)";

            MySqlCommand command = new MySqlCommand(sqlCmd, conn);

            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@country", country);

            conn.Open();
            try
            {
                int rowsAdd = command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                return false;
            }
           
            return true;
        }
    }
}
