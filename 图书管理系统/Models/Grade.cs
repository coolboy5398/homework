namespace StudentGradeManagement.Models
{
    /// <summary>
    /// 成绩实体类
    /// </summary>
    public class Grade
    {
        /// <summary>
        /// 成绩ID（自增）
        /// </summary>
        public int GradeID { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// 课程编号
        /// </summary>
        public string CourseID { get; set; }

        /// <summary>
        /// 学期
        /// </summary>
        public string Semester { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public decimal Score { get; set; }
    }
}
