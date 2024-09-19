using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    /// <inheritdoc />
    public partial class RelationShipChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_cs_role_permission_group_RoleId",
                schema: "console",
                table: "cs_role_permission_group",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_cs_role_menu_route_RoleId",
                schema: "console",
                table: "cs_role_menu_route",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_cs_package_permission_group_PackageId",
                schema: "console",
                table: "cs_package_permission_group",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_cs_package_menu_route_PackageId",
                schema: "console",
                table: "cs_package_menu_route",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_cs_package_menu_route_cs_package_info_PackageId",
                schema: "console",
                table: "cs_package_menu_route",
                column: "PackageId",
                principalSchema: "console",
                principalTable: "cs_package_info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cs_package_permission_group_cs_package_info_PackageId",
                schema: "console",
                table: "cs_package_permission_group",
                column: "PackageId",
                principalSchema: "console",
                principalTable: "cs_package_info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cs_role_menu_route_cs_role_info_RoleId",
                schema: "console",
                table: "cs_role_menu_route",
                column: "RoleId",
                principalSchema: "console",
                principalTable: "cs_role_info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cs_role_permission_group_cs_role_info_RoleId",
                schema: "console",
                table: "cs_role_permission_group",
                column: "RoleId",
                principalSchema: "console",
                principalTable: "cs_role_info",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cs_package_menu_route_cs_package_info_PackageId",
                schema: "console",
                table: "cs_package_menu_route");

            migrationBuilder.DropForeignKey(
                name: "FK_cs_package_permission_group_cs_package_info_PackageId",
                schema: "console",
                table: "cs_package_permission_group");

            migrationBuilder.DropForeignKey(
                name: "FK_cs_role_menu_route_cs_role_info_RoleId",
                schema: "console",
                table: "cs_role_menu_route");

            migrationBuilder.DropForeignKey(
                name: "FK_cs_role_permission_group_cs_role_info_RoleId",
                schema: "console",
                table: "cs_role_permission_group");

            migrationBuilder.DropIndex(
                name: "IX_cs_role_permission_group_RoleId",
                schema: "console",
                table: "cs_role_permission_group");

            migrationBuilder.DropIndex(
                name: "IX_cs_role_menu_route_RoleId",
                schema: "console",
                table: "cs_role_menu_route");

            migrationBuilder.DropIndex(
                name: "IX_cs_package_permission_group_PackageId",
                schema: "console",
                table: "cs_package_permission_group");

            migrationBuilder.DropIndex(
                name: "IX_cs_package_menu_route_PackageId",
                schema: "console",
                table: "cs_package_menu_route");
        }
    }
}
