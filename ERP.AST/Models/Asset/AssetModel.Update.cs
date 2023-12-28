using ERP.Databases;
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
                assetHistory[0].QuantityChange = assetData.Quantity;
                assetHistory[0].EndQuantity = assetData.Quantity;
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
                _logger.LogInformation($"[][{_className}][{method}] End");
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