using UserHandling.DTOs;

public class PdfService : IPdfService
{
    private readonly IPdfRepository _pdfRepository;
    private readonly MappingService _MappingService;

    public PdfService(IPdfRepository pdfRepository, MappingService MappingService)
    {
        _pdfRepository = pdfRepository;
        _MappingService = MappingService;
    }

    public async Task<IEnumerable<PdfDto>> GetAllPdfsAsync()
    {
        var pdfs = await _pdfRepository.GetAllPdfsAsync();
        return _MappingService.MapToPdfDtos(pdfs);
    }
}