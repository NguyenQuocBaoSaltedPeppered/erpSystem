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
    public partial class UserModel : IUserModel
    {
        private readonly string _connectionString;
        private readonly string _className = string.Empty;
        private readonly DataContext _context;
        private readonly ILogger<UserModel> _logger;
        public UserModel(DataContext context, ILogger<UserModel> logger, string connectionString)
        {
            _context = context;
            _logger = logger;
            _connectionString = connectionString;
        }
        public Whoami? Whoami(int id)
        {
            return _context.Users
                .Include(x => x.Employee)
                .Include(x => x.Branch)
                .Include(x => x.Department)
                .Include(x => x.Position)
                .Where(u => u.Id == id).Select(u => new Whoami{
                EmployeeId = u.Employee.Id,
                EmployeeCode = u.Employee.Code,
                UserId = u.Id,
                UserName = u.Name,
                BranchId = u.BranchId,
                BranchName = u.Branch.Name,
                DepartmentId = u.DepartmentId,
                DepartmentName = u.Department.Name,
                PositionId = u.PositionId,
                PositionName = u.Position.Name,
                Email = u.Email
            }).FirstOrDefault();
        }
        public async Task<List<Whoami>> GetUsers(SearchCondition searchCondition)
        {
            DbConnection _connection = _context.GetConnection();
            try
            {
                var query = GetSqlListUser(searchCondition);
                var param = new
                {
                    Keyword = searchCondition.Keyword,
                };
                var result = await _connection.QueryMultipleAsync(query, param);
                var listUsers = (await result.ReadAsync<Whoami>()).ToList();

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
                        ,	""Users"".""BranchId"" AS ""BranchId""
                        ,	""SYSBR"".""Name"" AS ""BranchName""
                        ,	""Users"".""DepartmentId"" AS ""DepartmentId""
                        ,	""SYSDPM"".""Name"" AS ""DepartmentName""
                        ,	""Users"".""PositionId"" AS ""PositionId""
                        ,	""SYSPOS"".""Name"" AS ""PositionName""
                        ,	""Users"".""Email""
                    FROM ""Users""
                        LEFT JOIN ""Employees"" ON ""Employees"".""Id"" = ""Users"".""EmployeeId"" AND ""Employees"".""DelFlag"" = FALSE
                        LEFT JOIN ""SYSBR"" ON ""SYSBR"".""Id"" = ""Users"".""BranchId"" AND ""SYSBR"".""DelFlag"" = FALSE
                        LEFT JOIN ""SYSDPM"" ON ""SYSDPM"".""Id"" = ""Users"".""DepartmentId"" AND ""SYSDPM"".""DelFlag"" = FALSE
                        LEFT JOIN ""SYSPOS"" ON ""SYSPOS"".""Id"" = ""Users"".""PositionId"" AND ""SYSPOS"".""DelFlag"" = FALSE
                    WHERE 1=1
                        AND ""Users"".""DelFlag"" = FALSE
                        {(!string.IsNullOrEmpty(searchCondition.Keyword)
                            ? $@" AND (LOWER (unaccent (""UserName"") LIKE LOWER (unaccent (@Keyword))
                                OR LOWER (unaccent (""EmployeeCode"")) LIKE LOWER (unaccent(@Keyword)))"
                            : "")}
                        ORDER BY ""Users"".""Id"";
            
                    SELECT * FROM ""TEMPUsers"";
                ";
            return sql;
        }
    }
}