using Microsoft.EntityFrameworkCore.Migrations;

namespace KinoSite.Migrations
{
    public partial class AddUserToSubComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "SubComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "SubComments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubComments_ApplicationUserId",
                table: "SubComments",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubComments_AspNetUsers_ApplicationUserId",
                table: "SubComments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubComments_AspNetUsers_ApplicationUserId",
                table: "SubComments");

            migrationBuilder.DropIndex(
                name: "IX_SubComments_ApplicationUserId",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "SubComments");
        }
    }
}
