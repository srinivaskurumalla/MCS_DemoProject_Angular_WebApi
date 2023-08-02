using MCS_DemoProject_Angular_WebApi.Interfaces;
using MCS_DemoProject_Angular_WebApi.Models;
using MCS_DemoProject_Angular_WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCS_DemoProject_Angular_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IUsers<ClaimsMaster> _claimsRepo;

        public ClaimsController(IUsers<ClaimsMaster> claimsRepo)
        {
            _claimsRepo = claimsRepo;
        }
        [Authorize]
        [HttpGet("GetAllClaims")]
        public async Task<IActionResult> GetAllUsers()
        {
            var allClaims = await _claimsRepo.GetAll();
            return Ok(allClaims);
        }
    }
}
