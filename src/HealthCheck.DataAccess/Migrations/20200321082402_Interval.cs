using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthCheck.DataAccess.Migrations
{
    public partial class Interval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Interval",
                table: "Apps",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "Apps");
        }
    }
}
