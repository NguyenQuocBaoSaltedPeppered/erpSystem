using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.AST.Models.Asset.Schemas;
using ERP.Databases;

namespace ERP.AST.Models
{
    public interface IAssetModel
    {
        Task<ResponseInfo> CreateAsset(AssetData assetData);
        Task<ListAssetData> GetListAssetData(AssetFilter assetFilter);
    }
}