using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Bases.Models.User.Schemas;
using Microsoft.EntityFrameworkCore;

namespace ERP.Bases.Models
{
    public class AuthModel : CommonModel, IAuthModel
    {
        private readonly string _className = string.Empty;
        private readonly ILogger<AuthModel> _logger;
        public AuthModel(ILogger<AuthModel> logger, IServiceProvider provider) : base(provider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _className = GetType().Name;
        }
        public Whoami AuthLogin(LoginInfo loginInfo)
        {
            try
            {
                Whoami? me = _context.Users
                    .Include(x => x.Employee)
                    .Include(x => x.Branch)
                    .Include(x => x.Department)
                    .Include(x => x.Position)
                    .Where(u => !u.DelFlag &&
                        u.Employee.Code == loginInfo.EmployeeCode &&
                        u.Password == loginInfo.Password
                    )
                    .Select(u => new Whoami{
                    EmployeeId = u.Employee.Id,
                    EmployeeCode = u.Employee.Code,
                    UserId = u.Id,
                    UserName = u.Name,
                    BranchId = u.BranchId,
                    BranchName = u.Branch.Name,
                    DepartmentId = u.DepartmentId,
                    DepartmentName = u.Department.Name,
                    PositionId = u.PositionId,
                    PositionName = u.Position.Name,
                    Email = u.Email
                }).FirstOrDefault();
                return me;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
        }
    }
}