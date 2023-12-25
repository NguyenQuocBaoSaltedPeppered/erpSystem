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
using TblAssetImport = ERP.Databases.Schemas.AssetImport;
using TblAssetImportDetail = ERP.Databases.Schemas.AssetImportDetail;
using TblAssetHistory = ERP.Databases.Schemas.AssetHistory;
using TblAssetStock = ERP.Databases.Schemas.AssetStock;

namespace ERP.AST.Models
{
    public partial class AssetImportAndExportModel
    {
        public async Task<ResponseInfo> CreateAssetImport(AssetImportData assetImportData)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                ResponseInfo responseInfo = new ResponseInfo();
                switch(assetImportData.Type)
                {
                    // Báo tăng
                    case 10:
                        return await AssetImportIncrease(assetImportData);
                    // Thu hồi
                    case 30:
                        return await AssetImportRecover(assetImportData);
                    // Cấp phát
                    case 40:
                        return await AssetImportAllocation(assetImportData);
                    default:
                        break;
                }
                return responseInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Type 10: Báo tăng
        /// </summary>
        /// <param name="assetImportData"></param>
        /// <returns></returns>
        public async Task<ResponseInfo> AssetImportIncrease(AssetImportData assetImportData)
        {
            string method = GetActualAsyncMethodName();
            DbConnection connection = _context.GetConnection();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                DateTime now = DateTime.Now;
                ResponseInfo responseInfo = new ResponseInfo();
                // AssetImport - AssetImportDetail
                TblAssetImport tblAssetImport = new()
                {
                    ImportDate = assetImportData.ImportDate,
                    // Code = AssetConstants.INCREASE_CODE + now.ToString("ddHHmmssff"),
                    Type = 10,
                    Reason = assetImportData.Reason,
                    Note = assetImportData.Note,
                };
                // Tổng số lượng và giá nhập
                int quantity = 0;
                double totalMoney = 0;
                List<TblAssetHistory> listTblAssetHistory = new List<TblAssetHistory>();
                foreach (var assetImportDetail in assetImportData.ListAssetImportDetails)
                {
                    // Cập nhật AssetStock
                    var assetStock = await _context.AssetStocks
                        .Include(x => x.Asset)
                        .Where(x => !x.DelFlag && x.Id == assetImportData.StockId)
                        .FirstOrDefaultAsync();
                    // Xử lý việc request cùng lúc
                    await WaitingGlobalLock(connection, assetStock.Id, "SYSASTST");
                    int quantityOld = assetStock.Asset.Quantity;
                    assetStock.Quantity += assetImportDetail.Quantity.Value;
                    assetStock.QuantityRemain += assetImportDetail.Quantity.Value;
                    // Cập nhật số lượng và giá nhập
                    quantity += assetImportDetail.Quantity.Value;
                    totalMoney += assetImportDetail.Quantity.Value * assetStock.Asset.OriginalPrice.Value;
                    // Cập nhật Asset
                    assetStock.Asset.Quantity += assetImportDetail.Quantity.Value;
                    assetStock.Asset.QuantityRemain += assetImportDetail.Quantity.Value;
                    //Them lich su
                    TblAssetHistory assetHistory = new()
                    {
                        // AcctionCode = tblAssetImport.Code,
                        ActionName = "Báo tăng",
                        BeginInventory = quantityOld,
                        QuantityChange = assetImportDetail.Quantity.Value,
                        EndQuantity = quantityOld + assetImportDetail.Quantity.Value,
                        ValueInventory = (quantityOld + assetImportDetail.Quantity.Value) * assetStock.Asset.OriginalPrice,
                        AssetStockId = assetStock.Id,
                        Note = assetImportDetail.Note,
                    };
                    listTblAssetHistory.Add(assetHistory);
                    // Xử lý việc request cùng lúc
                    await ReleaseGlobalLock(connection, assetStock.Id, "SYSASTST");
                    // Lưu lại thông tin cập nhật
                    await _context.SaveChangesAsync();
                    tblAssetImport.BranchId = assetStock.BranchId;
                    tblAssetImport.DepartmentId = assetStock.DepartmentId;
                    tblAssetImport.UserId = assetStock.UserId;
                    tblAssetImport.AssetImportDetails.Add(
                        new TblAssetImportDetail()
                        {
                            AssetId = assetStock.AssetId,
                            Quantity = assetImportDetail.Quantity,
                            Note = assetImportDetail.Note
                        }
                    );
                }
                tblAssetImport.TotalMoney = totalMoney;
                tblAssetImport.Quantity = quantity;
                await _context.AssetImports.AddAsync(tblAssetImport);
                await _context.SaveChangesAsync();
                // Render code & lưu lại
                string importCode = await GenIdCodeAsync(AssetConstants.INCREASE_CODE, tblAssetImport.Id, "SYSASTI");
                tblAssetImport.Code = importCode;
                foreach(var assetHistory in listTblAssetHistory)
                {
                    assetHistory.AcctionCode = importCode;
                }
                await _context.AssetHistories.AddRangeAsync(listTblAssetHistory);
                await _context.SaveChangesAsync();
                responseInfo.Data.Add("Id", tblAssetImport.Id.ToString());
                responseInfo.Data.Add("Code", tblAssetImport.Code);
                return responseInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Type 30: Nhập thu hồi
        /// </summary>
        /// <param name="assetImportData"></param>
        /// <returns></returns>
        public async Task<ResponseInfo> AssetImportRecover(AssetImportData assetImportData)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                DateTime now = DateTime.Now;
                ResponseInfo responseInfo = new ResponseInfo();
                // AssetImport - AssetImportDetail

                TblAssetImport tblAssetImport = new TblAssetImport()
                {
                    ImportDate = assetImportData.ImportDate,
                    BranchId = assetImportData.BranchId,
                    DepartmentId = assetImportData.DepartmentId,
                    UserId = assetImportData.UserId,
                    Code = AssetConstants.RECALL_CODE + now.ToString("ddHHmmssff"),
                    Type = 30,
                    Reason = assetImportData.Reason,
                    Note = assetImportData.Note,
                };

                // Tổng số lượng và giá nhập
                int quantity = 0;
                double totalMoney = 0;

                foreach (var assetImportDetail in assetImportData.ListAssetImportDetails)
                {
                    // Cập nhật lại StockMain của từng tài sản
                    var assetStock = await _context.AssetStocks
                        .Include(x => x.Asset)
                        .Where(x => !x.DelFlag && x.AssetId == assetImportDetail.AssetId
                            && x.BranchId == assetImportData.BranchId
                            && x.DepartmentId == assetImportData.DepartmentId
                            && x.UserId == assetImportData.UserId
                        )
                        .FirstOrDefaultAsync();
                    assetStock.Quantity += assetImportDetail.Quantity.Value;
                    assetStock.QuantityRemain += assetImportDetail.Quantity.Value;
                    // Cập nhật số lượng và giá nhập
                    quantity += assetImportDetail.Quantity.Value;
                    totalMoney += assetImportDetail.Quantity.Value * assetStock.Asset.OriginalPrice.Value;
                    // Cập nhật Asset
                    assetStock.Asset.Quantity += assetImportDetail.Quantity.Value;
                    assetStock.Asset.QuantityRemain += assetImportDetail.Quantity.Value;
                    //Them lich su
                    TblAssetHistory assetHistory = new()
                    {
                        AcctionCode = tblAssetImport.Code,
                        ActionName = "Thu hồi",
                        BeginInventory = 0,
                        QuantityChange = assetImportDetail.Quantity.Value,
                        EndQuantity = assetImportDetail.Quantity.Value,
                        ValueInventory = assetImportDetail.Quantity * assetStock.Asset.OriginalPrice,
                        AssetStockId = assetStock.Id
                    };
                    _context.AssetHistories.Add(assetHistory);
                    // Lưu lại thông tin cập nhật
                    await _context.SaveChangesAsync();
                    tblAssetImport.BranchId = assetStock.BranchId;
                    tblAssetImport.DepartmentId = assetStock.DepartmentId;
                    tblAssetImport.UserId = assetStock.UserId;
                    tblAssetImport.AssetImportDetails.Add(
                        new TblAssetImportDetail()
                        {
                            AssetId = assetStock.AssetId,
                            Quantity = assetImportDetail.Quantity,
                            Note = assetImportDetail.Note
                        }
                    );
                }
                tblAssetImport.TotalMoney = totalMoney;
                tblAssetImport.Quantity = quantity;
                _context.AssetImports.Add(tblAssetImport);
                await _context.SaveChangesAsync();
                responseInfo.Data.Add("Id", tblAssetImport.Id.ToString());
                responseInfo.Data.Add("Code", tblAssetImport.Code);
                return responseInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Type 40: Nhập cấp phát
        /// </summary>
        /// <param name="assetImportData"></param>
        /// <returns></returns>
        public async Task<ResponseInfo> AssetImportAllocation(AssetImportData assetImportData)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                DateTime now = DateTime.Now;
                ResponseInfo responseInfo = new();
                // AssetImport - AssetImportDetail

                TblAssetImport tblAssetImport = new()
                {
                    ImportDate = assetImportData.ImportDate,
                    BranchId = assetImportData.BranchId,
                    DepartmentId = assetImportData.DepartmentId,
                    UserId = assetImportData.UserId,
                    Code = AssetConstants.ALLOCATION_CODE + now.ToString("ddHHmmssff"),
                    Type = 40,
                    Reason = assetImportData.Reason,
                    Note = assetImportData.Note,
                };

                // Tổng số lượng và giá nhập
                int quantity = 0;
                double totalMoney = 0;

                foreach (var assetImportDetail in assetImportData.ListAssetImportDetails)
                {
                    // Cập nhật lại StockMain của từng tài sản
                    var assetStock = await _context.AssetStocks
                        .Include(x => x.Asset)
                        .Where(x => !x.DelFlag && x.AssetId == assetImportDetail.AssetId
                            && x.BranchId == assetImportData.BranchId
                            && x.DepartmentId == assetImportData.DepartmentId
                            && x.UserId == assetImportData.UserId
                        )
                        .FirstOrDefaultAsync();
                    // Nếu đã tồn tại trong kho
                    if(assetStock != null)
                    {
                        assetStock.Quantity += assetImportDetail.Quantity.Value;
                        assetStock.QuantityRemain += assetImportDetail.Quantity.Value;
                        // Cập nhật số lượng và giá nhập
                        quantity += assetImportDetail.Quantity.Value;
                        totalMoney += assetImportDetail.Quantity.Value * assetStock.Asset.OriginalPrice.Value;
                        // Cập nhật Asset
                        assetStock.Asset.Quantity += assetImportDetail.Quantity.Value;
                        assetStock.Asset.QuantityRemain += assetImportDetail.Quantity.Value;
                        //Them lich su
                        TblAssetHistory assetHistory = new()
                        {
                            AcctionCode = tblAssetImport.Code,
                            ActionName = "Cấp phát",
                            BeginInventory = 0,
                            QuantityChange = assetImportDetail.Quantity.Value,
                            EndQuantity = assetImportDetail.Quantity.Value,
                            ValueInventory = assetImportDetail.Quantity * assetStock.Asset.OriginalPrice,
                            AssetStockId = assetStock.Id
                        };
                        _context.AssetHistories.Add(assetHistory);
                        // Lưu lại thông tin cập nhật
                        await _context.SaveChangesAsync();
                    }
                    // Nếu chưa có trong kho
                    else
                    {
                        var assetData = await _context.Assets.Where(x => !x.DelFlag && x.Id == assetImportDetail.AssetId).FirstOrDefaultAsync();
                        TblAssetStock assetStockNew = new()
                        {
                            Quantity = assetImportDetail.Quantity.Value,
                            QuantityAllocated = 0,
                            QuantityRemain = assetImportDetail.Quantity.Value,
                            BranchId = assetImportData.BranchId,
                            // Loại tài sản từ kho được luân chuyển
                            Type = 2,
                            DepartmentId = assetImportData.DepartmentId,
                            UserId = assetImportData.UserId,
                            AssetId = assetData.Id,
                            //AssetHistorie
                            AssetHistories = new List<TblAssetHistory>{
                                new TblAssetHistory()
                                {
                                    AcctionCode = AssetConstants.ALLOCATION_CODE + now.ToString("ddHHmmssff"),
                                    ActionName = "Cấp phát",
                                    BeginInventory = 0,
                                    QuantityChange = assetImportDetail.Quantity.Value,
                                    EndQuantity = assetImportDetail.Quantity.Value,
                                    ValueInventory = assetImportDetail.Quantity * assetData.OriginalPrice,
                                }
                            }
                        };
                        _context.AssetStocks.Add(assetStockNew);
                        await _context.SaveChangesAsync();
                    }
                    tblAssetImport.BranchId = assetStock.BranchId;
                    tblAssetImport.DepartmentId = assetStock.DepartmentId;
                    tblAssetImport.UserId = assetStock.UserId;
                    tblAssetImport.AssetImportDetails.Add(
                        new TblAssetImportDetail()
                        {
                            AssetId = assetStock.AssetId,
                            Quantity = assetImportDetail.Quantity,
                            Note = assetImportDetail.Note
                        }
                    );
                }
                tblAssetImport.TotalMoney = totalMoney;
                tblAssetImport.Quantity = quantity;
                _context.AssetImports.Add(tblAssetImport);
                await _context.SaveChangesAsync();
                responseInfo.Data.Add("Id", tblAssetImport.Id.ToString());
                responseInfo.Data.Add("Code", tblAssetImport.Code);
                return responseInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[][{_className}][{method}] Exception: {ex.Message}");
                throw ex;
            }
        }
    }
}