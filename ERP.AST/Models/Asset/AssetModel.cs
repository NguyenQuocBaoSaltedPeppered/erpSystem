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

namespace ERP.AST.Models
{
    public class AssetModel : CommonModel, IAssetModel
    {
        private readonly string _className = string.Empty;
        private readonly ILogger<AssetModel> _logger;

        public AssetModel(ILogger<AssetModel> logger, IServiceProvider provider) : base(provider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _className = GetType().Name;
        }
        public async Task<ResponseInfo> CreateAsset(AssetData assetData)
        {
            IDbContextTransaction transaction = null;
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[{_className}][{method}] Start");
                ResponseInfo responseInfo = new();
                // 1. Mã chứng từ không trung lập
                var assetCheck = await _context.Assets.Where(x => !x.DelFlag
                    && (
                        x.FinancialCode == assetData.FinancialCode
                        && x.Code == assetData.Code
                    )
                ).FirstOrDefaultAsync();
                if (assetCheck != null)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.FINANCIAL_CODE_OR_ASSET_CODE_IS_EXISTED;
                    return responseInfo;
                }
                // kiểm tra mã tài sản
                var checkCode = await _context.Assets
                    .Where(x => !x.DelFlag && x.Code == assetData.Code)
                    .FirstOrDefaultAsync();
                if (checkCode != null){
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.FINANCIAL_CODE_OR_ASSET_CODE_IS_EXISTED;
                    return responseInfo;
                }
                // người tạo có tồn tại
                var users = await _context.Users
                    .Include(x => x.Employee)
                    .Where(x => !x.DelFlag && x.Id == assetData.UserId).FirstOrDefaultAsync();
                if (users == null)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.USER_NOT_FOUND;
                    return responseInfo;
                }
                var type = await _context.AssetTypes.Where(x => !x.DelFlag && x.Id == assetData.TypeId).FirstOrDefaultAsync();
                if (type == null)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.TYPE_OF_ASSET_NOT_FOUND;
                    return responseInfo;
                }
                if (type.ParentId == null)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.INVALID_ASSET_TYPE_OR_GROUP;
                    return responseInfo;
                }
                var unit = await _context.AssetUnits.Where(x => !x.DelFlag && x.Id == assetData.UnitId).FirstOrDefaultAsync();
                if (unit == null)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.CALCULATION_UNIT_NOT_FOUND;
                    return responseInfo;
                }
                double depreciatedRate = Math.Round(100 / assetData.DepreciatedMonth, 2);
                double depreciatedMoneyByMonth = depreciatedRate * assetData.DepreciatedValue/100;
                DateTime rightNow = DateTime.Now;
                DateTime? depreciationDate = assetData.DepreciationDate;
                int monthDiff = depreciationDate.HasValue ? rightNow.Subtract(depreciationDate.Value).Days/30 : 0;
                double depreciatedMoneyRemain = assetData.DepreciatedValue;
                double depreciatedMoney;
                if (monthDiff <= 0)
                {
                    depreciatedMoney = 0;
                }
                else if (monthDiff > assetData.DepreciatedMonth)
                {
                    depreciatedMoney = assetData.DepreciatedValue;
                }
                else
                {
                    depreciatedMoney = monthDiff * depreciatedMoneyByMonth;
                }
                depreciatedMoneyRemain = assetData.DepreciatedValue - depreciatedMoney;
                 // Asset
                TblAsset assetDataSubmit = new()
                {
                    // Thông tin tài sản
                    Name = assetData.Name,
                    FinancialCode = assetData.FinancialCode,
                    Code = assetData.Code,
                    AssetTypeId = assetData.TypeId,
                    AssetUnitId = assetData.UnitId,
                    // Quantity setup
                    Quantity = assetData.Quantity,
                    QuantityAllocated = 0,
                    QuantityRemain = assetData.Quantity,
                    QuantityCancel = 0,
                    QuantityBroken = 0,
                    QuantityGuarantee = 0,
                    QuantityLost = 0,
                    // End Quantity setup
                    UserId = assetData.UserId,
                    BranchId = users.BranchId,
                    DepartmentId = users.DepartmentId,
                    Vendor = assetData.VendorName,
                    DateBuy = assetData.DateBuy,
                    PurchasePrice = assetData.PurchasePrice,
                    // Thông tin kỹ thuật
                    ManufacturerCode = assetData.ManufacturerCode,
                    Serial = assetData.Serial,
                    ManufactureYear = assetData.ManufactureYear,
                    Country = assetData.Country,
                    GuaranteeTime = assetData.GuaranteeTime,
                    ConditionApplyGuarantee = assetData.ConditionApplyGuarantee,
                    // Thông tin khấu hao
                    DepreciationDate = assetData.DepreciationDate,
                    OriginalPrice = assetData.OriginalPrice,
                    DepreciatedValue = assetData.DepreciatedValue,
                    DepreciatedMonth = assetData.DepreciatedMonth,
                    DepreciatedRate = depreciatedRate,
                    DepreciatedMoneyByMonth = depreciatedMoneyByMonth,
                    DepreciatedMoney = depreciatedMoney,
                    DepreciatedMoneyRemain = depreciatedMoneyRemain,
                    // Ghi chú
                    Note = assetData.Note,
                    AssetStocks = new List<TblAssetStock>
                    {
                        new() 
                        {
                            Quantity = assetData.Quantity,
                            QuantityAllocated = 0,
                            QuantityRemain = assetData.Quantity,
                            QuantityCancel = 0,
                            QuantityBroken = 0,
                            QuantityGuarantee = 0,
                            QuantityLost = 0,
                            Type = (users.BranchId == 0) ? 1 : 2, // 1: kho tổng, 2: chi nhánh
                            UserId = assetData.UserId,
                            BranchId = users.BranchId,
                            DepartmentId = users.DepartmentId,
                        }
                    }
                };
                _context.Assets.Add(assetDataSubmit);
                await _context.SaveChangesAsync();
                return responseInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<ListAssetData> GetListAssetData(AssetFilter assetFilter)
        {
            string method = GetActualAsyncMethodName();
            DbConnection _connection = _context.GetConnection();
            try
            {
                ListAssetData listAssetData = new();
                if (assetFilter == null)
                {
                    assetFilter = new AssetFilter();
                }
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
                        TypeAsset = assetFilter.TypeAsset,
                        BranchId = assetFilter.BranchId,
                        VendorId = assetFilter.VendorId,
                        UserId = assetFilter.UserId,
                        DepartmentId = assetFilter.DepartmentId
                    };
                    var result = await _connection.QueryMultipleAsync(sqlMain, param);
                    int totalRecord = await result.ReadFirstOrDefaultAsync<int>();
                    listAssetData.Data = (await result.ReadAsync<AssetData>()).ToList();

                    if(totalRecord > 0)
                    {
                        listAssetData.Paging = new Paging(totalRecord, assetFilter.CurrentPage, assetFilter.PageSize);
                    }                
                return listAssetData;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
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
    }
}