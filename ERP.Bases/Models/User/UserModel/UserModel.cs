using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Databases;
using ERP.Bases.Models.User.Schemas;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace ERP.Bases.Models
{
    public partial class UserModel : CommonModel, IUserModel
    {
        private readonly string _className = string.Empty;
        private readonly ILogger<UserModel> _logger;
        public UserModel(ILogger<UserModel> logger, IServiceProvider provider) : base(provider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _className = GetType().Name;
        }
        public Whoami? Whoami(int id)
        {
            string method = GetActualAsyncMethodName();
            _logger.LogInformation($"[][{_className}][{method}]");
            return _context.Employees
                .Include(x => x.User)
                .Include(x => x.Branch)
                .Include(x => x.Department)
                .Include(x => x.Position)
                .Where(u => u.User.Id == id)
                .Select(u => new Whoami{
                    EmployeeId = u.Id,
                    EmployeeCode = u.Code,
                    UserId = u.User.Id,
                    UserName = u.User.Name,
                    BranchId = u.BranchId,
                    BranchName = u.Branch.Name,
                    DepartmentId = u.DepartmentId,
                    DepartmentName = u.Department.Name,
                    PositionId = u.PositionId,
                    PositionName = u.Position.Name,
                    Email = u.User.Email
                }).FirstOrDefault();
        }
        public async Task<List<Whoami>> GetUsers(SearchCondition searchCondition)
        {
            DbConnection _connection = _context.GetConnection();
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                var query = GetSqlListUser(searchCondition);
                var param = new
                {
                    Keyword = ConvertSearchTerm(searchCondition.Keyword),
                };
                _logger.LogInformation($"------ {query}");
                var result = await _connection.QueryMultipleAsync(query, param);
                var listUsers = (await result.ReadAsync<Whoami>()).ToList();
                _logger.LogInformation($"[][{_className}][{method}] End");
                return listUsers;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
        }

        private static string GetSqlListUser(SearchCondition searchCondition)
        {
            var sql = $@"
                    DROP TABLE IF EXISTS ""TEMPUsers"";
                    CREATE TEMPORARY TABLE ""TEMPUsers"" AS
                    SELECT
                            ""Employees"".""Id"" AS ""EmployeeId""
                        ,	""Employees"".""Code"" AS ""EmployeeCode""
                        ,	""Users"".""Id"" AS ""UserId""
                        ,	""Users"".""Name"" AS ""UserName""
                        ,	""Employees"".""BranchId"" AS ""BranchId""
                        ,	""SYSBR"".""Name"" AS ""BranchName""
                        ,	""Employees"".""DepartmentId"" AS ""DepartmentId""
                        ,	""SYSDPM"".""Name"" AS ""DepartmentName""
                        ,	""Employees"".""PositionId"" AS ""PositionId""
                        ,	""SYSPOS"".""Name"" AS ""PositionName""
                        ,	""Users"".""Email""
                    FROM ""Users""
                        LEFT JOIN ""Employees"" ON ""Employees"".""Id"" = ""Users"".""EmployeeId"" AND ""Employees"".""DelFlag"" = FALSE
                        LEFT JOIN ""SYSBR"" ON ""SYSBR"".""Id"" = ""Employees"".""BranchId"" AND ""SYSBR"".""DelFlag"" = FALSE
                        LEFT JOIN ""SYSDPM"" ON ""SYSDPM"".""Id"" = ""Employees"".""DepartmentId"" AND ""SYSDPM"".""DelFlag"" = FALSE
                        LEFT JOIN ""SYSPOS"" ON ""SYSPOS"".""Id"" = ""Employees"".""PositionId"" AND ""SYSPOS"".""DelFlag"" = FALSE
                    WHERE ""Users"".""DelFlag"" = FALSE
                        {(!string.IsNullOrEmpty(searchCondition.Keyword)
                            ? $@" AND (
									LOWER(UNACCENT(""Users"".""Name"")) LIKE LOWER(UNACCENT(@Keyword))
									OR LOWER(UNACCENT(""Employees"".""Code"")) LIKE LOWER(UNACCENT(@Keyword))
								)"
                            : "")}
                        ORDER BY ""Users"".""Id"";
                    SELECT * FROM ""TEMPUsers"";
                ";
            return sql;
        }
    }
}