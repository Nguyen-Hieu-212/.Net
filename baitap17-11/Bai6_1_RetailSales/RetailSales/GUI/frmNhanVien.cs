using System;
using System.Windows.Forms;
using RetailSales.BUS;
using RetailSales.DTO;

namespace RetailSales.GUI
{
    public partial class frmNhanVien : Form
    {
        private readonly NhanVienBUS bus = new NhanVienBUS();

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgvNhanVien.DataSource = bus.GetAll();
                GuiHelper.Header(dgvNhanVien, "MaNV", "Mã NV");
                GuiHelper.Header(dgvNhanVien, "TenNV", "Tên nhân viên");
                GuiHelper.Header(dgvNhanVien, "SoDienThoai", "Số điện thoại");
                GuiHelper.Header(dgvNhanVien, "ChucVu", "Chức vụ");
                GuiHelper.Hide(dgvNhanVien, "HienThi");
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void ClearForm()
        {
            txtMaNV.Clear(); txtTenNV.Clear(); txtSoDienThoai.Clear();
            cboChucVu.Text = "";
            dgvNhanVien.ClearSelection();
            txtTenNV.Focus();
        }

        private NhanVienDTO ReadForm()
        {
            int ma;
            int.TryParse(txtMaNV.Text, out ma);
            return new NhanVienDTO
            {
                MaNV = ma,
                TenNV = txtTenNV.Text,
                SoDienThoai = txtSoDienThoai.Text,
                ChucVu = cboChucVu.Text
            };
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            NhanVienDTO nv = dgvNhanVien.Rows[e.RowIndex].DataBoundItem as NhanVienDTO;
            if (nv == null) return;
            txtMaNV.Text = nv.MaNV.ToString();
            txtTenNV.Text = nv.TenNV;
            txtSoDienThoai.Text = nv.SoDienThoai;
            cboChucVu.Text = nv.ChucVu;
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
                GuiHelper.Info("Đã cập nhật nhân viên.");
                LoadData();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma;
            if (!int.TryParse(txtMaNV.Text, out ma)) { GuiHelper.Warn("Hãy chọn nhân viên cần xóa."); return; }
            if (!GuiHelper.Confirm("Xóa nhân viên '" + txtTenNV.Text + "'?")) return;
            try
            {
                bus.Xoa(ma);
                LoadData(); ClearForm();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }
    }
}
