using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using UserHandling.DTOs;
using UserHandling.Models;
using UserHandling.Repositories;

public class PdfService : IPdfService
{
    private readonly IPdfRepository _pdfRepository;
    private readonly MappingService _MappingService;
    private readonly IWebHostEnvironment _environment;

    public PdfService(IPdfRepository pdfRepository, MappingService MappingService, IWebHostEnvironment environment)
    {
        _pdfRepository = pdfRepository;
        _MappingService = MappingService;
        _environment = environment;
    }

    public async Task<IEnumerable<PdfDto>> GetAllPdfsAsync()
    {
        var pdfs = await _pdfRepository.GetAllPdfsAsync();
        return _MappingService.MapToPdfDtos(pdfs);
    }

    public async Task<PdfUploadDto> UploadPdfAsync(IFormFile file, string title, string description, int userId)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Invalid file");
        }

        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var filePath = Path.Combine(uploadsFolder, file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var pdf = new Pdf
        {
            Title = title,
            Description = description,
            FilePath = $"/uploads/{file.FileName}"
        };

        await _pdfRepository.SavePdfAsync(pdf, userId);

        return _MappingService.MapToPdfUploadDto(pdf);
    }
}
