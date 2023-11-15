
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Databases.Schemas;
using Microsoft.EntityFrameworkCore;

namespace ERP.Databases
{
    public partial class DataContext : DbContext
    {
        public IHttpContextAccessor _context = null!;
        private readonly string _connectionString = null!;
        public DataContext(
            DbContextOptions<DataContext> options, IConfiguration configuration
        ) : base(options)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_connectionString);

        #region System
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        #endregion
    }
}