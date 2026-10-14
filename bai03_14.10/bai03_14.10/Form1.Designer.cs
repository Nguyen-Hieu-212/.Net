namespace bai03_14
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
            this.grpNhapLieu = new System.Windows.Forms.GroupBox();
            this.lblMaVT = new System.Windows.Forms.Label();
            this.txtMaVT = new System.Windows.Forms.TextBox();
            this.lblTenVT = new System.Windows.Forms.Label();
            this.txtTenVT = new System.Windows.Forms.TextBox();
            this.lblDonViTinh = new System.Windows.Forms.Label();
            this.cboDonViTinh = new System.Windows.Forms.ComboBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.grpDanhSach = new System.Windows.Forms.GroupBox();
            this.lvVatTu = new System.Windows.Forms.ListView();
            this.colMaVT = new System.Windows.Forms.ColumnHeader();
            this.colTenVT = new System.Windows.Forms.ColumnHeader();
            this.colDVT = new System.Windows.Forms.ColumnHeader();
            this.colDonGia = new System.Windows.Forms.ColumnHeader();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.btnXoaTatCa = new System.Windows.Forms.Button();
            this.grpNhapLieu.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            this.SuspendLayout();
            //
            // grpNhapLieu
            //
            this.grpNhapLieu.Controls.Add(this.lblMaVT);
            this.grpNhapLieu.Controls.Add(this.txtMaVT);
            this.grpNhapLieu.Controls.Add(this.lblTenVT);
            this.grpNhapLieu.Controls.Add(this.txtTenVT);
            this.grpNhapLieu.Controls.Add(this.lblDonViTinh);
            this.grpNhapLieu.Controls.Add(this.cboDonViTinh);
            this.grpNhapLieu.Controls.Add(this.lblDonGia);
            this.grpNhapLieu.Controls.Add(this.txtDonGia);
            this.grpNhapLieu.Location = new System.Drawing.Point(12, 12);
            this.grpNhapLieu.Name = "grpNhapLieu";
            this.grpNhapLieu.Size = new System.Drawing.Size(300, 225);
            this.grpNhapLieu.TabIndex = 0;
            this.grpNhapLieu.TabStop = false;
            this.grpNhapLieu.Text = "Thông tin vật tư";
            //
            // lblMaVT
            //
            this.lblMaVT.AutoSize = true;
            this.lblMaVT.Location = new System.Drawing.Point(15, 38);
            this.lblMaVT.Name = "lblMaVT";
            this.lblMaVT.Text = "Mã vật tư:";
            //
            // txtMaVT
            //
            this.txtMaVT.Location = new System.Drawing.Point(115, 35);
            this.txtMaVT.Name = "txtMaVT";
            this.txtMaVT.Size = new System.Drawing.Size(170, 27);
            this.txtMaVT.TabIndex = 0;
            //
            // lblTenVT
            //
            this.lblTenVT.AutoSize = true;
            this.lblTenVT.Location = new System.Drawing.Point(15, 83);
            this.lblTenVT.Name = "lblTenVT";
            this.lblTenVT.Text = "Tên vật tư:";
            //
            // txtTenVT
            //
            this.txtTenVT.Location = new System.Drawing.Point(115, 80);
            this.txtTenVT.Name = "txtTenVT";
            this.txtTenVT.Size = new System.Drawing.Size(170, 27);
            this.txtTenVT.TabIndex = 1;
            //
            // lblDonViTinh
            //
            this.lblDonViTinh.AutoSize = true;
            this.lblDonViTinh.Location = new System.Drawing.Point(15, 128);
            this.lblDonViTinh.Name = "lblDonViTinh";
            this.lblDonViTinh.Text = "Đơn vị tính:";
            //
            // cboDonViTinh
            //
            this.cboDonViTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDonViTinh.Items.AddRange(new object[] { "Cái", "Bộ", "Kg", "Mét" });
            this.cboDonViTinh.Location = new System.Drawing.Point(115, 125);
            this.cboDonViTinh.Name = "cboDonViTinh";
            this.cboDonViTinh.Size = new System.Drawing.Size(170, 28);
            this.cboDonViTinh.TabIndex = 2;
            //
            // lblDonGia
            //
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new System.Drawing.Point(15, 173);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Text = "Đơn giá nhập:";
            //
            // txtDonGia
            //
            this.txtDonGia.Location = new System.Drawing.Point(115, 170);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(170, 27);
            this.txtDonGia.TabIndex = 3;
            //
            // grpDanhSach
            //
            this.grpDanhSach.Controls.Add(this.lvVatTu);
            this.grpDanhSach.Location = new System.Drawing.Point(330, 12);
            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Size = new System.Drawing.Size(478, 326);
            this.grpDanhSach.TabIndex = 1;
            this.grpDanhSach.TabStop = false;
            this.grpDanhSach.Text = "Danh sách vật tư";
            //
            // lvVatTu
            //
            this.lvVatTu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colMaVT, this.colTenVT, this.colDVT, this.colDonGia });
            this.lvVatTu.FullRowSelect = true;
            this.lvVatTu.GridLines = true;
            this.lvVatTu.HideSelection = false;
            this.lvVatTu.Location = new System.Drawing.Point(10, 26);
            this.lvVatTu.MultiSelect = false;
            this.lvVatTu.Name = "lvVatTu";
            this.lvVatTu.Size = new System.Drawing.Size(458, 290);
            this.lvVatTu.TabIndex = 8;
            this.lvVatTu.UseCompatibleStateImageBehavior = false;
            this.lvVatTu.View = System.Windows.Forms.View.Details;
            this.lvVatTu.SelectedIndexChanged += new System.EventHandler(this.lvVatTu_SelectedIndexChanged);
            //
            // colMaVT
            //
            this.colMaVT.Text = "Mã VT";
            this.colMaVT.Width = 75;
            //
            // colTenVT
            //
            this.colTenVT.Text = "Tên VT";
            this.colTenVT.Width = 170;
            //
            // colDVT
            //
            this.colDVT.Text = "Đơn vị tính";
            this.colDVT.Width = 95;
            //
            // colDonGia
            //
            this.colDonGia.Text = "Đơn giá";
            this.colDonGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colDonGia.Width = 105;
            //
            // btnThem
            //
            this.btnThem.Location = new System.Drawing.Point(12, 252);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(145, 38);
            this.btnThem.TabIndex = 4;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            //
            // btnCapNhat
            //
            this.btnCapNhat.Location = new System.Drawing.Point(167, 252);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(145, 38);
            this.btnCapNhat.TabIndex = 5;
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = true;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            //
            // btnXoaDong
            //
            this.btnXoaDong.Location = new System.Drawing.Point(12, 300);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Size = new System.Drawing.Size(145, 38);
            this.btnXoaDong.TabIndex = 6;
            this.btnXoaDong.Text = "Xóa dòng";
            this.btnXoaDong.UseVisualStyleBackColor = true;
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);
            //
            // btnXoaTatCa
            //
            this.btnXoaTatCa.Location = new System.Drawing.Point(167, 300);
            this.btnXoaTatCa.Name = "btnXoaTatCa";
            this.btnXoaTatCa.Size = new System.Drawing.Size(145, 38);
            this.btnXoaTatCa.TabIndex = 7;
            this.btnXoaTatCa.Text = "Xóa toàn bộ";
            this.btnXoaTatCa.UseVisualStyleBackColor = true;
            this.btnXoaTatCa.Click += new System.EventHandler(this.btnXoaTatCa_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 352);
            this.Controls.Add(this.grpNhapLieu);
            this.Controls.Add(this.grpDanhSach);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.btnXoaDong);
            this.Controls.Add(this.btnXoaTatCa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item List Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpNhapLieu.ResumeLayout(false);
            this.grpNhapLieu.PerformLayout();
            this.grpDanhSach.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpNhapLieu;
        private System.Windows.Forms.Label lblMaVT;
        private System.Windows.Forms.TextBox txtMaVT;
        private System.Windows.Forms.Label lblTenVT;
        private System.Windows.Forms.TextBox txtTenVT;
        private System.Windows.Forms.Label lblDonViTinh;
        private System.Windows.Forms.ComboBox cboDonViTinh;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.ListView lvVatTu;
        private System.Windows.Forms.ColumnHeader colMaVT;
        private System.Windows.Forms.ColumnHeader colTenVT;
        private System.Windows.Forms.ColumnHeader colDVT;
        private System.Windows.Forms.ColumnHeader colDonGia;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.Button btnXoaTatCa;
    }
}