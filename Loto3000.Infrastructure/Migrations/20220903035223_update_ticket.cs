using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000.Infrastructure.Migrations
{
    public partial class update_ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Combination_Tickets_TicketId",
                table: "Combination");

            migrationBuilder.RenameColumn(
                name: "Prizes",
                table: "Tickets",
                newName: "Prize");

            migrationBuilder.AddColumn<int>(
                name: "NumbersGuessed",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "Combination",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Combination_Tickets_TicketId",
                table: "Combination",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Combination_Tickets_TicketId",
                table: "Combination");

            migrationBuilder.DropColumn(
                name: "NumbersGuessed",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "Prize",
                table: "Tickets",
                newName: "Prizes");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "Combination",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Combination_Tickets_TicketId",
                table: "Combination",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }
    }
}
