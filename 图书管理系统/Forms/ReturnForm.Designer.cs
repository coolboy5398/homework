namespace LibraryManagement.Forms
{
    partial class ReturnForm
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
            this.grpReturn = new System.Windows.Forms.GroupBox();
            this.lblBookName = new System.Windows.Forms.Label();
            this.lblReaderName = new System.Windows.Forms.Label();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.txtBookID = new System.Windows.Forms.TextBox();
            this.txtReaderID = new System.Windows.Forms.TextBox();
            this.lblReturnDate = new System.Windows.Forms.Label();
            this.lblBookID = new System.Windows.Forms.Label();
            this.lblReaderID = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.grpReturn.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpReturn
            // 
            this.grpReturn.Controls.Add(this.lblBookName);
            this.grpReturn.Controls.Add(this.lblReaderName);
            this.grpReturn.Controls.Add(this.dtpReturnDate);
            this.grpReturn.Controls.Add(this.txtBookID);
            this.grpReturn.Controls.Add(this.txtReaderID);
            this.grpReturn.Controls.Add(this.lblReturnDate);
            this.grpReturn.Controls.Add(this.lblBookID);
            this.grpReturn.Controls.Add(this.lblReaderID);
            this.grpReturn.Location = new System.Drawing.Point(12, 12);
            this.grpReturn.Name = "grpReturn";
            this.grpReturn.Size = new System.Drawing.Size(360, 130);
            this.grpReturn.TabIndex = 0;
            this.grpReturn.Text = "归还信息";
            // 
            // lblReaderID
            // 
            this.lblReaderID.AutoSize = true;
            this.lblReaderID.Location = new System.Drawing.Point(20, 30);
            this.lblReaderID.Name = "lblReaderID";
            this.lblReaderID.Size = new System.Drawing.Size(65, 12);
            this.lblReaderID.Text = "读者证号：";
            // 
            // txtReaderID
            // 
            this.txtReaderID.Location = new System.Drawing.Point(90, 27);
            this.txtReaderID.Name = "txtReaderID";
            this.txtReaderID.Size = new System.Drawing.Size(100, 21);
            this.txtReaderID.Leave += new System.EventHandler(this.txtReaderID_Leave);
            // 
            // lblReaderName
            // 
            this.lblReaderName.AutoSize = true;
            this.lblReaderName.ForeColor = System.Drawing.Color.Blue;
            this.lblReaderName.Location = new System.Drawing.Point(200, 30);
            this.lblReaderName.Name = "lblReaderName";
            this.lblReaderName.Size = new System.Drawing.Size(0, 12);
            // 
            // lblBookID
            // 
            this.lblBookID.AutoSize = true;
            this.lblBookID.Location = new System.Drawing.Point(20, 65);
            this.lblBookID.Name = "lblBookID";
            this.lblBookID.Size = new System.Drawing.Size(65, 12);
            this.lblBookID.Text = "图书编号：";
            // 
            // txtBookID
            // 
            this.txtBookID.Location = new System.Drawing.Point(90, 62);
            this.txtBookID.Name = "txtBookID";
            this.txtBookID.Size = new System.Drawing.Size(100, 21);
            this.txtBookID.Leave += new System.EventHandler(this.txtBookID_Leave);
            // 
            // lblBookName
            // 
            this.lblBookName.AutoSize = true;
            this.lblBookName.ForeColor = System.Drawing.Color.Blue;
            this.lblBookName.Location = new System.Drawing.Point(200, 65);
            this.lblBookName.Name = "lblBookName";
            this.lblBookName.Size = new System.Drawing.Size(0, 12);
            // 
            // lblReturnDate
            // 
            this.lblReturnDate.AutoSize = true;
            this.lblReturnDate.Location = new System.Drawing.Point(20, 100);
            this.lblReturnDate.Name = "lblReturnDate";
            this.lblReturnDate.Size = new System.Drawing.Size(65, 12);
            this.lblReturnDate.Text = "归还日期：";
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReturnDate.Location = new System.Drawing.Point(90, 97);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(100, 21);
            // 
            // btnReturn
            // 
            this.btnReturn.Font = new System.Drawing.Font("宋体", 10F);
            this.btnReturn.Location = new System.Drawing.Point(140, 160);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(100, 35);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "确认归还";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // ReturnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.grpReturn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReturnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图书归还";
            this.Load += new System.EventHandler(this.ReturnForm_Load);
            this.grpReturn.ResumeLayout(false);
            this.grpReturn.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpReturn;
        private System.Windows.Forms.Label lblBookName;
        private System.Windows.Forms.Label lblReaderName;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        private System.Windows.Forms.TextBox txtBookID;
        private System.Windows.Forms.TextBox txtReaderID;
        private System.Windows.Forms.Label lblReturnDate;
        private System.Windows.Forms.Label lblBookID;
        private System.Windows.Forms.Label lblReaderID;
        private System.Windows.Forms.Button btnReturn;
    }
}
