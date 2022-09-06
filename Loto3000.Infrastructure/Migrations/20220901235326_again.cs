using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000.Infrastructure.Migrations
{
    public partial class again : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Draws_DrawId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DrawNumbersString",
                table: "Draws");

            migrationBuilder.AlterColumn<int>(
                name: "DrawId",
                table: "Tickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "TicketCreatedTime",
                table: "Tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Draws_DrawId",
                table: "Tickets",
                column: "DrawId",
                principalTable: "Draws",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Draws_DrawId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketCreatedTime",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "DrawId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DrawNumbersString",
                table: "Draws",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Draws_DrawId",
                table: "Tickets",
                column: "DrawId",
                principalTable: "Draws",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
