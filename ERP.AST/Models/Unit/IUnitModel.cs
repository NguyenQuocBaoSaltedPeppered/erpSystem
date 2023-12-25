using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.AST.Models.Unit.Schemas;
using ERP.Databases;

namespace ERP.AST.Models
{
    public interface IUnitModel
    {
        Task<ResponseInfo> CreateUnit(UnitCreateSchema createData);
        Task<List<Units>> GetUnits(SearchCondition searchCondition);
        Task<ResponseInfo> UpdateUnit(UnitCreateSchema createData);
        Task<ResponseInfo> DeleteUnit(int Id);
    }
}