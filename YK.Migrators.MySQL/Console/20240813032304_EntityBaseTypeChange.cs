using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    /// <inheritdoc />
    public partial class EntityBaseTypeChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_user_staff_role");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_user_staff_role");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_user_staff_role");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_user_staff_role");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_user_staff_org");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_user_staff_org");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_user_staff_org");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_user_staff_org");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_tenant_package");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_tenant_package");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_tenant_package");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_tenant_package");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_role_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_role_permission_group");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_role_permission_group");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_role_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_role_menu_route");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_role_menu_route");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_role_menu_route");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_role_menu_route");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_permission_group_api");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_permission_group_api");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_permission_group_api");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_permission_group_api");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_package_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_package_permission_group");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_package_permission_group");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_package_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_package_menu_route");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_package_menu_route");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_package_menu_route");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_package_menu_route");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_user_staff_role",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_user_staff_role",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_user_staff_role",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_user_staff_role",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_user_staff_org",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_user_staff_org",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_user_staff_org",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_user_staff_org",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_tenant_package",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_tenant_package",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_tenant_package",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_tenant_package",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_role_permission_group",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_role_permission_group",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_role_permission_group",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_role_permission_group",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_role_menu_route",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_role_menu_route",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_role_menu_route",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_role_menu_route",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_permission_group_api",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_permission_group_api",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_permission_group_api",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_permission_group_api",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_package_permission_group",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_package_permission_group",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_package_permission_group",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_package_permission_group",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_package_menu_route",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_package_menu_route",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_package_menu_route",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_package_menu_route",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
