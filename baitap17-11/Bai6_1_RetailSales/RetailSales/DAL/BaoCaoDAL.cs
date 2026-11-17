using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RetailSales.DTO;

namespace RetailSales.DAL
{
    public class BaoCaoDAL
    {
        /// <summary>Tổng doanh thu trong khoảng [tuNgay, denNgay) bằng ExecuteScalar.</summary>
        public decimal TongDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            object kq = DbHelper.ExecuteScalar(
                "SELECT ISNULL(SUM(TongTien), 0) FROM HoaDon WHERE NgayLap >= @tu AND NgayLap < @den",
                DbHelper.P("@tu", SqlDbType.DateTime, tuNgay),
                DbHelper.P("@den", SqlDbType.DateTime, denNgay));
            return Convert.ToDecimal(kq);
        }

        /// <summary>Top N sản phẩm bán chạy, đọc bằng SqlDataReader.</summary>
        public List<TopSanPhamDTO> TopSanPham(int top)
        {
            List<TopSanPhamDTO> list = new List<TopSanPhamDTO>();
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT TOP (@top) sp.MaSP, sp.TenSP, SUM(ct.SoLuong) AS TongSL, SUM(ct.SoLuong * ct.DonGia) AS DoanhThu
                  FROM ChiTietHoaDon ct
                  JOIN SanPham sp ON sp.MaSP = ct.MaSP
                  GROUP BY sp.MaSP, sp.TenSP
                  ORDER BY TongSL DESC, DoanhThu DESC", conn))
            {
                cmd.Parameters.Add(DbHelper.P("@top", SqlDbType.Int, top));
                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new TopSanPhamDTO
                        {
                            MaSP = rd.GetString(0),
                            TenSP = rd.GetString(1),
                            TongSoLuong = rd.GetInt32(2),
                            DoanhThu = rd.GetDecimal(3)
                        });
                    }
                }
            }
            return list;
        }
    }
}
