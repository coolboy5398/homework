using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace TodoList
{
    /// <summary>
    /// 待办事项应用程序主窗体
    /// </summary>
    public partial class Form1 : Form
    {
        // ==================== 任务数据类 ====================
        
        /// <summary>
        /// 任务项 - 存储单个任务的信息
        /// </summary>
        private class TaskItem
        {
            public string Id;           // 任务ID
            public string Content;      // 任务内容
            public bool IsCompleted;    // 是否完成
            public int Priority;        // 优先级: 0=低, 1=中, 2=高
            public int Category;        // 分类: 0=默认, 1=工作, 2=学习, 3=生活, 4=其他
            public string CreatedTime;  // 创建时间
            public string DueDate;      // 截止日期
            public string Notes;        // 备注

            // 构造函数 - 创建新任务时自动设置默认值
            public TaskItem()
            {
                Id = Guid.NewGuid().ToString().Substring(0, 8);
                Content = "";
                IsCompleted = false;
                Priority = 1;  // 默认中优先级
                Category = 0;  // 默认分类
                CreatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                DueDate = "";
                Notes = "";
            }
        }

        // ==================== 成员变量 ====================
        
        private List<TaskItem> allTasks;           // 所有任务列表
        private List<TaskItem> displayTasks;       // 当前显示的任务列表
        private string dataFilePath = "tasks.json"; // 数据文件路径
        private JavaScriptSerializer jsonHelper;    // JSON序列化工具
        
        // 筛选条件
        private string searchText = "";      // 搜索关键词
        private int filterCategory = -1;     // 筛选分类 (-1表示全部)
        private bool hideCompleted = false;  // 是否隐藏已完成

        // ==================== 构造函数 ====================

        public Form1()
        {
            InitializeComponent();
            
            // 初始化变量
            allTasks = new List<TaskItem>();
            displayTasks = new List<TaskItem>();
            jsonHelper = new JavaScriptSerializer();
        }

        // ==================== 窗体事件 ====================
        
        /// <summary>
        /// 窗体加载时执行
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // 创建右键菜单
            CreateContextMenu();
            
            // 加载数据
            LoadDataFromFile();
            
            // 初始化下拉框
            InitComboBoxes();
            
            // 刷新显示
            FilterAndRefresh();
            UpdateStatistics();
            
            // 聚焦到输入框
            txtTask.Focus();
        }

        /// <summary>
        /// 窗体关闭时保存数据
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveDataToFile();
        }

        /// <summary>
        /// 键盘快捷键处理
        /// </summary>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl+N: 聚焦到输入框
            if (e.Control && e.KeyCode == Keys.N)
            {
                txtTask.Focus();
                txtTask.SelectAll();
                e.Handled = true;
            }
            // Ctrl+S: 保存
            else if (e.Control && e.KeyCode == Keys.S)
            {
                SaveDataToFile();
                MessageBox.Show("保存成功！", "提示");
                e.Handled = true;
            }
            // Delete: 删除任务
            else if (e.KeyCode == Keys.Delete)
            {
                DeleteTask();
                e.Handled = true;
            }
            // F5: 刷新
            else if (e.KeyCode == Keys.F5)
            {
                FilterAndRefresh();
                e.Handled = true;
            }
        }

        /// <summary>
        /// 输入框按回车添加任务
        /// </summary>
        private void TxtTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        // ==================== 初始化方法 ====================

        /// <summary>
        /// 初始化所有下拉框
        /// </summary>
        private void InitComboBoxes()
        {
            // 优先级下拉框
            cmbPriority.Items.Clear();
            cmbPriority.Items.Add("低");
            cmbPriority.Items.Add("中");
            cmbPriority.Items.Add("高");
            cmbPriority.SelectedIndex = 1;

            // 任务分类下拉框
            cmbTaskCategory.Items.Clear();
            cmbTaskCategory.Items.Add("默认");
            cmbTaskCategory.Items.Add("工作");
            cmbTaskCategory.Items.Add("学习");
            cmbTaskCategory.Items.Add("生活");
            cmbTaskCategory.Items.Add("其他");
            cmbTaskCategory.SelectedIndex = 0;

            // 筛选分类下拉框
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("全部分类");
            cmbCategory.Items.Add("默认");
            cmbCategory.Items.Add("工作");
            cmbCategory.Items.Add("学习");
            cmbCategory.Items.Add("生活");
            cmbCategory.Items.Add("其他");
            cmbCategory.SelectedIndex = 0;
        }

        /// <summary>
        /// 创建右键菜单
        /// </summary>
        private void CreateContextMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            
            // 编辑
            ToolStripMenuItem editItem = new ToolStripMenuItem("编辑任务");
            editItem.Click += MenuEditItem_Click;
            menu.Items.Add(editItem);
            
            // 删除
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("删除任务");
            deleteItem.Click += MenuDeleteItem_Click;
            menu.Items.Add(deleteItem);
            
            menu.Items.Add(new ToolStripSeparator());
            
            // 设置优先级
            ToolStripMenuItem highItem = new ToolStripMenuItem("设为高优先级");
            highItem.Click += MenuHighPriority_Click;
            menu.Items.Add(highItem);
            
            ToolStripMenuItem mediumItem = new ToolStripMenuItem("设为中优先级");
            mediumItem.Click += MenuMediumPriority_Click;
            menu.Items.Add(mediumItem);
            
            ToolStripMenuItem lowItem = new ToolStripMenuItem("设为低优先级");
            lowItem.Click += MenuLowPriority_Click;
            menu.Items.Add(lowItem);
            
            lstTasks.ContextMenuStrip = menu;
        }

        // ==================== 右键菜单事件处理 ====================

        /// <summary>
        /// 右键菜单 - 编辑任务
        /// </summary>
        private void MenuEditItem_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        /// <summary>
        /// 右键菜单 - 删除任务
        /// </summary>
        private void MenuDeleteItem_Click(object sender, EventArgs e)
        {
            DeleteTask();
        }

        /// <summary>
        /// 右键菜单 - 设为高优先级
        /// </summary>
        private void MenuHighPriority_Click(object sender, EventArgs e)
        {
            SetPriority(2);
        }

        /// <summary>
        /// 右键菜单 - 设为中优先级
        /// </summary>
        private void MenuMediumPriority_Click(object sender, EventArgs e)
        {
            SetPriority(1);
        }

        /// <summary>
        /// 右键菜单 - 设为低优先级
        /// </summary>
        private void MenuLowPriority_Click(object sender, EventArgs e)
        {
            SetPriority(0);
        }

        // ==================== 文件读写方法 ====================

        /// <summary>
        /// 从JSON文件加载数据
        /// </summary>
        private void LoadDataFromFile()
        {
            try
            {
                // 检查文件是否存在
                if (File.Exists(dataFilePath))
                {
                    // 读取文件内容
                    string jsonText = File.ReadAllText(dataFilePath);
                    // 反序列化为任务列表
                    List<TaskItem> loaded = jsonHelper.Deserialize<List<TaskItem>>(jsonText);
                    if (loaded != null)
                    {
                        allTasks = loaded;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载数据失败：" + ex.Message, "错误");
                allTasks = new List<TaskItem>();
            }
        }

        /// <summary>
        /// 保存数据到JSON文件
        /// </summary>
        private void SaveDataToFile()
        {
            try
            {
                // 序列化为JSON字符串
                string jsonText = jsonHelper.Serialize(allTasks);
                // 写入文件
                File.WriteAllText(dataFilePath, jsonText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存数据失败：" + ex.Message, "错误");
            }
        }

        /// <summary>
        /// 导出任务到文件
        /// </summary>
        private void ExportToFile(string filePath)
        {
            try
            {
                string jsonText = jsonHelper.Serialize(allTasks);
                File.WriteAllText(filePath, jsonText);
                MessageBox.Show("导出成功！", "提示");
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败：" + ex.Message, "错误");
            }
        }

        /// <summary>
        /// 从文件导入任务
        /// </summary>
        private void ImportFromFile(string filePath)
        {
            try
            {
                string jsonText = File.ReadAllText(filePath);
                List<TaskItem> imported = jsonHelper.Deserialize<List<TaskItem>>(jsonText);
                
                if (imported != null && imported.Count > 0)
                {
                    // 询问用户是合并还是替换
                    DialogResult result = MessageBox.Show(
                        "选择[是]合并到现有任务，选择[否]替换现有任务",
                        "导入选项",
                        MessageBoxButtons.YesNoCancel);
                    
                    if (result == DialogResult.Yes)
                    {
                        // 合并：添加到现有列表
                        for (int i = 0; i < imported.Count; i++)
                        {
                            imported[i].Id = Guid.NewGuid().ToString().Substring(0, 8);
                            allTasks.Add(imported[i]);
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        // 替换：清空后添加
                        allTasks = imported;
                    }
                    else
                    {
                        return;
                    }
                    
                    SaveDataToFile();
                    FilterAndRefresh();
                    UpdateStatistics();
                    MessageBox.Show("导入成功！", "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入失败：" + ex.Message, "错误");
            }
        }

        // ==================== 按钮点击事件 ====================

        /// <summary>
        /// 添加任务按钮
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 检查输入是否为空
            string content = txtTask.Text.Trim();
            if (content == "")
            {
                MessageBox.Show("请输入任务内容", "提示");
                return;
            }

            // 创建新任务
            TaskItem newTask = new TaskItem();
            newTask.Content = content;
            newTask.Priority = cmbPriority.SelectedIndex;
            newTask.Category = cmbTaskCategory.SelectedIndex;
            
            // 设置截止日期（如果选中了）
            if (dtpDueDate.Checked)
            {
                newTask.DueDate = dtpDueDate.Value.ToString("yyyy-MM-dd");
            }

            // 添加到列表
            allTasks.Add(newTask);

            // 保存并刷新
            SaveDataToFile();
            FilterAndRefresh();
            UpdateStatistics();

            // 清空输入框
            txtTask.Clear();
            txtTask.Focus();
            dtpDueDate.Checked = false;
        }

        /// <summary>
        /// 删除任务按钮
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteTask();
        }

        /// <summary>
        /// 编辑任务按钮
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        /// <summary>
        /// 清空已完成按钮
        /// </summary>
        private void btnClearCompleted_Click(object sender, EventArgs e)
        {
            // 统计已完成数量
            int count = 0;
            for (int i = 0; i < allTasks.Count; i++)
            {
                if (allTasks[i].IsCompleted)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                MessageBox.Show("没有已完成的任务", "提示");
                return;
            }

            // 确认删除
            DialogResult result = MessageBox.Show(
                "确定要删除 " + count + " 个已完成的任务吗？",
                "确认",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // 从后往前删除（避免索引问题）
                for (int i = allTasks.Count - 1; i >= 0; i--)
                {
                    if (allTasks[i].IsCompleted)
                    {
                        allTasks.RemoveAt(i);
                    }
                }
                
                SaveDataToFile();
                FilterAndRefresh();
                UpdateStatistics();
            }
        }

        /// <summary>
        /// 导出按钮
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JSON文件|*.json";
            dialog.FileName = "tasks_backup.json";
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ExportToFile(dialog.FileName);
            }
        }

        /// <summary>
        /// 导入按钮
        /// </summary>
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JSON文件|*.json";
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ImportFromFile(dialog.FileName);
            }
        }

        /// <summary>
        /// 统计报告按钮
        /// </summary>
        private void btnStats_Click(object sender, EventArgs e)
        {
            ShowStatisticsReport();
        }

        // ==================== 筛选和排序事件 ====================

        /// <summary>
        /// 搜索框文本改变
        /// </summary>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchText = txtSearch.Text.Trim();
            FilterAndRefresh();
        }

        /// <summary>
        /// 分类筛选改变
        /// </summary>
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            filterCategory = cmbCategory.SelectedIndex - 1;  // -1表示全部
            FilterAndRefresh();
        }

        /// <summary>
        /// 只显示未完成复选框
        /// </summary>
        private void chkShowIncomplete_CheckedChanged(object sender, EventArgs e)
        {
            hideCompleted = chkShowIncomplete.Checked;
            FilterAndRefresh();
        }

        /// <summary>
        /// 任务列表勾选状态改变
        /// </summary>
        private void lstTasks_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = e.Index;
            if (index >= 0 && index < displayTasks.Count)
            {
                // 更新完成状态
                displayTasks[index].IsCompleted = (e.NewValue == CheckState.Checked);
                SaveDataToFile();
                
                // 延迟更新统计（因为事件在状态改变前触发）
                this.BeginInvoke(new MethodInvoker(UpdateStatistics));
            }
        }

        /// <summary>
        /// 任务列表选择改变
        /// </summary>
        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstTasks.SelectedIndex;
            if (index >= 0 && index < displayTasks.Count)
            {
                ShowTaskDetail(displayTasks[index]);
            }
            else
            {
                lblTaskDetail.Text = "任务详情\n━━━━━━━━━━\n选择一个任务查看详情";
            }
        }

        // ==================== 核心功能方法 ====================

        /// <summary>
        /// 筛选任务并刷新列表
        /// </summary>
        private void FilterAndRefresh()
        {
            // 清空显示列表
            displayTasks.Clear();

            // 遍历所有任务，筛选符合条件的
            for (int i = 0; i < allTasks.Count; i++)
            {
                TaskItem task = allTasks[i];
                bool show = true;

                // 检查分类筛选
                if (filterCategory >= 0 && task.Category != filterCategory)
                {
                    show = false;
                }

                // 检查是否隐藏已完成
                if (hideCompleted && task.IsCompleted)
                {
                    show = false;
                }

                // 检查搜索关键词
                if (searchText != "")
                {
                    // 在内容和备注中搜索（不区分大小写）
                    string contentLower = task.Content.ToLower();
                    string searchLower = searchText.ToLower();
                    string notesLower = (task.Notes ?? "").ToLower();
                    
                    if (!contentLower.Contains(searchLower) && !notesLower.Contains(searchLower))
                    {
                        show = false;
                    }
                }

                // 如果符合条件，添加到显示列表
                if (show)
                {
                    displayTasks.Add(task);
                }
            }

            // 刷新列表显示
            RefreshListBox();
        }

        /// <summary>
        /// 刷新列表框显示
        /// </summary>
        private void RefreshListBox()
        {
            lstTasks.Items.Clear();
            
            for (int i = 0; i < displayTasks.Count; i++)
            {
                TaskItem task = displayTasks[i];
                
                // 构建显示文本
                string priorityText = "";
                if (task.Priority == 0) priorityText = "[低]";
                else if (task.Priority == 1) priorityText = "[中]";
                else if (task.Priority == 2) priorityText = "[高]";
                
                string displayText = priorityText + " " + task.Content;
                
                // 添加截止日期
                if (task.DueDate != "")
                {
                    displayText += " [截止:" + task.DueDate + "]";
                }
                
                // 添加到列表并设置勾选状态
                lstTasks.Items.Add(displayText, task.IsCompleted);
            }
        }

        /// <summary>
        /// 删除选中的任务
        /// </summary>
        private void DeleteTask()
        {
            int index = lstTasks.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("请先选择要删除的任务", "提示");
                return;
            }

            DialogResult result = MessageBox.Show("确定要删除这个任务吗？", "确认", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // 从总列表中删除
                TaskItem taskToDelete = displayTasks[index];
                allTasks.Remove(taskToDelete);
                
                SaveDataToFile();
                FilterAndRefresh();
                UpdateStatistics();
                
                // 清空右侧任务详情
                lblTaskDetail.Text = "任务详情\n━━━━━━━━━━\n选择一个任务查看详情";
            }
        }

        /// <summary>
        /// 编辑选中的任务
        /// </summary>
        private void EditTask()
        {
            int index = lstTasks.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("请先选择要编辑的任务", "提示");
                return;
            }

            TaskItem task = displayTasks[index];
            ShowEditDialog(task);
        }

        /// <summary>
        /// 设置选中任务的优先级
        /// </summary>
        private void SetPriority(int priority)
        {
            int index = lstTasks.SelectedIndex;
            if (index >= 0 && index < displayTasks.Count)
            {
                displayTasks[index].Priority = priority;
                SaveDataToFile();
                FilterAndRefresh();
            }
        }

        // ==================== 显示方法 ====================

        /// <summary>
        /// 更新统计信息
        /// </summary>
        private void UpdateStatistics()
        {
            int total = allTasks.Count;
            int completed = 0;
            int overdue = 0;
            int highPriority = 0;

            // 遍历统计
            for (int i = 0; i < allTasks.Count; i++)
            {
                TaskItem task = allTasks[i];
                
                if (task.IsCompleted)
                {
                    completed++;
                }
                
                // 检查是否过期
                if (!task.IsCompleted && task.DueDate != "")
                {
                    try
                    {
                        DateTime dueDate = DateTime.Parse(task.DueDate);
                        if (dueDate.Date < DateTime.Now.Date)
                        {
                            overdue++;
                        }
                    }
                    catch { }
                }
                
                // 统计高优先级未完成任务
                if (task.Priority == 2 && !task.IsCompleted)
                {
                    highPriority++;
                }
            }

            // 更新标签
            lblStats.Text = string.Format(
                "总计: {0} 项 | 已完成: {1} 项 | 过期: {2} 项 | 高优先级: {3} 项",
                total, completed, overdue, highPriority);

            // 更新进度条
            if (total > 0)
            {
                progressBar.Value = completed * 100 / total;
            }
            else
            {
                progressBar.Value = 0;
            }
        }

        /// <summary>
        /// 显示任务详情
        /// </summary>
        private void ShowTaskDetail(TaskItem task)
        {
            // 获取优先级文本
            string priorityText = "中";
            if (task.Priority == 0) priorityText = "低";
            else if (task.Priority == 2) priorityText = "高";

            // 获取分类文本
            string[] categories = { "默认", "工作", "学习", "生活", "其他" };
            string categoryText = categories[task.Category];

            // 获取状态文本
            string statusText = "进行中";
            if (task.IsCompleted)
            {
                statusText = "已完成";
            }
            else if (task.DueDate != "")
            {
                try
                {
                    DateTime dueDate = DateTime.Parse(task.DueDate);
                    if (dueDate.Date < DateTime.Now.Date)
                    {
                        statusText = "已过期";
                    }
                }
                catch { }
            }

            // 构建详情文本
            string detail = "任务详情\n";
            detail += "━━━━━━━━━━\n";
            detail += "内容：" + task.Content + "\n";
            detail += "优先级：" + priorityText + "\n";
            detail += "分类：" + categoryText + "\n";
            detail += "创建：" + task.CreatedTime + "\n";
            detail += "截止：" + (task.DueDate == "" ? "无" : task.DueDate) + "\n";
            detail += "状态：" + statusText + "\n";
            detail += "备注：" + (task.Notes == "" ? "无" : task.Notes);

            lblTaskDetail.Text = detail;
        }

        /// <summary>
        /// 显示统计报告
        /// </summary>
        private void ShowStatisticsReport()
        {
            int total = allTasks.Count;
            int completed = 0;
            int overdue = 0;
            int[] priorityCounts = { 0, 0, 0 };  // 低、中、高
            int[] categoryCounts = { 0, 0, 0, 0, 0 };  // 默认、工作、学习、生活、其他

            // 遍历统计
            for (int i = 0; i < allTasks.Count; i++)
            {
                TaskItem task = allTasks[i];
                
                if (task.IsCompleted) completed++;
                
                if (!task.IsCompleted && task.DueDate != "")
                {
                    try
                    {
                        if (DateTime.Parse(task.DueDate).Date < DateTime.Now.Date)
                            overdue++;
                    }
                    catch { }
                }
                
                if (task.Priority >= 0 && task.Priority <= 2)
                    priorityCounts[task.Priority]++;
                    
                if (task.Category >= 0 && task.Category <= 4)
                    categoryCounts[task.Category]++;
            }

            // 计算完成率
            double rate = total > 0 ? (double)completed / total * 100 : 0;

            // 构建报告
            string report = "═══════════════════════\n";
            report += "      任务统计报告\n";
            report += "═══════════════════════\n\n";
            report += "【总体情况】\n";
            report += "  总任务数：" + total + "\n";
            report += "  已完成：" + completed + "\n";
            report += "  未完成：" + (total - completed) + "\n";
            report += "  已过期：" + overdue + "\n";
            report += "  完成率：" + rate.ToString("F1") + "%\n\n";
            report += "【优先级分布】\n";
            report += "  高优先级：" + priorityCounts[2] + "\n";
            report += "  中优先级：" + priorityCounts[1] + "\n";
            report += "  低优先级：" + priorityCounts[0] + "\n\n";
            report += "【分类分布】\n";
            report += "  默认：" + categoryCounts[0] + "\n";
            report += "  工作：" + categoryCounts[1] + "\n";
            report += "  学习：" + categoryCounts[2] + "\n";
            report += "  生活：" + categoryCounts[3] + "\n";
            report += "  其他：" + categoryCounts[4] + "\n";
            report += "═══════════════════════";

            MessageBox.Show(report, "任务统计");
        }

        // ==================== 编辑对话框 ====================

        /// <summary>
        /// 显示编辑任务对话框
        /// </summary>
        private void ShowEditDialog(TaskItem task)
        {
            // 创建编辑窗体
            EditTaskForm editForm = new EditTaskForm();
            
            // 设置当前任务的值
            editForm.TaskContent = task.Content;
            editForm.TaskPriority = task.Priority;
            editForm.TaskCategory = task.Category;
            editForm.TaskDueDate = task.DueDate;
            editForm.TaskNotes = task.Notes;

            // 显示对话框
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // 更新任务信息
                task.Content = editForm.TaskContent;
                task.Priority = editForm.TaskPriority;
                task.Category = editForm.TaskCategory;
                task.DueDate = editForm.TaskDueDate;
                task.Notes = editForm.TaskNotes;

                // 保存并刷新
                SaveDataToFile();
                FilterAndRefresh();
            }
        }
    }
}
