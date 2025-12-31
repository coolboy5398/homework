using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using LibraryManagement.Utils;

namespace LibraryManagement.Forms
{
    /// <summary>
    /// 分类管理窗体
    /// </summary>
    public partial class CategoryManageForm : Form
    {
        public CategoryManageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void CategoryManageForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 加载分类数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                string sql = "SELECT CategoryID AS 分类ID, CategoryName AS 分类名称 FROM Categories ORDER BY CategoryID";
                DataTable dt = DBHelper.ExecuteQuery(sql);
                dgvCategories.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 添加按钮点击事件
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 第一步：验证输入
            if (!ValidateInput()) return;

            string categoryId = txtCategoryID.Text.Trim();
            string categoryName = txtCategoryName.Text.Trim();

            try
            {
                // 第二步：检查分类ID是否已存在
                string checkSql = "SELECT COUNT(*) FROM Categories WHERE CategoryID = @CategoryID";
                SqlParameter[] checkParams = { new SqlParameter("@CategoryID", categoryId) };
                int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("分类ID已存在！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 第三步：检查分类名称是否已存在
                string checkNameSql = "SELECT COUNT(*) FROM Categories WHERE CategoryName = @CategoryName";
                SqlParameter[] checkNameParams = { new SqlParameter("@CategoryName", categoryName) };
                int nameCount = Convert.ToInt32(DBHelper.ExecuteScalar(checkNameSql, checkNameParams));

                if (nameCount > 0)
                {
                    MessageBox.Show("分类名称已存在！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 第四步：插入数据
                string sql = "INSERT INTO Categories (CategoryID, CategoryName) VALUES (@CategoryID, @CategoryName)";
                SqlParameter[] parameters = {
                    new SqlParameter("@CategoryID", categoryId),
                    new SqlParameter("@CategoryName", categoryName)
                };

                int result = DBHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("添加成功！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 修改按钮点击事件
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 第一步：检查是否选中数据
            if (dgvCategories.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要修改的分类！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 第二步：验证输入
            if (!ValidateInput()) return;

            try
            {
                // 第三步：更新数据
                string sql = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";
                SqlParameter[] parameters = {
                    new SqlParameter("@CategoryID", txtCategoryID.Text.Trim()),
                    new SqlParameter("@CategoryName", txtCategoryName.Text.Trim())
                };

                int result = DBHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("修改成功！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除按钮点击事件
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 第一步：检查是否选中数据
            if (dgvCategories.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要删除的分类！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string categoryId = dgvCategories.SelectedRows[0].Cells["分类ID"].Value.ToString();

            // 第二步：弹出确认框
            DialogResult result = MessageBox.Show("确定要删除这个分类吗？", "确认删除",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 第三步：检查该分类下是否有图书
                    string checkSql = "SELECT COUNT(*) FROM Books WHERE CategoryID = @CategoryID";
                    SqlParameter[] checkParams = { new SqlParameter("@CategoryID", categoryId) };
                    int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                    if (count > 0)
                    {
                        MessageBox.Show("该分类下有图书，无法删除！", "提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 第四步：执行删除
                    string sql = "DELETE FROM Categories WHERE CategoryID = @CategoryID";
                    SqlParameter[] parameters = { new SqlParameter("@CategoryID", categoryId) };
                    int deleteResult = DBHelper.ExecuteNonQuery(sql, parameters);

                    if (deleteResult > 0)
                    {
                        MessageBox.Show("删除成功！", "提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败：" + ex.Message, "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 刷新按钮点击事件
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadData();
        }

        /// <summary>
        /// 表格选中行改变事件
        /// </summary>
        private void dgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvCategories.SelectedRows[0];
                txtCategoryID.Text = row.Cells["分类ID"].Value.ToString();
                txtCategoryName.Text = row.Cells["分类名称"].Value.ToString();
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtCategoryID.Text.Trim()))
            {
                MessageBox.Show("分类ID不能为空！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryID.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtCategoryName.Text.Trim()))
            {
                MessageBox.Show("分类名称不能为空！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 清空输入
        /// </summary>
        private void ClearInputs()
        {
            txtCategoryID.Clear();
            txtCategoryName.Clear();
        }
    }
}
