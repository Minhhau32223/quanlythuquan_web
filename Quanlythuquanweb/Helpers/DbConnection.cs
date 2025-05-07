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
        private Random random = new Random();

        // Sinh số ngẫu nhiên từ min đến max
        public int GenerateRandomNumber(int min = 100000, int max = 999999)
        {
            return random.Next(min, max);
        }
        public int GenerateUniqueNumber(string tableName, string columnName)
        {
            DbConnection db = new DbConnection(); // Tạo mới trong hàm luôn, tránh null
            int number;
            bool isExist;

            do
            {
                number = GenerateRandomNumber();
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = {number}";
                DataTable dt = db.ExecuteQuery(query);

                isExist = dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0;
            }
            while (isExist);

            return number;
        }
    }
}
