namespace LibraryManagement.Forms
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBasicInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBookManage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReaderManage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCategoryManage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBorrowManage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBorrow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBorrowQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSystem,
            this.menuBasicInfo,
            this.menuBorrowManage,
            this.menuQuery});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
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
            this.menuLogout.Size = new System.Drawing.Size(124, 22);
            this.menuLogout.Text = "退出登录";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(124, 22);
            this.menuExit.Text = "退出系统";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuBasicInfo
            // 
            this.menuBasicInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBookManage,
            this.menuReaderManage,
            this.menuCategoryManage});
            this.menuBasicInfo.Name = "menuBasicInfo";
            this.menuBasicInfo.Size = new System.Drawing.Size(68, 21);
            this.menuBasicInfo.Text = "基础信息";
            // 
            // menuBookManage
            // 
            this.menuBookManage.Name = "menuBookManage";
            this.menuBookManage.Size = new System.Drawing.Size(124, 22);
            this.menuBookManage.Text = "图书管理";
            this.menuBookManage.Click += new System.EventHandler(this.menuBookManage_Click);
            // 
            // menuReaderManage
            // 
            this.menuReaderManage.Name = "menuReaderManage";
            this.menuReaderManage.Size = new System.Drawing.Size(124, 22);
            this.menuReaderManage.Text = "读者管理";
            this.menuReaderManage.Click += new System.EventHandler(this.menuReaderManage_Click);
            // 
            // menuCategoryManage
            // 
            this.menuCategoryManage.Name = "menuCategoryManage";
            this.menuCategoryManage.Size = new System.Drawing.Size(124, 22);
            this.menuCategoryManage.Text = "分类管理";
            this.menuCategoryManage.Click += new System.EventHandler(this.menuCategoryManage_Click);
            // 
            // menuBorrowManage
            // 
            this.menuBorrowManage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBorrow,
            this.menuReturn});
            this.menuBorrowManage.Name = "menuBorrowManage";
            this.menuBorrowManage.Size = new System.Drawing.Size(68, 21);
            this.menuBorrowManage.Text = "借阅管理";
            // 
            // menuBorrow
            // 
            this.menuBorrow.Name = "menuBorrow";
            this.menuBorrow.Size = new System.Drawing.Size(124, 22);
            this.menuBorrow.Text = "图书借阅";
            this.menuBorrow.Click += new System.EventHandler(this.menuBorrow_Click);
            // 
            // menuReturn
            // 
            this.menuReturn.Name = "menuReturn";
            this.menuReturn.Size = new System.Drawing.Size(124, 22);
            this.menuReturn.Text = "图书归还";
            this.menuReturn.Click += new System.EventHandler(this.menuReturn_Click);
            // 
            // menuQuery
            // 
            this.menuQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBorrowQuery,
            this.menuStatistics});
            this.menuQuery.Name = "menuQuery";
            this.menuQuery.Size = new System.Drawing.Size(68, 21);
            this.menuQuery.Text = "查询统计";
            // 
            // menuBorrowQuery
            // 
            this.menuBorrowQuery.Name = "menuBorrowQuery";
            this.menuBorrowQuery.Size = new System.Drawing.Size(124, 22);
            this.menuBorrowQuery.Text = "借阅查询";
            this.menuBorrowQuery.Click += new System.EventHandler(this.menuBorrowQuery_Click);
            // 
            // menuStatistics
            // 
            this.menuStatistics.Name = "menuStatistics";
            this.menuStatistics.Size = new System.Drawing.Size(124, 22);
            this.menuStatistics.Text = "借阅统计";
            this.menuStatistics.Click += new System.EventHandler(this.menuStatistics_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lblWelcome.Location = new System.Drawing.Point(300, 200);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(200, 25);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "欢迎使用图书管理系统";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblRole.Location = new System.Drawing.Point(350, 240);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(100, 20);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "角色：管理员";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图书管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuSystem;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuBasicInfo;
        private System.Windows.Forms.ToolStripMenuItem menuBookManage;
        private System.Windows.Forms.ToolStripMenuItem menuReaderManage;
        private System.Windows.Forms.ToolStripMenuItem menuCategoryManage;
        private System.Windows.Forms.ToolStripMenuItem menuBorrowManage;
        private System.Windows.Forms.ToolStripMenuItem menuBorrow;
        private System.Windows.Forms.ToolStripMenuItem menuReturn;
        private System.Windows.Forms.ToolStripMenuItem menuQuery;
        private System.Windows.Forms.ToolStripMenuItem menuBorrowQuery;
        private System.Windows.Forms.ToolStripMenuItem menuStatistics;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblRole;
    }
}
