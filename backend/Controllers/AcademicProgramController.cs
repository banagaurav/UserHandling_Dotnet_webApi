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
                FacultyId = ap.FacultyId,
                FacultyName = ap.Faculty.Name,
                UniversityId = ap.Faculty.UniversityId,
                UniversityName = ap.Faculty.University.Name
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
            .Include(f => f.University) // Include University to validate the university for the Faculty
            .FirstOrDefaultAsync(f => f.Id == programDto.FacultyId);

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

        // Check if the UniversityId matches the provided UniversityId in the DTO
        if (programDto.UniversityId != university.Id)
        {
            return BadRequest("The Faculty is associated with a different University.");
        }

        // Create the new Academic Program
        var program = new AcademicProgram
        {
            Name = programDto.Name,
            Code = programDto.Code,
            FacultyId = programDto.FacultyId
        };

        _context.AcademicPrograms.Add(program);
        await _context.SaveChangesAsync();

        // Return the created Academic Program with its Faculty and University details
        programDto.Id = program.Id;
        programDto.FacultyName = faculty.Name;
        programDto.UniversityId = university.Id;
        programDto.UniversityName = university.Name;

        return CreatedAtAction(nameof(GetAllAcademicPrograms), new { id = program.Id }, programDto);
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
