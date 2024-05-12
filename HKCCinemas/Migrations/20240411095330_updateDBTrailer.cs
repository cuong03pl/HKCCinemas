using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class updateDBTrailer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trailers_FilmId",
                table: "Trailers");

            migrationBuilder.DropColumn(
                name: "Trailer1",
                table: "Trailers");

            migrationBuilder.DropColumn(
                name: "Trailer2",
                table: "Trailers");

            migrationBuilder.RenameColumn(
                name: "Trailer3",
                table: "Trailers",
                newName: "link");

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_FilmId",
                table: "Trailers",
                column: "FilmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trailers_FilmId",
                table: "Trailers");

            migrationBuilder.RenameColumn(
                name: "link",
                table: "Trailers",
                newName: "Trailer3");

            migrationBuilder.AddColumn<string>(
                name: "Trailer1",
                table: "Trailers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trailer2",
                table: "Trailers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trailers_FilmId",
                table: "Trailers",
                column: "FilmId",
                unique: true);
        }
    }
}
