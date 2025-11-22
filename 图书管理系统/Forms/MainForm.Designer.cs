namespace StudentGradeManagement.Forms
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClass = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTeacher = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCourse = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGradeEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGradeQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMyGrade = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStudent,
            this.menuClass,
            this.menuTeacher,
            this.menuCourse,
            this.menuGradeEntry,
            this.menuGradeQuery,
            this.menuMyGrade,
            this.menuLogout});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuStudent
            // 
            this.menuStudent.Name = "menuStudent";
            this.menuStudent.Size = new System.Drawing.Size(68, 21);
            this.menuStudent.Text = "学生管理";
            this.menuStudent.Click += new System.EventHandler(this.menuStudent_Click);
            // 
            // menuClass
            // 
            this.menuClass.Name = "menuClass";
            this.menuClass.Size = new System.Drawing.Size(68, 21);
            this.menuClass.Text = "班级管理";
            this.menuClass.Click += new System.EventHandler(this.menuClass_Click);
            // 
            // menuTeacher
            // 
            this.menuTeacher.Name = "menuTeacher";
            this.menuTeacher.Size = new System.Drawing.Size(68, 21);
            this.menuTeacher.Text = "教师管理";
            this.menuTeacher.Click += new System.EventHandler(this.menuTeacher_Click);
            // 
            // menuCourse
            // 
            this.menuCourse.Name = "menuCourse";
            this.menuCourse.Size = new System.Drawing.Size(68, 21);
            this.menuCourse.Text = "课程管理";
            this.menuCourse.Click += new System.EventHandler(this.menuCourse_Click);
            // 
            // menuGradeEntry
            // 
            this.menuGradeEntry.Name = "menuGradeEntry";
            this.menuGradeEntry.Size = new System.Drawing.Size(68, 21);
            this.menuGradeEntry.Text = "成绩录入";
            this.menuGradeEntry.Click += new System.EventHandler(this.menuGradeEntry_Click);
            // 
            // menuGradeQuery
            // 
            this.menuGradeQuery.Name = "menuGradeQuery";
            this.menuGradeQuery.Size = new System.Drawing.Size(68, 21);
            this.menuGradeQuery.Text = "成绩查询";
            this.menuGradeQuery.Click += new System.EventHandler(this.menuGradeQuery_Click);
            // 
            // menuMyGrade
            // 
            this.menuMyGrade.Name = "menuMyGrade";
            this.menuMyGrade.Size = new System.Drawing.Size(68, 21);
            this.menuMyGrade.Text = "我的成绩";
            this.menuMyGrade.Click += new System.EventHandler(this.menuMyGrade_Click);
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(68, 21);
            this.menuLogout.Text = "退出登录";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWelcome.Location = new System.Drawing.Point(30, 50);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(88, 25);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "欢迎您！";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "学生成绩管理系统 - 主菜单";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuStudent;
        private System.Windows.Forms.ToolStripMenuItem menuClass;
        private System.Windows.Forms.ToolStripMenuItem menuTeacher;
        private System.Windows.Forms.ToolStripMenuItem menuCourse;
        private System.Windows.Forms.ToolStripMenuItem menuGradeEntry;
        private System.Windows.Forms.ToolStripMenuItem menuGradeQuery;
        private System.Windows.Forms.ToolStripMenuItem menuMyGrade;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;
        private System.Windows.Forms.Label lblWelcome;
    }
}
