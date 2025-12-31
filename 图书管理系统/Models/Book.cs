using System;

namespace LibraryManagement.Models
{
    /// <summary>
    /// 图书实体类
    /// </summary>
    public class Book
    {
        /// <summary>
        /// 图书编号
        /// </summary>
        public string BookID { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public string CategoryID { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int Stock { get; set; }
    }
}
