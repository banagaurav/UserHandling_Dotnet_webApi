using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider, AppDbContext context)
    {
        // Ensure the database is created
        context.Database.EnsureCreated();

        // Check if the data has already been seeded to avoid seeding duplicate data
        if (context.Universities.Any())
        {
            return;   // Database has been seeded
        }

        // Seed Universities
        var universityTU = new University { Name = "TU", Location = "Kathmandu" };
        var universityPU = new University { Name = "PU", Location = "Pokhara" };

        context.Universities.AddRange(universityTU, universityPU);
        context.SaveChanges();

        // Seed Faculties
        var facultyEngineeringTU = new Faculty { Name = "Faculty of Engineering", UniversityId = universityTU.Id };
        var facultyScienceTU = new Faculty { Name = "Faculty of Science", UniversityId = universityTU.Id };
        var facultyBusinessPU = new Faculty { Name = "Faculty of Business", UniversityId = universityPU.Id };
        var facultyArtsPU = new Faculty { Name = "Faculty of Arts", UniversityId = universityPU.Id };

        context.Faculties.AddRange(facultyEngineeringTU, facultyScienceTU, facultyBusinessPU, facultyArtsPU);
        context.SaveChanges();

        // Seed Academic Programs
        var programCS = new AcademicProgram { Name = "Computer Science", FacultyId = facultyEngineeringTU.Id };
        var programEE = new AcademicProgram { Name = "Electrical Engineering", FacultyId = facultyEngineeringTU.Id };
        var programPhysics = new AcademicProgram { Name = "Physics", FacultyId = facultyScienceTU.Id };
        var programMath = new AcademicProgram { Name = "Mathematics", FacultyId = facultyScienceTU.Id };
        var programBA = new AcademicProgram { Name = "Business Administration", FacultyId = facultyBusinessPU.Id };
        var programEco = new AcademicProgram { Name = "Economics", FacultyId = facultyBusinessPU.Id };
        var programHistory = new AcademicProgram { Name = "History", FacultyId = facultyArtsPU.Id };
        var programPhilosophy = new AcademicProgram { Name = "Philosophy", FacultyId = facultyArtsPU.Id };

        context.AcademicPrograms.AddRange(programCS, programEE, programPhysics, programMath, programBA, programEco, programHistory, programPhilosophy);
        context.SaveChanges();

        // Seed Subjects
        var subjectDS = new Subject { Name = "Data Structures", AcademicProgramId = programCS.Id };
        var subjectAlgo = new Subject { Name = "Algorithms", AcademicProgramId = programCS.Id };
        var subjectCircuit = new Subject { Name = "Circuit Theory", AcademicProgramId = programEE.Id };
        var subjectControl = new Subject { Name = "Control Systems", AcademicProgramId = programEE.Id };
        var subjectMgmt = new Subject { Name = "Management", AcademicProgramId = programBA.Id };
        var subjectFinance = new Subject { Name = "Finance", AcademicProgramId = programBA.Id };
        var subjectMicroeco = new Subject { Name = "Microeconomics", AcademicProgramId = programEco.Id };
        var subjectMacroeco = new Subject { Name = "Macroeconomics", AcademicProgramId = programEco.Id };
        var subjectAncientHistory = new Subject { Name = "Ancient History", AcademicProgramId = programHistory.Id };
        var subjectModernHistory = new Subject { Name = "Modern History", AcademicProgramId = programHistory.Id };
        var subjectLogic = new Subject { Name = "Logic", AcademicProgramId = programPhilosophy.Id };
        var subjectEthics = new Subject { Name = "Ethics", AcademicProgramId = programPhilosophy.Id };

        context.Subjects.AddRange(subjectDS, subjectAlgo, subjectCircuit, subjectControl, subjectMgmt, subjectFinance, subjectMicroeco, subjectMacroeco, subjectAncientHistory, subjectModernHistory, subjectLogic, subjectEthics);
        context.SaveChanges();
    }
}
