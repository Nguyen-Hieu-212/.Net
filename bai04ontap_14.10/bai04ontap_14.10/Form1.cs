using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace bai04ontap_14
{
    public partial class Form1 : Form
    {
        private enum TrangThai { Trong, DangChon, DaDat }

        private const int SoHang = 4;
        private const int SoCot = 5;

        private readonly Button[] dsViTri = new Button[SoHang * SoCot];
        private readonly TrangThai[] trangThai = new TrangThai[SoHang * SoCot];

        // Giá theo khung giờ: 0 = Sáng, 1 = Tối
        private readonly int[] giaKhungGio = { 100000, 150000 };
        private static readonly CultureInfo vn = new CultureInfo("vi-VN");

        // Màu theo trạng thái
        private readonly Color mauTrong = Color.Gainsboro;
        private readonly Color mauDangChon = Color.LimeGreen;
        private readonly Color mauDaDat = Color.IndianRed;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboKhungGio.Items.Add("Sáng - 100.000đ");
            cboKhungGio.Items.Add("Tối - 150.000đ");
            cboKhungGio.SelectedIndex = 0;

            // Vòng lặp for sinh 20 Button vị trí
            for (int i = 0; i < SoHang * SoCot; i++)
            {
                int hang = i / SoCot;
                int cot = i % SoCot;

                Button btn = new Button();
                btn.Name = "btnViTri" + (i + 1);
                btn.Text = ((char)('A' + hang)).ToString() + (cot + 1);   // A1..D5
                btn.Tag = i;
                btn.Dock = DockStyle.Fill;
                btn.Margin = new Padding(4);
                btn.FlatStyle = FlatStyle.Flat;
                btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                // Gán chung 1 hàm xử lý Click cho cả 20 nút
                btn.Click += ViTri_Click;

                dsViTri[i] = btn;
                tlpSoDo.Controls.Add(btn, cot, hang);
            }

            // Một vài vị trí đã có người đặt sẵn (demo)
            int[] daDatSan = { 2, 7, 12, 17 };
            foreach (int i in daDatSan)
            {
                trangThai[i] = TrangThai.DaDat;
            }

            for (int i = 0; i < dsViTri.Length; i++)
            {
                CapNhatMau(i);
            }
            CapNhatThongKe();
        }

        // Hàm xử lý Click dùng chung cho toàn bộ 20 nút
        private void ViTri_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int i = (int)btn.Tag;

            if (trangThai[i] == TrangThai.DaDat)
            {
                MessageBox.Show("Vị trí " + btn.Text + " đã có người đặt!", "Không thể chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Bấm chọn / bỏ chọn
            trangThai[i] = (trangThai[i] == TrangThai.DangChon) ? TrangThai.Trong : TrangThai.DangChon;

            CapNhatMau(i);
            CapNhatThongKe();
        }

        private void CapNhatMau(int i)
        {
            Button btn = dsViTri[i];
            switch (trangThai[i])
            {
                case TrangThai.Trong:
                    btn.BackColor = mauTrong;
                    btn.ForeColor = Color.Black;
                    break;
                case TrangThai.DangChon:
                    btn.BackColor = mauDangChon;
                    btn.ForeColor = Color.White;
                    break;
                case TrangThai.DaDat:
                    btn.BackColor = mauDaDat;
                    btn.ForeColor = Color.White;
                    break;
            }
        }

        private int DemViTriDangChon()
        {
            int dem = 0;
            foreach (TrangThai t in trangThai)
            {
                if (t == TrangThai.DangChon) dem++;
            }
            return dem;
        }

        private int TinhTongTien()
        {
            int idx = cboKhungGio.SelectedIndex;
            if (idx < 0) return 0;
            return DemViTriDangChon() * giaKhungGio[idx];
        }

        // Cập nhật số lượng và tạm tính tiền realtime
        private void CapNhatThongKe()
        {
            lblDangChon.Text = "Số vị trí đang chọn: " + DemViTriDangChon();
            lblTamTinh.Text = "Tạm tính tiền: " + TinhTongTien().ToString("N0", vn) + "đ";
        }

        private void cboKhungGio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatThongKe();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (DemViTriDangChon() == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một vị trí!", "Chưa chọn vị trí",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> dsTen = new List<string>();
            for (int i = 0; i < dsViTri.Length; i++)
            {
                if (trangThai[i] == TrangThai.DangChon) dsTen.Add(dsViTri[i].Text);
            }

            string noiDung =
                "Vị trí: " + string.Join(", ", dsTen) + "\n" +
                "Khung giờ: " + cboKhungGio.SelectedItem + "\n" +
                "Tổng tiền: " + TinhTongTien().ToString("N0", vn) + "đ\n\n" +
                "Xác nhận đặt chỗ?";

            DialogResult kq = MessageBox.Show(noiDung, "Xác nhận đặt",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (kq != DialogResult.Yes) return;

            // Chuyển các vị trí đang chọn sang trạng thái đã đặt (đỏ)
            for (int i = 0; i < dsViTri.Length; i++)
            {
                if (trangThai[i] == TrangThai.DangChon)
                {
                    trangThai[i] = TrangThai.DaDat;
                    CapNhatMau(i);
                }
            }
            CapNhatThongKe();

            MessageBox.Show("Đặt chỗ thành công!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHuyTatCa_Click(object sender, EventArgs e)
        {
            // Chỉ hủy các vị trí đang chọn, không đụng tới vị trí đã đặt
            for (int i = 0; i < dsViTri.Length; i++)
            {
                if (trangThai[i] == TrangThai.DangChon)
                {
                    trangThai[i] = TrangThai.Trong;
                    CapNhatMau(i);
                }
            }
            CapNhatThongKe();
        }
    }
}