using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Doanc_sharp.src.Helpers
{
    public class DbConnection
    {
        private string connStr = "server=localhost;user=root;database=quanlythuquan;port=3306;password=;";
        private MySqlConnection conn;

        public DbConnection()
        {
               conn = new MySqlConnection(connStr); 
        }
        public MySqlConnection GetConnection()
        {
            return conn;
        }
        public DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);

            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
                public int ExecuteNonQuery(string query)
        {
            int result = 0;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                result = cmd.ExecuteNonQuery(); // trả về số dòng ảnh hưởng
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn (NonQuery): " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

      
        public object ExecuteScalar(string query)
        {
            object result = null;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                result = cmd.ExecuteScalar(); 
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn (Scalar): " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
