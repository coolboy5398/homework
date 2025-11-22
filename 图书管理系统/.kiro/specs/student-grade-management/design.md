# Design Document

## Overview

学生成绩管理系统是一个基于 WinForm 的桌面应用程序，使用 SQL Server 作为数据库。系统采用简单的两层架构（表示层 + 数据访问层），不进行复杂的分层设计，适合作为学生期末作业项目。

### 技术栈

- **开发平台**: .NET Framework 4.7.2 或更高版本
- **UI框架**: Windows Forms
- **数据库**: SQL Server 2016 或更高版本
- **数据访问**: ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter)
- **开发语言**: C#
- **开发工具**: Visual Studio 2019/2022

### 系统特点

- 简单直观的 WinForm 界面
- 直接使用 ADO.NET 访问数据库，不使用 ORM
- 不进行分层架构，代码组织简单清晰
- 适合学生学习和期末作业展示

## Architecture

### 系统架构

```
┌─────────────────────────────────────────┐
│         Presentation Layer              │
│         (WinForm Forms)                 │
│  ┌──────────┐  ┌──────────┐  ┌────────┐│
│  │ Login    │  │ Main     │  │ Student││
│  │ Form     │  │ Menu     │  │ Forms  ││
│  └──────────┘  └──────────┘  └────────┘│
│  ┌──────────┐  ┌──────────┐  ┌────────┐│
│  │ Teacher  │  │ Course   │  │ Grade  ││
│  │ Forms    │  │ Forms    │  │ Forms  ││
│  └──────────┘  └──────────┘  └────────┘│
└─────────────────────────────────────────┘
                    │
                    │ ADO.NET
                    ▼
┌─────────────────────────────────────────┐
│         Database Layer                  │
│         (SQL Server)                    │
│  ┌──────────┐  ┌──────────┐  ┌────────┐│
│  │ Students │  │ Teachers │  │ Classes││
│  └──────────┘  └──────────┘  └────────┘│
│  ┌──────────┐  ┌──────────┐            │
│  │ Courses  │  │ Grades   │            │
│  └──────────┘  └──────────┘            │
└─────────────────────────────────────────┘
```

### 项目结构

```
StudentGradeManagement/
├── Forms/                      # 所有窗体
│   ├── LoginForm.cs           # 登录窗体
│   ├── MainForm.cs            # 主菜单窗体
│   ├── StudentManageForm.cs  # 学生管理窗体
│   ├── ClassManageForm.cs    # 班级管理窗体
│   ├── TeacherManageForm.cs  # 教师管理窗体
│   ├── CourseManageForm.cs   # 课程管理窗体
│   ├── GradeEntryForm.cs     # 成绩录入窗体
│   ├── GradeQueryForm.cs     # 成绩查询窗体
│   └── StudentGradeForm.cs   # 学生成绩查询窗体
├── Utils/                     # 工具类
│   ├── DBHelper.cs           # 数据库连接帮助类
│   └── UserSession.cs        # 用户会话管理类
├── Models/                    # 数据模型类（可选）
│   ├── Student.cs
│   ├── Teacher.cs
│   ├── Course.cs
│   ├── Class.cs
│   └── Grade.cs
├── App.config                 # 配置文件（数据库连接字符串）
└── Program.cs                 # 程序入口
```

## Components and Interfaces

### 1. 数据库连接组件 (DBHelper)

**职责**: 管理数据库连接，提供统一的数据库操作方法

**核心方法**:
```csharp
public class DBHelper
{
    private static string connectionString;
    
    // 获取数据库连接
    public static SqlConnection GetConnection()
    
    // 执行查询，返回 DataTable
    public static DataTable ExecuteQuery(string sql, SqlParameter[] parameters = null)
    
    // 执行增删改，返回受影响行数
    public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
    
    // 执行查询，返回单个值
    public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
}
```

### 2. 用户会话管理 (UserSession)

**职责**: 管理当前登录用户的信息和权限

**属性**:
```csharp
public static class UserSession
{
    public static string UserId { get; set; }
    public static string UserName { get; set; }
    public static string UserRole { get; set; }  // "Admin", "Teacher", "Student"
    public static bool IsLoggedIn { get; set; }
    
    public static void Login(string userId, string userName, string role)
    public static void Logout()
}
```

