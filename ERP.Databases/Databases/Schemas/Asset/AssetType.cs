using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases.Schemas
{
    [Table("SYSASTTP")]
    public class AssetType : TableHaveIdInt, ITable
    {
        public AssetType()
        {
            Assets = new HashSet<Asset>();
        }
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
        public virtual ICollection<Asset> Assets { get; set; }
        #endregion
    }
}