using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Recodo.DAL.Migrations
{
    public partial class AddedSharedEmails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "SharedEmails",
                table: "Videos",
                type: "text[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharedEmails",
                table: "Videos");
        }
    }
}
