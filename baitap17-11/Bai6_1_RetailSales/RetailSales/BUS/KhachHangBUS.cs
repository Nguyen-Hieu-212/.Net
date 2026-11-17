using System;
using System.Collections.Generic;
using RetailSales.DAL;
using RetailSales.DTO;

namespace RetailSales.BUS
{
    public class KhachHangBUS
    {
        private readonly KhachHangDAL dal = new KhachHangDAL();

        public List<KhachHangDTO> GetAll() { return dal.GetAll(); }

        /// <summary>Danh sách cho màn hình bán hàng: thêm dòng "Khách lẻ" (MaKH = 0) ở đầu.</summary>
        public List<KhachHangDTO> GetAllKemKhachLe()
        {
            List<KhachHangDTO> l = dal.GetAll();
            l.Insert(0, new KhachHangDTO { MaKH = 0, TenKH = "-- Khách lẻ --", DienThoai = "" });
            return l;
        }

        private static void Validate(KhachHangDTO kh)
        {
            if (string.IsNullOrWhiteSpace(kh.TenKH)) throw new ArgumentException("Tên khách hàng không được để trống.");
            if (string.IsNullOrWhiteSpace(kh.DienThoai)) throw new ArgumentException("Số điện thoại không được để trống.");
            if (kh.DienThoai.Trim().Length > 15) throw new ArgumentException("Số điện thoại tối đa 15 ký tự.");
            if (kh.DiemTichLuy < 0) throw new ArgumentException("Điểm tích lũy không được âm.");
            kh.TenKH = kh.TenKH.Trim();
            kh.DienThoai = kh.DienThoai.Trim();
        }

        public void Them(KhachHangDTO kh) { Validate(kh); dal.Insert(kh); }

        public void Sua(KhachHangDTO kh)
        {
            Validate(kh);
            if (dal.Update(kh) == 0) throw new InvalidOperationException("Không tìm thấy khách hàng cần sửa.");
        }

        public void Xoa(int maKH)
        {
            if (maKH <= 0) throw new ArgumentException("Chưa chọn khách hàng cần xóa.");
            dal.Delete(maKH);
        }
    }
}
