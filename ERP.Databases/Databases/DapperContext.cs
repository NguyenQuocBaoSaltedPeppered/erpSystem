using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases
{
    public interface IDapperContext
    {
        DbConnection GetConnection();
        Task<DbConnection> GetConnectionAsync();
        void Close();
        Task CloseAsync();
    }
    public class DapperContext : IDapperContext
    {
        private readonly DbConnection _connection;
        private readonly ILogger<DapperContext> _logger;
        public DapperContext(
            DbConnection connection,
            ILogger<DapperContext> logger
        )
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void Close()
        {
            _connection.Close();
        }

        public async Task CloseAsync()
        {
            await _connection.CloseAsync();
        }

        public DbConnection GetConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }

        public async Task<DbConnection> GetConnectionAsync()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                await _connection.OpenAsync();
            }
            return _connection;
        }
    }
}