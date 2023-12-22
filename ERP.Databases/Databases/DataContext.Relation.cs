using Microsoft.EntityFrameworkCore;
using ERP.Databases.Schemas;
using ERP.Databases.Databases.Migrations;

namespace EBookStore.Databases
{
    public partial class DataContext
    {
        public void OnModelCreatingForCoreSystem(ModelBuilder modelBuilder)
        {
            #region CoreSystem
            modelBuilder.Entity<User>()
                .HasOne(e => e.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<User>(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Departments)
                .WithOne(e => e.Branch)
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Department>()
                .HasMany(e => e.Positions)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Branch)
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Department>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Position>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Position)
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region Asset
            modelBuilder.Entity<AssetType>()
                .HasMany(e => e.Assets)
                .WithOne(e => e.AssetType)
                .HasForeignKey(e => e.AssetTypeId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AssetUnit>()
                .HasMany(e => e.Assets)
                .WithOne(e => e.AssetUnit)
                .HasForeignKey(e => e.AssetUnitId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Asset>()
                .HasMany(e => e.AssetStocks)
                .WithOne(e => e.Asset)
                .HasForeignKey(e => e.AssetId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AssetExport>()
                .HasMany(e => e.AssetExportDetails)
                .WithOne(e => e.AssetExport)
                .HasForeignKey(e => e.AssetExportId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AssetStock>()
                .HasMany(e => e.AssetExportDetails)
                .WithOne(e => e.AssetStock)
                .HasForeignKey(e => e.AssetStockId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AssetStock>()
                .HasMany(e => e.AssetHistories)
                .WithOne(e => e.AssetStock)
                .HasForeignKey(e => e.AssetStockId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AssetImport>()
                .HasMany(e => e.AssetImportDetails)
                .WithOne(e => e.AssetImport)
                .HasForeignKey(e => e.AssetImportId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Asset>()
                .HasMany(e => e.AssetImportDetails)
                .WithOne(e => e.Asset)
                .HasForeignKey(e => e.AssetId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AssetTransfer>()
                .HasOne(e => e.AssetExport)
                .WithOne(e => e.AssetTransfer)
                .HasForeignKey<AssetTransfer>(e => e.AssetExportId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AssetTransfer>()
                .HasOne(e => e.AssetImport)
                .WithOne(e => e.AssetTransfer)
                .HasForeignKey<AssetTransfer>(e => e.AssetImportId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}