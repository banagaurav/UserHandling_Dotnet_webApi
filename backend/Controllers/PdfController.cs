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
        .Select(p => new PdfDto
        {
            Id = p.Id,
            FileName = p.FileName,
            Subjects = p.SubjectPDFs.Select(s => new SubjectDto
            {
                Id = s.Subject.Id,
                Name = s.Subject.Name,
                Code = s.Subject.Code,
                CreditHours = s.Subject.CreditHours
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
            .Include(pdf => pdf.UserPDFs)      // Include the junction table
            .ThenInclude(up => up.User)       // Include the related User entity
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

        return Ok(new { message = "File uploaded successfully", pdfId = pdf.Id });
    }

    // Helper method to convert the uploaded file to a byte array
    private async Task<byte[]> ConvertFileToByteArray(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}