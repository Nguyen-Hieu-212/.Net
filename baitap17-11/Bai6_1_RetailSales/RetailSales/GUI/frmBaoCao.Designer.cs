namespace RetailSales.GUI
{
    partial class frmBaoCao
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
            this.lblNgay = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.btnDTNgay = new System.Windows.Forms.Button();
            this.lblKQNgay = new System.Windows.Forms.Label();
            this.lblThang = new System.Windows.Forms.Label();
            this.nudThang = new System.Windows.Forms.NumericUpDown();
            this.lblNam = new System.Windows.Forms.Label();
            this.nudNam = new System.Windows.Forms.NumericUpDown();
            this.btnDTThang = new System.Windows.Forms.Button();
            this.lblKQThang = new System.Windows.Forms.Label();
            this.btnTop5 = new System.Windows.Forms.Button();
            this.dgvTop5 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTop5)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNgay
            // 
            this.lblNgay.Location = new System.Drawing.Point(15, 23);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(100, 15);
            this.lblNgay.TabIndex = 0;
            this.lblNgay.Text = "Ngày:";
            this.lblNgay.AutoSize = true;
            // 
            // dtpNgay
            // 
            this.dtpNgay.Location = new System.Drawing.Point(70, 20);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(130, 23);
            this.dtpNgay.TabIndex = 1;
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            // 
            // btnDTNgay
            // 
            this.btnDTNgay.Location = new System.Drawing.Point(215, 17);
            this.btnDTNgay.Name = "btnDTNgay";
            this.btnDTNgay.Size = new System.Drawing.Size(140, 30);
            this.btnDTNgay.TabIndex = 2;
            this.btnDTNgay.Text = "Doanh thu ngày";
            this.btnDTNgay.UseVisualStyleBackColor = true;
            this.btnDTNgay.Click += new System.EventHandler(this.btnDTNgay_Click);
            // 
            // lblKQNgay
            // 
            this.lblKQNgay.Location = new System.Drawing.Point(370, 21);
            this.lblKQNgay.Name = "lblKQNgay";
            this.lblKQNgay.Size = new System.Drawing.Size(380, 20);
            this.lblKQNgay.TabIndex = 3;
            this.lblKQNgay.Text = "";
            this.lblKQNgay.AutoSize = false;
            this.lblKQNgay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            // 
            // lblThang
            // 
            this.lblThang.Location = new System.Drawing.Point(15, 68);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(100, 15);
            this.lblThang.TabIndex = 4;
            this.lblThang.Text = "Tháng:";
            this.lblThang.AutoSize = true;
            // 
            // nudThang
            // 
            this.nudThang.Location = new System.Drawing.Point(70, 65);
            this.nudThang.Name = "nudThang";
            this.nudThang.Size = new System.Drawing.Size(60, 23);
            this.nudThang.TabIndex = 5;
            this.nudThang.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.nudThang.Maximum = new decimal(new int[] {12, 0, 0, 0});
            this.nudThang.Value = new decimal(new int[] {1, 0, 0, 0});
            // 
            // lblNam
            // 
            this.lblNam.Location = new System.Drawing.Point(145, 68);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(100, 15);
            this.lblNam.TabIndex = 6;
            this.lblNam.Text = "Năm:";
            this.lblNam.AutoSize = true;
            // 
            // nudNam
            // 
            this.nudNam.Location = new System.Drawing.Point(185, 65);
            this.nudNam.Name = "nudNam";
            this.nudNam.Size = new System.Drawing.Size(80, 23);
            this.nudNam.TabIndex = 7;
            this.nudNam.Minimum = new decimal(new int[] {2000, 0, 0, 0});
            this.nudNam.Maximum = new decimal(new int[] {2100, 0, 0, 0});
            this.nudNam.Value = new decimal(new int[] {2026, 0, 0, 0});
            // 
            // btnDTThang
            // 
            this.btnDTThang.Location = new System.Drawing.Point(280, 62);
            this.btnDTThang.Name = "btnDTThang";
            this.btnDTThang.Size = new System.Drawing.Size(140, 30);
            this.btnDTThang.TabIndex = 8;
            this.btnDTThang.Text = "Doanh thu tháng";
            this.btnDTThang.UseVisualStyleBackColor = true;
            this.btnDTThang.Click += new System.EventHandler(this.btnDTThang_Click);
            // 
            // lblKQThang
            // 
            this.lblKQThang.Location = new System.Drawing.Point(435, 66);
            this.lblKQThang.Name = "lblKQThang";
            this.lblKQThang.Size = new System.Drawing.Size(315, 20);
            this.lblKQThang.TabIndex = 9;
            this.lblKQThang.Text = "";
            this.lblKQThang.AutoSize = false;
            this.lblKQThang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            // 
            // btnTop5
            // 
            this.btnTop5.Location = new System.Drawing.Point(15, 110);
            this.btnTop5.Name = "btnTop5";
            this.btnTop5.Size = new System.Drawing.Size(230, 30);
            this.btnTop5.TabIndex = 10;
            this.btnTop5.Text = "Top 5 sản phẩm bán chạy nhất";
            this.btnTop5.UseVisualStyleBackColor = true;
            this.btnTop5.Click += new System.EventHandler(this.btnTop5_Click);
            // 
            // dgvTop5
            // 
            this.dgvTop5.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
            this.dgvTop5.Location = new System.Drawing.Point(15, 150);
            this.dgvTop5.Name = "dgvTop5";
            this.dgvTop5.Size = new System.Drawing.Size(730, 260);
            this.dgvTop5.TabIndex = 11;
            this.dgvTop5.AllowUserToAddRows = false;
            this.dgvTop5.AllowUserToDeleteRows = false;
            this.dgvTop5.MultiSelect = false;
            this.dgvTop5.RowHeadersVisible = false;
            this.dgvTop5.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvTop5.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTop5.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTop5.ReadOnly = true;
            // 
            // frmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 430);
            this.Controls.Add(this.lblNgay);
            this.Controls.Add(this.dtpNgay);
            this.Controls.Add(this.btnDTNgay);
            this.Controls.Add(this.lblKQNgay);
            this.Controls.Add(this.lblThang);
            this.Controls.Add(this.nudThang);
            this.Controls.Add(this.lblNam);
            this.Controls.Add(this.nudNam);
            this.Controls.Add(this.btnDTThang);
            this.Controls.Add(this.lblKQThang);
            this.Controls.Add(this.btnTop5);
            this.Controls.Add(this.dgvTop5);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmBaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo - Thống kê";
            this.Load += new System.EventHandler(this.frmBaoCao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTop5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.Button btnDTNgay;
        private System.Windows.Forms.Label lblKQNgay;
        private System.Windows.Forms.Label lblThang;
        private System.Windows.Forms.NumericUpDown nudThang;
        private System.Windows.Forms.Label lblNam;
        private System.Windows.Forms.NumericUpDown nudNam;
        private System.Windows.Forms.Button btnDTThang;
        private System.Windows.Forms.Label lblKQThang;
        private System.Windows.Forms.Button btnTop5;
        private System.Windows.Forms.DataGridView dgvTop5;
    }
}
