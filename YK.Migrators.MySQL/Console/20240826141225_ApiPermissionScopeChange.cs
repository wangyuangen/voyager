using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    /// <inheritdoc />
    public partial class ApiPermissionScopeChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                schema: "console",
                table: "cs_permission_group",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "console",
                table: "cs_api_info",
                type: "varchar(128)",
                maxLength: 128,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "console",
                table: "cs_api_info");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "console",
                table: "cs_permission_group",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
