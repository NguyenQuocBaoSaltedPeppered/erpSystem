using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases.Schemas
{
    [Table("SYSASTI")]
    public class AssetImport : TableHaveIdInt, ITable
    {
        public AssetImport() {
            AssetImportDetails = new HashSet<AssetImportDetail>();
        }
        /// <summary>
        /// Mã chuyển tài sản
        /// </summary>
        /// <value></value>
        public string Code { get; set; } = null!;
        /// <summary>
        /// Ngày nhập
        /// </summary>
        /// <value></value>
        public DateTime? ImportDate { get; set; }
        /// <summary>
        /// Loại nhập
        /// 10: Báo tăng
        /// 30: Nhập thu hồi
        /// 40: Nhập cấp phát từ kho tổng
        /// </summary>
        /// <value></value>
        public int? Type { get; set; }
        /// <summary>
        /// Trạng thái nhập
        /// 60: Đã hoàn thành
        /// </summary>
        /// <value></value>
        public string? Status { get; set; }
        /// <summary>
        /// Số lượng xuất
        /// </summary>
        /// <value></value>
        public int? Quantity { get; set; }
        /// <summary>
        /// Số tiền xuất
        /// </summary>
        /// <value></value>
        public double? TotalMoney { get; set; }
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
        /// Chi nhánh nhập
        /// </summary>
        /// <value></value>
        public int? BranchId { get; set; }
        /// <summary>
        /// Mã phòng ban quản lí
        /// </summary>
        /// <value></value>
        public string? DepartmentId { get; set; }
        /// <summary>
        /// Mã người quản lí
        /// </summary>
        /// <value></value>
        public int? UserId { get; set; }
        public virtual ICollection<AssetImportDetail> AssetImportDetails {get; set;}
        public virtual AssetTransfer? AssetTransfer {get; set;}
        #endregion
    }
}