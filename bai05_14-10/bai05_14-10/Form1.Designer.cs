using System.Drawing;
using System.Windows.Forms;

namespace bai05_14_10
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
            this.components = new System.ComponentModel.Container();
            DataGridViewCellStyle styleSoLuong = new DataGridViewCellStyle();
            DataGridViewCellStyle styleTrongLuong = new DataGridViewCellStyle();
            DataGridViewCellStyle styleDonGia = new DataGridViewCellStyle();
            DataGridViewCellStyle styleTien = new DataGridViewCellStyle();

            this.spcChinh = new System.Windows.Forms.SplitContainer();
            this.tabLeft = new System.Windows.Forms.TabControl();
            this.tabKhachHang = new System.Windows.Forms.TabPage();
            this.lblMaDon = new System.Windows.Forms.Label();
            this.txtMaDon = new System.Windows.Forms.TextBox();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.tabVanChuyen = new System.Windows.Forms.TabPage();
            this.lblLoaiVC = new System.Windows.Forms.Label();
            this.cboLoaiVanChuyen = new System.Windows.Forms.ComboBox();
            this.lblNgayGiao = new System.Windows.Forms.Label();
            this.dtpNgayGiao = new System.Windows.Forms.DateTimePicker();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colTenHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrongLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThanhTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tslThoiGian = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslTongSL = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslTongTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslTongTien = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrDongHo = new System.Windows.Forms.Timer(this.components);
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);

            ((System.ComponentModel.ISupportInitialize)(this.spcChinh)).BeginInit();
            this.spcChinh.Panel1.SuspendLayout();
            this.spcChinh.Panel2.SuspendLayout();
            this.spcChinh.SuspendLayout();
            this.tabLeft.SuspendLayout();
            this.tabKhachHang.SuspendLayout();
            this.tabVanChuyen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            //
            // spcChinh (chia 2 cột: Trái = thông tin, Phải = chi tiết hàng hóa)
            //
            this.spcChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcChinh.Location = new System.Drawing.Point(0, 0);
            this.spcChinh.Name = "spcChinh";
            this.spcChinh.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.spcChinh.Panel1.Controls.Add(this.tabLeft);
            this.spcChinh.Panel1.Padding = new System.Windows.Forms.Padding(6);
            this.spcChinh.Panel2.Controls.Add(this.dgvChiTiet);
            this.spcChinh.Panel2.Controls.Add(this.lblHuongDan);
            this.spcChinh.Panel2.Padding = new System.Windows.Forms.Padding(6);
            this.spcChinh.Size = new System.Drawing.Size(1000, 538);
            this.spcChinh.SplitterDistance = 340;
            this.spcChinh.TabIndex = 0;
            //
            // tabLeft
            //
            this.tabLeft.Controls.Add(this.tabKhachHang);
            this.tabLeft.Controls.Add(this.tabVanChuyen);
            this.tabLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLeft.Name = "tabLeft";
            this.tabLeft.SelectedIndex = 0;
            this.tabLeft.TabIndex = 0;
            //
            // tabKhachHang
            //
            this.tabKhachHang.Controls.Add(this.lblMaDon);
            this.tabKhachHang.Controls.Add(this.txtMaDon);
            this.tabKhachHang.Controls.Add(this.lblTenKH);
            this.tabKhachHang.Controls.Add(this.txtTenKH);
            this.tabKhachHang.Controls.Add(this.lblSDT);
            this.tabKhachHang.Controls.Add(this.txtSDT);
            this.tabKhachHang.Controls.Add(this.lblDiaChi);
            this.tabKhachHang.Controls.Add(this.txtDiaChi);
            this.tabKhachHang.Name = "tabKhachHang";
            this.tabKhachHang.Padding = new System.Windows.Forms.Padding(3);
            this.tabKhachHang.Text = "Thông tin khách hàng";
            this.tabKhachHang.UseVisualStyleBackColor = true;
            //
            // lblMaDon
            //
            this.lblMaDon.AutoSize = true;
            this.lblMaDon.Location = new System.Drawing.Point(12, 25);
            this.lblMaDon.Name = "lblMaDon";
            this.lblMaDon.Text = "Mã đơn:";
            //
            // txtMaDon
            //
            this.txtMaDon.Location = new System.Drawing.Point(115, 22);
            this.txtMaDon.Name = "txtMaDon";
            this.txtMaDon.ReadOnly = true;
            this.txtMaDon.Size = new System.Drawing.Size(190, 27);
            this.txtMaDon.TabIndex = 0;
            //
            // lblTenKH
            //
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Location = new System.Drawing.Point(12, 68);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Text = "Khách hàng:";
            //
            // txtTenKH
            //
            this.txtTenKH.Location = new System.Drawing.Point(115, 65);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(190, 27);
            this.txtTenKH.TabIndex = 1;
            this.txtTenKH.Validating += new System.ComponentModel.CancelEventHandler(this.txtTenKH_Validating);
            //
            // lblSDT
            //
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(12, 111);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Text = "Số điện thoại:";
            //
            // txtSDT
            //
            this.txtSDT.Location = new System.Drawing.Point(115, 108);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(190, 27);
            this.txtSDT.TabIndex = 2;
            this.txtSDT.Validating += new System.ComponentModel.CancelEventHandler(this.txtSDT_Validating);
            //
            // lblDiaChi
            //
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(12, 154);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Text = "Địa chỉ giao:";
            //
            // txtDiaChi
            //
            this.txtDiaChi.Location = new System.Drawing.Point(115, 151);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(190, 80);
            this.txtDiaChi.TabIndex = 3;
            //
            // tabVanChuyen
            //
            this.tabVanChuyen.Controls.Add(this.lblLoaiVC);
            this.tabVanChuyen.Controls.Add(this.cboLoaiVanChuyen);
            this.tabVanChuyen.Controls.Add(this.lblNgayGiao);
            this.tabVanChuyen.Controls.Add(this.dtpNgayGiao);
            this.tabVanChuyen.Controls.Add(this.lblGhiChu);
            this.tabVanChuyen.Controls.Add(this.txtGhiChu);
            this.tabVanChuyen.Name = "tabVanChuyen";
            this.tabVanChuyen.Padding = new System.Windows.Forms.Padding(3);
            this.tabVanChuyen.Text = "Loại vận chuyển";
            this.tabVanChuyen.UseVisualStyleBackColor = true;
            //
            // lblLoaiVC
            //
            this.lblLoaiVC.AutoSize = true;
            this.lblLoaiVC.Location = new System.Drawing.Point(12, 25);
            this.lblLoaiVC.Name = "lblLoaiVC";
            this.lblLoaiVC.Text = "Loại vận chuyển:";
            //
            // cboLoaiVanChuyen
            //
            this.cboLoaiVanChuyen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiVanChuyen.Location = new System.Drawing.Point(15, 50);
            this.cboLoaiVanChuyen.Name = "cboLoaiVanChuyen";
            this.cboLoaiVanChuyen.Size = new System.Drawing.Size(290, 28);
            this.cboLoaiVanChuyen.TabIndex = 0;
            //
            // lblNgayGiao
            //
            this.lblNgayGiao.AutoSize = true;
            this.lblNgayGiao.Location = new System.Drawing.Point(12, 98);
            this.lblNgayGiao.Name = "lblNgayGiao";
            this.lblNgayGiao.Text = "Ngày giao dự kiến:";
            //
            // dtpNgayGiao
            //
            this.dtpNgayGiao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayGiao.Location = new System.Drawing.Point(15, 123);
            this.dtpNgayGiao.Name = "dtpNgayGiao";
            this.dtpNgayGiao.Size = new System.Drawing.Size(290, 27);
            this.dtpNgayGiao.TabIndex = 1;
            //
            // lblGhiChu
            //
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(12, 171);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Text = "Ghi chú:";
            //
            // txtGhiChu
            //
            this.txtGhiChu.Location = new System.Drawing.Point(15, 196);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(290, 90);
            this.txtGhiChu.TabIndex = 2;
            //
            // dgvChiTiet
            //
            styleSoLuong.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            styleTrongLuong.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            styleDonGia.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            styleTien.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            styleTien.BackColor = System.Drawing.Color.LemonChiffon;
            styleTien.Format = "N0";

            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.AllowUserToResizeRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colTenHang, this.colSoLuong, this.colTrongLuong, this.colDonGia, this.colThanhTien });
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgvChiTiet.MultiSelect = false;
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersWidth = 30;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.TabIndex = 0;
            this.dgvChiTiet.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellValueChanged);
            this.dgvChiTiet.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvChiTiet_RowsRemoved);
            this.dgvChiTiet.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvChiTiet_DataError);
            //
            // colTenHang
            //
            this.colTenHang.FillWeight = 160F;
            this.colTenHang.HeaderText = "Tên hàng";
            this.colTenHang.Name = "colTenHang";
            //
            // colSoLuong
            //
            this.colSoLuong.DefaultCellStyle = styleSoLuong;
            this.colSoLuong.FillWeight = 70F;
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.Name = "colSoLuong";
            //
            // colTrongLuong
            //
            this.colTrongLuong.DefaultCellStyle = styleTrongLuong;
            this.colTrongLuong.FillWeight = 85F;
            this.colTrongLuong.HeaderText = "Trọng lượng (kg)";
            this.colTrongLuong.Name = "colTrongLuong";
            //
            // colDonGia
            //
            this.colDonGia.DefaultCellStyle = styleDonGia;
            this.colDonGia.FillWeight = 90F;
            this.colDonGia.HeaderText = "Đơn giá";
            this.colDonGia.Name = "colDonGia";
            //
            // colThanhTien
            //
            this.colThanhTien.DefaultCellStyle = styleTien;
            this.colThanhTien.FillWeight = 100F;
            this.colThanhTien.HeaderText = "Thành tiền";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.ReadOnly = true;
            //
            // lblHuongDan
            //
            this.lblHuongDan.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHuongDan.ForeColor = System.Drawing.Color.DimGray;
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(100, 28);
            this.lblHuongDan.Text = "Phím tắt: F2 = thêm dòng mới  |  Delete = xóa dòng đang chọn";
            this.lblHuongDan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // statusStrip
            //
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.tslThoiGian, this.tslTongSL, this.tslTongTL, this.tslTongTien });
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.TabIndex = 1;
            //
            // tslThoiGian
            //
            this.tslThoiGian.Name = "tslThoiGian";
            this.tslThoiGian.Spring = true;
            this.tslThoiGian.Text = "Thời gian";
            this.tslThoiGian.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // tslTongSL
            //
            this.tslTongSL.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tslTongSL.Name = "tslTongSL";
            this.tslTongSL.Text = "Tổng số lượng: 0";
            //
            // tslTongTL
            //
            this.tslTongTL.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tslTongTL.Name = "tslTongTL";
            this.tslTongTL.Text = "Tổng trọng lượng: 0 kg";
            //
            // tslTongTien
            //
            this.tslTongTien.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.tslTongTien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tslTongTien.ForeColor = System.Drawing.Color.Firebrick;
            this.tslTongTien.Name = "tslTongTien";
            this.tslTongTien.Text = "Tổng tiền: 0đ";
            //
            // tmrDongHo
            //
            this.tmrDongHo.Interval = 1000;
            this.tmrDongHo.Tick += new System.EventHandler(this.tmrDongHo_Tick);
            //
            // errorProvider
            //
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 565);
            this.Controls.Add(this.spcChinh);
            this.Controls.Add(this.statusStrip);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(860, 480);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delivery Order Dashboard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);

            this.spcChinh.Panel1.ResumeLayout(false);
            this.spcChinh.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcChinh)).EndInit();
            this.spcChinh.ResumeLayout(false);
            this.tabLeft.ResumeLayout(false);
            this.tabKhachHang.ResumeLayout(false);
            this.tabKhachHang.PerformLayout();
            this.tabVanChuyen.ResumeLayout(false);
            this.tabVanChuyen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.SplitContainer spcChinh;
        private System.Windows.Forms.TabControl tabLeft;
        private System.Windows.Forms.TabPage tabKhachHang;
        private System.Windows.Forms.Label lblMaDon;
        private System.Windows.Forms.TextBox txtMaDon;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TabPage tabVanChuyen;
        private System.Windows.Forms.Label lblLoaiVC;
        private System.Windows.Forms.ComboBox cboLoaiVanChuyen;
        private System.Windows.Forms.Label lblNgayGiao;
        private System.Windows.Forms.DateTimePicker dtpNgayGiao;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrongLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThanhTien;
        private System.Windows.Forms.Label lblHuongDan;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tslThoiGian;
        private System.Windows.Forms.ToolStripStatusLabel tslTongSL;
        private System.Windows.Forms.ToolStripStatusLabel tslTongTL;
        private System.Windows.Forms.ToolStripStatusLabel tslTongTien;
        private System.Windows.Forms.Timer tmrDongHo;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}