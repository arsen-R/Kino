using Microsoft.EntityFrameworkCore.Migrations;

namespace KinoSite.Migrations
{
    public partial class AddDirectionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectionId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectionId",
                table: "Movies");
        }
    }
}
