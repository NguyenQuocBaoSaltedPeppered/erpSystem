using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Databases
{
    public class BaseSearchCondition
    {
        public BaseSearchCondition()
        {}
        public string? Keyword {get; set;}
        public int BranchId {get; set;}
        public string? DepartmentId {get; set;}
        public string? PositionId {get; set;}
        public int CurrentPage {get; set;}
        public int PageSize {get; set;}
    }
}