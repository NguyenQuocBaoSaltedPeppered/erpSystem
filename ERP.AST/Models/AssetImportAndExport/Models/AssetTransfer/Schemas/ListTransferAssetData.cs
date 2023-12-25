using ERP.Databases;

namespace ERP.AST.Models.AssetImportAndExport.Schemas
{
    public class ListTransferAssetData
    {
        /// <summary>
        /// Danh sách data
        /// </summary>
        /// <value></value>
        public List<AssetTransferSelectData> Data { get; set; }
        /// <summary>
        /// Thông tin phân trang: page, page size
        /// </summary>
        /// <value></value>
        public Paging Paging { get; set; }
    }
}