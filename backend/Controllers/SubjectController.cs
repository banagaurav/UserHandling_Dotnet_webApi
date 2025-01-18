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
    [HttpGet]
    // Get all subjects[HttpGet]
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
                AcademicProgram = new AcademicProgramDto
                {
                    Id = s.AcademicProgram.Id,
                    Name = s.AcademicProgram.Name,
                    Code = s.AcademicProgram.Code,
                    Faculty = new FacultyDto
                    {
                        Id = s.AcademicProgram.Faculty.Id,
                        Name = s.AcademicProgram.Faculty.Name,
                        University = new UniversityDto
                        {
                            Id = s.AcademicProgram.Faculty.University.Id,
                            Name = s.AcademicProgram.Faculty.University.Name,
                            Location = s.AcademicProgram.Faculty.University.Location
                        }
                    }
                }
            })
            .ToListAsync();

        return Ok(subjects);
    }


    // Upload a subject
    [HttpPost]
    public async Task<ActionResult<SubjectDto>> UploadSubject(SubjectDto subjectDto)
    {
        // Check if the referenced AcademicProgram exists
        var academicProgram = await _context.AcademicPrograms
            .Include(ap => ap.Faculty) // Include the Faculty details
            .ThenInclude(f => f.University) // Include the University details
            .FirstOrDefaultAsync(ap => ap.Id == subjectDto.AcademicProgram.Id);

        if (academicProgram == null)
        {
            return BadRequest($"AcademicProgram with ID {subjectDto.AcademicProgram.Id} not found.");
        }

        // Create the Subject entity
        var subject = new Subject
        {
            Name = subjectDto.Name,
            Code = subjectDto.Code,
            CreditHours = subjectDto.CreditHours,
            AcademicProgramId = academicProgram.Id
        };

        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        // Return the uploaded subject as a DTO with nested objects
        var createdSubjectDto = new SubjectDto
        {
            Id = subject.Id,
            Name = subject.Name,
            Code = subject.Code,
            CreditHours = subject.CreditHours,
            AcademicProgram = new AcademicProgramDto
            {
                Id = academicProgram.Id,
                Name = academicProgram.Name,
                Code = academicProgram.Code,
                Faculty = new FacultyDto
                {
                    Id = academicProgram.Faculty.Id,
                    Name = academicProgram.Faculty.Name,
                    University = new UniversityDto
                    {
                        Id = academicProgram.Faculty.University.Id,
                        Name = academicProgram.Faculty.University.Name,
                        Location = academicProgram.Faculty.University.Location
                    }
                }
            }
        };

        return CreatedAtAction(nameof(GetAllSubjects), new { id = subject.Id }, createdSubjectDto);
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
        var academicProgram = await _context.AcademicPrograms
            .Include(ap => ap.Faculty) // Include Faculty
            .ThenInclude(f => f.University) // Include University
            .FirstOrDefaultAsync(ap => ap.Id == subjectDto.AcademicProgram.Id);

        if (academicProgram == null)
        {
            return BadRequest($"AcademicProgram with ID {subjectDto.AcademicProgram.Id} not found.");
        }

        // Update the subject details
        subject.Name = subjectDto.Name;
        subject.Code = subjectDto.Code;
        subject.CreditHours = subjectDto.CreditHours;
        subject.AcademicProgramId = academicProgram.Id;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpPost("add-subject")]
    public async Task<ActionResult<SubjectDto>> CreateSubject(SubjectDto subjectDto)
    {
        // Validate the AcademicProgram existence
        var academicProgram = await _context.AcademicPrograms
            .Include(ap => ap.Faculty) // Include related Faculty
            .ThenInclude(f => f.University) // Include related University
            .FirstOrDefaultAsync(ap => ap.Id == subjectDto.AcademicProgram.Id);

        if (academicProgram == null)
        {
            return BadRequest($"AcademicProgram with ID {subjectDto.AcademicProgram.Id} not found.");
        }

        // Create the new Subject entity
        var subject = new Subject
        {
            Name = subjectDto.Name,
            Code = subjectDto.Code,
            CreditHours = subjectDto.CreditHours,
            AcademicProgramId = academicProgram.Id
        };

        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        // Map the created subject to the nested DTO structure
        var createdSubjectDto = new SubjectDto
        {
            Id = subject.Id,
            Name = subject.Name,
            Code = subject.Code,
            CreditHours = subject.CreditHours,
            AcademicProgram = new AcademicProgramDto
            {
                Id = academicProgram.Id,
                Name = academicProgram.Name,
                Code = academicProgram.Code,
                Faculty = new FacultyDto
                {
                    Id = academicProgram.Faculty.Id,
                    Name = academicProgram.Faculty.Name,
                    University = new UniversityDto
                    {
                        Id = academicProgram.Faculty.University.Id,
                        Name = academicProgram.Faculty.University.Name,
                        Location = academicProgram.Faculty.University.Location
                    }
                }
            }
        };

        // Return the created Subject
        return CreatedAtAction(nameof(GetAllSubjects), new { id = subject.Id }, createdSubjectDto);
    }


}
