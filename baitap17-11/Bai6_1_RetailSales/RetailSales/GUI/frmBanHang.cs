using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using RetailSales.BUS;
using RetailSales.DTO;

namespace RetailSales.GUI
{
    public partial class frmBanHang : Form
    {
        private readonly SanPhamBUS spBus = new SanPhamBUS();
        private readonly KhachHangBUS khBus = new KhachHangBUS();
        private readonly NhanVienBUS nvBus = new NhanVienBUS();
        private readonly HoaDonBUS hdBus = new HoaDonBUS();

        // Giỏ hàng: BindingList để DataGridView tự cập nhật khi thêm/xóa dòng
        private readonly BindingList<ChiTietHoaDonDTO> gio = new BindingList<ChiTietHoaDonDTO>();

        public frmBanHang()
        {
            InitializeComponent();
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            try
            {
                cboNhanVien.DisplayMember = "HienThi";
                cboNhanVien.ValueMember = "MaNV";
                cboNhanVien.DataSource = nvBus.GetAll();

                cboKhachHang.DisplayMember = "HienThi";
                cboKhachHang.ValueMember = "MaKH";
                cboKhachHang.DataSource = khBus.GetAllKemKhachLe();

                cboSanPham.DisplayMember = "HienThi";
                cboSanPham.ValueMember = "MaSP";
                LoadSanPham();

                dgvGioHang.DataSource = gio;
                GuiHelper.Hide(dgvGioHang, "MaHD");
                GuiHelper.Header(dgvGioHang, "MaSP", "Mã SP");
                GuiHelper.Header(dgvGioHang, "TenSP", "Tên sản phẩm");
                GuiHelper.Header(dgvGioHang, "SoLuong", "Số lượng", "N0");
                GuiHelper.Header(dgvGioHang, "DonGia", "Đơn giá", "N0");
                GuiHelper.Header(dgvGioHang, "ThanhTien", "Thành tiền", "N0");

                RefreshTotals();
                txtMaVach.Focus();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void LoadSanPham()
        {
            cboSanPham.DataSource = spBus.GetAll();
        }

        /// <summary>Tính lại tổng tiền và điểm tích lũy sẽ được cộng.</summary>
        private void RefreshTotals()
        {
            decimal tong = HoaDonBUS.TinhTongTien(gio);
            lblTongTien.Text = GuiHelper.Money(tong);

            KhachHangDTO kh = cboKhachHang.SelectedItem as KhachHangDTO;
            bool laKhachLe = kh == null || kh.MaKH == 0;
            lblDiem.Text = (laKhachLe ? 0 : HoaDonBUS.TinhDiem(tong)) + " điểm";
        }

        private void ThemVaoGio(SanPhamDTO sp, int soLuong)
        {
            ChiTietHoaDonDTO dong = gio.FirstOrDefault(x => x.MaSP == sp.MaSP);
            int daCo = dong == null ? 0 : dong.SoLuong;

            if (daCo + soLuong > sp.SoLuongTon)
            {
                GuiHelper.Warn("Không đủ hàng: '" + sp.TenSP + "' chỉ còn " + sp.SoLuongTon +
                               " (trong giỏ đã có " + daCo + ").");
                return;
            }

            if (dong == null)
            {
                gio.Add(new ChiTietHoaDonDTO
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    SoLuong = soLuong,
                    DonGia = sp.GiaBan
                });
            }
            else
            {
                dong.SoLuong += soLuong;
                gio.ResetBindings();        // làm mới lưới vì DTO không tự báo thay đổi
            }

            nudSoLuong.Value = 1;
            RefreshTotals();
        }

        private void btnThemGio_Click(object sender, EventArgs e)
        {
            SanPhamDTO sp = cboSanPham.SelectedItem as SanPhamDTO;
            if (sp == null) { GuiHelper.Warn("Hãy chọn sản phẩm."); return; }
            ThemVaoGio(sp, (int)nudSoLuong.Value);
        }

        // Máy quét mã vạch gõ mã rồi gửi phím Enter
        private void txtMaVach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;

            string ma = txtMaVach.Text.Trim();
            if (ma.Length == 0) return;
            try
            {
                SanPhamDTO sp = spBus.GetById(ma);
                if (sp == null)
                {
                    GuiHelper.Warn("Không tìm thấy sản phẩm có mã '" + ma + "'.");
                }
                else
                {
                    cboSanPham.SelectedValue = sp.MaSP;
                    ThemVaoGio(sp, (int)nudSoLuong.Value);
                }
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
            txtMaVach.Clear();
            txtMaVach.Focus();
        }

        private void cboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTotals();
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow == null) return;
            ChiTietHoaDonDTO dong = dgvGioHang.CurrentRow.DataBoundItem as ChiTietHoaDonDTO;
            if (dong == null) return;
            gio.Remove(dong);
            RefreshTotals();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            if (gio.Count > 0 && !GuiHelper.Confirm("Xóa toàn bộ giỏ hàng hiện tại?")) return;
            gio.Clear();
            RefreshTotals();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (gio.Count == 0) { GuiHelper.Warn("Giỏ hàng đang trống."); return; }

            NhanVienDTO nv = cboNhanVien.SelectedItem as NhanVienDTO;
            if (nv == null) { GuiHelper.Warn("Hãy chọn nhân viên bán hàng."); return; }

            KhachHangDTO kh = cboKhachHang.SelectedItem as KhachHangDTO;
            int? maKH = (kh == null || kh.MaKH == 0) ? (int?)null : kh.MaKH;

            decimal tong = HoaDonBUS.TinhTongTien(gio);
            if (!GuiHelper.Confirm("Thanh toán " + GuiHelper.Money(tong) + " ?")) return;

            try
            {
                int maHD = hdBus.ThanhToan(nv.MaNV, maKH, gio.ToList());
                GuiHelper.Info("Thanh toán thành công!\nMã hóa đơn: " + maHD +
                               "\nTổng tiền: " + GuiHelper.Money(tong) +
                               (maKH.HasValue ? "\nĐiểm tích lũy cộng thêm: " + HoaDonBUS.TinhDiem(tong) : ""));
                gio.Clear();
                RefreshTotals();
                LoadSanPham();          // tồn kho đã thay đổi
                txtMaVach.Focus();
            }
            catch (Exception ex)
            {
                GuiHelper.Error(ex);    // giao dịch đã Rollback, giỏ hàng được giữ nguyên
                LoadSanPham();
            }
        }
    }
}
