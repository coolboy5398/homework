using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using LibraryManagement.Utils;

namespace LibraryManagement.Forms
{
    /// <summary>
    /// 图书借阅窗体
    /// </summary>
    public partial class BorrowForm : Form
    {
        public BorrowForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void BorrowForm_Load(object sender, EventArgs e)
        {
            // 设置借阅日期为今天
            dtpBorrowDate.Value = DateTime.Now;
            
            // 设置应还日期为30天后
            dtpDueDate.Value = DateTime.Now.AddDays(30);
        }

        /// <summary>
        /// 借阅按钮点击事件
        /// </summary>
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            // 第一步：验证输入
            string readerId = txtReaderID.Text.Trim();
            string bookId = txtBookID.Text.Trim();

            if (string.IsNullOrEmpty(readerId))
            {
                MessageBox.Show("请输入读者证号！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReaderID.Focus();
                return;
            }

            if (string.IsNullOrEmpty(bookId))
            {
                MessageBox.Show("请输入图书编号！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookID.Focus();
                return;
            }

            try
            {
                // 第二步：检查读者是否存在
                string checkReaderSql = "SELECT COUNT(*) FROM Readers WHERE ReaderID = @ReaderID";
                SqlParameter[] readerParams = { new SqlParameter("@ReaderID", readerId) };
                int readerCount = Convert.ToInt32(DBHelper.ExecuteScalar(checkReaderSql, readerParams));

                if (readerCount == 0)
                {
                    MessageBox.Show("读者不存在！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 第三步：检查图书是否存在
                string checkBookSql = "SELECT Stock FROM Books WHERE BookID = @BookID";
                SqlParameter[] bookParams = { new SqlParameter("@BookID", bookId) };
                object stockObj = DBHelper.ExecuteScalar(checkBookSql, bookParams);

                if (stockObj == null)
                {
                    MessageBox.Show("图书不存在！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 第四步：检查库存是否充足
                int stock = Convert.ToInt32(stockObj);
                if (stock <= 0)
                {
                    MessageBox.Show("图书库存不足！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 第五步：创建借阅记录
                string insertSql = @"INSERT INTO BorrowRecords (ReaderID, BookID, BorrowDate, DueDate)
                                    VALUES (@ReaderID, @BookID, @BorrowDate, @DueDate)";
                SqlParameter[] insertParams = {
                    new SqlParameter("@ReaderID", readerId),
                    new SqlParameter("@BookID", bookId),
                    new SqlParameter("@BorrowDate", dtpBorrowDate.Value.Date),
                    new SqlParameter("@DueDate", dtpDueDate.Value.Date)
                };
                DBHelper.ExecuteNonQuery(insertSql, insertParams);

                // 第六步：减少图书库存
                string updateSql = "UPDATE Books SET Stock = Stock - 1 WHERE BookID = @BookID";
                SqlParameter[] updateParams = { new SqlParameter("@BookID", bookId) };
                DBHelper.ExecuteNonQuery(updateSql, updateParams);

                MessageBox.Show("借阅成功！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 清空输入
                txtReaderID.Clear();
                txtBookID.Clear();
                lblReaderName.Text = "";
                lblBookName.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("借阅失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 读者证号文本框离开事件 - 自动查询读者姓名
        /// </summary>
        private void txtReaderID_Leave(object sender, EventArgs e)
        {
            string readerId = txtReaderID.Text.Trim();
            if (string.IsNullOrEmpty(readerId))
            {
                lblReaderName.Text = "";
                return;
            }

            try
            {
                string sql = "SELECT ReaderName FROM Readers WHERE ReaderID = @ReaderID";
                SqlParameter[] parameters = { new SqlParameter("@ReaderID", readerId) };
                object result = DBHelper.ExecuteScalar(sql, parameters);

                if (result != null)
                {
                    lblReaderName.Text = result.ToString();
                }
                else
                {
                    lblReaderName.Text = "（未找到）";
                }
            }
            catch
            {
                lblReaderName.Text = "";
            }
        }

        /// <summary>
        /// 图书编号文本框离开事件 - 自动查询书名和库存
        /// </summary>
        private void txtBookID_Leave(object sender, EventArgs e)
        {
            string bookId = txtBookID.Text.Trim();
            if (string.IsNullOrEmpty(bookId))
            {
                lblBookName.Text = "";
                return;
            }

            try
            {
                string sql = "SELECT BookName, Stock FROM Books WHERE BookID = @BookID";
                SqlParameter[] parameters = { new SqlParameter("@BookID", bookId) };
                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);

                if (dt.Rows.Count > 0)
                {
                    string bookName = dt.Rows[0]["BookName"].ToString();
                    int stock = Convert.ToInt32(dt.Rows[0]["Stock"]);
                    lblBookName.Text = bookName + "（库存：" + stock + "）";
                }
                else
                {
                    lblBookName.Text = "（未找到）";
                }
            }
            catch
            {
                lblBookName.Text = "";
            }
        }
    }
}
