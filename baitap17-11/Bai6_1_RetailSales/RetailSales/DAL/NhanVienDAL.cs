using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RetailSales.DTO;

namespace RetailSales.DAL
{
    public class NhanVienDAL
    {
        private const string SelectSql = "SELECT MaNV, TenNV, SoDienThoai, ChucVu FROM NhanVien";

        private static NhanVienDTO Map(SqlDataReader rd)
        {
            return new NhanVienDTO
            {
                MaNV = rd.GetInt32(0),
                TenNV = rd.GetString(1),
                SoDienThoai = rd.IsDBNull(2) ? "" : rd.GetString(2),
                ChucVu = rd.IsDBNull(3) ? "" : rd.GetString(3)
            };
        }

        public List<NhanVienDTO> GetAll()
        {
            return DbHelper.ExecuteList(SelectSql + " ORDER BY TenNV", Map);
        }

        public int Insert(NhanVienDTO nv)
        {
            return DbHelper.ExecuteNonQuery(
                "INSERT INTO NhanVien (TenNV, SoDienThoai, ChucVu) VALUES (@ten, @sdt, @cv)",
                DbHelper.P("@ten", SqlDbType.NVarChar, 100, nv.TenNV),
                DbHelper.P("@sdt", SqlDbType.VarChar, 15, nv.SoDienThoai),
                DbHelper.P("@cv", SqlDbType.NVarChar, 50, nv.ChucVu));
        }

        public int Update(NhanVienDTO nv)
        {
            return DbHelper.ExecuteNonQuery(
                "UPDATE NhanVien SET TenNV = @ten, SoDienThoai = @sdt, ChucVu = @cv WHERE MaNV = @ma",
                DbHelper.P("@ma", SqlDbType.Int, nv.MaNV),
                DbHelper.P("@ten", SqlDbType.NVarChar, 100, nv.TenNV),
                DbHelper.P("@sdt", SqlDbType.VarChar, 15, nv.SoDienThoai),
                DbHelper.P("@cv", SqlDbType.NVarChar, 50, nv.ChucVu));
        }

        public int Delete(int maNV)
        {
            return DbHelper.ExecuteNonQuery("DELETE FROM NhanVien WHERE MaNV = @ma",
                DbHelper.P("@ma", SqlDbType.Int, maNV));
        }
    }
}
