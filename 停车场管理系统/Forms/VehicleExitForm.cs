using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ParkingManagement.Utils;

namespace ParkingManagement.Forms
{
    /// <summary>
    /// 车辆出场窗体
    /// </summary>
    public partial class VehicleExitForm : Form
    {
        // 当前查询到的停车记录ID
        private int currentRecordId = 0;
        // 当前车位ID
        private string currentSpaceId = "";

        public VehicleExitForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void VehicleExitForm_Load(object sender, EventArgs e)
        {
            // 设置出场时间为当前时间
            dtpExitTime.Value = DateTime.Now;
            
            // 清空显示
            ClearDisplay();
        }

        /// <summary>
        /// 查询按钮点击事件
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 第一步：获取车牌号
            string plateNumber = txtPlateNumber.Text.Trim().ToUpper();

            if (plateNumber == "")
            {
                MessageBox.Show("请输入车牌号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPlateNumber.Focus();
                return;
            }

            try
            {
                // 第二步：查询该车的在场记录
                string sql = @"SELECT r.RecordID, r.PlateNumber, r.SpaceID, s.SpaceName, s.SpaceType, r.EntryTime 
                               FROM ParkingRecords r 
                               INNER JOIN ParkingSpaces s ON r.SpaceID = s.SpaceID 
                               WHERE r.PlateNumber = @PlateNumber AND r.ExitTime IS NULL";
                SqlParameter[] parameters = { new SqlParameter("@PlateNumber", plateNumber) };
                DataTable dt = DBHelper.ExecuteQuery(sql, parameters);

                // 第三步：判断是否找到记录
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到该车辆的在场记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearDisplay();
                    return;
                }

                // 第四步：显示车辆信息
                DataRow row = dt.Rows[0];
                currentRecordId = Convert.ToInt32(row["RecordID"]);
                currentSpaceId = row["SpaceID"].ToString();
                DateTime entryTime = Convert.ToDateTime(row["EntryTime"]);

                lblPlateNumberValue.Text = row["PlateNumber"].ToString();
                lblSpaceValue.Text = row["SpaceName"].ToString() + " (" + row["SpaceType"].ToString() + ")";
                lblEntryTimeValue.Text = entryTime.ToString("yyyy-MM-dd HH:mm:ss");

                // 第五步：计算停车时长和费用
                CalculateFee(entryTime);

                // 启用出场按钮
                btnExit.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 计算停车费用
        /// </summary>
        private void CalculateFee(DateTime entryTime)
        {
            DateTime exitTime = dtpExitTime.Value;

            // 第一步：计算停车时长（小时）
            TimeSpan duration = exitTime - entryTime;
            double totalHours = duration.TotalHours;

            // 不足1小时按1小时算
            int hours = (int)Math.Ceiling(totalHours);
            if (hours < 1)
            {
                hours = 1;
            }

            // 显示停车时长
            lblDurationValue.Text = hours + " 小时";

            // 第二步：获取收费标准（默认小型车）
            decimal hourlyRate = 5.00m;
            decimal dailyMax = 50.00m;

            try
            {
                string sql = "SELECT HourlyRate, DailyMax FROM PriceRules WHERE VehicleType = '小型车'";
                DataTable dt = DBHelper.ExecuteQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    hourlyRate = Convert.ToDecimal(dt.Rows[0]["HourlyRate"]);
                    dailyMax = Convert.ToDecimal(dt.Rows[0]["DailyMax"]);
                }
            }
            catch
            {
                // 使用默认值
            }

            // 第三步：计算费用
            decimal fee = hours * hourlyRate;

            // 计算天数，每天有封顶
            int days = (int)Math.Ceiling(totalHours / 24);
            if (days < 1)
            {
                days = 1;
            }
            decimal maxFee = days * dailyMax;

            // 取较小值
            if (fee > maxFee)
            {
                fee = maxFee;
            }

            // 显示费用
            lblFeeValue.Text = fee.ToString("F2") + " 元";
        }

        /// <summary>
        /// 出场时间改变事件
        /// </summary>
        private void dtpExitTime_ValueChanged(object sender, EventArgs e)
        {
            // 如果已经查询到记录，重新计算费用
            if (currentRecordId > 0)
            {
                string entryTimeStr = lblEntryTimeValue.Text;
                if (entryTimeStr != "-")
                {
                    DateTime entryTime = DateTime.Parse(entryTimeStr);
                    CalculateFee(entryTime);
                }
            }
        }

        /// <summary>
        /// 出场按钮点击事件
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (currentRecordId == 0)
            {
                MessageBox.Show("请先查询车辆信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 确认出场
            DialogResult result = MessageBox.Show(
                "确认车辆出场？\n\n车牌号：" + lblPlateNumberValue.Text + 
                "\n停车时长：" + lblDurationValue.Text + 
                "\n应收费用：" + lblFeeValue.Text,
                "确认出场", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 第一步：获取费用
                    string feeStr = lblFeeValue.Text.Replace(" 元", "");
                    decimal fee = decimal.Parse(feeStr);
                    DateTime exitTime = dtpExitTime.Value;

                    // 第二步：更新停车记录
                    string updateRecordSql = "UPDATE ParkingRecords SET ExitTime = @ExitTime, Fee = @Fee WHERE RecordID = @RecordID";
                    SqlParameter[] recordParams = {
                        new SqlParameter("@ExitTime", exitTime),
                        new SqlParameter("@Fee", fee),
                        new SqlParameter("@RecordID", currentRecordId)
                    };
                    DBHelper.ExecuteNonQuery(updateRecordSql, recordParams);

                    // 第三步：更新车位状态为空闲
                    string updateSpaceSql = "UPDATE ParkingSpaces SET Status = '空闲' WHERE SpaceID = @SpaceID";
                    SqlParameter[] spaceParams = { new SqlParameter("@SpaceID", currentSpaceId) };
                    DBHelper.ExecuteNonQuery(updateSpaceSql, spaceParams);

                    MessageBox.Show("车辆出场成功！\n收费：" + fee.ToString("F2") + " 元", 
                        "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 清空显示
                    txtPlateNumber.Clear();
                    ClearDisplay();
                    txtPlateNumber.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("出场失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 清空显示
        /// </summary>
        private void ClearDisplay()
        {
            currentRecordId = 0;
            currentSpaceId = "";
            lblPlateNumberValue.Text = "-";
            lblSpaceValue.Text = "-";
            lblEntryTimeValue.Text = "-";
            lblDurationValue.Text = "-";
            lblFeeValue.Text = "-";
            btnExit.Enabled = false;
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
