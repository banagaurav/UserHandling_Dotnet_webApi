public class AcademicProgramDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public int FacultyId { get; set; }
    public string FacultyName { get; set; } = string.Empty;

    public int UniversityId { get; set; }
    public string UniversityName { get; set; } = string.Empty;
}
