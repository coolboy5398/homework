using System;
using System.Windows.Forms;
using LibraryManagement.Forms;

namespace LibraryManagement
{
    /// <summary>
    /// 程序入口类
    /// </summary>
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // 启动登录窗体
            Application.Run(new LoginForm());
        }
    }
}
