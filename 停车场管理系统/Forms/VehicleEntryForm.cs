using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ParkingManagement.Utils;

namespace ParkingManagement.Forms
{
    /// <summary>
    /// 车辆进场窗体
    /// </summary>
    public partial class VehicleEntryForm : Form
    {
        public VehicleEntryForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void VehicleEntryForm_Load(object sender, EventArgs e)
        {
            // 加载空闲车位
            LoadFreeSpaces();
            
            // 显示当前时间
            dtpEntryTime.Value = DateTime.Now;
        }

        /// <summary>
        /// 加载空闲车位到下拉框
        /// </summary>
        private void LoadFreeSpaces()
        {
            try
            {
                string sql = "SELECT SpaceID, SpaceName + ' (' + SpaceType + ')' AS DisplayName FROM ParkingSpaces WHERE Status = '空闲' ORDER BY SpaceID";
                DataTable dt = DBHelper.ExecuteQuery(sql);

                cmbSpace.DataSource = dt;
                cmbSpace.DisplayMember = "DisplayName";
                cmbSpace.ValueMember = "SpaceID";

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("当前没有空闲车位！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载车位失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 进场按钮点击事件
        /// </summary>
        private void btnEntry_Click(object sender, EventArgs e)
        {
            // 第一步：获取输入
            string plateNumber = txtPlateNumber.Text.Trim().ToUpper();

            // 第二步：验证输入
            if (plateNumber == "")
            {
                MessageBox.Show("请输入车牌号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPlateNumber.Focus();
                return;
            }

            if (cmbSpace.SelectedValue == null)
            {
                MessageBox.Show("请选择车位！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string spaceId = cmbSpace.SelectedValue.ToString();
            DateTime entryTime = dtpEntryTime.Value;

            try
            {
                // 第三步：检查该车是否已在场内
                string checkSql = "SELECT COUNT(*) FROM ParkingRecords WHERE PlateNumber = @PlateNumber AND ExitTime IS NULL";
                SqlParameter[] checkParams = { new SqlParameter("@PlateNumber", plateNumber) };
                int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("该车辆已在场内，请先办理出场！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 第四步：插入停车记录
                string insertSql = "INSERT INTO ParkingRecords (PlateNumber, SpaceID, EntryTime) VALUES (@PlateNumber, @SpaceID, @EntryTime)";
                SqlParameter[] insertParams = {
                    new SqlParameter("@PlateNumber", plateNumber),
                    new SqlParameter("@SpaceID", spaceId),
                    new SqlParameter("@EntryTime", entryTime)
                };
                DBHelper.ExecuteNonQuery(insertSql, insertParams);

                // 第五步：更新车位状态为占用
                string updateSql = "UPDATE ParkingSpaces SET Status = '占用' WHERE SpaceID = @SpaceID";
                SqlParameter[] updateParams = { new SqlParameter("@SpaceID", spaceId) };
                DBHelper.ExecuteNonQuery(updateSql, updateParams);

                MessageBox.Show("车辆进场成功！\n车牌号：" + plateNumber + "\n进场时间：" + entryTime.ToString("yyyy-MM-dd HH:mm:ss"), 
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 清空输入，刷新车位
                txtPlateNumber.Clear();
                LoadFreeSpaces();
                txtPlateNumber.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("进场失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
