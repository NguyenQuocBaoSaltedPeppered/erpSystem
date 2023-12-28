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
            return StatusQuery switch
            {
                AssetConstants.ASSET_ALLOCATED => $@" AND ""SYSAST"".""QuantityAllocated"" > 0",
                AssetConstants.ASSET_BROKEN => $@" AND ""SYSAST"".""QuantityBroken"" > 0",
                AssetConstants.ASSET_UNALLOCATED => $@" AND ""SYSAST"".""QuantityAllocated"" = 0",
                AssetConstants.ASSET_GUARANTEE => $@" AND ""SYSAST"".""QuantityGuarantee"" > 0",
                AssetConstants.ASSET_CANCEL => $@" AND ""SYSAST"".""QuantityCancel"" > 0",
                AssetConstants.ASSET_LOST => $@" AND ""SYSAST"".""QuantityLost"" > 0",
                _ => "",
            };
        }
    }
}