using System;

namespace ClinicAppointmentManager
{
    /// <summary>Mot lich hen kham.</summary>
    public class Appointment
    {
        public string Id { get; set; }            // Ma lich hen (duy nhat)
        public string PatientName { get; set; }   // Ho ten benh nhan
        public string Phone { get; set; }         // So dien thoai
        public DateTime Time { get; set; }        // Ngay gio hen
        public string Doctor { get; set; }        // Bac si
        public string Reason { get; set; }        // Ly do kham
        public string Status { get; set; }        // Trang thai
    }
}