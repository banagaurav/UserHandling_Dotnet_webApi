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

            // Configure the many-to-many relationship
            modelBuilder.Entity<UserPdf>()
                .HasKey(up => new { up.UserId, up.PdfId });  // Composite key

            modelBuilder.Entity<UserPdf>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPdfs)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserPdf>()
                .HasOne(up => up.Pdf)
                .WithMany(p => p.UserPdfs)
                .HasForeignKey(up => up.PdfId);

            // Seed Users with hashed passwords
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
                },
                new User
                {
                    UserId = 3,
                    Username = "user2",
                    Password = hasher.HashPassword(null, "user456"), // Use hashed passwords in production
                    Email = "user2@example.com",
                    Role = "User",
                    CreatedAt = new DateTime(2025, 1, 15, 10, 10, 0, DateTimeKind.Utc)
                },
                new User
                {
                    UserId = 4,
                    Username = "alice_jones",
                    Password = hasher.HashPassword(null, "alice789"), // Use hashed passwords in production
                    Email = "alice.jones@example.com",
                    Role = "User",
                    CreatedAt = new DateTime(2025, 1, 15, 10, 15, 0, DateTimeKind.Utc)
                },
                new User
                {
                    UserId = 5,
                    Username = "bob_white",
                    Password = hasher.HashPassword(null, "bob012"), // Use hashed passwords in production
                    Email = "bob.white@example.com",
                    Role = "Admin",
                    CreatedAt = new DateTime(2025, 1, 15, 10, 20, 0, DateTimeKind.Utc)
                }
            );

            modelBuilder.Entity<Pdf>().HasData(
                new Pdf { PdfId = 1, Title = "Introduction to C#", FilePath = "/pdfs/csharp_intro.pdf", Description = "A beginner's guide to C# programming." },
                new Pdf { PdfId = 2, Title = "Advanced C#", FilePath = "/pdfs/csharp_advanced.pdf", Description = "An advanced guide to C# programming." },
                new Pdf { PdfId = 3, Title = "Database Management Systems", FilePath = "/pdfs/dbms.pdf", Description = "Learn about database management systems." },
                new Pdf { PdfId = 4, Title = "Web Development Basics", FilePath = "/pdfs/web_development.pdf", Description = "Fundamentals of web development using HTML, CSS, and JavaScript." },
                new Pdf { PdfId = 5, Title = "Mobile App Development", FilePath = "/pdfs/mobile_app_development.pdf", Description = "Introduction to mobile app development for iOS and Android." }
            );

            // Seeding sample data (you can adjust as needed)
            modelBuilder.Entity<UserPdf>().HasData(
                new UserPdf { UserId = 1, PdfId = 1 },
                new UserPdf { UserId = 1, PdfId = 2 },
                new UserPdf { UserId = 2, PdfId = 3 },
                new UserPdf { UserId = 2, PdfId = 4 },
                new UserPdf { UserId = 3, PdfId = 5 }
            );
        }
    }
}
