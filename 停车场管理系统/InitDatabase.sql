-- ============================================
-- 停车场管理系统 - 数据库初始化脚本
-- 数据库名称：ParkingDB
-- ============================================

-- 第一步：创建数据库
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ParkingDB')
BEGIN
    CREATE DATABASE ParkingDB;
END
GO

USE ParkingDB;
GO

-- ============================================
-- 第二步：创建表
-- ============================================

-- 1. 用户表 - 存储管理员信息
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserID VARCHAR(20) PRIMARY KEY,
        UserName NVARCHAR(50) NOT NULL,
        Password VARCHAR(50) NOT NULL
    );
END
GO

-- 2. 车位表 - 存储车位信息
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ParkingSpaces')
BEGIN
    CREATE TABLE ParkingSpaces (
        SpaceID VARCHAR(20) PRIMARY KEY,       -- 车位编号
        SpaceName NVARCHAR(50) NOT NULL,       -- 车位名称（如A01、B02）
        SpaceType NVARCHAR(20) NOT NULL,       -- 车位类型（小型车位/大型车位）
        Status NVARCHAR(20) DEFAULT '空闲'      -- 状态：空闲/占用
    );
END
GO

-- 3. 收费规则表 - 存储收费标准
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PriceRules')
BEGIN
    CREATE TABLE PriceRules (
        RuleID INT PRIMARY KEY IDENTITY(1,1),
        VehicleType NVARCHAR(20) NOT NULL,     -- 车辆类型
        HourlyRate DECIMAL(10,2) NOT NULL,     -- 每小时费用
        DailyMax DECIMAL(10,2) NOT NULL        -- 每日封顶
    );
END
GO

-- 4. 停车记录表 - 存储进出场记录
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ParkingRecords')
BEGIN
    CREATE TABLE ParkingRecords (
        RecordID INT PRIMARY KEY IDENTITY(1,1),
        PlateNumber VARCHAR(20) NOT NULL,      -- 车牌号
        SpaceID VARCHAR(20) NOT NULL,          -- 车位编号
        EntryTime DATETIME NOT NULL,           -- 进场时间
        ExitTime DATETIME NULL,                -- 出场时间（NULL表示未出场）
        Fee DECIMAL(10,2) NULL,                -- 停车费用
        FOREIGN KEY (SpaceID) REFERENCES ParkingSpaces(SpaceID)
    );
END
GO

-- ============================================
-- 第三步：插入初始数据
-- ============================================

-- 1. 插入管理员账号
IF NOT EXISTS (SELECT * FROM Users WHERE UserID = 'admin')
BEGIN
    INSERT INTO Users (UserID, UserName, Password) 
    VALUES ('admin', '管理员', '123456');
END
GO

-- 2. 插入收费规则
IF NOT EXISTS (SELECT * FROM PriceRules WHERE VehicleType = '小型车')
BEGIN
    INSERT INTO PriceRules (VehicleType, HourlyRate, DailyMax) VALUES ('小型车', 5.00, 50.00);
    INSERT INTO PriceRules (VehicleType, HourlyRate, DailyMax) VALUES ('大型车', 10.00, 80.00);
END
GO

-- 3. 插入车位数据（A区10个小型车位，B区5个大型车位）
IF NOT EXISTS (SELECT * FROM ParkingSpaces WHERE SpaceID = 'A01')
BEGIN
    -- A区小型车位
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('A01', 'A区01号', '小型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('A02', 'A区02号', '小型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('A03', 'A区03号', '小型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('A04', 'A区04号', '小型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('A05', 'A区05号', '小型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('A06', 'A区06号', '小型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('A07', 'A区07号', '小型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('A08', 'A区08号', '小型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('A09', 'A区09号', '小型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('A10', 'A区10号', '小型车位', '空闲');
    
    -- B区大型车位
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('B01', 'B区01号', '大型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('B02', 'B区02号', '大型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('B03', 'B区03号', '大型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('B04', 'B区04号', '大型车位', '空闲');
    INSERT INTO ParkingSpaces (SpaceID, SpaceName, SpaceType, Status) VALUES ('B05', 'B区05号', '大型车位', '空闲');
END
GO

PRINT '数据库初始化完成！';
PRINT '管理员账号：admin';
PRINT '密码：123456';
GO
