public class AcademicProgramDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

    public FacultyDto Faculty { get; set; } = new FacultyDto();
}
