using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Databases;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using ERP.Bases.Models.Department.Schemas;

namespace ERP.Bases.Models
{
    public class DepartmentModel : CommonModel, IDepartmentModel
    {
        private readonly string _className = string.Empty;
        private readonly ILogger<DepartmentModel> _logger;

        public DepartmentModel(ILogger<DepartmentModel> logger, IServiceProvider provider) : base(provider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _className = GetType().Name;
        }
        public async Task<List<Departments>> GetDepartments(SearchCondition searchCondition)
        {
            DbConnection _connection = _context.GetConnection();
            try
            {
                var query = $@"
                    SELECT 
                            ""Id"" AS ""DepartmentId""
                        ,   ""Name"" AS ""DepartmentName""
                    FROM ""SYSDPM""
                    WHERE ""DelFlag"" = FALSE
                    {(!string.IsNullOrEmpty(searchCondition.Keyword)
                            ? $@" AND (
									LOWER(UNACCENT(""Name"")) LIKE LOWER(UNACCENT(@Keyword))
									OR LOWER(UNACCENT(""Id"")) LIKE LOWER(UNACCENT(@Keyword))
								)"
                            : "")}
                    ORDER BY ""Id"" DESC;
                ";
                _logger.LogInformation($"------ {query}");
                var param = new
                {
                    Keyword = ConvertSearchTerm(searchCondition.Keyword),
                };
                var result = await _connection.QueryMultipleAsync(query, param);
                var listDepartments = (await result.ReadAsync<Departments>()).ToList();
                return listDepartments;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
            
        }
    }
}