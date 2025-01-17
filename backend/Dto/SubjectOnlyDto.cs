namespace Backend.Dto;
public class SubjectOnlyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int CreditHours { get; set; }
}
