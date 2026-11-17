using System;

namespace HRM.DTO
{
    public class NhanVienDTO
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string CCCD { get; set; }
        public int? MaPB { get; set; }
        public string TenPB { get; set; }       // chỉ để hiển thị (JOIN)
        public decimal LuongCoBan { get; set; }
        public double HeSoLuong { get; set; }
    }
}
