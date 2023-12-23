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
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        /// <value></value>
        public string? Name { get; set; }
        /// <summary>
        /// Id cha của loại tài sản
        /// </summary>
        /// <value></value>
        public int? ParentId { get; set; }
        /// <summary>
        /// Mã của loại tài sản
        /// </summary>
        /// <value></value>
        public string? Code {get; set;}
        /// <summary>
        /// Level của loại tài sản
        /// </summary>
        /// <value></value>
        public int Level {get; set;}
        /// <summary>
        /// Mục của loại tài sản
        /// 10: Tài sản cố định
        /// 20: Công cụ dụng cụ
        /// 70: Tất cả
        /// </summary>
        /// <value></value>
        public int Category {get; set;}
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