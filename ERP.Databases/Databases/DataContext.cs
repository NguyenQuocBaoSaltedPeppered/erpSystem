
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using ERP.Databases.Schemas;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ERP.Databases
{
    public partial class DataContext : DbContext
    {
        public IHttpContextAccessor _context = null!;
        // private readonly string _connectionString = null!;
        // public DataContext(
        //     DbContextOptions<DataContext> options, IConfiguration configuration
        // ) : base(options)
        // {
        //     _connectionString = configuration.GetConnectionString("Default");
        // }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // => optionsBuilder.UseNpgsql(_connectionString);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("unaccent");
            modelBuilder.HasPostgresExtension("ltree");
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbConnection GetConnection()
        {
            DbConnection _connection = Database.GetDbConnection();
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }

        #region System
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        #endregion

        #region Asset
        /// <summary>
        /// Table SYSAST
        /// </summary>
        /// <value></value>
        public virtual DbSet<Asset> Assets {get; set;} = null!;
        /// <summary>
        /// Table SYSASTTP
        /// </summary>
        /// <value></value>
        public virtual DbSet<AssetType> AssetTypes {get; set;} = null!;
        /// <summary>
        /// Table SYSASTU
        /// </summary>
        /// <value></value>
        public virtual DbSet<AssetUnit> AssetUnits {get; set;} = null!;
        #endregion

        #region backgroundFunction
        public string GetRequestIp()
        {
            try
            {
                _context ??= StartupState.Instance.Services.GetService<IHttpContextAccessor>();
                string ip = _context?.HttpContext == null ? "::1" : _context.HttpContext.Request.Headers["X-Real-IP"].ToString();
                ip = string.IsNullOrEmpty(ip) ? _context.HttpContext.Request.Headers["x-request-ip"].ToString() : ip;
                ip = string.IsNullOrEmpty(ip) ? _context?.HttpContext.Connection.RemoteIpAddress.ToString() : ip;
                return ip;
            }
            catch
            {
                return "::1";
            }
        }
        #endregion
    }
}