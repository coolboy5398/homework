using System;

namespace StudentGradeManagement.Utils
{
    /// <summary>
    /// 用户会话管理类
    /// 管理当前登录用户的信息和权限
    /// </summary>
    public static class UserSession
    {
        /// <summary>
        /// 用户ID（学号、工号或管理员ID）
        /// </summary>
        public static string UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// 用户角色（Admin-管理员, Teacher-教师, Student-学生）
        /// </summary>
        public static string UserRole { get; set; }

        /// <summary>
        /// 是否已登录
        /// </summary>
        public static bool IsLoggedIn { get; set; }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名称</param>
        /// <param name="role">用户角色</param>
        public static void Login(string userId, string userName, string role)
        {
            UserId = userId;
            UserName = userName;
            UserRole = role;
            IsLoggedIn = true;
        }

        /// <summary>
        /// 退出登录方法
        /// </summary>
        public static void Logout()
        {
            UserId = null;
            UserName = null;
            UserRole = null;
            IsLoggedIn = false;
        }

        /// <summary>
        /// 检查是否为管理员
        /// </summary>
        /// <returns>是管理员返回true，否则返回false</returns>
        public static bool IsAdmin()
        {
            return IsLoggedIn && UserRole == "Admin";
        }

        /// <summary>
        /// 检查是否为教师
        /// </summary>
        /// <returns>是教师返回true，否则返回false</returns>
        public static bool IsTeacher()
        {
            return IsLoggedIn && UserRole == "Teacher";
        }

        /// <summary>
        /// 检查是否为学生
        /// </summary>
        /// <returns>是学生返回true，否则返回false</returns>
        public static bool IsStudent()
        {
            return IsLoggedIn && UserRole == "Student";
        }
    }
}
