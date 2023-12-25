using ERP.Databases;
namespace ERP.AST.Models.AssetImportAndExport.Schemas
{
    public class FilterTransfer : PagingFilter
    {
        public FilterTransfer()
        {
            IsAllocationToDepartment = false;
        }
        /// <summary>
        /// Từ khoá tìm kiếm
        /// </summary>
        /// <value></value>
        public string Keyword { get; set; }
        /// <summary>
        /// Mã code search cấp phát hoặc thu hồi
        /// </summary>
        /// <value></value>
        public string Code { get; set; }
        public int? AssetId { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        /// <value></value>
        public int? BranchId { get; set; }
        /// <summary>
        /// Bộ phận
        /// </summary>
        /// <value></value>
        public string DepartmentId { get; set; }
        /// <summary>
        /// Người nhận
        /// </summary>
        /// <value></value>
        public int? UserId { get; set; }
        /// <summary>
        /// Người bàn giao
        /// </summary>
        /// <value></value>
        public int? UserHandOverId { get; set; }
        /// <summary>
        /// Từ ngày
        /// </summary>
        /// <value></value>
        public string DateSearch { get; set; }
        /// <summary>
        /// Mã thu hồi
        /// </summary>
        /// <value></value>
        public string RecallCode { get; set; }
        /// <summary>
        /// Cấp phát cho bộ phận
        /// False: cấp phát cho cá nhân
        /// True: cấp phát cho bộ phận
        /// </summary>
        /// <value></value>
        public bool? IsAllocationToDepartment { get; set; }
        /// <summary>
        /// Loại tài sản
        /// </summary>
        /// <value></value>
        public int? TypeAsset { get; set; }
        /// <summary>
        /// Nhóm tài sản
        /// </summary>
        /// <value></value>
        public int? GroupAsset { get; set; }
        /// <summary>
        /// Nhà cung cấp
        /// </summary>
        /// <value></value>
        public int? VendorId {get; set;}
        /// <summary>
        /// Tình trạng
        /// </summary>
        /// <value></value>
        public string StatusQuality { get; set; }
    }
}