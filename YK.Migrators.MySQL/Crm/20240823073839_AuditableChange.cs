using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YK.Migrators.MySQL.Crm
{
    /// <inheritdoc />
    public partial class AuditableChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedOrgBy",
                schema: "crm",
                table: "crm_customer",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUserStaffBy",
                schema: "crm",
                table: "crm_customer",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOrgBy",
                schema: "crm",
                table: "crm_customer");

            migrationBuilder.DropColumn(
                name: "CreatedUserStaffBy",
                schema: "crm",
                table: "crm_customer");
        }
    }
}
