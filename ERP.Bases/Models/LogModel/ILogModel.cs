using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Bases.Models.Position.Schemas;

namespace ERP.Bases.Models
{
    public interface ILogModel
    {
        // Task<List<Positions>> GetPositions(SearchCondition searchCondition);
        Task<string> CreateNote(string note);
    }
}