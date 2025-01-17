public class AcademicProgram
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // Optional program code

    // Foreign key for Faculty
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; } = null!;

    // Navigation property for Subjects
    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
