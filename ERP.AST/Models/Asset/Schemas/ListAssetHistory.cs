using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Databases;

namespace ERP.AST.Models.Asset.Schemas
{
    public class ListAssetHistory
    {
        /// <summary>
        /// Danh sách data
        /// </summary>
        /// <value></value>
        public List<AssetHistoryData> Data { get; set; }
        /// <summary>
        /// Thông tin phân trang: page, page size
        /// </summary>
        /// <value></value>
        public Paging Paging { get; set; }
    }
}