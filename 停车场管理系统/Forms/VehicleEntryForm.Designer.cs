namespace ParkingManagement.Forms
{
    partial class VehicleEntryForm
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
            this.grpEntry = new System.Windows.Forms.GroupBox();
            this.dtpEntryTime = new System.Windows.Forms.DateTimePicker();
            this.cmbSpace = new System.Windows.Forms.ComboBox();
            this.txtPlateNumber = new System.Windows.Forms.TextBox();
            this.lblEntryTime = new System.Windows.Forms.Label();
            this.lblSpace = new System.Windows.Forms.Label();
            this.lblPlateNumber = new System.Windows.Forms.Label();
            this.btnEntry = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpEntry.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpEntry
            // 
            this.grpEntry.Controls.Add(this.dtpEntryTime);
            this.grpEntry.Controls.Add(this.cmbSpace);
            this.grpEntry.Controls.Add(this.txtPlateNumber);
            this.grpEntry.Controls.Add(this.lblEntryTime);
            this.grpEntry.Controls.Add(this.lblSpace);
            this.grpEntry.Controls.Add(this.lblPlateNumber);
            this.grpEntry.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.grpEntry.Location = new System.Drawing.Point(30, 20);
            this.grpEntry.Name = "grpEntry";
            this.grpEntry.Size = new System.Drawing.Size(340, 160);
            this.grpEntry.TabIndex = 0;
            this.grpEntry.TabStop = false;
            this.grpEntry.Text = "车辆进场信息";
            // 
            // lblPlateNumber
            // 
            this.lblPlateNumber.AutoSize = true;
            this.lblPlateNumber.Location = new System.Drawing.Point(30, 35);
            this.lblPlateNumber.Name = "lblPlateNumber";
            this.lblPlateNumber.Size = new System.Drawing.Size(65, 20);
            this.lblPlateNumber.TabIndex = 0;
            this.lblPlateNumber.Text = "车牌号：";
            // 
            // txtPlateNumber
            // 
            this.txtPlateNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlateNumber.Location = new System.Drawing.Point(105, 32);
            this.txtPlateNumber.Name = "txtPlateNumber";
            this.txtPlateNumber.Size = new System.Drawing.Size(200, 25);
            this.txtPlateNumber.TabIndex = 1;
            // 
            // lblSpace
            // 
            this.lblSpace.AutoSize = true;
            this.lblSpace.Location = new System.Drawing.Point(30, 75);
            this.lblSpace.Name = "lblSpace";
            this.lblSpace.Size = new System.Drawing.Size(65, 20);
            this.lblSpace.TabIndex = 2;
            this.lblSpace.Text = "选择车位：";
            // 
            // cmbSpace
            // 
            this.cmbSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpace.FormattingEnabled = true;
            this.cmbSpace.Location = new System.Drawing.Point(105, 72);
            this.cmbSpace.Name = "cmbSpace";
            this.cmbSpace.Size = new System.Drawing.Size(200, 28);
            this.cmbSpace.TabIndex = 3;
            // 
            // lblEntryTime
            // 
            this.lblEntryTime.AutoSize = true;
            this.lblEntryTime.Location = new System.Drawing.Point(30, 115);
            this.lblEntryTime.Name = "lblEntryTime";
            this.lblEntryTime.Size = new System.Drawing.Size(65, 20);
            this.lblEntryTime.TabIndex = 4;
            this.lblEntryTime.Text = "进场时间：";
            // 
            // dtpEntryTime
            // 
            this.dtpEntryTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEntryTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEntryTime.Location = new System.Drawing.Point(105, 112);
            this.dtpEntryTime.Name = "dtpEntryTime";
            this.dtpEntryTime.Size = new System.Drawing.Size(200, 25);
            this.dtpEntryTime.TabIndex = 5;
            // 
            // btnEntry
            // 
            this.btnEntry.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnEntry.Location = new System.Drawing.Point(80, 200);
            this.btnEntry.Name = "btnEntry";
            this.btnEntry.Size = new System.Drawing.Size(100, 35);
            this.btnEntry.TabIndex = 1;
            this.btnEntry.Text = "确认进场";
            this.btnEntry.UseVisualStyleBackColor = true;
            this.btnEntry.Click += new System.EventHandler(this.btnEntry_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnCancel.Location = new System.Drawing.Point(220, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // VehicleEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 260);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEntry);
            this.Controls.Add(this.grpEntry);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VehicleEntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "车辆进场";
            this.Load += new System.EventHandler(this.VehicleEntryForm_Load);
            this.grpEntry.ResumeLayout(false);
            this.grpEntry.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpEntry;
        private System.Windows.Forms.DateTimePicker dtpEntryTime;
        private System.Windows.Forms.ComboBox cmbSpace;
        private System.Windows.Forms.TextBox txtPlateNumber;
        private System.Windows.Forms.Label lblEntryTime;
        private System.Windows.Forms.Label lblSpace;
        private System.Windows.Forms.Label lblPlateNumber;
        private System.Windows.Forms.Button btnEntry;
        private System.Windows.Forms.Button btnCancel;
    }
}
