using System;
using System.Globalization;
using System.Windows.Forms;

namespace bai03_14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboDonViTinh.SelectedIndex = 0;
        }

        // Kiểm tra dữ liệu nhập, trả về đơn giá nếu hợp lệ
        private bool KiemTraDuLieu(out decimal donGia)
        {
            donGia = 0;

            if (string.IsNullOrWhiteSpace(txtMaVT.Text))
            {
                MessageBox.Show("Vui lòng nhập mã vật tư!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaVT.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenVT.Text))
            {
                MessageBox.Show("Vui lòng nhập tên vật tư!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenVT.Focus();
                return false;
            }
            if (cboDonViTinh.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn đơn vị tính!", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDonViTinh.Focus();
                return false;
            }
            if (!decimal.TryParse(txtDonGia.Text.Trim(), NumberStyles.Number,
                    CultureInfo.CurrentCulture, out donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá phải là số không âm!", "Sai dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }
            return true;
        }

        // Kiểm tra Mã VT đã tồn tại trong ListView chưa
        private bool MaDaTonTai(string ma)
        {
            foreach (ListViewItem item in lvVatTu.Items)
            {
                if (string.Equals(item.Text, ma, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private void LamSachOnhap()
        {
            txtMaVT.Clear();
            txtTenVT.Clear();
            txtDonGia.Clear();
            cboDonViTinh.SelectedIndex = 0;
            txtMaVT.ReadOnly = false;
            txtMaVT.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu(out decimal donGia)) return;

            string ma = txtMaVT.Text.Trim();
            if (MaDaTonTai(ma))
            {
                MessageBox.Show("Mã vật tư \"" + ma + "\" đã tồn tại!", "Trùng mã",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaVT.Focus();
                txtMaVT.SelectAll();
                return;
            }

            ListViewItem item = new ListViewItem(ma);
            item.SubItems.Add(txtTenVT.Text.Trim());
            item.SubItems.Add(cboDonViTinh.SelectedItem.ToString());
            item.SubItems.Add(donGia.ToString("N0", CultureInfo.CurrentCulture));
            lvVatTu.Items.Add(item);

            lvVatTu.SelectedItems.Clear();
            LamSachOnhap();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (lvVatTu.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật!", "Chưa chọn dòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!KiemTraDuLieu(out decimal donGia)) return;

            ListViewItem item = lvVatTu.SelectedItems[0];
            string ma = txtMaVT.Text.Trim();

            // Nếu đổi mã thì không được trùng với dòng khác
            if (!string.Equals(item.Text, ma, StringComparison.OrdinalIgnoreCase) && MaDaTonTai(ma))
            {
                MessageBox.Show("Mã vật tư \"" + ma + "\" đã tồn tại!", "Trùng mã",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaVT.Focus();
                return;
            }

            item.Text = ma;
            item.SubItems[1].Text = txtTenVT.Text.Trim();
            item.SubItems[2].Text = cboDonViTinh.SelectedItem.ToString();
            item.SubItems[3].Text = donGia.ToString("N0", CultureInfo.CurrentCulture);

            MessageBox.Show("Cập nhật thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (lvVatTu.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Chưa chọn dòng",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListViewItem item = lvVatTu.SelectedItems[0];
            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa vật tư \"" + item.Text + " - " + item.SubItems[1].Text + "\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                lvVatTu.Items.Remove(item);
                LamSachOnhap();
            }
        }

        private void btnXoaTatCa_Click(object sender, EventArgs e)
        {
            if (lvVatTu.Items.Count == 0) return;

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa toàn bộ danh sách?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (kq == DialogResult.Yes)
            {
                lvVatTu.Items.Clear();
                LamSachOnhap();
            }
        }

        // Chọn dòng -> đẩy dữ liệu ngược lại các ô bên trái
        private void lvVatTu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvVatTu.SelectedItems.Count == 0) return;

            ListViewItem item = lvVatTu.SelectedItems[0];
            txtMaVT.Text = item.Text;
            txtTenVT.Text = item.SubItems[1].Text;
            cboDonViTinh.SelectedItem = item.SubItems[2].Text;
            txtDonGia.Text = item.SubItems[3].Text;
        }
    }
}