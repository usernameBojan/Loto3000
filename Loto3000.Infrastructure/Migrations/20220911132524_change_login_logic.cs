using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000.Infrastructure.Migrations
{
    public partial class change_login_logic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminRole");

            migrationBuilder.DropTable(
                name: "PlayerRole");

            migrationBuilder.DropTable(
                name: "RoleSuperAdmin");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropColumn(
                name: "AdminCredentials",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "SuperAdminCredentials",
                table: "SuperAdmin",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "AuthorizationCode",
                table: "Admins",
                newName: "Role");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "SuperAdmin",
                newName: "SuperAdminCredentials");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Admins",
                newName: "AuthorizationCode");

            migrationBuilder.AddColumn<string>(
                name: "AdminCredentials",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminRole",
                columns: table => new
                {
                    AdminsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRole", x => new { x.AdminsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_AdminRole_Admins_AdminsId",
                        column: x => x.AdminsId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminRole_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerRole",
                columns: table => new
                {
                    PlayersId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRole", x => new { x.PlayersId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_PlayerRole_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerRole_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleSuperAdmin",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    SuperAdminsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleSuperAdmin", x => new { x.RolesId, x.SuperAdminsId });
                    table.ForeignKey(
                        name: "FK_RoleSuperAdmin_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleSuperAdmin_SuperAdmin_SuperAdminsId",
                        column: x => x.SuperAdminsId,
                        principalTable: "SuperAdmin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Administrator" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Player" });

            migrationBuilder.CreateIndex(
                name: "IX_AdminRole_RolesId",
                table: "AdminRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRole_RolesId",
                table: "PlayerRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleSuperAdmin_SuperAdminsId",
                table: "RoleSuperAdmin",
                column: "SuperAdminsId");
        }
    }
}
