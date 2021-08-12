using Microsoft.EntityFrameworkCore.Migrations;

namespace MassageStudioLorem.Data.Migrations
{
    public partial class MadeUserIdNotRequiredInMasseur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Masseurs_UserId",
                table: "Masseurs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Masseurs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Masseurs_UserId",
                table: "Masseurs",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Masseurs_UserId",
                table: "Masseurs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Masseurs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Masseurs_UserId",
                table: "Masseurs",
                column: "UserId",
                unique: true);
        }
    }
}
