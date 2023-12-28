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
    }
}