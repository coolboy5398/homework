namespace ParkingManagement.Forms
{
    partial class ParkingSpaceForm
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
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.cmbSpaceType = new System.Windows.Forms.ComboBox();
            this.txtSpaceName = new System.Windows.Forms.TextBox();
            this.txtSpaceID = new System.Windows.Forms.TextBox();
            this.lblSpaceType = new System.Windows.Forms.Label();
            this.lblSpaceName = new System.Windows.Forms.Label();
            this.lblSpaceID = new System.Windows.Forms.Label();
            this.dgvSpaces = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grpInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpaces)).BeginInit();
            this.SuspendLayout();
            // 
            // grpInput
            // 
            this.grpInput.Controls.Add(this.cmbSpaceType);
            this.grpInput.Controls.Add(this.txtSpaceName);
            this.grpInput.Controls.Add(this.txtSpaceID);
            this.grpInput.Controls.Add(this.lblSpaceType);
            this.grpInput.Controls.Add(this.lblSpaceName);
            this.grpInput.Controls.Add(this.lblSpaceID);
            this.grpInput.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.grpInput.Location = new System.Drawing.Point(20, 20);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(350, 150);
            this.grpInput.TabIndex = 0;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "车位信息";
            // 
            // lblSpaceID
            // 
            this.lblSpaceID.AutoSize = true;
            this.lblSpaceID.Location = new System.Drawing.Point(20, 35);
            this.lblSpaceID.Name = "lblSpaceID";
            this.lblSpaceID.Size = new System.Drawing.Size(79, 20);
            this.lblSpaceID.TabIndex = 0;
            this.lblSpaceID.Text = "车位编号：";
            // 
            // txtSpaceID
            // 
            this.txtSpaceID.Location = new System.Drawing.Point(105, 32);
            this.txtSpaceID.Name = "txtSpaceID";
            this.txtSpaceID.Size = new System.Drawing.Size(220, 25);
            this.txtSpaceID.TabIndex = 1;
            // 
            // lblSpaceName
            // 
            this.lblSpaceName.AutoSize = true;
            this.lblSpaceName.Location = new System.Drawing.Point(20, 70);
            this.lblSpaceName.Name = "lblSpaceName";
            this.lblSpaceName.Size = new System.Drawing.Size(79, 20);
            this.lblSpaceName.TabIndex = 2;
            this.lblSpaceName.Text = "车位名称：";
            // 
            // txtSpaceName
            // 
            this.txtSpaceName.Location = new System.Drawing.Point(105, 67);
            this.txtSpaceName.Name = "txtSpaceName";
            this.txtSpaceName.Size = new System.Drawing.Size(220, 25);
            this.txtSpaceName.TabIndex = 3;
            // 
            // lblSpaceType
            // 
            this.lblSpaceType.AutoSize = true;
            this.lblSpaceType.Location = new System.Drawing.Point(20, 105);
            this.lblSpaceType.Name = "lblSpaceType";
            this.lblSpaceType.Size = new System.Drawing.Size(79, 20);
            this.lblSpaceType.TabIndex = 4;
            this.lblSpaceType.Text = "车位类型：";
            // 
            // cmbSpaceType
            // 
            this.cmbSpaceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpaceType.FormattingEnabled = true;
            this.cmbSpaceType.Location = new System.Drawing.Point(105, 102);
            this.cmbSpaceType.Name = "cmbSpaceType";
            this.cmbSpaceType.Size = new System.Drawing.Size(220, 28);
            this.cmbSpaceType.TabIndex = 5;
            // 
            // dgvSpaces
            // 
            this.dgvSpaces.AllowUserToAddRows = false;
            this.dgvSpaces.AllowUserToDeleteRows = false;
            this.dgvSpaces.BackgroundColor = System.Drawing.Color.White;
            this.dgvSpaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpaces.Location = new System.Drawing.Point(20, 180);
            this.dgvSpaces.MultiSelect = false;
            this.dgvSpaces.Name = "dgvSpaces";
            this.dgvSpaces.ReadOnly = true;
            this.dgvSpaces.RowTemplate.Height = 25;
            this.dgvSpaces.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSpaces.Size = new System.Drawing.Size(560, 250);
            this.dgvSpaces.TabIndex = 1;
            this.dgvSpaces.SelectionChanged += new System.EventHandler(this.dgvSpaces_SelectionChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnAdd.Location = new System.Drawing.Point(400, 35);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 32);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnUpdate.Location = new System.Drawing.Point(500, 35);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 32);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnDelete.Location = new System.Drawing.Point(400, 85);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 32);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnRefresh.Location = new System.Drawing.Point(500, 85);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 32);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // ParkingSpaceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvSpaces);
            this.Controls.Add(this.grpInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParkingSpaceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "车位管理";
            this.Load += new System.EventHandler(this.ParkingSpaceForm_Load);
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpaces)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.ComboBox cmbSpaceType;
        private System.Windows.Forms.TextBox txtSpaceName;
        private System.Windows.Forms.TextBox txtSpaceID;
        private System.Windows.Forms.Label lblSpaceType;
        private System.Windows.Forms.Label lblSpaceName;
        private System.Windows.Forms.Label lblSpaceID;
        private System.Windows.Forms.DataGridView dgvSpaces;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
    }
}
