using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class updateDB256 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ShowDates_ShowDateId1",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ShowDateId1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ShowDateId1",
                table: "Schedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShowDateId1",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ShowDateId1",
                table: "Schedules",
                column: "ShowDateId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ShowDates_ShowDateId1",
                table: "Schedules",
                column: "ShowDateId1",
                principalTable: "ShowDates",
                principalColumn: "Id");
        }
    }
}
