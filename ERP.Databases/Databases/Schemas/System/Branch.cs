using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Databases.Schemas
{
    [Table("SYSBR")]
    public class Branch : TableHaveIdInt, ITable
    {
        public Branch()
        {
            Departments = new HashSet<Department>();
            Users = new HashSet<User>();
            Employees = new HashSet<Employee>();
        }
        /// <summary>
        /// Tên chi nhánh
        /// </summary>
        /// <value></value>
        [StringLength(255)]
        public string Name {get; set;} = null!;
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
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}