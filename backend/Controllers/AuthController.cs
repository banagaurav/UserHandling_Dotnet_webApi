using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(AppDbContext context, IConfiguration configuration, ILogger<AuthController> logger)
    {
        _logger = logger;
        _context = context;
        _configuration = configuration;
    }
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        _logger.LogInformation($"Login attempt for username: {loginDto.Username}");

        // Validate user credentials
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

        if (user == null)
        {
            _logger.LogWarning($"Invalid credentials for username: {loginDto.Username}");
            return Unauthorized("Invalid credentials.");
        }

        // Generate JWT Token
        var claims = new[]
        {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim("userId", user.Id.ToString()),
    };

        var secretKey = _configuration["Jwt:SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            _logger.LogError("JWT SecretKey is missing in configuration.");
            return StatusCode(500, "JWT SecretKey is not configured.");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        _logger.LogInformation($"User {user.Username} logged in successfully.");

        // Return token
        return Ok(new
        {
            Token = tokenString,
            Expiration = DateTime.Now.AddHours(1),
            Username = user.Username,
            Role = user.Role
        });
    }

    // [HttpPost("login")]
    // public async Task<ActionResult> Login([FromBody] UserLoginDto loginDto)
    // {
    //     var user = await _context.Users
    //         .FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

    //     if (user == null)
    //     {
    //         return Unauthorized("Invalid credentials.");
    //     }

    //     // Create JWT token
    //     var claims = new[]
    //     {
    //         new Claim(ClaimTypes.Name, user.Username),
    //         new Claim(ClaimTypes.Role, user.Role),
    //         new Claim("userId", user.Id.ToString()),
    //     };

    //     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
    //     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //     var token = new JwtSecurityToken(
    //         issuer: _configuration["Jwt:Issuer"],
    //         audience: _configuration["Jwt:Audience"],
    //         claims: claims,
    //         expires: DateTime.Now.AddHours(1),
    //         signingCredentials: creds
    //     );

    //     var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

    //     return Ok(new { Token = tokenString });
    // }


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

        if (newUser.Role == "Admin")
        {
            return Unauthorized("You cannot create an Admin account.");
        }

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
