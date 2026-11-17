using System;
using System.Data.SqlClient;

namespace RetailSales.BUS
{
    /// <summary>Chuyển lỗi kỹ thuật (SqlException...) thành thông báo dễ hiểu cho người dùng.</summary>
    public static class ErrorHelper
    {
        public static string GetMessage(Exception ex)
        {
            SqlException sql = ex as SqlException;
            if (sql == null) return ex.Message;

            switch (sql.Number)
            {
                case 2627:
                case 2601:
                    return "Dữ liệu bị trùng (mã sản phẩm hoặc số điện thoại đã tồn tại).";
                case 547:
                    return "Không thể thực hiện: dữ liệu đang được sử dụng ở bảng khác (hóa đơn...) " +
                           "hoặc giá trị vi phạm ràng buộc (giá bán > 0, tồn kho >= 0).";
                case -1:
                case 2:
                case 53:
                    return "Không kết nối được SQL Server. Hãy kiểm tra chuỗi kết nối trong App.config.";
                default:
                    return "Lỗi cơ sở dữ liệu: " + sql.Message;
            }
        }
    }
}
