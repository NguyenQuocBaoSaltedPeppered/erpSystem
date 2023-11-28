using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Bases.Models.Position.Schemas;

namespace ERP.Bases.Models
{
    public interface IPositionModel
    {
        Task<List<Positions>> GetPositions(SearchCondition searchCondition);

    }
}