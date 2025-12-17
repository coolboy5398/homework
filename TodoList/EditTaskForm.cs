using System;
using System.Windows.Forms;

namespace TodoList
{
    /// <summary>
    /// 编辑任务对话框窗体
    /// </summary>
    public partial class EditTaskForm : Form
    {
        // ==================== 属性 ====================
        
        // 任务内容
        public string TaskContent
        {
            get { return txtContent.Text.Trim(); }
            set { txtContent.Text = value; }
        }

        // 优先级 (0=低, 1=中, 2=高)
        public int TaskPriority
        {
            get { return cmbPriority.SelectedIndex; }
            set { cmbPriority.SelectedIndex = value; }
        }

        // 分类 (0=默认, 1=工作, 2=学习, 3=生活, 4=其他)
        public int TaskCategory
        {
            get { return cmbCategory.SelectedIndex; }
            set { cmbCategory.SelectedIndex = value; }
        }

        // 截止日期
        public string TaskDueDate
        {
            get
            {
                if (dtpDueDate.Checked)
                {
                    return dtpDueDate.Value.ToString("yyyy-MM-dd");
                }
                return "";
            }
            set
            {
                if (value != "")
                {
                    try
                    {
                        dtpDueDate.Value = DateTime.Parse(value);
                        dtpDueDate.Checked = true;
                    }
                    catch
                    {
                        dtpDueDate.Checked = false;
                    }
                }
                else
                {
                    dtpDueDate.Checked = false;
                }
            }
        }

        // 备注
        public string TaskNotes
        {
            get { return txtNotes.Text; }
            set { txtNotes.Text = value ?? ""; }
        }

        // ==================== 构造函数 ====================

        public EditTaskForm()
        {
            InitializeComponent();
            
            // 初始化下拉框选项
            InitComboBoxes();
        }

        // ==================== 初始化方法 ====================

        /// <summary>
        /// 初始化下拉框选项
        /// </summary>
        private void InitComboBoxes()
        {
            // 优先级下拉框
            cmbPriority.Items.Clear();
            cmbPriority.Items.Add("低");
            cmbPriority.Items.Add("中");
            cmbPriority.Items.Add("高");
            cmbPriority.SelectedIndex = 1;

            // 分类下拉框
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("默认");
            cmbCategory.Items.Add("工作");
            cmbCategory.Items.Add("学习");
            cmbCategory.Items.Add("生活");
            cmbCategory.Items.Add("其他");
            cmbCategory.SelectedIndex = 0;
        }
    }
}
