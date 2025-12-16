# 图书管理系统 - 需求文档

## Introduction

图书管理系统是一个基于 WinForms + SQL Server 的桌面应用程序，用于管理图书馆的日常运营。系统支持图书信息管理、读者信息管理、图书借阅归还、以及相关的查询统计功能。系统采用角色权限控制，分为管理员和普通用户两种角色。

## Glossary

- **LibrarySystem**: 图书管理系统，本文档描述的整个软件系统
- **Book**: 图书，图书馆中的藏书实体
- **Reader**: 读者，持有读者证可以借阅图书的用户
- **BorrowRecord**: 借阅记录，记录图书借阅和归还的信息
- **Category**: 图书分类，用于组织和分类图书
- **Administrator**: 管理员，拥有系统全部权限的用户角色
- **NormalUser**: 普通用户，只能查询信息的用户角色
- **BookID**: 图书编号，唯一标识一本图书
- **ReaderID**: 读者证号，唯一标识一个读者
- **BorrowDate**: 借阅日期，图书被借出的日期
- **DueDate**: 应还日期，图书应该归还的日期
- **ReturnDate**: 实还日期，图书实际归还的日期
- **Stock**: 库存数量，图书的可借数量
- **Overdue**: 逾期，图书超过应还日期未归还的状态

## Requirements

### Requirement 1: 用户登录认证

**User Story:** 作为系统用户，我希望通过用户名和密码登录系统，以便安全地访问系统功能

#### Acceptance Criteria

1. WHEN 用户输入有效的用户名和密码并点击登录按钮, THE LibrarySystem SHALL 验证用户凭据并允许访问主界面
2. IF 用户输入的用户名或密码为空, THEN THE LibrarySystem SHALL 显示"用户名和密码不能为空"的提示信息
3. IF 用户输入的用户名或密码不匹配数据库记录, THEN THE LibrarySystem SHALL 显示"用户名或密码错误"的提示信息并拒绝登录
4. WHEN 用户成功登录, THE LibrarySystem SHALL 记录用户会话信息包括用户ID、用户名和角色
5. THE LibrarySystem SHALL 在登录窗体中将密码输入框设置为密码字符显示模式

### Requirement 2: 图书信息管理

**User Story:** 作为管理员，我希望能够添加、修改、删除和查询图书信息，以便维护图书馆的藏书数据

#### Acceptance Criteria

1. WHEN 管理员输入完整的图书信息并点击添加按钮, THE LibrarySystem SHALL 验证图书编号唯一性后将图书信息保存到数据库
2. IF 管理员输入的图书编号已存在, THEN THE LibrarySystem SHALL 显示"图书编号已存在"的提示信息并拒绝添加
3. WHEN 管理员选择一条图书记录并点击修改按钮, THE LibrarySystem SHALL 更新该图书的信息到数据库
4. WHEN 管理员选择一条图书记录并点击删除按钮, THE LibrarySystem SHALL 检查该图书是否有未归还的借阅记录
5. IF 图书存在未归还的借阅记录, THEN THE LibrarySystem SHALL 显示"该图书有未归还记录，无法删除"的提示信息并拒绝删除
6. WHEN 管理员输入查询条件并点击查询按钮, THE LibrarySystem SHALL 根据图书编号、书名或作者模糊匹配并显示查询结果
7. THE LibrarySystem SHALL 在图书列表中显示图书编号、书名、作者、出版社、分类、价格和库存数量

### Requirement 3: 读者信息管理

**User Story:** 作为管理员，我希望能够添加、修改、删除和查询读者信息，以便管理图书馆的读者档案

#### Acceptance Criteria

1. WHEN 管理员输入完整的读者信息并点击添加按钮, THE LibrarySystem SHALL 验证读者证号唯一性后将读者信息保存到数据库
2. IF 管理员输入的读者证号已存在, THEN THE LibrarySystem SHALL 显示"读者证号已存在"的提示信息并拒绝添加
3. WHEN 管理员选择一条读者记录并点击修改按钮, THE LibrarySystem SHALL 更新该读者的信息到数据库
4. WHEN 管理员选择一条读者记录并点击删除按钮, THE LibrarySystem SHALL 检查该读者是否有未归还的借阅记录
5. IF 读者存在未归还的借阅记录, THEN THE LibrarySystem SHALL 显示"该读者有未归还图书，无法删除"的提示信息并拒绝删除
6. WHEN 管理员输入查询条件并点击查询按钮, THE LibrarySystem SHALL 根据读者证号或姓名模糊匹配并显示查询结果
7. THE LibrarySystem SHALL 在读者列表中显示读者证号、姓名、性别、联系方式和注册日期

### Requirement 4: 图书分类管理

**User Story:** 作为管理员，我希望能够管理图书分类，以便对图书进行有效的分类组织

#### Acceptance Criteria

1. WHEN 管理员输入分类名称并点击添加按钮, THE LibrarySystem SHALL 验证分类名称唯一性后将分类信息保存到数据库
2. IF 管理员输入的分类名称已存在, THEN THE LibrarySystem SHALL 显示"分类名称已存在"的提示信息并拒绝添加
3. WHEN 管理员选择一条分类记录并点击修改按钮, THE LibrarySystem SHALL 更新该分类的名称到数据库
4. WHEN 管理员选择一条分类记录并点击删除按钮, THE LibrarySystem SHALL 检查该分类下是否有图书
5. IF 分类下存在图书, THEN THE LibrarySystem SHALL 显示"该分类下有图书，无法删除"的提示信息并拒绝删除

