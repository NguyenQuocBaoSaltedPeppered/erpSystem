using System;
using System.Collections.Generic;

namespace ERP.AST.Models.AssetImportAndExport.Schemas
{
    public class AssetHandOverData
    {
        /// <summary>
        /// Chuyển
        /// </summary>
        /// <value></value>
        public DateTime? TransferDate { get; set; }
        /// <summary>
        /// Chi nhánh chuyển từ
        /// </summary>
        /// <value></value>
        public int? FromBranch { get; set; }
        /// <summary>
        /// Chi nhánh chuyển đến
        /// </summary>
        /// <value></value>
        public int? ToBranch { get; set; }
        /// <summary>
        /// Mã phòng ban quản lí
        /// </summary>
        /// <value></value>
        public string? DepartmentId { get; set; }
        /// <summary>
        /// Bàn giao đến
        /// </summary>
        /// <value></value>
        public int? UserToId { get; set; }
        /// <summary>
        /// Mã người bàn giao
        /// </summary>
        /// <value></value>
        public int? UserFromId { get; set; }
        /// <summary>
        /// Trạng thái nhập
        /// 60: Đã hoàn thành
        /// </summary>
        /// <value></value>
        public string? Status { get; set; }
        /// <summary>
        /// Cấp phát cho bộ phận
        /// False: cấp phát cho cá nhân
        /// True: cấp phát cho bộ phận
        /// </summary>
        /// <value></value>
        public bool IsAllocationToDepartment { get; set; }
        /// <summary>
        /// Lý do cấp phát, thu hồi
        /// </summary>
        /// <value></value>
        public string? Reason { get; set; }
        public List<AssetHandOverDetailData> ListAssetHandOverDetailData { get; set; }
    }
}