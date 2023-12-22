using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases.Schemas
{
    [Table("SYSASTE")]
    public class AssetExport : TableHaveIdInt, ITable
    {
        public AssetExport() {
            AssetExportDetails = new HashSet<AssetExportDetail>();
        }
        /// <summary>
        /// Mã chuyển tài sản
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        public string Code { get; set; } = null!;
        /// <summary>
        /// Ngày xuất
        /// </summary>
        /// <value></value>
        public DateTime? ExportDate { get; set; }
        /// <summary>
        /// Loại xuất
        /// 10: Báo hỏng
        /// 20: Bảo hành, sữa chữa
        /// 30: Xuất thu hồi
        /// 40: Xuất cấp phát nếu từ kho tổng
        /// 50: Báo hủy
        /// </summary>
        /// <value></value>
        public int? Type { get; set; }
        /// <summary>
        /// Trạng thái xuất
        /// 10: Nháp (Báo hỏng)
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
        /// Giá trị đền bù ( Khi báo hỏng/ báo hủy )
        /// </summary>
        /// <value></value>
        public double? CompensationValue { get; set; }
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
        /// Mã vị trí tài sản
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
        /// <summary>
        /// Người chịu trách nhiệm ( báo mất )
        /// </summary>
        /// <value></value>
        public int? ResponsibleId { get; set; }

        public ICollection<AssetExportDetail> AssetExportDetails {get; set;}
        public virtual AssetTransfer AssetTransfer {get; set;}
        #endregion
    }
}