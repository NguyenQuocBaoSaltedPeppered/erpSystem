using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.AST.Models.AssetImportAndExport.Schemas;
using ERP.Bases.Models;
using ERP.Databases;

namespace ERP.AST.Models
{
    public interface IAssetImportAndExportModel
    {
        Task<ResponseInfo> CreateAssetExport(AssetExportData assetExportData);
    }
    public partial class AssetImportAndExportModel : CommonModel, IAssetImportAndExportModel
    {
        private readonly ILogger<AssetImportAndExportModel> _logger;
        private string _className = string.Empty;

        public AssetImportAndExportModel(IServiceProvider provider, ILogger<AssetImportAndExportModel> logger) : base(provider)
        {
            _logger = logger;
            _className = GetType().Name;
        }
    }
}