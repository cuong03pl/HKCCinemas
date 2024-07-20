using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class updateBookingdetail66 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Seats_SeatId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Seats_SeatId1",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Tickets_TicketId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Tickets_TicketId1",
                table: "BookingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_SeatId1",
                table: "BookingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_TicketId1",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "SeatId1",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "TicketId1",
                table: "BookingDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Seats_SeatId",
                table: "BookingDetails",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Tickets_TicketId",
                table: "BookingDetails",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Seats_SeatId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Tickets_TicketId",
                table: "BookingDetails");

            migrationBuilder.AddColumn<int>(
                name: "SeatId1",
                table: "BookingDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketId1",
                table: "BookingDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_SeatId1",
                table: "BookingDetails",
                column: "SeatId1");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_TicketId1",
                table: "BookingDetails",
                column: "TicketId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Seats_SeatId",
                table: "BookingDetails",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Seats_SeatId1",
                table: "BookingDetails",
                column: "SeatId1",
                principalTable: "Seats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Tickets_TicketId",
                table: "BookingDetails",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Tickets_TicketId1",
                table: "BookingDetails",
                column: "TicketId1",
                principalTable: "Tickets",
                principalColumn: "Id");
        }
    }
}
