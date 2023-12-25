using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace ERP.AST.Models.Asset.Schemas
{
    public class AssetUserDetailFilter
    {
        /// <summary>
        /// Id Tài sản
        /// </summary>
        /// <value></value>
        public int? AssetId {get; set;}
        /// <summary>
        /// Id người sử dụng
        /// </summary>
        /// <value></value>
        public int UserId {get; set;}
    }
}
