using UserHandling.DTOs;

public interface IPdfService
{
    Task<IEnumerable<PdfDto>> GetAllPdfsAsync();
    Task<PdfUploadDto> UploadPdfAsync(IFormFile file, string title, string description, int userId);
}