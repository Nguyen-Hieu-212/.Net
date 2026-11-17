using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RetailSales.DAL
{
    /// <summary>
    /// Tiện ích truy cập CSDL. MỌI câu lệnh đều truyền giá trị qua SqlParameter (chống SQL Injection).
    /// </summary>
    public static class DbHelper
    {
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SalesDB"].ConnectionString; }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static SqlParameter P(string name, SqlDbType type, object value)
        {
            return new SqlParameter(name, type) { Value = value ?? DBNull.Value };
        }

        public static SqlParameter P(string name, SqlDbType type, int size, object value)
        {
            return new SqlParameter(name, type, size) { Value = value ?? DBNull.Value };
        }

        /// <summary>Tham số tiền tệ DECIMAL(18,2) - chỉ định rõ Precision/Scale để không bị làm tròn.</summary>
        public static SqlParameter PM(string name, decimal value)
        {
            return new SqlParameter(name, SqlDbType.Decimal) { Precision = 18, Scale = 2, Value = value };
        }

        public static int ExecuteNonQuery(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>Đọc dữ liệu bằng SqlDataReader rồi ánh xạ từng dòng sang DTO.</summary>
        public static List<T> ExecuteList<T>(string sql, Func<SqlDataReader, T> map, params SqlParameter[] ps)
        {
            List<T> list = new List<T>();
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) list.Add(map(rd));
                }
            }
            return list;
        }
    }
}
