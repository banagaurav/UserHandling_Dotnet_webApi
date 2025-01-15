namespace UserHandling.Models;
public class UserPdf
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int PdfId { get; set; }
    public Pdf Pdf { get; set; }
}
