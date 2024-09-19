using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    /// <inheritdoc />
    public partial class AuditableEntityChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_view_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_view_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_user_staff_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_user_staff_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_user_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_user_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_tenant_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_tenant_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_role_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_role_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_post_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_post_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_permission_group",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_permission_group",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_package_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_package_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_organize_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_organize_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_menu_route_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_menu_route_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_api_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_api_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_view_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_view_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_user_staff_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_user_staff_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_user_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_user_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_tenant_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_tenant_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_role_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_role_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_post_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_post_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_package_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_package_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_organize_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_organize_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_api_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_api_info");
        }
    }
}
