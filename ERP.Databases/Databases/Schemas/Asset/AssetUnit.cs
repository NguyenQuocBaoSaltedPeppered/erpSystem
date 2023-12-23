using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases.Schemas
{
    [Table("SYSASTU")]
    public class AssetUnit : TableHaveIdInt, ITable
    {
        public AssetUnit()
        {
            Assets = new HashSet<Asset>();
        }
        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        /// <value></value>
        public string Name {get; set;} = null!;
        /// <summary>
        /// Tên mã đơn vị tính
        /// </summary>
        /// <value></value>
        public string Code {get; set;} = null!;
        #region Default
        public DateTimeOffset CreatedAt {get; set;}
        public int CreatedBy {get; set;}
        public string CreatedIp {get; set;} = null!;
        public DateTimeOffset? UpdatedAt {get; set;}
        public int? UpdatedBy {get; set;}
        public string UpdatedIp {get; set;} = null!;
        public bool DelFlag {get; set;}
        #endregion

        #region Relation
        public int? UserId {get; set;}
        public virtual ICollection<Asset> Assets { get; set; }
        #endregion
    }
}