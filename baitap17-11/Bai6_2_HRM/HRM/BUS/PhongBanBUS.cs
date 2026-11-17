using System;
using System.Collections.Generic;
using HRM.DAL;
using HRM.DTO;

namespace HRM.BUS
{
    public class PhongBanBUS
    {
        private readonly PhongBanDAL dal = new PhongBanDAL();

        public List<PhongBanDTO> GetAll() { return dal.GetAll(); }

        private static void Validate(PhongBanDTO pb)
        {
            if (string.IsNullOrWhiteSpace(pb.TenPB)) throw new ArgumentException("Tên phòng ban không được để trống.");
            pb.TenPB = pb.TenPB.Trim();
            pb.MoTa = (pb.MoTa ?? "").Trim();
        }

        public void Them(PhongBanDTO pb) { Validate(pb); dal.Insert(pb); }

        public void Sua(PhongBanDTO pb)
        {
            Validate(pb);
            if (dal.Update(pb) == 0) throw new InvalidOperationException("Không tìm thấy phòng ban cần sửa.");
        }

        public void Xoa(int maPB)
        {
            if (maPB <= 0) throw new ArgumentException("Chưa chọn phòng ban cần xóa.");
            dal.Delete(maPB);
        }
    }
}
