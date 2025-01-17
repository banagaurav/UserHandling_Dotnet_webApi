using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(UserLoginDto loginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

        if (user == null || user.Password != loginDto.Password) // Password should be hashed and verified
        {
            return Unauthorized("Invalid username or password.");
        }

        return Ok(new
        {
            Message = "Login successful.",
            UserId = user.Id,
            user.FullName,
            user.Role
        });
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(UserRegisterDto registerDto)
    {
        if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
        {
            return BadRequest("Username already exists.");
        }

        var newUser = new User
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            PhoneNumber = registerDto.PhoneNumber,
            Email = registerDto.Email,
            Username = registerDto.Username,
            Password = registerDto.Password, // You should hash passwords before saving
            Role = "User" // Default role
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "User registered successfully.",
            UserId = newUser.Id,
            newUser.FullName
        });
    }
}
