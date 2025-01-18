public class PdfDtoUser
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public List<UserDto> Users { get; set; } = new List<UserDto>();
    public List<SubjectDto> Subjects { get; set; } = new List<SubjectDto>();

}
