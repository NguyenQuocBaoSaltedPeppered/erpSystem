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
using TblMSLOG = ERP.Databases.Schemas.SimpleLog;

namespace ERP.Bases.Models
{
    public class LogModel : CommonModel, ILogModel
    {
        private readonly string _className = string.Empty;
        private readonly ILogger<LogModel> _logger;

        public LogModel(ILogger<LogModel> logger, IServiceProvider provider) : base(provider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _className = GetType().Name;
        }
        public async Task<string> CreateNote(string note)
        {
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                TblMSLOG dataSubmit = new TblMSLOG()
                {
                    Note = note,
                };
                _context.SimpleLogs.Add(dataSubmit);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"[][{_className}][{method}] End");
                return $"Created {note}";
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[][{_className}][{method}] Error");
                throw ex;
            }
        }
    }
}