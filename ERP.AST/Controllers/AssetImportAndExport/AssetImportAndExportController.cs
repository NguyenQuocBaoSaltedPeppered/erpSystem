using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.AST.Models;
using ERP.AST.Models.AssetImportAndExport.Schemas;
using System.Net;

namespace ERP.AST.Controllers
{
    [Route("api/asset-import-export")]
    [ApiController]
    public class AssetImportAndExportController : ControllerBase
    {
        private readonly IAssetImportAndExportModel _assetImportAndExportModel;
        public AssetImportAndExportController(IAssetImportAndExportModel assetImportAndExportModel)
        {
            _assetImportAndExportModel = assetImportAndExportModel;
        }
        /// <summary>
        /// Tạo phiếu báo tăng
        /// <para>Created at: 25/12/2023</para>
        /// <para>Created by: BaoNQ</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpPost("import")]
        [ProducesResponseType(typeof(AssetImportData), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAssetImport([FromBody] AssetImportData assetImportData)
        {
            try
            {
                return Ok(await _assetImportAndExportModel.CreateAssetImport(assetImportData));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }
        /// <summary>
        /// Tạo phiếu báo giảm
        /// <para>Created at: 25/12/2023</para>
        /// <para>Created by: BaoNQ</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpPost("export")]
        [ProducesResponseType(typeof(AssetExportData), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAssetExport([FromBody] AssetExportData assetExportData)
        {
            try
            {
                return Ok(await _assetImportAndExportModel.CreateAssetExport(assetExportData));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }
        /// <summary>
        /// Tạo luân chuyển tài sản
        /// <para>Created at: 25/12/2023</para>
        /// <para>Created by: BaoNQ</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpPost("hand-over")]
        [ProducesResponseType(typeof(AssetExportData), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAssetHandOver([FromBody] AssetHandOverData assetHandOverData)
        {
            try
            {
                return Ok(await _assetImportAndExportModel.CreateAssetHandOver(assetHandOverData));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }
        /// <summary>
        /// Cập nhật bàn giao
        /// <para>Created at: 25/12/2023</para>
        /// <para>Created by: BaoNQ</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateData"></param>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpPut("hand-over/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateHandOver([FromRoute] int id, [FromBody] AssetHandOverData updateData)
        {
            try
            {
                return Ok(await _assetImportAndExportModel.UpdateHandOver(id, updateData));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }
    }
}