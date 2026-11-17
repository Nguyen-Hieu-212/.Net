using System;
using System.Collections.Generic;
using RetailSales.DAL;
using RetailSales.DTO;

namespace RetailSales.BUS
{
    public class SanPhamBUS
    {
        private readonly SanPhamDAL dal = new SanPhamDAL();

        public List<SanPhamDTO> GetAll() { return dal.GetAll(); }

        public List<SanPhamDTO> Search(string keyword)
        {
            return string.IsNullOrWhiteSpace(keyword) ? dal.GetAll() : dal.Search(keyword.Trim());
        }

        public SanPhamDTO GetById(string maSP)
        {
            return string.IsNullOrWhiteSpace(maSP) ? null : dal.GetById(maSP.Trim());
        }

        private static void Validate(SanPhamDTO sp)
        {
            if (string.IsNullOrWhiteSpace(sp.MaSP)) throw new ArgumentException("Mã sản phẩm không được để trống.");
            if (sp.MaSP.Trim().Length > 20) throw new ArgumentException("Mã sản phẩm tối đa 20 ký tự.");
            if (string.IsNullOrWhiteSpace(sp.TenSP)) throw new ArgumentException("Tên sản phẩm không được để trống.");
            if (sp.GiaBan <= 0) throw new ArgumentException("Giá bán phải lớn hơn 0.");
            if (sp.SoLuongTon < 0) throw new ArgumentException("Số lượng tồn không được âm.");
            sp.MaSP = sp.MaSP.Trim();
            sp.TenSP = sp.TenSP.Trim();
        }

        public void Them(SanPhamDTO sp)
        {
            Validate(sp);
            if (dal.GetById(sp.MaSP) != null) throw new InvalidOperationException("Mã sản phẩm '" + sp.MaSP + "' đã tồn tại.");
            dal.Insert(sp);
        }

        public void Sua(SanPhamDTO sp)
        {
            Validate(sp);
            if (dal.Update(sp) == 0) throw new InvalidOperationException("Không tìm thấy sản phẩm cần sửa.");
        }

        public void Xoa(string maSP)
        {
            if (string.IsNullOrWhiteSpace(maSP)) throw new ArgumentException("Chưa chọn sản phẩm cần xóa.");
            dal.Delete(maSP.Trim());
        }
    }
}
