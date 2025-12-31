using System;
using System.Windows.Forms;
using LibraryManagement.Utils;

namespace LibraryManagement.Forms
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // 显示欢迎信息
            lblWelcome.Text = "欢迎您，" + UserSession.UserName + "！";
            
            // 显示角色信息
            if (UserSession.IsAdmin())
            {
                lblRole.Text = "角色：管理员";
            }
            else
            {
                lblRole.Text = "角色：普通用户";
            }

            // 根据角色设置菜单权限
            SetMenuPermission();
        }

        /// <summary>
        /// 根据用户角色设置菜单权限
        /// </summary>
        private void SetMenuPermission()
        {
            // 普通用户只能查询，不能管理
            if (!UserSession.IsAdmin())
            {
                // 隐藏管理功能菜单
                menuBookManage.Visible = false;
                menuReaderManage.Visible = false;
                menuCategoryManage.Visible = false;
                menuBorrow.Visible = false;
                menuReturn.Visible = false;
                menuStatistics.Visible = false;
            }
        }

        /// <summary>
        /// 图书管理菜单点击
        /// </summary>
        private void menuBookManage_Click(object sender, EventArgs e)
        {
            BookManageForm form = new BookManageForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 读者管理菜单点击
        /// </summary>
        private void menuReaderManage_Click(object sender, EventArgs e)
        {
            ReaderManageForm form = new ReaderManageForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 分类管理菜单点击
        /// </summary>
        private void menuCategoryManage_Click(object sender, EventArgs e)
        {
            CategoryManageForm form = new CategoryManageForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 借阅操作菜单点击
        /// </summary>
        private void menuBorrow_Click(object sender, EventArgs e)
        {
            BorrowForm form = new BorrowForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 归还操作菜单点击
        /// </summary>
        private void menuReturn_Click(object sender, EventArgs e)
        {
            ReturnForm form = new ReturnForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 借阅查询菜单点击
        /// </summary>
        private void menuBorrowQuery_Click(object sender, EventArgs e)
        {
            BorrowQueryForm form = new BorrowQueryForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 统计查询菜单点击
        /// </summary>
        private void menuStatistics_Click(object sender, EventArgs e)
        {
            StatisticsForm form = new StatisticsForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 退出登录菜单点击
        /// </summary>
        private void menuLogout_Click(object sender, EventArgs e)
        {
            // 清除会话
            UserSession.Logout();

            // 显示登录窗体
            LoginForm loginForm = new LoginForm();
            loginForm.Show();

            // 关闭主窗体
            this.Close();
        }

        /// <summary>
        /// 退出系统菜单点击
        /// </summary>
        private void menuExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要退出系统吗？", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
