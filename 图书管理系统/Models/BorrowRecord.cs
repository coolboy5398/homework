using System;

namespace LibraryManagement.Models
{
    /// <summary>
    /// 借阅记录实体类
    /// </summary>
    public class BorrowRecord
    {
        /// <summary>
        /// 借阅ID
        /// </summary>
        public int BorrowID { get; set; }

        /// <summary>
        /// 读者证号
        /// </summary>
        public string ReaderID { get; set; }

        /// <summary>
        /// 图书编号
        /// </summary>
        public string BookID { get; set; }

        /// <summary>
        /// 借阅日期
        /// </summary>
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// 应还日期
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// 实还日期（NULL表示未归还）
        /// </summary>
        public DateTime? ReturnDate { get; set; }
    }
}
