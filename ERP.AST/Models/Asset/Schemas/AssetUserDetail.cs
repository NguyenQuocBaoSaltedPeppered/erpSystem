using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace ERP.AST.Models.Asset.Schemas
{
    public class AssetUserDetail
    {
        public AssetUserDetail()
        {
        }
        public int? StockId {get; set;}
        /// <summary>
        /// Số lượng
        /// </summary>
        /// <value></value>
        public int? StockQuantity {set; get;}
        /// <summary>
        /// Id vị trí lưu trữ
        /// </summary>
        /// <value></value>
        public int? UserBranchId {set; get;}
        /// <summary>
        /// Tên vị trí lưu trữ
        /// </summary>
        /// <value></value>
        public string UserBranchName {set; get;}
        /// <summary>
        /// Id người sử dụng
        /// </summary>
        /// <value></value>
        public int? AssetUserId {set; get;}
        /// <summary>
        /// Mã người sử dụng
        /// </summary>
        /// <value></value>
        public string UserCode {set; get;}
        /// <summary>
        /// Tên người sử dụng
        /// </summary>
        /// <value></value>
        public string UserName {set; get;}
        /// <summary>
        /// Id Bộ phận sử dụng
        /// </summary>
        /// <value></value>
        public string UserDepartmentId {set; get;}
        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        /// <value></value>
        public string UserDepartmentName {set; get;}
    }
}
