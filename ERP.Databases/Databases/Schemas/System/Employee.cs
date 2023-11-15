using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Databases.Schemas
{
    [Table("Employees")]
    public class Employee : TableHaveIdInt, ITable
    {
        public Employee()
        {

        }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// <value></value>
        [StringLength(255)]
        public string Code {get; set;} = null!;
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
        public virtual User User { get; set; } = null!;
    }
}