using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace ERP.AST.Models.Asset.Schemas
{
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
