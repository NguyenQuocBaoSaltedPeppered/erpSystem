using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using ERP.AST.Enum;
using ERP.AST.Models.AssetImportAndExport.Schemas;
using ERP.Bases.Models;
using ERP.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TblAssetExport = ERP.Databases.Schemas.AssetExport;
using TblAssetExportDetail = ERP.Databases.Schemas.AssetExportDetail;
using TblAssetHistory = ERP.Databases.Schemas.AssetHistory;
using TblAssetStock = ERP.Databases.Schemas.AssetStock;

namespace ERP.AST.Models
{
    public partial class AssetImportAndExportModel
    {
        public async Task<ResponseInfo> CreateAssetExport(AssetExportData assetExportData)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                DateTime now = DateTime.Now;
                ResponseInfo responseInfo = new();
                string AssetActionCode = "";
                int AssetActionType;
                switch (assetExportData.Type)
                {
                    // Broken (Báo hỏng)
                    case 10:
                        AssetActionCode = AssetConstants.BROKEN_CODE;
                        AssetActionType = 10;
                        return await AssetExportIncrease(assetExportData, AssetActionCode, AssetActionType);
                    // Guarantee (Bảo hành sửa chữa)
                    case 20:
                        AssetActionCode = AssetConstants.GUARANTEE_CODE;
                        AssetActionType = 20;
                        return await AssetExportIncrease(assetExportData, AssetActionCode, AssetActionType);
                    // Allocation (Cấp phát)
                    case 40:
                        AssetActionCode = AssetConstants.ALLOCATION_CODE;
                        AssetActionType = 40;
                        return await AssetExportIncrease(assetExportData, AssetActionCode, AssetActionType);
                    // CANCEL (Báo huỷ)
                    case 50:
                        AssetActionCode = AssetConstants.CANCELLATION_CODE;
                        AssetActionType = 50;
                        return await AssetExportIncrease(assetExportData, AssetActionCode, AssetActionType);
                    // LOST (Báo mất)
                    case 60:
                        AssetActionCode = AssetConstants.LOST_CODE;
                        AssetActionType = 60;
                        return await AssetExportIncrease(assetExportData, AssetActionCode, AssetActionType);
                    default:
                        break;
                }
                _logger.LogInformation($"[][{_className}][{method}] End");
                return responseInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Type 10: Báo hỏng => ko trừ số lượng trong kho, chỉ trừ số lượng remain
        /// Type 20: Bảo hành, sữa chữa từ phiếu báo hỏng => trừ số lượng trong kho, không trừ sô lượng còn lại
        /// </summary>
        /// <param name="assetExportData"></param>
        /// <returns></returns>
        public async Task<ResponseInfo> AssetExportIncrease(AssetExportData assetExportData, string AssetActionCode, int AssetActionType)
        {
            IDbContextTransaction transaction = null;
            string method = GetActualAsyncMethodName();
            DbConnection connection = _context.GetConnection();
            try
            {
                DateTime now = DateTime.Now;
                ResponseInfo responseInfo = new();
                // 1. Tạo phiếu xuất, phiếu xuất chi tiết
                var employee = await _context.Employees
                    .Include(x => x.User)
                    .Where(x => !x.DelFlag && !x.User.DelFlag && x.User.Id == assetExportData.UserId)
                    .FirstOrDefaultAsync();
                TblAssetExport tblAssetExport = new TblAssetExport()
                {
                    ExportDate = assetExportData.ExportDate,
                    // Code = AssetActionCode,
                    Type = AssetActionType,
                    Reason = assetExportData.Reason,
                    Note = assetExportData.Note,
                    Status = assetExportData.Status,
                    UserId = assetExportData.UserId,
                    BranchId = employee.BranchId,
                    DepartmentId = employee.DepartmentId,
                    CompensationValue = assetExportData.CompensationValue,
                    ResponsibleId = assetExportData.ResponsibleId
                };
                // Tổng số lượng và giá nhập
                int quantityExport = 0;
                double totalMoneyImport = 0;
                List<TblAssetHistory> TblAssetHistorys = new List<TblAssetHistory>();
                foreach (var assetExportDetail in assetExportData.ListAssetExportDetails)
                {
                    int quantity = 0;
                    int quantityRemain = 0;
                    double totalMoney = 0;
                    TblAssetStock assetStock = new TblAssetStock();
                    assetStock = await _context.AssetStocks
                        .Include(x => x.Asset)
                        .Where(x => !x.DelFlag
                            && x.AssetId == assetExportDetail.AssetId
                            && x.UserId == assetExportData.UserId
                        )
                        .FirstOrDefaultAsync();
                    if (assetStock == null)
                    {
                        responseInfo.Code = CodeResponse.HAVE_ERROR;
                        responseInfo.MsgNo = MSG_NO.ASSET_STOCK_NOT_FOUND;
                        return responseInfo;
                    }
                    // Xử lý việc request cùng lúc
                    await WaitingGlobalLock(connection, assetStock.Id, "SYSASTST");
                    // 1. Tạo phiếu xuất kho Detail
                    tblAssetExport.AssetExportDetails.Add(new TblAssetExportDetail()
                    {
                        AssetStockId = assetStock.Id,
                        Quantity = assetExportDetail.Quantity,
                        Note = assetExportDetail.Note,
                        AssetAllocationId = assetExportDetail.AssetAllocationId,
                    });

                    // Cập nhật số lượng
                    quantity += assetExportDetail.Quantity.Value;
                    quantityRemain += assetExportDetail.Quantity.Value;
                    totalMoney += assetExportDetail.Quantity.Value * assetStock.Asset.OriginalPrice.Value;
                    // Số lượng và sô tiền phiếu xuất
                    quantityExport += quantity;
                    totalMoneyImport += totalMoney;
                    // !!!.... Xử lý bảo hành sữa chữa
                    // Báo hỏng thì ko trừ số lượng trong kho
                    if (assetExportData.Type == 10)
                    {
                        quantity = 0;
                        totalMoney = 0;
                        tblAssetExport.Type = assetExportData.Type;
                    }
                    // 2. Cập nhật số lượng tài sản trong kho
                    if (quantity > assetStock.QuantityRemain)
                    {
                        responseInfo.Code = CodeResponse.HAVE_ERROR;
                        responseInfo.MsgNo = MSG_NO.OUTPUT_QUANTITY_EXCEED_ASSET_IN_STOCK;
                        return responseInfo;
                    }
                    // 3. Cập nhật số lượng tài sản trong TblAsset
                    int quantityOld = assetStock.Asset.Quantity;
                    assetStock.Quantity -= quantity;
                    assetStock.QuantityRemain -= quantityRemain;
                    assetStock.Asset.Quantity -= quantity;
                    assetStock.Asset.QuantityRemain -= quantityRemain;
                    // assetStock.Asset.DepreciatedMoneyRemain -= totalMoney;
                    string actionName = "";
                    switch (assetExportData.Type)
                    {
                        case 10:
                            assetStock.QuantityBroken += assetExportDetail.Quantity.Value;
                            assetStock.Asset.QuantityBroken += assetExportDetail.Quantity.Value;
                            actionName = "Báo hỏng";
                            break;
                        case 20:
                            assetStock.QuantityGuarantee += assetExportDetail.Quantity.Value;
                            assetStock.Asset.QuantityGuarantee += assetExportDetail.Quantity.Value;
                            actionName = "Bảo hành, sửa chữa";
                            break;
                        case 50:
                            assetStock.QuantityCancel += assetExportDetail.Quantity.Value;
                            assetStock.Asset.QuantityCancel += assetExportDetail.Quantity.Value;
                            actionName = "Báo huỷ";
                            break;
                        case 60:
                            assetStock.QuantityLost += assetExportDetail.Quantity.Value;
                            assetStock.Asset.QuantityLost += assetExportDetail.Quantity.Value;
                            actionName = "Báo mất";
                            break;
                        default:
                            break;
                    }
                    // 4. Tạo phiếu lưu lịch sử trong khox
                    TblAssetHistorys.Add(new TblAssetHistory()
                    {
                        // AcctionCode = tblAssetExport.Code,
                        ActionName = actionName,
                        BeginInventory = quantityOld,
                        QuantityChange = assetExportDetail.Quantity.Value,
                        EndQuantity = (quantityOld - quantity),
                        ValueInventory = (quantityOld - quantity) * assetStock.Asset.OriginalPrice,
                        AssetStockId = assetStock.Id,
                        Note = assetExportDetail.Note,
                    });
                    // Xử lý việc request cùng lúc
                    await ReleaseGlobalLock(connection, assetStock.Id, "SYSASTST");
                    await _context.SaveChangesAsync();
                }
                tblAssetExport.TotalMoney = totalMoneyImport;
                tblAssetExport.Quantity = quantityExport;
                await _context.AssetExports.AddAsync(tblAssetExport);
                await _context.SaveChangesAsync();
                // Render code & lưu lại
                string importCode = await GenIdCodeAsync(AssetActionCode, tblAssetExport.Id, "SYSASTE");
                tblAssetExport.Code = importCode;
                foreach(var assetHistory in TblAssetHistorys)
                {
                    assetHistory.AcctionCode = importCode;
                }
                await _context.AssetHistories.AddRangeAsync(TblAssetHistorys);
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