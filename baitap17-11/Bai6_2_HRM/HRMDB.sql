/* ===========================================================
   BÀI 6.2 - HRMDB (HRM & Payroll System)
   Chạy toàn bộ script trong SSMS (Execute / F5).
   =========================================================== */
USE master;
GO
IF DB_ID(N'HRMDB') IS NOT NULL
BEGIN
    ALTER DATABASE HRMDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE HRMDB;
END
GO
CREATE DATABASE HRMDB;
GO
USE HRMDB;
GO

CREATE TABLE PhongBan (
    MaPB  INT IDENTITY(1,1) CONSTRAINT PK_PhongBan PRIMARY KEY,
    TenPB NVARCHAR(100) NOT NULL,
    Mota  NVARCHAR(255) NULL
);

CREATE TABLE NhanVien (
    MaNV       INT IDENTITY(1,1) CONSTRAINT PK_NhanVien PRIMARY KEY,
    HoTen      NVARCHAR(100) NOT NULL,
    NgaySinh   DATETIME NULL,
    CCCD       VARCHAR(20) NULL CONSTRAINT UQ_NhanVien_CCCD UNIQUE,
    MaPB       INT NULL CONSTRAINT FK_NhanVien_PB REFERENCES PhongBan(MaPB),
    LuongCoBan DECIMAL(18,2) NOT NULL CONSTRAINT CK_NhanVien_Luong CHECK (LuongCoBan > 0),
    HeSoLuong  FLOAT NOT NULL CONSTRAINT DF_NhanVien_HeSo DEFAULT 1.0
);

/* Bảng chấm công tháng.
   - UNIQUE (MaNV, ThangNam): mỗi nhân viên chỉ có 1 dòng chấm công / tháng.
   - DaKhoa: cột bổ sung để "khóa dữ liệu chấm công" khi chốt lương. */
CREATE TABLE BangChamCong (
    MaCong     INT IDENTITY(1,1) CONSTRAINT PK_BangChamCong PRIMARY KEY,
    MaNV       INT NOT NULL CONSTRAINT FK_CC_NV REFERENCES NhanVien(MaNV),
    ThangNam   VARCHAR(7) NOT NULL,                       -- ví dụ '11/2026'
    SoNgayCong FLOAT NOT NULL CONSTRAINT DF_CC_Ngay DEFAULT 0
               CONSTRAINT CK_CC_Ngay CHECK (SoNgayCong >= 0 AND SoNgayCong <= 31),
    SoGioOT    FLOAT NOT NULL CONSTRAINT DF_CC_OT DEFAULT 0
               CONSTRAINT CK_CC_OT CHECK (SoGioOT >= 0),
    DaKhoa     BIT NOT NULL CONSTRAINT DF_CC_Khoa DEFAULT 0,
    CONSTRAINT UQ_CC_NV_Thang UNIQUE (MaNV, ThangNam)
);

/* Bảng lương: đề bài ghi PK vừa là MaLuong (Identity) vừa là (MaNV, ThangNam).
   Cách xử lý: MaLuong là PRIMARY KEY, còn (MaNV, ThangNam) là UNIQUE -> mỗi nhân viên 1 dòng lương / tháng. */
CREATE TABLE BangLuong (
    MaLuong  INT IDENTITY(1,1) CONSTRAINT PK_BangLuong PRIMARY KEY,
    MaNV     INT NOT NULL CONSTRAINT FK_BL_NV REFERENCES NhanVien(MaNV),
    ThangNam VARCHAR(7) NOT NULL,
    ThucLinh DECIMAL(18,2) NOT NULL,
    CONSTRAINT UQ_BL_NV_Thang UNIQUE (MaNV, ThangNam)
);
GO

/* ---------- Dữ liệu mẫu ---------- */
INSERT INTO PhongBan (TenPB, Mota) VALUES
 (N'Kỹ thuật',   N'Phát triển và vận hành hệ thống'),
 (N'Kinh doanh', N'Bán hàng và chăm sóc khách hàng'),
 (N'Hành chính', N'Nhân sự, kế toán, văn phòng');

INSERT INTO NhanVien (HoTen, NgaySinh, CCCD, MaPB, LuongCoBan, HeSoLuong) VALUES
 (N'Nguyễn Văn An',   '1995-03-12', '001095000001', 1, 15000000, 1.2),
 (N'Trần Thị Bình',   '1998-07-25', '001098000002', 1, 12000000, 1.0),
 (N'Lê Quốc Cường',   '1992-11-02', '001092000003', 2, 14000000, 1.3),
 (N'Phạm Thu Dung',   '1996-01-18', '001096000004', 2, 11000000, 1.0),
 (N'Hoàng Minh Đức',  '1990-09-09', '001090000005', 3, 13000000, 1.5),
 (N'Vũ Hải Yến',      '1999-05-30', '001099000006', 3, 10000000, 1.0);
GO
