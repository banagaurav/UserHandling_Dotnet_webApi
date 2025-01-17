using System.ComponentModel.DataAnnotations;

namespace backend.Models;
public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public string Role { get; set; } = "Client"; // Default role
}
