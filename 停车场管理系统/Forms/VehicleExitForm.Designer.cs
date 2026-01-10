namespace ParkingManagement.Forms
{
    partial class VehicleExitForm
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
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtPlateNumber = new System.Windows.Forms.TextBox();
            this.lblPlateNumber = new System.Windows.Forms.Label();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblFeeValue = new System.Windows.Forms.Label();
            this.lblFee = new System.Windows.Forms.Label();
            this.lblDurationValue = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblEntryTimeValue = new System.Windows.Forms.Label();
            this.lblEntryTime = new System.Windows.Forms.Label();
            this.lblSpaceValue = new System.Windows.Forms.Label();
            this.lblSpace = new System.Windows.Forms.Label();
            this.lblPlateNumberValue = new System.Windows.Forms.Label();
            this.lblPlateNumberTitle = new System.Windows.Forms.Label();
            this.lblExitTime = new System.Windows.Forms.Label();
            this.dtpExitTime = new System.Windows.Forms.DateTimePicker();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpSearch.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.txtPlateNumber);
            this.grpSearch.Controls.Add(this.lblPlateNumber);
            this.grpSearch.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.grpSearch.Location = new System.Drawing.Point(20, 15);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(410, 60);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "查询车辆";
            // 
            // lblPlateNumber
            // 
            this.lblPlateNumber.AutoSize = true;
            this.lblPlateNumber.Location = new System.Drawing.Point(15, 25);
            this.lblPlateNumber.Name = "lblPlateNumber";
            this.lblPlateNumber.Size = new System.Drawing.Size(65, 20);
            this.lblPlateNumber.TabIndex = 0;
            this.lblPlateNumber.Text = "车牌号：";
            // 
            // txtPlateNumber
            // 
            this.txtPlateNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlateNumber.Location = new System.Drawing.Point(85, 22);
            this.txtPlateNumber.Name = "txtPlateNumber";
            this.txtPlateNumber.Size = new System.Drawing.Size(200, 25);
            this.txtPlateNumber.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(300, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 28);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.lblFeeValue);
            this.grpInfo.Controls.Add(this.lblFee);
            this.grpInfo.Controls.Add(this.lblDurationValue);
            this.grpInfo.Controls.Add(this.lblDuration);
            this.grpInfo.Controls.Add(this.lblEntryTimeValue);
            this.grpInfo.Controls.Add(this.lblEntryTime);
            this.grpInfo.Controls.Add(this.lblSpaceValue);
            this.grpInfo.Controls.Add(this.lblSpace);
            this.grpInfo.Controls.Add(this.lblPlateNumberValue);
            this.grpInfo.Controls.Add(this.lblPlateNumberTitle);
            this.grpInfo.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.grpInfo.Location = new System.Drawing.Point(20, 85);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(410, 170);
            this.grpInfo.TabIndex = 1;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "车辆信息";
            // 
            // lblPlateNumberTitle
            // 
            this.lblPlateNumberTitle.AutoSize = true;
            this.lblPlateNumberTitle.Location = new System.Drawing.Point(20, 30);
            this.lblPlateNumberTitle.Name = "lblPlateNumberTitle";
            this.lblPlateNumberTitle.Size = new System.Drawing.Size(65, 20);
            this.lblPlateNumberTitle.TabIndex = 0;
            this.lblPlateNumberTitle.Text = "车牌号：";
            // 
            // lblPlateNumberValue
            // 
            this.lblPlateNumberValue.AutoSize = true;
            this.lblPlateNumberValue.ForeColor = System.Drawing.Color.Blue;
            this.lblPlateNumberValue.Location = new System.Drawing.Point(90, 30);
            this.lblPlateNumberValue.Name = "lblPlateNumberValue";
            this.lblPlateNumberValue.Size = new System.Drawing.Size(14, 20);
            this.lblPlateNumberValue.TabIndex = 1;
            this.lblPlateNumberValue.Text = "-";
            // 
            // lblSpace
            // 
            this.lblSpace.AutoSize = true;
            this.lblSpace.Location = new System.Drawing.Point(210, 30);
            this.lblSpace.Name = "lblSpace";
            this.lblSpace.Size = new System.Drawing.Size(51, 20);
            this.lblSpace.TabIndex = 2;
            this.lblSpace.Text = "车位：";
            // 
            // lblSpaceValue
            // 
            this.lblSpaceValue.AutoSize = true;
            this.lblSpaceValue.ForeColor = System.Drawing.Color.Blue;
            this.lblSpaceValue.Location = new System.Drawing.Point(265, 30);
            this.lblSpaceValue.Name = "lblSpaceValue";
            this.lblSpaceValue.Size = new System.Drawing.Size(14, 20);
            this.lblSpaceValue.TabIndex = 3;
            this.lblSpaceValue.Text = "-";
            // 
            // lblEntryTime
            // 
            this.lblEntryTime.AutoSize = true;
            this.lblEntryTime.Location = new System.Drawing.Point(20, 60);
            this.lblEntryTime.Name = "lblEntryTime";
            this.lblEntryTime.Size = new System.Drawing.Size(79, 20);
            this.lblEntryTime.TabIndex = 4;
            this.lblEntryTime.Text = "进场时间：";
            // 
            // lblEntryTimeValue
            // 
            this.lblEntryTimeValue.AutoSize = true;
            this.lblEntryTimeValue.ForeColor = System.Drawing.Color.Blue;
            this.lblEntryTimeValue.Location = new System.Drawing.Point(105, 60);
            this.lblEntryTimeValue.Name = "lblEntryTimeValue";
            this.lblEntryTimeValue.Size = new System.Drawing.Size(14, 20);
            this.lblEntryTimeValue.TabIndex = 5;
            this.lblEntryTimeValue.Text = "-";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(20, 95);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(79, 20);
            this.lblDuration.TabIndex = 6;
            this.lblDuration.Text = "停车时长：";
            // 
            // lblDurationValue
            // 
            this.lblDurationValue.AutoSize = true;
            this.lblDurationValue.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lblDurationValue.ForeColor = System.Drawing.Color.Green;
            this.lblDurationValue.Location = new System.Drawing.Point(105, 95);
            this.lblDurationValue.Name = "lblDurationValue";
            this.lblDurationValue.Size = new System.Drawing.Size(14, 19);
            this.lblDurationValue.TabIndex = 7;
            this.lblDurationValue.Text = "-";
            // 
            // lblFee
            // 
            this.lblFee.AutoSize = true;
            this.lblFee.Location = new System.Drawing.Point(20, 130);
            this.lblFee.Name = "lblFee";
            this.lblFee.Size = new System.Drawing.Size(79, 20);
            this.lblFee.TabIndex = 8;
            this.lblFee.Text = "应收费用：";
            // 
            // lblFeeValue
            // 
            this.lblFeeValue.AutoSize = true;
            this.lblFeeValue.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.lblFeeValue.ForeColor = System.Drawing.Color.Red;
            this.lblFeeValue.Location = new System.Drawing.Point(105, 125);
            this.lblFeeValue.Name = "lblFeeValue";
            this.lblFeeValue.Size = new System.Drawing.Size(20, 26);
            this.lblFeeValue.TabIndex = 9;
            this.lblFeeValue.Text = "-";
            // 
            // lblExitTime
            // 
            this.lblExitTime.AutoSize = true;
            this.lblExitTime.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblExitTime.Location = new System.Drawing.Point(20, 270);
            this.lblExitTime.Name = "lblExitTime";
            this.lblExitTime.Size = new System.Drawing.Size(79, 20);
            this.lblExitTime.TabIndex = 2;
            this.lblExitTime.Text = "出场时间：";
            // 
            // dtpExitTime
            // 
            this.dtpExitTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpExitTime.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.dtpExitTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExitTime.Location = new System.Drawing.Point(105, 267);
            this.dtpExitTime.Name = "dtpExitTime";
            this.dtpExitTime.Size = new System.Drawing.Size(200, 25);
            this.dtpExitTime.TabIndex = 3;
            this.dtpExitTime.ValueChanged += new System.EventHandler(this.dtpExitTime_ValueChanged);
            // 
            // btnExit
            // 
            this.btnExit.Enabled = false;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnExit.Location = new System.Drawing.Point(100, 310);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 35);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "确认出场";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnCancel.Location = new System.Drawing.Point(250, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // VehicleExitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 365);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.dtpExitTime);
            this.Controls.Add(this.lblExitTime);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.grpSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VehicleExitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "车辆出场";
            this.Load += new System.EventHandler(this.VehicleExitForm_Load);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtPlateNumber;
        private System.Windows.Forms.Label lblPlateNumber;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblFeeValue;
        private System.Windows.Forms.Label lblFee;
        private System.Windows.Forms.Label lblDurationValue;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblEntryTimeValue;
        private System.Windows.Forms.Label lblEntryTime;
        private System.Windows.Forms.Label lblSpaceValue;
        private System.Windows.Forms.Label lblSpace;
        private System.Windows.Forms.Label lblPlateNumberValue;
        private System.Windows.Forms.Label lblPlateNumberTitle;
        private System.Windows.Forms.Label lblExitTime;
        private System.Windows.Forms.DateTimePicker dtpExitTime;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
    }
}
