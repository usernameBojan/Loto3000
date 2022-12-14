using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Draws",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrawNumbersString = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    DrawTime = table.Column<DateTime>(type: "datetime2", maxLength: 64, nullable: false),
                    SessionStart = table.Column<DateTime>(type: "datetime2", maxLength: 64, nullable: false),
                    SessionEnd = table.Column<DateTime>(type: "datetime2", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Draws", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonregisteredPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonregisteredPlayer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperAdmin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrawNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    DrawId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrawNumbers_Draws_DrawId",
                        column: x => x.DrawId,
                        principalTable: "Draws",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Credits = table.Column<double>(type: "float", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", maxLength: 256, nullable: false),
                    ForgotPasswordCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForgotPasswordCodeCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumbersGuessed = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    CombinationNumbersString = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TicketCreatedTime = table.Column<DateTime>(type: "datetime2", maxLength: 256, nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    DrawId = table.Column<int>(type: "int", nullable: true),
                    Prize = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NonregisteredPlayerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Draws_DrawId",
                        column: x => x.DrawId,
                        principalTable: "Draws",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_NonregisteredPlayer_NonregisteredPlayerId",
                        column: x => x.NonregisteredPlayerId,
                        principalTable: "NonregisteredPlayer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositAmount = table.Column<double>(type: "float", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NonregisteredPlayerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_NonregisteredPlayer_NonregisteredPlayerId",
                        column: x => x.NonregisteredPlayerId,
                        principalTable: "NonregisteredPlayer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Combination",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Combination_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Draws",
                columns: new[] { "Id", "DrawNumbersString", "DrawTime", "SessionEnd", "SessionStart" },
                values: new object[] { 1, "", new DateTime(2022, 10, 31, 20, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 31, 20, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 22, 20, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SuperAdmin",
                columns: new[] { "Id", "Password", "Role", "Username" },
                values: new object[] { 1, "123456789101112", "SuperAdmin", "SystemAdministrator" });

            migrationBuilder.CreateIndex(
                name: "IX_Combination_TicketId",
                table: "Combination",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_DrawNumbers_DrawId",
                table: "DrawNumbers",
                column: "DrawId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DrawId",
                table: "Tickets",
                column: "DrawId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_NonregisteredPlayerId",
                table: "Tickets",
                column: "NonregisteredPlayerId",
                unique: true,
                filter: "[NonregisteredPlayerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PlayerId",
                table: "Tickets",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_NonregisteredPlayerId",
                table: "Transactions",
                column: "NonregisteredPlayerId",
                unique: true,
                filter: "[NonregisteredPlayerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PlayerId",
                table: "Transactions",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Combination");

            migrationBuilder.DropTable(
                name: "DrawNumbers");

            migrationBuilder.DropTable(
                name: "SuperAdmin");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Draws");

            migrationBuilder.DropTable(
                name: "NonregisteredPlayer");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
