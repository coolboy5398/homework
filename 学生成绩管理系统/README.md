# 学生成绩管理系统

## 项目简介

这是一个基于 WinForm 和 SQL Server 的学生成绩管理系统，专为学校教务管理人员设计，提供完整的学生信息、教师信息、课程信息和成绩数据管理功能。系统采用管理员单一角色设计，界面简洁直观，操作便捷高效。

## 技术栈

- **开发平台**: .NET Framework 4.7.2
- **UI框架**: Windows Forms
- **数据库**: SQL Server 2016 或更高版本
- **数据访问**: ADO.NET
- **开发语言**: C# 7.3
- **开发工具**: Visual Studio 2019/2022

## 核心功能

### 1. 用户认证
- 管理员登录验证
- 会话管理
- 安全退出

### 2. 基础数据管理
- **学生管理** - 学生信息的增删改查，支持按学号/姓名搜索
- **班级管理** - 班级信息的增删改查，支持按班级名称搜索
- **教师管理** - 教师信息的增删改查，支持按工号/姓名搜索
- **课程管理** - 课程信息的增删改查，支持按课程名称搜索

### 3. 成绩管理
- **成绩录入** - 批量录入和修改学生成绩，支持所有课程
- **成绩查询** - 多维度查询和统计分析（课程、班级、学期）
- **数据导出** - 成绩数据导出为CSV格式

### 4. 数据完整性保护
- 级联删除检查（防止误删有关联数据的记录）
- 数据唯一性验证（学号、工号、课程编号等）
- 成绩范围验证（0-100分）

## 数据库设计

系统使用 6 张核心数据表：

### 数据表结构

| 表名               | 说明       | 主要字段                                                        |
| ------------------ | ---------- | --------------------------------------------------------------- |
| **Administrators** | 管理员表   | AdminID, Username, Password                                     |
| **Classes**        | 班级信息表 | ClassID, ClassName, GradeLevel                                  |
| **Students**       | 学生信息表 | StudentID, StudentName, Gender, BirthDate, ClassID, ContactInfo |
| **Teachers**       | 教师信息表 | TeacherID, TeacherName, Gender, Department, ContactInfo         |
| **Courses**        | 课程信息表 | CourseID, CourseName, Credits, TeacherID                        |
| **Grades**         | 成绩记录表 | GradeID, StudentID, CourseID, Semester, Score                   |

### 数据关系
- Students.ClassID → Classes.ClassID（学生所属班级）
- Courses.TeacherID → Teachers.TeacherID（课程授课教师）
- Grades.StudentID → Students.StudentID（成绩所属学生）
- Grades.CourseID → Courses.CourseID（成绩所属课程）

## 快速开始

### 环境要求

- Windows 7 或更高版本
- .NET Framework 4.7.2 或更高版本
- SQL Server 2016 或更高版本
- Visual Studio 2019/2022（开发环境）

### 安装步骤

#### 1. 数据库初始化

```sql
-- 方式一：使用 SSMS
1. 打开 SQL Server Management Studio (SSMS)
2. 连接到你的 SQL Server 实例
3. 打开 InitDatabase.sql 文件
4. 执行脚本（F5）

-- 方式二：使用命令行
sqlcmd -S 服务器地址 -i InitDatabase.sql
```

脚本会自动完成：
- 创建 StudentGradeDB 数据库
- 创建 6 张数据表
- 插入管理员账号
- 插入测试数据（可选）

#### 2. 配置数据库连接

编辑 `App.config` 文件，修改连接字符串：

```xml
<connectionStrings>
  <add name="StudentGradeDB" 
       connectionString="Data Source=你的服务器地址;Initial Catalog=StudentGradeDB;Integrated Security=True" 
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

**连接字符串示例：**
- 本地实例：`Data Source=.;Initial Catalog=StudentGradeDB;Integrated Security=True`
- 命名实例：`Data Source=.\SQLEXPRESS;Initial Catalog=StudentGradeDB;Integrated Security=True`
- SQL认证：`Data Source=服务器地址;Initial Catalog=StudentGradeDB;User ID=用户名;Password=密码`

#### 3. 编译和运行

```bash
# 使用 Visual Studio
1. 打开 StudentGradeManagement.sln
2. 还原 NuGet 包（如果需要）
3. 生成解决方案（Ctrl + Shift + B）
4. 启动调试（F5）

