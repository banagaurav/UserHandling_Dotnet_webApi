using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserHandling.Migrations
{
    /// <inheritdoc />
    public partial class ReSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEKfy2SHnyPEUyf69Y0v+ElKH98SATsIpK8lf4ZwIeAI7R7LQNxrnMaSYo1ZSSwONPA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAELu5GqA1p1E9+DoIwxEtdMHxGV6C9v0wlo0IOgaguAlNi7ST9y7sGhojoFXz6Hlsww==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEMyS4dHXn92tz9T7EBc9ARXggelEk1Yxl49pKf9wLTot7jG10lCwHrXhoRgGXwo89Q==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEIRatFKFLIxmbR2Zrxu3EPCKbbfu9evOHGdVlGmRtKLV5T0DvNM29W68WoNzPer3bg==");
        }
    }
}
