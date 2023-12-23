using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ERP.Bases.Models.User.Schemas
{
    public class LoginInfo
    {
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// <value></value>
        public string? EmployeeCode {get; set;}
        /// <summary>
        /// Mật khẩu
        /// </summary>
        /// <value></value>
        public string? Password {get; set;}
    }
}