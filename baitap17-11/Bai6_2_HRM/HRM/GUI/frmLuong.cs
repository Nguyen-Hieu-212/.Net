using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HRM.BUS;
using HRM.DTO;

namespace HRM.GUI
{
    public partial class frmLuong : Form
    {
        private readonly LuongBUS bus = new LuongBUS();

        public frmLuong()
        {
            InitializeComponent();
            nudThang.Value = DateTime.Today.Month;
            nudNam.Value = DateTime.Today.Year;
            btnChot.Enabled = false;
        }

        private void LoadBangLuong()
        {
            try
            {
                bool daChot;
                List<LuongDTO> ds = bus.XemBangLuong((int)nudThang.Value, (int)nudNam.Value, out daChot);
                dgvLuong.DataSource = ds;

                GuiHelper.Header(dgvLuong, "MaNV", "Mã NV");
                GuiHelper.Header(dgvLuong, "HoTen", "Họ tên");
                GuiHelper.Header(dgvLuong, "TenPB", "Phòng ban");
                GuiHelper.Header(dgvLuong, "ThangNam", "Tháng/Năm");
                GuiHelper.Header(dgvLuong, "LuongCoBan", "Lương cơ bản", "N0");
                GuiHelper.Header(dgvLuong, "HeSoLuong", "Hệ số", "0.00");
                GuiHelper.Header(dgvLuong, "SoNgayCong", "Ngày công", "0.##");
                GuiHelper.Header(dgvLuong, "SoGioOT", "Giờ OT", "0.##");
                GuiHelper.Header(dgvLuong, "ThucLinh", "Thực lĩnh", "N0");

                decimal tong = 0;
                foreach (LuongDTO l in ds) tong += l.ThucLinh;
                lblTong.Text = "Tổng thực lĩnh: " + GuiHelper.Money(tong);

                if (daChot) lblTrangThai.Text = "ĐÃ CHỐT LƯƠNG - dữ liệu chấm công đã khóa.";
                else if (ds.Count == 0) lblTrangThai.Text = "Chưa có dữ liệu chấm công của tháng này.";
                else lblTrangThai.Text = "Chưa chốt - đây là bảng lương dự kiến.";

                btnChot.Enabled = !daChot && ds.Count > 0;
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadBangLuong();
        }

        private void btnChot_Click(object sender, EventArgs e)
        {
            int thang = (int)nudThang.Value, nam = (int)nudNam.Value;
            if (!GuiHelper.Confirm("Chốt lương tháng " + thang + "/" + nam +
                                   "?\nSau khi chốt, dữ liệu chấm công của tháng này sẽ bị khóa.")) return;
            try
            {
                bus.ChotLuong(thang, nam);          // SqlTransaction: ghi BangLuong + khóa BangChamCong
                GuiHelper.Info("Đã chốt lương tháng " + thang + "/" + nam + ".");
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
            LoadBangLuong();
        }
    }
}
