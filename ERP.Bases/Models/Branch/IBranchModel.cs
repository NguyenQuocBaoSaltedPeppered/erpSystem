using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Bases.Models.Branch.Schemas;

namespace ERP.Bases.Models
{
    public interface IBranchModel
    {
        Task<List<Branches>> GetBranches(SearchCondition searchCondition);

    }
}