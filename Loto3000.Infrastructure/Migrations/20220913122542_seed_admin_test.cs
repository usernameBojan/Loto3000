using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loto3000.Infrastructure.Migrations
{
    public partial class seed_admin_test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SuperAdmin",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "SuperAdmin",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Role", "Username" },
                values: new object[] { 2, "Super", "Admin", "123456789101112", "SuperAdmin", "SystemAdministrator" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SuperAdmin",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "SuperAdmin",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Role", "Username" },
                values: new object[] { 1, "Super", "Admin", "123456789101112", "SuperAdmin", "SystemAdministrator" });
        }
    }
}
