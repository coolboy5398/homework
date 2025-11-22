using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using StudentGradeManagement.Utils;

namespace StudentGradeManagement.Forms
{
    /// <summary>
    /// 登录窗体
    /// </summary>
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            // 设置窗体属性
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        /// <summary>
        /// 登录按钮点击事件
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 获取输入的用户名和密码
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // 验证输入
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("请输入用户名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                // 验证登录
                if (ValidateLogin(username, password))
                {
                    // 登录成功，打开主窗体
                    this.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"登录失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 验证登录信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>验证成功返回true，否则返回false</returns>
        private bool ValidateLogin(string username, string password)
        {
            // 只验证管理员登录
            string sql = "SELECT AdminID, AdminName FROM Administrators WHERE AdminID = @Username AND Password = @Password";
            SqlParameter[] parameters = {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };

            DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                // 管理员登录成功
                UserSession.Login(
                    dt.Rows[0]["AdminID"].ToString(),
                    dt.Rows[0]["AdminName"].ToString(),
                    "Admin"
                );
                return true;
            }

            return false;
        }

        /// <summary>
        /// 退出按钮点击事件
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // 测试数据库连接
            if (!DBHelper.TestConnection())
            {
                MessageBox.Show("数据库连接失败！请检查数据库配置。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
