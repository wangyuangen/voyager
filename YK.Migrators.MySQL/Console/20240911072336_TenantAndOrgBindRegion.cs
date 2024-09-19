using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    /// <inheritdoc />
    public partial class TenantAndOrgBindRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeCount",
                schema: "console",
                table: "cs_organize_info");

            migrationBuilder.AddColumn<string>(
                name: "RegionCode",
                schema: "console",
                table: "cs_tenant_info",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RegionText",
                schema: "console",
                table: "cs_tenant_info",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RegionCode",
                schema: "console",
                table: "cs_organize_info",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RegionText",
                schema: "console",
                table: "cs_organize_info",
                type: "varchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegionCode",
                schema: "console",
                table: "cs_tenant_info");

            migrationBuilder.DropColumn(
                name: "RegionText",
                schema: "console",
                table: "cs_tenant_info");

            migrationBuilder.DropColumn(
                name: "RegionCode",
                schema: "console",
                table: "cs_organize_info");

            migrationBuilder.DropColumn(
                name: "RegionText",
                schema: "console",
                table: "cs_organize_info");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeCount",
                schema: "console",
                table: "cs_organize_info",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
