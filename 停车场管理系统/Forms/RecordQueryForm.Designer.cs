namespace ParkingManagement.Forms
{
    partial class RecordQueryForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this.grpQuery = new System.Windows.Forms.GroupBox();
            this.chkInParkOnly = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.txtPlateNumber = new System.Windows.Forms.TextBox();
            this.lblPlateNumber = new System.Windows.Forms.Label();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.grpStats = new System.Windows.Forms.GroupBox();
            this.lblInParkCount = new System.Windows.Forms.Label();
            this.lblInParkTitle = new System.Windows.Forms.Label();
            this.lblTotalIncome = new System.Windows.Forms.Label();
            this.lblIncomeTitle = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblCountTitle = new System.Windows.Forms.Label();
            this.grpQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            this.grpStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpQuery
            // 
            this.grpQuery.Controls.Add(this.chkInParkOnly);
            this.grpQuery.Controls.Add(this.btnRefresh);
            this.grpQuery.Controls.Add(this.btnSearch);
            this.grpQuery.Controls.Add(this.dtpEndDate);
            this.grpQuery.Controls.Add(this.lblTo);
            this.grpQuery.Controls.Add(this.dtpStartDate);
            this.grpQuery.Controls.Add(this.lblDateRange);
            this.grpQuery.Controls.Add(this.txtPlateNumber);
            this.grpQuery.Controls.Add(this.lblPlateNumber);
            this.grpQuery.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.grpQuery.Location = new System.Drawing.Point(20, 20);
            this.grpQuery.Name = "grpQuery";
            this.grpQuery.Size = new System.Drawing.Size(760, 100);
            this.grpQuery.TabIndex = 0;
            this.grpQuery.TabStop = false;
            this.grpQuery.Text = "查询条件";
            // 
            // lblPlateNumber
            // 
            this.lblPlateNumber.AutoSize = true;
            this.lblPlateNumber.Location = new System.Drawing.Point(20, 35);
            this.lblPlateNumber.Name = "lblPlateNumber";
            this.lblPlateNumber.Size = new System.Drawing.Size(65, 20);
            this.lblPlateNumber.TabIndex = 0;
            this.lblPlateNumber.Text = "车牌号：";
            // 
            // txtPlateNumber
            // 
            this.txtPlateNumber.Location = new System.Drawing.Point(90, 32);
            this.txtPlateNumber.Name = "txtPlateNumber";
            this.txtPlateNumber.Size = new System.Drawing.Size(120, 25);
            this.txtPlateNumber.TabIndex = 1;
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Location = new System.Drawing.Point(230, 35);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(79, 20);
            this.lblDateRange.TabIndex = 2;
            this.lblDateRange.Text = "日期范围：";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(315, 32);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(110, 25);
            this.dtpStartDate.TabIndex = 3;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(430, 35);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(23, 20);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "至";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(458, 32);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(110, 25);
            this.dtpEndDate.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(590, 30);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 30);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(670, 30);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 30);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // chkInParkOnly
            // 
            this.chkInParkOnly.AutoSize = true;
            this.chkInParkOnly.Location = new System.Drawing.Point(90, 65);
            this.chkInParkOnly.Name = "chkInParkOnly";
            this.chkInParkOnly.Size = new System.Drawing.Size(124, 24);
            this.chkInParkOnly.TabIndex = 8;
            this.chkInParkOnly.Text = "只看在场车辆";
            this.chkInParkOnly.UseVisualStyleBackColor = true;
            this.chkInParkOnly.CheckedChanged += new System.EventHandler(this.chkInParkOnly_CheckedChanged);
            // 
            // dgvRecords
            // 
            this.dgvRecords.AllowUserToAddRows = false;
            this.dgvRecords.AllowUserToDeleteRows = false;
            this.dgvRecords.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecords.Location = new System.Drawing.Point(20, 130);
            this.dgvRecords.MultiSelect = false;
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.ReadOnly = true;
            this.dgvRecords.RowTemplate.Height = 25;
            this.dgvRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecords.Size = new System.Drawing.Size(760, 300);
            this.dgvRecords.TabIndex = 1;
            // 
            // grpStats
            // 
            this.grpStats.Controls.Add(this.lblInParkCount);
            this.grpStats.Controls.Add(this.lblInParkTitle);
            this.grpStats.Controls.Add(this.lblTotalIncome);
            this.grpStats.Controls.Add(this.lblIncomeTitle);
            this.grpStats.Controls.Add(this.lblTotalCount);
            this.grpStats.Controls.Add(this.lblCountTitle);
            this.grpStats.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.grpStats.Location = new System.Drawing.Point(20, 440);
            this.grpStats.Name = "grpStats";
            this.grpStats.Size = new System.Drawing.Size(760, 60);
            this.grpStats.TabIndex = 2;
            this.grpStats.TabStop = false;
            this.grpStats.Text = "统计信息";
            // 
            // lblCountTitle
            // 
            this.lblCountTitle.AutoSize = true;
            this.lblCountTitle.Location = new System.Drawing.Point(20, 28);
            this.lblCountTitle.Name = "lblCountTitle";
            this.lblCountTitle.Size = new System.Drawing.Size(79, 20);
            this.lblCountTitle.TabIndex = 0;
            this.lblCountTitle.Text = "记录总数：";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalCount.Location = new System.Drawing.Point(105, 28);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(19, 20);
            this.lblTotalCount.TabIndex = 1;
            this.lblTotalCount.Text = "0";
            // 
            // lblIncomeTitle
            // 
            this.lblIncomeTitle.AutoSize = true;
            this.lblIncomeTitle.Location = new System.Drawing.Point(200, 28);
            this.lblIncomeTitle.Name = "lblIncomeTitle";
            this.lblIncomeTitle.Size = new System.Drawing.Size(65, 20);
            this.lblIncomeTitle.TabIndex = 2;
            this.lblIncomeTitle.Text = "总收入：";
            // 
            // lblTotalIncome
            // 
            this.lblTotalIncome.AutoSize = true;
            this.lblTotalIncome.ForeColor = System.Drawing.Color.Green;
            this.lblTotalIncome.Location = new System.Drawing.Point(270, 28);
            this.lblTotalIncome.Name = "lblTotalIncome";
            this.lblTotalIncome.Size = new System.Drawing.Size(54, 20);
            this.lblTotalIncome.TabIndex = 3;
            this.lblTotalIncome.Text = "0.00 元";
            // 
            // lblInParkTitle
            // 
            this.lblInParkTitle.AutoSize = true;
            this.lblInParkTitle.Location = new System.Drawing.Point(420, 28);
            this.lblInParkTitle.Name = "lblInParkTitle";
            this.lblInParkTitle.Size = new System.Drawing.Size(93, 20);
            this.lblInParkTitle.TabIndex = 4;
            this.lblInParkTitle.Text = "当前在场：";
            // 
            // lblInParkCount
            // 
            this.lblInParkCount.AutoSize = true;
            this.lblInParkCount.ForeColor = System.Drawing.Color.Red;
            this.lblInParkCount.Location = new System.Drawing.Point(520, 28);
            this.lblInParkCount.Name = "lblInParkCount";
            this.lblInParkCount.Size = new System.Drawing.Size(19, 20);
            this.lblInParkCount.TabIndex = 5;
            this.lblInParkCount.Text = "0";
            // 
            // RecordQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.grpStats);
            this.Controls.Add(this.dgvRecords);
            this.Controls.Add(this.grpQuery);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecordQueryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "停车记录查询";
            this.Load += new System.EventHandler(this.RecordQueryForm_Load);
            this.grpQuery.ResumeLayout(false);
            this.grpQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            this.grpStats.ResumeLayout(false);
            this.grpStats.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpQuery;
        private System.Windows.Forms.CheckBox chkInParkOnly;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.TextBox txtPlateNumber;
        private System.Windows.Forms.Label lblPlateNumber;
        private System.Windows.Forms.DataGridView dgvRecords;
        private System.Windows.Forms.GroupBox grpStats;
        private System.Windows.Forms.Label lblInParkCount;
        private System.Windows.Forms.Label lblInParkTitle;
        private System.Windows.Forms.Label lblTotalIncome;
        private System.Windows.Forms.Label lblIncomeTitle;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblCountTitle;
    }
}
