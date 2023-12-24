using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace ERP.AST.Models.AssetImportAndExport.Schemas
{
    public class AssetExportDetailData
    {
        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// <value></value>
        public int? AssetId { get; set; }
        /// <summary>
        /// Mã kho tài sản
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
        public string Note { get; set; }
        /// <summary>
        /// Mã xuất kho ( mã phiếu báo hỏng )
        /// </summary>
        /// <value></value>
        public int? AssetExportId { get; set; }
        /// <summary>
        /// Mã cấp phát
        /// </summary>
        public int? AssetAllocationId { get; set; }
    }
}