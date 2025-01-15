using Microsoft.AspNetCore.Mvc;
using UserHandling.Dtos;
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

        [HttpPost]
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
    }
}
