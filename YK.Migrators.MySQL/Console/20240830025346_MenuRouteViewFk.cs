using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    /// <inheritdoc />
    public partial class MenuRouteViewFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cs_menu_route_info_cs_view_info_ViewInfoId",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropIndex(
                name: "IX_cs_menu_route_info_ViewInfoId",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropColumn(
                name: "ViewInfoId",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.CreateIndex(
                name: "IX_cs_menu_route_info_ViewId",
                schema: "console",
                table: "cs_menu_route_info",
                column: "ViewId");

            migrationBuilder.AddForeignKey(
                name: "FK_cs_menu_route_info_cs_view_info_ViewId",
                schema: "console",
                table: "cs_menu_route_info",
                column: "ViewId",
                principalSchema: "console",
                principalTable: "cs_view_info",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cs_menu_route_info_cs_view_info_ViewId",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.DropIndex(
                name: "IX_cs_menu_route_info_ViewId",
                schema: "console",
                table: "cs_menu_route_info");

            migrationBuilder.AddColumn<Guid>(
                name: "ViewInfoId",
                schema: "console",
                table: "cs_menu_route_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_cs_menu_route_info_ViewInfoId",
                schema: "console",
                table: "cs_menu_route_info",
                column: "ViewInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_cs_menu_route_info_cs_view_info_ViewInfoId",
                schema: "console",
                table: "cs_menu_route_info",
                column: "ViewInfoId",
                principalSchema: "console",
                principalTable: "cs_view_info",
                principalColumn: "Id");
        }
    }
}
