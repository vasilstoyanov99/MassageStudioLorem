using Microsoft.EntityFrameworkCore.Migrations;

namespace MassageStudioLorem.Data.Migrations
{
    public partial class AddedFullNameCollum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Masseurs");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Masseurs");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Masseurs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Masseurs");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Masseurs",
                type: "nvarchar(26)",
                maxLength: 26,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Masseurs",
                type: "nvarchar(26)",
                maxLength: 26,
                nullable: false,
                defaultValue: "");
        }
    }
}
