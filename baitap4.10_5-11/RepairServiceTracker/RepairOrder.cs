using System;

namespace RepairServiceTracker
{
    /// <summary>Mot don dang ky sua chua.</summary>
    public class RepairOrder
    {
        public const string New = "Moi tiep nhan";
        public const string Repairing = "Dang sua";
        public const string WaitingParts = "Cho linh kien";
        public const string Completed = "Hoan tat";
        public const string Cancelled = "Da huy";

        public static readonly string[] Statuses = { New, Repairing, WaitingParts, Completed, Cancelled };

        public string Id { get; set; }             // Ma don (duy nhat)
        public string CustomerName { get; set; }   // Ten khach hang
        public string Phone { get; set; }          // So dien thoai
        public string DeviceType { get; set; }     // Loai thiet bi
        public string Problem { get; set; }        // Mo ta loi
        public DateTime ReceivedDate { get; set; } // Ngay tiep nhan
        public decimal EstimatedCost { get; set; } // Chi phi du kien
        public string Status { get; set; }         // Trang thai
        public string ImagePath { get; set; }      // Duong dan anh thiet bi (co the rong)
    }
}