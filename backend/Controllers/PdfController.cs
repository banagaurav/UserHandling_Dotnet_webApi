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
            .Select(pdf => new PdfDto
            {
                Id = pdf.Id,
                FileName = pdf.FileName
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

        return File(pdf.FileData, "application/pdf", pdf.FileName);
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