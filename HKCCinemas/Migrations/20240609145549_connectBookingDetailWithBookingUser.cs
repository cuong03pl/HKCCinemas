using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class connectBookingDetailWithBookingUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingUserId",
                table: "BookingDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingUserId",
                table: "BookingDetails",
                column: "BookingUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_BookingUsers_BookingUserId",
                table: "BookingDetails",
                column: "BookingUserId",
                principalTable: "BookingUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_BookingUsers_BookingUserId",
                table: "BookingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_BookingUserId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "BookingUserId",
                table: "BookingDetails");
        }
    }
}
