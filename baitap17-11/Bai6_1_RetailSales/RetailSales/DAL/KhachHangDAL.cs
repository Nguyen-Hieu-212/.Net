using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using RetailSales.DTO;

namespace RetailSales.DAL
{
    public class KhachHangDAL
    {
        private const string SelectSql = "SELECT MaKH, TenKH, DienThoai, DiemTichLuy FROM KhachHang";

        private static KhachHangDTO Map(SqlDataReader rd)
        {
            return new KhachHangDTO
            {
                MaKH = rd.GetInt32(0),
                TenKH = rd.GetString(1),
                DienThoai = rd.IsDBNull(2) ? "" : rd.GetString(2),
                DiemTichLuy = rd.GetInt32(3)
            };
        }

        public List<KhachHangDTO> GetAll()
        {
            return DbHelper.ExecuteList(SelectSql + " ORDER BY TenKH", Map);
        }

        public int Insert(KhachHangDTO kh)
        {
            return DbHelper.ExecuteNonQuery(
                "INSERT INTO KhachHang (TenKH, DienThoai, DiemTichLuy) VALUES (@ten, @dt, @diem)",
                DbHelper.P("@ten", SqlDbType.NVarChar, 100, kh.TenKH),
                DbHelper.P("@dt", SqlDbType.VarChar, 15, kh.DienThoai),
                DbHelper.P("@diem", SqlDbType.Int, kh.DiemTichLuy));
        }

        public int Update(KhachHangDTO kh)
        {
            return DbHelper.ExecuteNonQuery(
                "UPDATE KhachHang SET TenKH = @ten, DienThoai = @dt, DiemTichLuy = @diem WHERE MaKH = @ma",
                DbHelper.P("@ma", SqlDbType.Int, kh.MaKH),
                DbHelper.P("@ten", SqlDbType.NVarChar, 100, kh.TenKH),
                DbHelper.P("@dt", SqlDbType.VarChar, 15, kh.DienThoai),
                DbHelper.P("@diem", SqlDbType.Int, kh.DiemTichLuy));
        }

        public int Delete(int maKH)
        {
            return DbHelper.ExecuteNonQuery("DELETE FROM KhachHang WHERE MaKH = @ma",
                DbHelper.P("@ma", SqlDbType.Int, maKH));
        }
    }
}
