using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Bases.Models.Unit.Schemas;

namespace ERP.Bases.Models
{
    public interface IUnitModel
    {
        Task<List<Units>> GetUnits(SearchCondition searchCondition);

    }
}