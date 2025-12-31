namespace LibraryManagement.Forms
{
    partial class BorrowForm
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
            this.grpBorrow = new System.Windows.Forms.GroupBox();
            this.lblBookName = new System.Windows.Forms.Label();
            this.lblReaderName = new System.Windows.Forms.Label();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.dtpBorrowDate = new System.Windows.Forms.DateTimePicker();
            this.txtBookID = new System.Windows.Forms.TextBox();
            this.txtReaderID = new System.Windows.Forms.TextBox();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.lblBorrowDate = new System.Windows.Forms.Label();
            this.lblBookID = new System.Windows.Forms.Label();
            this.lblReaderID = new System.Windows.Forms.Label();
            this.btnBorrow = new System.Windows.Forms.Button();
            this.grpBorrow.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBorrow
            // 
            this.grpBorrow.Controls.Add(this.lblBookName);
            this.grpBorrow.Controls.Add(this.lblReaderName);
            this.grpBorrow.Controls.Add(this.dtpDueDate);
            this.grpBorrow.Controls.Add(this.dtpBorrowDate);
            this.grpBorrow.Controls.Add(this.txtBookID);
            this.grpBorrow.Controls.Add(this.txtReaderID);
            this.grpBorrow.Controls.Add(this.lblDueDate);
            this.grpBorrow.Controls.Add(this.lblBorrowDate);
            this.grpBorrow.Controls.Add(this.lblBookID);
            this.grpBorrow.Controls.Add(this.lblReaderID);
            this.grpBorrow.Location = new System.Drawing.Point(12, 12);
            this.grpBorrow.Name = "grpBorrow";
            this.grpBorrow.Size = new System.Drawing.Size(378, 160);
            this.grpBorrow.TabIndex = 0;
            this.grpBorrow.TabStop = false;
            this.grpBorrow.Text = "借阅信息";
            // 
            // lblBookName
            // 
            this.lblBookName.AutoSize = true;
            this.lblBookName.ForeColor = System.Drawing.Color.Blue;
            this.lblBookName.Location = new System.Drawing.Point(200, 65);
            this.lblBookName.Name = "lblBookName";
            this.lblBookName.Size = new System.Drawing.Size(0, 12);
            this.lblBookName.TabIndex = 0;
            // 
            // lblReaderName
            // 
            this.lblReaderName.AutoSize = true;
            this.lblReaderName.ForeColor = System.Drawing.Color.Blue;
            this.lblReaderName.Location = new System.Drawing.Point(200, 30);
            this.lblReaderName.Name = "lblReaderName";
            this.lblReaderName.Size = new System.Drawing.Size(0, 12);
            this.lblReaderName.TabIndex = 1;
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDueDate.Location = new System.Drawing.Point(270, 97);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(100, 21);
            this.dtpDueDate.TabIndex = 2;
            // 
            // dtpBorrowDate
            // 
            this.dtpBorrowDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBorrowDate.Location = new System.Drawing.Point(90, 97);
            this.dtpBorrowDate.Name = "dtpBorrowDate";
            this.dtpBorrowDate.Size = new System.Drawing.Size(100, 21);
            this.dtpBorrowDate.TabIndex = 3;
            // 
            // txtBookID
            // 
            this.txtBookID.Location = new System.Drawing.Point(90, 62);
            this.txtBookID.Name = "txtBookID";
            this.txtBookID.Size = new System.Drawing.Size(100, 21);
            this.txtBookID.TabIndex = 4;
            this.txtBookID.Leave += new System.EventHandler(this.txtBookID_Leave);
            // 
            // txtReaderID
            // 
            this.txtReaderID.Location = new System.Drawing.Point(90, 27);
            this.txtReaderID.Name = "txtReaderID";
            this.txtReaderID.Size = new System.Drawing.Size(100, 21);
            this.txtReaderID.TabIndex = 5;
            this.txtReaderID.Leave += new System.EventHandler(this.txtReaderID_Leave);
            // 
            // lblDueDate
            // 
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Location = new System.Drawing.Point(200, 100);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(65, 12);
            this.lblDueDate.TabIndex = 6;
            this.lblDueDate.Text = "应还日期：";
            // 
            // lblBorrowDate
            // 
            this.lblBorrowDate.AutoSize = true;
            this.lblBorrowDate.Location = new System.Drawing.Point(20, 100);
            this.lblBorrowDate.Name = "lblBorrowDate";
            this.lblBorrowDate.Size = new System.Drawing.Size(65, 12);
            this.lblBorrowDate.TabIndex = 7;
            this.lblBorrowDate.Text = "借阅日期：";
            // 
            // lblBookID
            // 
            this.lblBookID.AutoSize = true;
            this.lblBookID.Location = new System.Drawing.Point(20, 65);
            this.lblBookID.Name = "lblBookID";
            this.lblBookID.Size = new System.Drawing.Size(65, 12);
            this.lblBookID.TabIndex = 8;
            this.lblBookID.Text = "图书编号：";
            // 
            // lblReaderID
            // 
            this.lblReaderID.AutoSize = true;
            this.lblReaderID.Location = new System.Drawing.Point(20, 30);
            this.lblReaderID.Name = "lblReaderID";
            this.lblReaderID.Size = new System.Drawing.Size(65, 12);
            this.lblReaderID.TabIndex = 9;
            this.lblReaderID.Text = "读者证号：";
            // 
            // btnBorrow
            // 
            this.btnBorrow.Font = new System.Drawing.Font("宋体", 10F);
            this.btnBorrow.Location = new System.Drawing.Point(140, 190);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(100, 35);
            this.btnBorrow.TabIndex = 1;
            this.btnBorrow.Text = "确认借阅";
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);
            // 
            // BorrowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 249);
            this.Controls.Add(this.btnBorrow);
            this.Controls.Add(this.grpBorrow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BorrowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图书借阅";
            this.Load += new System.EventHandler(this.BorrowForm_Load);
            this.grpBorrow.ResumeLayout(false);
            this.grpBorrow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBorrow;
        private System.Windows.Forms.Label lblBookName;
        private System.Windows.Forms.Label lblReaderName;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.DateTimePicker dtpBorrowDate;
        private System.Windows.Forms.TextBox txtBookID;
        private System.Windows.Forms.TextBox txtReaderID;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.Label lblBorrowDate;
        private System.Windows.Forms.Label lblBookID;
        private System.Windows.Forms.Label lblReaderID;
        private System.Windows.Forms.Button btnBorrow;
    }
}
