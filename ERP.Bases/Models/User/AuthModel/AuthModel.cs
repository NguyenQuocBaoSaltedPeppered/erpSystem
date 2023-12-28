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
          public  bool AuthRegister(RegisterInfo registerInfo)
    {
        try
        {
            // Kiểm tra xem mã nhân viên đã tồn tại chưa
               bool isEmployeeCodeExists = _context.Users.Any(u => u.Employee.Code == registerInfo.EmployeeCode);

            if (isEmployeeCodeExists)
            {
                // Mã nhân viên đã tồn tại, không thể đăng ký mới
                // Bạn có thể xử lý theo nhu cầu cụ thể của bạn
                return false;
            }
            DateTimeOffset currentDateTimeOffset = DateTimeOffset.Now;
            // Tạo mới thông tin nhân viên
            var newEmployee = new Databases.Schemas.Employee
            {
            Code = registerInfo.EmployeeCode,
            CreatedAt = currentDateTimeOffset,
            CreatedBy = 1, 
            CreatedIp = "::1",
            UpdatedAt = currentDateTimeOffset,
            UpdatedBy = 1, 
            UpdatedIp = "::1",
            DelFlag = false,
            BranchId = null, 
            DepartmentId = null, 
            PositionId = null, 
            };

            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            var newUser = new Databases.Schemas.User
            {
            Name = registerInfo.EmployeeName,
            Email = registerInfo.Email,
            Password = registerInfo.Password,
            CreatedAt = currentDateTimeOffset,
            CreatedBy = 1, 
            CreatedIp = "::1",
            UpdatedAt = currentDateTimeOffset,
            UpdatedBy = 1, 
            UpdatedIp = "::1",
            DelFlag = false,
            EmployeeId= newEmployee.Id,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return true;
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
        return false;
        }
    }
    }
}