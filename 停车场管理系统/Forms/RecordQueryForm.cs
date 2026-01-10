using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ParkingManagement.Utils;

namespace ParkingManagement.Forms
{
    /// <summary>
    /// 停车记录查询窗体
    /// </summary>
    public partial class RecordQueryForm : Form
    {
        public RecordQueryForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void RecordQueryForm_Load(object sender, EventArgs e)
        {
            // 设置默认日期范围（最近7天）
            dtpEndDate.Value = DateTime.Now;
            dtpStartDate.Value = DateTime.Now.AddDays(-7);

            // 加载所有记录
            LoadData();

            // 加载统计信息
            LoadStatistics();
        }

        /// <summary>
        /// 加载停车记录
        /// </summary>
        private void LoadData()
        {
            try
            {
                string sql = @"SELECT 
                    r.RecordID AS 记录ID,
                    r.PlateNumber AS 车牌号,
                    s.SpaceName AS 车位,
                    r.EntryTime AS 进场时间,
                    r.ExitTime AS 出场时间,
                    CASE WHEN r.ExitTime IS NULL THEN '在场' ELSE '已离场' END AS 状态,
                    ISNULL(r.Fee, 0) AS 费用
                FROM ParkingRecords r
                INNER JOIN ParkingSpaces s ON r.SpaceID = s.SpaceID
                WHERE r.EntryTime >= @StartDate AND r.EntryTime <= @EndDate
                ORDER BY r.EntryTime DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@StartDate", dtpStartDate.Value.Date),
                    new SqlParameter("@EndDate", dtpEndDate.Value.Date.AddDays(1))
                };

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
                dgvRecords.DataSource = dt;

                // 设置列宽
                if (dgvRecords.Columns.Count > 0)
                {
                    dgvRecords.Columns["记录ID"].Width = 60;
                    dgvRecords.Columns["车牌号"].Width = 100;
                    dgvRecords.Columns["车位"].Width = 100;
                    dgvRecords.Columns["进场时间"].Width = 140;
                    dgvRecords.Columns["出场时间"].Width = 140;
                    dgvRecords.Columns["状态"].Width = 70;
                    dgvRecords.Columns["费用"].Width = 80;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载统计信息
        /// </summary>
        private void LoadStatistics()
        {
            try
            {
                // 1. 查询总记录数
                string sqlTotal = @"SELECT COUNT(*) FROM ParkingRecords 
                                    WHERE EntryTime >= @StartDate AND EntryTime <= @EndDate";
                SqlParameter[] totalParams = {
                    new SqlParameter("@StartDate", dtpStartDate.Value.Date),
                    new SqlParameter("@EndDate", dtpEndDate.Value.Date.AddDays(1))
                };
                int totalCount = Convert.ToInt32(DBHelper.ExecuteScalar(sqlTotal, totalParams));

                // 2. 查询总收入
                string sqlIncome = @"SELECT ISNULL(SUM(Fee), 0) FROM ParkingRecords 
                                     WHERE EntryTime >= @StartDate AND EntryTime <= @EndDate AND ExitTime IS NOT NULL";
                SqlParameter[] incomeParams = {
                    new SqlParameter("@StartDate", dtpStartDate.Value.Date),
                    new SqlParameter("@EndDate", dtpEndDate.Value.Date.AddDays(1))
                };
                decimal totalIncome = Convert.ToDecimal(DBHelper.ExecuteScalar(sqlIncome, incomeParams));

                // 3. 查询当前在场车辆数
                string sqlInPark = "SELECT COUNT(*) FROM ParkingRecords WHERE ExitTime IS NULL";
                int inParkCount = Convert.ToInt32(DBHelper.ExecuteScalar(sqlInPark));

                // 4. 更新显示
                lblTotalCount.Text = totalCount.ToString();
                lblTotalIncome.Text = totalIncome.ToString("F2") + " 元";
                lblInParkCount.Text = inParkCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载统计失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 查询按钮点击事件
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 获取车牌号
            string plateNumber = txtPlateNumber.Text.Trim().ToUpper();

            try
            {
                string sql = @"SELECT 
                    r.RecordID AS 记录ID,
                    r.PlateNumber AS 车牌号,
                    s.SpaceName AS 车位,
                    r.EntryTime AS 进场时间,
                    r.ExitTime AS 出场时间,
                    CASE WHEN r.ExitTime IS NULL THEN '在场' ELSE '已离场' END AS 状态,
                    ISNULL(r.Fee, 0) AS 费用
                FROM ParkingRecords r
                INNER JOIN ParkingSpaces s ON r.SpaceID = s.SpaceID
                WHERE r.EntryTime >= @StartDate AND r.EntryTime <= @EndDate";

                // 如果输入了车牌号，添加筛选条件
                if (plateNumber != "")
                {
                    sql += " AND r.PlateNumber LIKE @PlateNumber";
                }

                sql += " ORDER BY r.EntryTime DESC";

                SqlParameter[] parameters;
                if (plateNumber != "")
                {
                    parameters = new SqlParameter[] {
                        new SqlParameter("@StartDate", dtpStartDate.Value.Date),
                        new SqlParameter("@EndDate", dtpEndDate.Value.Date.AddDays(1)),
                        new SqlParameter("@PlateNumber", "%" + plateNumber + "%")
                    };
                }
                else
                {
                    parameters = new SqlParameter[] {
                        new SqlParameter("@StartDate", dtpStartDate.Value.Date),
                        new SqlParameter("@EndDate", dtpEndDate.Value.Date.AddDays(1))
                    };
                }

                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);
                dgvRecords.DataSource = dt;

                // 更新统计
                LoadStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 刷新按钮点击事件
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtPlateNumber.Clear();
            LoadData();
            LoadStatistics();
        }

        /// <summary>
        /// 只看在场车辆复选框
        /// </summary>
        private void chkInParkOnly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkInParkOnly.Checked)
                {
                    // 只显示在场车辆
                    string sql = @"SELECT 
                        r.RecordID AS 记录ID,
                        r.PlateNumber AS 车牌号,
                        s.SpaceName AS 车位,
                        r.EntryTime AS 进场时间,
                        r.ExitTime AS 出场时间,
                        '在场' AS 状态,
                        0 AS 费用
                    FROM ParkingRecords r
                    INNER JOIN ParkingSpaces s ON r.SpaceID = s.SpaceID
                    WHERE r.ExitTime IS NULL
                    ORDER BY r.EntryTime DESC";

                    DataTable dt = DBHelper.ExecuteQuery(sql);
                    dgvRecords.DataSource = dt;
                }
                else
                {
                    // 显示所有记录
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
