using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UniversityController : ControllerBase
{
    private readonly AppDbContext _context;

    public UniversityController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<UniversityDto>> CreateUniversity(UniversityDto universityDto)
    {
        var university = new University
        {
            Name = universityDto.Name,
            Location = universityDto.Location
        };

        _context.Universities.Add(university);
        await _context.SaveChangesAsync();

        // Return the created university (you can use the DTO to return a simplified object)
        universityDto.Id = university.Id;
        return CreatedAtAction(nameof(GetAllUniversities), new { id = university.Id }, universityDto);
    }

    // Get all universities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UniversityDto>>> GetAllUniversities()
    {
        var universities = await _context.Universities
            .Select(u => new UniversityDto
            {
                Id = u.Id,
                Name = u.Name,
                Location = u.Location
            })
            .ToListAsync();

        return Ok(universities);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUniversity(int id)
    {
        // Find the university by ID
        var university = await _context.Universities.FindAsync(id);

        // Check if the university exists
        if (university == null)
        {
            return NotFound($"University with ID {id} not found.");
        }

        // Remove the university
        _context.Universities.Remove(university);
        await _context.SaveChangesAsync();

        // Return success response
        return NoContent();
    }
}
