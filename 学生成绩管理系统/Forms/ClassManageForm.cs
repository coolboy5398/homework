using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using StudentGradeManagement.Utils;

namespace StudentGradeManagement.Forms
{
    /// <summary>
    /// 班级管理窗体
    /// </summary>
    public partial class ClassManageForm : Form
    {
        public ClassManageForm()
        {
            InitializeComponent();
            
            // 设置窗体属性（保持Designer中的大小）
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            
            // 初始化DataGridView样式
            dgvClasses.AllowUserToAddRows = false;
            dgvClasses.AllowUserToDeleteRows = false;
            dgvClasses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClasses.MultiSelect = false;
            
            // 设置Enter键提交
            this.AcceptButton = btnAdd;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void ClassManageForm_Load(object sender, EventArgs e)
        {
            LoadClassData();
        }

        /// <summary>
        /// 加载班级数据
        /// </summary>
        private void LoadClassData()
        {
            try
            {
                string sql = @"SELECT ClassID AS '班级编号', 
                                     ClassName AS '班级名称', 
                                     GradeLevel AS '年级',
                                     (SELECT COUNT(*) FROM Students WHERE ClassID = Classes.ClassID) AS '学生人数'
                              FROM Classes 
                              ORDER BY GradeLevel DESC, ClassID";
                
                DataTable dt = DBHelper.ExecuteQuery(sql);
                dgvClasses.DataSource = dt;
                
                // 设置列宽
                if (dgvClasses.Columns.Count > 0)
                {
                    dgvClasses.Columns["班级编号"].Width = 120;
                    dgvClasses.Columns["班级名称"].Width = 200;
                    dgvClasses.Columns["年级"].Width = 100;
                    dgvClasses.Columns["学生人数"].Width = 100;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 添加班级
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string classID = txtClassID.Text.Trim();
            string className = txtClassName.Text.Trim();
            string gradeLevel = txtGradeLevel.Text.Trim();

            // 验证输入
            if (string.IsNullOrEmpty(classID))
            {
                MessageBox.Show("请输入班级编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClassID.Focus();
                return;
            }

            if (string.IsNullOrEmpty(className))
            {
                MessageBox.Show("请输入班级名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClassName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(gradeLevel))
            {
                MessageBox.Show("请输入年级！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGradeLevel.Focus();
                return;
            }

            if (!int.TryParse(gradeLevel, out int grade))
            {
                MessageBox.Show("年级必须是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGradeLevel.Focus();
                return;
            }

            if (grade < 1900 || grade > 2100)
            {
                MessageBox.Show("年级范围必须在 1900 到 2100 之间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGradeLevel.Focus();
                return;
            }

            try
            {
                // 检查班级编号是否已存在
                string checkSql = "SELECT COUNT(*) FROM Classes WHERE ClassID = @ClassID";
                SqlParameter[] checkParams = { new SqlParameter("@ClassID", classID) };
                int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("班级编号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 检查班级名称是否已存在
                checkSql = "SELECT COUNT(*) FROM Classes WHERE ClassName = @ClassName";
                checkParams = new SqlParameter[] { new SqlParameter("@ClassName", className) };
                count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("班级名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 插入数据
                string sql = "INSERT INTO Classes (ClassID, ClassName, GradeLevel) VALUES (@ClassID, @ClassName, @GradeLevel)";
                SqlParameter[] parameters = {
                    new SqlParameter("@ClassID", classID),
                    new SqlParameter("@ClassName", className),
                    new SqlParameter("@GradeLevel", grade)
                };

                int result = DBHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadClassData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 修改班级
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvClasses.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要修改的班级！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string classID = dgvClasses.SelectedRows[0].Cells["班级编号"].Value.ToString();
            string className = txtClassName.Text.Trim();
            string gradeLevel = txtGradeLevel.Text.Trim();

            // 验证输入
            if (string.IsNullOrEmpty(className))
            {
                MessageBox.Show("请输入班级名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClassName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(gradeLevel))
            {
                MessageBox.Show("请输入年级！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGradeLevel.Focus();
                return;
            }

            if (!int.TryParse(gradeLevel, out int grade))
            {
                MessageBox.Show("年级必须是数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGradeLevel.Focus();
                return;
            }

            if (grade < 1900 || grade > 2100)
            {
                MessageBox.Show("年级范围必须在 1900 到 2100 之间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGradeLevel.Focus();
                return;
            }

            try
            {
                // 检查班级名称是否与其他班级重复
                string checkSql = "SELECT COUNT(*) FROM Classes WHERE ClassName = @ClassName AND ClassID != @ClassID";
                SqlParameter[] checkParams = {
                    new SqlParameter("@ClassName", className),
                    new SqlParameter("@ClassID", classID)
                };
                int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("班级名称已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 更新数据
                string sql = "UPDATE Classes SET ClassName = @ClassName, GradeLevel = @GradeLevel WHERE ClassID = @ClassID";
                SqlParameter[] parameters = {
                    new SqlParameter("@ClassName", className),
                    new SqlParameter("@GradeLevel", grade),
                    new SqlParameter("@ClassID", classID)
                };

                int result = DBHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadClassData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除班级
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvClasses.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要删除的班级！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string classID = dgvClasses.SelectedRows[0].Cells["班级编号"].Value.ToString();
            string className = dgvClasses.SelectedRows[0].Cells["班级名称"].Value.ToString();

            DialogResult result = MessageBox.Show($"确定要删除班级 [{className}] 吗？", "确认删除", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 检查是否有学生
                    string checkSql = "SELECT COUNT(*) FROM Students WHERE ClassID = @ClassID";
                    SqlParameter[] checkParams = { new SqlParameter("@ClassID", classID) };
                    int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                    if (count > 0)
                    {
                        MessageBox.Show($"该班级有 {count} 名学生，无法删除！", "提示", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 删除数据
                    string sql = "DELETE FROM Classes WHERE ClassID = @ClassID";
                    SqlParameter[] parameters = { new SqlParameter("@ClassID", classID) };

                    int deleteResult = DBHelper.ExecuteNonQuery(sql, parameters);

                    if (deleteResult > 0)
                    {
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadClassData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 查询班级
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
                    LoadClassData();
                    return;
                }
                else
                {
                    // 按班级名称模糊查询
                    sql = @"SELECT ClassID AS '班级编号', 
                                  ClassName AS '班级名称', 
                                  GradeLevel AS '年级',
                                  (SELECT COUNT(*) FROM Students WHERE ClassID = Classes.ClassID) AS '学生人数'
                           FROM Classes 
                           WHERE ClassName LIKE @Keyword
                           ORDER BY GradeLevel DESC, ClassID";
                    
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@Keyword", "%" + keyword + "%") 
                    };
                }

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
                dgvClasses.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到匹配的班级！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            LoadClassData();
        }

        /// <summary>
        /// DataGridView选中行改变事件
        /// </summary>
        private void dgvClasses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClasses.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvClasses.SelectedRows[0];
                txtClassID.Text = row.Cells["班级编号"].Value.ToString();
                txtClassName.Text = row.Cells["班级名称"].Value.ToString();
                txtGradeLevel.Text = row.Cells["年级"].Value.ToString();
            }
        }

        /// <summary>
        /// 清空输入框
        /// </summary>
        private void ClearInputs()
        {
            txtClassID.Clear();
            txtClassName.Clear();
            txtGradeLevel.Clear();
            txtClassID.Focus();
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
