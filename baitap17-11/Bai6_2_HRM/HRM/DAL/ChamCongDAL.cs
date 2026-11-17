using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HRM.DAL
{
    /// <summary>
    /// Chấm công theo MÔ HÌNH NGẮT KẾT NỐI: SqlDataAdapter + DataTable + SqlCommandBuilder.
    /// Mỗi đối tượng giữ adapter của lần nạp gần nhất để adapter.Update() đồng bộ về SQL Server.
    /// </summary>
    public class ChamCongDAL
    {
        private SqlDataAdapter adapter;
        private SqlCommandBuilder builder;

        /// <summary>Tháng vừa nạp đã bị khóa (đã chốt lương) hay chưa.</summary>
        public bool ThangDaKhoa { get; private set; }

        /// <summary>
        /// Nạp bảng chấm công của 1 tháng. Nhân viên chưa có dòng chấm công trong tháng sẽ được
        /// thêm vào DataTable (RowState = Added) với 0 công / 0 giờ OT; chúng chỉ được ghi xuống
        /// CSDL khi gọi Save().
        /// </summary>
        public DataTable Load(string thangNam)
        {
            adapter = new SqlDataAdapter(
                "SELECT MaCong, MaNV, ThangNam, SoNgayCong, SoGioOT, DaKhoa FROM BangChamCong WHERE ThangNam = @t",
                DbHelper.ConnectionString);
            adapter.SelectCommand.Parameters.Add("@t", SqlDbType.VarChar, 7).Value = thangNam;
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            builder = new SqlCommandBuilder(adapter);
            builder.ConflictOption = ConflictOption.OverwriteChanges;   // WHERE chỉ theo khóa chính MaCong

            DataTable dt = new DataTable("BangChamCong");
            adapter.Fill(dt);

            // Khóa tự tăng tạm thời âm để không trùng với MaCong thật trong CSDL
            DataColumn id = dt.Columns["MaCong"];
            id.AutoIncrement = true;
            id.AutoIncrementSeed = -1;
            id.AutoIncrementStep = -1;

            // Cột hiển thị (không thuộc bảng BangChamCong nên adapter bỏ qua khi Update)
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("PhongBan", typeof(string));

            DataTable nv = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT nv.MaNV, nv.HoTen, ISNULL(pb.TenPB, N'') AS TenPB
                  FROM NhanVien nv LEFT JOIN PhongBan pb ON pb.MaPB = nv.MaPB
                  ORDER BY nv.MaNV", DbHelper.ConnectionString))
            {
                da.Fill(nv);
            }

            Dictionary<int, DataRow> daCo = new Dictionary<int, DataRow>();
            ThangDaKhoa = false;
            foreach (DataRow r in dt.Rows)
            {
                daCo[(int)r["MaNV"]] = r;
                if (Convert.ToBoolean(r["DaKhoa"])) ThangDaKhoa = true;
            }

            foreach (DataRow n in nv.Rows)
            {
                DataRow r;
                if (daCo.TryGetValue((int)n["MaNV"], out r))
                {
                    r["HoTen"] = n["HoTen"];
                    r["PhongBan"] = n["TenPB"];
                }
            }
            dt.AcceptChanges();     // việc gán tên ở trên không được tính là "đã sửa"

            // Thêm nhân viên chưa có chấm công (trừ khi tháng đã chốt lương)
            if (!ThangDaKhoa)
            {
                foreach (DataRow n in nv.Rows)
                {
                    if (daCo.ContainsKey((int)n["MaNV"])) continue;
                    DataRow r = dt.NewRow();
                    r["MaNV"] = n["MaNV"];
                    r["ThangNam"] = thangNam;
                    r["SoNgayCong"] = 0.0;
                    r["SoGioOT"] = 0.0;
                    r["DaKhoa"] = false;
                    r["HoTen"] = n["HoTen"];
                    r["PhongBan"] = n["TenPB"];
                    dt.Rows.Add(r);
                }
            }
            return dt;
        }

        /// <summary>Đồng bộ toàn bộ thay đổi (Added/Modified) về SQL Server trong 1 lần adapter.Update().</summary>
        public int Save(DataTable dt)
        {
            if (adapter == null) throw new InvalidOperationException("Chưa nạp dữ liệu chấm công.");
            return adapter.Update(dt);
        }
    }
}
