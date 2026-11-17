using System;
using System.Data;
using System.Data.SqlClient;
using RetailSales.DTO;

namespace RetailSales.DAL
{
    public class HoaDonDAL
    {
        /// <summary>
        /// Lập hóa đơn trong MỘT SqlTransaction (ACID):
        ///   1. Chèn HoaDon
        ///   2. Chèn ChiTietHoaDon
        ///   3. Trừ SoLuongTon (không đủ hàng => Rollback + báo lỗi)
        ///   4. Cộng DiemTichLuy cho khách hàng
        ///   5. Commit
        /// Trả về MaHD vừa tạo.
        /// </summary>
        public int LapHoaDon(HoaDonDTO hd, int diemCong)
        {
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 1. Chèn hóa đơn
                    int maHD;
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO HoaDon (NgayLap, MaNV, MaKH, TongTien) VALUES (@ngay, @nv, @kh, @tong); " +
                        "SELECT CAST(SCOPE_IDENTITY() AS int);", conn, trans))
                    {
                        cmd.Parameters.Add(DbHelper.P("@ngay", SqlDbType.DateTime, hd.NgayLap));
                        cmd.Parameters.Add(DbHelper.P("@nv", SqlDbType.Int, hd.MaNV));
                        cmd.Parameters.Add(DbHelper.P("@kh", SqlDbType.Int, hd.MaKH));
                        cmd.Parameters.Add(DbHelper.PM("@tong", hd.TongTien));
                        maHD = (int)cmd.ExecuteScalar();
                    }

                    foreach (ChiTietHoaDonDTO ct in hd.ChiTiet)
                    {
                        // Khóa dòng sản phẩm (UPDLOCK) rồi kiểm tra tồn kho
                        int ton;
                        using (SqlCommand cmd = new SqlCommand(
                            "SELECT SoLuongTon FROM SanPham WITH (UPDLOCK, ROWLOCK) WHERE MaSP = @ma", conn, trans))
                        {
                            cmd.Parameters.Add(DbHelper.P("@ma", SqlDbType.VarChar, 20, ct.MaSP));
                            object kq = cmd.ExecuteScalar();
                            if (kq == null)
                                throw new InvalidOperationException("Sản phẩm '" + ct.MaSP + "' không tồn tại.");
                            ton = (int)kq;
                        }

                        if (ton < ct.SoLuong)
                        {
                            trans.Rollback();   // hủy toàn bộ giao dịch
                            throw new InvalidOperationException(
                                "Sản phẩm '" + ct.TenSP + "' không đủ tồn kho (còn " + ton + ", cần " + ct.SoLuong +
                                "). Hóa đơn đã bị hủy.");
                        }

                        // 2. Chèn chi tiết hóa đơn
                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia) VALUES (@hd, @sp, @sl, @gia)", conn, trans))
                        {
                            cmd.Parameters.Add(DbHelper.P("@hd", SqlDbType.Int, maHD));
                            cmd.Parameters.Add(DbHelper.P("@sp", SqlDbType.VarChar, 20, ct.MaSP));
                            cmd.Parameters.Add(DbHelper.P("@sl", SqlDbType.Int, ct.SoLuong));
                            cmd.Parameters.Add(DbHelper.PM("@gia", ct.DonGia));
                            cmd.ExecuteNonQuery();
                        }

                        // 3. Trừ tồn kho
                        using (SqlCommand cmd = new SqlCommand(
                            "UPDATE SanPham SET SoLuongTon = SoLuongTon - @sl WHERE MaSP = @ma", conn, trans))
                        {
                            cmd.Parameters.Add(DbHelper.P("@sl", SqlDbType.Int, ct.SoLuong));
                            cmd.Parameters.Add(DbHelper.P("@ma", SqlDbType.VarChar, 20, ct.MaSP));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 4. Cộng điểm tích lũy (chỉ khi không phải khách lẻ)
                    if (hd.MaKH.HasValue && diemCong > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand(
                            "UPDATE KhachHang SET DiemTichLuy = DiemTichLuy + @diem WHERE MaKH = @kh", conn, trans))
                        {
                            cmd.Parameters.Add(DbHelper.P("@diem", SqlDbType.Int, diemCong));
                            cmd.Parameters.Add(DbHelper.P("@kh", SqlDbType.Int, hd.MaKH.Value));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 5. Hoàn tất
                    trans.Commit();
                    return maHD;
                }
                catch
                {
                    // Nếu đã Rollback ở trên (hoặc giao dịch đã hỏng) thì bỏ qua lỗi khi Rollback lần nữa
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
