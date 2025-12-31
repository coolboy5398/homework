using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using LibraryManagement.Utils;

namespace LibraryManagement.Forms
{
    /// <summary>
    /// 借阅查询窗体
    /// </summary>
    public partial class BorrowQueryForm : Form
    {
        public BorrowQueryForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void BorrowQueryForm_Load(object sender, EventArgs e)
        {
            // 初始化状态下拉框
            cmbStatus.Items.Add("全部");
            cmbStatus.Items.Add("未归还");
            cmbStatus.Items.Add("已归还");
            cmbStatus.SelectedIndex = 0;

            // 加载数据
            LoadData();
        }

        /// <summary>
        /// 加载借阅记录数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                string sql = @"SELECT br.BorrowID AS 借阅ID, br.ReaderID AS 读者证号, 
                              r.ReaderName AS 读者姓名, br.BookID AS 图书编号, 
                              b.BookName AS 书名, br.BorrowDate AS 借阅日期, 
                              br.DueDate AS 应还日期, br.ReturnDate AS 实还日期,
                              CASE WHEN br.ReturnDate IS NULL THEN '未归还' ELSE '已归还' END AS 状态
                              FROM BorrowRecords br
                              INNER JOIN Readers r ON br.ReaderID = r.ReaderID
                              INNER JOIN Books b ON br.BookID = b.BookID
                              ORDER BY br.BorrowID DESC";
                DataTable dt = DBHelper.ExecuteQuery(sql);
                dgvRecords.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 查询按钮点击事件
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string readerId = txtReaderID.Text.Trim();
                string bookId = txtBookID.Text.Trim();
                string status = cmbStatus.Text;

                string sql = @"SELECT br.BorrowID AS 借阅ID, br.ReaderID AS 读者证号, 
                              r.ReaderName AS 读者姓名, br.BookID AS 图书编号, 
                              b.BookName AS 书名, br.BorrowDate AS 借阅日期, 
                              br.DueDate AS 应还日期, br.ReturnDate AS 实还日期,
                              CASE WHEN br.ReturnDate IS NULL THEN '未归还' ELSE '已归还' END AS 状态
                              FROM BorrowRecords br
                              INNER JOIN Readers r ON br.ReaderID = r.ReaderID
                              INNER JOIN Books b ON br.BookID = b.BookID
                              WHERE 1=1";

                // 添加读者证号条件
                if (!string.IsNullOrEmpty(readerId))
                {
                    sql += " AND br.ReaderID LIKE @ReaderID";
                }

                // 添加图书编号条件
                if (!string.IsNullOrEmpty(bookId))
                {
                    sql += " AND br.BookID LIKE @BookID";
                }

                // 添加状态条件
                if (status == "未归还")
                {
                    sql += " AND br.ReturnDate IS NULL";
                }
                else if (status == "已归还")
                {
                    sql += " AND br.ReturnDate IS NOT NULL";
                }

                sql += " ORDER BY br.BorrowID DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@ReaderID", "%" + readerId + "%"),
                    new SqlParameter("@BookID", "%" + bookId + "%")
                };

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
                dgvRecords.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 查询逾期按钮点击事件
        /// </summary>
        private void btnOverdue_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = @"SELECT br.ReaderID AS 读者证号, r.ReaderName AS 读者姓名, 
                              br.BookID AS 图书编号, b.BookName AS 书名, 
                              br.BorrowDate AS 借阅日期, br.DueDate AS 应还日期,
                              DATEDIFF(DAY, br.DueDate, GETDATE()) AS 逾期天数
                              FROM BorrowRecords br
                              INNER JOIN Readers r ON br.ReaderID = r.ReaderID
                              INNER JOIN Books b ON br.BookID = b.BookID
                              WHERE br.ReturnDate IS NULL AND br.DueDate < GETDATE()
                              ORDER BY 逾期天数 DESC";

                DataTable dt = DBHelper.ExecuteQuery(sql);
                dgvRecords.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("没有逾期未还的图书！", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
            txtReaderID.Clear();
            txtBookID.Clear();
            cmbStatus.SelectedIndex = 0;
            LoadData();
        }
    }
}
