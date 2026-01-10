namespace ParkingManagement.Utils
{
    /// <summary>
    /// 用户会话管理类 - 保存当前登录用户信息
    /// </summary>
    public static class UserSession
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public static string UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// 是否已登录
        /// </summary>
        public static bool IsLoggedIn { get; set; }

        /// <summary>
        /// 登录
        /// </summary>
        public static void Login(string userId, string userName)
        {
            UserID = userId;
            UserName = userName;
            IsLoggedIn = true;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        public static void Logout()
        {
            UserID = null;
            UserName = null;
            IsLoggedIn = false;
        }
    }
}
