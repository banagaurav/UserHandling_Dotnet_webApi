public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // Optional subject code
    public int CreditHours { get; set; } // Optional

    // Foreign key for AcademicProgram
    public int AcademicProgramId { get; set; }
    public AcademicProgram AcademicProgram { get; set; } = null!;
}
