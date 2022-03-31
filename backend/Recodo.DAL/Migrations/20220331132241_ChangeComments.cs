using Microsoft.EntityFrameworkCore.Migrations;

namespace Recodo.DAL.Migrations
{
    public partial class ChangeComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VideoId1",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VideoId1",
                table: "Comments",
                column: "VideoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Videos_VideoId1",
                table: "Comments",
                column: "VideoId1",
                principalTable: "Videos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Videos_VideoId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_VideoId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "VideoId1",
                table: "Comments");
        }
    }
}
