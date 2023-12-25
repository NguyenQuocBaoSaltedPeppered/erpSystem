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

namespace ERP.AST.Models
{
    public partial class AssetImportAndExportModel
    {
        public async Task<ResponseInfo> UpdateHandOver(int transferId, AssetHandOverData assetHandOverData)
        {
            string method = GetActualAsyncMethodName();
            // Cập nhật lại ngày (TransferDate) và Lý do (Reason)
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                ResponseInfo responseInfo = new();
                TblAssetTransfer selectedData = await _context.AssetTransfers
                    .Include(x => x.AssetExport)
                    .Include(x => x.AssetImport)
                    .Include(x => x.AssetExport.AssetExportDetails)
                    .Include(x => x.AssetImport.AssetImportDetails)
                    .Where(x => !x.DelFlag && x.Id == transferId)
                    .FirstOrDefaultAsync();
                if(selectedData == null)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.TRANSFER_ORDER_IS_NOT_EXISTS;
                    return responseInfo;
                }
                //Phiếu nhập
                TblAssetImport tblAssetImport = await _context.AssetImports
                        .Where(x =>!x.DelFlag && x.Id == selectedData.AssetImportId)
                        .FirstOrDefaultAsync();
                //Phiếu xuất
                TblAssetExport tblAssetExport = await _context.AssetExports
                .Where(x =>!x.DelFlag && x.Id == selectedData.AssetExportId)
                .FirstOrDefaultAsync();

                selectedData.Reason = assetHandOverData.Reason;
                selectedData.AssetExport.ExportDate = assetHandOverData.TransferDate;
                selectedData.AssetImport.ImportDate = assetHandOverData.TransferDate;
                foreach(var tblAssetHandOverDetail in assetHandOverData?.ListAssetHandOverDetailData){
                    TblAsset assetData = await _context.Assets.Where(x => !x.DelFlag && x.Id == tblAssetHandOverDetail.AssetId).FirstOrDefaultAsync();
                    TblAssetImportDetail assetImportDetail = await _context.AssetImportDetails
                        .Include(x => x.AssetImport)
                        .Where(x => !x.DelFlag && x.AssetImport.Id == selectedData.AssetImportId && x.AssetId == tblAssetHandOverDetail.AssetId)
                        .FirstOrDefaultAsync();
                    TblAssetExportDetail assetExportDetail = await _context.AssetExportDetails
                        .Include(x => x.AssetStock)
                        .Where(x => !x.DelFlag && x.AssetExportId == selectedData.AssetExportId && x.AssetStock.AssetId == tblAssetHandOverDetail.AssetId && x.AssetStock.UserId == assetHandOverData.UserFromId)
                        .FirstOrDefaultAsync();
                    //Kho xuất (FromBranch)
                    TblAssetStock tblAssetStockExport = await _context.AssetStocks
                        .Include(x => x.Asset)
                        .Where(x => !x.DelFlag
                            && x.Id == assetExportDetail.AssetStockId
                        ).FirstOrDefaultAsync();
                    //Kho nhập (ToBranch)
                    TblAssetStock tblAssetStockImport = await _context.AssetStocks
                        .Include(x => x.Asset)
                        .Where(x => !x.DelFlag
                            && x.UserId == tblAssetImport.UserId
                            && x.AssetId == tblAssetHandOverDetail.AssetId
                        ).FirstOrDefaultAsync();
                    if( tblAssetStockExport == null )
                    {
                        responseInfo.Code = CodeResponse.HAVE_ERROR;
                        responseInfo.MsgNo = MSG_NO.ASSET_STOCK_NOT_FOUND;
                        return responseInfo;
                    }
                    // Nếu chuyển đi từ stock main: Tăng số lượng bàn giao table Asset
                    if(tblAssetStockExport.Type == 1)
                    {
                        tblAssetStockExport.Asset.QuantityAllocated = tblAssetStockExport.Asset.QuantityAllocated -  assetExportDetail.Quantity.Value + tblAssetHandOverDetail.Quantity;
                    }
                    // Nếu chuyển vào stock main: Giảm số lượng bàn giao table Asset
                    if(tblAssetStockImport.Type == 1)
                    {
                        tblAssetStockImport.Asset.QuantityAllocated -= tblAssetHandOverDetail.Quantity;
                    }
                    // Cập nhật lại số lượng, số lượng luân chuyển đi,số lượng còn lại trong kho xuất
                    tblAssetStockExport.Quantity = tblAssetStockExport.Quantity +  assetExportDetail.Quantity.Value - tblAssetHandOverDetail.Quantity;
                    tblAssetStockExport.QuantityRemain = tblAssetStockExport.QuantityRemain + assetExportDetail.Quantity.Value - tblAssetHandOverDetail.Quantity;
                    // Cập nhật lại số lượng được chuyển đến, số lượng và số lượng còn lại trong kho nhập
                    tblAssetStockImport.Quantity = tblAssetStockImport.Quantity -  assetImportDetail.Quantity.Value + tblAssetHandOverDetail.Quantity;
                    tblAssetStockImport.QuantityAllocated = tblAssetStockImport.QuantityAllocated - assetImportDetail.Quantity.Value + tblAssetHandOverDetail.Quantity;
                    tblAssetStockImport.QuantityRemain = tblAssetStockImport.QuantityRemain - assetImportDetail.Quantity.Value + tblAssetHandOverDetail.Quantity;

                    //cập nhật số lượng bàn giao
                    tblAssetImport.Quantity =  tblAssetImport.Quantity - assetImportDetail.Quantity + tblAssetHandOverDetail.Quantity;
                    tblAssetExport.Quantity = tblAssetExport.Quantity - assetExportDetail.Quantity.Value + tblAssetHandOverDetail.Quantity;
                    assetImportDetail.Quantity = tblAssetHandOverDetail.Quantity;
                    assetExportDetail.Quantity = tblAssetHandOverDetail.Quantity;
                }
                await _context.SaveChangesAsync();
                _logger.LogInformation($"[][{_className}][{method}] End");
                responseInfo.Data.Add("Id", selectedData.Id.ToString());
                responseInfo.Data.Add("Code", selectedData.Code);
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