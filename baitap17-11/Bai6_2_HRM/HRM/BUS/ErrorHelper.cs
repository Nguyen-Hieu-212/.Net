using System;
using System.Data.SqlClient;

namespace HRM.BUS
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
                    return "Dữ liệu bị trùng (số CCCD đã tồn tại, hoặc nhân viên đã có dữ liệu của tháng này).";
                case 547:
                    return "Không thể thực hiện: dữ liệu đang được sử dụng ở bảng khác " +
                           "(nhân viên thuộc phòng ban, đã có chấm công/lương...) hoặc giá trị vi phạm ràng buộc " +
                           "(số ngày công 0-31, lương cơ bản > 0).";
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
