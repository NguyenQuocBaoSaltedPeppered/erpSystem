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
using ERP.AST.Models.Type.Schemas;
using TblAssetType = ERP.Databases.Schemas.AssetType;
namespace ERP.AST.Models
{
    public class TypeModel : CommonModel, ITypeModel
    {
        private readonly string _className = string.Empty;
        private readonly ILogger<UnitModel> _logger;

        public TypeModel(ILogger<UnitModel> logger, IServiceProvider provider) : base(provider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _className = GetType().Name;
        }
        public async Task<ResponseInfo> CreateType(TypeData createData)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[{_className}][{method}] Start");
                ResponseInfo response = new();
                int Level = 1;
                if(createData.ParentId != null)
                {
                    var checkParentId = await _context.AssetTypes
                        .Where(x => !x.DelFlag && x.Id == createData.ParentId)
                        .FirstOrDefaultAsync();
                    if(checkParentId == null)
                    {
                        response.Code = CodeResponse.HAVE_ERROR;
                        response.MsgNo = MSG_NO.PARENT_TYPE_OF_ASSET_NOT_FOUND;
                        return response;
                    }
                    else
                    {
                        Level = checkParentId.Level + 1;
                        createData.Category = checkParentId.Category;
                    }
                }
                if(createData.Category == null)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.ASSET_CATEGORY_CODE_IS_EMPTY;
                    return response;
                }
                var checkName = await _context.AssetTypes
                    .Where(x => !x.DelFlag && x.Name == createData.Name)
                    .FirstOrDefaultAsync();
                if(checkName != null)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.NAME_OF_ASSET_TYPE_ALREADY_EXIST;
                    return response;
                }
                var checkCode = await _context.AssetTypes
                    .Where(x => !x.DelFlag && x.Code == createData.Code)
                    .FirstOrDefaultAsync();
                if(checkCode != null)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.CODE_OF_ASSET_TYPE_ALREADY_EXIST;
                    return response;
                }
                TblAssetType assetTypeDataSubmit = new()
                {
                    Name = createData.Name,
                    Code = createData.Code,
                    ParentId = createData.ParentId,
                    Level = Level,
                    Category = createData.Category.Value,
                };
                _context.AssetTypes.Add(assetTypeDataSubmit);
                await _context.SaveChangesAsync();
                response.Data.Add("Id", assetTypeDataSubmit.Id.ToString());
                response.Data.Add("ParentId", assetTypeDataSubmit.ParentId.ToString());
                response.Data.Add("Code", assetTypeDataSubmit.Code);
                _logger.LogInformation($"[{_className}][{method}] End");
                return response;
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
            
        }
        public async Task<List<TypeData>> GetTypes(SearchCondition searchCondition)
        {
            DbConnection _connection = _context.GetConnection();
            try
            {
                var query = $@"
                    SELECT 
                            ""Id"" AS ""TypeId""
                        ,   ""Name"" AS ""TypeName""
                    FROM ""SYSASTTP""
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
                var listTypes = (await result.ReadAsync<TypeData>()).ToList();
                return listTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
            
        }
    }
}