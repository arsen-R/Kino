using Microsoft.EntityFrameworkCore.Migrations;

namespace KinoSite.Migrations
{
    public partial class MigrationDb10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    //migrationBuilder.DropForeignKey(
        //    //    name: "FK_Movies_Directions_DirectionsId",
        //    //    table: "Movies");

        //    migrationBuilder.DropIndex(
        //        name: "IX_Movies_DirectionsId",
        //        table: "Movies");

        //    migrationBuilder.DropColumn(
        //        name: "DirectionsId",
        //        table: "Movies");

        //    migrationBuilder.AddColumn<int>(
        //        name: "DirectionId",
        //        table: "Movies",
        //        type: "int",
        //        nullable: false,
        //        defaultValue: 0);

        //    migrationBuilder.AddColumn<string>(
        //        name: "FullName",
        //        table: "Directions",
        //        type: "nvarchar(max)",
        //        nullable: true);

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Movies_DirectionId",
        //        table: "Movies",
        //        column: "DirectionId");

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_Movies_Directions_DirectionId",
        //        table: "Movies",
        //        column: "DirectionId",
        //        principalTable: "Directions",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Directions_DirectionId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_DirectionId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "DirectionId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Directions");

            migrationBuilder.AddColumn<int>(
                name: "DirectionsId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectionsId",
                table: "Movies",
                column: "DirectionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Directions_DirectionsId",
                table: "Movies",
                column: "DirectionsId",
                principalTable: "Directions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
