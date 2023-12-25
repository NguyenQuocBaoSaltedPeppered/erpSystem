using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.AST.Models;
using ERP.AST.Models.Type.Schemas;

namespace ERP.AST.Controllers
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
        /// <summary>
        /// Tạo tài sản
        /// <para>Created at: 23/12/2023</para>
        /// <para>Created by: HoangTH</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpPost("create")]
        public async Task<IActionResult> CreateType([FromBody] TypeData createData)
        {
            return Ok(await _typeModel.CreateType(createData));
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<Types>>> GetTypes([FromQuery] SearchCondition searchCondition)
        {
            return Ok(await _typeModel.GetTypes(searchCondition));
        }
        /// <summary>
        /// Xóa loại tài sản
        /// <para>Created at: 25/12/2023</para>
        /// <para>Created by: HoangTH</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType([FromRoute]int id)
        {
            return Ok(await _typeModel.DeleteType(id));
        }
        /// <summary>
        /// Cập nhật loại tài sản
        /// <para>Created at: 25/12/2023</para>
        /// <para>Created by: HoangTH</para>
        /// </summary>
        /// <returns>Id loại tài sản được cập nhật</returns>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpPut()]
        public async Task<IActionResult> UpdateType([FromBody] TypeData updateData)
        {
            return Ok(await _typeModel.UpdateType(updateData));
        }
        /// <summary>
        /// Lấy danh sách loại tài sản cho select box
        /// <para>Created at: 11/08/2023</para>
        /// <para>Created by: BaoNQ</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpGet("select-box")]
        public async Task<IActionResult> GetSelectBox()
        {
            return Ok(await _typeModel.GetSelectBox());
        }
    }
}