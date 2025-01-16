using UserHandling.DTOs;

public interface IPdfService
{
    Task<IEnumerable<PdfDto>> GetAllPdfsAsync();
}