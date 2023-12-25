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
        public async Task<List<Types>> GetTypes(SearchCondition searchCondition)
        {
            DbConnection _connection = _context.GetConnection();
            try
            {
                var query = $@"
                    SELECT 
                            ""Id""
                        ,   ""Name""
                        ,   ""Code""
                        ,   ""Level""
                        ,   ""ParentId""
                        ,   ""Category""
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
                var listTypes = (await result.ReadAsync<Types>()).ToList();
                return listTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
            
        }
        public async Task<ResponseInfo> DeleteType(int deletedId)
        {
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();
            try
            {
                _logger.LogInformation($"[{_className}][{method}] Start");
                ResponseInfo response = new ResponseInfo();
                TblAssetType assetTypeDeleted = await _context.AssetTypes
                    .Include(x => x.Assets.Where(x => !x.DelFlag))
                    .Where(x => !x.DelFlag && x.Id == deletedId)
                    .FirstOrDefaultAsync();
                if(assetTypeDeleted == null)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.TYPE_OF_ASSET_NOT_FOUND;
                    return response;
                }
                // Kiểm tra số lượng tài sản của loại tài sản cần xoá
                if(assetTypeDeleted.Assets.Count > 0)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.STILL_HAS_EXISTING_ASSETS;
                    response.Data.Add("SL con lai:", assetTypeDeleted.Assets.Count.ToString());
                    return response;
                }
                // Kiểm tra số lượng tài sản của các loại tài sản con
                List<TypeData> ChildrenList = await GetAllChild(deletedId);
                bool allAssetQuantitiesZero = ChildrenList.TrueForAll(item => item.AssetQuantity == 0);
                if(!allAssetQuantitiesZero)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.STILL_HAS_EXISTING_ASSETS;
                    return response;
                }
                List<int?> ChildrenId = ChildrenList.Select(x => x.Id).ToList();
                // Xoá tất cả các loại tài sản con
                string deleteChildrenQuery = @"
                    UPDATE ""SYSASTTP""
                    SET ""DelFlag"" = TRUE
                    WHERE ""Id"" = ANY(@Ids)
                ";
                var param = new {
                    Ids = ChildrenId,
                };
                await _connection.ExecuteAsync(deleteChildrenQuery, param);
                // Xoá loại tài sản được yêu cầu
                assetTypeDeleted.DelFlag = true;
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

        public async Task<ResponseInfo> UpdateType(TypeData updateData)
        {
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();
            try
            {
                _logger.LogInformation($"[{_className}][{method}] Start");
                ResponseInfo response = new();
                var oldType = await _context.AssetTypes
                    .Where(x => !x.DelFlag && x.Id == updateData.Id)
                    .FirstOrDefaultAsync();
                if(oldType == null)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.TYPE_OF_ASSET_NOT_FOUND;
                    return response;
                }
                // Kiểm tra tên loại tài sản
                var checkName = await _context.AssetTypes
                    .Where(x => !x.DelFlag && x.Name == updateData.Name && x.Id != updateData.Id)
                    .FirstOrDefaultAsync();
                if(checkName != null)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.NAME_OF_ASSET_TYPE_ALREADY_EXIST;
                    return response;
                }
                // Kiểm tra mã loại tài sản
                var checkCode = await _context.AssetTypes
                    .Where(x => !x.DelFlag && x.Code == updateData.Code && x.Id != updateData.Id)
                    .FirstOrDefaultAsync();
                if(checkCode != null)
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = MSG_NO.CODE_OF_ASSET_TYPE_ALREADY_EXIST;
                    return response;
                }
                // Kiểm tra nếu Category có thay đổi và loại tài sản cần cập nhật là TS Cha.
                if (updateData.Category != null &&
                    updateData.Category != oldType.Category &&
                    oldType.ParentId != null
                )
                {
                    response.Code = CodeResponse.HAVE_ERROR;
                    response.MsgNo = "Không thể cập nhật";
                    return response;
                }
                // Kiểm tra xem ParentId có thay đổi?
                if (oldType.ParentId != updateData.ParentId)
                {
                    // Nếu ParentId có thay đổi:
                    // B1: Lấy ra danh sách các phần tử con của bản ghi chính cần update.
                    // B2: Kiểm tra ParentId có tồn tại.
                    // B3: Kiểm tra ParentId mới có thuộc danh sách Id các bản ghi con không.
                    // B4: Cập nhật ParentId và Level của bản ghi chính.
                    // B5: Cập nhật Level của các bản ghi con.
                    int LevelChange;
                    // int? newCategory = updateData.Category;
                    List<TypeData> ChildrenList = new List<TypeData>();
                    List<int?> ChildrenId = new List<int?>();
                    // B1: Lấy ra danh sách các phần tử con của bản ghi chính cần update.
                    ChildrenList = await GetAllChild(updateData.Id);
                    ChildrenId = ChildrenList.Select(x => x.Id).ToList();
                    if (updateData.ParentId == null)
                    {
                        LevelChange = oldType.Level - 1;
                    }
                    else
                    {
                        // B2: Kiểm tra ParentId có tồn tại.
                        var newParent = await _context.AssetTypes
                            .Where(x => !x.DelFlag && x.Id == updateData.ParentId)
                            .Select(x => new {
                                x.Id,
                                x.Level,
                                // x.Category,
                            })
                            .FirstOrDefaultAsync();
                        if(newParent == null)
                        {
                            response.Code = CodeResponse.HAVE_ERROR;
                            response.MsgNo = MSG_NO.PARENT_TYPE_OF_ASSET_NOT_FOUND;
                            return response;
                        }
                        // B3: Kiểm tra ParentId mới có thuộc danh sách Id các bản ghi con không.
                        if (ChildrenId.Contains(updateData.ParentId))
                        {
                            response.Code = CodeResponse.HAVE_ERROR;
                            response.MsgNo = MSG_NO.CANNOT_BE_UPDATED_TO_CHILD;
                            return response;
                        }
                        LevelChange = oldType.Level - (newParent.Level + 1);
                        // newCategory = newParent.Category;
                    }
                    // B4: Cập nhật ParentId, Level và Mục của bản ghi chính.
                    oldType.ParentId = updateData.ParentId;
                    oldType.Level -= LevelChange;
                    // oldType.Category = newCategory.Value;
                    // B5: Cập nhật Level của các bản ghi con.
                    string updateChildrenQuery = @"
                        UPDATE ""SYSASTTP""
                        SET ""Level"" = ""Level"" - @LevelChange
                            ---, ""Category"" = @Category
                        WHERE ""Id"" = ANY(@Ids)
                    ";
                    var param = new {
                        LevelChange = LevelChange,
                        Ids = ChildrenId,
                        // Category = newCategory.Value,
                    };
                    await _connection.ExecuteAsync(updateChildrenQuery, param);
                }
                oldType.Name = updateData.Name;
                oldType.Code = updateData.Code;
                await _context.SaveChangesAsync();
                response.Data.Add("Id", oldType.Id.ToString());
                response.Data.Add("ParentId", oldType.ParentId.ToString());
                _logger.LogInformation($"[{_className}][{method}] End");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<List<TypeData>> GetSelectBox()
        {
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();
            try
            {
                _logger.LogInformation($"[{_className}][{method}] Start");
                string selectQuery = @"
                    WITH RECURSIVE ""typeTree"" AS (
                    SELECT ""SYSASTTP"".""Id""
                        , ""SYSASTTP"".""Name""
                        , ""SYSASTTP"".""Code""
                        , ""SYSASTTP"".""ParentId""
                        , ""SYSASTTP"".""Level""
                        , ""SYSASTTP"".""Category""
                        FROM ""SYSASTTP""
                    WHERE ""DelFlag"" = FALSE
                    UNION
                    SELECT type_.""Id""
                        , type_.""Name""
                        , type_.""Code""
                        , type_.""ParentId""
                        , type_.""Level""
                        , type_.""Category""
                    FROM ""SYSASTTP"" type_ INNER JOIN ""typeTree"" ""tT""
                    ON ""tT"".""ParentId"" = type_.""Id""
                    )
                    SELECT DISTINCT *, COALESCE(""ParentId"", ""Id"") AS ""ParentOldId"" FROM ""typeTree""
                    ORDER BY ""ParentOldId"", ""Level"", ""Id""
                ";
                List<TypeData> returnedData = (List<TypeData>) await _connection.QueryAsync<TypeData>(selectQuery);
                _logger.LogInformation($"[{_className}][{method}] End");
                return SortTypeByLevel(returnedData);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<List<TypeData>>GetAllChild(int? searchId){
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();
            try
            {
                string selectQuery = @"
                    WITH RECURSIVE subordinate AS (
                        SELECT
                            ""SYSASTTP"".""Id""
                            , ""SYSASTTP"".""Name""
                            , ""SYSASTTP"".""Code""
                            , ""SYSASTTP"".""ParentId""
                            , ""SYSASTTP"".""Level""
                            , (
                                SELECT COUNT (*) FROM ""SYSAST""
                                WHERE ""SYSAST"".""DelFlag"" = FALSE
                                AND ""SYSAST"".""AssetTypeId"" = ""SYSASTTP"".""Id""
                            ) AS ""AssetQuantity""
                        FROM ""SYSASTTP""
                        WHERE ""Id"" = @SearchId
                            AND ""DelFlag"" = FALSE
                        UNION ALL
                        SELECT
                            typ.""Id""
                            , typ.""Name""
                            , typ.""Code""
                            , typ.""ParentId""
                            , typ.""Level""
                            , (
                                SELECT COUNT (*) FROM ""SYSAST""
                                WHERE ""SYSAST"".""DelFlag"" = FALSE
                                AND ""SYSAST"".""AssetTypeId"" = typ.""Id""
                            ) AS ""AssetQuantity""
                        FROM ""SYSASTTP"" typ
                        JOIN subordinate s
                        ON typ.""ParentId"" = s.""Id""
                        WHERE typ.""DelFlag"" = FALSE
                    )
                    SELECT 
                        s.""Id""
                        , s.""Name""
                        , s.""Code""
                        , s.""Level""
                        , s.""AssetQuantity""
                    FROM subordinate s
                    WHERE s.""Id"" <> @SearchId
                ";
                var param = new {
                    SearchId = searchId,
                };
                List<TypeData> ChildList = (List<TypeData>) await _connection.QueryAsync<TypeData>(selectQuery, param);
                return ChildList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        public List<TypeData> SortTypeByLevel(List<TypeData> data)
        {
            try
            {
                List<TypeData> sortedData = new List<TypeData>();
                // Hàm đệ quy để duyệt và sắp xếp dữ liệu
                void RecursiveSort(int parentId)
                {
                    List<TypeData> children = data.Where(item => item.ParentId == parentId).ToList();
                    children.Sort((a, b) => a.Level.Value - b.Level.Value);
                    foreach (var child in children)
                    {
                        sortedData.Add(child);
                        RecursiveSort(child.Id.Value);
                    }
                }
                // Bắt đầu sắp xếp từ các phần tử có Level 1
                List<TypeData> topLevelItems = data.Where(item => item.Level == 1).ToList();
                topLevelItems.Sort((a, b) => a.Level.Value - b.Level.Value);
                foreach (var item in topLevelItems)
                {
                    sortedData.Add(item);
                    RecursiveSort(item.Id.Value);
                }
                return sortedData;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{_className}] Exception: {ex.Message}");
                throw;
            }
        }
    }
}