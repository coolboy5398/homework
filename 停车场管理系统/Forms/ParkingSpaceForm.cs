using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ParkingManagement.Utils;

namespace ParkingManagement.Forms
{
    /// <summary>
    /// 车位管理窗体
    /// </summary>
    public partial class ParkingSpaceForm : Form
    {
        public ParkingSpaceForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void ParkingSpaceForm_Load(object sender, EventArgs e)
        {
            // 初始化车位类型下拉框
            cmbSpaceType.Items.Clear();
            cmbSpaceType.Items.Add("小型车位");
            cmbSpaceType.Items.Add("大型车位");
            cmbSpaceType.SelectedIndex = 0;

            // 加载车位数据
            LoadData();
        }

        /// <summary>
        /// 加载车位数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                string sql = "SELECT SpaceID AS 车位编号, SpaceName AS 车位名称, SpaceType AS 车位类型, Status AS 状态 FROM ParkingSpaces ORDER BY SpaceID";
                DataTable dt = DBHelper.ExecuteQuery(sql);
                dgvSpaces.DataSource = dt;

                // 设置列宽
                if (dgvSpaces.Columns.Count > 0)
                {
                    dgvSpaces.Columns["车位编号"].Width = 100;
                    dgvSpaces.Columns["车位名称"].Width = 120;
                    dgvSpaces.Columns["车位类型"].Width = 100;
                    dgvSpaces.Columns["状态"].Width = 80;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 添加按钮点击事件
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 第一步：获取输入
            string spaceId = txtSpaceID.Text.Trim();
            string spaceName = txtSpaceName.Text.Trim();
            string spaceType = cmbSpaceType.Text;

            // 第二步：验证输入
            if (spaceId == "")
            {
                MessageBox.Show("请输入车位编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSpaceID.Focus();
                return;
            }

            if (spaceName == "")
            {
                MessageBox.Show("请输入车位名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSpaceName.Focus();
                return;
            }

            try
            {
                // 第三步：检查车位编号是否已存在
                string checkSql = "SELECT COUNT(*) FROM ParkingSpaces WHERE SpaceID = @SpaceID";
                SqlParameter[] checkParams = { new SqlParameter("@SpaceID", spaceId) };
                int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("车位编号已存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 第四步：插入数据
                string sql = "INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES (@SpaceID, @SpaceName, @SpaceType, '空闲')";
                SqlParameter[] parameters = {
                    new SqlParameter("@SpaceID", spaceId),
                    new SqlParameter("@SpaceName", spaceName),
                    new SqlParameter("@SpaceType", spaceType)
                };

                int result = DBHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 修改按钮点击事件
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 第一步：检查是否选中
            if (dgvSpaces.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要修改的车位！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 第二步：获取输入
            string spaceId = txtSpaceID.Text.Trim();
            string spaceName = txtSpaceName.Text.Trim();
            string spaceType = cmbSpaceType.Text;

            // 第三步：验证输入
            if (spaceName == "")
            {
                MessageBox.Show("请输入车位名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSpaceName.Focus();
                return;
            }

            try
            {
                // 第四步：更新数据
                string sql = "UPDATE ParkingSpaces SET SpaceName = @SpaceName, SpaceType = @SpaceType WHERE SpaceID = @SpaceID";
                SqlParameter[] parameters = {
                    new SqlParameter("@SpaceName", spaceName),
                    new SqlParameter("@SpaceType", spaceType),
                    new SqlParameter("@SpaceID", spaceId)
                };

                int result = DBHelper.ExecuteNonQuery(sql, parameters);

                if (result > 0)
                {
                    MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除按钮点击事件
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 第一步：检查是否选中
            if (dgvSpaces.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择要删除的车位！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string spaceId = dgvSpaces.SelectedRows[0].Cells["车位编号"].Value.ToString();
            string status = dgvSpaces.SelectedRows[0].Cells["状态"].Value.ToString();

            // 第二步：检查车位是否被占用
            if (status == "占用")
            {
                MessageBox.Show("该车位正在使用中，无法删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 第三步：确认删除
            DialogResult result = MessageBox.Show("确定要删除该车位吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 第四步：检查是否有停车记录
                    string checkSql = "SELECT COUNT(*) FROM ParkingRecords WHERE SpaceID = @SpaceID";
                    SqlParameter[] checkParams = { new SqlParameter("@SpaceID", spaceId) };
                    int count = Convert.ToInt32(DBHelper.ExecuteScalar(checkSql, checkParams));

                    if (count > 0)
                    {
                        MessageBox.Show("该车位有停车记录，无法删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 第五步：执行删除
                    string sql = "DELETE FROM ParkingSpaces WHERE SpaceID = @SpaceID";
                    SqlParameter[] parameters = { new SqlParameter("@SpaceID", spaceId) };

                    int deleteResult = DBHelper.ExecuteNonQuery(sql, parameters);

                    if (deleteResult > 0)
                    {
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 表格选中行改变事件
        /// </summary>
        private void dgvSpaces_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSpaces.SelectedRows.Count > 0)
            {
                // 自动填充表单
                DataGridViewRow row = dgvSpaces.SelectedRows[0];
                txtSpaceID.Text = row.Cells["车位编号"].Value.ToString();
                txtSpaceName.Text = row.Cells["车位名称"].Value.ToString();
                cmbSpaceType.Text = row.Cells["车位类型"].Value.ToString();

                // 禁止修改车位编号
                txtSpaceID.ReadOnly = true;
            }
        }

        /// <summary>
        /// 刷新按钮点击事件
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadData();
        }

        /// <summary>
        /// 清空输入框
        /// </summary>
        private void ClearInputs()
        {
            txtSpaceID.Text = "";
            txtSpaceName.Text = "";
            cmbSpaceType.SelectedIndex = 0;
            txtSpaceID.ReadOnly = false;
        }
    }
}
