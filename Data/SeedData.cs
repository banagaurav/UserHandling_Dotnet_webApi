using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserHandling.Models;

namespace UserHandling.Data
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
                    CreatedAt = new DateTime(2025, 1, 15, 10, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    UserId = 2,
                    Username = "user1",
                    Password = hasher.HashPassword(null, "user123"), // Use hashed passwords in production
                    Email = "user1@example.com",
                    Role = "User",
                    CreatedAt = new DateTime(2025, 1, 15, 10, 5, 0, DateTimeKind.Utc) // Hardcoded value
                }
            );

            modelBuilder.Entity<Pdf>().HasData(
                new Pdf { PdfId = 1, Title = "Introduction to C#", FilePath = "/pdfs/csharp_intro.pdf", Description = "A beginner's guide to C# programming." },
                new Pdf { PdfId = 2, Title = "Advanced C#", FilePath = "/pdfs/csharp_advanced.pdf", Description = "An advanced guide to C# programming." },
                new Pdf { PdfId = 3, Title = "Database Management Systems", FilePath = "/pdfs/dbms.pdf", Description = "Learn about database management systems." },
                new Pdf { PdfId = 4, Title = "Web Development Basics", FilePath = "/pdfs/web_development.pdf", Description = "Fundamentals of web development using HTML, CSS, and JavaScript." },
                new Pdf { PdfId = 5, Title = "Mobile App Development", FilePath = "/pdfs/mobile_app_development.pdf", Description = "Introduction to mobile app development for iOS and Android." }
            );
        }
    }
}