# 或使用 MSBuild 命令行
msbuild StudentGradeManagement.sln /p:Configuration=Release
```

### 默认登录账号

| 角色   | 用户名 | 密码     |
| ------ | ------ | -------- |
| 管理员 | admin  | admin123 |

> ⚠️ **安全提示**：首次登录后请及时修改默认密码

## 项目结构

```
StudentGradeManagement/
├── Forms/                      # 窗体层
│   ├── LoginForm.cs           # 登录窗体
│   ├── MainForm.cs            # 主菜单窗体
│   ├── StudentManageForm.cs   # 学生管理窗体
│   ├── ClassManageForm.cs     # 班级管理窗体
│   ├── TeacherManageForm.cs   # 教师管理窗体
│   ├── CourseManageForm.cs    # 课程管理窗体
│   ├── GradeEntryForm.cs      # 成绩录入窗体
│   └── GradeQueryForm.cs      # 成绩查询窗体
├── Utils/                      # 工具类层
│   ├── DBHelper.cs            # 数据库操作帮助类
│   └── UserSession.cs         # 用户会话管理类
├── Models/                     # 数据模型层
│   ├── Student.cs             # 学生实体类
│   ├── Teacher.cs             # 教师实体类
│   ├── Course.cs              # 课程实体类
│   ├── Class.cs               # 班级实体类
│   └── Grade.cs               # 成绩实体类
├── App.config                  # 应用程序配置文件
├── InitDatabase.sql           # 数据库初始化脚本
├── Program.cs                  # 程序入口点
└── README.md                   # 项目说明文档
```

### 架构说明

- **三层架构设计**：表示层（Forms）、业务逻辑层（Utils）、数据访问层（DBHelper）
- **ADO.NET 数据访问**：使用参数化查询防止SQL注入
- **会话管理**：UserSession 管理用户登录状态和权限

## 使用指南

### 系统登录

1. 启动程序，显示登录界面
2. 输入用户名：`admin`
3. 输入密码：`admin123`
4. 点击"登录"按钮进入主菜单

### 基础数据录入流程

**推荐按以下顺序录入数据：**

```
1. 班级管理 → 添加班级信息
   ↓
2. 教师管理 → 添加教师信息
   ↓
3. 学生管理 → 添加学生信息（选择所属班级）
   ↓
4. 课程管理 → 添加课程信息（选择授课教师）
   ↓
5. 成绩录入 → 录入学生成绩
```

### 功能操作说明

#### 学生管理
- **添加**：填写学号、姓名、性别、出生日期、班级、联系方式
- **修改**：选中学生记录，修改信息后点击"修改"
- **删除**：选中学生记录，点击"删除"（需确认无成绩记录）
- **查询**：输入学号或姓名关键字，点击"查询"

#### 成绩录入
1. 选择要录入成绩的课程
2. 输入学期（格式：2024-2025-1）
3. 点击"加载"按钮，显示所有学生列表
4. 在"成绩"列中直接输入分数（0-100）
5. 点击"保存"按钮批量保存

#### 成绩查询与统计
1. 设置筛选条件：
   - 课程：选择特定课程或"全部"
   - 班级：选择特定班级或"全部"
   - 学期：选择特定学期或"全部"
2. 点击"查询"按钮
3. 查看统计数据：
   - 平均分
   - 最高分
   - 最低分
   - 及格率
4. 点击"导出"可将数据导出为CSV文件

## 重要提示

### 数据完整性约束

| 操作     | 约束条件           | 提示信息                            |
| -------- | ------------------ | ----------------------------------- |
| 删除班级 | 该班级没有学生     | "该班级有 X 名学生，无法删除！"     |
| 删除教师 | 该教师没有授课     | "该教师有 X 门课程，无法删除！"     |
| 删除学生 | 该学生没有成绩记录 | "该学生有 X 条成绩记录，无法删除！" |
| 删除课程 | 该课程没有成绩记录 | "该课程有 X 条成绩记录，无法删除！" |

### 数据验证规则

- **学号/工号/课程编号**：不允许重复
- **成绩范围**：0-100 分
- **出生日期**：不能是未来日期，不能早于1900年
- **学分范围**：0.5-10.0
- **年级范围**：1900-2100
- **成绩唯一性**：同一学生在同一学期的同一课程只能有一条成绩记录

### 常见问题

**Q: 无法连接数据库？**
A: 检查 App.config 中的连接字符串是否正确，确认 SQL Server 服务已启动

**Q: 登录失败？**
A: 确认已执行 InitDatabase.sql 脚本，检查用户名密码是否正确

**Q: 成绩录入后看不到数据？**
A: 点击"刷新"按钮或重新加载数据

**Q: 导出的CSV文件乱码？**
A: 使用 Excel 打开时选择 UTF-8 编码

## 系统特性

### 已实现功能
- ✅ 管理员登录认证
- ✅ 学生信息管理（增删改查）
- ✅ 班级信息管理（增删改查）
- ✅ 教师信息管理（增删改查）
- ✅ 课程信息管理（增删改查）
- ✅ 批量成绩录入
- ✅ 多维度成绩查询
- ✅ 成绩统计分析
- ✅ CSV数据导出
- ✅ 数据完整性保护
- ✅ 参数化查询防SQL注入

### 技术亮点
- 三层架构设计，代码结构清晰
- ADO.NET 参数化查询，安全可靠
- DataGridView 数据绑定，操作便捷
- 级联删除检查，保护数据完整性
- 统一的异常处理和用户提示

## 开发信息

- **项目类型**：学生成绩管理系统
- **开发时间**：2024年
- **适用场景**：学校教务管理、课程成绩管理
- **技术架构**：WinForms + ADO.NET + SQL Server

## 许可证

本项目仅用于学习和教学目的，不得用于商业用途。

---

**最后更新时间**：2024年12月
