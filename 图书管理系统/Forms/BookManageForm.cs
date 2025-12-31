using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using LibraryManagement.Utils;

namespace LibraryManagement.Forms
{
    /// <summary>
    /// 图书管理窗体
    /// </summary>
    public partial class BookManageForm : Form
    {
        public BookManageForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void BookManageForm_Load(object sender, EventArgs e)
        {
            // 加载分类下拉框
            LoadCategories();
            
            // 加载图书数据
            LoadData();
        }

        /// <summary>
        /// 加载分类下拉框
        /// </summary>
        private void LoadCategories()
        {
            try
            {
                string sql = "SELECT CategoryID, CategoryName FROM Categories ORDER BY CategoryID";
                DataTable dt = DBHelper.ExecuteQuery(sql);

                // 添加"全部"选项
                DataRow allRow = dt.NewRow();
                allRow["CategoryID"] = "";
                allRow["CategoryName"] = "全部";
                dt.Rows.InsertAt(allRow, 0);

                // 绑定到下拉框
                cmbCategory.DataSource = dt;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryID";
                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载分类失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载图书数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                string sql = @"SELECT b.BookID AS 图书编号, b.BookName AS 书名, 
                              b.Author AS 作者, b.Publisher AS 出版社,
                              c.CategoryName AS 分类, b.Price AS 价格, b.Stock AS 库存
                              FROM Books b
                              LEFT JOIN Categories c ON b.CategoryID = c.CategoryID
                              ORDER BY b.BookID";
                DataTable dt = DBHelper.ExecuteQuery(sql);
                dgvBooks.DataSource = dt;
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

            string bookId = txtBookID.Text.Trim();

            try
            {
                // 第二步：检查图书编号是否已存在
                string checkSql = "SELECT COUNT(*) FROM Books WHERE BookID = @BookID";
                SqlParameter[] checkParams = { new SqlParameter("@BookID", bookId) };
                int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("图书编号已存在！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 第三步：插入数据
                string sql = @"INSERT INTO Books (BookID, BookName, Author, Publisher, CategoryID, Price, Stock)
                              VALUES (@BookID, @BookName, @Author, @Publisher, @CategoryID, @Price, @Stock)";
                SqlParameter[] parameters = {
                    new SqlParameter("@BookID", bookId),
                    new SqlParameter("@BookName", txtBookName.Text.Trim()),
                    new SqlParameter("@Author", txtAuthor.Text.Trim()),
                    new SqlParameter("@Publisher", txtPublisher.Text.Trim()),
                    new SqlParameter("@CategoryID", cmbCategory.SelectedValue.ToString()),
                    new SqlParameter("@Price", decimal.Parse(txtPrice.Text.Trim())),
                    new SqlParameter("@Stock", int.Parse(txtStock.Text.Trim()))
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
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要修改的图书！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 第二步：验证输入
            if (!ValidateInput()) return;

            try
            {
                // 第三步：更新数据
                string sql = @"UPDATE Books SET BookName = @BookName, Author = @Author, 
                              Publisher = @Publisher, CategoryID = @CategoryID, 
                              Price = @Price, Stock = @Stock
                              WHERE BookID = @BookID";
                SqlParameter[] parameters = {
                    new SqlParameter("@BookID", txtBookID.Text.Trim()),
                    new SqlParameter("@BookName", txtBookName.Text.Trim()),
                    new SqlParameter("@Author", txtAuthor.Text.Trim()),
                    new SqlParameter("@Publisher", txtPublisher.Text.Trim()),
                    new SqlParameter("@CategoryID", cmbCategory.SelectedValue.ToString()),
                    new SqlParameter("@Price", decimal.Parse(txtPrice.Text.Trim())),
                    new SqlParameter("@Stock", int.Parse(txtStock.Text.Trim()))
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
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要删除的图书！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string bookId = dgvBooks.SelectedRows[0].Cells["图书编号"].Value.ToString();

            // 第二步：弹出确认框
            DialogResult result = MessageBox.Show("确定要删除这本图书吗？", "确认删除",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 第三步：检查是否有未归还的借阅记录
                    string checkSql = @"SELECT COUNT(*) FROM BorrowRecords 
                                       WHERE BookID = @BookID AND ReturnDate IS NULL";
                    SqlParameter[] checkParams = { new SqlParameter("@BookID", bookId) };
                    int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                    if (count > 0)
                    {
                        MessageBox.Show("该图书有未归还记录，无法删除！", "提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 第四步：执行删除
                    string sql = "DELETE FROM Books WHERE BookID = @BookID";
                    SqlParameter[] parameters = { new SqlParameter("@BookID", bookId) };
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
                string categoryId = cmbSearchCategory.SelectedValue.ToString();

                string sql = @"SELECT b.BookID AS 图书编号, b.BookName AS 书名, 
                              b.Author AS 作者, b.Publisher AS 出版社,
                              c.CategoryName AS 分类, b.Price AS 价格, b.Stock AS 库存
                              FROM Books b
                              LEFT JOIN Categories c ON b.CategoryID = c.CategoryID
                              WHERE 1=1";

                // 添加关键字条件
                if (!string.IsNullOrEmpty(keyword))
                {
                    sql += " AND (b.BookID LIKE @Keyword OR b.BookName LIKE @Keyword OR b.Author LIKE @Keyword)";
                }

                // 添加分类条件
                if (!string.IsNullOrEmpty(categoryId))
                {
                    sql += " AND b.CategoryID = @CategoryID";
                }

                sql += " ORDER BY b.BookID";

                SqlParameter[] parameters = {
                    new SqlParameter("@Keyword", "%" + keyword + "%"),
                    new SqlParameter("@CategoryID", categoryId)
                };

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
                dgvBooks.DataSource = dt;
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
            cmbSearchCategory.SelectedIndex = 0;
            LoadData();
        }


        /// <summary>
        /// 表格选中行改变事件
        /// </summary>
        private void dgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvBooks.SelectedRows[0];
                txtBookID.Text = row.Cells["图书编号"].Value.ToString();
                txtBookName.Text = row.Cells["书名"].Value.ToString();
                txtAuthor.Text = row.Cells["作者"].Value?.ToString();
                txtPublisher.Text = row.Cells["出版社"].Value?.ToString();
                txtPrice.Text = row.Cells["价格"].Value?.ToString();
                txtStock.Text = row.Cells["库存"].Value?.ToString();

                // 设置分类下拉框
                string categoryName = row.Cells["分类"].Value?.ToString();
                for (int i = 0; i < cmbCategory.Items.Count; i++)
                {
                    DataRowView item = cmbCategory.Items[i] as DataRowView;
                    if (item != null && item["CategoryName"].ToString() == categoryName)
                    {
                        cmbCategory.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtBookID.Text.Trim()))
            {
                MessageBox.Show("图书编号不能为空！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookID.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtBookName.Text.Trim()))
            {
                MessageBox.Show("书名不能为空！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookName.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 清空输入
        /// </summary>
        private void ClearInputs()
        {
            txtBookID.Clear();
            txtBookName.Clear();
            txtAuthor.Clear();
            txtPublisher.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            cmbCategory.SelectedIndex = 0;
        }
    }
}
