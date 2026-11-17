namespace RetailSales.GUI
{
    partial class frmBanHang
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
            this.lblNV = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblKH = new System.Windows.Forms.Label();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblQuet = new System.Windows.Forms.Label();
            this.txtMaVach = new System.Windows.Forms.TextBox();
            this.lblSP = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.lblSL = new System.Windows.Forms.Label();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnThemGio = new System.Windows.Forms.Button();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTongTxt = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblDiemTxt = new System.Windows.Forms.Label();
            this.lblDiem = new System.Windows.Forms.Label();
            this.btnThanhToan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNV
            // 
            this.lblNV.Location = new System.Drawing.Point(15, 18);
            this.lblNV.Name = "lblNV";
            this.lblNV.Size = new System.Drawing.Size(100, 15);
            this.lblNV.TabIndex = 0;
            this.lblNV.Text = "Nhân viên:";
            this.lblNV.AutoSize = true;
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.Location = new System.Drawing.Point(95, 15);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(200, 23);
            this.cboNhanVien.TabIndex = 1;
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.FormattingEnabled = true;
            // 
            // lblKH
            // 
            this.lblKH.Location = new System.Drawing.Point(320, 18);
            this.lblKH.Name = "lblKH";
            this.lblKH.Size = new System.Drawing.Size(100, 15);
            this.lblKH.TabIndex = 2;
            this.lblKH.Text = "Khách hàng:";
            this.lblKH.AutoSize = true;
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.Location = new System.Drawing.Point(405, 15);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(280, 23);
            this.cboKhachHang.TabIndex = 3;
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.SelectedIndexChanged += new System.EventHandler(this.cboKhachHang_SelectedIndexChanged);
            // 
            // lblQuet
            // 
            this.lblQuet.Location = new System.Drawing.Point(15, 58);
            this.lblQuet.Name = "lblQuet";
            this.lblQuet.Size = new System.Drawing.Size(100, 15);
            this.lblQuet.TabIndex = 4;
            this.lblQuet.Text = "Quét mã vạch:";
            this.lblQuet.AutoSize = true;
            // 
            // txtMaVach
            // 
            this.txtMaVach.Location = new System.Drawing.Point(110, 55);
            this.txtMaVach.Name = "txtMaVach";
            this.txtMaVach.Size = new System.Drawing.Size(170, 23);
            this.txtMaVach.TabIndex = 5;
            this.txtMaVach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaVach_KeyDown);
            // 
            // lblSP
            // 
            this.lblSP.Location = new System.Drawing.Point(300, 58);
            this.lblSP.Name = "lblSP";
            this.lblSP.Size = new System.Drawing.Size(100, 15);
            this.lblSP.TabIndex = 6;
            this.lblSP.Text = "Sản phẩm:";
            this.lblSP.AutoSize = true;
            // 
            // cboSanPham
            // 
            this.cboSanPham.Location = new System.Drawing.Point(370, 55);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(330, 23);
            this.cboSanPham.TabIndex = 7;
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.FormattingEnabled = true;
            // 
            // lblSL
            // 
            this.lblSL.Location = new System.Drawing.Point(715, 58);
            this.lblSL.Name = "lblSL";
            this.lblSL.Size = new System.Drawing.Size(100, 15);
            this.lblSL.TabIndex = 8;
            this.lblSL.Text = "SL:";
            this.lblSL.AutoSize = true;
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.Location = new System.Drawing.Point(745, 55);
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(60, 23);
            this.nudSoLuong.TabIndex = 9;
            this.nudSoLuong.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.nudSoLuong.Maximum = new decimal(new int[] {9999, 0, 0, 0});
            this.nudSoLuong.Value = new decimal(new int[] {1, 0, 0, 0});
            // 
            // btnThemGio
            // 
            this.btnThemGio.Location = new System.Drawing.Point(820, 52);
            this.btnThemGio.Name = "btnThemGio";
            this.btnThemGio.Size = new System.Drawing.Size(150, 30);
            this.btnThemGio.TabIndex = 10;
            this.btnThemGio.Text = "Thêm vào giỏ";
            this.btnThemGio.UseVisualStyleBackColor = true;
            this.btnThemGio.Click += new System.EventHandler(this.btnThemGio_Click);
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
            this.dgvGioHang.Location = new System.Drawing.Point(15, 95);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.Size = new System.Drawing.Size(970, 380);
            this.dgvGioHang.TabIndex = 11;
            this.dgvGioHang.AllowUserToAddRows = false;
            this.dgvGioHang.AllowUserToDeleteRows = false;
            this.dgvGioHang.MultiSelect = false;
            this.dgvGioHang.RowHeadersVisible = false;
            this.dgvGioHang.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvGioHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGioHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGioHang.ReadOnly = true;
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.btnXoaDong.Location = new System.Drawing.Point(15, 495);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Size = new System.Drawing.Size(130, 32);
            this.btnXoaDong.TabIndex = 12;
            this.btnXoaDong.Text = "Xóa dòng chọn";
            this.btnXoaDong.UseVisualStyleBackColor = true;
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.btnLamMoi.Location = new System.Drawing.Point(155, 495);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 32);
            this.btnLamMoi.TabIndex = 13;
            this.btnLamMoi.Text = "Làm mới giỏ";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // lblTongTxt
            // 
            this.lblTongTxt.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblTongTxt.Location = new System.Drawing.Point(480, 500);
            this.lblTongTxt.Name = "lblTongTxt";
            this.lblTongTxt.Size = new System.Drawing.Size(100, 15);
            this.lblTongTxt.TabIndex = 14;
            this.lblTongTxt.Text = "Tổng tiền:";
            this.lblTongTxt.AutoSize = true;
            this.lblTongTxt.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            // 
            // lblTongTien
            // 
            this.lblTongTien.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblTongTien.Location = new System.Drawing.Point(590, 497);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(200, 25);
            this.lblTongTien.TabIndex = 15;
            this.lblTongTien.Text = "0 đ";
            this.lblTongTien.AutoSize = false;
            this.lblTongTien.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            // 
            // lblDiemTxt
            // 
            this.lblDiemTxt.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblDiemTxt.Location = new System.Drawing.Point(480, 545);
            this.lblDiemTxt.Name = "lblDiemTxt";
            this.lblDiemTxt.Size = new System.Drawing.Size(100, 15);
            this.lblDiemTxt.TabIndex = 16;
            this.lblDiemTxt.Text = "Điểm tích lũy:";
            this.lblDiemTxt.AutoSize = true;
            this.lblDiemTxt.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            // 
            // lblDiem
            // 
            this.lblDiem.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblDiem.Location = new System.Drawing.Point(620, 545);
            this.lblDiem.Name = "lblDiem";
            this.lblDiem.Size = new System.Drawing.Size(170, 20);
            this.lblDiem.TabIndex = 17;
            this.lblDiem.Text = "0 điểm";
            this.lblDiem.AutoSize = false;
            this.lblDiem.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.btnThanhToan.Location = new System.Drawing.Point(810, 495);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(175, 70);
            this.btnThanhToan.TabIndex = 18;
            this.btnThanhToan.Text = "THANH TOÁN";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // frmBanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 610);
            this.Controls.Add(this.lblNV);
            this.Controls.Add(this.cboNhanVien);
            this.Controls.Add(this.lblKH);
            this.Controls.Add(this.cboKhachHang);
            this.Controls.Add(this.lblQuet);
            this.Controls.Add(this.txtMaVach);
            this.Controls.Add(this.lblSP);
            this.Controls.Add(this.cboSanPham);
            this.Controls.Add(this.lblSL);
            this.Controls.Add(this.nudSoLuong);
            this.Controls.Add(this.btnThemGio);
            this.Controls.Add(this.dgvGioHang);
            this.Controls.Add(this.btnXoaDong);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.lblTongTxt);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.lblDiemTxt);
            this.Controls.Add(this.lblDiem);
            this.Controls.Add(this.btnThanhToan);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmBanHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bán hàng / Lập hóa đơn";
            this.Load += new System.EventHandler(this.frmBanHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblNV;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblKH;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblQuet;
        private System.Windows.Forms.TextBox txtMaVach;
        private System.Windows.Forms.Label lblSP;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label lblSL;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Button btnThemGio;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTongTxt;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblDiemTxt;
        private System.Windows.Forms.Label lblDiem;
        private System.Windows.Forms.Button btnThanhToan;
    }
}
