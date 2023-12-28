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
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[{_className}][{method}] Start");
                ResponseInfo responseInfo = new();
                // 1. Mã chứng từ không trung lập
                var assetCheck = await _context.Assets.Where(x => !x.DelFlag
                    && x.FinancialCode == assetData.FinancialCode
                        && x.Code == assetData.Code
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
                    BranchId = users.Employee.BranchId,
                    DepartmentId = users.Employee.DepartmentId,
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
                            Type = (users.Employee.BranchId == 0) ? 1 : 2, // 1: kho tổng, 2: chi nhánh
                            UserId = assetData.UserId,
                            BranchId = users.Employee.BranchId,
                            DepartmentId = users.Employee.DepartmentId,
                            AssetHistories = new List<TblAssetHistory>
                            {
                                new()
                                {
                                    ActionName = "Tăng mới",
                                    BeginInventory = 0,
                                    QuantityChange = assetData.Quantity,
                                    EndQuantity = assetData.Quantity,
                                    ValueInventory= assetData.Quantity * assetData.OriginalPrice
                                }
                            }
                        }
                    }
                };
                _context.Assets.Add(assetDataSubmit);
                await _context.SaveChangesAsync();
                int assetId = assetDataSubmit.Id;
                string actionCode = await GenIdCodeAsync("TM", assetId, "SYSAST");
                assetDataSubmit.AssetStocks.First().AssetHistories.First().AcctionCode = actionCode;
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
                        AssetData entry;
                        if(!dictionary.TryGetValue(asset.AssetId, out entry))
                        {
                            entry = asset;
                            asset.AssetUserDetails = new List<AssetUserDetail>();
                            dictionary.Add(entry.AssetId, entry);
                        }
                        if(userDetail != null)
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
        public async Task<AssetData> GetDetailAsset(int assetId)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                AssetData assetDetail = new();
                assetDetail = await _context.Assets
                    .Where(x => !x.DelFlag && x.Id == assetId)
                    .Select(
                        x =>
                            new AssetData()
                            {
                                UserId = x.UserId.Value,
                                AssetId = x.Id,
                                Name = x.Name,
                                FinancialCode = x.FinancialCode,
                                Code = x.Code,
                                TypeId = x.AssetTypeId.Value,
                                TypeName = _context.AssetTypes.Where(astype => !astype.DelFlag && astype.Id == x.AssetTypeId).Select(astype => astype.Name).FirstOrDefault(),
                                Quantity = x.Quantity,
                                PurchasePrice = x.PurchasePrice,
                                BranchId = x.BranchId.Value,
                                BranchName = _context.Branches.Where(branch => !branch.DelFlag && branch.Id == x.BranchId).Select(branch => branch.Name).FirstOrDefault(),
                                DepartmentId = x.DepartmentId,
                                DepartmentName = _context.Departments.Where(dpm => !dpm.DelFlag && dpm.Id == x.DepartmentId).Select(dpm => dpm.Name).FirstOrDefault(),
                                UserName = _context.Users.Where(us => !us.DelFlag && us.Id == x.UserId).Select(us => us.Name).FirstOrDefault(),
                                DateBuy = x.DateBuy.Value,
                                GuaranteeTime = x.GuaranteeTime,
                                VendorName = x.Vendor,
                                ManufactureYear = x.ManufactureYear,
                                ManufacturerCode = x.ManufacturerCode,
                                Serial = x.Serial,
                                Country = x.Country,
                                ConditionApplyGuarantee = x.ConditionApplyGuarantee,
                                DepreciationDate = x.DepreciationDate,
                                OriginalPrice = x.OriginalPrice.Value,
                                DepreciatedValue = x.DepreciatedValue.Value,
                                DepreciatedMonth = x.DepreciatedMonth.Value,
                                DepreciatedMoney = x.DepreciatedMoney,
                                DepreciatedMoneyRemain = x.DepreciatedMoneyRemain,
                                DepreciatedMoneyByMonth = x.DepreciatedMoneyByMonth,
                                DepreciatedRate = x.DepreciatedRate,
                                QuantityAllocated = x.QuantityAllocated,
                                QuantityRemain = x.QuantityRemain,
                                UnitId = x.AssetUnitId,
                                UnitName = _context.AssetUnits.Where(un => !un.DelFlag && un.Id == x.AssetUnitId).Select(un => un.Name).FirstOrDefault(),
                                Note = x.Note,
                            }
                    ).FirstOrDefaultAsync();
                List<TblAssetStock> assetStock = await _context.AssetStocks
                    .Where(x => !x.DelFlag && x.AssetId == assetDetail.AssetId)
                    .ToListAsync();
                List<TblAssetImportDetail> assetImportDetail = await _context.AssetImportDetails
                    .Where(x => !x.DelFlag && x.AssetId == assetDetail.AssetId)
                    .ToListAsync();
                List<TblAssetExportDetail> assetExportDetail = await _context.AssetExportDetails
                    .Where(x => !x.DelFlag && x.AssetStock.AssetId == assetDetail.AssetId)
                    .ToListAsync();
                List<TblAssetHistory> assetHistory = await _context.AssetHistories
                    .Where(x => !x.DelFlag && x.AssetStockId == assetStock[0].Id)
                    .ToListAsync();
                if(assetStock.Count > 1 || assetImportDetail.Count > 1 || assetExportDetail.Count > 0 || assetHistory.Count > 1)
                {
                    // Tài sản không thể được cập nhật
                    assetDetail.IsUpdatable = false;
                }
                else
                {
                    // Tài sản có thể được cập nhật
                    assetDetail.IsUpdatable = true;
                }
                return assetDetail;
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
                ListAssetHistory returnedData = new ListAssetHistory();
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
        public async Task<ResponseInfo> UpdateAsset(AssetData assetData)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                ResponseInfo responseInfo = new();
                List<TblAssetStock> assetStock = await _context.AssetStocks
                    .Where(x => !x.DelFlag && x.AssetId == assetData.AssetId)
                    .ToListAsync();
                List<TblAssetImportDetail> assetImportDetail = await _context.AssetImportDetails
                    .Include(x => x.AssetImport)
                    .Where(x => !x.DelFlag && x.AssetId == assetData.AssetId)
                    .ToListAsync();
                List<TblAssetExportDetail> assetExportDetail = await _context.AssetExportDetails
                    .Include(x => x.AssetStock)
                    .Where(x => !x.DelFlag && x.AssetStock.AssetId == assetData.AssetId )
                    .ToListAsync();
                List<TblAssetHistory> assetHistory = await _context.AssetHistories
                    .Where(x => !x.DelFlag && x.AssetStockId == assetStock[0].Id)
                    .ToListAsync();
                if(assetStock.Count > 1 || assetImportDetail.Count > 0 || assetExportDetail.Count > 0 || assetHistory.Count > 1 ){
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.ASSET_QUANTITY_ALREADY_CHANGED;
                    return responseInfo;
                }
                // Check dataImport
                var assetDataOld = await _context.Assets
                    .Where(x => !x.DelFlag && x.Id == assetData.AssetId)
                    .FirstOrDefaultAsync();
                // 1. Kiểm tra tài sản có tồn tại không
                if (assetDataOld == null)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.ASSET_NOT_FOUND;
                    return responseInfo;
                }
                // 1. Mã chứng từ và mã tài sản không trùng lập
                var assetCheck = await _context.Assets.Where(x => !x.DelFlag
                    && x.FinancialCode == assetData.FinancialCode
                    && x.Code == assetData.Code
                    && x.Code != assetDataOld.Code
                    && x.Id != assetDataOld.Id
                ).FirstOrDefaultAsync();
                if (assetCheck != null)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.FINANCIAL_CODE_OR_ASSET_CODE_IS_EXISTED;
                    return responseInfo;
                }
                // 4. Người quản lí có tồn tại
                var users = await _context.Users.Where(x => !x.DelFlag && x.Id == assetData.UserId).FirstOrDefaultAsync();
                if (users == null)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.USER_NOT_FOUND;
                    return responseInfo;
                }
                var employee = await _context.Employees.Where(x => !x.DelFlag && x.Id == users.EmployeeId).FirstOrDefaultAsync();
                // 3. Phòng ban có tồn tại
                if (string.IsNullOrEmpty(employee.DepartmentId))
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.DEPARTMENT_NOT_FOUND;
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
                // Thông tin tài sản
                assetDataOld.Name = assetData.Name;
                assetDataOld.FinancialCode = assetData.FinancialCode;
                assetDataOld.Code = assetData.Code;
                assetDataOld.AssetTypeId = assetData.TypeId;
                // Thông tin mua hàng
                assetDataOld.AssetUnitId = assetData.UnitId;
                assetDataOld.BranchId = employee.BranchId;
                assetDataOld.DepartmentId = employee.DepartmentId;
                assetDataOld.UserId = assetData.UserId;
                assetDataOld.DateBuy = assetData.DateBuy;
                assetDataOld.Vendor = assetData.VendorName;
                assetDataOld.PurchasePrice = assetData.PurchasePrice;
                // cập nhật số lượng tài sản
                assetDataOld.Quantity = assetData.Quantity;
                assetDataOld.QuantityRemain = assetData.Quantity;
                assetStock[0].Quantity = assetData.Quantity;
                assetStock[0].QuantityRemain = assetData.Quantity;
                _logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                assetHistory[0].QuantityChange = assetData.Quantity;
                assetHistory[0].EndQuantity = assetData.Quantity;
                _logger.LogInformation("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!ASTHTR");
                // Thông tin kỹ thuật
                assetDataOld.ManufacturerCode = assetData.ManufacturerCode;
                assetDataOld.ManufactureYear = assetData.ManufactureYear;
                assetDataOld.Serial = assetData.Serial;
                assetDataOld.Country = assetData.Country;
                assetDataOld.GuaranteeTime = assetData.GuaranteeTime;
                assetDataOld.ConditionApplyGuarantee = assetData.ConditionApplyGuarantee;
                // Thông tin khấu hao
                assetDataOld.DepreciationDate = assetData.DepreciationDate;
                DateTime rightNow = DateTime.Now;
                DateTime? depreciationDate = assetData.DepreciationDate;
                int monthDiff = depreciationDate.HasValue ? rightNow.Subtract(depreciationDate.Value).Days/30 : 0;
                double depreciatedMoney;
                if (monthDiff <= 0)
                {
                    depreciatedMoney = 0;
                }
                else if (monthDiff > assetDataOld.DepreciatedMonth)
                {
                    depreciatedMoney = assetDataOld.DepreciatedValue.Value;
                }
                else
                {
                    depreciatedMoney = monthDiff * assetDataOld.DepreciatedMoneyByMonth;
                }
                assetDataOld.DepreciatedMoney = depreciatedMoney;
                assetDataOld.DepreciatedMoneyRemain = assetDataOld.DepreciatedValue.Value - depreciatedMoney;
                // Note
                assetDataOld.Note = assetData.Note;
            await _context.SaveChangesAsync();
            return responseInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        public async Task<ResponseInfo> DeleteAsset(int assetId)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                ResponseInfo responseInfo = new();
                // Check Id
                // Xóa asset
                var assetCheck = await _context.Assets.Where(x => !x.DelFlag
                    && x.Id == assetId
                ).FirstOrDefaultAsync();
                if (assetCheck == null)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.ASSET_NOT_FOUND;
                    return responseInfo;
                }
                assetCheck.DelFlag = true;
                // Xóa AssetStock
                var listAssetStocks = await _context.AssetStocks.Where(x => !x.DelFlag && x.AssetId == assetCheck.Id).ToListAsync();
                foreach (TblAssetStock assetStock in listAssetStocks)
                {
                    assetStock.DelFlag = true;
                }
                // Xoa lich su
                var listAssetHistory = await _context.AssetHistories.Where(x => !x.DelFlag && x.Id == assetCheck.Id).ToListAsync();
                foreach (var assetHistory in listAssetHistory)
                {
                    assetHistory.DelFlag = true;
                }
                // Xóa phiếu xuất
                var listExport = await _context.AssetExports.Where(x => !x.DelFlag && x.AssetExportDetails.Any(item => item.AssetStock.AssetId == assetCheck.Id)).ToArrayAsync();
                foreach (var export in listExport)
                {
                    export.DelFlag = true;
                }
                // Xóa chi tiết phiếu xuất
                var listExportDetail = await _context.AssetExportDetails.Where(x => !x.DelFlag && x.AssetStock.AssetId == assetCheck.Id).ToArrayAsync();
                foreach (var exportDetail in listExportDetail)
                {
                    exportDetail.DelFlag = true;
                }
                // Xóa phiếu nhập
                var listImport = await _context.AssetImports.Where(x => !x.DelFlag && x.AssetImportDetails.Any(item => item.AssetId == assetCheck.Id)).ToArrayAsync();
                foreach (var import in listImport)
                {
                    import.DelFlag = true;
                }
                // Xóa chi tiết phiếu nhập
                var listImportDetail = await _context.AssetImportDetails.Where(x => !x.DelFlag && x.AssetId == assetCheck.Id).ToArrayAsync();
                foreach (var importDetail in listImportDetail)
                {
                    importDetail.DelFlag = true;
                }
                await _context.SaveChangesAsync();
                return responseInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
    }
}