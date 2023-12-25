using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Bases.Models.Type.Schemas;

namespace ERP.Bases.Models
{
    public interface ITypeModel
    {
        Task<List<Types>> GetTypes(SearchCondition searchCondition);

    }
}