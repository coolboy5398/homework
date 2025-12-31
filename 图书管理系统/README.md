# 图书管理系统

一个简单的图书馆管理软件，用 WinForms + SQL Server 开发。

## 功能介绍

- 登录系统（管理员/普通用户）
- 图书管理（添加、修改、删除、查询图书）
- 读者管理（添加、修改、删除、查询读者）
- 分类管理（管理图书分类）
- 借书还书（办理借阅和归还手续）
- 借阅查询（查看借阅记录、逾期图书）
- 借阅统计（统计图书借阅次数）

## 怎么运行这个项目？

### 第一步：创建数据库

1. 打开 SQL Server Management Studio
2. 新建查询，把 `InitDatabase.sql` 里的内容复制进去
3. 点击"执行"按钮，等它跑完

### 第二步：打开项目

1. 双击 `LibraryManagement.sln` 用 Visual Studio 打开
2. 按 `Ctrl + Shift + B` 编译项目
3. 按 `F5` 运行

### 第三步：登录系统

管理员账号：`admin`，密码：`123456`
普通用户：`user`，密码：`123456`

## 项目结构

```
图书管理系统/
├── Forms/          # 窗体文件（界面）
├── Models/         # 实体类（数据模型）
├── Utils/          # 工具类
│   ├── DBHelper.cs      # 数据库操作
│   └── UserSession.cs   # 用户登录信息
├── App.config      # 配置文件（数据库连接）
└── InitDatabase.sql # 数据库脚本
```

## 常见问题

### 连接不上数据库？

打开 `App.config`，修改连接字符串：

```xml
<add name="LibraryDB" 
     connectionString="Data Source=你的服务器地址;Initial Catalog=LibraryDB;Integrated Security=True" />
```

把 `你的服务器地址` 改成你自己的，比如 `localhost` 或 `.`

### 编译报错？

检查一下是不是 .NET Framework 4.7.2，没有的话去微软官网下载安装。
