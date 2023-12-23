using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Bases.Models;
using ERP.Bases.Models.User.Schemas;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Bases.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthModel _authModel;
        public AuthController(IAuthModel authModel)
        {
            _authModel = authModel;
        }
        [HttpPost("login")]
        public ActionResult<Whoami> AuthLogin([FromBody] LoginInfo loginInfo)
        {
            try
            {
                Whoami? me = _authModel.AuthLogin(loginInfo);
                if (me == null)
                {
                    return BadRequest("Employee Code or Password is incorrect");
                }
                return Ok(_authModel.AuthLogin(loginInfo));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message});
            }
        }
    }
}