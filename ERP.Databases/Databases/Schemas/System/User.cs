using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Databases.Schemas
{
    [Table("Users")]
    public class User : TableHaveIdInt, ITable
    {
        /// <summary>
        /// Tên người dùng
        /// </summary>
        /// <value></value>
        [StringLength(255)]
        public string Name {get; set;} = null!;
        /// <summary>
        /// Email người dùng
        /// </summary>
        /// <value></value>
        [StringLength(255)]
        public string Email {get; set;} = null!;
        /// <summary>
        /// Mật khẩu đăng nhập
        /// </summary>
        /// <value></value>
        [StringLength(255)]
        public string Password { get; set; } = null!;
        /// <summary>
        /// Mật khẩu đã mã hoá
        /// </summary>
        /// <value></value>
        public byte[] PasswordHash {get; set;} = null!;
        /// <summary>
        /// Salt để mã hoá mật khẩu
        /// </summary>
        /// <value></value>
        public byte[] PasswordSalt {get; set;} = null!;
        /// <summary>
        /// Id nhân viên
        /// </summary>
        /// <value></value>
        public int? EmployeeId { get; set; }
        /// <summary>
        /// Default
        /// </summary>
        /// <value></value>
        public DateTimeOffset CreatedAt {get; set;}
        public int CreatedBy {get; set;}
        public string CreatedIp {get; set;} = null!;
        public DateTimeOffset? UpdatedAt {get; set;}
        public int? UpdatedBy {get; set;}
        public string UpdatedIp {get; set;} = null!;
        public bool DelFlag {get; set;}
        /// <summary>
        /// Relation
        /// </summary>
        /// <value></value>
        public virtual Employee Employee { get; set; } = null!;
    }
}