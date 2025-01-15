public class Pdf
{
    public int PdfId { get; set; }  // Primary Key
    public string Title { get; set; }
    public string FilePath { get; set; }  // Path where the PDF is stored
    public string Description { get; set; }
}
