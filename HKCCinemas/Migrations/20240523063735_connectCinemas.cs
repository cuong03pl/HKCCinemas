using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class connectCinemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShowDateId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShowDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubCinemaId = table.Column<int>(type: "int", nullable: false),
                    CinemasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShowDates_Cinemas_CinemasId",
                        column: x => x.CinemasId,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ShowDateId",
                table: "Schedules",
                column: "ShowDateId");

            migrationBuilder.CreateIndex(
                name: "IX_ShowDates_CinemasId",
                table: "ShowDates",
                column: "CinemasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ShowDates_ShowDateId",
                table: "Schedules",
                column: "ShowDateId",
                principalTable: "ShowDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ShowDates_ShowDateId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "ShowDates");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ShowDateId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ShowDateId",
                table: "Schedules");
        }
    }
}
