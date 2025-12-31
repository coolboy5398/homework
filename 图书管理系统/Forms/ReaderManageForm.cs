using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using LibraryManagement.Utils;

namespace LibraryManagement.Forms
{
    /// <summary>
    /// 读者管理窗体
    /// </summary>
    public partial class ReaderManageForm : Form
    {
        public ReaderManageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void ReaderManageForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 加载读者数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                string sql = @"SELECT ReaderID AS 读者证号, ReaderName AS 姓名, 
                              Gender AS 性别, Phone AS 联系方式, 
                              RegisterDate AS 注册日期
                              FROM Readers ORDER BY ReaderID";
                DataTable dt = DBHelper.ExecuteQuery(sql);
                dgvReaders.DataSource = dt;
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

            string readerId = txtReaderID.Text.Trim();

            try
            {
                // 第二步：检查读者证号是否已存在
                string checkSql = "SELECT COUNT(*) FROM Readers WHERE ReaderID = @ReaderID";
                SqlParameter[] checkParams = { new SqlParameter("@ReaderID", readerId) };
                int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("读者证号已存在！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 第三步：插入数据
                string sql = @"INSERT INTO Readers (ReaderID, ReaderName, Gender, Phone, RegisterDate)
                              VALUES (@ReaderID, @ReaderName, @Gender, @Phone, @RegisterDate)";
                SqlParameter[] parameters = {
                    new SqlParameter("@ReaderID", readerId),
                    new SqlParameter("@ReaderName", txtReaderName.Text.Trim()),
                    new SqlParameter("@Gender", cmbGender.Text),
                    new SqlParameter("@Phone", txtPhone.Text.Trim()),
                    new SqlParameter("@RegisterDate", DateTime.Now)
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
            if (dgvReaders.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要修改的读者！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 第二步：验证输入
            if (!ValidateInput()) return;

            try
            {
                // 第三步：更新数据
                string sql = @"UPDATE Readers SET ReaderName = @ReaderName, 
                              Gender = @Gender, Phone = @Phone
                              WHERE ReaderID = @ReaderID";
                SqlParameter[] parameters = {
                    new SqlParameter("@ReaderID", txtReaderID.Text.Trim()),
                    new SqlParameter("@ReaderName", txtReaderName.Text.Trim()),
                    new SqlParameter("@Gender", cmbGender.Text),
                    new SqlParameter("@Phone", txtPhone.Text.Trim())
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
            if (dgvReaders.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要删除的读者！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string readerId = dgvReaders.SelectedRows[0].Cells["读者证号"].Value.ToString();

            // 第二步：弹出确认框
            DialogResult result = MessageBox.Show("确定要删除这个读者吗？", "确认删除",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 第三步：检查是否有未归还的借阅记录
                    string checkSql = @"SELECT COUNT(*) FROM BorrowRecords 
                                       WHERE ReaderID = @ReaderID AND ReturnDate IS NULL";
                    SqlParameter[] checkParams = { new SqlParameter("@ReaderID", readerId) };
                    int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                    if (count > 0)
                    {
                        MessageBox.Show("该读者有未归还图书，无法删除！", "提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 第四步：执行删除
                    string sql = "DELETE FROM Readers WHERE ReaderID = @ReaderID";
                    SqlParameter[] parameters = { new SqlParameter("@ReaderID", readerId) };
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
        /// 查询按钮点击事件
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();

                string sql = @"SELECT ReaderID AS 读者证号, ReaderName AS 姓名, 
                              Gender AS 性别, Phone AS 联系方式, 
                              RegisterDate AS 注册日期
                              FROM Readers
                              WHERE ReaderID LIKE @Keyword OR ReaderName LIKE @Keyword
                              ORDER BY ReaderID";

                SqlParameter[] parameters = {
                    new SqlParameter("@Keyword", "%" + keyword + "%")
                };

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
                dgvReaders.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 刷新按钮点击事件
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearInputs();
            txtSearch.Clear();
            LoadData();
        }

        /// <summary>
        /// 表格选中行改变事件
        /// </summary>
        private void dgvReaders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReaders.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvReaders.SelectedRows[0];
                txtReaderID.Text = row.Cells["读者证号"].Value.ToString();
                txtReaderName.Text = row.Cells["姓名"].Value.ToString();
                cmbGender.Text = row.Cells["性别"].Value?.ToString();
                txtPhone.Text = row.Cells["联系方式"].Value?.ToString();
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtReaderID.Text.Trim()))
            {
                MessageBox.Show("读者证号不能为空！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReaderID.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtReaderName.Text.Trim()))
            {
                MessageBox.Show("姓名不能为空！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReaderName.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 清空输入
        /// </summary>
        private void ClearInputs()
        {
            txtReaderID.Clear();
            txtReaderName.Clear();
            cmbGender.SelectedIndex = -1;
            txtPhone.Clear();
        }
    }
}
