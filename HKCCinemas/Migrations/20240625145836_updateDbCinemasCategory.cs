using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class updateDbCinemasCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_CinemasCategories_CinemasCategoryId1",
                table: "Cinemas");

            migrationBuilder.DropIndex(
                name: "IX_Cinemas_CinemasCategoryId1",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "CinemasCategoryId1",
                table: "Cinemas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinemasCategoryId1",
                table: "Cinemas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_CinemasCategoryId1",
                table: "Cinemas",
                column: "CinemasCategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_CinemasCategories_CinemasCategoryId1",
                table: "Cinemas",
                column: "CinemasCategoryId1",
                principalTable: "CinemasCategories",
                principalColumn: "Id");
        }
    }
}
