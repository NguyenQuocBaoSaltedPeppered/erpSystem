using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Bases.Models;
using ERP.Bases.Models.Department.Schemas;

namespace ERP.Bases.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogModel _LogModel;
        public LogController(ILogModel LogModel)
        {
            // _DepartmentModel = DepartmentModel;
            _LogModel = LogModel;
        }
        [HttpPost]
        public async Task<ActionResult<string>> GetDepartments([FromBody] string note)
        {
            return Ok(await _LogModel.CreateNote(note));
        }
    }
}