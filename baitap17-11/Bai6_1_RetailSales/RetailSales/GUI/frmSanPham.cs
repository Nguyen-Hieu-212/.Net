using System;
using System.Windows.Forms;
using RetailSales.BUS;
using RetailSales.DTO;

namespace RetailSales.GUI
{
    public partial class frmSanPham : Form
    {
        private readonly SanPhamBUS bus = new SanPhamBUS();

        public frmSanPham()
        {
            InitializeComponent();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

        private void LoadData()
        {
            try
            {
                dgvSanPham.DataSource = bus.Search(txtTimKiem.Text);
                GuiHelper.Header(dgvSanPham, "MaSP", "Mã SP / Mã vạch");
                GuiHelper.Header(dgvSanPham, "TenSP", "Tên sản phẩm");
                GuiHelper.Header(dgvSanPham, "GiaBan", "Giá bán", "N0");
                GuiHelper.Header(dgvSanPham, "SoLuongTon", "Tồn kho", "N0");
                GuiHelper.Hide(dgvSanPham, "HienThi");
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void ClearForm()
        {
            txtMaSP.Clear(); txtTenSP.Clear(); txtGiaBan.Clear();
            nudTon.Value = 0;
            txtMaSP.ReadOnly = false;      // cho phép nhập mã khi thêm mới
            dgvSanPham.ClearSelection();
            txtMaSP.Focus();
        }

        private SanPhamDTO ReadForm()
        {
            decimal gia;
            if (!GuiHelper.TryParseDecimal(txtGiaBan.Text, out gia))
                throw new ArgumentException("Giá bán không hợp lệ.");
            return new SanPhamDTO
            {
                MaSP = txtMaSP.Text,
                TenSP = txtTenSP.Text,
                GiaBan = gia,
                SoLuongTon = (int)nudTon.Value
            };
        }

        private void btnTim_Click(object sender, EventArgs e) { LoadData(); }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadData();
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            SanPhamDTO sp = dgvSanPham.Rows[e.RowIndex].DataBoundItem as SanPhamDTO;
            if (sp == null) return;
            txtMaSP.Text = sp.MaSP;
            txtTenSP.Text = sp.TenSP;
            txtGiaBan.Text = sp.GiaBan.ToString("0.##");
            nudTon.Value = sp.SoLuongTon;
            txtMaSP.ReadOnly = true;       // khóa khóa chính khi sửa
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                bus.Them(ReadForm());
                GuiHelper.Info("Đã thêm sản phẩm.");
                LoadData(); ClearForm();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                bus.Sua(ReadForm());
                GuiHelper.Info("Đã cập nhật sản phẩm.");
                LoadData();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text)) { GuiHelper.Warn("Hãy chọn sản phẩm cần xóa."); return; }
            if (!GuiHelper.Confirm("Xóa sản phẩm '" + txtTenSP.Text + "'?")) return;
            try
            {
                bus.Xoa(txtMaSP.Text);
                LoadData(); ClearForm();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadData();
            ClearForm();
        }
    }
}
