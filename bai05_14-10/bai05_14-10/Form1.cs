using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace bai05_14_10
{
    public partial class Form1 : Form
    {
        private static readonly CultureInfo vn = new CultureInfo("vi-VN");
        private bool dangTinh = false;   // chống gọi đệ quy khi tự gán Thành tiền

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtMaDon.Text = "DH" + DateTime.Now.ToString("yyMMddHHmm");

            cboLoaiVanChuyen.Items.Add("Tiêu chuẩn (3 - 5 ngày)");
            cboLoaiVanChuyen.Items.Add("Nhanh (1 - 2 ngày)");
            cboLoaiVanChuyen.Items.Add("Hỏa tốc (trong ngày)");
            cboLoaiVanChuyen.SelectedIndex = 0;

            // Dữ liệu mẫu
            dgvChiTiet.Rows.Add("Laptop Dell", 2, 4.5m, 15000000, 0m);
            dgvChiTiet.Rows.Add("Chuột không dây", 5, 0.5m, 200000, 0m);

            TinhLaiTatCa();

            // Đồng hồ hệ thống
            tmrDongHo_Tick(null, EventArgs.Empty);
            tmrDongHo.Start();
        }

        // ====== Đồng hồ trên StatusStrip ======
        private void tmrDongHo_Tick(object sender, EventArgs e)
        {
            tslThoiGian.Text = "Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // ====== Phân tích số từ giá trị ô ======
        private static bool TryParseSo(object value, out decimal kq)
        {
            kq = 0;
            if (value == null) return false;
            string s = Convert.ToString(value, CultureInfo.CurrentCulture);
            if (s == null) return false;
            return decimal.TryParse(s.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out kq);
        }

        // ====== Kiểm tra từng ô: lỗi thì hiện biểu tượng đỏ bên cạnh ô (ErrorText) ======
        private static bool KiemTraSoDuong(DataGridViewCell cell, string tenTruong, out decimal giaTri)
        {
            cell.ErrorText = "";
            if (!TryParseSo(cell.Value, out giaTri))
            {
                cell.ErrorText = tenTruong + " phải là số!";
                return false;
            }
            if (giaTri <= 0)
            {
                cell.ErrorText = tenTruong + " phải lớn hơn 0!";
                return false;
            }
            return true;
        }

        private static bool KiemTraDonGia(DataGridViewCell cell, out decimal giaTri)
        {
            cell.ErrorText = "";
            if (!TryParseSo(cell.Value, out giaTri))
            {
                cell.ErrorText = "Đơn giá phải là số!";
                return false;
            }
            if (giaTri < 0)
            {
                cell.ErrorText = "Đơn giá không được âm!";
                return false;
            }
            return true;
        }

        // ====== Tính lại Thành tiền từng dòng + Tổng trên StatusStrip ======
        private void TinhLaiTatCa()
        {
            if (dangTinh) return;
            dangTinh = true;

            decimal tongSL = 0, tongTL = 0, tongTien = 0;
            bool coLoi = false;

            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                if (row.IsNewRow) continue;

                bool slOk = KiemTraSoDuong(row.Cells[colSoLuong.Index], "Số lượng", out decimal sl);
                bool tlOk = KiemTraSoDuong(row.Cells[colTrongLuong.Index], "Trọng lượng", out decimal tl);
                bool dgOk = KiemTraDonGia(row.Cells[colDonGia.Index], out decimal dg);

                if (!slOk || !tlOk || !dgOk) coLoi = true;

                if (slOk) tongSL += sl;
                if (tlOk) tongTL += tl;

                decimal thanhTien = (slOk && dgOk) ? sl * dg : 0;
                row.Cells[colThanhTien.Index].Value = thanhTien;
                tongTien += thanhTien;
            }

            tslTongSL.Text = "Tổng số lượng: " + tongSL.ToString("0.##", vn);
            tslTongTL.Text = "Tổng trọng lượng: " + tongTL.ToString("N2", vn) + " kg";
            tslTongTien.Text = "Tổng tiền: " + tongTien.ToString("N0", vn) + "đ";

            // ErrorProvider: biểu tượng cảnh báo đỏ gắn với bảng khi có ô không hợp lệ
            errorProvider.SetIconAlignment(dgvChiTiet, ErrorIconAlignment.TopRight);
            errorProvider.SetIconPadding(dgvChiTiet, -22);
            errorProvider.SetError(dgvChiTiet,
                coLoi ? "Có ô nhập không hợp lệ (Số lượng / Trọng lượng phải > 0)" : "");

            dangTinh = false;
        }

        private void dgvChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex == colThanhTien.Index) return;
            TinhLaiTatCa();
        }

        private void dgvChiTiet_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            TinhLaiTatCa();
        }

        private void dgvChiTiet_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        // ====== Phím tắt: F2 thêm dòng, Delete xóa dòng ======
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                ThemDongMoi();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                // Đang gõ trong ô hoặc đang ở ô nhập khác thì để Delete xóa chữ như bình thường
                if (dgvChiTiet.IsCurrentCellInEditMode || !dgvChiTiet.ContainsFocus) return;

                XoaDongDangChon();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ThemDongMoi()
        {
            dgvChiTiet.EndEdit();

            int idx = dgvChiTiet.Rows.Add("", 1, 1, 0, 0m);
            dgvChiTiet.ClearSelection();
            dgvChiTiet.Rows[idx].Selected = true;
            dgvChiTiet.CurrentCell = dgvChiTiet.Rows[idx].Cells[colTenHang.Index];
            dgvChiTiet.Focus();
            dgvChiTiet.BeginEdit(true);

            TinhLaiTatCa();
        }

        private void XoaDongDangChon()
        {
            if (dgvChiTiet.SelectedRows.Count == 0) return;

            List<DataGridViewRow> dsXoa = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dgvChiTiet.SelectedRows)
            {
                if (!row.IsNewRow) dsXoa.Add(row);
            }
            foreach (DataGridViewRow row in dsXoa)
            {
                dgvChiTiet.Rows.Remove(row);
            }
            TinhLaiTatCa();
        }

        // ====== ErrorProvider cho các ô thông tin khách hàng ======
        private void txtTenKH_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError(txtTenKH,
                string.IsNullOrWhiteSpace(txtTenKH.Text) ? "Vui lòng nhập tên khách hàng!" : "");
        }

        private void txtSDT_Validating(object sender, CancelEventArgs e)
        {
            string sdt = txtSDT.Text.Trim();
            string loi = "";
            if (sdt.Length == 0) loi = "Vui lòng nhập số điện thoại!";
            else if (!Regex.IsMatch(sdt, @"^\d{9,11}$")) loi = "Số điện thoại gồm 9 - 11 chữ số!";
            errorProvider.SetError(txtSDT, loi);
        }

        private void dgvChiTiet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}