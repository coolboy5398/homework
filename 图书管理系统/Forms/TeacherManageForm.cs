using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using StudentGradeManagement.Utils;

namespace StudentGradeManagement.Forms
{
    /// <summary>
    /// 教师管理窗体
    /// </summary>
    public partial class TeacherManageForm : Form
    {
        public TeacherManageForm()
        {
            InitializeComponent();
            
            // 设置窗体属性（保持Designer中的大小）
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            
            // 初始化DataGridView样式
            dgvTeachers.AllowUserToAddRows = false;
            dgvTeachers.AllowUserToDeleteRows = false;
            dgvTeachers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTeachers.MultiSelect = false;
            
            // 设置Enter键提交
            this.AcceptButton = btnAdd;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void TeacherManageForm_Load(object sender, EventArgs e)
        {
            LoadTeacherData();
        }

        /// <summary>
        /// 加载教师数据
        /// </summary>
        private void LoadTeacherData()
        {
            try
            {
                string sql = @"SELECT t.TeacherID AS '工号', 
                                     t.TeacherName AS '姓名', 
                                     t.Gender AS '性别',
                                     t.Department AS '所属系部',
                                     t.ContactInfo AS '联系方式',
                                     (SELECT COUNT(*) FROM Courses WHERE TeacherID = t.TeacherID) AS '课程数量'
                              FROM Teachers t
                              ORDER BY t.TeacherID";
                
                DataTable dt = DBHelper.ExecuteQuery(sql);
                dgvTeachers.DataSource = dt;
                
                // 设置列宽
                if (dgvTeachers.Columns.Count > 0)
                {
                    dgvTeachers.Columns["工号"].Width = 100;
                    dgvTeachers.Columns["姓名"].Width = 100;
                    dgvTeachers.Columns["性别"].Width = 60;
                    dgvTeachers.Columns["所属系部"].Width = 150;
                    dgvTeachers.Columns["联系方式"].Width = 150;
                    dgvTeachers.Columns["课程数量"].Width = 80;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 添加教师
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 验证输入
            if (!ValidateInput())
            {
                return;
            }

            string teacherID = txtTeacherID.Text.Trim();
            string teacherName = txtTeacherName.Text.Trim();
            string gender = cmbGender.Text;
            string department = txtDepartment.Text.Trim();
            string contactInfo = txtContactInfo.Text.Trim();

            try
            {
                // 检查工号是否已存在
                string checkSql = "SELECT COUNT(*) FROM Teachers WHERE TeacherID = @TeacherID";
                SqlParameter[] checkParams = { new SqlParameter("@TeacherID", teacherID) };
                int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("工号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 插入数据
                string sql = @"INSERT INTO Teachers (TeacherID, TeacherName, Gender, Department, ContactInfo) 
                              VALUES (@TeacherID, @TeacherName, @Gender, @Department, @ContactInfo)";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@TeacherID", teacherID),
                    new SqlParameter("@TeacherName", teacherName),
                    new SqlParameter("@Gender", gender),
                    new SqlParameter("@Department", string.IsNullOrEmpty(department) ? (object)DBNull.Value : department),
                    new SqlParameter("@ContactInfo", string.IsNullOrEmpty(contactInfo) ? (object)DBNull.Value : contactInfo)
                };

                int result = DBHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadTeacherData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 修改教师
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTeachers.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要修改的教师！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 验证输入
            if (!ValidateInput())
            {
                return;
            }

            string teacherID = txtTeacherID.Text.Trim();
            string teacherName = txtTeacherName.Text.Trim();
            string gender = cmbGender.Text;
            string department = txtDepartment.Text.Trim();
            string contactInfo = txtContactInfo.Text.Trim();

            try
            {
                // 更新数据（工号不允许修改）
                string sql = @"UPDATE Teachers 
                              SET TeacherName = @TeacherName, 
                                  Gender = @Gender, 
                                  Department = @Department, 
                                  ContactInfo = @ContactInfo 
                              WHERE TeacherID = @TeacherID";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@TeacherName", teacherName),
                    new SqlParameter("@Gender", gender),
                    new SqlParameter("@Department", string.IsNullOrEmpty(department) ? (object)DBNull.Value : department),
                    new SqlParameter("@ContactInfo", string.IsNullOrEmpty(contactInfo) ? (object)DBNull.Value : contactInfo),
                    new SqlParameter("@TeacherID", teacherID)
                };

                int result = DBHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadTeacherData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除教师
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTeachers.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要删除的教师！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string teacherID = dgvTeachers.SelectedRows[0].Cells["工号"].Value.ToString();
            string teacherName = dgvTeachers.SelectedRows[0].Cells["姓名"].Value.ToString();

            DialogResult result = MessageBox.Show($"确定要删除教师 [{teacherName}] 吗？", "确认删除", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 检查是否有课程关联
                    string checkSql = "SELECT COUNT(*) FROM Courses WHERE TeacherID = @TeacherID";
                    SqlParameter[] checkParams = { new SqlParameter("@TeacherID", teacherID) };
                    int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                    if (count > 0)
                    {
                        MessageBox.Show($"该教师有 {count} 门课程，无法删除！", "提示", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 删除数据
                    string sql = "DELETE FROM Teachers WHERE TeacherID = @TeacherID";
                    SqlParameter[] parameters = { new SqlParameter("@TeacherID", teacherID) };

                    int deleteResult = DBHelper.ExecuteNonQuery(sql, parameters);

                    if (deleteResult > 0)
                    {
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadTeacherData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 查询教师
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
                    LoadTeacherData();
                    return;
                }
                else
                {
                    // 按工号或姓名查询
                    sql = @"SELECT t.TeacherID AS '工号', 
                                  t.TeacherName AS '姓名', 
                                  t.Gender AS '性别',
                                  t.Department AS '所属系部',
                                  t.ContactInfo AS '联系方式',
                                  (SELECT COUNT(*) FROM Courses WHERE TeacherID = t.TeacherID) AS '课程数量'
                           FROM Teachers t
                           WHERE t.TeacherID LIKE @Keyword OR t.TeacherName LIKE @Keyword
                           ORDER BY t.TeacherID";
                    
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@Keyword", "%" + keyword + "%") 
                    };
                }

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
                dgvTeachers.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到匹配的教师！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            LoadTeacherData();
        }

        /// <summary>
        /// DataGridView选中行改变事件
        /// </summary>
        private void dgvTeachers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTeachers.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvTeachers.SelectedRows[0];
                txtTeacherID.Text = row.Cells["工号"].Value.ToString();
                txtTeacherName.Text = row.Cells["姓名"].Value.ToString();
                cmbGender.Text = row.Cells["性别"].Value.ToString();
                txtDepartment.Text = row.Cells["所属系部"].Value?.ToString() ?? "";
                txtContactInfo.Text = row.Cells["联系方式"].Value?.ToString() ?? "";
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtTeacherID.Text.Trim()))
            {
                MessageBox.Show("请输入工号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTeacherID.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtTeacherName.Text.Trim()))
            {
                MessageBox.Show("请输入姓名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTeacherName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(cmbGender.Text))
            {
                MessageBox.Show("请选择性别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGender.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 清空输入框
        /// </summary>
        private void ClearInputs()
        {
            txtTeacherID.Clear();
            txtTeacherName.Clear();
            cmbGender.SelectedIndex = -1;
            txtDepartment.Clear();
            txtContactInfo.Clear();
            txtTeacherID.Focus();
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
