using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using LibraryManagement.Utils;

namespace LibraryManagement.Forms
{
    /// <summary>
    /// 图书归还窗体
    /// </summary>
    public partial class ReturnForm : Form
    {
        public ReturnForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void ReturnForm_Load(object sender, EventArgs e)
        {
            // 设置归还日期为今天
            dtpReturnDate.Value = DateTime.Now;
        }

        /// <summary>
        /// 归还按钮点击事件
        /// </summary>
        private void btnReturn_Click(object sender, EventArgs e)
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
                // 第二步：查询未归还的借阅记录
                string checkSql = @"SELECT BorrowID, DueDate FROM BorrowRecords 
                                   WHERE ReaderID = @ReaderID AND BookID = @BookID AND ReturnDate IS NULL";
                SqlParameter[] checkParams = {
                    new SqlParameter("@ReaderID", readerId),
                    new SqlParameter("@BookID", bookId)
                };
                DataTable dt = DBHelper.ExecuteQuery(checkSql, checkParams);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到借阅记录！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 第三步：获取借阅信息
                int borrowId = Convert.ToInt32(dt.Rows[0]["BorrowID"]);
                DateTime dueDate = Convert.ToDateTime(dt.Rows[0]["DueDate"]);
                DateTime returnDate = dtpReturnDate.Value.Date;

                // 第四步：更新借阅记录的归还日期
                string updateBorrowSql = "UPDATE BorrowRecords SET ReturnDate = @ReturnDate WHERE BorrowID = @BorrowID";
                SqlParameter[] updateBorrowParams = {
                    new SqlParameter("@ReturnDate", returnDate),
                    new SqlParameter("@BorrowID", borrowId)
                };
                DBHelper.ExecuteNonQuery(updateBorrowSql, updateBorrowParams);

                // 第五步：增加图书库存
                string updateStockSql = "UPDATE Books SET Stock = Stock + 1 WHERE BookID = @BookID";
                SqlParameter[] updateStockParams = { new SqlParameter("@BookID", bookId) };
                DBHelper.ExecuteNonQuery(updateStockSql, updateStockParams);

                // 第六步：检查是否逾期并显示提示
                if (returnDate > dueDate)
                {
                    int overdueDays = (returnDate - dueDate).Days;
                    MessageBox.Show("归还成功，逾期" + overdueDays + "天！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("归还成功！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // 清空输入
                txtReaderID.Clear();
                txtBookID.Clear();
                lblReaderName.Text = "";
                lblBookName.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("归还失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 读者证号文本框离开事件
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
        /// 图书编号文本框离开事件
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
                string sql = "SELECT BookName FROM Books WHERE BookID = @BookID";
                SqlParameter[] parameters = { new SqlParameter("@BookID", bookId) };
                object result = DBHelper.ExecuteScalar(sql, parameters);

                if (result != null)
                {
                    lblBookName.Text = result.ToString();
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
