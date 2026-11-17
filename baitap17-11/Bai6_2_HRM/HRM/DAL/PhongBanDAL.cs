using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;

namespace HRM.DAL
{
    public class PhongBanDAL
    {
        private static PhongBanDTO Map(SqlDataReader rd)
        {
            return new PhongBanDTO
            {
                MaPB = rd.GetInt32(0),
                TenPB = rd.GetString(1),
                MoTa = rd.IsDBNull(2) ? "" : rd.GetString(2)
            };
        }

        public List<PhongBanDTO> GetAll()
        {
            return DbHelper.ExecuteList("SELECT MaPB, TenPB, Mota FROM PhongBan ORDER BY TenPB", Map);
        }

        public int Insert(PhongBanDTO pb)
        {
            return DbHelper.ExecuteNonQuery("INSERT INTO PhongBan (TenPB, Mota) VALUES (@ten, @mt)",
                DbHelper.P("@ten", SqlDbType.NVarChar, 100, pb.TenPB),
                DbHelper.P("@mt", SqlDbType.NVarChar, 255, pb.MoTa));
        }

        public int Update(PhongBanDTO pb)
        {
            return DbHelper.ExecuteNonQuery("UPDATE PhongBan SET TenPB = @ten, Mota = @mt WHERE MaPB = @ma",
                DbHelper.P("@ma", SqlDbType.Int, pb.MaPB),
                DbHelper.P("@ten", SqlDbType.NVarChar, 100, pb.TenPB),
                DbHelper.P("@mt", SqlDbType.NVarChar, 255, pb.MoTa));
        }

        public int Delete(int maPB)
        {
            return DbHelper.ExecuteNonQuery("DELETE FROM PhongBan WHERE MaPB = @ma",
                DbHelper.P("@ma", SqlDbType.Int, maPB));
        }
    }
}
