namespace TodoList
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 初始化所有控件
            this.txtTask = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lstTasks = new System.Windows.Forms.CheckedListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblStats = new System.Windows.Forms.Label();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.cmbTaskCategory = new System.Windows.Forms.ComboBox();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.chkShowIncomplete = new System.Windows.Forms.CheckBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClearCompleted = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.lblTaskDetail = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBoxAdd = new System.Windows.Forms.GroupBox();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.groupBoxList = new System.Windows.Forms.GroupBox();
            this.groupBoxDetail = new System.Windows.Forms.GroupBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblFilterCategory = new System.Windows.Forms.Label();

            this.groupBoxAdd.SuspendLayout();
            this.groupBoxFilter.SuspendLayout();
            this.groupBoxList.SuspendLayout();
            this.groupBoxDetail.SuspendLayout();
            this.SuspendLayout();


            // ========================================
            // groupBoxAdd - 添加任务区域
            // ========================================
            this.groupBoxAdd.Controls.Add(this.txtTask);
            this.groupBoxAdd.Controls.Add(this.lblPriority);
            this.groupBoxAdd.Controls.Add(this.cmbPriority);
            this.groupBoxAdd.Controls.Add(this.lblCategory);
            this.groupBoxAdd.Controls.Add(this.cmbTaskCategory);
            this.groupBoxAdd.Controls.Add(this.lblDueDate);
            this.groupBoxAdd.Controls.Add(this.dtpDueDate);
            this.groupBoxAdd.Controls.Add(this.btnAdd);
            this.groupBoxAdd.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.groupBoxAdd.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAdd.Name = "groupBoxAdd";
            this.groupBoxAdd.Size = new System.Drawing.Size(560, 100);
            this.groupBoxAdd.TabIndex = 0;
            this.groupBoxAdd.TabStop = false;
            this.groupBoxAdd.Text = "添加新任务";

            // txtTask - 任务输入框
            this.txtTask.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.txtTask.Location = new System.Drawing.Point(10, 25);
            this.txtTask.Name = "txtTask";
            this.txtTask.Size = new System.Drawing.Size(460, 25);
            this.txtTask.TabIndex = 0;
            this.txtTask.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtTask_KeyDown);

            // lblPriority - 优先级标签
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(10, 60);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(50, 17);
            this.lblPriority.TabIndex = 1;
            this.lblPriority.Text = "优先级:";

            // cmbPriority - 优先级下拉框
            this.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriority.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.cmbPriority.Location = new System.Drawing.Point(65, 57);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(60, 25);
            this.cmbPriority.TabIndex = 2;

            // lblCategory - 分类标签
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(135, 60);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(38, 17);
            this.lblCategory.TabIndex = 3;
            this.lblCategory.Text = "分类:";

            // cmbTaskCategory - 任务分类下拉框
            this.cmbTaskCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaskCategory.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.cmbTaskCategory.Location = new System.Drawing.Point(175, 57);
            this.cmbTaskCategory.Name = "cmbTaskCategory";
            this.cmbTaskCategory.Size = new System.Drawing.Size(70, 25);
            this.cmbTaskCategory.TabIndex = 4;

            // lblDueDate - 截止日期标签
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Location = new System.Drawing.Point(255, 60);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(62, 17);
            this.lblDueDate.TabIndex = 5;
            this.lblDueDate.Text = "截止日期:";

            // dtpDueDate - 截止日期选择器
            this.dtpDueDate.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.dtpDueDate.Location = new System.Drawing.Point(320, 57);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.ShowCheckBox = true;
            this.dtpDueDate.Size = new System.Drawing.Size(150, 23);
            this.dtpDueDate.TabIndex = 6;
            this.dtpDueDate.Checked = false;

            // btnAdd - 添加按钮
            this.btnAdd.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.btnAdd.Location = new System.Drawing.Point(480, 25);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 55);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);


            // ========================================
            // groupBoxFilter - 筛选区域
            // ========================================
            this.groupBoxFilter.Controls.Add(this.lblSearch);
            this.groupBoxFilter.Controls.Add(this.txtSearch);
            this.groupBoxFilter.Controls.Add(this.lblFilterCategory);
            this.groupBoxFilter.Controls.Add(this.cmbCategory);
            this.groupBoxFilter.Controls.Add(this.chkShowIncomplete);
            this.groupBoxFilter.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.groupBoxFilter.Location = new System.Drawing.Point(12, 118);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(560, 55);
            this.groupBoxFilter.TabIndex = 1;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "筛选";

            // lblSearch - 搜索标签
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(10, 22);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(38, 17);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "搜索:";

            // txtSearch - 搜索框
            this.txtSearch.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(50, 19);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(120, 23);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // lblFilterCategory - 筛选分类标签
            this.lblFilterCategory.AutoSize = true;
            this.lblFilterCategory.Location = new System.Drawing.Point(180, 22);
            this.lblFilterCategory.Name = "lblFilterCategory";
            this.lblFilterCategory.Size = new System.Drawing.Size(38, 17);
            this.lblFilterCategory.TabIndex = 2;
            this.lblFilterCategory.Text = "分类:";

            // cmbCategory - 分类筛选下拉框
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.cmbCategory.Location = new System.Drawing.Point(220, 19);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(90, 25);
            this.cmbCategory.TabIndex = 3;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);

            // chkShowIncomplete - 只显示未完成复选框
            this.chkShowIncomplete.AutoSize = true;
            this.chkShowIncomplete.Location = new System.Drawing.Point(320, 21);
            this.chkShowIncomplete.Name = "chkShowIncomplete";
            this.chkShowIncomplete.Size = new System.Drawing.Size(87, 21);
            this.chkShowIncomplete.TabIndex = 4;
            this.chkShowIncomplete.Text = "只显示未完成";
            this.chkShowIncomplete.UseVisualStyleBackColor = true;
            this.chkShowIncomplete.CheckedChanged += new System.EventHandler(this.chkShowIncomplete_CheckedChanged);


            // ========================================
            // groupBoxList - 任务列表区域
            // ========================================
            this.groupBoxList.Controls.Add(this.lstTasks);
            this.groupBoxList.Controls.Add(this.btnDelete);
            this.groupBoxList.Controls.Add(this.btnEdit);
            this.groupBoxList.Controls.Add(this.btnClearCompleted);
            this.groupBoxList.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.groupBoxList.Location = new System.Drawing.Point(12, 179);
            this.groupBoxList.Name = "groupBoxList";
            this.groupBoxList.Size = new System.Drawing.Size(380, 300);
            this.groupBoxList.TabIndex = 2;
            this.groupBoxList.TabStop = false;
            this.groupBoxList.Text = "任务列表";

            // lstTasks - 任务列表
            this.lstTasks.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lstTasks.FormattingEnabled = true;
            this.lstTasks.Location = new System.Drawing.Point(10, 22);
            this.lstTasks.Name = "lstTasks";
            this.lstTasks.Size = new System.Drawing.Size(280, 268);
            this.lstTasks.TabIndex = 0;
            this.lstTasks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstTasks_ItemCheck);
            this.lstTasks.SelectedIndexChanged += new System.EventHandler(this.lstTasks_SelectedIndexChanged);

            // btnDelete - 删除按钮
            this.btnDelete.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnDelete.Location = new System.Drawing.Point(300, 22);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 30);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // btnEdit - 编辑按钮
            this.btnEdit.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnEdit.Location = new System.Drawing.Point(300, 58);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 30);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            // btnClearCompleted - 清空已完成按钮
            this.btnClearCompleted.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnClearCompleted.Location = new System.Drawing.Point(300, 94);
            this.btnClearCompleted.Name = "btnClearCompleted";
            this.btnClearCompleted.Size = new System.Drawing.Size(70, 30);
            this.btnClearCompleted.TabIndex = 3;
            this.btnClearCompleted.Text = "清空完成";
            this.btnClearCompleted.UseVisualStyleBackColor = true;
            this.btnClearCompleted.Click += new System.EventHandler(this.btnClearCompleted_Click);


            // ========================================
            // groupBoxDetail - 任务详情区域
            // ========================================
            this.groupBoxDetail.Controls.Add(this.lblTaskDetail);
            this.groupBoxDetail.Controls.Add(this.btnExport);
            this.groupBoxDetail.Controls.Add(this.btnImport);
            this.groupBoxDetail.Controls.Add(this.btnStats);
            this.groupBoxDetail.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.groupBoxDetail.Location = new System.Drawing.Point(398, 179);
            this.groupBoxDetail.Name = "groupBoxDetail";
            this.groupBoxDetail.Size = new System.Drawing.Size(174, 300);
            this.groupBoxDetail.TabIndex = 3;
            this.groupBoxDetail.TabStop = false;
            this.groupBoxDetail.Text = "任务详情";

            // lblTaskDetail - 任务详情标签
            this.lblTaskDetail.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblTaskDetail.Location = new System.Drawing.Point(10, 22);
            this.lblTaskDetail.Name = "lblTaskDetail";
            this.lblTaskDetail.Size = new System.Drawing.Size(154, 170);
            this.lblTaskDetail.TabIndex = 0;
            this.lblTaskDetail.Text = "任务详情\n━━━━━━━━━━━━━━━━━━━━\n选择一个任务查看详情";

            // btnExport - 导出按钮
            this.btnExport.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnExport.Location = new System.Drawing.Point(10, 200);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(70, 30);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            // btnImport - 导入按钮
            this.btnImport.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnImport.Location = new System.Drawing.Point(90, 200);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(70, 30);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);

            // btnStats - 统计按钮
            this.btnStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.btnStats.Location = new System.Drawing.Point(10, 236);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(150, 30);
            this.btnStats.TabIndex = 3;
            this.btnStats.Text = "查看统计报告";
            this.btnStats.UseVisualStyleBackColor = true;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);


            // ========================================
            // 底部状态栏区域
            // ========================================

            // progressBar - 进度条
            this.progressBar.Location = new System.Drawing.Point(12, 485);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(560, 20);
            this.progressBar.TabIndex = 4;

            // lblStats - 统计标签
            this.lblStats.AutoSize = true;
            this.lblStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblStats.Location = new System.Drawing.Point(12, 510);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(300, 17);
            this.lblStats.TabIndex = 5;
            this.lblStats.Text = "总计: 0 项 | 已完成: 0 项 | 过期: 0 项 | 高优先级: 0 项";

            // ========================================
            // Form1 - 主窗体
            // ========================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 536);
            this.Controls.Add(this.groupBoxAdd);
            this.Controls.Add(this.groupBoxFilter);
            this.Controls.Add(this.groupBoxList);
            this.Controls.Add(this.groupBoxDetail);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblStats);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "待办事项 - Todo List";
            this.KeyPreview = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);

            this.groupBoxAdd.ResumeLayout(false);
            this.groupBoxAdd.PerformLayout();
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            this.groupBoxList.ResumeLayout(false);
            this.groupBoxDetail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // ========================================
        // 控件声明
        // ========================================

        // 添加任务区域控件
        private System.Windows.Forms.GroupBox groupBoxAdd;
        private System.Windows.Forms.TextBox txtTask;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbTaskCategory;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Button btnAdd;

        // 筛选区域控件
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblFilterCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.CheckBox chkShowIncomplete;

        // 任务列表区域控件
        private System.Windows.Forms.GroupBox groupBoxList;
        private System.Windows.Forms.CheckedListBox lstTasks;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClearCompleted;

        // 任务详情区域控件
        private System.Windows.Forms.GroupBox groupBoxDetail;
        private System.Windows.Forms.Label lblTaskDetail;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnStats;

        // 底部状态栏控件
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStats;
    }
}
