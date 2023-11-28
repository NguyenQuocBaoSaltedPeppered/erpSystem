using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ERP.Bases.Models;
using ERP.Bases.Models.Branch.Schemas;

namespace ERP.Bases.Controllers
{
    [Route("api/branch")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchModel _branchModel;
        public BranchController(IBranchModel branchModel)
        {
            _branchModel = branchModel;
        }
        [HttpGet]
        public async Task<ActionResult<List<Branches>>> GetBranches([FromQuery] SearchCondition searchCondition)
        {
            return Ok(await _branchModel.GetBranches(searchCondition));
        }
    }
}