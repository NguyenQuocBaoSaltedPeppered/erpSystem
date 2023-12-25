using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.AST.Enum;
using ERP.Databases;

namespace ERP.AST.Common
{
    public class CommonModel : BaseModel
    {
        protected readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<CommonModel> _logger;
        private string _className = string.Empty;

        public CommonModel(IServiceProvider provider) : base(provider)
        {
            IWebHostEnvironment webHostEnvironment = provider.GetService<IWebHostEnvironment>();
            ILogger<CommonModel> logger = provider.GetService<ILogger<CommonModel>>();
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _className = GetType().Name;
        }
        public string GetAssetStatusQuery(int StatusQuery)
        {
            switch(StatusQuery)
            {
                case AssetConstants.ASSET_ALLOCATED:
                    return $@" AND ""SYSAST"".""QuantityAllocated"" > 0";
                case AssetConstants.ASSET_BROKEN:
                    return $@" AND ""SYSAST"".""QuantityBroken"" > 0";
                case AssetConstants.ASSET_UNALLOCATED:
                    return $@" AND ""SYSAST"".""QuantityAllocated"" = 0";
                case AssetConstants.ASSET_GUARANTEE:
                    return $@" AND ""SYSAST"".""QuantityGuarantee"" > 0";
                case AssetConstants.ASSET_CANCEL:
                    return $@" AND ""SYSAST"".""QuantityCancel"" > 0";
                case AssetConstants.ASSET_LOST:
                    return $@" AND ""SYSAST"".""QuantityLost"" > 0";
                case AssetConstants.ASSET_LIQUIDATION:
                    return $@" AND ""SYSAST"".""QuantityLiquidation"" > 0";
                default:
                    return "";
            }
        }
    }
}