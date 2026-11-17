using System;
using System.Collections.Generic;
using System.Linq;
using RetailSales.DAL;
using RetailSales.DTO;

namespace RetailSales.BUS
{
    public class HoaDonBUS
    {
        private readonly HoaDonDAL dal = new HoaDonDAL();

        /// <summary>Cứ 100.000đ được 1 điểm (làm tròn xuống).</summary>
        public static int TinhDiem(decimal tongTien)
        {
            return (int)Math.Floor(tongTien / 100000m);
        }

        public static decimal TinhTongTien(IEnumerable<ChiTietHoaDonDTO> gio)
        {
            return gio.Sum(x => x.ThanhTien);
        }

        /// <summary>Thanh toán giỏ hàng. maKH = null nghĩa là khách lẻ. Trả về mã hóa đơn.</summary>
        public int ThanhToan(int maNV, int? maKH, IList<ChiTietHoaDonDTO> gio)
        {
            if (maNV <= 0) throw new ArgumentException("Chưa chọn nhân viên bán hàng.");
            if (gio == null || gio.Count == 0) throw new ArgumentException("Giỏ hàng đang trống.");
            if (gio.Any(x => x.SoLuong <= 0)) throw new ArgumentException("Số lượng phải lớn hơn 0.");

            HoaDonDTO hd = new HoaDonDTO
            {
                NgayLap = DateTime.Now,
                MaNV = maNV,
                MaKH = maKH,
                TongTien = TinhTongTien(gio),
                ChiTiet = gio.ToList()
            };
            int diem = maKH.HasValue ? TinhDiem(hd.TongTien) : 0;
            return dal.LapHoaDon(hd, diem);
        }
    }
}
