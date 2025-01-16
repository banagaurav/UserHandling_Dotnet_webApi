using UserHandling.Models;

public interface IPdfRepository
{
    Task<IEnumerable<Pdf>> GetAllPdfsAsync();
    Task SavePdfAsync(Pdf pdf, int userId);
}