### 3. 窗体组件

#### LoginForm (登录窗体)
- **输入**: 用户名、密码
- **功能**: 验证用户身份，设置用户会话
- **输出**: 跳转到主菜单或显示错误信息

#### MainForm (主菜单窗体)
- **功能**: 根据用户角色显示不同的菜单选项
- **管理员菜单**: 学生管理、班级管理、教师管理、课程管理、成绩查询
- **教师菜单**: 成绩录入、成绩查询
- **学生菜单**: 我的成绩

#### StudentManageForm (学生管理窗体)
- **功能**: 学生信息的增删改查
- **控件**: DataGridView (显示学生列表)、TextBox (输入学生信息)、Button (操作按钮)
- **操作**: 添加、修改、删除、查询学生

#### ClassManageForm (班级管理窗体)
- **功能**: 班级信息的增删改查
- **控件**: DataGridView、TextBox、Button
- **操作**: 添加、修改、删除、查询班级

#### TeacherManageForm (教师管理窗体)
- **功能**: 教师信息的增删改查
- **控件**: DataGridView、TextBox、Button
- **操作**: 添加、修改、删除、查询教师

#### CourseManageForm (课程管理窗体)
- **功能**: 课程信息的增删改查
- **控件**: DataGridView、TextBox、ComboBox (选择教师)、Button
- **操作**: 添加、修改、删除、查询课程

#### GradeEntryForm (成绩录入窗体)
- **功能**: 教师录入和修改学生成绩
- **控件**: ComboBox (选择课程、学期)、DataGridView (显示学生和成绩)、Button
- **操作**: 批量录入成绩、保存成绩

#### GradeQueryForm (成绩查询窗体)
- **功能**: 教师查询和统计成绩
- **控件**: ComboBox (筛选条件)、DataGridView (显示成绩)、Label (统计信息)、Button
- **操作**: 查询成绩、显示统计数据、导出报表

#### StudentGradeForm (学生成绩查询窗体)
- **功能**: 学生查询自己的成绩
- **控件**: ComboBox (学期筛选)、DataGridView (显示成绩)、Label (GPA显示)
- **操作**: 查询成绩、查看统计

## Data Models

### 数据库表结构

#### 1. Classes (班级表)
```sql
CREATE TABLE Classes (
    ClassID VARCHAR(20) PRIMARY KEY,
    ClassName NVARCHAR(50) NOT NULL,
    GradeLevel INT NOT NULL,  -- 年级（如 2021、2022）
    CONSTRAINT UQ_ClassName UNIQUE (ClassName)
)
```

#### 2. Students (学生表)
```sql
CREATE TABLE Students (
    StudentID VARCHAR(20) PRIMARY KEY,
    StudentName NVARCHAR(50) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    BirthDate DATE,
    ClassID VARCHAR(20),
    ContactInfo NVARCHAR(100),
    Password NVARCHAR(50) DEFAULT '123456',  -- 默认密码
    FOREIGN KEY (ClassID) REFERENCES Classes(ClassID)
)
```

#### 3. Teachers (教师表)
```sql
CREATE TABLE Teachers (
    TeacherID VARCHAR(20) PRIMARY KEY,
    TeacherName NVARCHAR(50) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    Department NVARCHAR(50),
    ContactInfo NVARCHAR(100),
    Password NVARCHAR(50) DEFAULT '123456'  -- 默认密码
)
```

#### 4. Courses (课程表)
```sql
CREATE TABLE Courses (
    CourseID VARCHAR(20) PRIMARY KEY,
    CourseName NVARCHAR(100) NOT NULL,
    Credits DECIMAL(3,1) NOT NULL,
    TeacherID VARCHAR(20),
    FOREIGN KEY (TeacherID) REFERENCES Teachers(TeacherID),
    CONSTRAINT UQ_CourseName UNIQUE (CourseName)
)
```

#### 5. Grades (成绩表)
```sql
CREATE TABLE Grades (
    GradeID INT PRIMARY KEY IDENTITY(1,1),
    StudentID VARCHAR(20) NOT NULL,
    CourseID VARCHAR(20) NOT NULL,
    Semester NVARCHAR(20) NOT NULL,  -- 学期（如 "2023-2024-1"）
    Score DECIMAL(5,2) NOT NULL,
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
    CONSTRAINT CHK_Score CHECK (Score >= 0 AND Score <= 100),
    CONSTRAINT UQ_Grade UNIQUE (StudentID, CourseID, Semester)
)
```

