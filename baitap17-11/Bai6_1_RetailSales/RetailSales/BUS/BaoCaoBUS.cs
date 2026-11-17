using System;
using System.Collections.Generic;
using RetailSales.DAL;
using RetailSales.DTO;

namespace RetailSales.BUS
{
    public class BaoCaoBUS
    {
        private readonly BaoCaoDAL dal = new BaoCaoDAL();

        public decimal DoanhThuNgay(DateTime ngay)
        {
            DateTime tu = ngay.Date;
            return dal.TongDoanhThu(tu, tu.AddDays(1));
        }

        public decimal DoanhThuThang(int thang, int nam)
        {
            DateTime tu = new DateTime(nam, thang, 1);
            return dal.TongDoanhThu(tu, tu.AddMonths(1));
        }

        public List<TopSanPhamDTO> Top5SanPham()
        {
            return dal.TopSanPham(5);
        }
    }
}
