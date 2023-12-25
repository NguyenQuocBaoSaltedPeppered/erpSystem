using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace ERP.AST.Models.AssetImportAndExport.Schemas
{
    public class AssetImportDetailData
    {
        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// <value></value>
        public int? AssetId { get; set; }
        /// <summary>
        /// Số lượng nhập
        /// </summary>
        /// <value></value>
        public int? Quantity { get; set; }
        /// <summary>
        /// Mã cấp phát ( Được lưu ở phiếu chuyển kho )
        /// </summary>
        /// <value></value>
        public int? AssetTransferId { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// <value></value>
        public string? Note { get; set; }
    }
}