#### 6. Administrators (管理员表 - 可选)
```sql
CREATE TABLE Administrators (
    AdminID VARCHAR(20) PRIMARY KEY,
    AdminName NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
)
```

### 数据模型类（C# 实体类）

```csharp
public class Student
{
    public string StudentID { get; set; }
    public string StudentName { get; set; }
    public string Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string ClassID { get; set; }
    public string ContactInfo { get; set; }
}

public class Teacher
{
    public string TeacherID { get; set; }
    public string TeacherName { get; set; }
    public string Gender { get; set; }
    public string Department { get; set; }
    public string ContactInfo { get; set; }
}

public class Course
{
    public string CourseID { get; set; }
    public string CourseName { get; set; }
    public decimal Credits { get; set; }
    public string TeacherID { get; set; }
}

public class Class
{
    public string ClassID { get; set; }
    public string ClassName { get; set; }
    public int GradeLevel { get; set; }
}

public class Grade
{
    public int GradeID { get; set; }
    public string StudentID { get; set; }
    public string CourseID { get; set; }
    public string Semester { get; set; }
    public decimal Score { get; set; }
}
```

## Error Handling

### 数据库错误处理

```csharp
try
{
    // 数据库操作
}
catch (SqlException ex)
{
    // 处理 SQL 异常
    MessageBox.Show($"数据库错误: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
}
catch (Exception ex)
{
    // 处理其他异常
    MessageBox.Show($"系统错误: {ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
}
```

### 输入验证

- **必填字段验证**: 检查文本框是否为空
- **格式验证**: 验证学号、工号格式，日期格式，成绩范围
- **唯一性验证**: 检查学号、工号、课程名是否重复
- **外键验证**: 删除前检查是否有关联数据

### 常见错误场景

1. **登录失败**: 用户名或密码错误
2. **重复添加**: 学号、工号、课程名已存在
3. **删除失败**: 存在关联数据（如学生有成绩记录）
4. **数据格式错误**: 成绩不在 0-100 范围内
5. **数据库连接失败**: 连接字符串错误或数据库服务未启动

## Testing Strategy

### 功能测试

#### 1. 登录功能测试
- 测试管理员登录
- 测试教师登录
- 测试学生登录
- 测试错误密码
- 测试不存在的用户

#### 2. 学生管理测试
- 添加学生（正常数据）
- 添加学生（重复学号）
- 修改学生信息
- 删除学生（无成绩记录）
- 删除学生（有成绩记录，应失败）
- 查询学生

#### 3. 成绩管理测试
- 教师录入成绩
- 教师修改成绩
- 成绩范围验证（0-100）
- 重复成绩记录验证
- 成绩查询和统计

#### 4. 权限测试
- 管理员访问所有功能
- 教师只能访问成绩相关功能
- 学生只能查看自己的成绩

### 数据库测试

#### 1. 约束测试
- 主键约束
- 外键约束
- 唯一性约束
- 检查约束（成绩范围）

#### 2. 数据完整性测试
- 级联删除测试
- 数据一致性测试

### 界面测试

- 窗体正常显示
- 控件布局合理
- 数据正确绑定到 DataGridView
- 按钮响应正常
- 错误提示友好

### 测试数据准备

```sql
-- 插入测试班级
INSERT INTO Classes VALUES ('CS2021', '计算机2021级1班', 2021);
INSERT INTO Classes VALUES ('CS2022', '计算机2022级1班', 2022);

-- 插入测试教师
INSERT INTO Teachers VALUES ('T001', '张老师', '男', '计算机系', '13800138001', '123456');
INSERT INTO Teachers VALUES ('T002', '李老师', '女', '数学系', '13800138002', '123456');

-- 插入测试学生
INSERT INTO Students VALUES ('S2021001', '王小明', '男', '2003-05-15', 'CS2021', '13900139001', '123456');
INSERT INTO Students VALUES ('S2021002', '李小红', '女', '2003-08-20', 'CS2021', '13900139002', '123456');

-- 插入测试课程
INSERT INTO Courses VALUES ('C001', 'C语言程序设计', 4.0, 'T001');
INSERT INTO Courses VALUES ('C002', '高等数学', 5.0, 'T002');

-- 插入测试成绩
INSERT INTO Grades (StudentID, CourseID, Semester, Score) 
VALUES ('S2021001', 'C001', '2023-2024-1', 85.5);

-- 插入管理员
INSERT INTO Administrators VALUES ('admin', '系统管理员', 'admin123');
```

