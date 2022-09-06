using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000.Infrastructure.Migrations
{
    public partial class update_ticket_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Draws_DrawId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Players_PlayerId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DrawId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Draws_DrawId",
                table: "Tickets",
                column: "DrawId",
                principalTable: "Draws",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Players_PlayerId",
                table: "Tickets",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Draws_DrawId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Players_PlayerId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Tickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DrawId",
                table: "Tickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Draws_DrawId",
                table: "Tickets",
                column: "DrawId",
                principalTable: "Draws",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Players_PlayerId",
                table: "Tickets",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
