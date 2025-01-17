using Backend.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class SubjectController : ControllerBase
{
    private readonly AppDbContext _context;

    public SubjectController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("subjects-only")]
    public async Task<ActionResult<IEnumerable<SubjectOnlyDto>>> GetSubjectsOnly()
    {
        var subjects = await _context.Subjects
            .Select(s => new SubjectOnlyDto
            {
                Id = s.Id,
                Name = s.Name,
                Code = s.Code,
                CreditHours = s.CreditHours
            })
            .ToListAsync();

        return Ok(subjects);
    }

    // Get all subjects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubjectDto>>> GetAllSubjects()
    {
        var subjects = await _context.Subjects
            .Include(s => s.AcademicProgram)  // Include related AcademicProgram
            .ThenInclude(ap => ap.Faculty)   // Include related Faculty
            .ThenInclude(f => f.University) // Include related University
            .Select(s => new SubjectDto
            {
                Id = s.Id,
                Name = s.Name,
                Code = s.Code,
                CreditHours = s.CreditHours,
                AcademicProgramId = s.AcademicProgramId,
                AcademicProgramName = s.AcademicProgram.Name,
                FacultyId = s.AcademicProgram.FacultyId,
                FacultyName = s.AcademicProgram.Faculty.Name,
                UniversityId = s.AcademicProgram.Faculty.UniversityId,
                UniversityName = s.AcademicProgram.Faculty.University.Name
            })
            .ToListAsync();

        return Ok(subjects);
    }


    // Upload a subject
    [HttpPost]
    public async Task<ActionResult<SubjectDto>> UploadSubject(SubjectDto subjectDto)
    {

        // Check if the referenced AcademicProgram exists
        var academicProgram = await _context.AcademicPrograms.FindAsync(subjectDto.AcademicProgramId);
        if (academicProgram == null)
        {
            return BadRequest($"AcademicProgram with ID {subjectDto.AcademicProgramId} not found.");
        }

        // Create the Subject entity
        var subject = new Subject
        {
            Name = subjectDto.Name,
            Code = subjectDto.Code,
            CreditHours = subjectDto.CreditHours,
            AcademicProgramId = subjectDto.AcademicProgramId
        };

        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        // Return the uploaded subject as a DTO
        subjectDto.Id = subject.Id;
        return CreatedAtAction(nameof(GetAllSubjects), new { id = subject.Id }, subjectDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubject(int id, SubjectDto subjectDto)
    {
        // Check if the subject exists
        var subject = await _context.Subjects.FindAsync(id);
        if (subject == null)
        {
            return NotFound($"Subject with ID {id} not found.");
        }

        // Check if the referenced AcademicProgram exists
        var academicProgram = await _context.AcademicPrograms.FindAsync(subjectDto.AcademicProgramId);
        if (academicProgram == null)
        {
            return BadRequest($"AcademicProgram with ID {subjectDto.AcademicProgramId} not found.");
        }

        // Update the subject details
        subject.Name = subjectDto.Name;
        subject.Code = subjectDto.Code;
        subject.CreditHours = subjectDto.CreditHours;
        subject.AcademicProgramId = subjectDto.AcademicProgramId;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
