using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class FacultyController : ControllerBase
{
    private readonly AppDbContext _context;

    public FacultyController(AppDbContext context)
    {
        _context = context;
    }

    // GET all Faculties
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FacultyDto>>> GetAllFaculties()
    {
        var faculties = await _context.Faculties
            .Include(f => f.University) // Include the University data in the response
            .Select(f => new FacultyDto
            {
                Id = f.Id,
                Name = f.Name,
                University = new UniversityDto
                {
                    Id = f.University.Id,
                    Name = f.University.Name,
                    Location = f.University.Location
                }
            })
            .ToListAsync();

        return Ok(faculties);
    }


    // POST a new Faculty and associate it with an existing University
    [HttpPost]
    public async Task<ActionResult<FacultyDto>> CreateFaculty(FacultyDto facultyDto)
    {
        // Validate FacultyDto
        if (string.IsNullOrWhiteSpace(facultyDto.Name))
        {
            return BadRequest("Faculty name is required.");
        }

        // Check if the University exists
        var university = await _context.Universities
            .FirstOrDefaultAsync(u => u.Id == facultyDto.University.Id);

        if (university == null)
        {
            return NotFound("University not found.");
        }

        try
        {
            // Create the new Faculty
            var faculty = new Faculty
            {
                Name = facultyDto.Name,
                UniversityId = university.Id
            };

            _context.Faculties.Add(faculty);
            await _context.SaveChangesAsync();

            // Return the created Faculty data with University details
            var createdFacultyDto = new FacultyDto
            {
                Id = faculty.Id,
                Name = faculty.Name,
                University = new UniversityDto
                {
                    Id = university.Id,
                    Name = university.Name,
                    Location = university.Location
                }
            };

            return CreatedAtAction(nameof(GetAllFaculties), new { id = faculty.Id }, createdFacultyDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFaculty(int id)
    {
        // Find the Faculty by ID
        var faculty = await _context.Faculties
            .Include(f => f.University) // Optional: You can include the related University data if needed
            .FirstOrDefaultAsync(f => f.Id == id);

        // If the Faculty does not exist, return NotFound
        if (faculty == null)
        {
            return NotFound($"Faculty with ID {id} not found.");
        }

        // Remove the Faculty from the database
        _context.Faculties.Remove(faculty);
        await _context.SaveChangesAsync();

        // Return a response indicating successful deletion
        return NoContent(); // 204 No Content (successful delete with no data in response)
    }

}

