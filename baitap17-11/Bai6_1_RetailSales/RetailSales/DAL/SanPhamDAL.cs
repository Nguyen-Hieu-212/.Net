using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RetailSales.DTO;

namespace RetailSales.DAL
{
    public class SanPhamDAL
    {
        private const string SelectSql = "SELECT MaSP, TenSP, GiaBan, SoLuongTon FROM SanPham";

        private static SanPhamDTO Map(SqlDataReader rd)
        {
            return new SanPhamDTO
            {
                MaSP = rd.GetString(0),
                TenSP = rd.GetString(1),
                GiaBan = rd.GetDecimal(2),
                SoLuongTon = rd.GetInt32(3)
            };
        }

        public List<SanPhamDTO> GetAll()
        {
            return DbHelper.ExecuteList(SelectSql + " ORDER BY TenSP", Map);
        }

        /// <summary>Tìm theo tên hoặc mã vạch, dùng SqlParameter có ký tự đại diện %.</summary>
        public List<SanPhamDTO> Search(string keyword)
        {
            return DbHelper.ExecuteList(
                SelectSql + " WHERE MaSP LIKE @kw OR TenSP LIKE @kw ORDER BY TenSP", Map,
                DbHelper.P("@kw", SqlDbType.NVarChar, 160, "%" + keyword + "%"));
        }

        public SanPhamDTO GetById(string maSP)
        {
            List<SanPhamDTO> l = DbHelper.ExecuteList(SelectSql + " WHERE MaSP = @ma", Map,
                DbHelper.P("@ma", SqlDbType.VarChar, 20, maSP));
            return l.Count > 0 ? l[0] : null;
        }

        public int Insert(SanPhamDTO sp)
        {
            return DbHelper.ExecuteNonQuery(
                "INSERT INTO SanPham (MaSP, TenSP, GiaBan, SoLuongTon) VALUES (@ma, @ten, @gia, @ton)",
                DbHelper.P("@ma", SqlDbType.VarChar, 20, sp.MaSP),
                DbHelper.P("@ten", SqlDbType.NVarChar, 150, sp.TenSP),
                DbHelper.PM("@gia", sp.GiaBan),
                DbHelper.P("@ton", SqlDbType.Int, sp.SoLuongTon));
        }

        public int Update(SanPhamDTO sp)
        {
            return DbHelper.ExecuteNonQuery(
                "UPDATE SanPham SET TenSP = @ten, GiaBan = @gia, SoLuongTon = @ton WHERE MaSP = @ma",
                DbHelper.P("@ma", SqlDbType.VarChar, 20, sp.MaSP),
                DbHelper.P("@ten", SqlDbType.NVarChar, 150, sp.TenSP),
                DbHelper.PM("@gia", sp.GiaBan),
                DbHelper.P("@ton", SqlDbType.Int, sp.SoLuongTon));
        }

        public int Delete(string maSP)
        {
            return DbHelper.ExecuteNonQuery("DELETE FROM SanPham WHERE MaSP = @ma",
                DbHelper.P("@ma", SqlDbType.VarChar, 20, maSP));
        }
    }
}
