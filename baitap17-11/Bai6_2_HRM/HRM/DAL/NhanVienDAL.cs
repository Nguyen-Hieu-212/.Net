using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;

namespace HRM.DAL
{
    public class NhanVienDAL
    {
        private const string SelectSql =
            @"SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.CCCD, nv.MaPB, pb.TenPB, nv.LuongCoBan, nv.HeSoLuong
              FROM NhanVien nv LEFT JOIN PhongBan pb ON pb.MaPB = nv.MaPB";

        private static NhanVienDTO Map(SqlDataReader rd)
        {
            return new NhanVienDTO
            {
                MaNV = rd.GetInt32(0),
                HoTen = rd.GetString(1),
                NgaySinh = rd.IsDBNull(2) ? (DateTime?)null : rd.GetDateTime(2),
                CCCD = rd.IsDBNull(3) ? "" : rd.GetString(3),
                MaPB = rd.IsDBNull(4) ? (int?)null : rd.GetInt32(4),
                TenPB = rd.IsDBNull(5) ? "" : rd.GetString(5),
                LuongCoBan = rd.GetDecimal(6),
                HeSoLuong = rd.GetDouble(7)
            };
        }

        /// <summary>Tìm theo tên hoặc CCCD (LIKE %...%), có thể lọc thêm theo phòng ban (maPB = 0: tất cả).</summary>
        public List<NhanVienDTO> Search(string keyword, int maPB)
        {
            return DbHelper.ExecuteList(
                SelectSql + @" WHERE (nv.HoTen LIKE @kw OR nv.CCCD LIKE @kw) AND (@pb = 0 OR nv.MaPB = @pb)
                               ORDER BY nv.HoTen", Map,
                DbHelper.P("@kw", SqlDbType.NVarChar, 110, "%" + (keyword ?? "") + "%"),
                DbHelper.P("@pb", SqlDbType.Int, maPB));
        }

        public int Insert(NhanVienDTO nv)
        {
            return DbHelper.ExecuteNonQuery(
                @"INSERT INTO NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong)
                  VALUES (@ten, @ns, @cccd, @pb, @luong, @hs)",
                DbHelper.P("@ten", SqlDbType.NVarChar, 100, nv.HoTen),
                DbHelper.P("@ns", SqlDbType.DateTime, nv.NgaySinh),
                DbHelper.P("@cccd", SqlDbType.VarChar, 20, nv.CCCD),
                DbHelper.P("@pb", SqlDbType.Int, nv.MaPB),
                DbHelper.PM("@luong", nv.LuongCoBan),
                DbHelper.P("@hs", SqlDbType.Float, nv.HeSoLuong));
        }

        public int Update(NhanVienDTO nv)
        {
            return DbHelper.ExecuteNonQuery(
                @"UPDATE NhanVien SET HoTen = @ten, NgaySinh = @ns, CCCD = @cccd, MaPB = @pb,
                         LuongCoBan = @luong, HeSoLuong = @hs
                  WHERE MaNV = @ma",
                DbHelper.P("@ma", SqlDbType.Int, nv.MaNV),
                DbHelper.P("@ten", SqlDbType.NVarChar, 100, nv.HoTen),
                DbHelper.P("@ns", SqlDbType.DateTime, nv.NgaySinh),
                DbHelper.P("@cccd", SqlDbType.VarChar, 20, nv.CCCD),
                DbHelper.P("@pb", SqlDbType.Int, nv.MaPB),
                DbHelper.PM("@luong", nv.LuongCoBan),
                DbHelper.P("@hs", SqlDbType.Float, nv.HeSoLuong));
        }

        public int ChuyenPhongBan(int maNV, int maPB)
        {
            return DbHelper.ExecuteNonQuery("UPDATE NhanVien SET MaPB = @pb WHERE MaNV = @ma",
                DbHelper.P("@ma", SqlDbType.Int, maNV),
                DbHelper.P("@pb", SqlDbType.Int, maPB));
        }

        public int Delete(int maNV)
        {
            return DbHelper.ExecuteNonQuery("DELETE FROM NhanVien WHERE MaNV = @ma",
                DbHelper.P("@ma", SqlDbType.Int, maNV));
        }
    }
}
