namespace StudentGradeManagement.Models
{
    /// <summary>
    /// 课程实体类
    /// </summary>
    public class Course
    {
        /// <summary>
        /// 课程编号
        /// </summary>
        public string CourseID { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        public decimal Credits { get; set; }

        /// <summary>
        /// 授课教师ID
        /// </summary>
        public string TeacherID { get; set; }
    }
}
