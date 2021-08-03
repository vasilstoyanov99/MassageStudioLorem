using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MassageStudioLorem.Data.Migrations
{
    public partial class RemovedBookedOnColumnInAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookedOn",
                table: "Appointments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BookedOn",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
