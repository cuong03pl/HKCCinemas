using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class updateBookingDetail96 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_AspNetUsers_UserID",
                table: "BookingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_UserID",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "BookingDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "BookingDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_UserID",
                table: "BookingDetails",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_AspNetUsers_UserID",
                table: "BookingDetails",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
