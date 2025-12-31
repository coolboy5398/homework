using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using LibraryManagement.Utils;

namespace LibraryManagement.Forms
{
    /// <summary>
    /// 借阅统计窗体
    /// </summary>
    public partial class StatisticsForm : Form
    {
        public StatisticsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void StatisticsForm_Load(object sender, EventArgs e)
        {
            // 设置默认日期范围（最近一年）
            dtpStartDate.Value = DateTime.Now.AddYears(-1);
            dtpEndDate.Value = DateTime.Now;

            // 加载统计数据
            LoadStatistics();
        }

        /// <summary>
        /// 加载借阅统计数据
        /// </summary>
        private void LoadStatistics()
        {
            try
            {
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date;

                string sql = @"SELECT b.BookID AS 图书编号, b.BookName AS 书名, 
                              b.Author AS 作者, c.CategoryName AS 分类,
                              COUNT(br.BorrowID) AS 借阅次数
                              FROM Books b
                              LEFT JOIN Categories c ON b.CategoryID = c.CategoryID
                              LEFT JOIN BorrowRecords br ON b.BookID = br.BookID 
                                  AND br.BorrowDate >= @StartDate AND br.BorrowDate <= @EndDate
                              GROUP BY b.BookID, b.BookName, b.Author, c.CategoryName
                              ORDER BY 借阅次数 DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@StartDate", startDate),
                    new SqlParameter("@EndDate", endDate)
                };

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
                dgvStatistics.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载统计数据失败：" + ex.Message, "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 统计按钮点击事件
        /// </summary>
        private void btnStatistics_Click(object sender, EventArgs e)
        {
            // 验证日期范围
            if (dtpStartDate.Value.Date > dtpEndDate.Value.Date)
            {
                MessageBox.Show("开始日期不能大于结束日期！", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadStatistics();
        }
    }
}
