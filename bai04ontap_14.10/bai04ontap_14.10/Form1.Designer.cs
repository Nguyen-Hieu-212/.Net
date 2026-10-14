namespace bai04ontap_14
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
            this.lblSoDo = new System.Windows.Forms.Label();
            this.tlpSoDo = new System.Windows.Forms.TableLayoutPanel();
            this.grpThongKe = new System.Windows.Forms.GroupBox();
            this.lblKhungGio = new System.Windows.Forms.Label();
            this.cboKhungGio = new System.Windows.Forms.ComboBox();
            this.lblDangChon = new System.Windows.Forms.Label();
            this.lblTamTinh = new System.Windows.Forms.Label();
            this.lblChuThichTrong = new System.Windows.Forms.Label();
            this.lblChuThichChon = new System.Windows.Forms.Label();
            this.lblChuThichDat = new System.Windows.Forms.Label();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuyTatCa = new System.Windows.Forms.Button();
            this.grpThongKe.SuspendLayout();
            this.SuspendLayout();
            //
            // lblSoDo
            //
            this.lblSoDo.AutoSize = true;
            this.lblSoDo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSoDo.Location = new System.Drawing.Point(12, 10);
            this.lblSoDo.Name = "lblSoDo";
            this.lblSoDo.Text = "Sơ đồ chỗ ngồi (4 x 5)";
            //
            // tlpSoDo
            //
            this.tlpSoDo.ColumnCount = 5;
            this.tlpSoDo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpSoDo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpSoDo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpSoDo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpSoDo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpSoDo.RowCount = 4;
            this.tlpSoDo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpSoDo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpSoDo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpSoDo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpSoDo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlpSoDo.Location = new System.Drawing.Point(12, 38);
            this.tlpSoDo.Name = "tlpSoDo";
            this.tlpSoDo.Size = new System.Drawing.Size(500, 330);
            this.tlpSoDo.TabIndex = 0;
            //
            // grpThongKe
            //
            this.grpThongKe.Controls.Add(this.lblKhungGio);
            this.grpThongKe.Controls.Add(this.cboKhungGio);
            this.grpThongKe.Controls.Add(this.lblDangChon);
            this.grpThongKe.Controls.Add(this.lblTamTinh);
            this.grpThongKe.Controls.Add(this.lblChuThichTrong);
            this.grpThongKe.Controls.Add(this.lblChuThichChon);
            this.grpThongKe.Controls.Add(this.lblChuThichDat);
            this.grpThongKe.Location = new System.Drawing.Point(530, 12);
            this.grpThongKe.Name = "grpThongKe";
            this.grpThongKe.Size = new System.Drawing.Size(230, 280);
            this.grpThongKe.TabIndex = 1;
            this.grpThongKe.TabStop = false;
            this.grpThongKe.Text = "Thống kê";
            //
            // lblKhungGio
            //
            this.lblKhungGio.AutoSize = true;
            this.lblKhungGio.Location = new System.Drawing.Point(15, 30);
            this.lblKhungGio.Name = "lblKhungGio";
            this.lblKhungGio.Text = "Khung giờ:";
            //
            // cboKhungGio
            //
            this.cboKhungGio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhungGio.Location = new System.Drawing.Point(15, 55);
            this.cboKhungGio.Name = "cboKhungGio";
            this.cboKhungGio.Size = new System.Drawing.Size(200, 28);
            this.cboKhungGio.TabIndex = 0;
            this.cboKhungGio.SelectedIndexChanged += new System.EventHandler(this.cboKhungGio_SelectedIndexChanged);
            //
            // lblDangChon
            //
            this.lblDangChon.AutoSize = true;
            this.lblDangChon.Location = new System.Drawing.Point(15, 105);
            this.lblDangChon.Name = "lblDangChon";
            this.lblDangChon.Text = "Số vị trí đang chọn: 0";
            //
            // lblTamTinh
            //
            this.lblTamTinh.AutoSize = true;
            this.lblTamTinh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTamTinh.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTamTinh.Location = new System.Drawing.Point(15, 140);
            this.lblTamTinh.Name = "lblTamTinh";
            this.lblTamTinh.Text = "Tạm tính tiền: 0đ";
            //
            // lblChuThichTrong
            //
            this.lblChuThichTrong.BackColor = System.Drawing.Color.Gainsboro;
            this.lblChuThichTrong.Location = new System.Drawing.Point(15, 190);
            this.lblChuThichTrong.Name = "lblChuThichTrong";
            this.lblChuThichTrong.Size = new System.Drawing.Size(200, 26);
            this.lblChuThichTrong.Text = "Trống";
            this.lblChuThichTrong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblChuThichChon
            //
            this.lblChuThichChon.BackColor = System.Drawing.Color.LimeGreen;
            this.lblChuThichChon.Location = new System.Drawing.Point(15, 222);
            this.lblChuThichChon.Name = "lblChuThichChon";
            this.lblChuThichChon.Size = new System.Drawing.Size(200, 26);
            this.lblChuThichChon.Text = "Đang chọn";
            this.lblChuThichChon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblChuThichDat
            //
            this.lblChuThichDat.BackColor = System.Drawing.Color.IndianRed;
            this.lblChuThichDat.ForeColor = System.Drawing.Color.White;
            this.lblChuThichDat.Location = new System.Drawing.Point(15, 254);
            this.lblChuThichDat.Name = "lblChuThichDat";
            this.lblChuThichDat.Size = new System.Drawing.Size(200, 26);
            this.lblChuThichDat.Text = "Đã đặt / Đã khóa";
            this.lblChuThichDat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnXacNhan
            //
            this.btnXacNhan.Location = new System.Drawing.Point(530, 304);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(230, 30);
            this.btnXacNhan.TabIndex = 2;
            this.btnXacNhan.Text = "Xác nhận đặt";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            //
            // btnHuyTatCa
            //
            this.btnHuyTatCa.Location = new System.Drawing.Point(530, 340);
            this.btnHuyTatCa.Name = "btnHuyTatCa";
            this.btnHuyTatCa.Size = new System.Drawing.Size(230, 30);
            this.btnHuyTatCa.TabIndex = 3;
            this.btnHuyTatCa.Text = "Hủy chọn tất cả";
            this.btnHuyTatCa.UseVisualStyleBackColor = true;
            this.btnHuyTatCa.Click += new System.EventHandler(this.btnHuyTatCa_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 385);
            this.Controls.Add(this.lblSoDo);
            this.Controls.Add(this.tlpSoDo);
            this.Controls.Add(this.grpThongKe);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.btnHuyTatCa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interactive Slot Booking";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpThongKe.ResumeLayout(false);
            this.grpThongKe.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblSoDo;
        private System.Windows.Forms.TableLayoutPanel tlpSoDo;
        private System.Windows.Forms.GroupBox grpThongKe;
        private System.Windows.Forms.Label lblKhungGio;
        private System.Windows.Forms.ComboBox cboKhungGio;
        private System.Windows.Forms.Label lblDangChon;
        private System.Windows.Forms.Label lblTamTinh;
        private System.Windows.Forms.Label lblChuThichTrong;
        private System.Windows.Forms.Label lblChuThichChon;
        private System.Windows.Forms.Label lblChuThichDat;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuyTatCa;
    }
}