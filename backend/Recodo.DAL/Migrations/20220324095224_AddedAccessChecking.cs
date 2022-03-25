using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Recodo.DAL.Migrations
{
    public partial class AddedAccessChecking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Folders_ParentId",
                table: "Folders",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_ParentId",
                table: "Folders",
                column: "ParentId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.CreateTable(
                name: "AccessesForRegisteredUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    VideoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessesForRegisteredUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessesForRegisteredUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessesForRegisteredUsers_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessesForUnregisteredUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    VideoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessesForUnregisteredUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessesForUnregisteredUsers_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessesForRegisteredUsers_UserId",
                table: "AccessesForRegisteredUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessesForRegisteredUsers_VideoId",
                table: "AccessesForRegisteredUsers",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessesForUnregisteredUsers_VideoId",
                table: "AccessesForUnregisteredUsers",
                column: "VideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessesForRegisteredUsers");

            migrationBuilder.DropTable(
                name: "AccessesForUnregisteredUsers");
        }
    }
}
