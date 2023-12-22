using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases.Schemas
{
    [Table("SYSASTST")]
    public class AssetStock : TableHaveIdInt, IAssetQuantityColumn, ITable
    {
        public AssetStock() {
            AssetExportDetails = new HashSet<AssetExportDetail>();
            AssetHistories = new HashSet<AssetHistory>();
        }
        /// <summary>
        /// Loại kho
        /// </summary>
        /// <value></value>
        public int Type {get; set;}

        #region Default
        public int Quantity {get; set;}
        public int QuantityAllocated {get; set;}
        public int QuantityRemain {get; set;}
        public int QuantityBroken {get; set;}
        public int QuantityCancel {get; set;}
        public int QuantityGuarantee {get; set;}
        public int QuantityLost {get; set;}
        public DateTimeOffset CreatedAt {get; set;}
        public int CreatedBy {get; set;}
        public string CreatedIp {get; set;} = null!;
        public DateTimeOffset? UpdatedAt {get; set;}
        public int? UpdatedBy {get; set;}
        public string UpdatedIp {get; set;} = null!;
        public bool DelFlag {get; set;}
        #endregion

        #region Relation
        /// <summary>
        /// Mã vị trí tài sản
        /// </summary>
        /// <value></value>
        public int? BranchId { get; set; }
        /// <summary>
        /// Mã phòng ban quản lí tài sản
        /// </summary>
        /// <value></value>
        public string? DepartmentId { get; set; }
        /// <summary>
        /// Mã người quản lí
        /// </summary>
        /// <value></value>
        public int? UserId { get; set; }
        /// <summary>
        /// Id tài sản
        /// </summary>
        /// <value></value>
        public int AssetId {get; set;}
        public virtual Asset? Asset {get; set;}
        public ICollection<AssetExportDetail> AssetExportDetails {get; set;}
        public ICollection<AssetHistory> AssetHistories {get; set;}
        #endregion
    }
}