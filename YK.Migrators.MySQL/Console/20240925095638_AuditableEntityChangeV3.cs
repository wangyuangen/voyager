using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    /// <inheritdoc />
    public partial class AuditableEntityChangeV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_view_info");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_view_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_view_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_view_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_view_info");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_view_info");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_view_info");

            migrationBuilder.DropColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_view_info");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_role_info");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_role_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_role_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_role_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_role_info");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_role_info");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_role_info");

            migrationBuilder.DropColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_role_info");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_post_info");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_post_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_post_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_post_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_post_info");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_post_info");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_post_info");

            migrationBuilder.DropColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_post_info");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_package_info");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_package_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_package_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_package_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_package_info");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_package_info");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_package_info");

            migrationBuilder.DropColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_package_info");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "console",
                table: "cs_data_dict");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "console",
                table: "cs_data_dict");

            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_data_dict");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_data_dict");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_data_dict");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_data_dict");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_data_dict");

            migrationBuilder.DropColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_data_dict");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_view_info",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_view_info",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_view_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_view_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_view_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_view_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_view_info",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_view_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_role_info",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_role_info",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_role_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_role_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_role_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_role_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_role_info",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_role_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_post_info",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_post_info",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_post_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_post_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_post_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_post_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_post_info",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_post_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_permission_group",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_permission_group",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_permission_group",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_permission_group",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_permission_group",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_permission_group",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_permission_group",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_permission_group",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_package_info",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_package_info",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_package_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_package_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_package_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_package_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_package_info",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_package_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_menu_route_info",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_menu_route_info",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_menu_route_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_menu_route_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_menu_route_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_menu_route_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_menu_route_info",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_menu_route_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "console",
                table: "cs_data_dict",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "console",
                table: "cs_data_dict",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "console",
                table: "cs_data_dict",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_data_dict",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "console",
                table: "cs_data_dict",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "console",
                table: "cs_data_dict",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "console",
                table: "cs_data_dict",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_data_dict",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
