using Microsoft.EntityFrameworkCore.Migrations;

namespace KinoSite.Migrations.Movie
{
    public partial class RemoveSurname_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameDirection",
                table: "Directions");

            migrationBuilder.DropColumn(
                name: "SurnameDirection",
                table: "Directions");

            migrationBuilder.DropColumn(
                name: "NameActor",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "SurnameActor",
                table: "Actors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameDirection",
                table: "Directions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurnameDirection",
                table: "Directions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameActor",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurnameActor",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
