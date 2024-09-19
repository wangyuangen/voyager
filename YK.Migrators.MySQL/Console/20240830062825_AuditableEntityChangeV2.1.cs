using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    /// <inheritdoc />
    public partial class AuditableEntityChangeV21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_view_info",
                newName: "ModifiedUserName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_user_staff_info",
                newName: "ModifiedUserName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_user_info",
                newName: "ModifiedUserName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_tenant_info",
                newName: "ModifiedUserName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_role_info",
                newName: "ModifiedUserName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_post_info",
                newName: "ModifiedUserName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_permission_group",
                newName: "ModifiedUserName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_package_info",
                newName: "ModifiedUserName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_organize_info",
                newName: "ModifiedUserName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_menu_route_info",
                newName: "ModifiedUserName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_data_dict_item",
                newName: "ModifiedUserName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_data_dict",
                newName: "ModifiedUserName");

            migrationBuilder.RenameColumn(
                name: "CreatedUserStaffName",
                schema: "console",
                table: "cs_api_info",
                newName: "ModifiedUserName");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_view_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_user_staff_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_user_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_tenant_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_role_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_post_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_permission_group",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_package_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_organize_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_menu_route_info",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_data_dict_item",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_data_dict",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
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
                name: "CreatedUserName",
                schema: "console",
                table: "cs_view_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_user_staff_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_user_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_tenant_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_role_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_post_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_permission_group");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_package_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_organize_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_data_dict_item");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_data_dict");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                schema: "console",
                table: "cs_api_info");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_view_info",
                newName: "CreatedUserStaffName");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_user_staff_info",
                newName: "CreatedUserStaffName");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_user_info",
                newName: "CreatedUserStaffName");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_tenant_info",
                newName: "CreatedUserStaffName");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_role_info",
                newName: "CreatedUserStaffName");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_post_info",
                newName: "CreatedUserStaffName");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_permission_group",
                newName: "CreatedUserStaffName");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_package_info",
                newName: "CreatedUserStaffName");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_organize_info",
                newName: "CreatedUserStaffName");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_menu_route_info",
                newName: "CreatedUserStaffName");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_data_dict_item",
                newName: "CreatedUserStaffName");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_data_dict",
                newName: "CreatedUserStaffName");

            migrationBuilder.RenameColumn(
                name: "ModifiedUserName",
                schema: "console",
                table: "cs_api_info",
                newName: "CreatedUserStaffName");
        }
    }
}
