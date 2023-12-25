namespace ERP.AST.Models.AssetImportAndExport.Schemas
{
    public class AssetHandOverDetailData
    {
        public int AssetId { get; set; }
        public int? StockId { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        /// <summary>
        /// Mã phòng ban quản lí
        /// </summary>
        /// <value></value>
        public string? DepartmentId { get; set; }
        /// <summary>
        /// Mã người quản lí
        /// </summary>
        /// <value></value>
        public int? UserId { get; set; }
    }
}