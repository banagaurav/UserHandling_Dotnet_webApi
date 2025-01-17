using System.ComponentModel.DataAnnotations;

public class PDF
{
    [Key]
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public byte[] FileData { get; set; } = Array.Empty<byte>();

    // Navigation property for related Users
    public ICollection<UserPDF> UserPDFs { get; set; } = new List<UserPDF>();
}
