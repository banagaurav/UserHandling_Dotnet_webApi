public class Faculty
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Foreign key for University
    public int UniversityId { get; set; }
    public University University { get; set; } = null!;

    // Navigation property for AcademicPrograms
    public ICollection<AcademicProgram> AcademicPrograms { get; set; } = new List<AcademicProgram>();
}
