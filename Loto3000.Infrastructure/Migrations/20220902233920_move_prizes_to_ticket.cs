using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000.Infrastructure.Migrations
{
    public partial class move_prizes_to_ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prizes",
                table: "Draws");

            migrationBuilder.AddColumn<int>(
                name: "Prizes",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prizes",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "Prizes",
                table: "Draws",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
