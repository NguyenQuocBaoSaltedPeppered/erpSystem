using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.AST.Models.Type.Schemas;
using ERP.Databases;

namespace ERP.AST.Models
{
    public interface ITypeModel
    {
        Task<ResponseInfo> CreateType(TypeData createData);
        Task<List<Types>> GetTypes(SearchCondition searchCondition);


    }
}