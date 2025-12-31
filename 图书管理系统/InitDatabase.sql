-- ============================================
-- 图书管理系统 - 数据库初始化脚本
-- 数据库名称：LibraryDB
-- 创建日期：2025年12月
-- ============================================

-- 第一步：创建数据库
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LibraryDB')
BEGIN
    CREATE DATABASE LibraryDB;
END
GO

USE LibraryDB;
GO

-- ============================================
-- 第二步：创建表
-- ============================================

-- 1. 用户表 - 存储系统登录用户
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserID VARCHAR(20) PRIMARY KEY,          -- 用户ID
        UserName VARCHAR(50) NOT NULL,           -- 用户名（登录用）
        Password VARCHAR(50) NOT NULL,           -- 密码
        RealName NVARCHAR(50),                   -- 真实姓名
        Role VARCHAR(20) NOT NULL                -- 角色：Administrator/NormalUser
    );
END
GO

-- 2. 分类表 - 存储图书分类
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
BEGIN
    CREATE TABLE Categories (
        CategoryID VARCHAR(20) PRIMARY KEY,      -- 分类ID
        CategoryName NVARCHAR(50) NOT NULL UNIQUE -- 分类名称（唯一）
    );
END
GO

-- 3. 图书表 - 存储图书信息
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Books')
BEGIN
    CREATE TABLE Books (
        BookID VARCHAR(20) PRIMARY KEY,          -- 图书编号
        BookName NVARCHAR(100) NOT NULL,         -- 书名
        Author NVARCHAR(50),                     -- 作者
        Publisher NVARCHAR(100),                 -- 出版社
        CategoryID VARCHAR(20),                  -- 分类ID（外键）
        Price DECIMAL(10,2),                     -- 价格
        Stock INT DEFAULT 0,                     -- 库存数量
        FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
    );
END
GO

-- 4. 读者表 - 存储读者信息
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Readers')
BEGIN
    CREATE TABLE Readers (
        ReaderID VARCHAR(20) PRIMARY KEY,        -- 读者证号
        ReaderName NVARCHAR(50) NOT NULL,        -- 姓名
        Gender NVARCHAR(10),                     -- 性别
        Phone VARCHAR(20),                       -- 联系方式
        RegisterDate DATE DEFAULT GETDATE()      -- 注册日期
    );
END
GO

-- 5. 借阅记录表 - 存储借阅和归还信息
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'BorrowRecords')
BEGIN
    CREATE TABLE BorrowRecords (
        BorrowID INT IDENTITY(1,1) PRIMARY KEY,  -- 借阅ID（自增）
        ReaderID VARCHAR(20) NOT NULL,           -- 读者证号（外键）
        BookID VARCHAR(20) NOT NULL,             -- 图书编号（外键）
        BorrowDate DATE NOT NULL,                -- 借阅日期
        DueDate DATE NOT NULL,                   -- 应还日期
        ReturnDate DATE NULL,                    -- 实还日期（NULL表示未归还）
        FOREIGN KEY (ReaderID) REFERENCES Readers(ReaderID),
        FOREIGN KEY (BookID) REFERENCES Books(BookID)
    );
END
GO

-- ============================================
-- 第三步：插入初始数据
-- ============================================

-- 1. 插入用户数据
IF NOT EXISTS (SELECT * FROM Users WHERE UserID = 'admin')
BEGIN
    INSERT INTO Users (UserID, UserName, Password, RealName, Role) 
    VALUES ('admin', 'admin', '123456', '系统管理员', 'Administrator');
END

IF NOT EXISTS (SELECT * FROM Users WHERE UserID = 'user')
BEGIN
    INSERT INTO Users (UserID, UserName, Password, RealName, Role) 
    VALUES ('user', 'user', '123456', '普通用户', 'NormalUser');
END
GO

-- 2. 插入分类数据
IF NOT EXISTS (SELECT * FROM Categories WHERE CategoryID = 'C001')
BEGIN
    INSERT INTO Categories (CategoryID, CategoryName) VALUES ('C001', '文学');
    INSERT INTO Categories (CategoryID, CategoryName) VALUES ('C002', '科技');
    INSERT INTO Categories (CategoryID, CategoryName) VALUES ('C003', '历史');
    INSERT INTO Categories (CategoryID, CategoryName) VALUES ('C004', '艺术');
    INSERT INTO Categories (CategoryID, CategoryName) VALUES ('C005', '教育');
END
GO

