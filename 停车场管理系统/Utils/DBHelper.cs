using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ParkingManagement.Utils
{
    /// <summary>
    /// 数据库操作帮助类
    /// </summary>
    public static class DBHelper
    {
        // 从配置文件读取连接字符串
        private static string connectionString = ConfigurationManager.ConnectionStrings["ParkingDB"].ConnectionString;

        /// <summary>
        /// 执行查询，返回DataTable
        /// </summary>
        public static DataTable ExecuteQuery(string sql, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // 添加参数
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("数据库查询失败：" + ex.Message);
            }

            return dt;
        }

        /// <summary>
        /// 执行增删改，返回受影响的行数
        /// </summary>
        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // 添加参数
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("数据库操作失败：" + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 执行查询，返回单个值
        /// </summary>
        public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            object result = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // 添加参数
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        result = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("数据库查询失败：" + ex.Message);
            }

            return result;
        }
    }
}
