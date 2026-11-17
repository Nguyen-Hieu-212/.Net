using System;
using System.Windows.Forms;
using RetailSales.BUS;
using RetailSales.DTO;

namespace RetailSales.GUI
{
    public partial class frmKhachHang : Form
    {
        private readonly KhachHangBUS bus = new KhachHangBUS();

        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgvKhachHang.DataSource = bus.GetAll();
                GuiHelper.Header(dgvKhachHang, "MaKH", "Mã KH");
                GuiHelper.Header(dgvKhachHang, "TenKH", "Tên khách hàng");
                GuiHelper.Header(dgvKhachHang, "DienThoai", "Điện thoại");
                GuiHelper.Header(dgvKhachHang, "DiemTichLuy", "Điểm tích lũy", "N0");
                GuiHelper.Hide(dgvKhachHang, "HienThi");
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void ClearForm()
        {
            txtMaKH.Clear(); txtTenKH.Clear(); txtDienThoai.Clear();
            nudDiem.Value = 0;
            dgvKhachHang.ClearSelection();
            txtTenKH.Focus();
        }

        private KhachHangDTO ReadForm()
        {
            int ma;
            int.TryParse(txtMaKH.Text, out ma);
            return new KhachHangDTO
            {
                MaKH = ma,
                TenKH = txtTenKH.Text,
                DienThoai = txtDienThoai.Text,
                DiemTichLuy = (int)nudDiem.Value
            };
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            KhachHangDTO kh = dgvKhachHang.Rows[e.RowIndex].DataBoundItem as KhachHangDTO;
            if (kh == null) return;
            txtMaKH.Text = kh.MaKH.ToString();
            txtTenKH.Text = kh.TenKH;
            txtDienThoai.Text = kh.DienThoai;
            nudDiem.Value = kh.DiemTichLuy;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                bus.Them(ReadForm());
                GuiHelper.Info("Đã thêm khách hàng.");
                LoadData(); ClearForm();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                bus.Sua(ReadForm());
                GuiHelper.Info("Đã cập nhật khách hàng.");
                LoadData();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma;
            if (!int.TryParse(txtMaKH.Text, out ma)) { GuiHelper.Warn("Hãy chọn khách hàng cần xóa."); return; }
            if (!GuiHelper.Confirm("Xóa khách hàng '" + txtTenKH.Text + "'?")) return;
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
