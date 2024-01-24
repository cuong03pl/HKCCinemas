using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class updateFKCommnentvfilm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilmId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FilmId",
                table: "Comment",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Film_FilmId",
                table: "Comment",
                column: "FilmId",
                principalTable: "Film",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Film_FilmId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_FilmId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "FilmId",
                table: "Comment");
        }
    }
}
