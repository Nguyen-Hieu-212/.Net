using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HRM.BUS;
using HRM.DTO;

namespace HRM.GUI
{
    public partial class frmNhanVien : Form
    {
        private readonly NhanVienBUS bus = new NhanVienBUS();
        private readonly PhongBanBUS pbBus = new PhongBanBUS();

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                // Hai ComboBox dùng hai danh sách riêng để lựa chọn không bị đồng bộ với nhau
                cboPhongBan.DisplayMember = "TenPB";
                cboPhongBan.ValueMember = "MaPB";
                cboPhongBan.DataSource = pbBus.GetAll();

                List<PhongBanDTO> dsLoc = pbBus.GetAll();
                dsLoc.Insert(0, new PhongBanDTO { MaPB = 0, TenPB = "-- Tất cả --" });
                cboLocPB.DisplayMember = "TenPB";
                cboLocPB.ValueMember = "MaPB";
                cboLocPB.DataSource = dsLoc;

                LoadData();
                ClearForm();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private static int? GetValue(ComboBox cbo)
        {
            return cbo.SelectedValue is int ? (int?)(int)cbo.SelectedValue : null;
        }

        private void LoadData()
        {
            try
            {
                dgvNhanVien.DataSource = bus.Search(txtTimKiem.Text, GetValue(cboLocPB) ?? 0);
                GuiHelper.Header(dgvNhanVien, "MaNV", "Mã NV");
                GuiHelper.Header(dgvNhanVien, "HoTen", "Họ tên");
                GuiHelper.Header(dgvNhanVien, "NgaySinh", "Ngày sinh", null);
                if (dgvNhanVien.Columns.Contains("NgaySinh"))
                    dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                GuiHelper.Header(dgvNhanVien, "CCCD", "CCCD");
                GuiHelper.Hide(dgvNhanVien, "MaPB");
                GuiHelper.Header(dgvNhanVien, "TenPB", "Phòng ban");
                GuiHelper.Header(dgvNhanVien, "LuongCoBan", "Lương cơ bản", "N0");
                GuiHelper.Header(dgvNhanVien, "HeSoLuong", "Hệ số lương", "0.00");
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void ClearForm()
        {
            txtMaNV.Clear(); txtHoTen.Clear(); txtCCCD.Clear(); txtLuongCoBan.Clear();
            dtpNgaySinh.Value = new DateTime(1995, 1, 1);
            nudHeSo.Value = 1.0m;
            if (cboPhongBan.Items.Count > 0) cboPhongBan.SelectedIndex = 0;
            dgvNhanVien.ClearSelection();
            txtHoTen.Focus();
        }

        private NhanVienDTO ReadForm()
        {
            decimal luong;
            if (!GuiHelper.TryParseDecimal(txtLuongCoBan.Text, out luong))
                throw new ArgumentException("Lương cơ bản không hợp lệ.");
            int ma;
            int.TryParse(txtMaNV.Text, out ma);
            return new NhanVienDTO
            {
                MaNV = ma,
                HoTen = txtHoTen.Text,
                NgaySinh = dtpNgaySinh.Value.Date,
                CCCD = txtCCCD.Text,
                MaPB = GetValue(cboPhongBan),
                LuongCoBan = luong,
                HeSoLuong = (double)nudHeSo.Value
            };
        }

        private void btnTim_Click(object sender, EventArgs e) { LoadData(); }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboLocPB.SelectedIndex = 0;
            LoadData();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            NhanVienDTO nv = dgvNhanVien.Rows[e.RowIndex].DataBoundItem as NhanVienDTO;
            if (nv == null) return;

            txtMaNV.Text = nv.MaNV.ToString();
            txtHoTen.Text = nv.HoTen;
            dtpNgaySinh.Value = nv.NgaySinh ?? new DateTime(1995, 1, 1);
            txtCCCD.Text = nv.CCCD;
            txtLuongCoBan.Text = nv.LuongCoBan.ToString("0.##");
            nudHeSo.Value = (decimal)nv.HeSoLuong;
            if (nv.MaPB.HasValue) cboPhongBan.SelectedValue = nv.MaPB.Value;
            else cboPhongBan.SelectedIndex = -1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                bus.Them(ReadForm());
                GuiHelper.Info("Đã thêm nhân viên.");
                LoadData(); ClearForm();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                bus.Sua(ReadForm());
                GuiHelper.Info("Đã cập nhật hồ sơ nhân viên.");
                LoadData();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnChuyenPB_Click(object sender, EventArgs e)
        {
            int ma;
            if (!int.TryParse(txtMaNV.Text, out ma)) { GuiHelper.Warn("Hãy chọn nhân viên cần chuyển phòng ban."); return; }
            int? pb = GetValue(cboPhongBan);
            if (!pb.HasValue) { GuiHelper.Warn("Hãy chọn phòng ban mới ở ô \"Phòng ban\"."); return; }
            try
            {
                bus.ChuyenPhongBan(ma, pb.Value);
                GuiHelper.Info("Đã chuyển '" + txtHoTen.Text + "' sang phòng ban mới.");
                LoadData();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma;
            if (!int.TryParse(txtMaNV.Text, out ma)) { GuiHelper.Warn("Hãy chọn nhân viên cần xóa."); return; }
            if (!GuiHelper.Confirm("Xóa nhân viên '" + txtHoTen.Text + "'?")) return;
            try
            {
                bus.Xoa(ma);
                LoadData(); ClearForm();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cboLocPB.SelectedIndex = 0;
            LoadData();
            ClearForm();
        }
    }
}
