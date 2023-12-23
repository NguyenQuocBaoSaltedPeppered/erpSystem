using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using ERP.AST.Models;
using ERP.Bases.Models;
using ERP.AST.Models.Unit.Schemas;
using TblAssetUnit = ERP.Databases.Schemas.AssetUnit;

namespace ERP.AST.Models
{
    public class UnitModel : CommonModel, IUnitModel
    {
        private readonly string _className = string.Empty;
        private readonly ILogger<UnitModel> _logger;

        public UnitModel(ILogger<UnitModel> logger, IServiceProvider provider) : base(provider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _className = GetType().Name;
        }
        public async Task<ResponseInfo> CreateUnit(UnitCreateSchema createData)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[{_className}][{method}] Start");
                ResponseInfo response = new();
                var checkCode = await _context.AssetUnits.Where(x => !x.DelFlag && x.Code == createData.Code).FirstOrDefaultAsync();
                if (checkCode != null)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.CALCULATION_UNIT_CODE_IS_EXISTED;
                    return response;
                }
                TblAssetUnit assetUnitDataSubmit = new TblAssetUnit()
                {
                    Name = createData.Name,
                    Code = createData.Code,
                };
                _context.AssetUnits.Add(assetUnitDataSubmit);
                await _context.SaveChangesAsync();
                response.Data.Add("Id", assetUnitDataSubmit.Id.ToString());
                return response;
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
            
        }
    }
}