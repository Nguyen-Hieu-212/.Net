/* ===========================================================
   BÀI 6.1 - SalesDB (Retail Sales System)
   Chạy toàn bộ script trong SSMS (Execute / F5).
   =========================================================== */
USE master;
GO
IF DB_ID(N'SalesDB') IS NOT NULL
BEGIN
    ALTER DATABASE SalesDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SalesDB;
END
GO
CREATE DATABASE SalesDB;
GO
USE SalesDB;
GO

CREATE TABLE NhanVien (
    MaNV        INT IDENTITY(1,1) CONSTRAINT PK_NhanVien PRIMARY KEY,
    TenNV       NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15)   NULL CONSTRAINT UQ_NhanVien_SDT UNIQUE,
    ChucVu      NVARCHAR(50)  NULL
);

CREATE TABLE KhachHang (
    MaKH        INT IDENTITY(1,1) CONSTRAINT PK_KhachHang PRIMARY KEY,
    TenKH       NVARCHAR(100) NOT NULL,
    DienThoai   VARCHAR(15)   NULL CONSTRAINT UQ_KhachHang_DT UNIQUE,
    DiemTichLuy INT NOT NULL CONSTRAINT DF_KhachHang_Diem DEFAULT 0
);

CREATE TABLE SanPham (
    MaSP        VARCHAR(20)   CONSTRAINT PK_SanPham PRIMARY KEY,
    TenSP       NVARCHAR(150) NOT NULL,
    GiaBan      DECIMAL(18,2) NOT NULL CONSTRAINT CK_SanPham_Gia CHECK (GiaBan > 0),
    SoLuongTon  INT           NOT NULL CONSTRAINT CK_SanPham_Ton CHECK (SoLuongTon >= 0)
);

CREATE TABLE HoaDon (
    MaHD     INT IDENTITY(1,1) CONSTRAINT PK_HoaDon PRIMARY KEY,
    NgayLap  DATETIME NOT NULL CONSTRAINT DF_HoaDon_Ngay DEFAULT GETDATE(),
    MaNV     INT NOT NULL CONSTRAINT FK_HoaDon_NV REFERENCES NhanVien(MaNV),
    MaKH     INT NULL     CONSTRAINT FK_HoaDon_KH REFERENCES KhachHang(MaKH),   -- NULL = khách lẻ
    TongTien DECIMAL(18,2) NOT NULL CONSTRAINT DF_HoaDon_Tong DEFAULT 0
);

CREATE TABLE ChiTietHoaDon (
    MaHD    INT         NOT NULL CONSTRAINT FK_CT_HD REFERENCES HoaDon(MaHD),
    MaSP    VARCHAR(20) NOT NULL CONSTRAINT FK_CT_SP REFERENCES SanPham(MaSP),
    SoLuong INT         NOT NULL CONSTRAINT CK_CT_SL CHECK (SoLuong > 0),
    DonGia  DECIMAL(18,2) NOT NULL,
    CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY (MaHD, MaSP)
);
GO

/* ---------- Dữ liệu mẫu ---------- */
INSERT INTO NhanVien (TenNV, SoDienThoai, ChucVu) VALUES
 (N'Nguyễn Văn An',  '0901000001', N'Thu ngân'),
 (N'Trần Thị Bình',  '0901000002', N'Thu ngân'),
 (N'Lê Quốc Cường',  '0901000003', N'Quản lý');

INSERT INTO KhachHang (TenKH, DienThoai, DiemTichLuy) VALUES
 (N'Phạm Minh Đức',  '0912000001', 0),
 (N'Hoàng Thu Hà',   '0912000002', 15),
 (N'Vũ Thanh Tùng',  '0912000003', 40);

INSERT INTO SanPham (MaSP, TenSP, GiaBan, SoLuongTon) VALUES
 ('8934563138164', N'Mì Hảo Hảo tôm chua cay',       4500,  200),
 ('8934588012013', N'Nước ngọt Coca-Cola lon 330ml', 10000, 150),
 ('8935049510012', N'Sữa tươi Vinamilk 1L',          32000,  80),
 ('8936036020018', N'Dầu ăn Simply 1L',              58000,  40),
 ('8934673101010', N'Bánh quy Cosy hộp 540g',        45000,  60),
 ('8935001234567', N'Gạo ST25 túi 5kg',             165000,  30),
 ('8934804012345', N'Nước mắm Nam Ngư 500ml',        36000,  50),
 ('8936011234567', N'Bột giặt OMO 3kg',             135000,  25),
 ('8935212345678', N'Trứng gà vỉ 10 quả',            38000,  70),
 ('8934567890123', N'Nước suối Aquafina 500ml',       6000, 300);
GO
