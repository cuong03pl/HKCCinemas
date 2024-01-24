using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class addTableFilmActor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Film_FilmId",
                table: "Actor");

            migrationBuilder.DropIndex(
                name: "IX_Actor_FilmId",
                table: "Actor");

            migrationBuilder.DropColumn(
                name: "FilmId",
                table: "Actor");

            migrationBuilder.CreateTable(
                name: "FilmActors",
                columns: table => new
                {
                    filmId = table.Column<int>(type: "int", nullable: false),
                    actorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmActors", x => new { x.filmId, x.actorId });
                    table.ForeignKey(
                        name: "FK_FilmActors_Actor_actorId",
                        column: x => x.actorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmActors_Film_filmId",
                        column: x => x.filmId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmActors_actorId",
                table: "FilmActors",
                column: "actorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmActors");

            migrationBuilder.AddColumn<int>(
                name: "FilmId",
                table: "Actor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actor_FilmId",
                table: "Actor",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Film_FilmId",
                table: "Actor",
                column: "FilmId",
                principalTable: "Film",
                principalColumn: "Id");
        }
    }
}
