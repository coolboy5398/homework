# Implementation Plan

- [x] 1. 创建项目和数据库基础结构
  - 使用 Visual Studio 创建 WinForm 项目 (StudentGradeManagement)
  - 配置项目属性，设置 .NET Framework 版本为 4.7.2
  - 创建 SQL Server 数据库 (StudentGradeDB)
  - 编写并执行数据库初始化脚本，创建 5 张表 (Classes, Students, Teachers, Courses, Grades)
  - 插入初始管理员账号数据
  - 配置 App.config 文件，添加数据库连接字符串
  - _Requirements: 8.1, 8.2_

- [x] 2. 实现数据库连接和工具类
  - 创建 Utils 文件夹
  - 实现 DBHelper 类，提供 GetConnection、ExecuteQuery、ExecuteNonQuery、ExecuteScalar 方法
  - 实现 UserSession 类，管理当前登录用户信息 (UserId, UserName, UserRole, IsLoggedIn)
  - 添加 Login 和 Logout 方法到 UserSession
  - 测试数据库连接是否正常
  - _Requirements: 8.1, 8.2, 8.4_

- [x] 3. 创建数据模型类
  - 创建 Models 文件夹
  - 实现 Student 类，包含 StudentID, StudentName, Gender, BirthDate, ClassID, ContactInfo 属性
  - 实现 Teacher 类，包含 TeacherID, TeacherName, Gender, Department, ContactInfo 属性
  - 实现 Course 类，包含 CourseID, CourseName, Credits, TeacherID 属性
  - 实现 Class 类，包含 ClassID, ClassName, GradeLevel 属性
  - 实现 Grade 类，包含 GradeID, StudentID, CourseID, Semester, Score 属性
  - _Requirements: 1.1, 2.1, 3.1, 4.1, 7.1_

- [x] 4. 实现登录窗体
  - 创建 Forms 文件夹
  - 设计 LoginForm 界面，添加用户名、密码文本框和登录按钮
  - 实现登录验证逻辑，查询 Administrators、Teachers、Students 表
  - 验证成功后调用 UserSession.Login 设置用户信息
  - 根据用户角色跳转到主菜单窗体
  - 显示友好的错误提示信息
  - _Requirements: 8.1, 8.2, 8.3, 8.4_

- [x] 5. 实现主菜单窗体
  - 设计 MainForm 界面，使用 MenuStrip 或按钮组
  - 根据 UserSession.UserRole 动态显示菜单项
  - 管理员菜单：学生管理、班级管理、教师管理、课程管理、成绩查询、退出
  - 教师菜单：成绩录入、成绩查询、退出
  - 学生菜单：我的成绩、退出
  - 实现退出功能，调用 UserSession.Logout 并返回登录窗体
  - _Requirements: 8.4, 8.5_

- [x] 6. 实现班级管理窗体
  - 设计 ClassManageForm 界面，包含 DataGridView 和操作按钮
  - 实现加载班级列表功能，显示 ClassID, ClassName, GradeLevel
  - 实现添加班级功能，验证必填字段和唯一性
  - 实现修改班级功能，允许修改 ClassName 和 GradeLevel
  - 实现删除班级功能，检查是否有学生关联
  - 实现查询班级功能，支持按班级名称模糊查询
  - 添加输入验证和错误处理
  - _Requirements: 2.1, 2.2, 2.3, 2.4, 2.5_

- [x] 7. 实现学生管理窗体
  - 设计 StudentManageForm 界面，包含 DataGridView 和输入控件
  - 实现加载学生列表功能，显示学生信息和所属班级名称
  - 实现添加学生功能，包含 StudentID, StudentName, Gender, BirthDate, ClassID, ContactInfo
  - 添加 ComboBox 选择班级，从 Classes 表加载数据
  - 实现修改学生功能，不允许修改 StudentID
  - 实现删除学生功能，检查是否有成绩记录
  - 实现查询学生功能，支持按学号或姓名查询
  - 添加输入验证（必填字段、日期格式、学号唯一性）
  - _Requirements: 1.1, 1.2, 1.3, 1.4, 1.5_

- [x] 8. 实现教师管理窗体
  - 设计 TeacherManageForm 界面，包含 DataGridView 和输入控件
  - 实现加载教师列表功能，显示教师信息
  - 实现添加教师功能，包含 TeacherID, TeacherName, Gender, Department, ContactInfo
  - 实现修改教师功能，不允许修改 TeacherID
  - 实现删除教师功能，检查是否有课程关联
  - 实现查询教师功能，支持按工号或姓名查询
  - 添加输入验证（必填字段、工号唯一性）
  - _Requirements: 7.1, 7.2, 7.3, 7.4, 7.5_

