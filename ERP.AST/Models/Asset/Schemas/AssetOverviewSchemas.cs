using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace ERP.AST.Models.Asset.Schemas
{
    public class AssetOverview
    {
        /// <summary>
        /// Số lượng tài sản
        /// </summary>
        /// <value></value>
        public int QuantityAsset { get; set; }
        /// <summary>
        /// Số lượng tài sản chưa cấp phát
        /// </summary>
        /// <value></value>
        public int QuantityAssetUnallocated { get; set; }
        /// <summary>
        /// Số lượng tài sản đã cấp phát ( Đang sử dụng )
        /// </summary>
        /// <value></value>
        public int QuantityAssetAllocated { get; set; }
        /// <summary>
        /// Số lượng tài sản hỏng
        /// </summary>
        /// <value></value>
        public int QuantityBroken { get; set; }
        /// <summary>
        /// Số lượng tài sản sữa chữa, bảo hành
        /// </summary>
        /// <value></value>
        public int QuantityGuarantee { get; set;}
        /// <summary>
        /// Số lượng tài sản mất
        /// </summary>
        /// <value></value>
        public int QuantityLost { get; set; }
        /// <summary>
        /// Số lượng tài sản hủy
        /// </summary>
        /// <value></value>
        public int QuantityCancel { get; set;}
    }
    public class AssetValueOverview
    {
        /// <summary>
        /// Tổng giá trị mua
        /// </summary>
        /// <value></value>
        public double TotalValue {get; set;}
        /// <summary>
        /// Tổng giá trị hao mòn
        /// </summary>
        /// <value></value>
        public double TotalDepreciated {get; set;}
        /// <summary>
        /// Tổng giá trị còn lại
        /// </summary>
        /// <value></value>
        public double TotalRemain {get; set;}
        /// <summary>
        /// Tổng số lượng tài sản
        /// </summary>
        /// <value></value>
        public int TotalQuantity {get; set;}
    }
}
