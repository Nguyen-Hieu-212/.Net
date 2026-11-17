using System;
using System.Windows.Forms;

namespace RetailSales.GUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void Open(Form f)
        {
            using (f) { f.ShowDialog(this); }
        }

        private void btnBanHang_Click(object sender, EventArgs e) { Open(new frmBanHang()); }
        private void btnSanPham_Click(object sender, EventArgs e) { Open(new frmSanPham()); }
        private void btnKhachHang_Click(object sender, EventArgs e) { Open(new frmKhachHang()); }
        private void btnNhanVien_Click(object sender, EventArgs e) { Open(new frmNhanVien()); }
        private void btnBaoCao_Click(object sender, EventArgs e) { Open(new frmBaoCao()); }
    }
}
