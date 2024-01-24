using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HKCCinemas.Migrations
{
    public partial class updateDBShowTimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTime_Category_CategoryId",
                table: "ShowTime");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ShowTime",
                newName: "CinemasId");

            migrationBuilder.RenameIndex(
                name: "IX_ShowTime_CategoryId",
                table: "ShowTime",
                newName: "IX_ShowTime_CinemasId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTime_Cinemas_CinemasId",
                table: "ShowTime",
                column: "CinemasId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTime_Cinemas_CinemasId",
                table: "ShowTime");

            migrationBuilder.RenameColumn(
                name: "CinemasId",
                table: "ShowTime",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ShowTime_CinemasId",
                table: "ShowTime",
                newName: "IX_ShowTime_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTime_Category_CategoryId",
                table: "ShowTime",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
