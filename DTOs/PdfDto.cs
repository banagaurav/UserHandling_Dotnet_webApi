namespace UserHandling.DTOs;

public class PdfDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> Authors { get; set; } // List of authors who uploaded the PDF
}
