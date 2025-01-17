public class PdfDto
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public List<SubjectDto> Subjects { get; set; } = new List<SubjectDto>();
}
