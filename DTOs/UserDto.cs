namespace UserHandling.DTOs;
public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
}
