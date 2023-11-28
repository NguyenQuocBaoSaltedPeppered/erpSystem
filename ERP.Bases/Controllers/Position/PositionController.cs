using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Bases.Models;
using ERP.Bases.Models.Position.Schemas;

namespace ERP.Bases.Controllers
{
    [Route("api/position")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionModel _PositionModel;
        public PositionController(IPositionModel PositionModel)
        {
            _PositionModel = PositionModel;
        }
        [HttpGet]
        public async Task<ActionResult<List<Positions>>> GetPositions([FromQuery] SearchCondition searchCondition)
        {
            return Ok(await _PositionModel.GetPositions(searchCondition));
        }
    }
}