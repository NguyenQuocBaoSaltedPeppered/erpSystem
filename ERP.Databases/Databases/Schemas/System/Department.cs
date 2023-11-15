using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Databases.Schemas
{
    [Table("SYSDPM")]
    public class Department : ITable
    {
        public Department()
        {
            Positions = new HashSet<Position>();
            Users = new HashSet<User>();
        }
        /// <summary>
        /// Id bộ phận
        /// </summary>
        /// <value></value>
        [Key]
        [StringLength(50)]
        public string Id { get; set; } = null!;
        /// <summary>
        /// Tên bộ phận
        /// </summary>
        /// <value></value>
        [StringLength(255)]
        public string Name {get; set;} = null!;
        /// <summary>
        /// Id chi nhánh
        /// </summary>
        /// <value></value>
        public int? BranchId { get; set; }
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
        public virtual Branch? Branch { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}