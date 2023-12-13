
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
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
        public DataContext(
            DbContextOptions<DataContext> options
            // , IConfiguration configuration
        ) : base(options)
        {
            // _connectionString = configuration.GetConnectionString("Default");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // => optionsBuilder.UseNpgsql(_connectionString);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("unaccent");
            modelBuilder.HasPostgresExtension("ltree");
        }
        // public DataContext(DbContextOptions<DataContext> options) : base(options)
        // {

        // }
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

        #region LogControl
        public virtual DbSet<SimpleLog> SimpleLogs {get; set;} = null!;
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
        public string GetAccountId()
        {
            try
            {
                _context ??= StartupState.Instance.Services.GetService<IHttpContextAccessor>();
                string accountId = "0";
                ClaimsPrincipal user = null;
                if (_context != null && _context.HttpContext != null)
                {
                    user = _context.HttpContext.User;
                }
                if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
                {
                    var identity = user.Identity as ClaimsIdentity;
                    accountId = identity.Claims.Where(p => p.Type == "UserId").FirstOrDefault()?.Value;
                }
                return accountId;
            }
            catch (Exception e)
            {
                return "0";
            }
        }
        private void OnBeforeSaving()
        {
            try
            {
                // Nếu có sự thay đổi dữ liệu
                if (ChangeTracker.HasChanges())
                {
                    // Láy các thông tin cơ bản từ hệ thống
                    DateTimeOffset now = DateTimeOffset.Now;
                    int accountId = Convert.ToInt32(GetAccountId());
                    string ip = GetRequestIp();
                    // Duyệt qua hết tất cả dối tượng có thay đổi
                    foreach (var entry in ChangeTracker.Entries())
                    {
                        try
                        {
                            if (entry.Entity is ITable root)
                            {
                                switch (entry.State)
                                {
                                    // Nếu là thêm mới thì cập nhật thông tin thêm mới
                                    case EntityState.Added:
                                        {
                                            root.CreatedAt = root.CreatedAt.ToString("yyyy") == "0001" ? now : root.CreatedAt;
                                            root.CreatedBy = root.CreatedBy > 0 ? root.CreatedBy : accountId;
                                            root.CreatedIp = ip;
                                            root.UpdatedAt = null;
                                            root.UpdatedBy = null;
                                            root.UpdatedIp = ip;
                                            root.DelFlag = false;
                                            break;
                                        }
                                    // Nếu là update thì cập nhật các trường liên quan đến update
                                    case EntityState.Modified:
                                        {
                                            root.UpdatedAt = now;
                                            root.UpdatedBy = accountId;
                                            root.UpdatedIp = ip;
                                            break;
                                        }
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override int SaveChanges()
        {
            try
            {
                OnBeforeSaving();
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                OnBeforeSaving();
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}