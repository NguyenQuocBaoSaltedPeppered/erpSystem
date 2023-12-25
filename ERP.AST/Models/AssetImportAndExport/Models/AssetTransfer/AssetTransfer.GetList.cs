using System;
using System.Collections.Generic;
using System.Data.Common;
using ERP.AST.Enum;
using ERP.AST.Models.AssetImportAndExport.Schemas;
using ERP.Databases;
using Microsoft.EntityFrameworkCore;
using TblAssetTransfer = ERP.Databases.Schemas.AssetTransfer;
using TblAssetExport = ERP.Databases.Schemas.AssetExport;
using TblAssetExportDetail = ERP.Databases.Schemas.AssetExportDetail;
using TblAssetImport = ERP.Databases.Schemas.AssetImport;
using TblAssetImportDetail = ERP.Databases.Schemas.AssetImportDetail;
using TblAssetStock = ERP.Databases.Schemas.AssetStock;
using TblAsset = ERP.Databases.Schemas.Asset;
using Dapper;

namespace ERP.AST.Models
{
    public partial class AssetImportAndExportModel
    {
        public async Task<ListTransferAssetData> GetListAssetHandOverData(FilterTransfer filter)
        {
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();

            try
            {
                _logger.LogInformation($"[{_className}][{method}] Start");
                ListTransferAssetData returnedData = new ListTransferAssetData();
                string selectQuery = @"
                    SELECT
                        -- Mã bàn giao
                        ""SYSASTTF"".""Id""
                        , ""SYSASTTF"".""Code"" AS ""Code""
                        , ""SYSASTI"".""ImportDate"" AS ""Date""
                        -- Số lượng
                        , SUM(""SYSASTIDT"".""Quantity"") AS ""Quantity""
                        -- Từ chi nhánh
                        , ""BranchExport"".""Id"" AS ""BranchExportId""
                        , ""BranchExport"".""Name"" AS ""BranchExportName""
                        -- Bàn giao về
                        , ""SYSBR"".""Id"" AS ""BranchId""
                        , ""SYSBR"".""Name"" AS ""BranchName""
                        -- Người bàn giao
                        , ""UserHandOver"".""Id"" AS ""UserHandOverId""
                        , ""EmployeeHandOver"".""Code"" AS ""UserHandOverCode""
                        , ""UserHandOver"".""Name"" AS ""UserHandOverName""
                        -- Người nhận
                        , ""UserReceive"".""Id"" AS ""UserId""
                        , ""EmployeeReceive"".""Code"" AS ""EmployeeCode""
                        , ""UserReceive"".""Name"" AS ""UserName""
                        -- Bộ phận
                        , ""SYSDPM"".""Id"" AS ""DepartmentId""
                        , ""SYSDPM"".""Name"" AS ""DepartmentName""
                        -- Lí do
                        , COALESCE(""SYSASTTF"".""Reason"", '') AS ""Reason""
                        -- Ngày tạo
                        , ""SYSASTTF"".""CreatedAt""
                    FROM
                        ""SYSASTTF""
                    INNER JOIN ""SYSASTI""
                        ON (
                            ""SYSASTTF"".""AssetImportId"" = ""SYSASTI"".""Id""
                            AND ""SYSASTI"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""SYSASTIDT""
                        ON (
                            ""SYSASTIDT"".""AssetImportId"" = ""SYSASTI"".""Id""
                            AND ""SYSASTIDT"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""SYSAST""
                        ON (
                            ""SYSAST"".""Id"" = ""SYSASTIDT"".""AssetId""
                            AND ""SYSAST"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""SYSDPM""
                        ON (
                            ""SYSASTI"".""DepartmentId"" = ""SYSDPM"".""Id""
                            AND ""SYSDPM"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""Users"" ""UserHandOver""
                        ON (
                            ""SYSASTTF"".""FromUser"" = ""UserHandOver"".""Id""
                            AND ""UserHandOver"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""Employees"" ""EmployeeHandOver""
                        ON (
                            ""UserHandOver"".""EmployeeId"" = ""EmployeeHandOver"".""Id""
                            AND ""EmployeeHandOver"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""Users"" ""UserReceive""
                        ON (
                            ""SYSASTTF"".""ToUser"" = ""UserReceive"".""Id""
                            AND ""UserReceive"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""Employees"" ""EmployeeReceive""
                        ON (
                            ""UserReceive"".""EmployeeId"" = ""EmployeeReceive"".""Id""
                            AND ""EmployeeReceive"".""DelFlag"" = FALSE
                        )
                    LEFT JOIN ""SYSBR""
                        ON (
                            ""SYSASTI"".""BranchId"" = ""SYSBR"".""Id""
                            AND ""SYSBR"".""DelFlag"" = FALSE
                        )
                    INNER JOIN ""SYSASTE""
                        ON (
                            ""SYSASTTF"".""AssetExportId"" = ""SYSASTE"".""Id""
                            AND ""SYSASTE"".""DelFlag"" = FALSE
                        )
                    LEFT JOIN ""SYSBR"" AS ""BranchExport""
                        ON (
                            ""BranchExport"".""Id"" = ""SYSASTE"".""BranchId""
                            AND ""BranchExport"".""DelFlag"" = FALSE
                        )
                    WHERE
                        ""SYSASTTF"".""DelFlag"" = FALSE
                        AND ""SYSASTTF"".""Type"" = @Type
                        -- AND ""SYSASTTF"".""IsAllocationToDepartment"" = @IsAllocationToDepartment
                        AND (
                            @Keyword IS NULL
                            OR @Keyword = ''
                            OR LOWER (unaccent (""SYSASTTF"".""Code"")) LIKE @Keyword
                            OR LOWER (unaccent (""SYSAST"".""Code"")) LIKE @Keyword
                            OR LOWER (unaccent (""SYSAST"".""Name"")) LIKE @Keyword
                        )
                        AND (
                            @Code IS NULL
                            OR @Code = ''
                            OR LOWER (unaccent (""SYSASTTF"".""Code"")) LIKE @Code
                        )
                        AND (
                            @DateSearch IS NULL
                            OR @DateSearch = ''
                            OR ""SYSASTI"".""ImportDate"" :: DATE = @DateSearch :: DATE
                        )
                        AND (
                            @UserId IS NULL
                            OR ""UserReceive"".""Id"" = @UserId
                        )
                        AND (
                            @UserHandOverId IS NULL
                            OR ""UserHandOver"".""Id"" = @UserHandOverId
                        )
                        AND (
                            @DepartmentId IS NULL
                            OR @DepartmentId = ''
                            OR ""SYSDPM"".""Id"" = @DepartmentId
                        )
                        AND (
                            @BranchId IS NULL
                            OR ""SYSBR"".""Id"" = @BranchId
                        )
                    GROUP BY
                        -- Mã bàn giao
                        ""SYSASTTF"".""Id""
                        , ""SYSASTTF"".""Code""
                        , ""SYSASTI"".""ImportDate""
                        -- Từ chi nhánh
                        , ""BranchExport"".""Id""
                        , ""BranchExport"".""Name""
                        -- Bàn giao về
                        , ""SYSBR"".""Id""
                        , ""SYSBR"".""Name""
                        -- Người bàn giao
                        , ""UserHandOver"".""Id""
                        , ""EmployeeHandOver"".""Code""
                        , ""UserHandOver"".""Name""
                        -- Người nhận
                        , ""UserReceive"".""Id""
                        , ""EmployeeReceive"".""Code""
                        , ""UserReceive"".""Name""
                        -- Bộ phận
                        , ""SYSDPM"".""Id""
                        , ""SYSDPM"".""Name""
                        -- Lí do
                        , COALESCE(""SYSASTTF"".""Reason"", '')
                        , ""SYSASTTF"".""CreatedAt""
                    ORDER BY
                        ""SYSASTTF"".""Id"" DESC
                ";
                string sqlCount = $@"
                    SELECT
                        COUNT(*)
                    FROM ({selectQuery}) AS c
                ";
                var param = new
                {
                    Keyword = ConvertSearchTerm(filter.Keyword),
                    Code = ConvertSearchTerm(filter.Code),
                    AssetId = filter.AssetId,
                    Type = 70,
                    DateSearch = filter.DateSearch,
                    UserId = filter.UserId,
                    UserHandOverId = filter.UserHandOverId,
                    DepartmentId = filter.DepartmentId,
                    BranchId = filter.BranchId,
                    // IsAllocationToDepartment = filter.IsAllocationToDepartment,
                };
                int count = await _connection.QueryFirstOrDefaultAsync<int>(sqlCount, param);
                Console.WriteLine(selectQuery);

                returnedData.Paging = new Paging(count, filter.CurrentPage, filter.PageSize);
                returnedData.Data = (List<AssetTransferSelectData>)await _connection.QueryAsync<AssetTransferSelectData>(selectQuery, param);
                _logger.LogInformation($"[{_className}][{method}] End");
                return returnedData;

            }
            catch (Exception ex)
            {
                _logger.LogError($"[{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
    }
}