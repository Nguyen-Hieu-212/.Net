using System;
using System.Windows.Forms;

namespace HRM.GUI
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

        private void btnPhongBan_Click(object sender, EventArgs e) { Open(new frmPhongBan()); }
        private void btnNhanVien_Click(object sender, EventArgs e) { Open(new frmNhanVien()); }
        private void btnChamCong_Click(object sender, EventArgs e) { Open(new frmChamCong()); }
        private void btnLuong_Click(object sender, EventArgs e) { Open(new frmLuong()); }
    }
}
