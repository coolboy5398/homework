using System;

namespace LibraryManagement.Models
{
    /// <summary>
    /// 图书分类实体类
    /// </summary>
    public class Category
    {
        /// <summary>
        /// 分类ID
        /// </summary>
        public string CategoryID { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
    }
}
