using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PDFs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDFs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faculties_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPDFs",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PDFId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPDFs", x => new { x.UserId, x.PDFId });
                    table.ForeignKey(
                        name: "FK_UserPDFs_PDFs_PDFId",
                        column: x => x.PDFId,
                        principalTable: "PDFs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPDFs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacultyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicPrograms_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditHours = table.Column<int>(type: "int", nullable: false),
                    AcademicProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectPDFs",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PDFId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectPDFs", x => new { x.SubjectId, x.PDFId });
                    table.ForeignKey(
                        name: "FK_SubjectPDFs_PDFs_PDFId",
                        column: x => x.PDFId,
                        principalTable: "PDFs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectPDFs_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Universities",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "", "PU" },
                    { 2, "", "TU" }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "Name", "UniversityId" },
                values: new object[,]
                {
                    { 1, "Faculty of Engineering", 1 },
                    { 2, "Faculty of Science", 1 },
                    { 3, "Faculty of Business", 2 },
                    { 4, "Faculty of Arts", 2 }
                });

            migrationBuilder.InsertData(
                table: "AcademicPrograms",
                columns: new[] { "Id", "Code", "FacultyId", "Name" },
                values: new object[,]
                {
                    { 1, "", 1, "BSc Computer Science" },
                    { 2, "", 2, "BSc Physics" },
                    { 3, "", 3, "MBA" },
                    { 4, "", 4, "BA English" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "AcademicProgramId", "Code", "CreditHours", "Name" },
                values: new object[,]
                {
                    { 1, 1, "", 3, "Data Structures" },
                    { 2, 1, "", 3, "Operating Systems" },
                    { 3, 2, "", 4, "Quantum Mechanics" },
                    { 4, 3, "", 3, "Financial Management" },
                    { 5, 4, "", 2, "English Literature" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPrograms_FacultyId",
                table: "AcademicPrograms",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_UniversityId",
                table: "Faculties",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPDFs_PDFId",
                table: "SubjectPDFs",
                column: "PDFId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_AcademicProgramId",
                table: "Subjects",
                column: "AcademicProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPDFs_PDFId",
                table: "UserPDFs",
                column: "PDFId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectPDFs");

            migrationBuilder.DropTable(
                name: "UserPDFs");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "PDFs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AcademicPrograms");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
