using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using StudentGradeManagement.Utils;

namespace StudentGradeManagement.Forms
{
    /// <summary>
    /// 成绩查询统计窗体
    /// </summary>
    public partial class GradeQueryForm : Form
    {
        public GradeQueryForm()
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
        private void GradeQueryForm_Load(object sender, EventArgs e)
        {
            LoadCourseComboBox();
            LoadClassComboBox();
            LoadSemesterComboBox();
        }

        /// <summary>
        /// 加载课程下拉框
        /// </summary>
        private void LoadCourseComboBox()
        {
            try
            {
                string sql = "SELECT CourseID, CourseName FROM Courses ORDER BY CourseID";
                DataTable dt = DBHelper.ExecuteQuery(sql);

                // 添加"全部"选项
                DataRow allRow = dt.NewRow();
                allRow["CourseID"] = "";
                allRow["CourseName"] = "全部";
                dt.Rows.InsertAt(allRow, 0);

                cmbCourse.DataSource = dt;
                cmbCourse.DisplayMember = "CourseName";
                cmbCourse.ValueMember = "CourseID";
                cmbCourse.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载课程列表失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载班级下拉框
        /// </summary>
        private void LoadClassComboBox()
        {
            try
            {
                string sql = "SELECT ClassID, ClassName FROM Classes ORDER BY ClassID";
                DataTable dt = DBHelper.ExecuteQuery(sql);

                // 添加"全部"选项
                DataRow allRow = dt.NewRow();
                allRow["ClassID"] = "";
                allRow["ClassName"] = "全部";
                dt.Rows.InsertAt(allRow, 0);

                cmbClass.DataSource = dt;
                cmbClass.DisplayMember = "ClassName";
                cmbClass.ValueMember = "ClassID";
                cmbClass.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载班级列表失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载学期下拉框
        /// </summary>
        private void LoadSemesterComboBox()
        {
            try
            {
                string sql = "SELECT DISTINCT Semester FROM Grades ORDER BY Semester DESC";
                DataTable dt = DBHelper.ExecuteQuery(sql);

                // 添加"全部"选项
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
        /// 查询成绩
        /// </summary>
        private void QueryGrades()
        {
            try
            {
                // 构建查询SQL
                StringBuilder sql = new StringBuilder();
                sql.Append(@"SELECT s.StudentID AS '学号', 
                                   s.StudentName AS '姓名',
                                   c.CourseName AS '课程名称',
                                   g.Semester AS '学期',
                                   g.Score AS '成绩',
                                   cl.ClassName AS '班级'
                            FROM Grades g
                            INNER JOIN Students s ON g.StudentID = s.StudentID
                            INNER JOIN Courses c ON g.CourseID = c.CourseID
                            LEFT JOIN Classes cl ON s.ClassID = cl.ClassID
                            WHERE 1=1");

                // 添加筛选条件
                string courseID = cmbCourse.SelectedValue?.ToString();
                string classID = cmbClass.SelectedValue?.ToString();
                string semester = cmbSemester.SelectedValue?.ToString();

                if (!string.IsNullOrEmpty(courseID))
                {
                    sql.Append(" AND g.CourseID = @CourseID");
                }

                if (!string.IsNullOrEmpty(classID))
                {
                    sql.Append(" AND s.ClassID = @ClassID");
                }

                if (!string.IsNullOrEmpty(semester))
                {
                    sql.Append(" AND g.Semester = @Semester");
                }

                sql.Append(" ORDER BY s.StudentID, c.CourseName");

                // 设置参数
                SqlParameter[] parameters = {
                    new SqlParameter("@CourseID", string.IsNullOrEmpty(courseID) ? (object)DBNull.Value : courseID),
                    new SqlParameter("@ClassID", string.IsNullOrEmpty(classID) ? (object)DBNull.Value : classID),
                    new SqlParameter("@Semester", string.IsNullOrEmpty(semester) ? (object)DBNull.Value : semester)
                };

                DataTable dt = DBHelper.ExecuteQuery(sql.ToString(), parameters);
                dgvGrades.DataSource = dt;

                // 设置列宽
                if (dgvGrades.Columns.Count > 0)
                {
                    dgvGrades.Columns["学号"].Width = 100;
                    dgvGrades.Columns["姓名"].Width = 120;
                    dgvGrades.Columns["课程名称"].Width = 150;
                    dgvGrades.Columns["学期"].Width = 100;
                    dgvGrades.Columns["成绩"].Width = 80;
                    dgvGrades.Columns["班级"].Width = 120;
                }

                // 计算统计信息
                CalculateStatistics(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 计算统计信息
        /// </summary>
        private void CalculateStatistics(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                lblAverage.Text = "平均分：--";
                lblMax.Text = "最高分：--";
                lblMin.Text = "最低分：--";
                lblPassRate.Text = "及格率：--";
                return;
            }

            decimal sum = 0;
            decimal max = decimal.MinValue;
            decimal min = decimal.MaxValue;
            int passCount = 0;
            int totalCount = dt.Rows.Count;

            foreach (DataRow row in dt.Rows)
            {
                decimal score = Convert.ToDecimal(row["成绩"]);
                sum += score;

                if (score > max) max = score;
                if (score < min) min = score;
                if (score >= 60) passCount++;
            }

            decimal average = sum / totalCount;
            decimal passRate = (decimal)passCount / totalCount * 100;

            lblAverage.Text = $"平均分：{average:F2}";
            lblMax.Text = $"最高分：{max:F2}";
            lblMin.Text = $"最低分：{min:F2}";
            lblPassRate.Text = $"及格率：{passRate:F2}%";
        }

        /// <summary>
        /// 导出成绩到CSV文件
        /// </summary>
        private void ExportToCSV()
        {
            if (dgvGrades.Rows.Count == 0)
            {
                MessageBox.Show("没有数据可以导出！", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "CSV文件|*.csv|文本文件|*.txt";
            saveDialog.Title = "导出成绩数据";
            saveDialog.FileName = $"成绩数据_{DateTime.Now:yyyyMMddHHmmss}";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();

                    // 写入列标题
                    for (int i = 0; i < dgvGrades.Columns.Count; i++)
                    {
                        sb.Append(dgvGrades.Columns[i].HeaderText);
                        if (i < dgvGrades.Columns.Count - 1)
                            sb.Append(",");
                    }
                    sb.AppendLine();

                    // 写入数据行
                    foreach (DataGridViewRow row in dgvGrades.Rows)
                    {
                        if (row.IsNewRow) continue;

                        for (int i = 0; i < dgvGrades.Columns.Count; i++)
                        {
                            object value = row.Cells[i].Value;
                            sb.Append(value?.ToString() ?? "");
                            if (i < dgvGrades.Columns.Count - 1)
                                sb.Append(",");
                        }
                        sb.AppendLine();
                    }

                    // 写入统计信息
                    sb.AppendLine();
                    sb.AppendLine("统计信息：");
                    sb.AppendLine(lblAverage.Text);
                    sb.AppendLine(lblMax.Text);
                    sb.AppendLine(lblMin.Text);
                    sb.AppendLine(lblPassRate.Text);

                    // 保存文件
                    File.WriteAllText(saveDialog.FileName, sb.ToString(), Encoding.UTF8);

                    MessageBox.Show("导出成功！", "提示", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"导出失败：{ex.Message}", "错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 清空筛选条件
        /// </summary>
        private void ClearFilters()
        {
            cmbCourse.SelectedIndex = 0;
            cmbClass.SelectedIndex = 0;
            cmbSemester.SelectedIndex = 0;
            dgvGrades.DataSource = null;
            lblAverage.Text = "平均分：--";
            lblMax.Text = "最高分：--";
            lblMin.Text = "最低分：--";
            lblPassRate.Text = "及格率：--";
        }

        /// <summary>
        /// 查询按钮点击事件
        /// </summary>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryGrades();
        }

        /// <summary>
        /// 导出按钮点击事件
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToCSV();
        }

        /// <summary>
        /// 清空按钮点击事件
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilters();
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
