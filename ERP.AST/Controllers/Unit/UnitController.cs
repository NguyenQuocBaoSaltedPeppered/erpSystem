using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.AST.Models;
using ERP.AST.Models.Unit.Schemas;

namespace ERP.AST.Controllers
{
    [Route("api/unit")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitModel _unitModel;
        public UnitController(IUnitModel unitModel)
        {
            _unitModel = unitModel;
        }
        /// <summary>
        /// Tạo tài sản
        /// <para>Created at: 23/12/2023</para>
        /// <para>Created by: HoangTH</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpPost("create")]
        public async Task<IActionResult> CreateUnit([FromBody] UnitCreateSchema createData)
        {
            return Ok(await _unitModel.CreateUnit(createData));
        }
    }
}