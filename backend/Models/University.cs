public class University
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    // Navigation property for Faculties
    public ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();
}
