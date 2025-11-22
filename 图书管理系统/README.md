# 学生成绩管理系统

## 项目简介

这是一个基于 WinForm 和 SQL Server 的学生成绩管理系统，用于学校教务管理人员管理学生信息、教师信息、课程信息和成绩数据。

## 技术栈

- **开发平台**: .NET Framework 4.7.2
- **UI框架**: Windows Forms
- **数据库**: SQL Server 2016 或更高版本
- **数据访问**: ADO.NET
- **开发语言**: C#
- **开发工具**: Visual Studio 2019/2022

## 功能模块

1. **登录验证** - 管理员登录系统
2. **学生管理** - 学生信息的增删改查
3. **班级管理** - 班级信息的增删改查
4. **教师管理** - 教师信息的增删改查
5. **课程管理** - 课程信息的增删改查
6. **成绩录入** - 录入和修改学生成绩
7. **成绩查询** - 查询和统计成绩数据
8. **学生成绩查询** - 按学号查询学生成绩

## 数据库设计

系统使用 5 张核心数据表：

1. **Classes** - 班级信息表
2. **Students** - 学生信息表
3. **Teachers** - 教师信息表
4. **Courses** - 课程信息表
5. **Grades** - 成绩记录表
6. **Administrators** - 管理员表（用于登录）

## 安装和部署

### 1. 数据库初始化

1. 确保已安装 SQL Server
2. 打开 SQL Server Management Studio (SSMS)
3. 执行 `InitDatabase.sql` 脚本
4. 脚本会自动创建数据库、表结构和测试数据

### 2. 配置连接字符串

修改 `App.config` 文件中的数据库连接字符串：

```xml
<add name="StudentGradeDB" 
     connectionString="Data Source=你的服务器地址;Initial Catalog=StudentGradeDB;Integrated Security=True" 
     providerName="System.Data.SqlClient"/>
```

### 3. 编译和运行

1. 使用 Visual Studio 打开项目
2. 还原 NuGet 包（如果需要）
3. 编译项目（Ctrl + Shift + B）
4. 运行项目（F5）

## 默认账号

- **管理员账号**: admin
- **管理员密码**: admin123

## 项目结构

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
├── Models/                    # 数据模型类
│   ├── Student.cs
│   ├── Teacher.cs
│   ├── Course.cs
│   ├── Class.cs
│   └── Grade.cs
├── App.config                 # 配置文件
├── InitDatabase.sql          # 数据库初始化脚本
└── Program.cs                 # 程序入口
```

## 使用说明

### 登录系统

1. 运行程序后会显示登录界面
2. 输入管理员账号和密码
3. 点击"登录"按钮进入主菜单

### 管理基础数据

1. 先添加班级信息
2. 再添加教师信息
3. 然后添加学生信息（需要选择班级）
4. 最后添加课程信息（需要选择授课教师）

### 成绩管理

1. 选择"成绩录入"功能
2. 选择课程和学期
3. 在表格中输入或修改学生成绩
4. 点击"保存"按钮

### 成绩查询

1. 选择"成绩查询"功能
2. 设置筛选条件（课程、班级、学期）
3. 点击"查询"按钮
4. 查看统计信息（平均分、最高分、最低分、及格率）

## 注意事项

1. 删除班级前需要确保该班级没有学生
2. 删除教师前需要确保该教师没有授课
3. 删除学生前需要确保该学生没有成绩记录
4. 删除课程前需要确保该课程没有成绩记录
5. 成绩范围必须在 0-100 之间
6. 同一学生在同一学期的同一课程只能有一条成绩记录

## 开发者信息

- **项目类型**: 学生期末作业
- **开发时间**: 2024年
- **技术支持**: 请联系开发者

## 许可证

本项目仅用于学习和教学目的。