-- 3. 插入图书数据
IF NOT EXISTS (SELECT * FROM Books WHERE BookID = 'B001')
BEGIN
    INSERT INTO Books (BookID, BookName, Author, Publisher, CategoryID, Price, Stock) 
    VALUES ('B001', '红楼梦', '曹雪芹', '人民文学出版社', 'C001', 59.00, 5);
    
    INSERT INTO Books (BookID, BookName, Author, Publisher, CategoryID, Price, Stock) 
    VALUES ('B002', '三国演义', '罗贯中', '人民文学出版社', 'C001', 49.00, 3);
    
    INSERT INTO Books (BookID, BookName, Author, Publisher, CategoryID, Price, Stock) 
    VALUES ('B003', '水浒传', '施耐庵', '人民文学出版社', 'C001', 45.00, 4);
    
    INSERT INTO Books (BookID, BookName, Author, Publisher, CategoryID, Price, Stock) 
    VALUES ('B004', '西游记', '吴承恩', '人民文学出版社', 'C001', 55.00, 6);
    
    INSERT INTO Books (BookID, BookName, Author, Publisher, CategoryID, Price, Stock) 
    VALUES ('B005', 'C#程序设计', '张三', '清华大学出版社', 'C002', 68.00, 10);
    
    INSERT INTO Books (BookID, BookName, Author, Publisher, CategoryID, Price, Stock) 
    VALUES ('B006', '数据库原理', '李四', '机械工业出版社', 'C002', 55.00, 8);
    
    INSERT INTO Books (BookID, BookName, Author, Publisher, CategoryID, Price, Stock) 
    VALUES ('B007', '中国通史', '王五', '中华书局', 'C003', 128.00, 3);
    
    INSERT INTO Books (BookID, BookName, Author, Publisher, CategoryID, Price, Stock) 
    VALUES ('B008', '艺术概论', '赵六', '高等教育出版社', 'C004', 42.00, 5);
END
GO

-- 4. 插入读者数据
IF NOT EXISTS (SELECT * FROM Readers WHERE ReaderID = 'R001')
BEGIN
    INSERT INTO Readers (ReaderID, ReaderName, Gender, Phone, RegisterDate) 
    VALUES ('R001', '张伟', '男', '13800138001', '2025-01-15');
    
    INSERT INTO Readers (ReaderID, ReaderName, Gender, Phone, RegisterDate) 
    VALUES ('R002', '李娜', '女', '13800138002', '2025-02-20');
    
    INSERT INTO Readers (ReaderID, ReaderName, Gender, Phone, RegisterDate) 
    VALUES ('R003', '王强', '男', '13800138003', '2025-03-10');
    
    INSERT INTO Readers (ReaderID, ReaderName, Gender, Phone, RegisterDate) 
    VALUES ('R004', '刘芳', '女', '13800138004', '2025-04-05');
    
    INSERT INTO Readers (ReaderID, ReaderName, Gender, Phone, RegisterDate) 
    VALUES ('R005', '陈明', '男', '13800138005', '2025-05-18');
END
GO

-- 5. 插入借阅记录数据（用于测试）
IF NOT EXISTS (SELECT * FROM BorrowRecords WHERE ReaderID = 'R001' AND BookID = 'B001')
BEGIN
    -- 已归还的记录
    INSERT INTO BorrowRecords (ReaderID, BookID, BorrowDate, DueDate, ReturnDate) 
    VALUES ('R001', 'B001', '2025-10-01', '2025-10-31', '2025-10-25');
    
    INSERT INTO BorrowRecords (ReaderID, BookID, BorrowDate, DueDate, ReturnDate) 
    VALUES ('R002', 'B005', '2025-10-15', '2025-11-14', '2025-11-10');
    
    -- 未归还的记录
    INSERT INTO BorrowRecords (ReaderID, BookID, BorrowDate, DueDate, ReturnDate) 
    VALUES ('R003', 'B002', '2025-12-01', '2025-12-31', NULL);
    
    INSERT INTO BorrowRecords (ReaderID, BookID, BorrowDate, DueDate, ReturnDate) 
    VALUES ('R004', 'B006', '2025-12-10', '2025-01-09', NULL);
    
    -- 逾期未还的记录（用于测试逾期查询）
    INSERT INTO BorrowRecords (ReaderID, BookID, BorrowDate, DueDate, ReturnDate) 
    VALUES ('R005', 'B003', '2025-11-01', '2025-12-01', NULL);
END
GO

PRINT '数据库初始化完成！';
PRINT '管理员账号：admin / 123456';
PRINT '普通用户：user / 123456';
GO
