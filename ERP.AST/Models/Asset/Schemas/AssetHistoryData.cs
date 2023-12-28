using System;

namespace ERP.AST.Models.Asset.Schemas
{
    public class AssetHistoryData
    {
        public int Id { set; get; }
        /// <summary>
        /// Ngày (CreatedAt)
        /// </summary>
        /// <value></value>
        public DateTime CreatedAt { set; get; }
        /// <summary>
        /// Mã hành động
        /// </summary>
        /// <value></value>
        public string Code { set; get; }
        /// <summary>
        /// Tên hành động
        /// </summary>
        /// <value></value>
        public string Name { set; get; }
        /// <summary>
        /// Id của lần cấp phát hoặc thu hồi
        /// </summary>
        /// <value></value>
        public int? TransferId { set; get; }
        /// <summary>
        /// Tồn đầu kỳ
        /// </summary>
        /// <value></value>
        public int BeginInventory { set; get; }
        /// <summary>
        /// Số lượng
        /// </summary>
        /// <value></value>
        public int QuantityChange { set; get; }
        /// <summary>
        /// Tồn cuối kỳ
        /// </summary>
        /// <value></value>
        public int EndQuantity { set; get; }
        /// <summary>
        /// Giá trị tồn
        /// </summary>
        /// <value></value>
        public double ValueInventory { set; get; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// <value></value>
        public string Note { set; get; }
    }
}