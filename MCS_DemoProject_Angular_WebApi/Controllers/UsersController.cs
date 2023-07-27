using MCS_DemoProject_Angular_WebApi.Interfaces;
using MCS_DemoProject_Angular_WebApi.Models;
using MCS_DemoProject_Angular_WebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace MCS_DemoProject_Angular_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /* private readonly UserRepo _userRepo;

         public UsersController(UserRepo userRepo)
         {
             _userRepo = userRepo;
         }*/
        private readonly IUsers<User> _userRepo;

        public UsersController(IUsers<User> userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
           
            var userRegistered = await  _userRepo.Create(user);
            if(userRegistered)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("user already exists or try with different email Id");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            var userUpdated = await _userRepo.Update(id, user);
            if(userUpdated!=null)
            {
                return Ok(userUpdated);
            }
            return NotFound();
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = await _userRepo.GetAll();
            return Ok(allUsers);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepo.GetById(id);
            if(user != null )
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

    

    }
}
