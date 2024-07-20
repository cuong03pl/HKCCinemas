using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class updateBookingdetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Bookings_BookingId",
                table: "BookingDetails");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "BookingId",
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

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "BookingDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_SeatId1",
                table: "BookingDetails",
                column: "SeatId1");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_TicketId1",
                table: "BookingDetails",
                column: "TicketId1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Seats_SeatId1",
                table: "BookingDetails",
                column: "SeatId1",
                principalTable: "Seats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Tickets_TicketId1",
                table: "BookingDetails",
                column: "TicketId1",
                principalTable: "Tickets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_AspNetUsers_UserID",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Seats_SeatId1",
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

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_UserID",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "SeatId1",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "TicketId1",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "BookingDetails");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "BookingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserID",
                table: "Bookings",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Bookings_BookingId",
                table: "BookingDetails",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
