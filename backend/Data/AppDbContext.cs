using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<PDF> PDFs { get; set; } = null!;
    public DbSet<UserPDF> UserPDFs { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the UserPDF relationship
        modelBuilder.Entity<UserPDF>()
            .HasKey(up => new { up.UserId, up.PDFId }); // Composite Key

        modelBuilder.Entity<UserPDF>()
            .HasOne(up => up.User)
            .WithMany(u => u.UserPDFs)
            .HasForeignKey(up => up.UserId);

        modelBuilder.Entity<UserPDF>()
            .HasOne(up => up.PDF)
            .WithMany(p => p.UserPDFs)
            .HasForeignKey(up => up.PDFId);
    }
}
