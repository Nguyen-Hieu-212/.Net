namespace RetailSales.DTO
{
    public class KhachHangDTO
    {
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public string DienThoai { get; set; }
        public int DiemTichLuy { get; set; }

        // Dùng cho ComboBox
        public string HienThi
        {
            get { return MaKH == 0 ? TenKH : TenKH + " - " + DienThoai; }
        }
    }
}
