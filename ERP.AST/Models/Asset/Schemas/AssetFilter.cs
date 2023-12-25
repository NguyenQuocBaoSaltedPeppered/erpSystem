using ERP.Databases;

namespace ERP.AST.Models.Asset.Schemas
{
    public class AssetFilter : PagingFilter
    {
        public AssetFilter()
        {
        }
        public string Keyword { get; set; }
        /// <summary>
        /// Loại tài sản
        /// </summary>
        public int? TypeAsset { get; set; }
        /// <summary>
        /// ĐVT
        /// </summary>
        public int? UnitId { get; set; }
        /// <summary>
        /// Nhà cung cấp
        /// </summary>
        /// <value></value>
        public string VendorId { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        public int? BranchId { get; set; }
        /// <summary>
        /// Người chịu trách nhiệm
        /// </summary>
        /// <value></value>
        public int? UserId { get; set; }
        /// <summary>
        /// Phòng ban
        /// </summary>
        /// <value></value>
        public string DepartmentId { get; set; }
        /// <summary>
        /// Trạng thái tài sản
        /// 0: Tất cả (Default)
        /// 1: Tài sản đã cấp phát ( đang dùng )
        /// 2: Tài sản lỗi hỏng
        /// 3: Tài sản chưa cấp phát
        /// 4: Tài sản đang sửa, bảo hành
        /// 5: Hủy
        /// 6: Mất
        /// 7: Thanh lý
        /// </summary>
        /// <value></value>
        public int Status { get; set; }
        /// <summary>
        /// Mục
        /// 10: TSCĐ
        /// 20: CCDC
        /// </summary>
        /// <value></value>
        public int? Category { get; set; }
        /// <summary>
        /// Tình trạng
        /// 10: Mới
        /// 20: Cũ còn tốt
        /// 30: Lỗi
        /// 40: Hỏng
        /// 50: Bảo hành
        /// </summary>
        /// <value></value>
        public string StatusQuality { get; set; }
        /// <summary>
        /// Cách sắp xếp
        /// 0: desc - Giảm dần
        /// 1: asc - Tăng dần
        /// </summary>
        public int? OrderBy { get; set; }
        /// <summary>
        /// Trường được dùng để sắp xếp
        /// null (default): Id
        /// 1: Giá trị
        /// 2: Tổng GT
        /// 3: Hạn BH
        /// </summary>
        public int? SortedBy { get; set; }
        /// <summary>
        /// Loại kho
        /// 1: Kho vật tư tài sản
        /// 2: Các kho khác (Tài sản chờ bàn giao)
        /// </summary>
        /// <value></value>
        public int? BranchType { get; set; }
    }
}
