using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class addOrderCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderCode",
                table: "BookingUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderCode",
                table: "BookingUsers");
        }
    }
}
