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
        [HttpGet("list")]
        public async Task<ActionResult<List<Units>>> GetUnits([FromQuery] SearchCondition searchCondition)
        {
            return Ok(await _unitModel.GetUnits(searchCondition));
        }
        /// <summary>
        /// Cập nhật đơn vị tính
        /// <para>Created at: 25/12/2023</para>
        /// <para>Created by: HoangTH</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUnit([FromBody] UnitCreateSchema updateData)
        {
            return Ok(await _unitModel.UpdateUnit(updateData));
        }
        /// <summary>
        /// Cập nhật đơn vị tính
        /// <para>Created at: 25/12/2023</para>
        /// <para>Created by: HoangTH</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit([FromRoute] int id)
        {
            return Ok(await _unitModel.DeleteUnit(id));
        }
    }
}