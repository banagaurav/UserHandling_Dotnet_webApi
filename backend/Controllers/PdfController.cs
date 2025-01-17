using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PdfController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager; // To access user information

    public PdfController(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
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


}
