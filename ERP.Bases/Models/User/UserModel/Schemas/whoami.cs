using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ERP.Bases.Models.User.Schemas
{
    public class Whoami
    {
        public int EmployeeId {get; set;}
        public string? EmployeeCode {get; set;}
        public int UserId {get; set;}
        public string UserName {get; set;} = null!;
        public int? BranchId {get; set;}
        public string BranchName {get; set;} = null!;
        public string? DepartmentId {get; set;}
        public string DepartmentName {get; set;} = null!;
        public string? PositionId {get; set;}
        public string PositionName {get; set;} = null!;
        public string? Email {get; set;}
    }
}