using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserHandling.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdditionalUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pdfs",
                columns: table => new
                {
                    PdfId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pdfs", x => x.PdfId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserPdfs",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PdfId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPdfs", x => new { x.UserId, x.PdfId });
                    table.ForeignKey(
                        name: "FK_UserPdfs_Pdfs_PdfId",
                        column: x => x.PdfId,
                        principalTable: "Pdfs",
                        principalColumn: "PdfId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPdfs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pdfs",
                columns: new[] { "PdfId", "Description", "FilePath", "Title" },
                values: new object[,]
                {
                    { 1, "A beginner's guide to C# programming.", "/pdfs/csharp_intro.pdf", "Introduction to C#" },
                    { 2, "An advanced guide to C# programming.", "/pdfs/csharp_advanced.pdf", "Advanced C#" },
                    { 3, "Learn about database management systems.", "/pdfs/dbms.pdf", "Database Management Systems" },
                    { 4, "Fundamentals of web development using HTML, CSS, and JavaScript.", "/pdfs/web_development.pdf", "Web Development Basics" },
                    { 5, "Introduction to mobile app development for iOS and Android.", "/pdfs/mobile_app_development.pdf", "Mobile App Development" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), "admin@example.com", "AQAAAAIAAYagAAAAEE55v591Wy9/V2Sl4/9gOkx8cyUywIJ5bXOR3eOd03sCitN8cO7c/tN/4iUM3ONgSA==", "Admin", "admin" },
                    { 2, new DateTime(2025, 1, 15, 10, 5, 0, 0, DateTimeKind.Utc), "user1@example.com", "AQAAAAIAAYagAAAAEFqC+9F0gQNCLEVTz8ES4ZLQWrprMd7C5LvCq01NALKZSJMpZg1eCZ7/FNsmQk3img==", "User", "user1" },
                    { 3, new DateTime(2025, 1, 15, 10, 10, 0, 0, DateTimeKind.Utc), "user2@example.com", "AQAAAAIAAYagAAAAEMMaHQ8i4KZbcPQIT1lVueRfT0UEzxqCwu1pZRAhaDOEOIG2ggqELYNvtNCY/e5jQg==", "User", "user2" },
                    { 4, new DateTime(2025, 1, 15, 10, 15, 0, 0, DateTimeKind.Utc), "alice.jones@example.com", "AQAAAAIAAYagAAAAEKPWJOutnj0zx2f2OU60XAEEz6aEnZTA3YOto0/OLwiwdSUb/0pSX/S3uLlYgsdbTQ==", "User", "alice_jones" },
                    { 5, new DateTime(2025, 1, 15, 10, 20, 0, 0, DateTimeKind.Utc), "bob.white@example.com", "AQAAAAIAAYagAAAAEFnJ+PJv3Z4qRQeGS1UwoAIm/uGg9rSCJSbAHSV08Xl0bpbu3jlz6M0tvD9DbZjXjA==", "Admin", "bob_white" }
                });

            migrationBuilder.InsertData(
                table: "UserPdfs",
                columns: new[] { "PdfId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPdfs_PdfId",
                table: "UserPdfs",
                column: "PdfId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPdfs");

            migrationBuilder.DropTable(
                name: "Pdfs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
