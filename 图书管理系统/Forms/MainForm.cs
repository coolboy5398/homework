using System;
using System.Windows.Forms;
using StudentGradeManagement.Utils;

namespace StudentGradeManagement.Forms
{
    /// <summary>
    /// 主菜单窗体
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
            // 设置窗体属性
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // 显示欢迎信息
            lblWelcome.Text = $"欢迎您，{UserSession.UserName}！";

            // 根据用户角色显示不同的菜单
            ConfigureMenuByRole();
        }

        /// <summary>
        /// 根据用户角色配置菜单
        /// </summary>
        private void ConfigureMenuByRole()
        {
            // 隐藏所有菜单项
            menuStudent.Visible = false;
            menuClass.Visible = false;
            menuTeacher.Visible = false;
            menuCourse.Visible = false;
            menuGradeEntry.Visible = false;
            menuGradeQuery.Visible = false;
            menuMyGrade.Visible = false;

            // 根据角色显示对应菜单
            if (UserSession.IsAdmin())
            {
                // 管理员：显示所有管理菜单
                menuStudent.Visible = true;
                menuClass.Visible = true;
                menuTeacher.Visible = true;
                menuCourse.Visible = true;
                menuGradeEntry.Visible = true;
                menuGradeQuery.Visible = true;
            }
            else if (UserSession.IsTeacher())
            {
                // 教师：显示成绩录入和查询
                menuGradeEntry.Visible = true;
                menuGradeQuery.Visible = true;
            }
            else if (UserSession.IsStudent())
            {
                // 学生：只显示我的成绩
                menuMyGrade.Visible = true;
            }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        private void menuLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要退出登录吗？", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                UserSession.Logout();
                this.Close();
                
                // 重新显示登录窗体
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        // 以下是各个菜单项的点击事件处理方法（暂时为空，后续实现）
        
        private void menuStudent_Click(object sender, EventArgs e)
        {
            StudentManageForm form = new StudentManageForm();
            form.ShowDialog();
        }

        private void menuClass_Click(object sender, EventArgs e)
        {
            ClassManageForm form = new ClassManageForm();
            form.ShowDialog();
        }

        private void menuTeacher_Click(object sender, EventArgs e)
        {
            TeacherManageForm form = new TeacherManageForm();
            form.ShowDialog();
        }

        private void menuCourse_Click(object sender, EventArgs e)
        {
            CourseManageForm form = new CourseManageForm();
            form.ShowDialog();
        }

        private void menuGradeEntry_Click(object sender, EventArgs e)
        {
            GradeEntryForm form = new GradeEntryForm();
            form.ShowDialog();
        }

        private void menuGradeQuery_Click(object sender, EventArgs e)
        {
            GradeQueryForm form = new GradeQueryForm();
            form.ShowDialog();
        }

        private void menuMyGrade_Click(object sender, EventArgs e)
        {
            StudentGradeForm form = new StudentGradeForm();
            form.ShowDialog();
        }
    }
}
