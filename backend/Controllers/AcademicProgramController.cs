using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AcademicProgramController : ControllerBase
{
    private readonly AppDbContext _context;

    public AcademicProgramController(AppDbContext context)
    {
        _context = context;
    }

    // Get all AcademicPrograms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AcademicProgramDto>>> GetAllAcademicPrograms()
    {
        var programs = await _context.AcademicPrograms
            .Include(ap => ap.Faculty) // Include Faculty details
                .ThenInclude(f => f.University) // Include University details
            .Select(ap => new AcademicProgramDto
            {
                Id = ap.Id,
                Name = ap.Name,
                Code = ap.Code,
                Faculty = new FacultyDto
                {
                    Id = ap.Faculty.Id,
                    Name = ap.Faculty.Name,
                    University = new UniversityDto
                    {
                        Id = ap.Faculty.University.Id,
                        Name = ap.Faculty.University.Name,
                        Location = ap.Faculty.University.Location
                    }
                }
            })
            .ToListAsync();

        return Ok(programs);
    }
    // Upload (Create) AcademicProgram
    [HttpPost]
    public async Task<ActionResult<AcademicProgramDto>> CreateAcademicProgram(AcademicProgramDto programDto)
    {
        // Validate if Faculty exists
        var faculty = await _context.Faculties
            .Include(f => f.University) // Include University to validate the association
            .FirstOrDefaultAsync(f => f.Id == programDto.Faculty.Id);

        if (faculty == null)
        {
            return NotFound("Faculty not found.");
        }

        // Validate if the University is valid
        var university = faculty.University;
        if (university == null)
        {
            return NotFound("University not found.");
        }

        // Check if the provided University ID matches the Faculty's associated University
        if (programDto.Faculty.University.Id != university.Id)
        {
            return BadRequest("The Faculty is associated with a different University.");
        }

        // Create the new Academic Program
        var program = new AcademicProgram
        {
            Name = programDto.Name,
            Code = programDto.Code,
            FacultyId = faculty.Id
        };

        _context.AcademicPrograms.Add(program);
        await _context.SaveChangesAsync();

        // Map the response to the updated DTO structure
        var createdProgramDto = new AcademicProgramDto
        {
            Id = program.Id,
            Name = program.Name,
            Code = program.Code,
            Faculty = new FacultyDto
            {
                Id = faculty.Id,
                Name = faculty.Name,
                University = new UniversityDto
                {
                    Id = university.Id,
                    Name = university.Name,
                    Location = university.Location
                }
            }
        };

        return CreatedAtAction(nameof(GetAllAcademicPrograms), new { id = program.Id }, createdProgramDto);
    }
    // Delete AcademicProgram
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAcademicProgram(int id)
    {
        var program = await _context.AcademicPrograms.FindAsync(id);
        if (program == null)
        {
            return NotFound("Academic Program not found.");
        }

        _context.AcademicPrograms.Remove(program);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
