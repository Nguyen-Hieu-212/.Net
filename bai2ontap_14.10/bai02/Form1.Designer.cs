namespace bai02
{
    partial class Form1
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
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblMaPhieu = new System.Windows.Forms.Label();
            this.txtMaPhieu = new System.Windows.Forms.TextBox();
            this.lblNguoiYeuCau = new System.Windows.Forms.Label();
            this.txtNguoiYeuCau = new System.Windows.Forms.TextBox();
            this.lblNgayGhiNhan = new System.Windows.Forms.Label();
            this.dtpNgayGhiNhan = new System.Windows.Forms.DateTimePicker();
            this.lblMucDo = new System.Windows.Forms.Label();
            this.rdoThap = new System.Windows.Forms.RadioButton();
            this.rdoTrungBinh = new System.Windows.Forms.RadioButton();
            this.rdoKhanCap = new System.Windows.Forms.RadioButton();
            this.grpPhanLoai = new System.Windows.Forms.GroupBox();
            this.lblLoaiSuCo = new System.Windows.Forms.Label();
            this.cboLoaiSuCo = new System.Windows.Forms.ComboBox();
            this.lblThietBi = new System.Windows.Forms.Label();
            this.chkMayTinhBan = new System.Windows.Forms.CheckBox();
            this.chkLaptop = new System.Windows.Forms.CheckBox();
            this.chkMayIn = new System.Windows.Forms.CheckBox();
            this.chkDienThoai = new System.Windows.Forms.CheckBox();
            this.lblAnh = new System.Windows.Forms.Label();
            this.picAnhLoi = new System.Windows.Forms.PictureBox();
            this.btnTaiAnh = new System.Windows.Forms.Button();
            this.btnGui = new System.Windows.Forms.Button();
            this.btnNhapLai = new System.Windows.Forms.Button();
            this.grpThongTin.SuspendLayout();
            this.grpPhanLoai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhLoi)).BeginInit();
            this.SuspendLayout();
            //
            // grpThongTin
            //
            this.grpThongTin.Controls.Add(this.lblMaPhieu);
            this.grpThongTin.Controls.Add(this.txtMaPhieu);
            this.grpThongTin.Controls.Add(this.lblNguoiYeuCau);
            this.grpThongTin.Controls.Add(this.txtNguoiYeuCau);
            this.grpThongTin.Controls.Add(this.lblNgayGhiNhan);
            this.grpThongTin.Controls.Add(this.dtpNgayGhiNhan);
            this.grpThongTin.Controls.Add(this.lblMucDo);
            this.grpThongTin.Controls.Add(this.rdoThap);
            this.grpThongTin.Controls.Add(this.rdoTrungBinh);
            this.grpThongTin.Controls.Add(this.rdoKhanCap);
            this.grpThongTin.Location = new System.Drawing.Point(12, 12);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(616, 150);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin phiếu";
            //
            // lblMaPhieu
            //
            this.lblMaPhieu.AutoSize = true;
            this.lblMaPhieu.Location = new System.Drawing.Point(20, 32);
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.Text = "Mã phiếu:";
            //
            // txtMaPhieu
            //
            this.txtMaPhieu.Location = new System.Drawing.Point(130, 29);
            this.txtMaPhieu.Name = "txtMaPhieu";
            this.txtMaPhieu.Size = new System.Drawing.Size(180, 27);
            this.txtMaPhieu.TabIndex = 0;
            //
            // lblNguoiYeuCau
            //
            this.lblNguoiYeuCau.AutoSize = true;
            this.lblNguoiYeuCau.Location = new System.Drawing.Point(330, 32);
            this.lblNguoiYeuCau.Name = "lblNguoiYeuCau";
            this.lblNguoiYeuCau.Text = "Người yêu cầu:";
            //
            // txtNguoiYeuCau
            //
            this.txtNguoiYeuCau.Location = new System.Drawing.Point(440, 29);
            this.txtNguoiYeuCau.Name = "txtNguoiYeuCau";
            this.txtNguoiYeuCau.Size = new System.Drawing.Size(160, 27);
            this.txtNguoiYeuCau.TabIndex = 1;
            //
            // lblNgayGhiNhan
            //
            this.lblNgayGhiNhan.AutoSize = true;
            this.lblNgayGhiNhan.Location = new System.Drawing.Point(20, 72);
            this.lblNgayGhiNhan.Name = "lblNgayGhiNhan";
            this.lblNgayGhiNhan.Text = "Ngày ghi nhận:";
            //
            // dtpNgayGhiNhan
            //
            this.dtpNgayGhiNhan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayGhiNhan.Location = new System.Drawing.Point(130, 68);
            this.dtpNgayGhiNhan.Name = "dtpNgayGhiNhan";
            this.dtpNgayGhiNhan.Size = new System.Drawing.Size(180, 27);
            this.dtpNgayGhiNhan.TabIndex = 2;
            //
            // lblMucDo
            //
            this.lblMucDo.AutoSize = true;
            this.lblMucDo.Location = new System.Drawing.Point(20, 112);
            this.lblMucDo.Name = "lblMucDo";
            this.lblMucDo.Text = "Mức độ ưu tiên:";
            //
            // rdoThap
            //
            this.rdoThap.AutoSize = true;
            this.rdoThap.Location = new System.Drawing.Point(130, 110);
            this.rdoThap.Name = "rdoThap";
            this.rdoThap.TabIndex = 3;
            this.rdoThap.Text = "Thấp";
            //
            // rdoTrungBinh
            //
            this.rdoTrungBinh.AutoSize = true;
            this.rdoTrungBinh.Location = new System.Drawing.Point(230, 110);
            this.rdoTrungBinh.Name = "rdoTrungBinh";
            this.rdoTrungBinh.TabIndex = 4;
            this.rdoTrungBinh.Text = "Trung bình";
            //
            // rdoKhanCap
            //
            this.rdoKhanCap.AutoSize = true;
            this.rdoKhanCap.Location = new System.Drawing.Point(360, 110);
            this.rdoKhanCap.Name = "rdoKhanCap";
            this.rdoKhanCap.TabIndex = 5;
            this.rdoKhanCap.Text = "Khẩn cấp";
            //
            // grpPhanLoai
            //
            this.grpPhanLoai.Controls.Add(this.lblLoaiSuCo);
            this.grpPhanLoai.Controls.Add(this.cboLoaiSuCo);
            this.grpPhanLoai.Controls.Add(this.lblThietBi);
            this.grpPhanLoai.Controls.Add(this.chkMayTinhBan);
            this.grpPhanLoai.Controls.Add(this.chkLaptop);
            this.grpPhanLoai.Controls.Add(this.chkMayIn);
            this.grpPhanLoai.Controls.Add(this.chkDienThoai);
            this.grpPhanLoai.Controls.Add(this.lblAnh);
            this.grpPhanLoai.Controls.Add(this.picAnhLoi);
            this.grpPhanLoai.Controls.Add(this.btnTaiAnh);
            this.grpPhanLoai.Location = new System.Drawing.Point(12, 175);
            this.grpPhanLoai.Name = "grpPhanLoai";
            this.grpPhanLoai.Size = new System.Drawing.Size(616, 330);
            this.grpPhanLoai.TabIndex = 1;
            this.grpPhanLoai.TabStop = false;
            this.grpPhanLoai.Text = "Phân loại && Chi tiết";
            //
            // lblLoaiSuCo
            //
            this.lblLoaiSuCo.AutoSize = true;
            this.lblLoaiSuCo.Location = new System.Drawing.Point(20, 34);
            this.lblLoaiSuCo.Name = "lblLoaiSuCo";
            this.lblLoaiSuCo.Text = "Loại sự cố:";
            //
            // cboLoaiSuCo
            //
            this.cboLoaiSuCo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiSuCo.Location = new System.Drawing.Point(150, 31);
            this.cboLoaiSuCo.Name = "cboLoaiSuCo";
            this.cboLoaiSuCo.Size = new System.Drawing.Size(200, 28);
            this.cboLoaiSuCo.TabIndex = 6;
            //
            // lblThietBi
            //
            this.lblThietBi.AutoSize = true;
            this.lblThietBi.Location = new System.Drawing.Point(20, 77);
            this.lblThietBi.Name = "lblThietBi";
            this.lblThietBi.Text = "Thiết bị ảnh hưởng:";
            //
            // chkMayTinhBan
            //
            this.chkMayTinhBan.AutoSize = true;
            this.chkMayTinhBan.Location = new System.Drawing.Point(150, 75);
            this.chkMayTinhBan.Name = "chkMayTinhBan";
            this.chkMayTinhBan.TabIndex = 7;
            this.chkMayTinhBan.Text = "Máy tính bàn";
            //
            // chkLaptop
            //
            this.chkLaptop.AutoSize = true;
            this.chkLaptop.Location = new System.Drawing.Point(300, 75);
            this.chkLaptop.Name = "chkLaptop";
            this.chkLaptop.TabIndex = 8;
            this.chkLaptop.Text = "Laptop";
            //
            // chkMayIn
            //
            this.chkMayIn.AutoSize = true;
            this.chkMayIn.Location = new System.Drawing.Point(150, 104);
            this.chkMayIn.Name = "chkMayIn";
            this.chkMayIn.TabIndex = 9;
            this.chkMayIn.Text = "Máy in";
            //
            // chkDienThoai
            //
            this.chkDienThoai.AutoSize = true;
            this.chkDienThoai.Location = new System.Drawing.Point(300, 104);
            this.chkDienThoai.Name = "chkDienThoai";
            this.chkDienThoai.TabIndex = 10;
            this.chkDienThoai.Text = "Điện thoại";
            //
            // lblAnh
            //
            this.lblAnh.AutoSize = true;
            this.lblAnh.Location = new System.Drawing.Point(20, 150);
            this.lblAnh.Name = "lblAnh";
            this.lblAnh.Text = "Ảnh chụp lỗi:";
            //
            // picAnhLoi
            //
            this.picAnhLoi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAnhLoi.Location = new System.Drawing.Point(150, 145);
            this.picAnhLoi.Name = "picAnhLoi";
            this.picAnhLoi.Size = new System.Drawing.Size(300, 170);
            this.picAnhLoi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAnhLoi.TabStop = false;
            //
            // btnTaiAnh
            //
            this.btnTaiAnh.Location = new System.Drawing.Point(470, 145);
            this.btnTaiAnh.Name = "btnTaiAnh";
            this.btnTaiAnh.Size = new System.Drawing.Size(130, 34);
            this.btnTaiAnh.TabIndex = 11;
            this.btnTaiAnh.Text = "Tải ảnh lỗi";
            this.btnTaiAnh.UseVisualStyleBackColor = true;
            this.btnTaiAnh.Click += new System.EventHandler(this.btnTaiAnh_Click);
            //
            // btnGui
            //
            this.btnGui.Location = new System.Drawing.Point(388, 520);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(120, 38);
            this.btnGui.TabIndex = 12;
            this.btnGui.Text = "Gửi yêu cầu";
            this.btnGui.UseVisualStyleBackColor = true;
            this.btnGui.Click += new System.EventHandler(this.btnGui_Click);
            //
            // btnNhapLai
            //
            this.btnNhapLai.Location = new System.Drawing.Point(516, 520);
            this.btnNhapLai.Name = "btnNhapLai";
            this.btnNhapLai.Size = new System.Drawing.Size(112, 38);
            this.btnNhapLai.TabIndex = 13;
            this.btnNhapLai.Text = "Nhập lại";
            this.btnNhapLai.UseVisualStyleBackColor = true;
            this.btnNhapLai.Click += new System.EventHandler(this.btnNhapLai_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 575);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.grpPhanLoai);
            this.Controls.Add(this.btnGui);
            this.Controls.Add(this.btnNhapLai);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IT Support Ticket Form";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.grpPhanLoai.ResumeLayout(false);
            this.grpPhanLoai.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnhLoi)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblMaPhieu;
        private System.Windows.Forms.TextBox txtMaPhieu;
        private System.Windows.Forms.Label lblNguoiYeuCau;
        private System.Windows.Forms.TextBox txtNguoiYeuCau;
        private System.Windows.Forms.Label lblNgayGhiNhan;
        private System.Windows.Forms.DateTimePicker dtpNgayGhiNhan;
        private System.Windows.Forms.Label lblMucDo;
        private System.Windows.Forms.RadioButton rdoThap;
        private System.Windows.Forms.RadioButton rdoTrungBinh;
        private System.Windows.Forms.RadioButton rdoKhanCap;
        private System.Windows.Forms.GroupBox grpPhanLoai;
        private System.Windows.Forms.Label lblLoaiSuCo;
        private System.Windows.Forms.ComboBox cboLoaiSuCo;
        private System.Windows.Forms.Label lblThietBi;
        private System.Windows.Forms.CheckBox chkMayTinhBan;
        private System.Windows.Forms.CheckBox chkLaptop;
        private System.Windows.Forms.CheckBox chkMayIn;
        private System.Windows.Forms.CheckBox chkDienThoai;
        private System.Windows.Forms.Label lblAnh;
        private System.Windows.Forms.PictureBox picAnhLoi;
        private System.Windows.Forms.Button btnTaiAnh;
        private System.Windows.Forms.Button btnGui;
        private System.Windows.Forms.Button btnNhapLai;
    }
}