using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserHandling.Migrations
{
    /// <inheritdoc />
    public partial class AddPdfTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEHxN0mxOCfiAttACyI1yKUUQWhP8GFmuPIlJomy2KiT3PUCc9FulWr0Z+O40EzX88w==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEJF2vKWYctnUJ9ArEF6ESXhI4m/KzKeUOiV+iZiqJAPYmB+HVRx4hYOQNq8qYH+qXw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEFJSZ8uRZTb21T05oUgJAdPQYhv6gueUPMAVnWdnVu8POn0Ud5MA54OAhzSKWVBLkQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEFKbIClj0LeQolNRUhlmGCbwL1Rswm2z/Ej5/0Im2We9o5X+fvd6G1miaw4LGpVpYA==");
        }
    }
}
