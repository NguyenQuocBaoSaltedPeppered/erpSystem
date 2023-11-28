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
using ERP.Bases.Models.Position.Schemas;

namespace ERP.Bases.Models
{
    public class PositionModel : CommonModel, IPositionModel
    {
        private readonly string _className = string.Empty;
        private readonly ILogger<PositionModel> _logger;

        public PositionModel(ILogger<PositionModel> logger, IServiceProvider provider) : base(provider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _className = GetType().Name;
        }
        public async Task<List<Positions>> GetPositions(SearchCondition searchCondition)
        {
            DbConnection _connection = _context.GetConnection();
            try
            {
                var query = $@"
                    SELECT 
                            ""Id"" AS ""PositionId""
                        ,   ""Name"" AS ""PositionName""
                    FROM ""SYSPOS""
                    WHERE ""DelFlag"" = FALSE
                    {(!string.IsNullOrEmpty(searchCondition.Keyword)
                            ? $@" AND (
									LOWER(UNACCENT(""Name"")) LIKE LOWER(UNACCENT(@Keyword))
									OR LOWER(UNACCENT(""Id"")) LIKE LOWER(UNACCENT(@Keyword))
								)"
                            : "")}
                    ORDER BY ""Id"";
                ";
                _logger.LogInformation($"------ {query}");
                var param = new
                {
                    Keyword = ConvertSearchTerm(searchCondition.Keyword),
                };
                var result = await _connection.QueryMultipleAsync(query, param);
                var listPositions = (await result.ReadAsync<Positions>()).ToList();
                return listPositions;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
            
        }
    }
}