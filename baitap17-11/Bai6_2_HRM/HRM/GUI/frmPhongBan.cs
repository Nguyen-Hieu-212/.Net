using System;
using System.Windows.Forms;
using HRM.BUS;
using HRM.DTO;

namespace HRM.GUI
{
    public partial class frmPhongBan : Form
    {
        private readonly PhongBanBUS bus = new PhongBanBUS();

        public frmPhongBan()
        {
            InitializeComponent();
        }

        private void frmPhongBan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgvPhongBan.DataSource = bus.GetAll();
                GuiHelper.Header(dgvPhongBan, "MaPB", "Mã PB");
                GuiHelper.Header(dgvPhongBan, "TenPB", "Tên phòng ban");
                GuiHelper.Header(dgvPhongBan, "MoTa", "Mô tả");
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void ClearForm()
        {
            txtMaPB.Clear(); txtTenPB.Clear(); txtMoTa.Clear();
            dgvPhongBan.ClearSelection();
            txtTenPB.Focus();
        }

        private PhongBanDTO ReadForm()
        {
            int ma;
            int.TryParse(txtMaPB.Text, out ma);
            return new PhongBanDTO { MaPB = ma, TenPB = txtTenPB.Text, MoTa = txtMoTa.Text };
        }

        private void dgvPhongBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            PhongBanDTO pb = dgvPhongBan.Rows[e.RowIndex].DataBoundItem as PhongBanDTO;
            if (pb == null) return;
            txtMaPB.Text = pb.MaPB.ToString();
            txtTenPB.Text = pb.TenPB;
            txtMoTa.Text = pb.MoTa;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                bus.Them(ReadForm());
                GuiHelper.Info("Đã thêm phòng ban.");
                LoadData(); ClearForm();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                bus.Sua(ReadForm());
                GuiHelper.Info("Đã cập nhật phòng ban.");
                LoadData();
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma;
            if (!int.TryParse(txtMaPB.Text, out ma)) { GuiHelper.Warn("Hãy chọn phòng ban cần xóa."); return; }
            if (!GuiHelper.Confirm("Xóa phòng ban '" + txtTenPB.Text + "'?")) return;
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
