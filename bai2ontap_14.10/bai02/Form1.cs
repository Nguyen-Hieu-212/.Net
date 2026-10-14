using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace bai02
{
    public partial class Form1 : Form
    {
        private string duongDanAnh = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboLoaiSuCo.Items.AddRange(new object[] { "Phần cứng", "Phần mềm", "Mạng", "Tài khoản" });
            NhapLai();
        }

        // Đưa form về trạng thái ban đầu
        private void NhapLai()
        {
            txtMaPhieu.Text = "IT" + DateTime.Now.ToString("yyMMddHHmm");
            txtNguoiYeuCau.Clear();
            dtpNgayGhiNhan.Value = DateTime.Now;
            rdoTrungBinh.Checked = true;
            cboLoaiSuCo.SelectedIndex = -1;

            chkMayTinhBan.Checked = false;
            chkLaptop.Checked = false;
            chkMayIn.Checked = false;
            chkDienThoai.Checked = false;

            if (picAnhLoi.Image != null)
            {
                picAnhLoi.Image.Dispose();
                picAnhLoi.Image = null;
            }
            duongDanAnh = "";
            txtNguoiYeuCau.Focus();
        }

        private void btnTaiAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh chụp lỗi";
                ofd.Filter = "Tệp hình ảnh (*.jpg;*.png)|*.jpg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Copy ra Bitmap mới để không khóa file gốc
                        using (Image anhGoc = Image.FromFile(ofd.FileName))
                        {
                            if (picAnhLoi.Image != null) picAnhLoi.Image.Dispose();
                            picAnhLoi.Image = new Bitmap(anhGoc);
                        }
                        picAnhLoi.SizeMode = PictureBoxSizeMode.StretchImage;
                        duongDanAnh = ofd.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể mở ảnh: " + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu
            if (string.IsNullOrWhiteSpace(txtMaPhieu.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPhieu.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNguoiYeuCau.Text))
            {
                MessageBox.Show("Vui lòng nhập tên người yêu cầu!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNguoiYeuCau.Focus();
                return;
            }
            if (cboLoaiSuCo.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn loại sự cố!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiSuCo.Focus();
                return;
            }

            // Mức độ ưu tiên
            string mucDo = rdoThap.Checked ? "Thấp"
                         : rdoTrungBinh.Checked ? "Trung bình"
                         : rdoKhanCap.Checked ? "Khẩn cấp" : "(chưa chọn)";

            // Thiết bị ảnh hưởng
            List<string> thietBi = new List<string>();
            if (chkMayTinhBan.Checked) thietBi.Add(chkMayTinhBan.Text);
            if (chkLaptop.Checked) thietBi.Add(chkLaptop.Text);
            if (chkMayIn.Checked) thietBi.Add(chkMayIn.Text);
            if (chkDienThoai.Checked) thietBi.Add(chkDienThoai.Text);
            string dsThietBi = thietBi.Count > 0 ? string.Join(", ", thietBi) : "(không chọn)";

            string anh = string.IsNullOrEmpty(duongDanAnh)
                ? "(chưa tải ảnh)"
                : System.IO.Path.GetFileName(duongDanAnh);

            string tomTat =
                "Mã phiếu: " + txtMaPhieu.Text.Trim() + "\n" +
                "Người yêu cầu: " + txtNguoiYeuCau.Text.Trim() + "\n" +
                "Ngày ghi nhận: " + dtpNgayGhiNhan.Value.ToString("dd/MM/yyyy") + "\n" +
                "Mức độ ưu tiên: " + mucDo + "\n" +
                "Loại sự cố: " + cboLoaiSuCo.SelectedItem + "\n" +
                "Thiết bị ảnh hưởng: " + dsThietBi + "\n" +
                "Ảnh chụp lỗi: " + anh;

            MessageBox.Show(tomTat, "Tóm tắt phiếu yêu cầu IT",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnNhapLai_Click(object sender, EventArgs e)
        {
            NhapLai();
        }
    }
}