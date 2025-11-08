using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flappyBirb_serveur.Migrations
{
    /// <inheritdoc />
    public partial class ajoutliesonentreTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Score",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Score_UserId",
                table: "Score",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_AspNetUsers_UserId",
                table: "Score",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_AspNetUsers_UserId",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Score_UserId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Score");
        }
    }
}
