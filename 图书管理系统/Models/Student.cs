using System;

namespace StudentGradeManagement.Models
{
    /// <summary>
    /// 学生实体类
    /// </summary>
    public class Student
    {
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentID { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        public string ClassID { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string ContactInfo { get; set; }
    }
}
