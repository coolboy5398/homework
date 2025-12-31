using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using LibraryManagement.Utils;

namespace LibraryManagement.Forms
{
    /// <summary>
    /// 登录窗体
    /// </summary>
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // 设置窗体居中显示
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // 设置焦点到用户名输入框
            txtUserName.Focus();
        }

        /// <summary>
        /// 登录按钮点击事件
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 第一步：获取输入的用户名和密码
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            // 第二步：验证输入不能为空
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("用户名不能为空！", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("密码不能为空！", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // 第三步：查询数据库验证用户
            try
            {
                string sql = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";
                SqlParameter[] parameters = {
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@Password", password)
                };

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);

                // 第四步：判断是否登录成功
                if (dt.Rows.Count > 0)
                {
                    // 登录成功，保存用户信息到会话
                    DataRow row = dt.Rows[0];
                    string userId = row["UserID"].ToString();
                    string realName = row["RealName"].ToString();
                    string role = row["Role"].ToString();

                    UserSession.Login(userId, realName, role);

                    // 打开主窗体
                    MainForm mainForm = new MainForm();
                    mainForm.Show();

                    // 隐藏登录窗体
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录失败：" + ex.Message, "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
