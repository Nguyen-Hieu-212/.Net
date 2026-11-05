using System;

namespace LabEquipmentCheckout
{
    /// <summary>Mot phieu muon thiet bi.</summary>
    public class Loan
    {
        public const string Borrowing = "Dang muon";
        public const string Returned = "Da tra";
        public const string Overdue = "Qua han";

        public string Id { get; set; }               // Ma phieu (duy nhat)
        public string StudentId { get; set; }        // Ma sinh vien
        public string StudentName { get; set; }      // Ho ten sinh vien
        public string Equipment { get; set; }        // Thiet bi
        public int Quantity { get; set; }            // So luong
        public DateTime BorrowDate { get; set; }     // Ngay muon
        public DateTime DueDate { get; set; }        // Han tra
        public DateTime? ReturnedDate { get; set; }  // Ngay tra thuc te (null = chua tra)

        // Tinh trang tu dong: da tra -> Da tra; chua tra va han tra < hom nay -> Qua han; con lai -> Dang muon
        public string Status => GetStatus(ReturnedDate, DueDate);

        public static string GetStatus(DateTime? returned, DateTime due)
        {
            if (returned.HasValue) return Returned;
            return due.Date < DateTime.Today ? Overdue : Borrowing;
        }
    }
}