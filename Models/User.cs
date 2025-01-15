namespace MyWebApi.Models
{
    public class User
    {
        public int UserId { get; set; } // Primary key

        public string Username { get; set; } = string.Empty; // Required, unique

        public string Password { get; set; } = string.Empty; // Required, hashed in real-world applications

        public string Email { get; set; } = string.Empty; // Required, valid email format

        public string Role { get; set; } = "User"; // Default role, e.g., Admin/User

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for creation
    }
}
