using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using StudentGradeManagement.Utils;

namespace StudentGradeManagement.Forms
{
    /// <summary>
    /// 学生成绩查询窗体
    /// </summary>
    public partial class StudentGradeForm : Form
    {
        public StudentGradeForm()
        {
            InitializeComponent();
            
            // 设置窗体属性（保持Designer中的大小）
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            
            // 初始化DataGridView样式
            dgvGrades.AllowUserToAddRows = false;
            dgvGrades.AllowUserToDeleteRows = false;
            dgvGrades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGrades.MultiSelect = false;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void StudentGradeForm_Load(object sender, EventArgs e)
        {
            // 显示学生信息
            lblStudentInfo.Text = $"学生：{UserSession.UserName} ({UserSession.UserId})";

            // 加载学期下拉框
            LoadSemesterComboBox();

            // 加载成绩数据
            LoadGradeData();
        }

        /// <summary>
        /// 加载学期下拉框
        /// </summary>
        private void LoadSemesterComboBox()
        {
            try
            {
                // 查询当前学生的所有学期
                string sql = @"SELECT DISTINCT Semester 
                              FROM Grades 
                              WHERE StudentID = @StudentID 
                              ORDER BY Semester DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@StudentID", UserSession.UserId)
                };

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);

                // 添加"全部学期"选项
                DataRow allRow = dt.NewRow();
                allRow["Semester"] = "";
                dt.Rows.InsertAt(allRow, 0);

                cmbSemester.DataSource = dt;
                cmbSemester.DisplayMember = "Semester";
                cmbSemester.ValueMember = "Semester";
                cmbSemester.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载学期列表失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载成绩数据
        /// </summary>
        private void LoadGradeData()
        {
            try
            {
                // 构建查询SQL
                string sql = @"SELECT c.CourseName AS '课程名称',
                                     g.Semester AS '学期',
                                     g.Score AS '成绩',
                                     c.Credits AS '学分'
                              FROM Grades g
                              INNER JOIN Courses c ON g.CourseID = c.CourseID
                              WHERE g.StudentID = @StudentID";

                // 添加学期筛选条件
                string semester = cmbSemester.SelectedValue?.ToString();
                if (!string.IsNullOrEmpty(semester))
                {
                    sql += " AND g.Semester = @Semester";
                }

                sql += " ORDER BY g.Semester DESC, c.CourseName";

                // 设置参数
                SqlParameter[] parameters;
                if (!string.IsNullOrEmpty(semester))
                {
                    parameters = new SqlParameter[] {
                        new SqlParameter("@StudentID", UserSession.UserId),
                        new SqlParameter("@Semester", semester)
                    };
                }
                else
                {
                    parameters = new SqlParameter[] {
                        new SqlParameter("@StudentID", UserSession.UserId)
                    };
                }

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
                dgvGrades.DataSource = dt;

                // 设置列宽
                if (dgvGrades.Columns.Count > 0)
                {
                    dgvGrades.Columns["课程名称"].Width = 200;
                    dgvGrades.Columns["学期"].Width = 120;
                    dgvGrades.Columns["成绩"].Width = 100;
                    dgvGrades.Columns["学分"].Width = 100;
                }

                // 计算统计信息
                CalculateStatistics(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载成绩数据失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 计算统计信息（总学分和GPA）
        /// </summary>
        private void CalculateStatistics(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                lblTotalCredits.Text = "总学分：0";
                lblGPA.Text = "GPA：--";
                return;
            }

            decimal totalCredits = 0;
            decimal totalGradePoints = 0;

            foreach (DataRow row in dt.Rows)
            {
                decimal score = Convert.ToDecimal(row["成绩"]);
                decimal credits = Convert.ToDecimal(row["学分"]);

                // 计算单科绩点：(成绩/10 - 5) * 学分
                decimal gradePoint = (score / 10 - 5) * credits;

                totalCredits += credits;
                totalGradePoints += gradePoint;
            }

            // 计算GPA：总绩点 / 总学分
            decimal gpa = totalCredits > 0 ? totalGradePoints / totalCredits : 0;

            lblTotalCredits.Text = $"总学分：{totalCredits:F1}";
            lblGPA.Text = $"GPA：{gpa:F2}";
        }

        /// <summary>
        /// DataGridView单元格格式化事件 - 突出显示不及格科目
        /// </summary>
        private void dgvGrades_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // 检查是否是数据行
                if (e.RowIndex < 0 || e.RowIndex >= dgvGrades.Rows.Count)
                    return;

                DataGridViewRow row = dgvGrades.Rows[e.RowIndex];

                // 获取成绩列的值
                if (row.Cells["成绩"].Value != null && row.Cells["成绩"].Value != DBNull.Value)
                {
                    decimal score = Convert.ToDecimal(row.Cells["成绩"].Value);

                    // 不及格科目（成绩 < 60）用红色背景突出显示
                    if (score < 60)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
            catch
            {
                // 忽略格式化错误
            }
        }

        /// <summary>
        /// 查询按钮点击事件
        /// </summary>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadGradeData();
        }

        /// <summary>
        /// 关闭按钮点击事件
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
