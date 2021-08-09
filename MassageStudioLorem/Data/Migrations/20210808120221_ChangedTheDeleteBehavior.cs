using Microsoft.EntityFrameworkCore.Migrations;

namespace MassageStudioLorem.Data.Migrations
{
    public partial class ChangedTheDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Massages_Categories_CategoryId",
                table: "Massages");

            migrationBuilder.AddForeignKey(
                name: "FK_Massages_Categories_CategoryId",
                table: "Massages",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Massages_Categories_CategoryId",
                table: "Massages");

            migrationBuilder.AddForeignKey(
                name: "FK_Massages_Categories_CategoryId",
                table: "Massages",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
