using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace flappyBirb_serveur.Migrations
{
    /// <inheritdoc />
    public partial class AjoutDuSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", 0, "2d18f652-5247-4a1e-96b6-f05ad66e2ea9", "max1@gmail.com", false, false, null, "MAX1@GMAIL.COM", "MAX", "AQAAAAIAAYagAAAAEGKQnEb22Kd87g1WfxGXnECjkC2jjn1xS1BtLrn/NvOTKJie/oVTt1BqREcsc6Cm9A==", null, false, "2469d434-79d3-4c3e-a583-a838be0c7dda", false, "Max" },
                    { "11111111-1111-1111-1111-111111111112", 0, "1dbeea0e-b51a-4b00-a871-9a1e800b33d0", "eva48@gmail.com", false, false, null, "EVA48@GMAIL.COM", "EVA", "AQAAAAIAAYagAAAAEBABvai0ONjggoSjsSTdRa9731UAxm7jge3C9i8yt4n7j5nH3yj86XbchOzpxHKPBA==", null, false, "1fb2e813-3fd2-449c-b246-5ce928db9d7b", false, "Eva" }
                });

            migrationBuilder.InsertData(
                table: "Score",
                columns: new[] { "Id", "Date", "IsPublic", "Pseudo", "ScoreValue", "TimeInSeconds", "UserId" },
                values: new object[,]
                {
                    { 1, "06-Apr-2025 22:08:06", true, "Max", 95, 59.26f, "11111111-1111-1111-1111-111111111111" },
                    { 2, "15-Nov-2025 09:48:26", false, "Max", 2, 5.4f, "11111111-1111-1111-1111-111111111111" },
                    { 3, "20-Feb-2025 15:37:00", true, "Eva", 89, 45.39f, "11111111-1111-1111-1111-111111111112" },
                    { 4, "31-Dec-2025 23:59:59", false, "Eva", 1, 2.1f, "11111111-1111-1111-1111-111111111112" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Score",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Score",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Score",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Score",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111112");
        }
    }
}
