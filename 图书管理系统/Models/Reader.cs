using System;

namespace LibraryManagement.Models
{
    /// <summary>
    /// 读者实体类
    /// </summary>
    public class Reader
    {
        /// <summary>
        /// 读者证号
        /// </summary>
        public string ReaderID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string ReaderName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 注册日期
        /// </summary>
        public DateTime RegisterDate { get; set; }
    }
}
