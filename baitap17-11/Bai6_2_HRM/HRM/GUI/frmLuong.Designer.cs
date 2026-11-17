namespace HRM.GUI
{
    partial class frmLuong
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
            this.lblThang = new System.Windows.Forms.Label();
            this.nudThang = new System.Windows.Forms.NumericUpDown();
            this.lblNam = new System.Windows.Forms.Label();
            this.nudNam = new System.Windows.Forms.NumericUpDown();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnChot = new System.Windows.Forms.Button();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.dgvLuong = new System.Windows.Forms.DataGridView();
            this.lblTong = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // lblThang
            // 
            this.lblThang.Location = new System.Drawing.Point(15, 20);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(100, 15);
            this.lblThang.TabIndex = 0;
            this.lblThang.Text = "Tháng:";
            this.lblThang.AutoSize = true;
            // 
            // nudThang
            // 
            this.nudThang.Location = new System.Drawing.Point(70, 17);
            this.nudThang.Name = "nudThang";
            this.nudThang.Size = new System.Drawing.Size(60, 23);
            this.nudThang.TabIndex = 1;
            this.nudThang.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.nudThang.Maximum = new decimal(new int[] {12, 0, 0, 0});
            this.nudThang.Value = new decimal(new int[] {11, 0, 0, 0});
            // 
            // lblNam
            // 
            this.lblNam.Location = new System.Drawing.Point(150, 20);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(100, 15);
            this.lblNam.TabIndex = 2;
            this.lblNam.Text = "Năm:";
            this.lblNam.AutoSize = true;
            // 
            // nudNam
            // 
            this.nudNam.Location = new System.Drawing.Point(190, 17);
            this.nudNam.Name = "nudNam";
            this.nudNam.Size = new System.Drawing.Size(80, 23);
            this.nudNam.TabIndex = 3;
            this.nudNam.Minimum = new decimal(new int[] {2000, 0, 0, 0});
            this.nudNam.Maximum = new decimal(new int[] {2100, 0, 0, 0});
            this.nudNam.Value = new decimal(new int[] {2026, 0, 0, 0});
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(290, 14);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(160, 30);
            this.btnXem.TabIndex = 4;
            this.btnXem.Text = "Xem bảng lương";
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // btnChot
            // 
            this.btnChot.Location = new System.Drawing.Point(465, 14);
            this.btnChot.Name = "btnChot";
            this.btnChot.Size = new System.Drawing.Size(190, 30);
            this.btnChot.TabIndex = 5;
            this.btnChot.Text = "Chốt lương tháng";
            this.btnChot.UseVisualStyleBackColor = true;
            this.btnChot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChot.Click += new System.EventHandler(this.btnChot_Click);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Location = new System.Drawing.Point(670, 20);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(300, 20);
            this.lblTrangThai.TabIndex = 6;
            this.lblTrangThai.Text = "";
            this.lblTrangThai.AutoSize = false;
            this.lblTrangThai.ForeColor = System.Drawing.Color.Firebrick;
            // 
            // dgvLuong
            // 
            this.dgvLuong.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
            this.dgvLuong.Location = new System.Drawing.Point(15, 55);
            this.dgvLuong.Name = "dgvLuong";
            this.dgvLuong.Size = new System.Drawing.Size(950, 450);
            this.dgvLuong.TabIndex = 7;
            this.dgvLuong.AllowUserToAddRows = false;
            this.dgvLuong.AllowUserToDeleteRows = false;
            this.dgvLuong.MultiSelect = false;
            this.dgvLuong.RowHeadersVisible = false;
            this.dgvLuong.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvLuong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLuong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLuong.ReadOnly = true;
            // 
            // lblTong
            // 
            this.lblTong.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblTong.Location = new System.Drawing.Point(15, 520);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(600, 25);
            this.lblTong.TabIndex = 8;
            this.lblTong.Text = "Tổng thực lĩnh: 0 đ";
            this.lblTong.AutoSize = false;
            this.lblTong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            // 
            // frmLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 570);
            this.Controls.Add(this.lblThang);
            this.Controls.Add(this.nudThang);
            this.Controls.Add(this.lblNam);
            this.Controls.Add(this.nudNam);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.btnChot);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.dgvLuong);
            this.Controls.Add(this.lblTong);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmLuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tính lương & Chốt lương";
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblThang;
        private System.Windows.Forms.NumericUpDown nudThang;
        private System.Windows.Forms.Label lblNam;
        private System.Windows.Forms.NumericUpDown nudNam;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnChot;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.DataGridView dgvLuong;
        private System.Windows.Forms.Label lblTong;
    }
}
