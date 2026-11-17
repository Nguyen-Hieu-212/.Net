using System;
using System.Collections.Generic;
using HRM.DAL;
using HRM.DTO;

namespace HRM.BUS
{
    public class LuongBUS
    {
        public const int NgayCongChuan = 22;
        public const decimal TienOT = 50000m;

        private readonly LuongDAL dal = new LuongDAL();

        /// <summary>ThucLinh = (LuongCoBan * HeSoLuong / 22) * SoNgayCong + (SoGioOT * 50.000)</summary>
        public static decimal TinhThucLinh(decimal luongCoBan, double heSoLuong, double soNgayCong, double soGioOT)
        {
            decimal luongNgay = luongCoBan * (decimal)heSoLuong / NgayCongChuan;
            decimal thuc = luongNgay * (decimal)soNgayCong + (decimal)soGioOT * TienOT;
            return Math.Round(thuc, 2);
        }

        /// <summary>
        /// Nếu tháng đã chốt: trả về bảng lương đã lưu. Nếu chưa: tính lương dự kiến từ chấm công.
        /// </summary>
        public List<LuongDTO> XemBangLuong(int thang, int nam, out bool daChot)
        {
            string tn = ChamCongBUS.ThangNam(thang, nam);
            daChot = dal.DaChot(tn);
            if (daChot) return dal.GetBangLuongDaChot(tn);

            List<LuongDTO> ds = dal.GetDuLieuTinhLuong(tn);
            foreach (LuongDTO l in ds)
                l.ThucLinh = TinhThucLinh(l.LuongCoBan, l.HeSoLuong, l.SoNgayCong, l.SoGioOT);
            return ds;
        }

        public void ChotLuong(int thang, int nam)
        {
            string tn = ChamCongBUS.ThangNam(thang, nam);
            if (dal.DaChot(tn)) throw new InvalidOperationException("Tháng " + tn + " đã được chốt lương.");

            List<LuongDTO> ds = dal.GetDuLieuTinhLuong(tn);
            if (ds.Count == 0)
                throw new InvalidOperationException("Tháng " + tn + " chưa có dữ liệu chấm công (hãy chấm công và lưu trước).");

            foreach (LuongDTO l in ds)
                l.ThucLinh = TinhThucLinh(l.LuongCoBan, l.HeSoLuong, l.SoNgayCong, l.SoGioOT);

            dal.ChotLuong(tn, ds);      // SqlTransaction nằm trong DAL
        }
    }
}