### Requirement 5: 图书借阅操作

**User Story:** 作为管理员，我希望能够办理图书借阅手续，以便读者可以借阅图书

#### Acceptance Criteria

1. WHEN 管理员输入读者证号和图书编号并点击借阅按钮, THE LibrarySystem SHALL 验证读者和图书的有效性
2. IF 输入的读者证号不存在, THEN THE LibrarySystem SHALL 显示"读者不存在"的提示信息并拒绝借阅
3. IF 输入的图书编号不存在, THEN THE LibrarySystem SHALL 显示"图书不存在"的提示信息并拒绝借阅
4. IF 图书的库存数量为0, THEN THE LibrarySystem SHALL 显示"图书库存不足"的提示信息并拒绝借阅
5. WHEN 借阅条件满足, THE LibrarySystem SHALL 创建借阅记录、减少图书库存数量、设置借阅日期为当前日期、设置应还日期为借阅日期后30天
6. THE LibrarySystem SHALL 在借阅成功后显示"借阅成功"的提示信息

### Requirement 6: 图书归还操作

**User Story:** 作为管理员，我希望能够办理图书归还手续，以便读者可以归还图书

#### Acceptance Criteria

1. WHEN 管理员输入读者证号和图书编号并点击归还按钮, THE LibrarySystem SHALL 查询该读者该图书的未归还借阅记录
2. IF 未找到未归还的借阅记录, THEN THE LibrarySystem SHALL 显示"未找到借阅记录"的提示信息并拒绝归还
3. WHEN 找到未归还的借阅记录, THE LibrarySystem SHALL 更新借阅记录的实还日期为当前日期、增加图书库存数量
4. IF 实还日期晚于应还日期, THEN THE LibrarySystem SHALL 计算逾期天数并在提示信息中显示"归还成功，逾期X天"
5. IF 实还日期不晚于应还日期, THEN THE LibrarySystem SHALL 显示"归还成功"的提示信息

### Requirement 7: 借阅记录查询

**User Story:** 作为系统用户，我希望能够查询借阅记录，以便了解图书的借阅情况

#### Acceptance Criteria

1. WHEN 用户选择查询条件并点击查询按钮, THE LibrarySystem SHALL 根据读者证号、图书编号或借阅状态查询借阅记录
2. WHERE 用户选择"全部"状态, THE LibrarySystem SHALL 显示所有借阅记录
3. WHERE 用户选择"未归还"状态, THE LibrarySystem SHALL 仅显示实还日期为空的借阅记录
4. WHERE 用户选择"已归还"状态, THE LibrarySystem SHALL 仅显示实还日期不为空的借阅记录
5. THE LibrarySystem SHALL 在借阅记录列表中显示借阅ID、读者证号、读者姓名、图书编号、书名、借阅日期、应还日期、实还日期和借阅状态

### Requirement 8: 逾期图书查询

**User Story:** 作为管理员，我希望能够查询逾期未还的图书，以便及时催还

#### Acceptance Criteria

1. WHEN 管理员点击逾期查询按钮, THE LibrarySystem SHALL 查询所有实还日期为空且应还日期早于当前日期的借阅记录
2. THE LibrarySystem SHALL 在查询结果中显示读者证号、读者姓名、图书编号、书名、借阅日期、应还日期和逾期天数
3. THE LibrarySystem SHALL 按逾期天数降序排列查询结果
4. THE LibrarySystem SHALL 计算逾期天数为当前日期减去应还日期的天数

### Requirement 9: 图书借阅统计

**User Story:** 作为管理员，我希望能够查看图书借阅统计信息，以便了解图书的受欢迎程度

#### Acceptance Criteria

1. WHEN 管理员点击借阅统计按钮, THE LibrarySystem SHALL 统计每本图书的总借阅次数
2. THE LibrarySystem SHALL 在统计结果中显示图书编号、书名、作者、分类和借阅次数
3. THE LibrarySystem SHALL 按借阅次数降序排列统计结果
4. WHERE 管理员选择时间范围, THE LibrarySystem SHALL 仅统计该时间范围内的借阅记录

### Requirement 10: 系统权限控制

**User Story:** 作为系统设计者，我希望系统能够根据用户角色控制功能访问权限，以便保护系统数据安全

#### Acceptance Criteria

1. WHEN 用户登录成功, THE LibrarySystem SHALL 根据用户角色显示或隐藏相应的菜单项
2. WHILE 用户角色为Administrator, THE LibrarySystem SHALL 显示所有管理功能菜单包括图书管理、读者管理、借阅管理和统计查询
3. WHILE 用户角色为NormalUser, THE LibrarySystem SHALL 仅显示查询功能菜单包括图书查询和借阅记录查询
4. THE LibrarySystem SHALL 在主界面显示当前登录用户的姓名和角色信息
5. WHEN 用户点击退出登录菜单, THE LibrarySystem SHALL 清除会话信息并返回登录界面
