using Microsoft.EntityFrameworkCore;
using ERP.Databases.Schemas;

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
        }
    }
}