using Microsoft.EntityFrameworkCore;
using System.Data;
using ERP.AST.Models.Asset.Schemas;
using TblAssetStock = ERP.Databases.Schemas.AssetStock;
using TblAssetExportDetail = ERP.Databases.Schemas.AssetExportDetail;
using TblAssetImportDetail = ERP.Databases.Schemas.AssetImportDetail;
using TblAssetHistory = ERP.Databases.Schemas.AssetHistory;

namespace ERP.AST.Models
{
    public partial class AssetModel
    {
        public async Task<AssetData> GetDetailAsset(int assetId)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
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
                if(assetStock.Count > 1 || assetImportDetail.Count > 0 || assetExportDetail.Count > 0 || assetHistory.Count > 1)
                {
                    // Tài sản không thể được cập nhật
                    assetDetail.IsUpdatable = false;
                }
                else
                {
                    // Tài sản có thể được cập nhật
                    assetDetail.IsUpdatable = true;
                }
                _logger.LogInformation($"[][{_className}][{method}] End");
                return assetDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
    }
}