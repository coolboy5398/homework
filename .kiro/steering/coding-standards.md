# WinForms 编码规范 - 面向大学生

## 核心原则

代码要让不认真听讲的大学生也能看懂。

## 代码简洁原则

### 避免使用的写法
```csharp
// ❌ 不要用 LINQ 复杂查询
var result = students.Where(s => s.Age > 18).Select(s => s.Name).ToList();

// ✅ 用简单的 foreach
List<string> result = new List<string>();
foreach (var student in students)
{
    if (student.Age > 18)
    {
        result.Add(student.Name);
    }
}
```

```csharp
// ❌ 不要用三元运算符嵌套
string msg = score >= 90 ? "优秀" : score >= 60 ? "及格" : "不及格";

// ✅ 用 if-else
string msg;
if (score >= 90)
{
    msg = "优秀";
}
else if (score >= 60)
{
    msg = "及格";
}
else
{
    msg = "不及格";
}
```

```csharp
// ❌ 不要用 lambda 表达式
button.Click += (s, e) => { DoSomething(); };

// ✅ 用普通方法
button.Click += button_Click;

private void button_Click(object sender, EventArgs e)
{
    DoSomething();
}
```

### 推荐的写法
- 用 `if-else` 代替三元运算符
- 用 `foreach` 代替 LINQ
- 用普通方法代替 lambda
- 一行只做一件事

## 命名规范

### 变量命名
```csharp
// ✅ 好的命名 - 一看就懂
string studentName = "张三";
int studentAge = 20;
decimal mathScore = 85.5m;

// ❌ 不好的命名 - 看不懂
string sn = "张三";
int a = 20;
decimal ms = 85.5m;
```

### 控件命名
| 控件类型     | 前缀 | 示例              |
| ------------ | ---- | ----------------- |
| Button       | btn  | btnAdd, btnDelete |
| TextBox      | txt  | txtName, txtAge   |
| ComboBox     | cmb  | cmbClass          |
| DataGridView | dgv  | dgvStudents       |
| Label        | lbl  | lblTitle          |

## 注释规范

### 每个方法都要写注释
```csharp
/// <summary>
/// 加载学生数据到表格
/// </summary>
private void LoadData()
{
    // 1. 写SQL查询语句
    string sql = "SELECT * FROM Students";
    
    // 2. 执行查询，得到数据
    DataTable dt = DBHelper.ExecuteQuery(sql);
    
    // 3. 把数据显示到表格
    dgvStudents.DataSource = dt;
}
```

### 复杂逻辑要分步骤注释
```csharp
private void btnDelete_Click(object sender, EventArgs e)
{
    // 第一步：检查有没有选中数据
    if (dgvStudents.SelectedRows.Count == 0)
    {
        MessageBox.Show("请先选择要删除的学生！");
        return;
    }
    
    // 第二步：弹出确认框
    DialogResult result = MessageBox.Show("确定要删除吗？", "确认", 
        MessageBoxButtons.YesNo);
    
    if (result == DialogResult.Yes)
    {
        // 第三步：执行删除
        string id = dgvStudents.SelectedRows[0].Cells["StudentID"].Value.ToString();
        string sql = "DELETE FROM Students WHERE StudentID = @ID";
        // ...
    }
}
```

## 结构规范

### 避免深层嵌套
```csharp
// ❌ 嵌套太深，看着头疼
if (condition1)
{
    if (condition2)
    {
        if (condition3)
        {
            // 做事情
        }
    }
}

// ✅ 提前返回，逻辑清晰
if (!condition1) return;
if (!condition2) return;
if (!condition3) return;

// 做事情
```

### 方法不要太长
- 一个方法最好不超过 50 行
- 如果太长，拆成多个小方法

