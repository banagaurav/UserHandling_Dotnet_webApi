using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserHandling.DTOs;
using UserHandling.Services;

namespace UserHandling.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}
