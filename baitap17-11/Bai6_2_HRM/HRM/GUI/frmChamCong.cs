using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HRM.BUS;

namespace HRM.GUI
{
    /// <summary>Chấm công theo mô hình ngắt kết nối: DataTable + SqlDataAdapter + SqlCommandBuilder.</summary>
    public partial class frmChamCong : Form
    {
        private readonly ChamCongBUS bus = new ChamCongBUS();
        private DataTable dt;

        public frmChamCong()
        {
            InitializeComponent();
            nudThang.Value = DateTime.Today.Month;
            nudNam.Value = DateTime.Today.Year;
        }

        private void btnTai_Click(object sender, EventArgs e)
        {
            if (bus.CoThayDoi(dt) && !GuiHelper.Confirm("Bảng hiện tại có thay đổi chưa lưu. Bỏ qua và nạp tháng khác?"))
                return;
            try
            {
                dt = bus.Load((int)nudThang.Value, (int)nudNam.Value);
                dgvChamCong.DataSource = dt;
                lblTrangThai.Text = bus.ThangDaKhoa ? "Tháng này đã chốt lương - dữ liệu chấm công bị khóa." : "";
                btnLuu.Enabled = !bus.ThangDaKhoa;
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }

        // Định dạng lưới + khóa các dòng đã chốt lương sau khi gán DataSource
        private void dgvChamCong_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            string[] thuTu = { "MaNV", "HoTen", "PhongBan", "ThangNam", "SoNgayCong", "SoGioOT", "DaKhoa" };
            for (int i = 0; i < thuTu.Length; i++)
                if (dgvChamCong.Columns.Contains(thuTu[i])) dgvChamCong.Columns[thuTu[i]].DisplayIndex = i;

            GuiHelper.Hide(dgvChamCong, "MaCong");
            GuiHelper.Header(dgvChamCong, "MaNV", "Mã NV");
            GuiHelper.Header(dgvChamCong, "HoTen", "Họ tên");
            GuiHelper.Header(dgvChamCong, "PhongBan", "Phòng ban");
            GuiHelper.Header(dgvChamCong, "ThangNam", "Tháng/Năm");
            GuiHelper.Header(dgvChamCong, "SoNgayCong", "Số ngày công");
            GuiHelper.Header(dgvChamCong, "SoGioOT", "Số giờ OT");
            GuiHelper.Header(dgvChamCong, "DaKhoa", "Đã khóa");

            // Chỉ cho sửa 2 cột: SoNgayCong và SoGioOT
            foreach (DataGridViewColumn c in dgvChamCong.Columns)
                c.ReadOnly = !(c.Name == "SoNgayCong" || c.Name == "SoGioOT");

            foreach (DataGridViewRow row in dgvChamCong.Rows)
            {
                DataRowView v = row.DataBoundItem as DataRowView;
                if (v != null && Convert.ToBoolean(v["DaKhoa"]))
                {
                    row.ReadOnly = true;
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void dgvChamCong_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            GuiHelper.Warn("Giá trị không hợp lệ. Hãy nhập số (ví dụ: 22 hoặc 21,5).");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dt == null) { GuiHelper.Warn("Hãy nạp bảng chấm công trước."); return; }
            try
            {
                // Đẩy giá trị đang gõ dở trong ô/dòng hiện tại vào DataTable
                dgvChamCong.EndEdit();
                BindingContext[dt].EndCurrentEdit();

                int n = bus.Luu(dt);        // adapter.Update(dt) trong 1 thao tác
                GuiHelper.Info("Đã lưu chấm công (" + n + " dòng được đồng bộ về SQL Server).");
            }
            catch (Exception ex) { GuiHelper.Error(ex); }
        }
    }
}
