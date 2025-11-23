namespace StudentGradeManagement.Models
{
    /// <summary>
    /// 教师实体类
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// 教师工号
        /// </summary>
        public string TeacherID { get; set; }

        /// <summary>
        /// 教师姓名
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 所属系部
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string ContactInfo { get; set; }
    }
}
