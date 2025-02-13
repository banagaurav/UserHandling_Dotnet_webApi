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
            .HasForeignKey(up => up.PDFId)
            .OnDelete(DeleteBehavior.Cascade);


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
            .HasForeignKey(sp => sp.PDFId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<University>().HasData(
            new University { Id = 1, Name = "PU" },
            new University { Id = 2, Name = "TU" }
            );

        // Seed data for Faculties
        modelBuilder.Entity<Faculty>().HasData(
            new Faculty { Id = 1, Name = "Faculty of Engineering", UniversityId = 1 },
            new Faculty { Id = 2, Name = "Faculty of Science", UniversityId = 1 },
            new Faculty { Id = 3, Name = "Faculty of Business", UniversityId = 2 },
            new Faculty { Id = 4, Name = "Faculty of Arts", UniversityId = 2 }
        );

        // Seed data for Academic Programs
        modelBuilder.Entity<AcademicProgram>().HasData(
            new AcademicProgram { Id = 1, Name = "BSc Computer Science", FacultyId = 1 },
            new AcademicProgram { Id = 2, Name = "BSc Physics", FacultyId = 2 },
            new AcademicProgram { Id = 3, Name = "MBA", FacultyId = 3 },
            new AcademicProgram { Id = 4, Name = "BA English", FacultyId = 4 }
        );

        // Seed data for Subjects
        modelBuilder.Entity<Subject>().HasData(
            new Subject { Id = 1, Name = "Data Structures", AcademicProgramId = 1, CreditHours = 3 },
            new Subject { Id = 2, Name = "Operating Systems", AcademicProgramId = 1, CreditHours = 3 },
            new Subject { Id = 3, Name = "Quantum Mechanics", AcademicProgramId = 2, CreditHours = 4 },
            new Subject { Id = 4, Name = "Financial Management", AcademicProgramId = 3, CreditHours = 3 },
            new Subject { Id = 5, Name = "English Literature", AcademicProgramId = 4, CreditHours = 2 }
        );

    }
}
