using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dapper;
using ERP.Databases;

namespace ERP.Bases.Models
{
    public interface ICommonModel
    {
    }
    public class CommonModel : BaseModel, ICommonModel
    {
        protected readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<CommonModel> _logger;
        private readonly string _className = string.Empty;
        private int _delay = 1000; // Time to delay
        public CommonModel(IServiceProvider provider) : base(provider)
        {
            IWebHostEnvironment webHostEnvironment = provider.GetService<IWebHostEnvironment>();
            ILogger<CommonModel> logger = provider.GetService<ILogger<CommonModel>>();
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _className = GetType().Name;
        }
        protected static string GetActualAsyncMethodName([CallerMemberName] string name = null) => name;
        /// <summary>
        /// Hàm tạo từ khoá truy vấn
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        protected string ConvertSearchTerm(string searchTerm)
        {
            return string.IsNullOrEmpty(searchTerm) ? searchTerm : "%" + Helpers.ConvertToUnSign(searchTerm.Trim()).ToLower() + "%";
        }
        public async Task<string> GenIdCodeAsync(string prefix, int id, string tbl = "SYSAST")
        {
            DbConnection _connection = _context.GetConnection();
            try
            {
                var date = DateTimeOffset.Now;
                string sqlGetLast = $@"
                SELECT
                    COALESCE(MIN(""{tbl}"".""Id""), {id})
                FROM ""{tbl}""
                WHERE
                    ""{tbl}"".""CreatedAt""::date = '{date:yyyy-MM-dd}'
                ";
                _logger.LogInformation(sqlGetLast);
                int index = await _connection.QueryFirstOrDefaultAsync<int>(sqlGetLast);
                int nextId = id - index + 1;
                _logger.LogInformation("Current Id: " + id);
                _logger.LogInformation("Current Date: " + date.ToString("yyyy-MM-dd"));
                _logger.LogInformation("Last Id: " + index);
                _logger.LogInformation("Next Id: " + nextId);
                if (nextId < 0)
                {
                    _logger.LogError("nextId < 0 ");
                    throw new Exception();
                }
                if (nextId < 10000)
                {
                    return $"{prefix}.{date.Day:D2}.{date.Month:D2}.{date.Year.ToString()[2..]}.{nextId:D5}";
                }
                else
                {
                    _logger.LogError("Id > 10000");
                    return $"{prefix}.{date.Day:D2}.{date.Month:D2}.{date.Year.ToString()[2..]}.{nextId:D5}";
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                _connection.Close();
            }
        }
        /// <summary>
        /// Get paging query string
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="numberOfRecord"></param>
        /// <returns></returns>
        protected string GetPagingQueryString(int currentPage, int numberOfRecord)
        {
            string pagingQuery = "";
            if (numberOfRecord > 0)
            {
                pagingQuery = $@"
                    OFFSET {(currentPage - 1) * numberOfRecord} ROWS
                    FETCH NEXT {numberOfRecord} ROWS ONLY
                ";
            }
            return pagingQuery;
        }
        protected void SetDelay(int delay)
        {
            _delay = delay;
        }
        protected async Task WaitingGlobalLock(
            DbConnection conn,
            int lockId,
            string tableName = null
        )
        {
            while (!await TryGetGlobalLock(conn, lockId, tableName))
            {
                await Task.Delay(_delay);
            }
        }
        protected async Task<bool> TryGetGlobalLock(
            DbConnection conn,
            int lockId,
            string tableName = null
        )
        {
            string sql = "SELECT pg_try_advisory_lock(@id);";
            if (!string.IsNullOrEmpty(tableName))
            {
                sql = $@"SELECT pg_try_advisory_lock('""{tableName}""'::regclass::integer, @id);";
            }
            return await conn.QueryFirstAsync<bool>(sql, new { id = lockId });
        }
        protected Task<bool> ReleaseGlobalLock(
            DbConnection conn,
            int lockId,
            string tableName = null
        )
        {
            string sql = "SELECT pg_advisory_unlock(@id);";
            if (!string.IsNullOrEmpty(tableName))
            {
                sql = $@"SELECT pg_advisory_unlock('""{tableName}""'::regclass::integer, @id);";
            }
            return conn.QueryFirstAsync<bool>(sql, new { id = lockId });
        }
    }
}