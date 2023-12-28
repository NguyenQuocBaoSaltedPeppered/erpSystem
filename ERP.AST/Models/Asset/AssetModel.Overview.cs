using Dapper;
using System.Data.Common;
using ERP.AST.Models.Asset.Schemas;

namespace ERP.AST.Models
{
    public partial class AssetModel
    {
        public async Task<AssetValueOverview> GetAssetValueOverview(AssetFilter assetFilter)
        {
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                AssetValueOverview assetValueOverview = new();
                string selectQuery = $@"
                    SELECT
                        SUM(""UniqueAsset"".""Quantity"") AS ""TotalQuantity""
                        , SUM(""UniqueAsset"".""Total"") AS ""TotalValue""
                        , ROUND(SUM(""UniqueAsset"".""DepreciatedMoney"")::numeric, 2) AS ""TotalDepreciated""
                        --, SUM(""UniqueAsset"".""DepreciatedMoney"") AS ""TotalDepreciated""
                    FROM (
                        SELECT
                            DISTINCT ""SYSAST"".""Id""
                            , ""SYSAST"".""Quantity""
                            , ""SYSAST"".""Quantity"" * ""SYSAST"".""OriginalPrice"" AS ""Total""
                            , ""SYSAST"".""DepreciatedMoney""
                        FROM ""SYSAST""
                        INNER JOIN ""SYSASTTP""
                            ON (
                                ""SYSAST"".""AssetTypeId"" = ""SYSASTTP"".""Id""
                                AND ""SYSASTTP"".""DelFlag"" = FALSE
                            )
                        INNER JOIN ""SYSASTU""
                            ON (
                                ""SYSAST"".""AssetUnitId"" = ""SYSASTU"".""Id""
                                AND ""SYSASTU"".""DelFlag"" = FALSE
                            )
                        INNER JOIN ""SYSASTST""
                            ON (
                                ""SYSASTST"".""AssetId"" = ""SYSAST"".""Id""
                                AND ""SYSASTST"".""DelFlag"" = FALSE
                            )
                        INNER JOIN ""Users"" UserDatas
                            ON (
                                ""SYSASTST"".""UserId"" = UserDatas.""Id""
                                AND UserDatas.""DelFlag"" = FALSE
                            )
                        INNER JOIN ""Employees"" EmployeeDatas
                            ON (
                                EmployeeDatas.""Id"" = UserDatas.""EmployeeId""
                                AND EmployeeDatas.""DelFlag"" = FALSE
                            )
                        INNER JOIN ""SYSDPM"" UserDepartments
                            ON (
                                EmployeeDatas.""DepartmentId"" = UserDepartments.""Id""
                                AND UserDepartments.""DelFlag"" = FALSE
                            )
                        LEFT JOIN ""SYSBR"" UserBranches
                            ON (
                                UserDepartments.""BranchId"" = UserBranches.""Id""
                                AND UserBranches.""DelFlag"" = FALSE
                            )
                        WHERE
                            ""SYSAST"".""DelFlag"" = FALSE
                            AND (
                                @TypeAsset IS NULL
                                OR ""SYSASTTP"".""Id"" = @TypeAsset
                            )
                            AND (
                                @Keyword IS NULL
                                OR @Keyword = ''
                                OR LOWER (unaccent (""SYSAST"".""Name"")) LIKE @Keyword
                                OR LOWER (unaccent (""SYSAST"".""Code"")) LIKE @Keyword
                            )
                            AND (
                                @BranchId IS NULL
                                OR UserBranches.""Id"" = @BranchId
                            )
                            AND (
                                @UserId IS NULL
                                OR UserDatas.""Id"" = @UserId
                            )
                            AND (
                                @DepartmentId IS NULL
                                OR @DepartmentId = ''
                                OR UserDepartments.""Id"" = @DepartmentId
                            )
                            {GetAssetStatusQuery(assetFilter.Status)}
                    ) AS ""UniqueAsset""
                ";
                var param = new
                {
                    Keyword = ConvertSearchTerm(assetFilter.Keyword),
                    assetFilter.TypeAsset,
                    assetFilter.BranchId,
                    assetFilter.UserId,
                    assetFilter.DepartmentId
                };
                _logger.LogInformation($"[][{_className}][{method}] Query start");
                _logger.LogInformation($"[][{_className}][{method}] {selectQuery}");
                assetValueOverview = await _connection.QueryFirstOrDefaultAsync<AssetValueOverview>(selectQuery, param);
                assetValueOverview.TotalRemain = assetValueOverview.TotalValue - assetValueOverview.TotalDepreciated;
                _logger.LogInformation($"[][{_className}][{method}] End");
                return assetValueOverview;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<AssetOverview> GetAssetOverview(AssetFilter assetFilter)
        {
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                AssetOverview assetOver = new();
                assetFilter ??= new AssetFilter();
                string selectQuery = $@"
                    SELECT
                        SUM(CASE WHEN ""UniqueAsset"".""Quantity"" > 0 THEN 1 END) AS ""QuantityAsset""
                        , SUM(CASE WHEN ""UniqueAsset"".""QuantityAllocated"" = 0 THEN 1 END) AS ""QuantityAssetUnallocated""
                        , SUM(CASE WHEN ""UniqueAsset"".""QuantityAllocated"" > 0 THEN 1 END) AS ""QuantityAssetAllocated""
                        , SUM(CASE WHEN ""UniqueAsset"".""QuantityBroken"" > 0 THEN 1 END) AS ""QuantityBroken""
                        , SUM(CASE WHEN ""UniqueAsset"".""QuantityGuarantee"" > 0 THEN 1 END) AS ""QuantityGuarantee""
                        , SUM(CASE WHEN ""UniqueAsset"".""QuantityLost"" > 0 THEN 1 END) AS ""QuantityLost""
                        , SUM(CASE WHEN ""UniqueAsset"".""QuantityCancel"" > 0 THEN 1 END) AS ""QuantityCancel""
                    FROM (
                        SELECT
                        DISTINCT
                            ""SYSAST"".""Id""
                            , ""SYSAST"".""Quantity""
                            , ""SYSAST"".""QuantityAllocated""
                            , ""SYSAST"".""QuantityBroken""
                            , ""SYSAST"".""QuantityGuarantee""
                            , ""SYSAST"".""QuantityLost""
                            , ""SYSAST"".""QuantityCancel""
                        FROM
                            ""SYSAST""
                        INNER JOIN ""SYSASTTP""
                            ON (
                                ""SYSAST"".""AssetTypeId"" = ""SYSASTTP"".""Id""
                                AND ""SYSASTTP"".""DelFlag"" = FALSE
                            )
                        INNER JOIN ""SYSASTU""
                            ON (
                                ""SYSAST"".""AssetUnitId"" = ""SYSASTU"".""Id""
                                AND ""SYSASTU"".""DelFlag"" = FALSE
                            )
                        INNER JOIN ""SYSASTST""
                            ON (
                                ""SYSASTST"".""AssetId"" = ""SYSAST"".""Id""
                                AND ""SYSASTST"".""DelFlag"" = FALSE
                            )
                        INNER JOIN ""Users"" UserDatas
                            ON (
                                ""SYSASTST"".""UserId"" = UserDatas.""Id""
                                AND UserDatas.""DelFlag"" = FALSE
                            )
                        INNER JOIN ""Employees"" EmployeeDatas
                            ON (
                                EmployeeDatas.""Id"" = UserDatas.""EmployeeId""
                                AND EmployeeDatas.""DelFlag"" = FALSE
                            )
                        INNER JOIN ""SYSDPM"" UserDepartments
                            ON (
                                EmployeeDatas.""DepartmentId"" = UserDepartments.""Id""
                                AND UserDepartments.""DelFlag"" = FALSE
                            )
                        LEFT JOIN ""SYSBR"" UserBranches
                            ON (
                                UserDepartments.""BranchId"" = UserBranches.""Id""
                                AND UserBranches.""DelFlag"" = FALSE
                            )
                        WHERE
                            ""SYSAST"".""DelFlag"" = FALSE
                            AND (
                                @TypeAsset IS NULL
                                OR ""SYSASTTP"".""Id"" = @TypeAsset
                            )
                            AND (
                                @Keyword IS NULL
                                OR @Keyword = ''
                                OR LOWER (unaccent (""SYSAST"".""Name"")) LIKE @Keyword
                                OR LOWER (unaccent (""SYSAST"".""Code"")) LIKE @Keyword
                            )
                            AND (
                                @BranchId IS NULL
                                OR UserBranches.""Id"" = @BranchId
                            )
                            AND (
                                @UserId IS NULL
                                OR UserDatas.""Id"" = @UserId
                            )
                            AND (
                                @DepartmentId IS NULL
                                OR @DepartmentId = ''
                                OR UserDepartments.""Id"" = @DepartmentId
                            )
                    ) AS ""UniqueAsset""
                ";
                var param = new
                {
                    Keyword = ConvertSearchTerm(assetFilter.Keyword),
                    assetFilter.TypeAsset,
                    assetFilter.BranchId,
                    assetFilter.UserId,
                    assetFilter.DepartmentId
                };
                _logger.LogInformation($"[][{_className}][{method}] Query start");
                _logger.LogInformation($"[][{_className}][{method}] {selectQuery}");
                assetOver = await _connection.QueryFirstOrDefaultAsync<AssetOverview>(selectQuery, param);
                _logger.LogInformation($"[][{_className}][{method}] End");
                return assetOver;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
    }
}