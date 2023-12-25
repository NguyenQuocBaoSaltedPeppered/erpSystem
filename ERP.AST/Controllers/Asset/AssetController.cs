using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.AST.Models;
using ERP.AST.Models.Asset.Schemas;
using System.Net;

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
        /// <summary>
        /// Danh sách người sử dụng tài sản
        /// <para>Created at: 25/12/2023</para>
        /// <para>Created by: BaoNQ</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpGet("list/user")]
        [ProducesResponseType(typeof(List<AssetUserDetail>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAssetUsers([FromQuery] AssetUserDetailFilter filter)
        {
            try
            {
                return Ok(await _assetModel.GetAssetUsers(filter));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }
        /// <summary>
        /// Chi tiết người sử dụng tài sản
        /// <para>Created at: 25/12/2023</para>
        /// <para>Created by: BaoNQ</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpGet("list/user-detail")]
        [ProducesResponseType(typeof(AssetUserDetail), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAssetUserDetail([FromQuery] AssetUserDetailFilter filter)
        {
            try
            {
                return Ok(await _assetModel.GetAssetUserDetail(filter));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }
        /// <summary>
        /// Danh sách tài sản
        /// <para>Created at: 08/08/2023</para>
        /// <para>Created by: PhucVinh</para>
        /// </summary>
        /// <returns>Thông tin</returns>
        /// <response code="404">Không tìm thấy thông tin</response>
        /// <response code="500">Lỗi khi có exception</response>
        [HttpGet("asset-stock")]
        [ProducesResponseType(typeof(ListAssetData), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListAssetStockData([FromQuery] AssetFilter filter)
        {
            try
            {
                return Ok(await _assetModel.GetListAssetStockData(filter));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }
    }
}