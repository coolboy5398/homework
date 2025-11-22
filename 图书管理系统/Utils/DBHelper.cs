using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StudentGradeManagement.Utils
{
    /// <summary>
    /// 数据库连接帮助类
    /// 提供统一的数据库操作方法
    /// </summary>
    public class DBHelper
    {
        // 从配置文件读取连接字符串
        private static readonly string connectionString = 
            ConfigurationManager.ConnectionStrings["StudentGradeDB"].ConnectionString;

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns>SqlConnection对象</returns>
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
                // 处理SQL异常，转换为友好提示
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
                        // 添加参数
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
                // 处理SQL异常，转换为友好提示
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
                        // 添加参数
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
                // 处理SQL异常，转换为友好提示
                string errorMessage = GetFriendlyErrorMessage(ex);
                throw new Exception(errorMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// 测试数据库连接是否正常
        /// </summary>
        /// <returns>连接成功返回true，否则返回false</returns>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将SqlException转换为友好的错误提示
        /// </summary>
        /// <param name="ex">SQL异常</param>
        /// <returns>友好的错误信息</returns>
        private static string GetFriendlyErrorMessage(SqlException ex)
        {
            // 根据SQL错误号返回友好提示
            switch (ex.Number)
            {
                case 2627: // 违反唯一约束
                case 2601:
                    return "数据已存在，无法重复添加！";
                
                case 547: // 违反外键约束
                    return "该数据被其他记录引用，无法删除！";
                
                case 515: // 不能插入NULL值
                    return "必填字段不能为空！";
                
                case 8152: // 字符串数据过长
                    return "输入的数据过长，请检查输入内容！";
                
                case 245: // 数据类型转换错误
                case 8114:
                    return "数据格式不正确，请检查输入！";
                
                case 53: // 无法连接到服务器
                case -1:
                    return "无法连接到数据库服务器，请检查数据库服务是否启动！";
                
                case 4060: // 无法打开数据库
                    return "无法打开数据库，请检查数据库是否存在！";
                
                case 18456: // 登录失败
                    return "数据库登录失败，请检查连接字符串配置！";
                
                default:
                    // 其他错误返回原始消息
                    return $"数据库操作失败：{ex.Message}";
            }
        }
    }
}
