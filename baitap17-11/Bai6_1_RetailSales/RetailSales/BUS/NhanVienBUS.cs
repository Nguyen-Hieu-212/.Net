using System;
using System.Collections.Generic;
using RetailSales.DAL;
using RetailSales.DTO;

namespace RetailSales.BUS
{
    public class NhanVienBUS
    {
        private readonly NhanVienDAL dal = new NhanVienDAL();

        public List<NhanVienDTO> GetAll() { return dal.GetAll(); }

        private static void Validate(NhanVienDTO nv)
        {
            if (string.IsNullOrWhiteSpace(nv.TenNV)) throw new ArgumentException("Tên nhân viên không được để trống.");
            if (string.IsNullOrWhiteSpace(nv.SoDienThoai)) throw new ArgumentException("Số điện thoại không được để trống.");
            if (nv.SoDienThoai.Trim().Length > 15) throw new ArgumentException("Số điện thoại tối đa 15 ký tự.");
            nv.TenNV = nv.TenNV.Trim();
            nv.SoDienThoai = nv.SoDienThoai.Trim();
            nv.ChucVu = (nv.ChucVu ?? "").Trim();
        }

        public void Them(NhanVienDTO nv) { Validate(nv); dal.Insert(nv); }

        public void Sua(NhanVienDTO nv)
        {
            Validate(nv);
            if (dal.Update(nv) == 0) throw new InvalidOperationException("Không tìm thấy nhân viên cần sửa.");
        }

        public void Xoa(int maNV)
        {
            if (maNV <= 0) throw new ArgumentException("Chưa chọn nhân viên cần xóa.");
            dal.Delete(maNV);
        }
    }
}
