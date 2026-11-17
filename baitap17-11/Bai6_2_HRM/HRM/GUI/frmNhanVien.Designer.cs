namespace HRM.GUI
{
    partial class frmNhanVien
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
            this.lblTim = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblLoc = new System.Windows.Forms.Label();
            this.cboLocPB = new System.Windows.Forms.ComboBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.btnTatCa = new System.Windows.Forms.Button();
            this.dgvNhanVien = new System.Windows.Forms.DataGridView();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.lblPB = new System.Windows.Forms.Label();
            this.cboPhongBan = new System.Windows.Forms.ComboBox();
            this.lblLuong = new System.Windows.Forms.Label();
            this.txtLuongCoBan = new System.Windows.Forms.TextBox();
            this.lblHeSo = new System.Windows.Forms.Label();
            this.nudHeSo = new System.Windows.Forms.NumericUpDown();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnChuyenPB = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeSo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTim
            // 
            this.lblTim.Location = new System.Drawing.Point(15, 18);
            this.lblTim.Name = "lblTim";
            this.lblTim.Size = new System.Drawing.Size(100, 15);
            this.lblTim.TabIndex = 0;
            this.lblTim.Text = "Từ khóa (tên / CCCD):";
            this.lblTim.AutoSize = true;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(150, 15);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(200, 23);
            this.txtTimKiem.TabIndex = 1;
            // 
            // lblLoc
            // 
            this.lblLoc.Location = new System.Drawing.Point(370, 18);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(100, 15);
            this.lblLoc.TabIndex = 2;
            this.lblLoc.Text = "Phòng ban:";
            this.lblLoc.AutoSize = true;
            // 
            // cboLocPB
            // 
            this.cboLocPB.Location = new System.Drawing.Point(445, 15);
            this.cboLocPB.Name = "cboLocPB";
            this.cboLocPB.Size = new System.Drawing.Size(180, 23);
            this.cboLocPB.TabIndex = 3;
            this.cboLocPB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocPB.FormattingEnabled = true;
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(640, 13);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(80, 28);
            this.btnTim.TabIndex = 4;
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // btnTatCa
            // 
            this.btnTatCa.Location = new System.Drawing.Point(730, 13);
            this.btnTatCa.Name = "btnTatCa";
            this.btnTatCa.Size = new System.Drawing.Size(110, 28);
            this.btnTatCa.TabIndex = 5;
            this.btnTatCa.Text = "Hiện tất cả";
            this.btnTatCa.UseVisualStyleBackColor = true;
            this.btnTatCa.Click += new System.EventHandler(this.btnTatCa_Click);
            // 
            // dgvNhanVien
            // 
            this.dgvNhanVien.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom);
            this.dgvNhanVien.Location = new System.Drawing.Point(15, 50);
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.Size = new System.Drawing.Size(930, 290);
            this.dgvNhanVien.TabIndex = 6;
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.MultiSelect = false;
            this.dgvNhanVien.RowHeadersVisible = false;
            this.dgvNhanVien.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNhanVien_CellClick);
            // 
            // lblMaNV
            // 
            this.lblMaNV.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblMaNV.Location = new System.Drawing.Point(15, 368);
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Size = new System.Drawing.Size(100, 15);
            this.lblMaNV.TabIndex = 7;
            this.lblMaNV.Text = "Mã NV:";
            this.lblMaNV.AutoSize = true;
            // 
            // txtMaNV
            // 
            this.txtMaNV.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.txtMaNV.Location = new System.Drawing.Point(110, 365);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(100, 23);
            this.txtMaNV.TabIndex = 8;
            this.txtMaNV.ReadOnly = true;
            // 
            // lblHoTen
            // 
            this.lblHoTen.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblHoTen.Location = new System.Drawing.Point(240, 368);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(100, 15);
            this.lblHoTen.TabIndex = 9;
            this.lblHoTen.Text = "Họ tên:";
            this.lblHoTen.AutoSize = true;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.txtHoTen.Location = new System.Drawing.Point(310, 365);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(230, 23);
            this.txtHoTen.TabIndex = 10;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblNgaySinh.Location = new System.Drawing.Point(580, 368);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(100, 15);
            this.lblNgaySinh.TabIndex = 11;
            this.lblNgaySinh.Text = "Ngày sinh:";
            this.lblNgaySinh.AutoSize = true;
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.dtpNgaySinh.Location = new System.Drawing.Point(660, 365);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(140, 23);
            this.dtpNgaySinh.TabIndex = 12;
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            // 
            // lblCCCD
            // 
            this.lblCCCD.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblCCCD.Location = new System.Drawing.Point(15, 408);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(100, 15);
            this.lblCCCD.TabIndex = 13;
            this.lblCCCD.Text = "CCCD:";
            this.lblCCCD.AutoSize = true;
            // 
            // txtCCCD
            // 
            this.txtCCCD.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.txtCCCD.Location = new System.Drawing.Point(110, 405);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(150, 23);
            this.txtCCCD.TabIndex = 14;
            // 
            // lblPB
            // 
            this.lblPB.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblPB.Location = new System.Drawing.Point(290, 408);
            this.lblPB.Name = "lblPB";
            this.lblPB.Size = new System.Drawing.Size(100, 15);
            this.lblPB.TabIndex = 15;
            this.lblPB.Text = "Phòng ban:";
            this.lblPB.AutoSize = true;
            // 
            // cboPhongBan
            // 
            this.cboPhongBan.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.cboPhongBan.Location = new System.Drawing.Point(365, 405);
            this.cboPhongBan.Name = "cboPhongBan";
            this.cboPhongBan.Size = new System.Drawing.Size(230, 23);
            this.cboPhongBan.TabIndex = 16;
            this.cboPhongBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhongBan.FormattingEnabled = true;
            // 
            // lblLuong
            // 
            this.lblLuong.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblLuong.Location = new System.Drawing.Point(15, 448);
            this.lblLuong.Name = "lblLuong";
            this.lblLuong.Size = new System.Drawing.Size(100, 15);
            this.lblLuong.TabIndex = 17;
            this.lblLuong.Text = "Lương cơ bản:";
            this.lblLuong.AutoSize = true;
            // 
            // txtLuongCoBan
            // 
            this.txtLuongCoBan.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.txtLuongCoBan.Location = new System.Drawing.Point(110, 445);
            this.txtLuongCoBan.Name = "txtLuongCoBan";
            this.txtLuongCoBan.Size = new System.Drawing.Size(150, 23);
            this.txtLuongCoBan.TabIndex = 18;
            // 
            // lblHeSo
            // 
            this.lblHeSo.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblHeSo.Location = new System.Drawing.Point(290, 448);
            this.lblHeSo.Name = "lblHeSo";
            this.lblHeSo.Size = new System.Drawing.Size(100, 15);
            this.lblHeSo.TabIndex = 19;
            this.lblHeSo.Text = "Hệ số lương:";
            this.lblHeSo.AutoSize = true;
            // 
            // nudHeSo
            // 
            this.nudHeSo.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.nudHeSo.Location = new System.Drawing.Point(365, 445);
            this.nudHeSo.Name = "nudHeSo";
            this.nudHeSo.Size = new System.Drawing.Size(100, 23);
            this.nudHeSo.TabIndex = 20;
            this.nudHeSo.Minimum = new decimal(new int[] {10, 0, 0, 131072});
            this.nudHeSo.Maximum = new decimal(new int[] {2000, 0, 0, 131072});
            this.nudHeSo.Value = new decimal(new int[] {100, 0, 0, 131072});
            this.nudHeSo.DecimalPlaces = 2;
            this.nudHeSo.Increment = new decimal(new int[] {5, 0, 0, 131072});
            // 
            // btnThem
            // 
            this.btnThem.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.btnThem.Location = new System.Drawing.Point(15, 500);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 32);
            this.btnThem.TabIndex = 21;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.btnSua.Location = new System.Drawing.Point(125, 500);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 32);
            this.btnSua.TabIndex = 22;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.btnXoa.Location = new System.Drawing.Point(235, 500);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 32);
            this.btnXoa.TabIndex = 23;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnChuyenPB
            // 
            this.btnChuyenPB.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.btnChuyenPB.Location = new System.Drawing.Point(345, 500);
            this.btnChuyenPB.Name = "btnChuyenPB";
            this.btnChuyenPB.Size = new System.Drawing.Size(160, 32);
            this.btnChuyenPB.TabIndex = 24;
            this.btnChuyenPB.Text = "Chuyển phòng ban";
            this.btnChuyenPB.UseVisualStyleBackColor = true;
            this.btnChuyenPB.Click += new System.EventHandler(this.btnChuyenPB_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.btnLamMoi.Location = new System.Drawing.Point(515, 500);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 32);
            this.btnLamMoi.TabIndex = 25;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // frmNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 560);
            this.Controls.Add(this.lblTim);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.lblLoc);
            this.Controls.Add(this.cboLocPB);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.btnTatCa);
            this.Controls.Add(this.dgvNhanVien);
            this.Controls.Add(this.lblMaNV);
            this.Controls.Add(this.txtMaNV);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblNgaySinh);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.lblCCCD);
            this.Controls.Add(this.txtCCCD);
            this.Controls.Add(this.lblPB);
            this.Controls.Add(this.cboPhongBan);
            this.Controls.Add(this.lblLuong);
            this.Controls.Add(this.txtLuongCoBan);
            this.Controls.Add(this.lblHeSo);
            this.Controls.Add(this.nudHeSo);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnChuyenPB);
            this.Controls.Add(this.btnLamMoi);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmNhanVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Hồ sơ nhân sự";
            this.AcceptButton = this.btnTim;
            this.Load += new System.EventHandler(this.frmNhanVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeSo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTim;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.ComboBox cboLocPB;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Button btnTatCa;
        private System.Windows.Forms.DataGridView dgvNhanVien;
        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblCCCD;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.Label lblPB;
        private System.Windows.Forms.ComboBox cboPhongBan;
        private System.Windows.Forms.Label lblLuong;
        private System.Windows.Forms.TextBox txtLuongCoBan;
        private System.Windows.Forms.Label lblHeSo;
        private System.Windows.Forms.NumericUpDown nudHeSo;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnChuyenPB;
        private System.Windows.Forms.Button btnLamMoi;
    }
}
