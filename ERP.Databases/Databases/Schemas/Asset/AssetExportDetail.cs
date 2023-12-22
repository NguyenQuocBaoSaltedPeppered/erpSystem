using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases.Schemas
{
    [Table("SYSASTEDT")]
    public class AssetExportDetail : TableHaveIdInt, ITable
    {
        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// <value></value>
        public int? AssetStockId { get; set; }
        /// <summary>
        /// Số lượng xuất
        /// </summary>
        /// <value></value>
        public int? Quantity { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// <value></value>
        public string? Note { get; set; }

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
        /// <summary>
        /// Mã xuất kho
        /// </summary>
        /// <value></value>
        public int? AssetExportId { get; set; }
        public virtual AssetExport? AssetExport {get; set;}
        public virtual AssetStock? AssetStock {get; set;}
        #endregion
    }
}