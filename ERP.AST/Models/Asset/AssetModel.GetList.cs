using ERP.Databases;
using Dapper;
using System.Data.Common;
using ERP.AST.Models.Asset.Schemas;

namespace ERP.AST.Models
{
    public partial class AssetModel
    {
        public async Task<ListAssetData> GetListAssetData(AssetFilter assetFilter)
        {
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();
            try
            {
                ListAssetData listAssetData = new();
                assetFilter ??= new AssetFilter();
                    string selectQuery = $@"
                    SELECT * FROM (
                        SELECT *
                            , DENSE_RANK() OVER({MakingSortingQueryString(assetFilter.OrderBy, assetFilter.SortedBy)}) offset_
                            FROM (
                                SELECT
                                    ""SYSAST"".""Id"" AS ""AssetId""
                                    , ""SYSASTST"".""Id"" AS ""SYSASTSTId""
                                    , ""SYSAST"".""Code""
                                    , ""SYSAST"".""Name""
                                    , ""SYSAST"".""FinancialCode""
                                    , ""SYSASTTP"".""Name"" AS ""TypeName""
                                    , ""SYSASTTP"".""Code"" AS ""TypeCode""
                                    , ""SYSASTU"".""Name"" AS ""UnitName""
                                    , ""SYSAST"".""Quantity""
                                    , COALESCE(""SYSAST"".""QuantityAllocated"") AS ""QuantityAllocated""
                                    , ""SYSAST"".""QuantityRemain""
                                    -- Giá mua và Thành tiền
                                    , ""SYSAST"".""PurchasePrice""
                                    , ""SYSAST"".""Quantity"" * ""SYSAST"".""OriginalPrice"" AS ""Total""
                                    , ""SYSAST"".""OriginalPrice""
                                    , ""SYSAST"".""DateBuy""
                                    ---, ""SYSAST"".""DateBuy"" + INTERVAL '1 month' * ""SYSAST"".""GuaranteeTime"" AS ""GuaranteeExpirationDate""
                                    , CASE
                                        WHEN
                                            ""SYSAST"".""GuaranteeTime"" IS NOT NULL
                                        THEN
                                            ""SYSAST"".""DateBuy"" + INTERVAL '1 month' * ""SYSAST"".""GuaranteeTime""
                                        ELSE
                                            NULL
                                        END AS ""GuaranteeExpirationDate""
                                    -- Asset Info
                                    , ""SYSBR"".""Id"" AS ""BranchId""
                                    , ""SYSBR"".""Name"" AS ""BranchName""
                                    , ""SYSDPM"".""Name"" AS ""DepartmentName""
                                    , ""SYSAST"".""CreatedAt""
                                    , ""SYSAST"".""UpdatedAt""
                                    , ""SYSAST"".""UserId""
                                    , ""Employees"".""Code"" AS ""ManagerCode""
                                    , ""Users"".""Name"" AS ""UserName""
                                    , ""SYSAST"".""QuantityBroken""
                                    , ""SYSAST"".""QuantityCancel""
                                    , ""SYSAST"".""QuantityGuarantee""
                                    , ""SYSAST"".""QuantityLost""
                                    , ""SYSAST"".""Vendor"" AS ""VendorName""
                                    , ""SYSAST"".""DepreciationDate""
                                    , ""SYSAST"".""DepreciatedMonth""
                                    , ""SYSAST"".""TransferCount""
                                    , ""SYSAST"".""Note""
                                    , CASE
                                        WHEN
                                            (SELECT COUNT(*) FROM ""SYSASTST""
                                            WHERE ""SYSASTST"".""AssetId"" = ""SYSAST"".""Id"") > 1
                                            OR
                                            (SELECT COUNT(*) FROM ""SYSASTIDT""
                                            WHERE ""SYSASTIDT"".""AssetId"" = ""SYSAST"".""Id"") > 1
                                            OR
                                            (SELECT COUNT(*) FROM ""SYSASTEDT""
                                            INNER JOIN ""SYSASTST""
                                            ON (
                                                ""SYSASTEDT"".""AssetStockId"" = ""SYSASTST"".""Id""
                                                AND ""SYSASTST"".""DelFlag"" = FALSE
                                            )
                                            WHERE ""SYSASTST"".""AssetId"" = ""SYSAST"".""Id"") > 0
                                            OR
                                            (SELECT COUNT(*) FROM ""SYSASTHTR""
                                            WHERE ""SYSASTHTR"".""AssetStockId"" = ""SYSASTST"".""Id"") > 1
                                        THEN
                                            FALSE
                                        ELSE
                                            TRUE
                                    END AS ""IsUpdatable""
                                    , ""SYSASTST"".""Id"" AS ""AssetStockId""
                                    , ""SYSASTST"".""Quantity"" AS ""StockQuantity""
                                    -- Asset stockInfo
                                    , UserBranches.""Id"" AS ""UserBranchId""
                                    , UserBranches.""Name"" AS ""UserBranchName""
                                    , UserDepartments.""Id"" AS ""UserDepartmentId""
                                    , UserDepartments.""Name"" AS ""UserDepartmentName""
                                    , UserDatas.""Id"" AS ""AssetUserId""
                                    , EmployeeDatas.""Code"" AS ""UserCode""
                                FROM
                                    ""SYSAST""
                                --Asset
                                LEFT JOIN ""SYSBR""
                                    ON (
                                        ""SYSAST"".""BranchId"" = ""SYSBR"".""Id""
                                        AND ""SYSBR"".""DelFlag"" = FALSE
                                    )
                                LEFT JOIN ""SYSDPM""
                                    ON (
                                        ""SYSAST"".""DepartmentId"" = ""SYSDPM"".""Id""
                                        AND ""SYSDPM"".""DelFlag"" = FALSE
                                    )
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
                                INNER JOIN ""Users""
                                    ON (
                                        ""SYSAST"".""UserId"" = ""Users"".""Id""
                                        AND ""Users"".""DelFlag"" = FALSE
                                    )
                                INNER JOIN ""Employees""
                                    ON (
                                        ""Employees"".""Id"" = ""Users"".""EmployeeId""
                                        AND ""Employees"".""DelFlag"" = FALSE
                                    )
                                -- Asset StockInfo
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
                                        EmployeeDatas.""BranchId"" = UserBranches.""Id""
                                        AND UserBranches.""DelFlag"" = FALSE
                                    )
                                WHERE
                                    ""SYSAST"".""DelFlag"" = FALSE
                                    AND ""SYSAST"".""Quantity"" > 0
                                    AND ""SYSASTST"".""Quantity"" > 0
                                    AND (
                                        @TypeAsset IS NULL
                                        OR ""SYSASTTP"".""Id"" = @TypeAsset
                                    )
                                    AND (
                                        @UserId IS NULL
                                        OR UserDatas.""Id"" = @UserId
                                    )
                                    AND (
                                        @BranchId IS NULL
                                        OR UserBranches.""Id"" = @BranchId
                                    )
                                    AND (
                                        @DepartmentId IS NULL
                                        OR @DepartmentId = ''
                                        OR UserDepartments.""Id"" = @DepartmentId
                                    )
                                    AND (
                                        @Keyword IS NULL
                                        OR @Keyword = ''
                                        OR LOWER (unaccent (""SYSAST"".""Name"")) LIKE @Keyword
                                        OR LOWER (unaccent (""SYSAST"".""Code"")) LIKE @Keyword
                                    )
                                    {GetAssetStatusQuery(assetFilter.Status)}
                            ) result
                        ) result_offset
                    ";
                    string sqlMain = $@"
                        SELECT
                            COUNT(DISTINCT(offset_)):: INT
                        FROM ({selectQuery}) AS c;
                        {selectQuery}
                        {(assetFilter.PageSize == 0 ? "" : MakePagingQueryString(assetFilter.CurrentPage, assetFilter.PageSize))}
                    ";
                    var param = new
                    {
                        Keyword = ConvertSearchTerm(assetFilter.Keyword),
                        assetFilter.TypeAsset,
                        assetFilter.BranchId,
                        assetFilter.UserId,
                        assetFilter.DepartmentId
                    };
                    var dictionary = new Dictionary<int, AssetData>();
                var grid = await _connection.QueryMultipleAsync(sqlMain, param);
                int totalRecord = await grid.ReadFirstOrDefaultAsync<int>();
                if(totalRecord > 0)
                {
                    listAssetData.Paging = new Paging(totalRecord, assetFilter.CurrentPage, assetFilter.PageSize);
                }
                listAssetData.Data = grid.Read<AssetData, AssetUserDetail, AssetData>(
                    (asset, userDetail) => {
                        if (!dictionary.TryGetValue(asset.AssetId, out AssetData entry))
                        {
                            entry = asset;
                            asset.AssetUserDetails = new List<AssetUserDetail>();
                            dictionary.Add(entry.AssetId, entry);
                        }
                        if (userDetail != null)
                        {
                            entry.AssetUserDetails.Add(new AssetUserDetail()
                            {
                                StockQuantity = userDetail.StockQuantity,
                                UserBranchId = userDetail.UserBranchId,
                                UserBranchName = userDetail.UserBranchName,
                                UserDepartmentId = userDetail.UserDepartmentId,
                                UserDepartmentName = userDetail.UserDepartmentName,
                                AssetUserId = userDetail.AssetUserId,
                                UserName = userDetail.UserName,
                                UserCode = userDetail.UserCode,
                            });
                        }
                        return entry;
                    }, splitOn: "AssetStockId"
                ).Distinct().AsList();
                _logger.LogInformation($"[{_className}][{method}] End");
                return listAssetData;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
        }
    }
}