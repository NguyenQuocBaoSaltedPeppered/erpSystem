using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Bases.Models;
using ERP.Bases.Models.User.Schemas;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Bases.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserModel _userModel;
        public UserController(IUserModel userModel)
        {
            _userModel = userModel;
        }
        [HttpGet("me/{id}")]
        public ActionResult<Whoami> Whoami([FromRoute] int id)
        {
            Whoami? me = _userModel.Whoami(id);
            if(me is null) return NotFound("User not Found");
            return Ok(me);
        }
    }
}