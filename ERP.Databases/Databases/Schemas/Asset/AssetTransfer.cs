using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases.Schemas
{
    [Table("SYSASTTF")]
    public class AssetTransfer : TableHaveIdInt, ITable
    {
        /// <summary>
        /// Id xuất kho
        /// </summary>
        /// <value></value>
        public int? AssetExportId { get; set; }
        /// <summary>
        /// Id nhập kho
        /// </summary>
        /// <value></value>
        public int? AssetImportId { get; set; }
        /// <summary>
        /// Mã Code
        /// </summary>
        /// <value></value>
        public string? Code { get; set; }
        /// <summary>
        /// Lý do
        /// </summary>
        /// <value></value>
        public string? Reason { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// <value></value>
        public string? Note { get; set; }
        /// <summary>
        /// Chi nhánh chuyển từ
        /// </summary>
        /// <value></value>
        public int? FromBranch { get; set; }
        /// <summary>
        /// Chi nhánh chuyển đến
        /// </summary>
        /// <value></value>
        public int? ToBranch { get; set; }
        /// <summary>
        /// Chuyển từ người
        /// </summary>
        /// <value></value>
        public int? FromUser { get; set; }
        /// <summary>
        /// Chuyển đến người
        /// </summary>
        /// <value></value>
        public int? ToUser { get; set; }
        /// <summary>
        /// Id cấp phát nếu là phiếu thu hồi
        /// </summary>
        /// <value></value>
        public int? AssetTranferOldId { get; set; }
        /// <summary>
        /// Loại chuyển
        /// 30: Thu hồi
        /// 40: Cấp phát
        /// </summary>
        /// <value></value>
        public int? Type { get; set; }
        /// <summary>
        /// Cấp phát cho bộ phận
        /// False: cấp phát cho cá nhân
        /// True: cấp phát cho bộ phận
        /// </summary>
        /// <value></value>
        public bool? IsAllocationToDepartment { get; set; }

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
        public virtual AssetExport? AssetExport {get; set;}
        public virtual AssetImport? AssetImport {get; set;}
        #endregion
    }
}