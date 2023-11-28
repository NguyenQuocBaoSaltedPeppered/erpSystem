using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Bases.Models;
using ERP.Bases.Models.Department.Schemas;

namespace ERP.Bases.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentModel _DepartmentModel;
        public DepartmentController(IDepartmentModel DepartmentModel)
        {
            _DepartmentModel = DepartmentModel;
        }
        [HttpGet]
        public async Task<ActionResult<List<Departments>>> GetDepartments([FromQuery] SearchCondition searchCondition)
        {
            return Ok(await _DepartmentModel.GetDepartments(searchCondition));
        }
    }
}