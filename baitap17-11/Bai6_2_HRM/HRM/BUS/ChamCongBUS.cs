using System;
using System.Data;
using HRM.DAL;

namespace HRM.BUS
{
    public class ChamCongBUS
    {
        private readonly ChamCongDAL dal = new ChamCongDAL();

        /// <summary>Định dạng "MM/yyyy", ví dụ "11/2026".</summary>
        public static string ThangNam(int thang, int nam)
        {
            return thang.ToString("00") + "/" + nam;
        }

        public bool ThangDaKhoa { get { return dal.ThangDaKhoa; } }

        public DataTable Load(int thang, int nam)
        {
            return dal.Load(ThangNam(thang, nam));
        }

        public bool CoThayDoi(DataTable dt)
        {
            return dt != null && dt.GetChanges() != null;
        }

        /// <summary>Kiểm tra dữ liệu rồi gọi adapter.Update() thông qua DAL. Trả về số dòng đã ghi.</summary>
        public int Luu(DataTable dt)
        {
            if (dt == null) throw new InvalidOperationException("Chưa nạp bảng chấm công.");

            foreach (DataRow r in dt.Rows)
            {
                if (r.RowState != DataRowState.Modified && r.RowState != DataRowState.Added) continue;

                string ten = Convert.ToString(r["HoTen"]);

                if (r.RowState == DataRowState.Modified &&
                    Convert.ToBoolean(r["DaKhoa", DataRowVersion.Original]))
                    throw new InvalidOperationException("Dữ liệu của '" + ten + "' đã bị khóa do chốt lương, không được sửa.");

                if (r.IsNull("SoNgayCong") || r.IsNull("SoGioOT"))
                    throw new ArgumentException("Số ngày công / giờ OT của '" + ten + "' không được để trống.");

                double ngay = Convert.ToDouble(r["SoNgayCong"]);
                double ot = Convert.ToDouble(r["SoGioOT"]);
                if (ngay < 0 || ngay > 31)
                    throw new ArgumentException("Số ngày công của '" + ten + "' phải từ 0 đến 31.");
                if (ot < 0)
                    throw new ArgumentException("Số giờ OT của '" + ten + "' không được âm.");
            }

            return dal.Save(dt);
        }
    }
}
