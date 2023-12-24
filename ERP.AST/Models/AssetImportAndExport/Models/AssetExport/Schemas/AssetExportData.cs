using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace ERP.AST.Models.AssetImportAndExport.Schemas
{
    public class AssetExportData
    {
        public AssetExportData()
        {
            ExportDate = DateTime.UtcNow;
            IsGuarantee = false;
        }
        /// <summary>
        /// Mã chuyển tài sản
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        public string Code { get; set; }
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
        /// 60: Báo mất
        /// 80: Báo thanh lý
        /// </summary>
        /// <value></value>
        public int? Type { get; set; }
        /// <summary>
        /// Mã vị trí tài sản
        /// </summary>
        /// <value></value>
        public int? BranchId { get; set; }
        /// <summary>
        /// Mã phòng ban quản lí
        /// </summary>
        /// <value></value>
        public string DepartmentId { get; set; }
        /// <summary>
        /// Mã người quản lí
        /// </summary>
        /// <value></value>
        public int? UserId { get; set; }
        /// <summary>
        /// Trạng thái xuất
        /// 10: Nháp (Báo hỏng)
        /// 60: Đã hoàn thành
        /// </summary>
        /// <value></value>
        public string Status { get; set; }
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
        public string Reason { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// <value></value>
        public string Note { get; set; }
        /// <summary>
        /// Trạng thái tài sản
        /// 1. Tài sản đã cấp phát
        /// 2. Từ phiếu báo hổng
        /// 3. Tài sản chưa cấp phát
        /// </summary>
        public int? AssetStatus { get; set; }
        /// <summary>
        /// Người chịu trách nhiệm ( báo mất )
        /// </summary>
        /// <value></value>
        public int? ResponsibleId { get; set; }
        /// <summary>
        /// Bảo hành, sữa chữa
        /// FALSE: Không bảo hành, sữa chữa
        /// TRUE: Có bảo hành, sữa chữa
        /// </summary>
        public bool? IsGuarantee { get; set; }
        /// <summary>
        /// Relation
        /// </summary>
        /// <value></value>
        public List<AssetExportDetailData> ListAssetExportDetails { get; set; }
    }
}