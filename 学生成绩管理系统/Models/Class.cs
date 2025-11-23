namespace StudentGradeManagement.Models
{
    /// <summary>
    /// 班级实体类
    /// </summary>
    public class Class
    {
        /// <summary>
        /// 班级编号
        /// </summary>
        public string ClassID { get; set; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 年级
        /// </summary>
        public int GradeLevel { get; set; }
    }
}
