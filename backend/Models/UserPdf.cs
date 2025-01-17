public class UserPDF
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int PDFId { get; set; }
    public PDF PDF { get; set; } = null!;
}
