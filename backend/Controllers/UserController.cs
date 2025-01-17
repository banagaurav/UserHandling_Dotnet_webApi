using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/users
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var users = await _context.Users
                                .Include(u => u.UserPDFs)
                                    .ThenInclude(up => up.PDF)
                                .ToListAsync();

        if (users == null || users.Count == 0)
        {
            return NotFound("No users found.");
        }

        return Ok(users);
    }

    [HttpGet("all-dto")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersDto()
    {
        var users = await _context.Users
            .Select(user => new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role
            })
            .ToListAsync();

        if (users.Count == 0)
        {
            return NotFound("No users found.");
        }

        return Ok(users);
    }

    [HttpGet("all-with-pdfs")]
    public async Task<ActionResult<IEnumerable<UserDtoPdf>>> GetAllUsersWithPdfs()
    {
        var users = await _context.Users
            .Include(user => user.UserPDFs)
            .ThenInclude(up => up.PDF)
            .Select(user => new UserDtoPdf
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role,
                Pdfs = user.UserPDFs.Select(up => new PdfDto
                {
                    Id = up.PDF.Id,
                    FileName = up.PDF.FileName,

                }).ToList()
            })
            .ToListAsync();

        if (users.Count == 0)
        {
            return NotFound("No users found.");
        }

        return Ok(users);
    }

}
