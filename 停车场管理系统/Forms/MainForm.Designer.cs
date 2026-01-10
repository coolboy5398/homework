namespace ParkingManagement.Forms
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuParking = new System.Windows.Forms.ToolStripMenuItem();
            this.menuParkingSpace = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVehicle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVehicleEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVehicleExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRecordQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.grpOverview = new System.Windows.Forms.GroupBox();
            this.lblTodayIncomeTitle = new System.Windows.Forms.Label();
            this.lblTodayIncome = new System.Windows.Forms.Label();
            this.lblUsedSpacesTitle = new System.Windows.Forms.Label();
            this.lblUsedSpaces = new System.Windows.Forms.Label();
            this.lblFreeSpacesTitle = new System.Windows.Forms.Label();
            this.lblFreeSpaces = new System.Windows.Forms.Label();
            this.lblTotalSpacesTitle = new System.Windows.Forms.Label();
            this.lblTotalSpaces = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.grpOverview.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuParking,
            this.menuVehicle,
            this.menuQuery,
            this.menuSystem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(600, 25);
            this.menuStrip.TabIndex = 0;
            // 
            // menuParking
            // 
            this.menuParking.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuParkingSpace});
            this.menuParking.Name = "menuParking";
            this.menuParking.Size = new System.Drawing.Size(68, 21);
            this.menuParking.Text = "车位管理";
            // 
            // menuParkingSpace
            // 
            this.menuParkingSpace.Name = "menuParkingSpace";
            this.menuParkingSpace.Size = new System.Drawing.Size(180, 22);
            this.menuParkingSpace.Text = "车位信息管理";
            this.menuParkingSpace.Click += new System.EventHandler(this.menuParkingSpace_Click);
            // 
            // menuVehicle
            // 
            this.menuVehicle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuVehicleEntry,
            this.menuVehicleExit});
            this.menuVehicle.Name = "menuVehicle";
            this.menuVehicle.Size = new System.Drawing.Size(68, 21);
            this.menuVehicle.Text = "车辆管理";
            // 
            // menuVehicleEntry
            // 
            this.menuVehicleEntry.Name = "menuVehicleEntry";
            this.menuVehicleEntry.Size = new System.Drawing.Size(180, 22);
            this.menuVehicleEntry.Text = "车辆进场";
            this.menuVehicleEntry.Click += new System.EventHandler(this.menuVehicleEntry_Click);
            // 
            // menuVehicleExit
            // 
            this.menuVehicleExit.Name = "menuVehicleExit";
            this.menuVehicleExit.Size = new System.Drawing.Size(180, 22);
            this.menuVehicleExit.Text = "车辆出场";
            this.menuVehicleExit.Click += new System.EventHandler(this.menuVehicleExit_Click);
            // 
            // menuQuery
            // 
            this.menuQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRecordQuery});
            this.menuQuery.Name = "menuQuery";
            this.menuQuery.Size = new System.Drawing.Size(68, 21);
            this.menuQuery.Text = "查询统计";
            // 
            // menuRecordQuery
            // 
            this.menuRecordQuery.Name = "menuRecordQuery";
            this.menuRecordQuery.Size = new System.Drawing.Size(180, 22);
            this.menuRecordQuery.Text = "停车记录查询";
            this.menuRecordQuery.Click += new System.EventHandler(this.menuRecordQuery_Click);
            // 
            // menuSystem
            // 
            this.menuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLogout,
            this.menuExit});
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(68, 21);
            this.menuSystem.Text = "系统管理";
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(180, 22);
            this.menuLogout.Text = "退出登录";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(180, 22);
            this.menuExit.Text = "退出系统";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // grpOverview
            // 
            this.grpOverview.Controls.Add(this.lblTodayIncomeTitle);
            this.grpOverview.Controls.Add(this.lblTodayIncome);
            this.grpOverview.Controls.Add(this.lblUsedSpacesTitle);
            this.grpOverview.Controls.Add(this.lblUsedSpaces);
            this.grpOverview.Controls.Add(this.lblFreeSpacesTitle);
            this.grpOverview.Controls.Add(this.lblFreeSpaces);
            this.grpOverview.Controls.Add(this.lblTotalSpacesTitle);
            this.grpOverview.Controls.Add(this.lblTotalSpaces);
            this.grpOverview.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.grpOverview.Location = new System.Drawing.Point(50, 80);
            this.grpOverview.Name = "grpOverview";
            this.grpOverview.Size = new System.Drawing.Size(500, 200);
            this.grpOverview.TabIndex = 1;
            this.grpOverview.TabStop = false;
            this.grpOverview.Text = "车位概况";
            // 
            // lblTotalSpacesTitle
            // 
            this.lblTotalSpacesTitle.AutoSize = true;
            this.lblTotalSpacesTitle.Location = new System.Drawing.Point(50, 45);
            this.lblTotalSpacesTitle.Name = "lblTotalSpacesTitle";
            this.lblTotalSpacesTitle.Size = new System.Drawing.Size(79, 20);
            this.lblTotalSpacesTitle.TabIndex = 0;
            this.lblTotalSpacesTitle.Text = "总车位数：";
            // 
            // lblTotalSpaces
            // 
            this.lblTotalSpaces.AutoSize = true;
            this.lblTotalSpaces.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalSpaces.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalSpaces.Location = new System.Drawing.Point(135, 40);
            this.lblTotalSpaces.Name = "lblTotalSpaces";
            this.lblTotalSpaces.Size = new System.Drawing.Size(25, 26);
            this.lblTotalSpaces.TabIndex = 1;
            this.lblTotalSpaces.Text = "0";
            // 
            // lblFreeSpacesTitle
            // 
            this.lblFreeSpacesTitle.AutoSize = true;
            this.lblFreeSpacesTitle.Location = new System.Drawing.Point(280, 45);
            this.lblFreeSpacesTitle.Name = "lblFreeSpacesTitle";
            this.lblFreeSpacesTitle.Size = new System.Drawing.Size(79, 20);
            this.lblFreeSpacesTitle.TabIndex = 2;
            this.lblFreeSpacesTitle.Text = "空闲车位：";
            // 
            // lblFreeSpaces
            // 
            this.lblFreeSpaces.AutoSize = true;
            this.lblFreeSpaces.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.lblFreeSpaces.ForeColor = System.Drawing.Color.Green;
            this.lblFreeSpaces.Location = new System.Drawing.Point(365, 40);
            this.lblFreeSpaces.Name = "lblFreeSpaces";
            this.lblFreeSpaces.Size = new System.Drawing.Size(25, 26);
            this.lblFreeSpaces.TabIndex = 3;
            this.lblFreeSpaces.Text = "0";
            // 
            // lblUsedSpacesTitle
            // 
            this.lblUsedSpacesTitle.AutoSize = true;
            this.lblUsedSpacesTitle.Location = new System.Drawing.Point(50, 95);
            this.lblUsedSpacesTitle.Name = "lblUsedSpacesTitle";
            this.lblUsedSpacesTitle.Size = new System.Drawing.Size(79, 20);
            this.lblUsedSpacesTitle.TabIndex = 4;
            this.lblUsedSpacesTitle.Text = "占用车位：";
            // 
            // lblUsedSpaces
            // 
            this.lblUsedSpaces.AutoSize = true;
            this.lblUsedSpaces.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.lblUsedSpaces.ForeColor = System.Drawing.Color.Red;
            this.lblUsedSpaces.Location = new System.Drawing.Point(135, 90);
            this.lblUsedSpaces.Name = "lblUsedSpaces";
            this.lblUsedSpaces.Size = new System.Drawing.Size(25, 26);
            this.lblUsedSpaces.TabIndex = 5;
            this.lblUsedSpaces.Text = "0";
            // 
            // lblTodayIncomeTitle
            // 
            this.lblTodayIncomeTitle.AutoSize = true;
            this.lblTodayIncomeTitle.Location = new System.Drawing.Point(280, 95);
            this.lblTodayIncomeTitle.Name = "lblTodayIncomeTitle";
            this.lblTodayIncomeTitle.Size = new System.Drawing.Size(79, 20);
            this.lblTodayIncomeTitle.TabIndex = 6;
            this.lblTodayIncomeTitle.Text = "今日收入：";
            // 
            // lblTodayIncome
            // 
            this.lblTodayIncome.AutoSize = true;
            this.lblTodayIncome.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.lblTodayIncome.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblTodayIncome.Location = new System.Drawing.Point(365, 90);
            this.lblTodayIncome.Name = "lblTodayIncome";
            this.lblTodayIncome.Size = new System.Drawing.Size(68, 26);
            this.lblTodayIncome.TabIndex = 7;
            this.lblTodayIncome.Text = "0.00 元";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblWelcome.Location = new System.Drawing.Point(50, 40);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(90, 21);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "欢迎您，xxx";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnRefresh.Location = new System.Drawing.Point(250, 300);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "刷新数据";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 380);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.grpOverview);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "停车场管理系统";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.grpOverview.ResumeLayout(false);
            this.grpOverview.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuParking;
        private System.Windows.Forms.ToolStripMenuItem menuParkingSpace;
        private System.Windows.Forms.ToolStripMenuItem menuVehicle;
        private System.Windows.Forms.ToolStripMenuItem menuVehicleEntry;
        private System.Windows.Forms.ToolStripMenuItem menuVehicleExit;
        private System.Windows.Forms.ToolStripMenuItem menuQuery;
        private System.Windows.Forms.ToolStripMenuItem menuRecordQuery;
        private System.Windows.Forms.ToolStripMenuItem menuSystem;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.GroupBox grpOverview;
        private System.Windows.Forms.Label lblTotalSpacesTitle;
        private System.Windows.Forms.Label lblTotalSpaces;
        private System.Windows.Forms.Label lblFreeSpacesTitle;
        private System.Windows.Forms.Label lblFreeSpaces;
        private System.Windows.Forms.Label lblUsedSpacesTitle;
        private System.Windows.Forms.Label lblUsedSpaces;
        private System.Windows.Forms.Label lblTodayIncomeTitle;
        private System.Windows.Forms.Label lblTodayIncome;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnRefresh;
    }
}
