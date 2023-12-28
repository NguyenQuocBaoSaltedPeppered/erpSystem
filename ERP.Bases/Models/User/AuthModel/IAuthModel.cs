using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Bases.Models.User.Schemas;

namespace ERP.Bases.Models
{
    public interface IAuthModel
    {
        Whoami AuthLogin(LoginInfo loginInfo);
        bool AuthRegister(RegisterInfo registerInfo);

    }
}