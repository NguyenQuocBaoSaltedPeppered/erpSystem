using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Bases.Models;
using ERP.Bases.Models.Unit.Schemas;

namespace ERP.Bases.Controllers
{
    [Route("api/unit")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitModel _UnitModel;
        public UnitController(IUnitModel UnitModel)
        {
            _UnitModel = UnitModel;
        }
        [HttpGet]
        public async Task<ActionResult<List<Units>>> GetUnits([FromQuery] SearchCondition searchCondition)
        {
            return Ok(await _UnitModel.GetUnits(searchCondition));
        }
    }
}