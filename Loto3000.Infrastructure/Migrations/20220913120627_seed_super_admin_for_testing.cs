using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000.Infrastructure.Migrations
{
    public partial class seed_super_admin_for_testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminCredentials",
                table: "SuperAdmin");

            migrationBuilder.DropColumn(
                name: "AuthorizationCode",
                table: "SuperAdmin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminCredentials",
                table: "SuperAdmin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorizationCode",
                table: "SuperAdmin",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "SuperAdmin",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AdminCredentials", "AuthorizationCode" },
                values: new object[] { "SystemAdministrator", "123456789101112" });
        }
    }
}