## UI Design Guidelines

### 窗体设计原则

1. **一致性**: 所有窗体使用统一的布局和样式
2. **简洁性**: 界面简洁明了，不要过多装饰
3. **易用性**: 操作流程清晰，按钮位置合理

### 标准窗体布局

```
┌─────────────────────────────────────────┐
│  窗体标题                          [_][□][×]│
├─────────────────────────────────────────┤
│  查询条件区域                            │
│  [关键字: ________] [查询] [重置]        │
├─────────────────────────────────────────┤
│  数据显示区域 (DataGridView)             │
│  ┌───────────────────────────────────┐  │
│  │ ID  │ 姓名  │ 性别  │ ...        │  │
│  ├───────────────────────────────────┤  │
│  │     │       │       │            │  │
│  └───────────────────────────────────┘  │
├─────────────────────────────────────────┤
│  操作按钮区域                            │
│  [添加] [修改] [删除] [刷新] [关闭]      │
└─────────────────────────────────────────┘
```

### 控件命名规范

- 窗体: `xxxForm` (如 `StudentManageForm`)
- 按钮: `btn` + 功能 (如 `btnAdd`, `btnDelete`)
- 文本框: `txt` + 字段名 (如 `txtStudentID`, `txtStudentName`)
- 标签: `lbl` + 字段名 (如 `lblStudentID`)
- 下拉框: `cmb` + 字段名 (如 `cmbClass`)
- 数据表格: `dgv` + 数据类型 (如 `dgvStudents`)

### 颜色方案

- 主色调: 蓝色系 (#007ACC)
- 背景色: 白色或浅灰色
- 按钮: 标准 Windows 按钮样式
- 错误提示: 红色
- 成功提示: 绿色

## Configuration

### App.config 配置

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="StudentGradeDB" 
         connectionString="Data Source=localhost;Initial Catalog=StudentGradeDB;Integrated Security=True" 
         providerName="System.Data.SqlClient"/>
  </connectionStrings>
  <appSettings>
    <add key="DefaultPassword" value="123456"/>
    <add key="PageSize" value="20"/>
  </appSettings>
</configuration>
```

### 数据库初始化脚本

创建一个 `InitDatabase.sql` 文件，包含：
1. 创建数据库
2. 创建所有表
3. 插入初始管理员账号
4. 插入测试数据（可选）

## Security Considerations

### 密码安全

- 初始密码: 统一为 "123456"
- 建议: 首次登录后提示修改密码（可选功能）
- 存储: 明文存储（简化处理，实际项目应加密）

### SQL 注入防护

- 使用参数化查询 (SqlParameter)
- 不要拼接 SQL 字符串

```csharp
// 正确做法
string sql = "SELECT * FROM Students WHERE StudentID = @StudentID";
SqlParameter[] parameters = {
    new SqlParameter("@StudentID", studentId)
};
DataTable dt = DBHelper.ExecuteQuery(sql, parameters);

// 错误做法（容易 SQL 注入）
string sql = $"SELECT * FROM Students WHERE StudentID = '{studentId}'";
```

### 权限控制

- 登录验证: 检查用户名和密码
- 菜单控制: 根据用户角色显示不同菜单
- 数据访问控制: 学生只能查看自己的成绩，教师只能管理自己的课程成绩

## Deployment

### 部署步骤

1. **安装 SQL Server**: 确保目标机器安装了 SQL Server
2. **创建数据库**: 运行 `InitDatabase.sql` 脚本
3. **配置连接字符串**: 修改 `App.config` 中的数据库连接字符串
4. **发布应用程序**: 使用 Visual Studio 发布功能生成可执行文件
5. **安装 .NET Framework**: 确保目标机器安装了对应版本的 .NET Framework

### 打包说明

- 使用 Visual Studio 的发布功能
- 选择 "文件夹" 发布方式
- 包含所有必需的 DLL 文件
- 提供数据库初始化脚本和使用说明文档
