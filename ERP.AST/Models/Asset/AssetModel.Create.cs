using ERP.Databases;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ERP.AST.Models.Asset.Schemas;
using TblAsset = ERP.Databases.Schemas.Asset;
using TblAssetStock = ERP.Databases.Schemas.AssetStock;
using TblAssetHistory = ERP.Databases.Schemas.AssetHistory;

namespace ERP.AST.Models
{
    public partial class AssetModel
    {
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
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
    }
}