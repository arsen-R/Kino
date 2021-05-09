using Microsoft.EntityFrameworkCore.Migrations;

namespace KinoSite.Migrations
{
    public partial class MigrationDb13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Directions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Directions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
