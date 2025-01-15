using System.ComponentModel.DataAnnotations;

namespace UserHandling.Models;
public class Pdf
{
    [Key]
    public int PdfId { get; set; }  // Primary Key
    public string Title { get; set; }
    public string FilePath { get; set; }  // Path where the PDF is stored
    public string Description { get; set; }

    public ICollection<UserPdf> UserPdfs { get; set; }
}
