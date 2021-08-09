using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KinoSite.Migrations
{
    public partial class AddDirection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectionId",
                table: "Movies",
                column: "DirectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Directions_DirectionId",
                table: "Movies",
                column: "DirectionId",
                principalTable: "Directions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Directions_DirectionId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Directions");

            migrationBuilder.DropIndex(
                name: "IX_Movies_DirectionId",
                table: "Movies");
        }
    }
}
