using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using StudentGradeManagement.Utils;

namespace StudentGradeManagement.Forms
{
    /// <summary>
    /// 课程管理窗体
    /// </summary>
    public partial class CourseManageForm : Form
    {
        public CourseManageForm()
        {
            InitializeComponent();
            
            // 设置窗体属性（保持Designer中的大小）
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            
            // 初始化DataGridView样式
            dgvCourses.AllowUserToAddRows = false;
            dgvCourses.AllowUserToDeleteRows = false;
            dgvCourses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCourses.MultiSelect = false;
            
            // 设置Enter键提交
            this.AcceptButton = btnAdd;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void CourseManageForm_Load(object sender, EventArgs e)
        {
            LoadTeacherComboBox();
            LoadCourseData();
        }

        /// <summary>
        /// 加载教师下拉框
        /// </summary>
        private void LoadTeacherComboBox()
        {
            try
            {
                string sql = "SELECT TeacherID, TeacherName FROM Teachers ORDER BY TeacherID";
                DataTable dt = DBHelper.ExecuteQuery(sql);

                cmbTeacher.DataSource = dt;
                cmbTeacher.DisplayMember = "TeacherName";
                cmbTeacher.ValueMember = "TeacherID";
                cmbTeacher.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载教师列表失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载课程数据
        /// </summary>
        private void LoadCourseData()
        {
            try
            {
                string sql = @"SELECT c.CourseID AS '课程编号', 
                                     c.CourseName AS '课程名称', 
                                     c.Credits AS '学分',
                                     t.TeacherName AS '授课教师',
                                     (SELECT COUNT(*) FROM Grades WHERE CourseID = c.CourseID) AS '成绩记录数'
                              FROM Courses c
                              LEFT JOIN Teachers t ON c.TeacherID = t.TeacherID
                              ORDER BY c.CourseID";
                
                DataTable dt = DBHelper.ExecuteQuery(sql);
                dgvCourses.DataSource = dt;
                
                // 设置列宽
                if (dgvCourses.Columns.Count > 0)
                {
                    dgvCourses.Columns["课程编号"].Width = 120;
                    dgvCourses.Columns["课程名称"].Width = 200;
                    dgvCourses.Columns["学分"].Width = 80;
                    dgvCourses.Columns["授课教师"].Width = 120;
                    dgvCourses.Columns["成绩记录数"].Width = 100;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 添加课程
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 验证输入
            if (!ValidateInput())
            {
                return;
            }

            string courseID = txtCourseID.Text.Trim();
            string courseName = txtCourseName.Text.Trim();
            decimal credits = decimal.Parse(txtCredits.Text.Trim());
            string teacherID = cmbTeacher.SelectedValue?.ToString();

            try
            {
                // 检查课程编号是否已存在
                string checkSql = "SELECT COUNT(*) FROM Courses WHERE CourseID = @CourseID";
                SqlParameter[] checkParams = { new SqlParameter("@CourseID", courseID) };
                int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("课程编号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 检查课程名称是否已存在
                checkSql = "SELECT COUNT(*) FROM Courses WHERE CourseName = @CourseName";
                checkParams = new SqlParameter[] { new SqlParameter("@CourseName", courseName) };
                count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("课程名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 插入数据
                string sql = @"INSERT INTO Courses (CourseID, CourseName, Credits, TeacherID) 
                              VALUES (@CourseID, @CourseName, @Credits, @TeacherID)";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@CourseID", courseID),
                    new SqlParameter("@CourseName", courseName),
                    new SqlParameter("@Credits", credits),
                    new SqlParameter("@TeacherID", string.IsNullOrEmpty(teacherID) ? (object)DBNull.Value : teacherID)
                };

                int result = DBHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadCourseData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 修改课程
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要修改的课程！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 验证输入
            if (!ValidateInput())
            {
                return;
            }

            string courseID = txtCourseID.Text.Trim();
            string courseName = txtCourseName.Text.Trim();
            decimal credits = decimal.Parse(txtCredits.Text.Trim());
            string teacherID = cmbTeacher.SelectedValue?.ToString();

            try
            {
                // 检查课程名称是否与其他课程重复
                string checkSql = "SELECT COUNT(*) FROM Courses WHERE CourseName = @CourseName AND CourseID != @CourseID";
                SqlParameter[] checkParams = {
                    new SqlParameter("@CourseName", courseName),
                    new SqlParameter("@CourseID", courseID)
                };
                int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("课程名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 更新数据
                string sql = @"UPDATE Courses 
                              SET CourseName = @CourseName, 
                                  Credits = @Credits, 
                                  TeacherID = @TeacherID 
                              WHERE CourseID = @CourseID";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@CourseName", courseName),
                    new SqlParameter("@Credits", credits),
                    new SqlParameter("@TeacherID", string.IsNullOrEmpty(teacherID) ? (object)DBNull.Value : teacherID),
                    new SqlParameter("@CourseID", courseID)
                };

                int result = DBHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadCourseData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除课程
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要删除的课程！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string courseID = dgvCourses.SelectedRows[0].Cells["课程编号"].Value.ToString();
            string courseName = dgvCourses.SelectedRows[0].Cells["课程名称"].Value.ToString();

            DialogResult result = MessageBox.Show($"确定要删除课程 [{courseName}] 吗？", "确认删除", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 检查是否有成绩记录
                    string checkSql = "SELECT COUNT(*) FROM Grades WHERE CourseID = @CourseID";
                    SqlParameter[] checkParams = { new SqlParameter("@CourseID", courseID) };
                    int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                    if (count > 0)
                    {
                        MessageBox.Show($"该课程有 {count} 条成绩记录，无法删除！", "提示", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 删除数据
                    string sql = "DELETE FROM Courses WHERE CourseID = @CourseID";
                    SqlParameter[] parameters = { new SqlParameter("@CourseID", courseID) };

                    int deleteResult = DBHelper.ExecuteNonQuery(sql, parameters);

                    if (deleteResult > 0)
                    {
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadCourseData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 查询课程
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            try
            {
                string sql;
                SqlParameter[] parameters = null;

                if (string.IsNullOrEmpty(keyword))
                {
                    // 查询所有
                    LoadCourseData();
                    return;
                }
                else
                {
                    // 按课程名称模糊查询
                    sql = @"SELECT c.CourseID AS '课程编号', 
                                  c.CourseName AS '课程名称', 
                                  c.Credits AS '学分',
                                  t.TeacherName AS '授课教师',
                                  (SELECT COUNT(*) FROM Grades WHERE CourseID = c.CourseID) AS '成绩记录数'
                           FROM Courses c
                           LEFT JOIN Teachers t ON c.TeacherID = t.TeacherID
                           WHERE c.CourseName LIKE @Keyword
                           ORDER BY c.CourseID";
                    
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@Keyword", "%" + keyword + "%") 
                    };
                }

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
                dgvCourses.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到匹配的课程！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearInputs();
            txtSearch.Clear();
            LoadCourseData();
        }

        /// <summary>
        /// DataGridView选中行改变事件
        /// </summary>
        private void dgvCourses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCourses.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvCourses.SelectedRows[0];
                txtCourseID.Text = row.Cells["课程编号"].Value.ToString();
                txtCourseName.Text = row.Cells["课程名称"].Value.ToString();
                txtCredits.Text = row.Cells["学分"].Value.ToString();

                // 设置教师下拉框
                string teacherName = row.Cells["授课教师"].Value?.ToString();
                if (!string.IsNullOrEmpty(teacherName))
                {
                    for (int i = 0; i < cmbTeacher.Items.Count; i++)
                    {
                        DataRowView drv = cmbTeacher.Items[i] as DataRowView;
                        if (drv != null && drv["TeacherName"].ToString() == teacherName)
                        {
                            cmbTeacher.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    cmbTeacher.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtCourseID.Text.Trim()))
            {
                MessageBox.Show("请输入课程编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCourseID.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCourseName.Text.Trim()))
            {
                MessageBox.Show("请输入课程名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCourseName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCredits.Text.Trim()))
            {
                MessageBox.Show("请输入学分！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCredits.Focus();
                return false;
            }

            if (!decimal.TryParse(txtCredits.Text.Trim(), out decimal credits))
            {
                MessageBox.Show("学分必须是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCredits.Focus();
                return false;
            }

            if (credits < 0.5m || credits > 10.0m)
            {
                MessageBox.Show("学分范围必须在 0.5 到 10.0 之间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCredits.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 清空输入框
        /// </summary>
        private void ClearInputs()
        {
            txtCourseID.Clear();
            txtCourseName.Clear();
            txtCredits.Clear();
            cmbTeacher.SelectedIndex = -1;
            txtCourseID.Focus();
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
