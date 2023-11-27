using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Bases.Models.User.Schemas;

namespace ERP.Bases.Models
{
    public interface IUserModel
    {
        Whoami? Whoami(int id);
        Task<List<Whoami>> GetUsers(SearchCondition searchCondition);
    }
}