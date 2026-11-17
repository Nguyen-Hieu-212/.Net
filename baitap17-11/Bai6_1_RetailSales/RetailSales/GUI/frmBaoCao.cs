using System;
using System.Windows.Forms;
using RetailSales.BUS;

namespace RetailSales.GUI
{
    public partial class frmBaoCao : Form
    {
        private readonly BaoCaoBUS bus = new BaoCaoBUS();

        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            dtpNgay.Value = DateTime.Today;
            nudThang.Value = DateTime.Today.Month;
            nudNam.Value = DateTime.Today.Year;
        }

        private void btnDTNgay_Click(object sender, EventArgs e)
        {
            try
            {
                decimal dt = bus.DoanhThuNgay(dtpNgay.Value);
                lblKQNgay.Text = "Doanh thu ngày " + dtpNgay.Value.ToString("dd/MM/yyyy") + ": " + GuiHelper.Money(dt);
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnDTThang_Click(object sender, EventArgs e)
        {
            try
            {
                int thang = (int)nudThang.Value, nam = (int)nudNam.Value;
                decimal dt = bus.DoanhThuThang(thang, nam);
                lblKQThang.Text = "Doanh thu tháng " + thang + "/" + nam + ": " + GuiHelper.Money(dt);
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnTop5_Click(object sender, EventArgs e)
        {
            try
            {
                dgvTop5.DataSource = bus.Top5SanPham();
                GuiHelper.Header(dgvTop5, "MaSP", "Mã SP");
                GuiHelper.Header(dgvTop5, "TenSP", "Tên sản phẩm");
                GuiHelper.Header(dgvTop5, "TongSoLuong", "Tổng SL đã bán", "N0");
                GuiHelper.Header(dgvTop5, "DoanhThu", "Doanh thu", "N0");
                if (dgvTop5.Rows.Count == 0) GuiHelper.Info("Chưa có hóa đơn nào để thống kê.");
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }
    }
}
