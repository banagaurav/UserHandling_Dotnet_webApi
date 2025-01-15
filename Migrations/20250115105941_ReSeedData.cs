using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserHandling.Migrations
{
    /// <inheritdoc />
    public partial class ReSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Utc), "admin@example.com", "AQAAAAIAAYagAAAAEPqdv6roU0tq0tdeqqJmUcvhbcGr7wYqZ8bAXctCaNpXY9U13EZFsP1uY8Nstv1b3g==", "Admin", "admin" },
                    { 2, new DateTime(2025, 1, 15, 10, 5, 0, 0, DateTimeKind.Utc), "user1@example.com", "AQAAAAIAAYagAAAAEPNbzArmnxj5q8gQYhuxuBSnwQUGsKVdD4a1NnqpTvnPePIaI/ynSEHtrms8wXTgiw==", "User", "user1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
