
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Databases.Schemas;
using Microsoft.EntityFrameworkCore;

namespace ERP.Databases
{
    public class DataContext : DbContext
    {
        private readonly string _connectionString = null!;
        public DataContext(
            DbContextOptions<DataContext> options, IConfiguration configuration
        ) : base(options)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        public DbSet<User> Users { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_connectionString);
    }
}