using Microsoft.EntityFrameworkCore;
using UserHandling.Models;

namespace UserHandling.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Pdf> Pdfs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Call seed logic
            SeedData.Seed(modelBuilder);
        }
    }
}
