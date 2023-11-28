using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Bases.Models.Department.Schemas;

namespace ERP.Bases.Models
{
    public interface IDepartmentModel
    {
        Task<List<Departments>> GetDepartments(SearchCondition searchCondition);

    }
}