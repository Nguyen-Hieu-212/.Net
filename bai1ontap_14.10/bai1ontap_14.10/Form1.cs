using System;
using System.Windows.Forms;

namespace bai1ontap_14._10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTinhTien_Click(object sender, EventArgs e)
        {
 if (!double.TryParse(txtDonGia.Text, out double donGia) ||
                !int.TryParse(txtSoLuong.Text, out int soLuong) ||
                !double.TryParse(txtGiamGia.Text, out double giamGia))
            {
                MessageBox.Show("Dữ liệu không hợp lệ! Vui lòng nhập số và không để trống.",
                                "Lỗi nhập liệu",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            double tongTien = (donGia * soLuong) * ((100.0 - giamGia) / 100.0);
            lblTongTien.Text = "Tổng tiền thanh toán: " + tongTien.ToString("N0") + " VNĐ";
        }

        private void lblTongTien_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu: Nếu để trống hoặc nhập chữ, hiển thị cảnh báo[cite: 1]
           
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtGiamGia.Clear();
            lblTongTien.Text = "Tổng tiền thanh toán: 0 VNĐ";

            // Đưa con trỏ chuột về ô Đơn giá
            txtDonGia.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}