using Microsoft.EntityFrameworkCore.Migrations;

namespace MassageStudioLorem.Data.Migrations
{
    public partial class UpdatedRelationshipBetweenAppointmentAndMassage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_MassageId",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "MassageId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MassageId",
                table: "Appointments",
                column: "MassageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_MassageId",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "MassageId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MassageId",
                table: "Appointments",
                column: "MassageId",
                unique: true);
        }
    }
}
