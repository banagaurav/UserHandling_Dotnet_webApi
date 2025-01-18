using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PdfController : ControllerBase
{
    private readonly AppDbContext _context;

    public PdfController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PdfDto>>> GetAllPdfs()
    {
        var pdfs = await _context.PDFs
            .Include(p => p.SubjectPDFs)
                .ThenInclude(s => s.Subject)
                    .ThenInclude(sub => sub.AcademicProgram)
                    .ThenInclude(ap => ap.Faculty)
                        .ThenInclude(f => f.University)
            .Select(p => new PdfDto
            {
                Id = p.Id,
                FileName = p.FileName,
                Subjects = p.SubjectPDFs.Select(s => new SubjectDto
                {
                    Id = s.Subject.Id,
                    Name = s.Subject.Name,
                    Code = s.Subject.Code,
                    CreditHours = s.Subject.CreditHours,
                    AcademicProgramName = s.Subject.AcademicProgram.Name,
                    FacultyName = s.Subject.AcademicProgram.Faculty.Name,
                    UniversityName = s.Subject.AcademicProgram.Faculty.University.Name
                }).ToList()
            })
            .ToListAsync();

        if (pdfs.Count == 0)
        {
            return NotFound("No PDFs found.");
        }

        return Ok(pdfs);
    }

    // GET: api/pdf/{pdfId}
    [HttpGet("{pdfId}")]
    public async Task<ActionResult> GetPdfById(int pdfId)
    {
        var pdf = await _context.PDFs
            .FirstOrDefaultAsync(p => p.Id == pdfId);

        if (pdf == null)
        {
            return NotFound("PDF not found.");
        }

        var response = new
        {
            FileName = pdf.FileName,
            FileData = Convert.ToBase64String(pdf.FileData) // Encode file data as base64
        };

        return Ok(response);
        // return File(pdf.FileData, "application/pdf", pdf.FileName);
    }
    [HttpGet("all-with-User")]
    public async Task<ActionResult<IEnumerable<PdfDtoUser>>> GetAllPdfWithUser()
    {
        var PDFs = await _context.PDFs
            .Include(pdf => pdf.UserPDFs)
                .ThenInclude(up => up.User)
            .Include(pdf => pdf.SubjectPDFs)
                .ThenInclude(sp => sp.Subject)
                .ThenInclude(sub => sub.AcademicProgram)
                .ThenInclude(ap => ap.Faculty)
                .ThenInclude(f => f.University)
            .Select(pdf => new PdfDtoUser
            {
                Id = pdf.Id,
                FileName = pdf.FileName,
                Users = pdf.UserPDFs.Select(up => new UserDto
                {
                    Id = up.User.Id,
                    FullName = up.User.FullName,
                    Email = up.User.Email,
                    Username = up.User.Username,
                    Role = up.User.Role
                }).ToList(),
                Subjects = pdf.SubjectPDFs.Select(sp => new SubjectDto
                {
                    Id = sp.Subject.Id,
                    Name = sp.Subject.Name,
                    Code = sp.Subject.Code,
                    CreditHours = sp.Subject.CreditHours,
                    AcademicProgramId = sp.Subject.AcademicProgramId,
                    AcademicProgramName = sp.Subject.AcademicProgram.Name,
                    FacultyId = sp.Subject.AcademicProgram.FacultyId,
                    FacultyName = sp.Subject.AcademicProgram.Faculty.Name,
                    UniversityId = sp.Subject.AcademicProgram.Faculty.UniversityId,
                    UniversityName = sp.Subject.AcademicProgram.Faculty.University.Name
                }).ToList()
            })
            .ToListAsync();

        if (PDFs == null || PDFs.Count == 0)
        {
            return NotFound("No PDFs found.");
        }

        return Ok(PDFs);
    }
    [HttpPost("upload")]
    [Authorize] // Ensure the user is authenticated
    public async Task<ActionResult> UploadPdf([FromForm] UploadPdfDto uploadPdfDto)
    {
        // Get the logged-in user from JWT claims
        var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User is not authenticated.");
        }

        // Check if the file is valid
        if (uploadPdfDto.PdfFile == null || uploadPdfDto.PdfFile.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        // Check if the Subject, AcademicProgram, Faculty, and University exist
        var subject = await _context.Subjects.FindAsync(uploadPdfDto.SubjectId);
        var academicProgram = await _context.AcademicPrograms.FindAsync(uploadPdfDto.AcademicProgramId);
        var faculty = await _context.Faculties.FindAsync(uploadPdfDto.FacultyId);
        var university = await _context.Universities.FindAsync(uploadPdfDto.UniversityId);

        if (subject == null || academicProgram == null || faculty == null || university == null)
        {
            return BadRequest("Invalid Subject, Academic Program, Faculty, or University.");
        }

        // Save the file data to the database
        var pdf = new PDF
        {
            FileName = uploadPdfDto.PdfFile.FileName,
            FileData = await ConvertFileToByteArray(uploadPdfDto.PdfFile),
        };

        // Add the PDF to the database
        _context.PDFs.Add(pdf);
        await _context.SaveChangesAsync();

        // Create a junction entry to associate this PDF with the logged-in user
        var userPdf = new UserPDF
        {
            UserId = int.Parse(userId),  // Assuming userId is an integer in your system
            PDFId = pdf.Id
        };

        _context.UserPDFs.Add(userPdf);
        await _context.SaveChangesAsync();

        // Create the association between PDF, Subject, Academic Program, Faculty, and University
        var subjectPdf = new SubjectPDF
        {
            SubjectId = uploadPdfDto.SubjectId,
            PDFId = pdf.Id
        };

        _context.SubjectPDFs.Add(subjectPdf);
        await _context.SaveChangesAsync();

        return Ok(new { message = "File uploaded successfully", pdfId = pdf.Id });
    }

    // Helper method to convert the uploaded file to a byte array
    private async Task<byte[]> ConvertFileToByteArray(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    [HttpDelete("delete/{id}")]
    [Authorize] // Ensure the user is authenticated
    public async Task<ActionResult> DeletePdf(int id)
    {
        // Get the logged-in user from JWT claims
        var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("User is not authenticated.");
        }

        // Find the PDF by its ID
        var pdf = await _context.PDFs.FindAsync(id);

        if (pdf == null)
        {
            return NotFound("PDF not found.");
        }

        // Check if the logged-in user is the one who uploaded the PDF or an admin
        var userRole = User.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

        if (pdf.UserPDFs.All(up => up.UserId.ToString() != userId) && userRole != "Admin")
        {
            return Unauthorized("You can only delete PDFs that you uploaded or if you are an admin.");
        }

        // Remove the PDF from the database (cascade delete will handle related records)
        _context.PDFs.Remove(pdf);
        await _context.SaveChangesAsync();

        return Ok(new { message = "PDF deleted successfully." });
    }

}