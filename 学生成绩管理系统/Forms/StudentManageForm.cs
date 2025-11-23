using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using StudentGradeManagement.Utils;

namespace StudentGradeManagement.Forms
{
    /// <summary>
    /// 学生管理窗体
    /// </summary>
    public partial class StudentManageForm : Form
    {
        public StudentManageForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void StudentManageForm_Load(object sender, EventArgs e)
        {
            LoadClassComboBox();
            LoadStudentData();
        }

        /// <summary>
        /// 加载班级下拉框
        /// </summary>
        private void LoadClassComboBox()
        {
            try
            {
                string sql = "SELECT ClassID, ClassName FROM Classes ORDER BY GradeLevel DESC, ClassID";
                DataTable dt = DBHelper.ExecuteQuery(sql);

                cmbClass.DataSource = dt;
                cmbClass.DisplayMember = "ClassName";
                cmbClass.ValueMember = "ClassID";
                cmbClass.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载班级列表失败：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载学生数据
        /// </summary>
        private void LoadStudentData()
        {
            try
            {
                string sql = @"SELECT s.StudentID AS '学号', 
                                     s.StudentName AS '姓名', 
                                     s.Gender AS '性别',
                                     CONVERT(VARCHAR(10), s.BirthDate, 120) AS '出生日期',
                                     c.ClassName AS '班级',
                                     s.ContactInfo AS '联系方式'
                              FROM Students s
                              LEFT JOIN Classes c ON s.ClassID = c.ClassID
                              ORDER BY s.StudentID";
                
                DataTable dt = DBHelper.ExecuteQuery(sql);
                dgvStudents.DataSource = dt;
                
                // 设置列宽
                if (dgvStudents.Columns.Count > 0)
                {
                    dgvStudents.Columns["学号"].Width = 100;
                    dgvStudents.Columns["姓名"].Width = 100;
                    dgvStudents.Columns["性别"].Width = 60;
                    dgvStudents.Columns["出生日期"].Width = 100;
                    dgvStudents.Columns["班级"].Width = 150;
                    dgvStudents.Columns["联系方式"].Width = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 添加学生
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 验证输入
            if (!ValidateInput())
            {
                return;
            }

            string studentID = txtStudentID.Text.Trim();
            string studentName = txtStudentName.Text.Trim();
            string gender = cmbGender.Text;
            DateTime birthDate = dtpBirthDate.Value;
            string classID = cmbClass.SelectedValue?.ToString();
            string contactInfo = txtContactInfo.Text.Trim();

            try
            {
                // 检查学号是否已存在
                string checkSql = "SELECT COUNT(*) FROM Students WHERE StudentID = @StudentID";
                SqlParameter[] checkParams = { new SqlParameter("@StudentID", studentID) };
                int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("学号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 插入数据
                string sql = @"INSERT INTO Students (StudentID, StudentName, Gender, BirthDate, ClassID, ContactInfo) 
                              VALUES (@StudentID, @StudentName, @Gender, @BirthDate, @ClassID, @ContactInfo)";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@StudentID", studentID),
                    new SqlParameter("@StudentName", studentName),
                    new SqlParameter("@Gender", gender),
                    new SqlParameter("@BirthDate", birthDate),
                    new SqlParameter("@ClassID", string.IsNullOrEmpty(classID) ? (object)DBNull.Value : classID),
                    new SqlParameter("@ContactInfo", string.IsNullOrEmpty(contactInfo) ? (object)DBNull.Value : contactInfo)
                };

                int result = DBHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadStudentData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 修改学生
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要修改的学生！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 验证输入
            if (!ValidateInput())
            {
                return;
            }

            string studentID = txtStudentID.Text.Trim();
            string studentName = txtStudentName.Text.Trim();
            string gender = cmbGender.Text;
            DateTime birthDate = dtpBirthDate.Value;
            string classID = cmbClass.SelectedValue?.ToString();
            string contactInfo = txtContactInfo.Text.Trim();

            try
            {
                // 更新数据（学号不允许修改）
                string sql = @"UPDATE Students 
                              SET StudentName = @StudentName, 
                                  Gender = @Gender, 
                                  BirthDate = @BirthDate, 
                                  ClassID = @ClassID, 
                                  ContactInfo = @ContactInfo 
                              WHERE StudentID = @StudentID";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@StudentName", studentName),
                    new SqlParameter("@Gender", gender),
                    new SqlParameter("@BirthDate", birthDate),
                    new SqlParameter("@ClassID", string.IsNullOrEmpty(classID) ? (object)DBNull.Value : classID),
                    new SqlParameter("@ContactInfo", string.IsNullOrEmpty(contactInfo) ? (object)DBNull.Value : contactInfo),
                    new SqlParameter("@StudentID", studentID)
                };

                int result = DBHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadStudentData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要删除的学生！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string studentID = dgvStudents.SelectedRows[0].Cells["学号"].Value.ToString();
            string studentName = dgvStudents.SelectedRows[0].Cells["姓名"].Value.ToString();

            DialogResult result = MessageBox.Show($"确定要删除学生 [{studentName}] 吗？", "确认删除", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 检查是否有成绩记录
                    string checkSql = "SELECT COUNT(*) FROM Grades WHERE StudentID = @StudentID";
                    SqlParameter[] checkParams = { new SqlParameter("@StudentID", studentID) };
                    int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                    if (count > 0)
                    {
                        MessageBox.Show($"该学生有 {count} 条成绩记录，无法删除！", "提示", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 删除数据
                    string sql = "DELETE FROM Students WHERE StudentID = @StudentID";
                    SqlParameter[] parameters = { new SqlParameter("@StudentID", studentID) };

                    int deleteResult = DBHelper.ExecuteNonQuery(sql, parameters);

                    if (deleteResult > 0)
                    {
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadStudentData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 查询学生
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
                    LoadStudentData();
                    return;
                }
                else
                {
                    // 按学号或姓名查询
                    sql = @"SELECT s.StudentID AS '学号', 
                                  s.StudentName AS '姓名', 
                                  s.Gender AS '性别',
                                  CONVERT(VARCHAR(10), s.BirthDate, 120) AS '出生日期',
                                  c.ClassName AS '班级',
                                  s.ContactInfo AS '联系方式'
                           FROM Students s
                           LEFT JOIN Classes c ON s.ClassID = c.ClassID
                           WHERE s.StudentID LIKE @Keyword OR s.StudentName LIKE @Keyword
                           ORDER BY s.StudentID";
                    
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@Keyword", "%" + keyword + "%") 
                    };
                }

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
                dgvStudents.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到匹配的学生！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            LoadStudentData();
        }

        /// <summary>
        /// DataGridView选中行改变事件
        /// </summary>
        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvStudents.SelectedRows[0];
                txtStudentID.Text = row.Cells["学号"].Value.ToString();
                txtStudentName.Text = row.Cells["姓名"].Value.ToString();
                cmbGender.Text = row.Cells["性别"].Value.ToString();
                
                if (DateTime.TryParse(row.Cells["出生日期"].Value.ToString(), out DateTime birthDate))
                {
                    dtpBirthDate.Value = birthDate;
                }

                // 设置班级下拉框
                string className = row.Cells["班级"].Value?.ToString();
                if (!string.IsNullOrEmpty(className))
                {
                    for (int i = 0; i < cmbClass.Items.Count; i++)
                    {
                        DataRowView drv = cmbClass.Items[i] as DataRowView;
                        if (drv != null && drv["ClassName"].ToString() == className)
                        {
                            cmbClass.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    cmbClass.SelectedIndex = -1;
                }

                txtContactInfo.Text = row.Cells["联系方式"].Value?.ToString() ?? "";
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtStudentID.Text.Trim()))
            {
                MessageBox.Show("请输入学号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentID.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtStudentName.Text.Trim()))
            {
                MessageBox.Show("请输入姓名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbGender.Text))
            {
                MessageBox.Show("请选择性别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGender.Focus();
                return false;
            }

            // 验证出生日期不能是未来日期
            if (dtpBirthDate.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("出生日期不能是未来日期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpBirthDate.Focus();
                return false;
            }

            // 验证出生日期合理性（不能早于1900年）
            if (dtpBirthDate.Value.Year < 1900)
            {
                MessageBox.Show("出生日期不能早于 1900 年！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpBirthDate.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 清空输入框
        /// </summary>
        private void ClearInputs()
        {
            txtStudentID.Clear();
            txtStudentName.Clear();
            cmbGender.SelectedIndex = -1;
            dtpBirthDate.Value = DateTime.Now;
            cmbClass.SelectedIndex = -1;
            txtContactInfo.Clear();
            txtStudentID.Focus();
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
