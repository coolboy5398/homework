namespace StudentGradeManagement.Forms
{
    partial class StudentGradeForm
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
            this.lblStudentInfo = new System.Windows.Forms.Label();
            this.lblSemester = new System.Windows.Forms.Label();
            this.cmbSemester = new System.Windows.Forms.ComboBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dgvGrades = new System.Windows.Forms.DataGridView();
            this.lblTotalCredits = new System.Windows.Forms.Label();
            this.lblGPA = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrades)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStudentInfo
            // 
            this.lblStudentInfo.AutoSize = true;
            this.lblStudentInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStudentInfo.Location = new System.Drawing.Point(20, 20);
            this.lblStudentInfo.Name = "lblStudentInfo";
            this.lblStudentInfo.Size = new System.Drawing.Size(200, 19);
            this.lblStudentInfo.TabIndex = 0;
            this.lblStudentInfo.Text = "学生：";
            // 
            // lblSemester
            // 
            this.lblSemester.AutoSize = true;
            this.lblSemester.Location = new System.Drawing.Point(20, 25);
            this.lblSemester.Name = "lblSemester";
            this.lblSemester.Size = new System.Drawing.Size(65, 17);
            this.lblSemester.TabIndex = 1;
            this.lblSemester.Text = "学期筛选：";
            // 
            // cmbSemester
            // 
            this.cmbSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSemester.FormattingEnabled = true;
            this.cmbSemester.Location = new System.Drawing.Point(91, 22);
            this.cmbSemester.Name = "cmbSemester";
            this.cmbSemester.Size = new System.Drawing.Size(200, 25);
            this.cmbSemester.TabIndex = 2;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(310, 20);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 30);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dgvGrades
            // 
            this.dgvGrades.AllowUserToAddRows = false;
            this.dgvGrades.AllowUserToDeleteRows = false;
            this.dgvGrades.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGrades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrades.Location = new System.Drawing.Point(20, 140);
            this.dgvGrades.Name = "dgvGrades";
            this.dgvGrades.ReadOnly = true;
            this.dgvGrades.RowHeadersWidth = 51;
            this.dgvGrades.RowTemplate.Height = 27;
            this.dgvGrades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGrades.Size = new System.Drawing.Size(760, 350);
            this.dgvGrades.TabIndex = 4;
            this.dgvGrades.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvGrades_CellFormatting);
            // 
            // lblTotalCredits
            // 
            this.lblTotalCredits.AutoSize = true;
            this.lblTotalCredits.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTotalCredits.Location = new System.Drawing.Point(20, 25);
            this.lblTotalCredits.Name = "lblTotalCredits";
            this.lblTotalCredits.Size = new System.Drawing.Size(100, 19);
            this.lblTotalCredits.TabIndex = 5;
            this.lblTotalCredits.Text = "总学分：0";
            // 
            // lblGPA
            // 
            this.lblGPA.AutoSize = true;
            this.lblGPA.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGPA.Location = new System.Drawing.Point(200, 25);
            this.lblGPA.Name = "lblGPA";
            this.lblGPA.Size = new System.Drawing.Size(80, 19);
            this.lblGPA.TabIndex = 6;
            this.lblGPA.Text = "GPA：--";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(680, 560);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSemester);
            this.groupBox1.Controls.Add(this.cmbSemester);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(20, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 70);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTotalCredits);
            this.groupBox2.Controls.Add(this.lblGPA);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(20, 500);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 60);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "统计信息";
            // 
            // StudentGradeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 610);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvGrades);
            this.Controls.Add(this.lblStudentInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StudentGradeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "学生成绩查询";
            this.Load += new System.EventHandler(this.StudentGradeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrades)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStudentInfo;
        private System.Windows.Forms.Label lblSemester;
        private System.Windows.Forms.ComboBox cmbSemester;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridView dgvGrades;
        private System.Windows.Forms.Label lblTotalCredits;
        private System.Windows.Forms.Label lblGPA;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
