public class SubjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    // Add the AcademicProgram property as an object of AcademicProgramDto
    public AcademicProgramDto AcademicProgram { get; set; } = new AcademicProgramDto();
}
