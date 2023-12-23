using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases.Schemas
{
    [Table("SYSASTHTR")]
    public class AssetHistory : TableHaveIdInt, ITable
    {
        /// <summary>
        /// Tên hành động
        /// </summary>
        /// <value></value>
        public string? ActionName { get; set; }
        /// <summary>
        /// Mã hành động
        /// </summary>
        /// <value></value>
        public string? AcctionCode { get; set; }
        /// <summary>
        /// Số lượng tồn ban đầu
        /// </summary>
        /// <value></value>
        public int? BeginInventory { get; set; }
        /// <summary>
        /// Số lượng thay đổi
        /// </summary>
        /// <value></value>
        public int QuantityChange { get; set; }
        /// <summary>
        /// Số lượng sau thay đổi
        /// </summary>
        /// <value></value>
        public int EndQuantity { get; set; }
        public string? Note { get; set; }
        /// <summary>
        /// Giá trị tồn
        /// </summary>
        /// <value></value>
        public double? ValueInventory { get; set; }

        #region Default
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
        /// Mã tài sản trong kho
        /// </summary>
        /// <value></value>
        public int? AssetStockId { get; set; }
        public int? UserId {get; set;}
        public virtual AssetStock? AssetStock {get; set;}
        #endregion
    }
}