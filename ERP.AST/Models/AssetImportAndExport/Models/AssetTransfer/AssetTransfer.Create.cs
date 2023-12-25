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
using TblAssetHistory = ERP.Databases.Schemas.AssetHistory;
using TblAsset = ERP.Databases.Schemas.Asset;

namespace ERP.AST.Models
{
    public partial class AssetImportAndExportModel
    {
        /// <summary>
        /// Các bước thực hiện quá trình chuyển kho
        /// B1: Tạo phiếu chuyển kho, xuất kho, phiếu nhập kho
        /// B2: Kiểm tra số lượng tài sản còn lại trong kho lớn hơn số lượng đã cấp phát
        /// B3: Tạo chi tiết phiếu chuyển, phiếu xuất
        /// B4: Cập nhật lại số lượng, số lượng đã chuyển, số lượng còn lại đã chuyển trong kho xuất
        /// B5: Cập nhật lại số lượng được chuyển đến, số lượng và số lượng còn lại trong kho nhập
        /// B6: Cập nhật lại số lượng phiếu xuất kho, nhập kho
        /// B7: Lưu lịch sử xuất - nhập kho
        /// </summary>
        /// <param name="assetHandOverData"></param>
        /// <returns></returns>
        /// <inheritdoc cref="IAssetImportAndExportModel.CreateAssetHandOver(AssetHandOverData)"/>
        public async Task<ResponseInfo> CreateAssetHandOver(AssetHandOverData assetHandOverData)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                DbConnection connection = _context.GetConnection();
                ResponseInfo responseInfo = new ResponseInfo();
                int transferAssetCount = assetHandOverData.ListAssetHandOverDetailData.Count();
                if (transferAssetCount == 0)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.NO_ASSET_FOR_HANDOVER;
                    return responseInfo;
                }
                // B1: Tạo phiếu chuyển kho, xuất kho, phiếu nhập kho
                // 1.1: Phiếu chuyển kho
                TblAssetTransfer tblAssetTransfer = new()
                {
                    Reason = assetHandOverData.Reason,
                    FromBranch = assetHandOverData.FromBranch,
                    ToBranch = assetHandOverData.ToBranch,
                    // Luân chuyển type = 70
                    Type = 70,
                    IsAllocationToDepartment = assetHandOverData.IsAllocationToDepartment
                };
                // Lấy deparmentId, branchId người bàn giao
                UserInfoData userExportInfo = await _context.Users
                    .Include(x => x.Employee)
                    .Where(x => !x.DelFlag && x.Id == assetHandOverData.UserFromId)
                    .Select(x => new UserInfoData()
                    {
                        UserId = x.Id,
                        BranchId = x.Employee.BranchId,
                        DepartmentId = x.Employee.DepartmentId
                    })
                    .FirstOrDefaultAsync();
                // Lấy deparmentId, branchId người được bàn giao
                UserInfoData userImportInfo = await _context.Users
                    .Include(x => x.Employee)
                    .Where(x => !x.DelFlag && x.Id == assetHandOverData.UserToId)
                    .Select(x => new UserInfoData()
                    {
                        UserId = x.Id,
                        BranchId = x.Employee.BranchId,
                        DepartmentId = x.Employee.DepartmentId
                    })
                    .FirstOrDefaultAsync();
                if(userImportInfo == null)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.EMPLOYEE_ID_EXISTING;
                    return responseInfo;
                }
                if(string.IsNullOrEmpty(userImportInfo.DepartmentId))
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.USER_IMPORT_DEPARTMENT_NOT_FOUND;
                    return responseInfo;
                }
                if(userImportInfo.BranchId == null)
                {
                    responseInfo.Code = CodeResponse.HAVE_ERROR;
                    responseInfo.MsgNo = MSG_NO.USER_IMPORT_BRANCH_NOT_FOUND;
                    return responseInfo;
                }
                // Update userFrom, userTo
                tblAssetTransfer.FromUser = userExportInfo.UserId;
                tblAssetTransfer.ToUser = userImportInfo.UserId;
                // 1.2: Phiếu xuất kho
                tblAssetTransfer.AssetExport = new TblAssetExport()
                {
                    ExportDate = assetHandOverData.TransferDate,
                    BranchId = userExportInfo.BranchId,
                    UserId = userExportInfo.UserId,
                    DepartmentId = userExportInfo.DepartmentId,
                    // Luân chuyển type = 70
                    Type = 70,
                    Status = assetHandOverData.Status,
                    Quantity = 0,
                };
                // 1.3: Phiếu nhập kho
                tblAssetTransfer.AssetImport = new TblAssetImport()
                {
                    ImportDate = assetHandOverData.TransferDate,
                    BranchId = userImportInfo.BranchId,
                    DepartmentId = userImportInfo.DepartmentId,
                    UserId = userImportInfo.UserId,
                    // Luân chuyển type = 70
                    Type = 70,
                    Status = assetHandOverData.Status,
                    Quantity = 0,
                };
                // Danh sách kho mới ( Nếu chuyển sang kho mới)
                List<TblAssetStock> listAssetStockNew = new();
                // Lịch sử nhập kho mới
                List<TblAssetHistory> listAssetHistory = new List<TblAssetHistory>();
                foreach(var assetHandOverDetailData in assetHandOverData?.ListAssetHandOverDetailData)
                {
                    // B2: Kiểm tra số lượng tài sản còn lại trong kho lớn hơn số lượng đã cấp phát
                    // Kho xuất (FromBranch)
                    TblAssetStock tblAssetStockExport = await _context.AssetStocks
                        .Include(x => x.Asset)
                        .Where(x => !x.DelFlag
                            && x.UserId == assetHandOverData.UserFromId
                            && x.AssetId == assetHandOverDetailData.AssetId
                        ).FirstOrDefaultAsync();
                    // Kho nhập (ToBranch)
                    TblAssetStock tblAssetStockImport = await _context.AssetStocks
                        .Include(x => x.Asset)
                        .Where(x => !x.DelFlag
                            && x.UserId == assetHandOverData.UserToId
                            && x.AssetId == assetHandOverDetailData.AssetId
                        ).FirstOrDefaultAsync();
                    if( tblAssetStockExport == null )
                    {
                        responseInfo.Code = CodeResponse.HAVE_ERROR;
                        responseInfo.MsgNo = MSG_NO.ASSET_STOCK_NOT_FOUND;
                        return responseInfo;
                    }
                    // Tài sản
                    TblAsset assetData = await _context.Assets.Where(x => !x.DelFlag && x.Id == assetHandOverDetailData.AssetId).FirstOrDefaultAsync();
                    // Tăng số lần chuyển tài sản
                    assetData.TransferCount++;
                    // Xử lý việc request cùng lúc
                    await WaitingGlobalLock(connection, tblAssetStockExport.Id, "SYSASTST");
                    // B3: Tạo chi tiết phiếu chuyển, phiếu xuất
                    // B3.1: Tạo phiếu xuất kho, chi tiết phiếu xuất kho
                    tblAssetTransfer.AssetExport.AssetExportDetails.Add(
                        new TblAssetExportDetail()
                        {
                            AssetStockId = tblAssetStockExport.Id,
                            Quantity = assetHandOverDetailData.Quantity,
                            Note = assetHandOverDetailData.Note,
                        }
                    );
                    // B3.2: Tạo phiếu nhập kho, chi tiết phiếu nhập kho
                    tblAssetTransfer.AssetImport.AssetImportDetails.Add(
                    new TblAssetImportDetail()
                    {
                        AssetId = assetHandOverDetailData.AssetId,
                        Quantity = assetHandOverDetailData.Quantity,
                        Note = assetHandOverDetailData.Note,
                    });
                    // B4: Cập nhật lại số lượng, số lượng luân chuyển đi,số lượng còn lại trong kho xuất
                    tblAssetStockExport.Quantity -= assetHandOverDetailData.Quantity;
                    tblAssetStockExport.QuantityRemain -= assetHandOverDetailData.Quantity;
                    // B5: Cập nhật lại số lượng được chuyển đến, số lượng và số lượng còn lại trong kho nhập
                    if(tblAssetStockImport == null)
                    {
                        tblAssetStockImport = new TblAssetStock()
                        {
                            Quantity = assetHandOverDetailData.Quantity,
                            QuantityAllocated = assetHandOverDetailData.Quantity,
                            QuantityBroken = 0,
                            QuantityGuarantee = 0,
                            QuantityCancel = 0,
                            QuantityLost = 0,
                            QuantityRemain = assetHandOverDetailData.Quantity,
                            BranchId = userImportInfo.BranchId,
                            // Loại kho luân chuyển type = 2
                            Type = 2,
                            DepartmentId = userImportInfo.DepartmentId,
                            UserId = assetHandOverData.UserToId,
                            AssetId = assetHandOverDetailData.AssetId,
                        };
                        listAssetStockNew.Add(tblAssetStockImport);
                    } else {
                        tblAssetStockImport.Quantity += assetHandOverDetailData.Quantity;
                        tblAssetStockImport.QuantityRemain += assetHandOverDetailData.Quantity;
                        tblAssetStockImport.QuantityAllocated += assetHandOverDetailData.Quantity;
                    }
                    // Nếu chuyển đi từ stock main: Tăng số lượng bàn giao table Asset
                    if(tblAssetStockExport.Type == 1)
                    {
                        tblAssetStockExport.Asset.QuantityAllocated +=  assetHandOverDetailData.Quantity;
                    }
                    // Nếu chuyển vào stock main: Giảm số lượng bàn giao table Asset
                    if(tblAssetStockImport.Type == 1)
                    {
                        tblAssetStockImport.Asset.QuantityAllocated -=  tblAssetStockImport.Quantity;
                    }
                    // B6: Cập nhật lại số lượng phiếu xuất kho, nhập kho
                    // 6.1: Số lượng nhập kho
                    tblAssetTransfer.AssetImport.Quantity += assetHandOverDetailData.Quantity;
                    // 6.2: Số lượng xuất kho
                    tblAssetTransfer.AssetExport.Quantity += assetHandOverDetailData.Quantity;
                    await _context.SaveChangesAsync();
                    // Xử lý việc request cùng lúc
                    await ReleaseGlobalLock(connection, tblAssetStockExport.Id, "SYSASTST");
                }
                await _context.AssetStocks.AddRangeAsync(listAssetStockNew);
                await _context.AssetTransfers.AddAsync(tblAssetTransfer);
                await _context.SaveChangesAsync();
                // Render code & lưu lại
                // Code transfer
                string transferCode = await GenIdCodeAsync(AssetConstants.ASSET_TRANSFER, tblAssetTransfer.Id, "SYSASTTF");
                // Code Import
                string importCode = await GenIdCodeAsync(AssetConstants.ASSET_HAND_OVER_IMPORT, tblAssetTransfer.AssetImport.Id, "SYSASTI");
                // Code Export
                string exportCode = await GenIdCodeAsync(AssetConstants.ASSET_HAND_OVER_EXPORT, tblAssetTransfer.AssetExport.Id, "SYSASTE");
                // Update code
                tblAssetTransfer.Code = transferCode;
                tblAssetTransfer.AssetImport.Code = importCode;
                tblAssetTransfer.AssetExport.Code = exportCode;
                foreach(var assetHistory in listAssetHistory)
                {
                    assetHistory.AcctionCode = transferCode;
                }
                foreach(var assetStockNew in listAssetStockNew)
                {
                    foreach(var assetHistory in assetStockNew?.AssetHistories)
                    {
                        assetHistory.AcctionCode = transferCode;
                    }
                }
                await _context.SaveChangesAsync();
                responseInfo.Data.Add("Id", tblAssetTransfer.Id.ToString());
                responseInfo.Data.Add("Code", tblAssetTransfer.Code);
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