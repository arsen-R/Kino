using Microsoft.EntityFrameworkCore.Migrations;

namespace KinoSite.Migrations.Movie
{
    public partial class MG3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GenreMovies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "GenreMovies");
        }
    }
}
