using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace ERP.AST.Models.AssetImportAndExport.Schemas
{
    public class AssetImportData
    {
        public AssetImportData()
        {
            ImportDate = DateTime.UtcNow;
        }
        /// <summary>
        /// Id kho
        /// </summary>
        /// <value></value>
        public int? StockId {get; set;}
        /// <summary>
        /// Mã chuyển tài sản
        /// </summary>
        /// <value></value>
        public string Code { get; set; }
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
        public List<AssetImportDetailData> ListAssetImportDetails { get; set; }
    }
}