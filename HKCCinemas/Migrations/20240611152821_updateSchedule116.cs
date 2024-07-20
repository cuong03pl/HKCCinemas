using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class updateSchedule116 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Film_FilmId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ShowDates_ShowDateId",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "ShowDateId1",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RoomId",
                table: "Schedules",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ShowDateId1",
                table: "Schedules",
                column: "ShowDateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Film_FilmId",
                table: "Schedules",
                column: "FilmId",
                principalTable: "Film",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Rooms_RoomId",
                table: "Schedules",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ShowDates_ShowDateId",
                table: "Schedules",
                column: "ShowDateId",
                principalTable: "ShowDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ShowDates_ShowDateId1",
                table: "Schedules",
                column: "ShowDateId1",
                principalTable: "ShowDates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Film_FilmId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Rooms_RoomId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ShowDates_ShowDateId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ShowDates_ShowDateId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_RoomId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ShowDateId1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ShowDateId1",
                table: "Schedules");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Film_FilmId",
                table: "Schedules",
                column: "FilmId",
                principalTable: "Film",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ShowDates_ShowDateId",
                table: "Schedules",
                column: "ShowDateId",
                principalTable: "ShowDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
