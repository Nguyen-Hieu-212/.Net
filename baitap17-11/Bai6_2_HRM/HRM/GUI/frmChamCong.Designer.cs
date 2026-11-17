namespace HRM.GUI
{
    partial class frmChamCong
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
            this.btnTai = new System.Windows.Forms.Button();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.dgvChamCong = new System.Windows.Forms.DataGridView();
            this.btnLuu = new System.Windows.Forms.Button();
            this.lblGhiChu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamCong)).BeginInit();
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
            // btnTai
            // 
            this.btnTai.Location = new System.Drawing.Point(290, 14);
            this.btnTai.Name = "btnTai";
            this.btnTai.Size = new System.Drawing.Size(170, 30);
            this.btnTai.TabIndex = 4;
            this.btnTai.Text = "Nạp bảng chấm công";
            this.btnTai.UseVisualStyleBackColor = true;
            this.btnTai.Click += new System.EventHandler(this.btnTai_Click);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Location = new System.Drawing.Point(475, 20);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(400, 20);
            this.lblTrangThai.TabIndex = 5;
            this.lblTrangThai.Text = "";
            this.lblTrangThai.AutoSize = false;
            this.lblTrangThai.ForeColor = System.Drawing.Color.Firebrick;
            // 
            // dgvChamCong
            // 
            this.dgvChamCong.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
            this.dgvChamCong.Location = new System.Drawing.Point(15, 55);
            this.dgvChamCong.Name = "dgvChamCong";
            this.dgvChamCong.Size = new System.Drawing.Size(870, 440);
            this.dgvChamCong.TabIndex = 6;
            this.dgvChamCong.AllowUserToAddRows = false;
            this.dgvChamCong.AllowUserToDeleteRows = false;
            this.dgvChamCong.MultiSelect = false;
            this.dgvChamCong.RowHeadersVisible = false;
            this.dgvChamCong.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvChamCong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChamCong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChamCong.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvChamCong_DataError);
            this.dgvChamCong.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvChamCong_DataBindingComplete);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.btnLuu.Location = new System.Drawing.Point(15, 505);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(170, 38);
            this.btnLuu.TabIndex = 7;
            this.btnLuu.Text = "Lưu chấm công";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblGhiChu.Location = new System.Drawing.Point(200, 517);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(100, 15);
            this.lblGhiChu.TabIndex = 8;
            this.lblGhiChu.Text = "Nhập trực tiếp số ngày công và giờ OT trên lưới, sau đó bấm Lưu. Dòng màu xám = đã khóa.";
            this.lblGhiChu.AutoSize = true;
            // 
            // frmChamCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 560);
            this.Controls.Add(this.lblThang);
            this.Controls.Add(this.nudThang);
            this.Controls.Add(this.lblNam);
            this.Controls.Add(this.nudNam);
            this.Controls.Add(this.btnTai);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.dgvChamCong);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.lblGhiChu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmChamCong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chấm công tháng (Disconnected Model)";
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamCong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblThang;
        private System.Windows.Forms.NumericUpDown nudThang;
        private System.Windows.Forms.Label lblNam;
        private System.Windows.Forms.NumericUpDown nudNam;
        private System.Windows.Forms.Button btnTai;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.DataGridView dgvChamCong;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Label lblGhiChu;
    }
}
