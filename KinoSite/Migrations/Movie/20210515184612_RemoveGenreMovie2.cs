using Microsoft.EntityFrameworkCore.Migrations;

namespace KinoSite.Migrations.Movie
{
    public partial class RemoveGenreMovie2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreMovies");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_GenreId",
                table: "Genres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_MovieId",
                table: "Genres",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Genres_GenreId",
                table: "Genres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Movies_MovieId",
                table: "Genres",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Genres_GenreId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Movies_MovieId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_GenreId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_MovieId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Genres");

            migrationBuilder.CreateTable(
                name: "GenreMovies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovies", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_GenreMovies_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovies_GenreId",
                table: "GenreMovies",
                column: "GenreId");
        }
    }
}
