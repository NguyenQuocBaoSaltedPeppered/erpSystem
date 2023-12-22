using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases.Schemas
{
    [Table("SYSAST")]
    public class Asset : TableHaveIdInt, IAssetQuantityColumn, ITable
    {
        public Asset() {
            AssetStocks = new HashSet<AssetStock>();
            AssetImportDetails = new HashSet<AssetImportDetail>();
        }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// <value></value>
        [StringLength(500)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Mã chứng từ (User input, can be duplicated)
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        public string FinancialCode { get; set; } = null!;
        /// <summary>
        /// Mã tài sản (Generate tự động, unique)
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        public string Code { get; set; } = null!;
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
        /// Người bán
        /// </summary>
        /// <value></value>
        public string? Vendor {get; set;}
        /// <summary>
        /// Mã nhà sản xuất
        /// </summary>
        /// <value></value>
        public string? ManufacturerCode {get; set;}
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
        public string? Serial { get; set; }
        /// <summary>
        /// Đất nước
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        public string? Country { get; set; }
        /// <summary>
        /// Điều kiện áp dụng bảo hành
        /// </summary>
        /// <value></value>
        [StringLength(2000)]
        public string? ConditionApplyGuarantee { get; set; }
        /// <summary>
        /// Nguyên giá
        /// </summary>
        /// <value></value>
        public decimal? OriginalPrice { get; set; }
        /// <summary>
        /// Giá mua
        /// </summary>
        /// <value></value>
        public decimal? PurchasePrice { get; set; }
        /// <summary>
        /// Ngày bắt đầu tính khấu hao
        /// </summary>
        /// <value></value>
        public DateTime? DepreciationDate { get; set; }
        /// <summary>
        /// Giá trị tính khấu hao
        /// </summary>
        /// <value></value>
        public double? DepreciatedValue { get; set; }
        /// <summary>
        /// Giá trị tính khấu hao theo tháng
        /// </summary>
        /// <value></value>
        public double DepreciatedMoneyByMonth { get; set; }
        /// <summary>
        /// Số tháng khấu hao
        /// </summary>
        /// <value></value>
        public double? DepreciatedMonth { get; set; }
        /// <summary>
        /// Số tiền đã khấu hao
        /// </summary>
        /// <value></value>
        public decimal? DepreciatedMoney { get; set; }
        /// <summary>
        /// Số tiền chưa khấu hao
        /// </summary>
        /// <value></value>
        public decimal? DepreciatedMoneyRemain { get; set; }
        /// <summary>
        /// Tỉ lệ khấu hao
        /// </summary>
        /// <value></value>
        public double? DepreciatedRate { get; set; }
        /// <summary>
        /// Lần chuyển
        /// </summary>
        /// <value></value>
        public int TransferCount {get; set;}
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// <value></value>
        public string? Note { get; set; }

        #region Default
        public int Quantity {get; set;}
        public int QuantityAllocated {get; set;}
        public int QuantityRemain {get; set;}
        public int QuantityBroken {get; set;}
        public int QuantityCancel {get; set;}
        public int QuantityGuarantee {get; set;}
        public int QuantityLost {get; set;}
        public DateTimeOffset CreatedAt {get; set;}
        public int CreatedBy {get; set;}
        public string CreatedIp {get; set;} = null!;
        public DateTimeOffset? UpdatedAt {get; set;}
        public int? UpdatedBy {get; set;}
        public string UpdatedIp {get; set;} = null!;
        public bool DelFlag {get; set;}
        #endregion

        #region Relation
        /// <summary>
        /// Mã vị trí tài sản
        /// </summary>
        /// <value></value>
        public int? BranchId { get; set; }
        /// <summary>
        /// Mã phòng ban quản lí tài sản
        /// </summary>
        /// <value></value>
        public string? DepartmentId { get; set; }
        /// <summary>
        /// Mã người quản lí
        /// </summary>
        /// <value></value>
        public int? UserId { get; set; }
        /// <summary>
        /// Id loại tài sản
        /// </summary>
        /// <value></value>
        public int? AssetTypeId { get; set; }
        /// <summary>
        /// Id đơn vị tính tài sản
        /// </summary>
        /// <value></value>
        public int? AssetUnitId { get; set; }

        public virtual AssetType? AssetType {get; set;}
        public virtual AssetUnit? AssetUnit {get; set;}
        public virtual ICollection<AssetStock>? AssetStocks {get; set;}
        public virtual ICollection<AssetImportDetail>? AssetImportDetails {get; set;}
        #endregion
    }
}