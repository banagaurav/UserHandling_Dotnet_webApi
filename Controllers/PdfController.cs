using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UserHandling.DTOs;
using UserHandling.Services;

namespace UserHandling.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowFrontend")]
    public class PdfController : ControllerBase
    {
        private readonly IPdfService _pdfService;

        public PdfController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PdfDto>>> GetAllPdfs()
        {
            var pdfs = await _pdfService.GetAllPdfsAsync();
            return Ok(pdfs);
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<ActionResult<PdfUploadDto>> UploadPdf([FromForm] IFormFile file, [FromForm] string title, [FromForm] string description)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            // Get user ID from the authenticated user
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
            {
                return Unauthorized("User ID not found.");
            }

            var pdfDto = await _pdfService.UploadPdfAsync(file, title, description, userId);
            return CreatedAtAction(nameof(GetAllPdfs), new { id = pdfDto.PdfId }, pdfDto);
        }
    }
}
