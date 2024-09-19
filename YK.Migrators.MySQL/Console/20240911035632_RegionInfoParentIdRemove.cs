using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Console
{
    /// <inheritdoc />
    public partial class RegionInfoParentIdRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cs_region_info_cs_region_info_ParentId",
                schema: "console",
                table: "cs_region_info");

            migrationBuilder.DropIndex(
                name: "IX_cs_region_info_ParentId",
                schema: "console",
                table: "cs_region_info");

            migrationBuilder.DropColumn(
                name: "ParentId",
                schema: "console",
                table: "cs_region_info");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                schema: "console",
                table: "cs_region_info",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_cs_region_info_ParentId",
                schema: "console",
                table: "cs_region_info",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_cs_region_info_cs_region_info_ParentId",
                schema: "console",
                table: "cs_region_info",
                column: "ParentId",
                principalSchema: "console",
                principalTable: "cs_region_info",
                principalColumn: "Id");
        }
    }
}
