using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases
{
    /// <summary>
    /// Bảng có khoá chính là kiểu int và tự động tăng
    /// </summary>
    public class TableHaveIdInt
    {
        /// <summary>
        /// Id định danh cho tất cả các bảng (khoá chính)
        /// </summary>
        /// <value></value>
        [Key]
        [Column(Order = 1)]
        public int Id {get; set;}
    }
    /// <summary>
    /// Tập hợp dữ liệu chung cho tất cả các bảng
    /// </summary>
    public interface ITable
    {
        /// <summary>
        /// Ngày tạo data (changes)
        /// </summary>
        /// <value></value>
        public DateTimeOffset CreatedAt {get; set;}
        /// <summary>
        /// Id user tạo data (changes)
        /// </summary>
        /// <value></value>
        public int CreatedBy {get; set;}
        /// <summary>
        /// Ip máy tạo data (changes)
        /// </summary>
        /// <value></value>
        public string CreatedIp {get; set;}
        /// <summary>
        /// Ngày cập nhật data (changes)
        /// </summary>
        /// <value></value>
        public DateTimeOffset? UpdatedAt {get; set;}
        /// <summary>
        /// Id user cập nhật data (changes)
        /// </summary>
        /// <value></value>
        public int? UpdatedBy {get; set;}
        /// <summary>
        /// Ip máy cập nhật data (changes)
        /// </summary>
        /// <value></value>
        public string UpdatedIp {get; set;}
        /// <summary>
        /// Cờ xoá dữ liệu
        /// <para>False: Chưa xoá</para>
        /// <para>True: Đã xoá</para>
        /// </summary>
        public bool DelFlag {get; set;}
    }
}