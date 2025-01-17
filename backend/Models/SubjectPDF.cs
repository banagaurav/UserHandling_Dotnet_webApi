public class SubjectPDF
{
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    public int PDFId { get; set; }
    public PDF PDF { get; set; } = null!;
}
