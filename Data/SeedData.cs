using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;

namespace MyWebApi.Data
{
    public static class SeedData
    {

        public static void Seed(ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<User>();
            string hashedPassword = hasher.HashPassword(null, "admin123");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    Password = hasher.HashPassword(null, "admin123"), // Use hashed passwords in production
                    Email = "admin@example.com",
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    UserId = 2,
                    Username = "user1",
                    Password = hasher.HashPassword(null, "user123"), // Use hashed passwords in production
                    Email = "user1@example.com",
                    Role = "User",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
