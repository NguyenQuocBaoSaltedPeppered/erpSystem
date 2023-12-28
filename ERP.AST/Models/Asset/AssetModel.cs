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
using ERP.AST.Models.Asset.Schemas;
using TblAsset = ERP.Databases.Schemas.Asset;
using TblAssetStock = ERP.Databases.Schemas.AssetStock;
using TblAssetExportDetail = ERP.Databases.Schemas.AssetExportDetail;
using TblAssetImportDetail = ERP.Databases.Schemas.AssetImportDetail;
using TblAssetHistory = ERP.Databases.Schemas.AssetHistory;
using ERP.AST.Enum;

namespace ERP.AST.Models
{
    public partial class AssetModel : CommonModel, IAssetModel
    {
        private readonly string _className = string.Empty;
        private readonly ILogger<AssetModel> _logger;

        public AssetModel(ILogger<AssetModel> logger, IServiceProvider provider) : base(provider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _className = GetType().Name;
        }
        protected string MakePagingQueryString(int CurrentPage, int PageSize)
        {
            return $@"
                WHERE offset_ > {(CurrentPage - 1) * PageSize}
                AND offset_ <= {CurrentPage * PageSize}
            ";
        }
        protected string MakingSortingQueryString(int? OrderBy, int? SortedBy)
        {
            if (SortedBy != null && OrderBy == null)
            {
                throw new NullReferenceException("There's no sort method");
            }
            string orderBy = OrderBy == 0 ? "DESC" : "";
            switch (SortedBy)
            {
                case 1:
                    // 1: Sort theo Giá trị (OriginalPrice)
                    return $@"ORDER BY ""OriginalPrice"" {orderBy}";
                case 2:
                    // 2: Sort theo Tổng GT (Total)
                    return $@"ORDER BY ""Total"" {orderBy}";
                case 3:
                    // 3: Sort theo hạn BH (GuaranteeExpirationDate)
                    return $@"ORDER BY ""GuaranteeExpirationDate"" {orderBy}";
                default:
                    return $@"ORDER BY ""AssetId"" DESC";
            }
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
        public async Task<List<AssetUserDetail>> GetAssetUsers (AssetUserDetailFilter filter)
        {
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                List<AssetUserDetail> userList = new();
                string selectQuery = @"
                    SELECT ""Users"".""Id"" AS ""AssetUserId""
                        , ""Users"".""Name"" AS ""UserName""
                    FROM ""SYSAST""
                    INNER JOIN ""SYSASTST""
                    ON (
                        ""SYSAST"".""Id"" = ""SYSASTST"".""AssetId""
                        AND ""SYSASTST"".""DelFlag"" = FALSE
                    )
                    ---UserData
                    INNER JOIN ""Users""
                    ON (
                        ""SYSASTST"".""UserId"" = ""Users"".""Id""
                        AND ""Users"".""DelFlag"" = FALSE
                    )
                    WHERE ""SYSAST"".""DelFlag"" = FALSE
                    AND ""SYSAST"".""Id"" = @AssetId
                    ORDER BY ""SYSASTST"".""Id"" DESC
                ";
                var param = new
                {
                    filter.AssetId,
                };
                _logger.LogInformation($"[][{_className}][{method}] Query start:");
                _logger.LogInformation($"{selectQuery}");
                userList = (List<AssetUserDetail>) await _connection.QueryAsync<AssetUserDetail>(selectQuery, param);
                _logger.LogInformation($"[][{_className}][{method}] End");
                return userList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<AssetUserDetail> GetAssetUserDetail (AssetUserDetailFilter filter)
        {
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                AssetUserDetail userInfo = new();
                string selectQuery = @"
                    SELECT ""SYSASTST"".""Id"" AS ""StockId""
                        , ""SYSASTST"".""Quantity"" AS ""StockQuantity""
                        ---UserData
                        , ""Users"".""Id"" AS ""AssetUserId""
                        , ""Employees"".""Code"" AS ""UserCode""
                        , ""Users"".""Name"" AS ""UserName""
                        ---BranchData
                        , ""SYSBR"".""Id"" AS ""UserBranchId""
                        , ""SYSBR"".""Name"" AS ""UserBranchName""
                        ---DepartmentData
                        , ""SYSDPM"".""Id"" AS ""UserDepartmentId""
                        , ""SYSDPM"".""Name"" AS ""UserDepartmentName""
                    FROM ""SYSAST""
                    INNER JOIN ""SYSASTST""
                    ON (
                        ""SYSAST"".""Id"" = ""SYSASTST"".""AssetId""
                        AND ""SYSASTST"".""DelFlag"" = FALSE
                    )
                    ---UserData
                    INNER JOIN ""Users""
                    ON (
                        ""SYSASTST"".""UserId"" = ""Users"".""Id""
                        AND ""Users"".""DelFlag"" = FALSE
                    )
                    INNER JOIN ""Employees""
                    ON (
                        ""Users"".""EmployeeId"" = ""Employees"".""Id""
                        AND ""Employees"".""DelFlag"" = FALSE
                    )
                    ---BranchData
                    LEFT JOIN ""SYSBR""
                    ON (
                        ""SYSBR"".""Id"" = ""SYSASTST"".""BranchId""
                        AND ""SYSBR"".""DelFlag"" = FALSE
                    )
                    ---DepartmentData
                    LEFT JOIN ""SYSDPM""
                    ON (
                        ""SYSDPM"".""Id"" = ""SYSASTST"".""DepartmentId""
                        AND ""SYSDPM"".""DelFlag"" = FALSE
                    )
                    WHERE ""SYSAST"".""DelFlag"" = FALSE
                    AND (
                        @AssetId IS NULL
                        OR ""SYSAST"".""Id"" = @AssetId
                    )
                    AND (
                        @UserId IS NULL
                        OR ""Users"".""Id"" = @UserId
                    )
                ";
                var param = new
                {
                    filter.AssetId,
                    filter.UserId,
                };
                _logger.LogInformation($"[][{_className}][{method}] Query start:");
                _logger.LogInformation($"{selectQuery}");
                userInfo = await _connection.QueryFirstOrDefaultAsync<AssetUserDetail>(selectQuery, param);
                _logger.LogInformation($"[][{_className}][{method}] End");
                return userInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<ListAssetData> GetListAssetStockData(AssetFilter assetFilter)
        {
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                ListAssetData listAssetData = new();
                assetFilter ??= new AssetFilter();
                string selectQuery = $@"
                    SELECT
                        ""SYSAST"".""Id"" AS ""AssetId""
                        , ""SYSASTST"".""Id"" AS ""SYSASTSTId""
                        , ""SYSAST"".""Code""
                        , ""SYSAST"".""Name""
                        , ""SYSASTTP"".""Id"" AS ""TypeId""
                        , ""SYSASTTP"".""Name"" AS ""TypeName""
                        , ""SYSASTTP"".""Code"" AS ""TypeCode""
                        , ""SYSASTU"".""Id"" AS ""UnitId""
                        , ""SYSASTU"".""Name"" AS ""UnitName""
                        , ""SYSASTU"".""Code"" AS ""UnitCode""
                        , ""SYSASTST"".""Quantity""
                        , ""SYSASTST"".""QuantityRemain""
                        , ""SYSBR"".""Id"" AS ""BranchId""
                        , ""SYSBR"".""Name"" AS ""BranchName""
                        , ""SYSDPM"".""Id"" AS ""DepartmentId""
                        , ""SYSDPM"".""Name"" AS ""DepartmentName""
                        , ""Users"".""Id"" AS ""UserId""
                        , ""Employees"".""Code"" AS ""EmployeeCode""
                        , ""Users"".""Name"" AS ""UserName""
                    FROM
                        ""SYSAST""
                    INNER JOIN ""SYSASTST""
                        ON (
                            ""SYSASTST"".""AssetId"" = ""SYSAST"".""Id""
                            AND ""SYSASTST"".""DelFlag"" = FALSE
                        )
                    LEFT JOIN ""SYSASTTP""
                        ON (
                            ""SYSAST"".""AssetTypeId"" = ""SYSASTTP"".""Id""
                            AND ""SYSASTTP"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""SYSASTU""
                        ON (
                            ""SYSAST"".""AssetUnitId"" = ""SYSASTU"".""Id""
                            AND ""SYSASTU"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""Users""
                        ON (
                            ""Users"".""Id"" = ""SYSASTST"".""UserId""
                            AND ""Users"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""Employees""
                        ON (
                            ""Users"".""EmployeeId"" = ""Employees"".""Id""
                            AND ""Employees"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""SYSDPM""
                        ON (
                            ""Employees"".""DepartmentId"" = ""SYSDPM"".""Id""
                            AND ""SYSDPM"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""SYSBR""
                        ON (
                            ""SYSDPM"".""BranchId"" = ""SYSBR"".""Id""
                            AND ""SYSBR"".""DelFlag"" = FALSE
                        )
                    WHERE
                        ""SYSAST"".""DelFlag"" = FALSE
                        AND (
                            @BranchId IS NULL
                            OR ""SYSASTST"".""BranchId"" = @BranchId
                        )
                        AND (
                            @UserId IS NULL
                            OR ""SYSASTST"".""UserId"" = @UserId
                        )
                        AND (
                            @Keyword IS NULL
                            OR @Keyword = ''
                            OR LOWER (""SYSAST"".""Code"") LIKE @Keyword
                        )
                        AND ""SYSASTST"".""QuantityRemain"" > 0
                    ORDER BY ""SYSAST"".""Id"" DESC
                ";
                string sqlCount = $@"
                    SELECT
                        COUNT(*)
                    FROM ({selectQuery}) AS c
                ";
                var param = new
                {
                    Keyword = string.IsNullOrEmpty(assetFilter.Keyword) ?  assetFilter.Keyword : assetFilter.Keyword.ToLower(),
                    assetFilter.TypeAsset,
                    assetFilter.BranchId,
                    assetFilter.UserId
                };
                _logger.LogInformation($"[][{_className}][{method}] QueryStart: {selectQuery}");
                int count = await _connection.QueryFirstOrDefaultAsync<int>(sqlCount, param);
                listAssetData.Paging = new Paging(count, assetFilter.CurrentPage, assetFilter.PageSize);
                listAssetData.Data = (List<AssetData>)await _connection.QueryAsync<AssetData>(selectQuery + GetPagingQueryString(listAssetData.Paging), param);
                _logger.LogInformation($"[][{_className}][{method}] End");
                return listAssetData;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<ListAssetHistory> GetAssetHistory(int IdAsset, PagingFilter pagingFilter)
        {
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();

            try
            {
                _logger.LogInformation($"[{_className}][{method}] Start");
                ListAssetHistory returnedData = new();
                string selectQuery = @"
                    SELECT
                        ""SYSASTHTR"".""Id"",
                        ""SYSASTHTR"".""CreatedAt"",
                        ""SYSASTHTR"".""AcctionCode"" AS ""Code"",
                        ""SYSASTHTR"".""ActionName"" AS ""Name"",
                        ""SYSASTTF"".""Id"" AS ""TransferId"",
                        ""SYSASTHTR"".""BeginInventory"",
                        ""SYSASTHTR"".""QuantityChange"",
                        ""SYSASTHTR"".""EndQuantity"",
                        ""SYSASTHTR"".""ValueInventory"",
                        COALESCE(""SYSASTHTR"".""Note"", '') AS ""Note""
                    FROM
                        ""SYSASTHTR""
                    INNER JOIN ""SYSASTST""
                    ON (
                        ""SYSASTHTR"".""AssetStockId"" = ""SYSASTST"".""Id""
                        AND ""SYSASTST"".""DelFlag"" = FALSE
                    )
                    INNER JOIN ""SYSAST""
                    ON (
                        ""SYSASTST"".""AssetId"" = ""SYSAST"".""Id""
                        AND ""SYSAST"".""DelFlag"" = FALSE
                    )
                    LEFT JOIN ""SYSASTTF""
                    ON (
                        ""SYSASTTF"".""Code"" = ""SYSASTHTR"".""AcctionCode""
                        AND ""SYSASTTF"".""DelFlag"" = FALSE
                    )
                    WHERE ""SYSASTHTR"".""DelFlag"" = FALSE
                    AND ""SYSAST"".""Id"" = @Id
                    ORDER BY ""SYSASTHTR"".""Id"" DESC
                ";
                string sqlCount = $@"
                    SELECT
                        COUNT(*)
                    FROM ({selectQuery}) AS c
                ";
                var param = new
                {
                    Id = IdAsset,
                };
                Console.WriteLine(sqlCount);
                int count = await _connection.QueryFirstOrDefaultAsync<int>(sqlCount, param);
                returnedData.Paging = new Paging(count, pagingFilter.CurrentPage, pagingFilter.PageSize);
                returnedData.Data = (List<AssetHistoryData>)await _connection.QueryAsync<AssetHistoryData>(selectQuery + GetPagingQueryString(returnedData.Paging), param);
                _logger.LogInformation($"[{_className}][{method}] End");
                return returnedData;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{_className}][{method}] Exception: {ex.Message}");
                throw ;
            }
        }
    }
}