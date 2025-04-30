using BUGSystem.BL;
using BUGSystem.BL.DTOs.User;
using BUGSystem.BL.Mangers.UserManager;
using BUGSystem.DAL;
using BUGSystem.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BUGSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly UserManager<User> _identityUserManager;
        private readonly IUserManager _userManager;

        public UsersController(MyContext context , UserManager<User> identityUserManager , 
            IUserManager userManager)
        {
            _context = context;
            _identityUserManager = identityUserManager;
            _userManager = userManager;
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            // Simple validation: check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email already exists.");

            var user = new User
            {
                UserName = dto.Username,
                Email = dto.Email,
                    
            };

            // Hash the password
            var result = await _identityUserManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest("User creation failed. " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var result = await _userManager.LoginAsync(dto);

            if (!result.IsSuccess)
                return Unauthorized(result.Errors);

            return Ok(new { token = result.Data });
        }
    }
}
