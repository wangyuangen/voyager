using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    /// <inheritdoc />
    public partial class UserStaffType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                schema: "console",
                table: "cs_user_info");

            migrationBuilder.AddColumn<int>(
                name: "UserStaffType",
                schema: "console",
                table: "cs_user_staff_info",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_cs_permission_group_api_PermissionGroupId",
                schema: "console",
                table: "cs_permission_group_api",
                column: "PermissionGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_cs_permission_group_api_cs_permission_group_PermissionGroupId",
                schema: "console",
                table: "cs_permission_group_api",
                column: "PermissionGroupId",
                principalSchema: "console",
                principalTable: "cs_permission_group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cs_permission_group_api_cs_permission_group_PermissionGroupId",
                schema: "console",
                table: "cs_permission_group_api");

            migrationBuilder.DropIndex(
                name: "IX_cs_permission_group_api_PermissionGroupId",
                schema: "console",
                table: "cs_permission_group_api");

            migrationBuilder.DropColumn(
                name: "UserStaffType",
                schema: "console",
                table: "cs_user_staff_info");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                schema: "console",
                table: "cs_user_info",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
