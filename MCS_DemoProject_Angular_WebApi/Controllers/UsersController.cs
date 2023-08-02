using MCS_DemoProject_Angular_WebApi.Interfaces;
using MCS_DemoProject_Angular_WebApi.Models;
using MCS_DemoProject_Angular_WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        private readonly IConfiguration _configuration;
        


        public UsersController(IUsers<User> userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
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
        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = await _userRepo.GetAll();
            return Ok(allUsers);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepo.GetUserById(id);
            if(user != null )
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var currentUser = await _userRepo.GetUserByEmail(user.Email);
            if (currentUser == null)
            {
                return BadRequest("Invalid Email");
            }

            var isValidPassword = VerifyPassword(user.Password, currentUser.PasswordSalt, currentUser.PasswordHash);

            if (!isValidPassword)
            {
                return BadRequest("Invalid Password");
            }

            var token = GenerateToken(currentUser);

            if (token == null)
            {
                return NotFound("Invalid credentials");
            }

            return Ok(token);
        }

       
        [NonAction]
        public bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return hash.SequenceEqual(passwordHash);
            }
        }

        [NonAction]
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SECRET_KEY"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var myClaims = new List<Claim>
            {
                new Claim("name",user.FirstName),
                new Claim("lastName",user.LastName),
                new Claim("email", user.Email),
                new Claim("Role", user.Role)
            };

            var token = new JwtSecurityToken(issuer: _configuration["JWT:issuer"],
                                             claims: myClaims,
                                             expires: DateTime.Now.AddHours(10),
                                             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
