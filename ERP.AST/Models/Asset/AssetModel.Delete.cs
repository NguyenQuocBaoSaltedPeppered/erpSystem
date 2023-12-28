using ERP.Databases;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TblAssetStock = ERP.Databases.Schemas.AssetStock;

namespace ERP.AST.Models
{
    public partial class AssetModel
    {
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