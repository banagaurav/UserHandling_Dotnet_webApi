using UserHandling.Models;

public interface IPdfRepository
{
    Task<IEnumerable<Pdf>> GetAllPdfsAsync();
}
