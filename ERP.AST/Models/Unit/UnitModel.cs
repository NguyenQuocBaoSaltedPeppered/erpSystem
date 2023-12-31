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

        public async Task<List<Units>> GetUnits(SearchCondition searchCondition)
        {
            DbConnection _connection = _context.GetConnection();
            try
            {
                var query = $@"
                    SELECT 
                            ""Id""
                        ,   ""Name""
                        ,   ""Code""
                    FROM ""SYSASTU""
                    WHERE ""DelFlag"" = FALSE
                    {(!string.IsNullOrEmpty(searchCondition.Keyword)
                            ? $@" AND (
									LOWER(UNACCENT(""Name"")) LIKE LOWER(UNACCENT(@Keyword))
								)"
                            : "")}
                    ORDER BY ""Id"" DESC;
                ";
                _logger.LogInformation($"------ {query}");
                var param = new
                {
                    Keyword = ConvertSearchTerm(searchCondition.Keyword),
                };
                var result = await _connection.QueryMultipleAsync(query, param);
                var listUnits = (await result.ReadAsync<Units>()).ToList();
                return listUnits;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
        }
        public async Task<ResponseInfo> UpdateUnit(UnitCreateSchema updateData)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[{_className}][{method}] Start");
                ResponseInfo response = new();
                var checkCode = await _context.AssetUnits
                    .Where(x => x.Code == updateData.Code && !x.DelFlag && x.Id != updateData.Id)
                    .FirstOrDefaultAsync();
                if (checkCode != null)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.CALCULATION_UNIT_CODE_IS_EXISTED;
                    return response;
                }
                var assetUnitOld = await _context.AssetUnits
                    .Where(x => !x.DelFlag && x.Id == updateData.Id)
                    .FirstOrDefaultAsync();
                if (assetUnitOld == null)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.CALCULATION_UNIT_NOT_FOUND;
                    return response;
                }
                assetUnitOld.Name = updateData.Name;
                assetUnitOld.Code = updateData.Code;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"[{_className}][{method}] End");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<ResponseInfo> DeleteUnit(int Id)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[{_className}][{method}] Start");
                ResponseInfo response = new();
                var assetUnitDelete = await _context.AssetUnits
                    .Where(x => !x.DelFlag && x.Id == Id)
                    .FirstOrDefaultAsync();
                if (assetUnitDelete == null)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.CALCULATION_UNIT_NOT_FOUND;
                    return response;
                }
                var isUnitHaveAsset = await _context.Assets
                    .Where(x => !x.DelFlag && x.AssetUnitId == Id)
                    .FirstOrDefaultAsync();
                if(isUnitHaveAsset != null)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.CALCULATION_UNIT_ALREADY_IN_USE;
                    return response;
                }
                assetUnitDelete.DelFlag = true;
                await _context.SaveChangesAsync();
                _logger.LogInformation($"[{_className}][{method}] End");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
    }
}