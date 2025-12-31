using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement.Utils
{
    /// <summary>
    /// 数据库连接帮助类
    /// 提供统一的数据库操作方法
    /// </summary>
    public class DBHelper
    {
        // 从配置文件读取连接字符串
        private static readonly string connectionString = 
            ConfigurationManager.ConnectionStrings["LibraryDB"].ConnectionString;

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// 执行查询，返回 DataTable
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>查询结果DataTable</returns>
        public static DataTable ExecuteQuery(string sql, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // 添加参数
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = GetFriendlyErrorMessage(ex);
                throw new Exception(errorMessage, ex);
            }

            return dt;
        }


        /// <summary>
        /// 执行增删改操作，返回受影响的行数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            int result = 0;

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        conn.Open();
                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = GetFriendlyErrorMessage(ex);
                throw new Exception(errorMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// 执行查询，返回单个值
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>查询结果（单个值）</returns>
        public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            object result = null;

            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        conn.Open();
                        result = cmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                string errorMessage = GetFriendlyErrorMessage(ex);
                throw new Exception(errorMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// 将SqlException转换为友好的错误提示
        /// </summary>
        private static string GetFriendlyErrorMessage(SqlException ex)
        {
            // 根据SQL错误号返回友好提示
            if (ex.Number == 2627 || ex.Number == 2601)
            {
                return "数据已存在，无法重复添加！";
            }
            else if (ex.Number == 547)
            {
                return "该数据被其他记录引用，无法删除！";
            }
            else if (ex.Number == 515)
            {
                return "必填字段不能为空！";
            }
            else if (ex.Number == 53 || ex.Number == -1)
            {
                return "无法连接到数据库服务器，请检查数据库服务是否启动！";
            }
            else if (ex.Number == 4060)
            {
                return "无法打开数据库，请检查数据库是否存在！";
            }
            else
            {
                return "数据库操作失败：" + ex.Message;
            }
        }
    }
}
