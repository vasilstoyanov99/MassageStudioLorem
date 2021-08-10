using Microsoft.EntityFrameworkCore.Migrations;

namespace MassageStudioLorem.Data.Migrations
{
    public partial class RemovedMasseurRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatersCount",
                table: "Masseurs");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Masseurs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatersCount",
                table: "Masseurs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Masseurs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
