public class FacultyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public UniversityDto University { get; set; } = new UniversityDto();
}
