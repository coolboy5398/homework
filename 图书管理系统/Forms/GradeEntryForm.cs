using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using StudentGradeManagement.Utils;

namespace StudentGradeManagement.Forms
{
    /// <summary>
    /// 成绩录入窗体
    /// </summary>
    public partial class GradeEntryForm : Form
    {
        public GradeEntryForm()
        {
            InitializeComponent();
            
            // 设置窗体属性（保持Designer中的大小）
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            
            // 初始化DataGridView样式（允许编辑成绩列）
            dgvGrades.AllowUserToAddRows = false;
            dgvGrades.AllowUserToDeleteRows = false;
            dgvGrades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGrades.MultiSelect = false;
            dgvGrades.BackgroundColor = System.Drawing.Color.White;
            dgvGrades.RowTemplate.Height = 28;
            dgvGrades.ColumnHeadersHeight = 32;
            dgvGrades.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void GradeEntryForm_Load(object sender, EventArgs e)
        {
            LoadCourseComboBox();
        }

        /// <summary>
        /// 加载课程下拉框（只显示当前教师授课的课程）
        /// </summary>
        private void LoadCourseComboBox()
        {
            try
            {
                string sql = @"SELECT CourseID, CourseName 
                              FROM Courses 
                              WHERE TeacherID = @TeacherID 
                              ORDER BY CourseID";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@TeacherID", UserSession.UserId)
                };

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);

                cmbCourse.DataSource = dt;
                cmbCourse.DisplayMember = "CourseName";
                cmbCourse.ValueMember = "CourseID";
                cmbCourse.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载课程列表失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 加载学生成绩数据
        /// </summary>
        private void LoadGradeData()
        {
            if (cmbCourse.SelectedIndex == -1)
            {
                MessageBox.Show("请先选择课程！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSemester.Text))
            {
                MessageBox.Show("请输入学期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string courseID = cmbCourse.SelectedValue.ToString();
                string semester = txtSemester.Text.Trim();

                // 查询所有学生及其成绩（如果已录入）
                string sql = @"SELECT s.StudentID AS '学号', 
                                     s.StudentName AS '姓名',
                                     ISNULL(g.Score, NULL) AS '成绩'
                              FROM Students s
                              LEFT JOIN Grades g ON s.StudentID = g.StudentID 
                                  AND g.CourseID = @CourseID 
                                  AND g.Semester = @Semester
                              ORDER BY s.StudentID";

                SqlParameter[] parameters = {
                    new SqlParameter("@CourseID", courseID),
                    new SqlParameter("@Semester", semester)
                };

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
                dgvGrades.DataSource = dt;

                // 设置列属性
                dgvGrades.Columns["学号"].ReadOnly = true;
                dgvGrades.Columns["姓名"].ReadOnly = true;
                dgvGrades.Columns["成绩"].ReadOnly = false;

                dgvGrades.Columns["学号"].Width = 120;
                dgvGrades.Columns["姓名"].Width = 150;
                dgvGrades.Columns["成绩"].Width = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 保存成绩
        /// </summary>
        private void SaveGrades()
        {
            if (cmbCourse.SelectedIndex == -1)
            {
                MessageBox.Show("请先选择课程！", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSemester.Text))
            {
                MessageBox.Show("请输入学期！", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string courseID = cmbCourse.SelectedValue.ToString();
                string semester = txtSemester.Text.Trim();
                int savedCount = 0;

                // 遍历 DataGridView 中的每一行
                foreach (DataGridViewRow row in dgvGrades.Rows)
                {
                    if (row.IsNewRow) continue;

                    string studentID = row.Cells["学号"].Value?.ToString();
                    object scoreValue = row.Cells["成绩"].Value;

                    // 跳过没有输入成绩的行
                    if (scoreValue == null || scoreValue == DBNull.Value || 
                        string.IsNullOrWhiteSpace(scoreValue.ToString()))
                    {
                        continue;
                    }

                    // 验证成绩范围
                    if (!decimal.TryParse(scoreValue.ToString(), out decimal score))
                    {
                        MessageBox.Show($"学号 {studentID} 的成绩格式不正确！", "错误", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (score < 0 || score > 100)
                    {
                        MessageBox.Show($"学号 {studentID} 的成绩必须在 0-100 之间！", "错误", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 检查成绩是否已存在
                    string checkSql = @"SELECT COUNT(*) FROM Grades 
                                       WHERE StudentID = @StudentID 
                                       AND CourseID = @CourseID 
                                       AND Semester = @Semester";

                    SqlParameter[] checkParams = {
                        new SqlParameter("@StudentID", studentID),
                        new SqlParameter("@CourseID", courseID),
                        new SqlParameter("@Semester", semester)
                    };

                    int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));


                    string sql;
                    SqlParameter[] parameters;

                    if (count > 0)
                    {
                        // 更新已有成绩
                        sql = @"UPDATE Grades 
                               SET Score = @Score 
                               WHERE StudentID = @StudentID 
                               AND CourseID = @CourseID 
                               AND Semester = @Semester";

                        parameters = new SqlParameter[] {
                            new SqlParameter("@Score", score),
                            new SqlParameter("@StudentID", studentID),
                            new SqlParameter("@CourseID", courseID),
                            new SqlParameter("@Semester", semester)
                        };
                    }
                    else
                    {
                        // 插入新成绩
                        sql = @"INSERT INTO Grades (StudentID, CourseID, Semester, Score) 
                               VALUES (@StudentID, @CourseID, @Semester, @Score)";

                        parameters = new SqlParameter[] {
                            new SqlParameter("@StudentID", studentID),
                            new SqlParameter("@CourseID", courseID),
                            new SqlParameter("@Semester", semester),
                            new SqlParameter("@Score", score)
                        };
                    }

                    int result = DBHelper.ExecuteNonQuery(sql, parameters);
                    if (result > 0)
                    {
                        savedCount++;
                    }
                }

                MessageBox.Show($"成绩保存成功！共保存 {savedCount} 条记录。", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 重新加载数据
                LoadGradeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// 加载按钮点击事件
        /// </summary>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadGradeData();
        }

        /// <summary>
        /// 保存按钮点击事件
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveGrades();
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
