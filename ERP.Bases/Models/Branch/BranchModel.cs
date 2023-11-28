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
using ERP.Bases.Models.Branch.Schemas;

namespace ERP.Bases.Models
{
    public class BranchModel : CommonModel, IBranchModel
    {
        private readonly string _className = string.Empty;
        private readonly ILogger<BranchModel> _logger;

        public BranchModel(ILogger<BranchModel> logger, IServiceProvider provider) : base(provider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _className = GetType().Name;
        }
        public async Task<List<Branches>> GetBranches(SearchCondition searchCondition)
        {
            DbConnection _connection = _context.GetConnection();
            try
            {
                var query = $@"
                    SELECT 
                            ""Id"" AS ""BranchId""
                        ,   ""Name"" AS ""BranchName""
                    FROM ""SYSBR""
                    WHERE ""DelFlag"" = FALSE
                    {(!string.IsNullOrEmpty(searchCondition.Keyword)
                            ? $@" AND (
									LOWER(UNACCENT(""Name"")) LIKE LOWER(UNACCENT(@Keyword))
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
                var listBranches = (await result.ReadAsync<Branches>()).ToList();
                return listBranches;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
            
        }
    }
}