using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class updateFilm146 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Film",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Background",
                table: "Film");
        }
    }
}
