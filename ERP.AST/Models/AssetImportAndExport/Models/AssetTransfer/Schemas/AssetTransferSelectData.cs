namespace ERP.AST.Models.AssetImportAndExport.Schemas
{
    public class AssetTransferSelectData
    {
        public AssetTransferSelectData()
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
        /// Id cấp phát
        /// </summary>
        /// <value></value>
        public int? AllocationId { get; set; }
        /// <summary>
        /// Mã cấp phát (hoặc mã lần cấp phát trước nếu là danh sách thu hồi)
        /// </summary>
        /// <value></value>
        public string AllocationCode { get; set; }
        /// <summary>
        /// Id lần cấp phát trước
        /// </summary>
        /// <value></value>
        public int? AssetTransferOldId { get; set; }
        /// <summary>
        /// Mã thu hồi
        /// </summary>
        /// <value></value>
        public int? RecallId { get; set; }
        /// <summary>
        /// Mã thu hồi
        /// </summary>
        /// <value></value>
        public string RecallCode { get; set; }
        /// <summary>
        /// Ngày cấp phát/ thu hồi
        /// </summary>
        /// <value></value>
        public string Date { get; set; }
        /// <summary>
        /// Số lượng tài sản
        /// </summary>
        /// <value></value>
        public int? Quantity { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// <value></value>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// <value></value>
        public string EmployeeExportCode { get; set; }
        /// <summary>
        /// Id nhân viên
        /// </summary>
        /// <value></value>
        public int? UserId { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// <value></value>
        public string UserName { get; set; }
        /// <summary>
        /// Id người bàn giao
        /// </summary>
        /// <value></value>
        public int? UserHandOverId { get; set; }
        /// <summary>
        /// Tên người bàn giao
        /// </summary>
        /// <value></value>
        public string UserHandOverName { get; set; }
        /// <summary>
        /// Mã người bàn giao
        /// </summary>
        /// <value></value>
        public string UserHandOverCode { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        /// <value></value>
        public int? BranchId { get; set; }
        /// <summary>
        /// Tên chi nhánh
        /// </summary>
        /// <value></value>
        public string BranchName { get; set; }
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
        public DateTime? CreatedAt { get; set; }
        public List<AssetTransferDataDetail> ListAssetTransferDetail { get; set; }
    }
}