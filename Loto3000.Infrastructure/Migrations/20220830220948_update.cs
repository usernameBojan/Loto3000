using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000.Infrastructure.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Draws_Session_SessionId",
                table: "Draws");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Draws_SessionId",
                table: "Draws");

            migrationBuilder.DropColumn(
                name: "DrawSequenceNumber",
                table: "Draws");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Draws");

            migrationBuilder.AddColumn<DateTime>(
                name: "SessionEnd",
                table: "Draws",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SessionStart",
                table: "Draws",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionEnd",
                table: "Draws");

            migrationBuilder.DropColumn(
                name: "SessionStart",
                table: "Draws");

            migrationBuilder.AddColumn<int>(
                name: "DrawSequenceNumber",
                table: "Draws",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Draws",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Draws_SessionId",
                table: "Draws",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Draws_Session_SessionId",
                table: "Draws",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id");
        }
    }
}
