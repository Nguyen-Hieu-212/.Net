using System;
using System.Collections.Generic;
using HRM.DAL;
using HRM.DTO;

namespace HRM.BUS
{
    public class NhanVienBUS
    {
        private readonly NhanVienDAL dal = new NhanVienDAL();

        public List<NhanVienDTO> Search(string keyword, int maPB)
        {
            return dal.Search((keyword ?? "").Trim(), maPB);
        }

        private static void Validate(NhanVienDTO nv)
        {
            if (string.IsNullOrWhiteSpace(nv.HoTen)) throw new ArgumentException("Họ tên không được để trống.");
            if (string.IsNullOrWhiteSpace(nv.CCCD)) throw new ArgumentException("Số CCCD không được để trống.");
            if (nv.CCCD.Trim().Length > 20) throw new ArgumentException("Số CCCD tối đa 20 ký tự.");
            if (nv.NgaySinh.HasValue && nv.NgaySinh.Value.Date >= DateTime.Today)
                throw new ArgumentException("Ngày sinh phải nhỏ hơn ngày hiện tại.");
            if (!nv.MaPB.HasValue) throw new ArgumentException("Hãy chọn phòng ban.");
            if (nv.LuongCoBan <= 0) throw new ArgumentException("Lương cơ bản phải lớn hơn 0.");
            if (nv.HeSoLuong <= 0) throw new ArgumentException("Hệ số lương phải lớn hơn 0.");
            nv.HoTen = nv.HoTen.Trim();
            nv.CCCD = nv.CCCD.Trim();
        }

        public void Them(NhanVienDTO nv) { Validate(nv); dal.Insert(nv); }

        public void Sua(NhanVienDTO nv)
        {
            Validate(nv);
            if (dal.Update(nv) == 0) throw new InvalidOperationException("Không tìm thấy nhân viên cần sửa.");
        }

        public void ChuyenPhongBan(int maNV, int maPB)
        {
            if (maNV <= 0) throw new ArgumentException("Hãy chọn nhân viên cần chuyển phòng ban.");
            if (maPB <= 0) throw new ArgumentException("Hãy chọn phòng ban mới.");
            if (dal.ChuyenPhongBan(maNV, maPB) == 0) throw new InvalidOperationException("Không tìm thấy nhân viên.");
        }

        public void Xoa(int maNV)
        {
            if (maNV <= 0) throw new ArgumentException("Chưa chọn nhân viên cần xóa.");
            dal.Delete(maNV);
        }
    }
}
