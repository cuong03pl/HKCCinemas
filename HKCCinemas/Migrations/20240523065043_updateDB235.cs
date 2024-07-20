using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class updateDB235 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "CinemasCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CinemasCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CinemasCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Cinemas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialOffer",
                table: "Cinemas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Background",
                table: "CinemasCategories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CinemasCategories");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "CinemasCategories");

            migrationBuilder.DropColumn(
                name: "Background",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "SpecialOffer",
                table: "Cinemas");
        }
    }
}
