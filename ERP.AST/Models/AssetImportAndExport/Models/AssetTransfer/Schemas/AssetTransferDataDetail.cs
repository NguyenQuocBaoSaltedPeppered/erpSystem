using System;
using System.Collections.Generic;

namespace ERP.AST.Models.AssetImportAndExport.Schemas
{
    public class AssetTransferDataDetail
    {
        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// <value></value>
        public int? AssetId { get; set; }
        /// <summary>
        /// Tên TS
        /// </summary>
        /// <value></value>
        public string AssetName { get; set; }
        /// <summary>
        /// Mã TS
        /// </summary>
        /// <value></value>
        public string AssetCode { get; set; }
        /// <summary>
        /// Số lượng nhập
        /// </summary>
        /// <value></value>
        public int? Quantity { get; set; }
        /// <summary>
        /// Số lượng tài sản đã cấp phát (Còn lại)
        /// </summary>
        /// <value></value>
        public int? QuantityAllocated { get; set; }
        /// <summary>
        /// Số lượng tài sản còn lại
        /// </summary>
        /// <value></value>
        public int? QuantityRemain { get; set; }
        /// <summary>
        /// Mã kho tài sản nếu là cấp phát
        /// </summary>
        /// <value></value>
        public int? AssetStockId { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// <value></value>
        public string Note { get; set; }
        /// <summary>
        /// Thông tin về vị trí lưu trữ
        /// </summary>
        /// <value></value>
        public BranchInfo branch {get; set;}
        /// <summary>
        /// Thông tin về bộ phận
        /// </summary>
        /// <value></value>
        public DepartmentInfo department {get; set;}
    }
    public class BranchInfo {
        /// <summary>
        /// Id Vị trí lưu trữ
        /// </summary>
        /// <value></value>
        public int? BranchId {get; set;}
        /// <summary>
        /// Tên vị trí lưu trữ
        /// </summary>
        /// <value></value>
        public string? BranchName {get; set;}
    }
    public class DepartmentInfo
    {
        /// <summary>
        /// Id bộ phận
        /// </summary>
        /// <value></value>
        public string? DepartmentId {get; set;}
        /// <summary>
        /// Tên bộ phận
        /// </summary>
        /// <value></value>
        public string? DepartmentName {get; set;}
    }
}