using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Bases.Models;
using ERP.Bases.Models.Type.Schemas;

namespace ERP.Bases.Controllers
{
    [Route("api/type")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeModel _typeModel;
        public TypeController(ITypeModel typeModel)
        {
            _typeModel = typeModel;
        }
        [HttpGet]
        public async Task<ActionResult<List<Types>>> GetTypes([FromQuery] SearchCondition searchCondition)
        {
            return Ok(await _typeModel.GetTypes(searchCondition));
        }
    }
}