using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<PDF> PDFs { get; set; } = null!;
    public DbSet<UserPDF> UserPDFs { get; set; } = null!;


    public DbSet<University> Universities { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<AcademicProgram> AcademicPrograms { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    public DbSet<SubjectPDF> SubjectPDFs { get; set; }

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


        // University-Faculty relationship
        modelBuilder.Entity<Faculty>()
            .HasOne(f => f.University)
            .WithMany(u => u.Faculties)
            .HasForeignKey(f => f.UniversityId);

        // Faculty-AcademicProgram relationship
        modelBuilder.Entity<AcademicProgram>()
            .HasOne(ap => ap.Faculty)
            .WithMany(f => f.AcademicPrograms)
            .HasForeignKey(ap => ap.FacultyId);

        // AcademicProgram-Subject relationship
        modelBuilder.Entity<Subject>()
            .HasOne(s => s.AcademicProgram)
            .WithMany(ap => ap.Subjects)
            .HasForeignKey(s => s.AcademicProgramId);


        // Configure Subject-PDF relationship
        modelBuilder.Entity<SubjectPDF>()
            .HasKey(sp => new { sp.SubjectId, sp.PDFId });

        modelBuilder.Entity<SubjectPDF>()
            .HasOne(sp => sp.Subject)
            .WithMany(s => s.SubjectPDFs)
            .HasForeignKey(sp => sp.SubjectId);

        modelBuilder.Entity<SubjectPDF>()
            .HasOne(sp => sp.PDF)
            .WithMany(p => p.SubjectPDFs)
            .HasForeignKey(sp => sp.PDFId);
    }
}
