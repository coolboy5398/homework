using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ParkingManagement.Utils;

namespace ParkingManagement.Forms
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
            
            // 设置回车键触发登录
            this.AcceptButton = btnLogin;
            
            // 聚焦到用户名输入框
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

            // 第二步：检查输入是否为空
            if (userName == "")
            {
                MessageBox.Show("请输入用户名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return;
            }

            if (password == "")
            {
                MessageBox.Show("请输入密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // 第三步：查询数据库验证用户
            try
            {
                string sql = "SELECT UserID, UserName FROM Users WHERE UserName = @UserName AND Password = @Password";
                SqlParameter[] parameters = {
                    new SqlParameter("@UserName", userName),
                    new SqlParameter("@Password", password)
                };

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);

                // 第四步：判断是否登录成功
                if (dt.Rows.Count > 0)
                {
                    // 登录成功，保存用户信息
                    string userId = dt.Rows[0]["UserID"].ToString();
                    string realName = dt.Rows[0]["UserName"].ToString();
                    UserSession.Login(userId, realName);

                    // 打开主窗体
                    MainForm mainForm = new MainForm();
                    mainForm.Show();

                    // 隐藏登录窗体
                    this.Hide();
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
                MessageBox.Show("登录失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
