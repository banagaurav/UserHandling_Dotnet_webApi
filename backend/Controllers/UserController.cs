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

    [HttpGet("users/all-with-pdfs")]
    public async Task<ActionResult<IEnumerable<UserDtoPdf>>> GetAllUsersWithPdfs()
    {
        var users = await _context.Users
            .Include(user => user.UserPDFs)  // Include the UserPDFs collection
            .ThenInclude(up => up.PDF) // Access the PDF through the UserPDF entity
            .ThenInclude(pdf => pdf.SubjectPDFs) // Include the SubjectPDFs (junction table)
            .ThenInclude(sp => sp.Subject)  // Access the related Subject entity
            .ThenInclude(s => s.AcademicProgram)  // Include the related AcademicProgram for the Subject
            .ThenInclude(ap => ap.Faculty)  // Include the Faculty related to AcademicProgram
            .ThenInclude(f => f.University) // Include the University related to Faculty
            .Select(user => new UserDtoPdf
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role,  // Role is a string, so it’s fine as is
                Pdfs = user.UserPDFs.Select(up => new PdfDto
                {
                    Id = up.PDF.Id,
                    FileName = up.PDF.FileName,
                    // Include the Subjects related to the PDF through SubjectPDFs
                    Subjects = up.PDF.SubjectPDFs.Select(sp => new SubjectDto
                    {
                        Id = sp.Subject.Id,
                        Name = sp.Subject.Name,
                        Code = sp.Subject.Code,
                        AcademicProgram = new AcademicProgramDto
                        {
                            Id = sp.Subject.AcademicProgram.Id,
                            Name = sp.Subject.AcademicProgram.Name,
                            Code = sp.Subject.AcademicProgram.Code,
                            Faculty = new FacultyDto
                            {
                                Id = sp.Subject.AcademicProgram.Faculty.Id,
                                Name = sp.Subject.AcademicProgram.Faculty.Name,
                                University = new UniversityDto
                                {
                                    Id = sp.Subject.AcademicProgram.Faculty.University.Id,
                                    Name = sp.Subject.AcademicProgram.Faculty.University.Name
                                }
                            }
                        }
                    }).ToList()  // Include the Subjects related to the PDF
                }).ToList()
            })
            .ToListAsync();

        if (users.Count == 0)
        {
            return NotFound("No users found.");
        }

        return Ok(users);
    }

    [HttpGet("users/{userId}/all-pdfs")]
    public async Task<ActionResult<UserDtoPdf>> GetUserByIdWithPdfs(int userId)
    {
        var user = await _context.Users
            .Include(u => u.UserPDFs)  // Include the UserPDFs collection
            .ThenInclude(up => up.PDF) // Now access the PDF through the UserPDF entity
            .ThenInclude(pdf => pdf.SubjectPDFs) // Include the SubjectPDFs (junction table)
            .ThenInclude(sp => sp.Subject)  // Access the related Subject entity
            .ThenInclude(s => s.AcademicProgram)  // Include the related AcademicProgram for the Subject
            .ThenInclude(ap => ap.Faculty)            // Include the Faculty related to AcademicProgram
            .ThenInclude(f => f.University)          // Include the University related to Faculty
            .Where(u => u.Id == userId)  // Filter by userId
            .Select(u => new UserDtoPdf
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                Username = u.Username,
                Role = u.Role,  // Role is a string, so it’s fine as is
                Pdfs = u.UserPDFs.Select(up => new PdfDto
                {
                    Id = up.PDF.Id,
                    FileName = up.PDF.FileName,
                    // Include the Subjects related to the PDF through SubjectPDFs
                    Subjects = up.PDF.SubjectPDFs.Select(sp => new SubjectDto
                    {
                        Id = sp.Subject.Id,
                        Name = sp.Subject.Name,
                        Code = sp.Subject.Code,
                        AcademicProgram = new AcademicProgramDto
                        {
                            Id = sp.Subject.AcademicProgram.Id,
                            Name = sp.Subject.AcademicProgram.Name,
                            Code = sp.Subject.AcademicProgram.Code,
                            Faculty = new FacultyDto
                            {
                                Id = sp.Subject.AcademicProgram.Faculty.Id,
                                Name = sp.Subject.AcademicProgram.Faculty.Name,
                                University = new UniversityDto
                                {
                                    Id = sp.Subject.AcademicProgram.Faculty.University.Id,
                                    Name = sp.Subject.AcademicProgram.Faculty.University.Name
                                }
                            }
                        }
                    }).ToList()  // Include the Subjects related to the PDF
                }).ToList()
            })
            .FirstOrDefaultAsync();  // Get the first (and only) user or null if not found

        if (user == null)
        {
            return NotFound("User not found.");
        }

        return Ok(user);
    }

}
