using System;
using System.Collections.Generic;

namespace ERP.AST.Models.AssetImportAndExport.Schemas
{
    public class AssetHandOverDetail
    {
        public AssetHandOverDetail()
        {

        }
        /// <summary>
        /// Id chuyển
        /// </summary>
        /// <value></value>
        public int Id { get; set; }
        /// <summary>
        /// Mã chuyển
        /// </summary>
        /// <value></value>
        public string Code { get; set; }
        /// <summary>
        /// Id lần cấp phát trước
        /// </summary>
        /// <value></value>
        public int? AssetTransferOldId { get; set; }
        /// <summary>
        /// Ngày bàn giao
        /// </summary>
        /// <value></value>
        public string HandOverDate { get; set; }
        /// <summary>
        /// Ngày tạo phiếu
        /// </summary>
        /// <value></value>
        public DateTimeOffset CreatedDate { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// <value></value>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Id nhân viên nhận
        /// </summary>
        /// <value></value>
        public int? ToUserId { get; set; }
        /// <summary>
        /// Id nhân viên bàn giao
        /// </summary>
        /// <value></value>
        public int? FromUserId { get; set; }
        /// <summary>
        /// Tên nhân viên nhận
        /// </summary>
        /// <value></value>
        public string ToUserName { get; set; }
        /// <summary>
        /// Tên nhân viên bàn giao
        /// </summary>
        /// <value></value>
        public string FromUserName { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        /// <value></value>
        public int? ToBranchId { get; set; }
        /// <summary>
        /// Tên chi nhánh
        /// </summary>
        /// <value></value>
        public string ToBranchName { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        /// <value></value>
        public int? FromBranchId { get; set; }
        /// <summary>
        /// Tên chi nhánh
        /// </summary>
        /// <value></value>
        public string FromBranchName { get; set; }
        /// <summary>
        /// Bộ phận
        /// </summary>
        /// <value></value>
        public string DepartmentId { get; set; }
        /// <summary>
        /// Tên bộ phận
        /// </summary>
        /// <value></value>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Id nhân viên
        /// </summary>
        /// <value></value>
        public int? UserExportId { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// <value></value>
        public string UserExportName { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        /// <value></value>
        public int? BranchExportId { get; set; }
        /// <summary>
        /// Tên chi nhánh
        /// </summary>
        /// <value></value>
        public string BranchExportName { get; set; }
        /// <summary>
        /// Bộ phận
        /// </summary>
        /// <value></value>
        public string DepartmentExportId { get; set; }
        /// <summary>
        /// Tên bộ phận
        /// </summary>
        /// <value></value>
        public string DepartmentExportName { get; set; }
        /// <summary>
        /// Lý do
        /// </summary>
        /// <value></value>
        public string Reason { get; set; }
        /// <summary>
        /// Cấp phát cho bộ phận
        /// False: cấp phát cho cá nhân
        /// True: cấp phát cho bộ phận
        /// </summary>
        /// <value></value>
        public bool? IsAllocationToDepartment { get; set; }
        public List<AssetTransferDataDetail> ListAssetHandOverDetail { get; set; }
    }
}