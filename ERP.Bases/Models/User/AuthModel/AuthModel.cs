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
            string method = GetActualAsyncMethodName();
            try
            {
                _logger.LogInformation($"[][{_className}][{method}] Start");
                Whoami? me = _context.Employees
                .Include(x => x.User)
                .Include(x => x.Branch)
                .Include(x => x.Department)
                .Include(x => x.Position)
                .Where(u => u.Code == loginInfo.EmployeeCode
                    && u.User.Password == loginInfo.Password
                )
                .Select(u => new Whoami{
                    EmployeeId = u.Id,
                    EmployeeCode = u.Code,
                    UserId = u.User.Id,
                    UserName = u.User.Name,
                    BranchId = u.BranchId,
                    BranchName = u.Branch.Name,
                    DepartmentId = u.DepartmentId,
                    DepartmentName = u.Department.Name,
                    PositionName = u.Position.Name,
                    Email = u.User.Email
                }).FirstOrDefault();
                _logger.LogInformation($"[][{_className}][{method}] End");
                return me;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw;
            }
        }
          public  Whoami AuthRegister(RegisterInfo registerInfo)
    {
        try
        {
            // Kiểm tra xem mã nhân viên đã tồn tại chưa
               bool isEmployeeCodeExists = _context.Users.Any(u => u.Employee.Code == registerInfo.EmployeeCode);

            if (isEmployeeCodeExists)
            {
                return null;
            }
            var newEmployee = new Databases.Schemas.Employee
            {
            Code = registerInfo.EmployeeCode,
            };

            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            var newUser = new Databases.Schemas.User
            {
            Name = registerInfo.EmployeeName,
            Email = registerInfo.Email,
            Password = registerInfo.Password,
            EmployeeId= newEmployee.Id,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return new Whoami
            {
                EmployeeId = newEmployee.Id,
                EmployeeCode = newEmployee.Code,
                UserId = newUser.Id,
                UserName = newUser.Name,
                Email= newUser.Email,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
             
             Exception innerException = ex.InnerException;
        while (innerException != null)
        {
            _logger.LogError($"Inner Exception: {innerException.Message}");
            innerException = innerException.InnerException;
        }
        return null;
        }
    }
    }
}