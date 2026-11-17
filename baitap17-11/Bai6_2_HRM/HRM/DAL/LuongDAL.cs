using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HRM.DTO;

namespace HRM.DAL
{
    public class LuongDAL
    {
        /// <summary>Dữ liệu để tính lương: nhân viên có dòng chấm công trong tháng (ThucLinh chưa tính).</summary>
        public List<LuongDTO> GetDuLieuTinhLuong(string thangNam)
        {
            return DbHelper.ExecuteList(
                @"SELECT nv.MaNV, nv.HoTen, ISNULL(pb.TenPB, N''), cc.ThangNam,
                         nv.LuongCoBan, nv.HeSoLuong, cc.SoNgayCong, cc.SoGioOT
                  FROM BangChamCong cc
                  JOIN NhanVien nv ON nv.MaNV = cc.MaNV
                  LEFT JOIN PhongBan pb ON pb.MaPB = nv.MaPB
                  WHERE cc.ThangNam = @t
                  ORDER BY nv.MaNV",
                rd => new LuongDTO
                {
                    MaNV = rd.GetInt32(0),
                    HoTen = rd.GetString(1),
                    TenPB = rd.GetString(2),
                    ThangNam = rd.GetString(3),
                    LuongCoBan = rd.GetDecimal(4),
                    HeSoLuong = rd.GetDouble(5),
                    SoNgayCong = rd.GetDouble(6),
                    SoGioOT = rd.GetDouble(7)
                },
                DbHelper.P("@t", SqlDbType.VarChar, 7, thangNam));
        }

        /// <summary>Bảng lương ĐÃ CHỐT (ThucLinh lấy từ bảng BangLuong).</summary>
        public List<LuongDTO> GetBangLuongDaChot(string thangNam)
        {
            return DbHelper.ExecuteList(
                @"SELECT nv.MaNV, nv.HoTen, ISNULL(pb.TenPB, N''), bl.ThangNam,
                         nv.LuongCoBan, nv.HeSoLuong, ISNULL(cc.SoNgayCong, 0), ISNULL(cc.SoGioOT, 0), bl.ThucLinh
                  FROM BangLuong bl
                  JOIN NhanVien nv ON nv.MaNV = bl.MaNV
                  LEFT JOIN PhongBan pb ON pb.MaPB = nv.MaPB
                  LEFT JOIN BangChamCong cc ON cc.MaNV = bl.MaNV AND cc.ThangNam = bl.ThangNam
                  WHERE bl.ThangNam = @t
                  ORDER BY nv.MaNV",
                rd => new LuongDTO
                {
                    MaNV = rd.GetInt32(0),
                    HoTen = rd.GetString(1),
                    TenPB = rd.GetString(2),
                    ThangNam = rd.GetString(3),
                    LuongCoBan = rd.GetDecimal(4),
                    HeSoLuong = rd.GetDouble(5),
                    SoNgayCong = rd.GetDouble(6),
                    SoGioOT = rd.GetDouble(7),
                    ThucLinh = rd.GetDecimal(8)
                },
                DbHelper.P("@t", SqlDbType.VarChar, 7, thangNam));
        }

        public bool DaChot(string thangNam)
        {
            object kq = DbHelper.ExecuteScalar("SELECT COUNT(*) FROM BangLuong WHERE ThangNam = @t",
                DbHelper.P("@t", SqlDbType.VarChar, 7, thangNam));
            return Convert.ToInt32(kq) > 0;
        }

        /// <summary>
        /// CHỐT LƯƠNG trong một SqlTransaction:
        ///   1. Kiểm tra tháng chưa chốt
        ///   2. Ghi bảng lương cho toàn bộ nhân viên có chấm công trong tháng
        ///   3. Khóa dữ liệu chấm công (DaKhoa = 1)
        ///   4. Commit; bất kỳ lỗi nào => Rollback (không ghi dở dang)
        /// </summary>
        public void ChotLuong(string thangNam, List<LuongDTO> ds)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM BangLuong WHERE ThangNam = @t", conn, trans))
                    {
                        cmd.Parameters.Add(DbHelper.P("@t", SqlDbType.VarChar, 7, thangNam));
                        if ((int)cmd.ExecuteScalar() > 0)
                            throw new InvalidOperationException("Tháng " + thangNam + " đã được chốt lương trước đó.");
                    }

                    foreach (LuongDTO l in ds)
                    {
                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO BangLuong (MaNV, ThangNam, ThucLinh) VALUES (@nv, @t, @tl)", conn, trans))
                        {
                            cmd.Parameters.Add(DbHelper.P("@nv", SqlDbType.Int, l.MaNV));
                            cmd.Parameters.Add(DbHelper.P("@t", SqlDbType.VarChar, 7, thangNam));
                            cmd.Parameters.Add(DbHelper.PM("@tl", l.ThucLinh));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    int soDongKhoa;
                    using (SqlCommand cmd = new SqlCommand(
                        "UPDATE BangChamCong SET DaKhoa = 1 WHERE ThangNam = @t AND DaKhoa = 0", conn, trans))
                    {
                        cmd.Parameters.Add(DbHelper.P("@t", SqlDbType.VarChar, 7, thangNam));
                        soDongKhoa = cmd.ExecuteNonQuery();
                    }

                    if (soDongKhoa != ds.Count)
                        throw new InvalidOperationException(
                            "Dữ liệu chấm công đã thay đổi trong lúc chốt lương. Vui lòng xem lại bảng lương và thử lại.");

                    trans.Commit();
                }
                catch
                {
                    try { if (trans.Connection != null) trans.Rollback(); } catch { }
                    throw;
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }
    }
}
