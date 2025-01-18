public class UploadPdfDto
{
    public IFormFile PdfFile { get; set; }
    public int SubjectId { get; set; }
    public int AcademicProgramId { get; set; }
    public int FacultyId { get; set; }
    public int UniversityId { get; set; }
}
