using Microsoft.EntityFrameworkCore.Migrations;

namespace MassageStudioLorem.Data.Migrations
{
    public partial class AddedDefaultHourScheduleAndRemovedMasseursAvailableHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasseursAvailableHours");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Appointments",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "Hour",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DefaultHoursSchedule",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultHoursSchedule", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultHoursSchedule");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Appointments",
                newName: "DateTime");

            migrationBuilder.CreateTable(
                name: "MasseursAvailableHours",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasseurId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasseursAvailableHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasseursAvailableHours_Masseurs_MasseurId",
                        column: x => x.MasseurId,
                        principalTable: "Masseurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasseursAvailableHours_MasseurId",
                table: "MasseursAvailableHours",
                column: "MasseurId");
        }
    }
}
