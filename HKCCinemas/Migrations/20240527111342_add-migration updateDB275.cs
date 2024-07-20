using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class addmigrationupdateDB275 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Bookings_BookingId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Seats_SeatId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_CinemasCategories_CinemasCategoryId",
                table: "Cinemas");

            migrationBuilder.AddColumn<int>(
                name: "CinemasId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CinemasCategoryId1",
                table: "Cinemas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CinemasId",
                table: "Rooms",
                column: "CinemasId");

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_CinemasCategoryId1",
                table: "Cinemas",
                column: "CinemasCategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_TicketId",
                table: "BookingDetails",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Bookings_BookingId",
                table: "BookingDetails",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Seats_SeatId",
                table: "BookingDetails",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Tickets_TicketId",
                table: "BookingDetails",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_CinemasCategories_CinemasCategoryId",
                table: "Cinemas",
                column: "CinemasCategoryId",
                principalTable: "CinemasCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_CinemasCategories_CinemasCategoryId1",
                table: "Cinemas",
                column: "CinemasCategoryId1",
                principalTable: "CinemasCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Cinemas_CinemasId",
                table: "Rooms",
                column: "CinemasId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Bookings_BookingId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Seats_SeatId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Tickets_TicketId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_CinemasCategories_CinemasCategoryId",
                table: "Cinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_CinemasCategories_CinemasCategoryId1",
                table: "Cinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Cinemas_CinemasId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_CinemasId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Cinemas_CinemasCategoryId1",
                table: "Cinemas");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_TicketId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "CinemasId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CinemasCategoryId1",
                table: "Cinemas");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Bookings_BookingId",
                table: "BookingDetails",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Seats_SeatId",
                table: "BookingDetails",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_CinemasCategories_CinemasCategoryId",
                table: "Cinemas",
                column: "CinemasCategoryId",
                principalTable: "CinemasCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
