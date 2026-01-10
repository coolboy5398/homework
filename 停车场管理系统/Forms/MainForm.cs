using System;
using System.Data;
using System.Windows.Forms;
using ParkingManagement.Utils;

namespace ParkingManagement.Forms
{
    /// <summary>
    /// 主窗体 - 显示菜单和车位概况
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // 显示欢迎信息
            lblWelcome.Text = "欢迎您，" + UserSession.UserName + "！";
            
            // 加载车位概况
            LoadParkingOverview();
        }

        /// <summary>
        /// 加载车位概况
        /// </summary>
        private void LoadParkingOverview()
        {
            try
            {
                // 1. 查询总车位数
                string sqlTotal = "SELECT COUNT(*) FROM ParkingSpaces";
                int totalSpaces = Convert.ToInt32(DBHelper.ExecuteScalar(sqlTotal));

                // 2. 查询空闲车位数
                string sqlFree = "SELECT COUNT(*) FROM ParkingSpaces WHERE Status = '空闲'";
                int freeSpaces = Convert.ToInt32(DBHelper.ExecuteScalar(sqlFree));

                // 3. 计算占用车位数
                int usedSpaces = totalSpaces - freeSpaces;

                // 4. 更新显示
                lblTotalSpaces.Text = totalSpaces.ToString();
                lblFreeSpaces.Text = freeSpaces.ToString();
                lblUsedSpaces.Text = usedSpaces.ToString();

                // 5. 查询今日收入
                string sqlIncome = @"SELECT ISNULL(SUM(Fee), 0) FROM ParkingRecords 
                                     WHERE CONVERT(DATE, ExitTime) = CONVERT(DATE, GETDATE())";
                decimal todayIncome = Convert.ToDecimal(DBHelper.ExecuteScalar(sqlIncome));
                lblTodayIncome.Text = todayIncome.ToString("F2") + " 元";
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 车位管理菜单
        /// </summary>
        private void menuParkingSpace_Click(object sender, EventArgs e)
        {
            ParkingSpaceForm form = new ParkingSpaceForm();
            form.ShowDialog();
            LoadParkingOverview();
        }

        /// <summary>
        /// 车辆进场菜单
        /// </summary>
        private void menuVehicleEntry_Click(object sender, EventArgs e)
        {
            VehicleEntryForm form = new VehicleEntryForm();
            form.ShowDialog();
            LoadParkingOverview();
        }

        /// <summary>
        /// 车辆出场菜单
        /// </summary>
        private void menuVehicleExit_Click(object sender, EventArgs e)
        {
            VehicleExitForm form = new VehicleExitForm();
            form.ShowDialog();
            LoadParkingOverview();
        }

        /// <summary>
        /// 记录查询菜单
        /// </summary>
        private void menuRecordQuery_Click(object sender, EventArgs e)
        {
            RecordQueryForm form = new RecordQueryForm();
            form.ShowDialog();
        }

        /// <summary>
        /// 退出登录菜单
        /// </summary>
        private void menuLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要退出登录吗？", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                UserSession.Logout();
                
                // 显示登录窗体
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                
                // 关闭当前窗体
                this.Close();
            }
        }

        /// <summary>
        /// 退出系统菜单
        /// </summary>
        private void menuExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定要退出系统吗？", "确认", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// 刷新按钮点击事件
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadParkingOverview();
            MessageBox.Show("刷新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
