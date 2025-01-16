using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserHandling.Dtos;
using UserHandling.Models;
using UserHandling.Services;

namespace UserHandling.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly MappingService _mappingService;

        public UserController(IUserService userService, MappingService mappingService)
        {
            _userService = userService;
            _mappingService = mappingService;
        }

        [Authorize]
        [HttpGet("protected-data")]
        public IActionResult GetProtectedData()
        {
            // This action is only accessible if the user is authenticated
            return Ok(new { data = "This is protected data" });
        }


        [HttpGet("GetUserDtoAll")]
        public async Task<IActionResult> GetUserDtoAll()
        {
            var users = await _userService.GetUserDtoAll();
            return Ok(users);
        }

        [HttpGet("GetUserDtoById/{id}")]
        public async Task<IActionResult> GetUserDtoById(int id)
        {
            var user = await _userService.GetUserDtoById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user); // Return a UserDto object
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);  // Use async method
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user); // Return User entity (raw data)
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto == null)
            {
                return BadRequest("User data is required");
            }

            // Map DTO to User entity
            var user = _mappingService.MapToUser(createUserDto);

            // Call service to add user
            var createdUser = await _userService.AddUserAsync(user);

            if (createdUser == null)
            {
                return BadRequest("Failed to create user");
            }

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _userService.AuthenticateUserAsync(loginUserDto.Username, loginUserDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate JWT token
            var token = GenerateJwtToken(user);

            // Return a token or any other relevant response
            return Ok(user);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Optional: Clear any server-side session or cached user data if applicable.

            // Since JWT is stateless, client-side should handle token removal.
            return Ok("Logged out successfully.");
        }

        // Helper method to generate JWT token
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
    };

            var key = "your-secret-key-here-256bits-long-enough"; // Ensure it's long enough
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourapp.com",
                audience: "yourapp.com",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
