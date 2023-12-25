using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.AST.Models;
using ERP.AST.Models.Asset.Schemas;

namespace ERP.AST.Controllers
{
    [Route("api/asset")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetModel _assetModel;
        public AssetController(IAssetModel assetModel)
        {
            _assetModel = assetModel;
        }
        /// <summary>
        /// Tạo tài sản
        /// <para>Created at: 23/12/2023</para>
        /// <para>Created by: HoangTH</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpPost()]
        public async Task<IActionResult> CreateAsset([FromForm] AssetData assetData)
        {
            return Ok(await _assetModel.CreateAsset(assetData));
        }
        /// <summary>
        /// Danh sách tài sản
        /// <para>Created at: 23/12/2023</para>
        /// <para>Created by: HoangTH</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpGet()]
        public async Task<IActionResult> GetListAssetData([FromQuery] AssetFilter filter)
        {
            return Ok(await _assetModel.GetListAssetData(filter));
        }
    }
}