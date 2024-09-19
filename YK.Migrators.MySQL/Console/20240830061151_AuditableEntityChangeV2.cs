using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    /// <inheritdoc />
    public partial class AuditableEntityChangeV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_view_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_user_staff_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_user_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_tenant_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_role_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_post_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_permission_group",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_package_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_organize_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_menu_route_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_data_dict_item",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_data_dict",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_api_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_view_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_user_staff_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_user_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_tenant_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_role_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_post_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_package_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_organize_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_data_dict_item");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_data_dict");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_api_info");
        }
    }
}
