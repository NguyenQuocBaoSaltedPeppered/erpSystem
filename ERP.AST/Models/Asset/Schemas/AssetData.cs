using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace ERP.AST.Models.Asset.Schemas
{
    public class AssetData
	{
		public AssetData()
		{
        }
        public int? AssetId { get; set; }
        /// <summary>
        /// Mã kho
        /// </summary>
        /// <value></value>

        public int? SYSASTSTId { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// <value></value>
        [StringLength(500)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Tên mục (dể xuất excel)
        /// 10: Tài sản
        /// 20: Công cụ
        /// </summary>
        /// <value></value>
        public string CategoryName {get; set;}
        /// <summary>
        /// Mã chứng từ
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        public string FinancialCode { get; set; }
        /// <summary>
        /// Loại tài sản
        /// </summary>
        /// <value></value>
        [Required]
        public int TypeId { get; set; }
        /// <summary>
        /// Đơn vị tính
        /// </summary>
        [Required]
        public int? UnitId { get; set; }
        public string TypeName { get; set; }
        public string TypeCode { get; set; }
        public string UnitName { get; set; }
        /// <summary>
        /// Số lượng tài sản
        /// </summary>
        /// <value></value>
        public int Quantity { get; set; }
        public int? QuantityAllocated { get; set; }
        public int? QuantityRecovered { get; set; }
        public int? QuantityRemain { get; set; }
        /// <summary>
        /// Nguyên giá
        /// </summary>
        /// <value></value>
        public double OriginalPrice { get; set; }
        /// <summary>
        /// Tổng giá trị
        /// </summary>
        /// <value></value>
        public double? Total { get; set; }
        /// <summary>
        /// Mã vị trí tài sản
        /// </summary>
        /// <value></value>
        public int? BranchId { get; set; }
        /// <summary>
        /// Mã vị trí tài sản
        /// </summary>
        /// <value></value>
        public string BranchName { get; set; }
        /// <summary>
        /// Mã phòng ban quản lí
        /// </summary>
        /// <value></value>
        public string DepartmentId { get; set; }
        /// <summary>
        /// Mã phòng ban quản lí
        /// </summary>
        /// <value></value>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mã phòng ban quản lí
        /// </summary>
        /// <value></value>
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Id người quản lí
        /// </summary>
        /// <value></value>
        public int UserId { get; set; }
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Mã người quản lí
        /// </summary>
        /// <value></value>
        public string ManagerCode { get; set; }
        /// <summary>
        /// Tên người quản lí
        /// </summary>
        /// <value></value>
        public string UserName { get; set; }
        /// <summary>
        /// Ngày mua
        /// </summary>
        /// <value></value>
        public DateTime? DateBuy { get; set; }
        /// <summary>
        /// Thời gian bảo hành
        /// </summary>
        /// <value></value>
        public int? GuaranteeTime { get; set; }
        /// <summary>
        /// Hạn bảo hành
        /// </summary>
        /// <value></value>
        public DateTime? GuaranteeExpirationDate { get; set; }
        /// <summary>
        /// Trạng thái ghi tăng
        /// 10: Mua mới
        /// 20: Mua cũ
        /// </summary>
        /// <value></value>
        [StringLength(5)]
        public string StatusBuy { get; set; }
        /// <summary>
        /// Trạng thái chất lượng hiện tại
        /// 10 : Mới
        /// 20 : Cũ còn tốt
        /// 30 : Lỗi
        /// 40 : Hỏng
        /// 50 : Bảo hành
        /// </summary>
        /// <value></value>
        [StringLength(5)]
        public string StatusQuality { get; set; }
        /// <summary>
        /// Tên trạng thái chất lượng hiện tại
        /// </summary>
        /// <value></value>
        public string StatusQualityName {get; set;}
        /// <summary>
        /// Id nhà cung cấp
        /// </summary>
        /// <value></value>
        public string VendorName { get; set; }
        /// <summary>
        /// Năm sản xuất
        /// </summary>
        /// <value></value>
        public int? ManufactureYear { get; set; }
        /// <summary>
        /// Số hiệu
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        public string Serial { get; set; }
        /// <summary>
        /// Đất nước
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        public string Country { get; set; }
        /// <summary>
        /// Điều kiện áp dụng bảo hành
        /// </summary>
        /// <value></value>
        [StringLength(2000)]
        public string ConditionApplyGuarantee { get; set; }
        /// <summary>
        /// Ngày bắt đầu khấu hao
        /// </summary>
        /// <value></value>
        public DateTime? DepreciationDate { get; set; }
        /// <summary>
        /// Giá trị tính khấu hao
        /// </summary>
        /// <value></value>
        public double DepreciatedValue { get; set; }
        /// <summary>
        /// Số tháng khấu hao
        /// </summary>
        /// <value></value>
        public double DepreciatedMonth { get; set; }
        /// <summary>
        /// Giá trị tính khấu hao theo tháng
        /// </summary>
        /// <value></value>
        public double DepreciatedMoneyByMonth { get; set; }
        /// <summary>
        /// Số tiền đã khấu hao
        /// </summary>
        /// <value></value>
        public double? DepreciatedMoney { get; set; }
        /// <summary>
        /// Số tiền chưa khấu hao
        /// </summary>
        /// <value></value>
        public double? DepreciatedMoneyRemain { get; set; }
        /// <summary>
        /// Tỉ lệ khấu hao
        /// </summary>
        /// <value></value>
        public double? DepreciatedRate { get; set; }
        /// <summary>
        /// Lần chuyển
        /// </summary>
        /// <value></value>
        public int? TransferCount { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// <value></value>
        public string Note { get; set; }
        /// <summary>
        /// Mục
        /// 10: Tài sản
        /// 20: Công cụ
        /// </summary>
        /// <value></value>
        public int? Category {get; set;}
        /// <summary>
        /// Mã nhà sản xuất
        /// </summary>
        /// <value></value>
        public string ManufacturerCode {get; set;}
        public double? PurchasePrice {get; set;}
        /// <summary>
        /// Tài sản có thể cập nhật được không
        /// </summary>
        /// <value>TRUE: Có thể được cập nhật</value>
        /// <value>FALSE: Không thể thể được cập nhật</value>
        public bool? IsUpdatable {get; set;}
        /// <summary>
        /// File
        /// </summary>
        public List<IFormFile> File { get; set; }
        /// <summary>
        /// FileLink
        /// </summary>
        public List<string> FileLink {get; set;}
        /// <summary>
        /// Image
        /// </summary>
        public IFormFile AssetImage { get; set; }
        /// <summary>
        /// ImageLink
        /// </summary>
        public string AssetImageLink { get; set; }
        public int? QuantityBroken {get; set;}
        public int? QuantityCancel {get; set;}
        public int? QuantityGuarantee {get; set;}
        public int? QuantityLost {get; set;}
    }
}