- [x] 9. 实现课程管理窗体
  - 设计 CourseManageForm 界面，包含 DataGridView 和输入控件
  - 实现加载课程列表功能，显示课程信息和授课教师姓名
  - 实现添加课程功能，包含 CourseID, CourseName, Credits, TeacherID
  - 添加 ComboBox 选择授课教师，从 Teachers 表加载数据
  - 实现修改课程功能，允许修改课程名称、学分和授课教师
  - 实现删除课程功能，检查是否有成绩记录
  - 实现查询课程功能，支持按课程名称查询
  - 添加输入验证（必填字段、学分范围、课程名唯一性）
  - _Requirements: 3.1, 3.2, 3.3, 3.4, 3.5_

- [x] 10. 实现成绩录入窗体
  - 设计 GradeEntryForm 界面，包含筛选条件和 DataGridView
  - 添加 ComboBox 选择课程，只显示当前教师授课的课程
  - 添加 ComboBox 或 TextBox 输入学期（如 "2023-2024-1"）
  - 根据选择的课程和学期加载学生列表和已有成绩
  - 在 DataGridView 中显示 StudentID, StudentName, Score 列
  - 允许在 DataGridView 中直接编辑 Score 列
  - 实现保存成绩功能，验证成绩范围 (0-100)
  - 使用 INSERT 或 UPDATE 语句保存成绩，处理重复记录
  - 显示保存成功的记录数量
  - _Requirements: 4.1, 4.2, 4.3, 4.4, 4.5_

- [x] 11. 实现成绩查询统计窗体
  - 设计 GradeQueryForm 界面，包含筛选条件、DataGridView 和统计信息显示区
  - 添加 ComboBox 筛选课程、班级、学期
  - 实现查询功能，显示学生姓名、课程名称、学期、成绩
  - 计算并显示平均分、最高分、最低分
  - 计算并显示及格率（成绩 >= 60 的百分比）
  - 实现导出功能，将查询结果保存为文本文件或 CSV 格式
  - 添加清空筛选条件功能
  - _Requirements: 5.1, 5.2, 5.3, 5.4, 5.5_

- [x] 12. 实现学生成绩查询窗体
  - 设计 StudentGradeForm 界面，包含学期筛选和 DataGridView
  - 根据 UserSession.UserId 查询当前学生的成绩
  - 显示课程名称、学期、成绩、学分
  - 添加 ComboBox 筛选学期
  - 计算并显示总学分和 GPA（平均绩点）
  - GPA 计算公式：(成绩/10 - 5) * 学分 的加权平均
  - 显示友好的界面，突出显示不及格科目（成绩 < 60）
  - _Requirements: 6.1, 6.2, 6.3, 6.4, 6.5_

- [x] 13. 完善错误处理和输入验证
  - 在所有数据库操作中添加 try-catch 块，捕获 SqlException
  - 统一错误提示格式，使用 MessageBox 显示友好的错误信息
  - 在所有添加和修改操作中验证必填字段
  - 验证数据格式（日期、数字、成绩范围）
  - 验证唯一性约束（学号、工号、课程名）
  - 在删除操作前检查外键关联
  - 添加操作确认对话框（删除操作）
  - _Requirements: All requirements_

- [x] 14. 优化用户界面和用户体验
  - 统一所有窗体的布局和样式
  - 设置合适的窗体大小和位置（居中显示）
  - 优化 DataGridView 列宽和显示格式
  - 添加数据加载提示（Loading...）
  - 实现双击 DataGridView 行快速编辑功能
  - 添加快捷键支持（Enter 提交，Esc 取消）
  - 优化按钮布局和命名
  - 添加图标和美化界面（可选）
  - _Requirements: All requirements_

- [ ]* 15. 编写测试数据和测试用例
  - 准备测试数据 SQL 脚本，包含班级、学生、教师、课程、成绩数据
  - 测试登录功能（管理员、教师、学生、错误密码）
  - 测试学生管理的增删改查功能
  - 测试班级、教师、课程管理功能
  - 测试成绩录入功能（正常录入、重复录入、超范围成绩）
  - 测试成绩查询和统计功能
  - 测试学生成绩查询功能
  - 测试权限控制（不同角色的菜单显示）
  - 测试删除操作的外键约束
  - 记录测试结果和发现的问题
  - _Requirements: All requirements_

- [ ]* 16. 编写项目文档
  - 编写系统使用说明文档
  - 编写数据库设计文档
  - 编写部署说明文档
  - 准备演示 PPT（可选）
  - 整理项目源代码和数据库脚本
  - _Requirements: All requirements_
