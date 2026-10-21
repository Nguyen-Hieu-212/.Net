using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MdiApp
{
    // Student của bài Quản lý Sinh viên (implement INotifyPropertyChanged để binding 2 chiều)
    public class Student : INotifyPropertyChanged
    {
        private string _studentId = "";
        private string _fullName = "";
        private int _age;
        private string _gender = "Nam";
        private string _avatarPath = "";

        public string StudentId { get => _studentId; set => Set(ref _studentId, value); }
        public string FullName { get => _fullName; set => Set(ref _fullName, value); }
        public int Age { get => _age; set => Set(ref _age, value); }
        public string Gender { get => _gender; set => Set(ref _gender, value); }
        public string AvatarPath { get => _avatarPath; set => Set(ref _avatarPath, value); }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
        {
            if (Equals(field, value)) return;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class Product
    {
        public string ProductId { get; set; } = "";
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    // Kho dữ liệu dùng chung: đóng rồi mở lại Form con vẫn giữ nguyên dữ liệu
    public static class DataStore
    {
        public static BindingList<Student> Students { get; } = new BindingList<Student>
        {
            new Student { StudentId = "SV001", FullName = "Nguyễn Văn A", Age = 20, Gender = "Nam" },
            new Student { StudentId = "SV002", FullName = "Trần Thị B",   Age = 19, Gender = "Nữ" },
            new Student { StudentId = "SV003", FullName = "Lê Văn C",     Age = 21, Gender = "Nam" }
        };

        public static BindingList<Product> Products { get; } = new BindingList<Product>
        {
            new Product { ProductId = "SP001", ProductName = "Bàn phím cơ", Price = 850000, Quantity = 15 },
            new Product { ProductId = "SP002", ProductName = "Chuột không dây", Price = 320000, Quantity = 40 },
            new Product { ProductId = "SP003", ProductName = "Màn hình 24 inch", Price = 3200000, Quantity = 8 }
        };
    }
}