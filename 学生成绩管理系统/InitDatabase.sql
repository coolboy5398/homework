-- =============================================
-- 学生成绩管理系统 - 数据库初始化脚本
-- =============================================

-- 创建数据库
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'StudentGradeDB')
BEGIN
    CREATE DATABASE StudentGradeDB;
END
GO

USE StudentGradeDB;
GO

-- =============================================
-- 1. 创建班级表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Classes')
BEGIN
    CREATE TABLE Classes (
        ClassID VARCHAR(20) PRIMARY KEY,
        ClassName NVARCHAR(50) NOT NULL,
        GradeLevel INT NOT NULL,
        CONSTRAINT UQ_ClassName UNIQUE (ClassName)
    );
END
GO

-- =============================================
-- 2. 创建学生表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Students')
BEGIN
    CREATE TABLE Students (
        StudentID VARCHAR(20) PRIMARY KEY,
        StudentName NVARCHAR(50) NOT NULL,
        Gender NVARCHAR(10) NOT NULL,
        BirthDate DATE,
        ClassID VARCHAR(20),
        ContactInfo NVARCHAR(100),
        FOREIGN KEY (ClassID) REFERENCES Classes(ClassID)
    );
END
GO

-- =============================================
-- 3. 创建教师表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Teachers')
BEGIN
    CREATE TABLE Teachers (
        TeacherID VARCHAR(20) PRIMARY KEY,
        TeacherName NVARCHAR(50) NOT NULL,
        Gender NVARCHAR(10) NOT NULL,
        Department NVARCHAR(50),
        ContactInfo NVARCHAR(100)
    );
END
GO

-- =============================================
-- 4. 创建课程表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Courses')
BEGIN
    CREATE TABLE Courses (
        CourseID VARCHAR(20) PRIMARY KEY,
        CourseName NVARCHAR(100) NOT NULL,
        Credits DECIMAL(3,1) NOT NULL,
        TeacherID VARCHAR(20),
        FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID),
        CONSTRAINT UQ_CourseName UNIQUE (CourseName)
    );
END
GO

-- =============================================
-- 5. 创建成绩表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Grades')
BEGIN
    CREATE TABLE Grades (
        GradeID INT PRIMARY KEY IDENTITY(1,1),
        StudentID VARCHAR(20) NOT NULL,
        CourseID VARCHAR(20) NOT NULL,
        Semester NVARCHAR(20) NOT NULL,
        Score DECIMAL(5,2) NOT NULL,
        FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
        FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
        CONSTRAINT CHK_Score CHECK (Score >= 0 AND Score <= 100),
        CONSTRAINT UQ_Grade UNIQUE (StudentID, CourseID, Semester)
    );
END
GO

-- =============================================
-- 6. 创建管理员表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Administrators')
BEGIN
    CREATE TABLE Administrators (
        AdminID VARCHAR(20) PRIMARY KEY,
        AdminName NVARCHAR(50) NOT NULL,
        Password NVARCHAR(50) NOT NULL
    );
END
GO

-- =============================================
-- 插入初始管理员账号
-- =============================================
IF NOT EXISTS (SELECT * FROM Administrators WHERE AdminID = 'admin')
BEGIN
    INSERT INTO Administrators (AdminID, AdminName, Password)
    VALUES ('admin', '系统管理员', 'admin123');
END
GO

-- =============================================
-- 插入测试数据（可选）
-- =============================================

-- 插入测试班级
IF NOT EXISTS (SELECT * FROM Classes WHERE ClassID = 'CS2021')
BEGIN
    INSERT INTO Classes (ClassID, ClassName, GradeLevel) VALUES
    ('CS2021', '计算机2021级1班', 2021),
    ('CS2022', '计算机2022级1班', 2022),
    ('CS2023', '计算机2023级1班', 2023);
END
GO

-- 插入测试教师
IF NOT EXISTS (SELECT * FROM Teachers WHERE TeacherID = 'T001')
BEGIN
    INSERT INTO Teachers (TeacherID, TeacherName, Gender, Department, ContactInfo) VALUES
    ('T001', '张老师', '男', '计算机系', '13800138001'),
    ('T002', '李老师', '女', '数学系', '13800138002'),
    ('T003', '王老师', '男', '英语系', '13800138003');
END
GO

-- 插入测试学生
IF NOT EXISTS (SELECT * FROM Students WHERE StudentID = 'S2021001')
BEGIN
    INSERT INTO Students (StudentID, StudentName, Gender, BirthDate, ClassID, ContactInfo) VALUES
    ('S2021001', '王小明', '男', '2003-05-15', 'CS2021', '13900139001'),
    ('S2021002', '李小红', '女', '2003-08-20', 'CS2021', '13900139002'),
    ('S2022001', '张三', '男', '2004-03-10', 'CS2022', '13900139003'),
    ('S2022002', '李四', '女', '2004-06-25', 'CS2022', '13900139004');
END
GO

-- 插入测试课程
IF NOT EXISTS (SELECT * FROM Courses WHERE CourseID = 'C001')
BEGIN
    INSERT INTO Courses (CourseID, CourseName, Credits, TeacherID) VALUES
    ('C001', 'C语言程序设计', 4.0, 'T001'),
    ('C002', '高等数学', 5.0, 'T002'),
    ('C003', '大学英语', 3.0, 'T003'),
    ('C004', '数据结构', 4.0, 'T001');
END
GO

-- 插入测试成绩
IF NOT EXISTS (SELECT * FROM Grades WHERE StudentID = 'S2021001' AND CourseID = 'C001')
BEGIN
    INSERT INTO Grades (StudentID, CourseID, Semester, Score) VALUES
    ('S2021001', 'C001', '2023-2024-1', 85.5),
    ('S2021001', 'C002', '2023-2024-1', 78.0),
    ('S2021002', 'C001', '2023-2024-1', 92.0),
    ('S2021002', 'C002', '2023-2024-1', 88.5);
END
GO

PRINT '数据库初始化完成！';
PRINT '默认管理员账号: admin';
PRINT '默认管理员密码: admin123';
GO
